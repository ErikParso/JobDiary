using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Droid.Services;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace MyJobDiary.Droid
{
    [Activity (Label = "MyJobDiary.Droid",
		Icon = "@drawable/icon",
		MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		Theme = "@style/MyCustomTheme")]
	public class MainActivity : FormsApplicationActivity
    {

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Initialize Azure Mobile Apps
			CurrentPlatform.Init();

			// Initialize Xamarin Forms
			Forms.Init (this, bundle);

            //Init App services
            App.SetLoginService(new LoginService(this));
            App.SetLoadingService(new LoadingService(this));
            App.SetDialogService(new DialogService(this));

            // Load the main application
            LoadApplication (new App ());
		}
    }
}

