using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Services;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid
{
    public class AndroidLoginService : ILoginService
    {
        private FormsApplicationActivity _mainActivity;

        public MobileServiceUser User { get; private set; }

        public AndroidLoginService(FormsApplicationActivity mainActivity)
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
                User = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(_mainActivity,
                    MobileServiceAuthenticationProvider.Google, "myjobdiary");
                if (User != null)
                {
                    message = string.Format("you are now signed-in as {0}.",
                        User.UserId);
                    success = true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(_mainActivity);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }
    }
}