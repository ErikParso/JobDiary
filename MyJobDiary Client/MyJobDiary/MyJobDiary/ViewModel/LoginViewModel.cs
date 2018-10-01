using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private readonly ILoadingService _loadingService;
        private readonly IDialogService _dialogService;
        private readonly ILoginService _loginService;
        private readonly MobileServiceClient _mobileServiceClient;

        public LoginViewModel(
            ILoadingService loadingService,
            IDialogService dialogservice,
            ILoginService loginService,
            MobileServiceClient mobileServiceClient)
        {
            _loadingService = loadingService;
            _dialogService = dialogservice;
            _loginService = loginService;
            _mobileServiceClient = mobileServiceClient;
            LoginCommand = new Command(Login);
        }

        private bool _workInProgress;
        public bool WorkInProgress
        {
            get => _workInProgress;
            set => SetField(ref _workInProgress, value);
        }

        #region Login

        public ICommand LoginCommand { get; private set; }

        public Action<(string, string)> LoggedIn { get; set; }

        public async void Login()
        {
            WorkInProgress = true;
            _loadingService.StartLoading("prebieha prihlasovanie");
            try
            {
                if (await _loginService.Login(_mobileServiceClient))
                {
                    LoggedIn?.Invoke(await GetUserInformation());
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowDialog("Nepodarilo sa prihlásiť", e.Message);
            }
            _loadingService.StopLoading();
            WorkInProgress = false;
        }

        public async Task<(string, string)> GetUserInformation()
        {
            var res = await _mobileServiceClient.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
            return (res[0].UserClaims[4].Value, res[0].UserClaims[8].Value);
        }

        #endregion
    }
}
