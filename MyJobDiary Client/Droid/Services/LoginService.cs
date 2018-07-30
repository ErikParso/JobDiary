using Android.App;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid.Services
{
    public class LoginService : ILoginService
    {
        private FormsApplicationActivity _mainActivity;

        public MobileServiceUser User { get; private set; }

        public LoginService(FormsApplicationActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                User = await TodoItemManager.Current.Value.CurrentClient.LoginAsync(_mainActivity,
                    MobileServiceAuthenticationProvider.Google, "myjobdiary");
                if (User != null)
                {
                    success = true;
                }
            }
            catch (Exception) { }

            return success;
        }
    }
}