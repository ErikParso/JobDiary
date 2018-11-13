using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Autofac;
using MyJobDiary.Droid.Services;
using MyJobDiary.Services;
using Xamarin.Droid.Utils.Services;
using Xamarin.Forms;
using Xamarin.Forms.Utils.Services;

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
            App app = new App(RegisterPlatformSpecificTypes);
            LoadApplication(app);
        }

        private void RegisterPlatformSpecificTypes(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>()
                .WithParameter(new TypedParameter(typeof(Context), this))
                .WithParameter("uriScheme", "myjobdiary")
                .WithParameter("accountStorePassword", "bobik")
                .SingleInstance();
            containerBuilder.RegisterInstance(new LoadingService(this)).As<ILoadingService>();
            containerBuilder.RegisterInstance(new DialogService(this)).As<IDialogService>();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}