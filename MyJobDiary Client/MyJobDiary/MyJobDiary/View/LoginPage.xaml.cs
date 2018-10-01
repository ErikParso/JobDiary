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
            _viewModel.LoggedIn = (info) => LoggedIn(info.Item1, info.Item2);
        }

        private void LoggedIn(string userName, string photoUrl)
        {
            var masterViewModel = new MasterViewModel(userName, photoUrl);
            App.Current.MainPage = new MasterDetailPage(masterViewModel);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Login();
        }
    }
}