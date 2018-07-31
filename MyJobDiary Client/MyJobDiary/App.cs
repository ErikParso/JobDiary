using MyJobDiary.Services;
using System;
using Xamarin.Forms;

namespace MyJobDiary
{
    public class App : Application
    {
        public static ILoginService LoginService { get; private set; }
        public static ILoadingService LoadingService { get; private set; }
        public static IDialogService DialogService { get; private set; }

        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new MainPage());
        }

        public static void SetLoginService(ILoginService loginService)
        {
            LoginService = loginService;
        }

        public static void SetLoadingService(ILoadingService loadingService)
        {
            LoadingService = loadingService;
        }

        public static void SetDialogService(IDialogService dialogService)
        {
            DialogService = dialogService;
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

