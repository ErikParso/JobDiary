using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid
{
    [Activity(Label = "MyJobDiary.Droid",
            Icon = "@drawable/icon",
            MainLauncher = true,
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
            Theme = "@android:style/Theme.Holo.Light")]
    public class LoginActivity : FormsApplicationActivity, ILoginService
    {
        public MobileServiceUser User { get; private set; }

        public async Task<bool> Authenticate()
        {
            var success = false;
            var message = string.Empty;
            try
            {
                // Sign in with Facebook login using a server-managed flow.
                User = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this,
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
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Initialize Azure Mobile Apps
            CurrentPlatform.Init();

            // Initialize Xamarin Forms
            Forms.Init(this, bundle);

            // Initialize the authenticator before loading the app.
            App.InitLoginService(this);

            // Load the main application
            LoadApplication(new App());
        }
    }
}