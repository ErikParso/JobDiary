using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;
using MyJobDiary.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoginService : ILoginService
    {
        #region Private fields

        private readonly FormsAppCompatActivity _mainActivity;
        private readonly AccountStore _accountStore;

        #endregion


        #region Constructor

        public LoginService(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
            _accountStore = AccountStore.Create(mainActivity, "bobik");
        }

        #endregion


        #region Login, Logout

        public async Task<bool> Login(MobileServiceClient client)
        {
            client.CurrentUser = RetrieveTokenFromSecureStore();
            if (client.CurrentUser != null)
            {
                try
                {
                    var refreshed = await client.RefreshUserAsync();
                    if (refreshed != null)
                    {
                        client.CurrentUser = refreshed;
                        StoreTokenInSecureStore(refreshed);
                        return true;
                    }
                }
                catch (Exception)
                {

                }
            }

            if (client.CurrentUser != null && !IsTokenExpired(client.CurrentUser.MobileServiceAuthenticationToken))
            {
                // User has previously been authenticated, no refresh is required
                return true;
            }

            // We need to ask for credentials at this point
            await LoginAsync(client);
            if (client.CurrentUser != null)
            {
                // We were able to successfully log in
                StoreTokenInSecureStore(client.CurrentUser);
            }

            return client.CurrentUser != null;
        }

        public async Task Logout(MobileServiceClient client)
        {
            if (client.CurrentUser == null || client.CurrentUser.MobileServiceAuthenticationToken == null)
                return;

            // Invalidate the token on the mobile backend
            var authUri = new Uri($"{client.MobileAppUri}/.auth/logout");
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("X-ZUMO-AUTH", client.CurrentUser.MobileServiceAuthenticationToken);
                await httpClient.GetAsync(authUri);
            }

            // Remove the token from the cache
            RemoveTokenFromSecureStore();

            // Remove the token from the MobileServiceClient
            await client.LogoutAsync();
        }

        #endregion


        #region Private helpers

        private MobileServiceUser RetrieveTokenFromSecureStore()
        {
            var accounts = _accountStore.FindAccountsForService("myjobdiary");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    if (acct.Properties.TryGetValue("token", out string token))
                    {
                        return new MobileServiceUser(acct.Username)
                        {
                            MobileServiceAuthenticationToken = token
                        };
                    }
                }
            }
            return null;
        }

        private void StoreTokenInSecureStore(MobileServiceUser user)
        {
            var account = new Account(user.UserId);
            account.Properties.Add("token", user.MobileServiceAuthenticationToken);
            _accountStore.Save(account, "myjobdiary");
        }

        private void RemoveTokenFromSecureStore()
        {
            var accounts = _accountStore.FindAccountsForService("myjobdiary");
            if (accounts != null)
            {
                foreach (var acct in accounts)
                {
                    _accountStore.Delete(acct, "myjobdiary");
                }
            }
        }

        private async Task<MobileServiceUser> LoginAsync(MobileServiceClient client)
        {
            // Server-Flow Version
             return await client.LoginAsync(
                _mainActivity, MobileServiceAuthenticationProvider.Google, "myjobdiary", new Dictionary<string, string>
                {
                    { "access_type", "offline" },
                    { "prompt", "consent" }
                });
        }

        private bool IsTokenExpired(string token)
        {
            // Get just the JWT part of the token (without the signature).
            var jwt = token.Split(new Char[] { '.' })[1];

            // Undo the URL encoding.
            jwt = jwt.Replace('-', '+').Replace('_', '/');
            switch (jwt.Length % 4)
            {
                case 0: break;
                case 2: jwt += "=="; break;
                case 3: jwt += "="; break;
                default:
                    throw new ArgumentException("The token is not a valid Base64 string.");
            }
            var bytes = Convert.FromBase64String(jwt);
            string jsonString = UTF8Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            // Parse as JSON object and get the exp field value,
            // which is the expiration date as a JavaScript primative date.
            JObject jsonObj = JObject.Parse(jsonString);
            var exp = Convert.ToDouble(jsonObj["exp"].ToString());

            // Calculate the expiration by adding the exp value (in seconds) to the
            // base date of 1/1/1970.
            DateTime minTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var expire = minTime.AddSeconds(exp);
            return (expire < DateTime.UtcNow);
        }

        #endregion
    }
}