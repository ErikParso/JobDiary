using MyJobDiary.Managers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        private bool _workInProgress;
        public bool WorkInProgress
        {
            get => _workInProgress;
            set => SetField(ref _workInProgress, value);
        }

        public MainPageViewModel()
        {
            LoginCommand = new Command(Login);
            LogoutCommand = new Command(Logout);
        }


        #region Login

        private bool _isAutenthicated;
        public bool IsAuthenticated
        {
            get => _isAutenthicated;
            set => SetField(ref _isAutenthicated, value);
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetField(ref _userName, value);
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }

        public async void Login()
        {
            WorkInProgress = true;
            App.LoadingService.StartLoading("prebieha prihlasovanie");
            try
            {
                IsAuthenticated = await App.LoginService.Login();
                UserName = await GetUserName();
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa prihlásiť", e.Message);
            }
            App.LoadingService.StopLoading();
            WorkInProgress = false;
        }

        public async void Logout()
        {
            WorkInProgress = true;
            App.LoadingService.StartLoading("prebieha odhlasovanie");
            try
            {
                await App.LoginService.Logout();
                IsAuthenticated = false;
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa odhlásiť", e.Message);
            }
            App.LoadingService.StopLoading();
            WorkInProgress = false;
        }

        private async Task<string> GetUserName()
        {
            var res = await MyClient.Current.Value.InvokeApiAsync("/.auth/me");
            var ret = res[0]["user_claims"][3]["val"];
            return ret.ToString();
        }

        #endregion

    }
}
