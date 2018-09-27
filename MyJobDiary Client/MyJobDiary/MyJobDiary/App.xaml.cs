using Autofac;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using MyJobDiary.View;
using MyJobDiary.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyJobDiary
{
    public partial class App : Application
    {
        public static IContainer Container { get; set; }

        public static MobileServiceClient Client { get; private set; } =
            new MobileServiceClient(Constants.ApplicationURL);

        public App()
        {
            MainPage = Container.Resolve<LoginPage>();
        }

        public static void InitConatiner(
            ILoginService loginService,
            ILoadingService loadingService,
            IDialogService dialogService)
        {
            ContainerBuilder builder = new ContainerBuilder();
            //MobileServiceClient
            builder.RegisterInstance(Client).As<IMobileServiceClient>();
            //Services
            builder.RegisterInstance(loginService);
            builder.RegisterInstance(loadingService);
            builder.RegisterInstance(dialogService);
            builder.RegisterType<DietCalculationService>().SingleInstance().As<IDietCalculationService>();
            builder.RegisterType<LocationService>().SingleInstance().As<ILocationService>();
            builder.RegisterType<ValidationService>().SingleInstance().As<IValidationService>();
            //Managers
            builder.RegisterType<CachedTableManager<Shift>>().SingleInstance();
            builder.RegisterType<CachedTableManager<DietPaymentItem>>().SingleInstance();
            //ViewModels
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<ShiftListViewModel>().SingleInstance();
            builder.RegisterType<ShiftFormViewModel>();
            builder.RegisterType<AttendanceListViewModel>().SingleInstance();
            builder.RegisterType<DietsPaymentViewModel>().SingleInstance();
            builder.RegisterType<MainPageViewModel>().SingleInstance();
            //Views
            builder.RegisterType<LoginPage>().SingleInstance();

            Container = builder.Build();
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