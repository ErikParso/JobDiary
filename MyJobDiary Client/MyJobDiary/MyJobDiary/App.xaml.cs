using Autofac;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using MyJobDiary.View;
using MyJobDiary.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyJobDiary
{
    public partial class App : Application
    {
        private readonly IContainer _container;

        public App(Action<ContainerBuilder> registerPlatformSpecific)
        {
            ContainerBuilder builder = new ContainerBuilder();
            RegisterShared(builder);
            registerPlatformSpecific?.Invoke(builder);
            _container = builder.Build();

            MainPage = _container.Resolve<LoginPage>();
        }

        public void RegisterShared(ContainerBuilder builder)
        {
            //MobileServiceClient
            builder.RegisterInstance(new MobileServiceClient(Constants.ApplicationURL));
            //Services
            builder.RegisterType<AccountInformationService>().SingleInstance().As<IAccountInformationService>();
            builder.RegisterType<DietCalculationService>().SingleInstance().As<IDietCalculationService>();
            builder.RegisterType<LocationService>().SingleInstance().As<ILocationService>();
            builder.RegisterType<ValidationService>().SingleInstance().As<IValidationService>();
            builder.RegisterType<FastInsertService>().SingleInstance().As<IFastInsertService>();
            //Managers
            builder.RegisterType<CachedTableManager<Shift>>().SingleInstance();
            builder.RegisterType<CachedTableManager<DietPaymentItem>>().SingleInstance();
            //ViewModels
            builder.RegisterType<LoginViewModel>().SingleInstance();
            builder.RegisterType<MasterViewModel>().SingleInstance();
            builder.RegisterType<ShiftListViewModel>().SingleInstance();
            builder.RegisterType<ShiftFormViewModel>();
            builder.RegisterType<AttendanceListViewModel>().SingleInstance();
            builder.RegisterType<DietsPaymentViewModel>().SingleInstance();
            builder.RegisterType<MainPageViewModel>().SingleInstance();
            //Views
            builder.RegisterType<LoginPage>().SingleInstance();
        }

        public static IContainer CurrentAppContainer => ((App)Current)._container;

    }
}