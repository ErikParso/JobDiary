
using Android.App;
using Android.Content.PM;
using Android.OS;
using MyJobDiary.Droid.Services;
using Xamarin.Forms;

namespace MyJobDiary.Droid
{
    [Activity(Label = "MyJobDiary", Icon = "@mipmap/icon", Theme = "@style/AppTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Forms.Init(this, savedInstanceState);

            App.SetLoginService(new LoginService(this));
            App.SetLoadingService(new LoadingService(this));
            App.SetDialogService(new DialogService(this));

            LoadApplication(new App());
        }
    }
}