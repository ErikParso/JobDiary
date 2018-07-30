using MyJobDiary.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyJobDiary
{
    public class App : Application
	{

        public static ILoginService LoginService { get; private set; }
        public static ILoadingService LoadingService { get; private set; }

        public App ()
		{
			// The root page of your application
			MainPage = new NavigationPage(new MainPage());
		}

        public static void InitLoginService(ILoginService loginService)
        {
            LoginService = loginService;
        }

        public static void InitLoadingService(ILoadingService loadingService)
        {
            LoadingService = loadingService;
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

