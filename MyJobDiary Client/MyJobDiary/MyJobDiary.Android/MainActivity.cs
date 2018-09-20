using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using MyJobDiary.Droid.Services;
using Xamarin.Forms;

namespace MyJobDiary.Droid
{
    [Activity(Theme = "@style/AppTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            SetServices();
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void SetServices()
        {
            App.SetLoginService(new LoginService(this));
            App.SetLoadingService(new LoadingService(this));
            App.SetDialogService(new DialogService(this));
        }
    }
}