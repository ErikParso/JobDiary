using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        private bool _isAutenthicated;
        public bool IsAuthenticated
        {
            get => _isAutenthicated;
            set => SetField(ref _isAutenthicated, value);
        }

        private bool _isLoginAvailable;
        public bool IsLoginAvailable
        {
            get => _isLoginAvailable;
            set => SetField(ref _isLoginAvailable, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetField(ref _userName, value);
        }

        public ICommand LoginCommand { get; private set; }

        public MainPageViewModel()
        {
            LoginCommand = new Command(Login);
            IsLoginAvailable = true;
        }

        public async void Login()
        {
            IsLoginAvailable = false;
            App.LoadingService.StartLoading("prebieha prihlasovanie");
            try
            {
                IsAuthenticated = await App.LoginService.Authenticate();
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa autentifikovať", e.Message);
            }
            App.LoadingService.StopLoading();
            IsLoginAvailable = true;
        }
    }
}
