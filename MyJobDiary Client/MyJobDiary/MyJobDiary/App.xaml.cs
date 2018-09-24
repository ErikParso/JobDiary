using MyJobDiary.Services;
using MyJobDiary.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyJobDiary
{
    public partial class App : Application
    {
        public static ILoginService LoginService { get; private set; }
        public static ILoadingService LoadingService { get; private set; }
        public static IDialogService DialogService { get; private set; }

        public App()
        {
            MainPage = new LoginPage();
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