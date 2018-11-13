using Autofac;
using MyJobDiary.ViewModel;
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
            _viewModel.LoginSuccessfull = Authenticated;
            _viewModel.AuthenticateSuccessfull = Authenticated;
        }

        private void Authenticated()
        {
            var masterViewModel = App.CurrentAppContainer.Resolve<MasterViewModel>();
            App.Current.MainPage = new MasterDetailPage(masterViewModel);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Authenticate();
        }
    }
}