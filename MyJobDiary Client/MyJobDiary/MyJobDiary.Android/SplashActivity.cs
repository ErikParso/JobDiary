using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Xamarin.Forms;

namespace MyJobDiary.Droid
{
    [Activity(Theme = "@style/Splash", Label = "Pracovný diár", Icon = "@mipmap/icon", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.ApplicationContext, typeof(MainActivity)));
        }
    }
}