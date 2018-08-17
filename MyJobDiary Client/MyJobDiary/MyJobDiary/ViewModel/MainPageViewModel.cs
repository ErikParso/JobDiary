using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        public MainPageViewModel()
        {
            LoginCommand = new Command(Login);
            LogoutCommand = new Command(Logout);
        }

        private bool _workInProgress;
        public bool WorkInProgress
        {
            get => _workInProgress;
            set => SetField(ref _workInProgress, value);
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

        private ImageSource _photo;
        public ImageSource Photo
        {
            get => _photo;
            set => SetField(ref _photo, value);
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
                await SetUserInfo();
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

        private async Task SetUserInfo()
        {
            var res = await MyClient.Current.Value.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
            UserName = res[0].UserClaims[3].Value;
            Photo = ImageSource.FromUri(new Uri(res[0].UserClaims[10].Value));
        }

        #endregion

    }
}
