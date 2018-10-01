using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoginService : ILoginService
    {
        private readonly FormsAppCompatActivity _mainActivity;
        private readonly AccountStore _accountStore;

        public LoginService(FormsAppCompatActivity mainActivity)
        {
            _mainActivity = mainActivity;
            _accountStore = AccountStore.Create(mainActivity, "bobik");
        }

        public async Task<bool> Login(MobileServiceClient client)
        {
            var accounts = _accountStore.FindAccountsForService("myjobdiary");
            if (accounts != null && accounts.Count() > 0)
            {
                foreach (var acct in accounts)
                {
                    if (acct.Properties.TryGetValue("token", out string token))
                    {
                        if (!IsTokenExpired(token))
                        {
                            client.CurrentUser = new MobileServiceUser(acct.Username);
                            client.CurrentUser.MobileServiceAuthenticationToken = token;
                            break;
                        }
                    }
                }
            }
            else
            {
                client.CurrentUser = await client.LoginAsync(
                    _mainActivity, MobileServiceAuthenticationProvider.Google, "myjobdiary");
                var account = new Account(client.CurrentUser.UserId);
                account.Properties.Add("token", client.CurrentUser.MobileServiceAuthenticationToken);
                _accountStore.Save(account, "myjobdiary");
            }
            return (client.CurrentUser != null);
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
    }
}