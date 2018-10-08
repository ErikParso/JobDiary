using Autofac;
using MyJobDiary.ViewModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private readonly LoginViewModel _viewModel;

        public LoginPage(LoginViewModel loginViewModel)
		{
			InitializeComponent();
            _viewModel = loginViewModel;
            BindingContext = loginViewModel;
            AddHandlers();
		}

        private void AddHandlers()
        {
            _viewModel.LoginSuccessfull = LoggedIn;
        }

        private void LoggedIn()
        {
            var masterViewModel = App.Container.Resolve<MasterViewModel>();
            App.Current.MainPage = new MasterDetailPage(masterViewModel);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Login();
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            await btnLogin.TranslateTo(10, 0, 250, Easing.BounceIn);
            await btnLogin.TranslateTo(0, 0, 100);
        }
    }
}