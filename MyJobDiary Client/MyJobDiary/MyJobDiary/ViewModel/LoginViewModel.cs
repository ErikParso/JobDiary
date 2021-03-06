﻿using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Utils.Services;

namespace MyJobDiary.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        private readonly ILoadingService _loadingService;
        private readonly IDialogService _dialogService;
        private readonly IAuthenticationService _authenticationService;
        private readonly MobileServiceClient _mobileServiceClient;

        public LoginViewModel(
            ILoadingService loadingService,
            IDialogService dialogservice,
            IAuthenticationService authenticationService,
            MobileServiceClient mobileServiceClient)
        {
            _loadingService = loadingService;
            _dialogService = dialogservice;
            _authenticationService = authenticationService;
            _mobileServiceClient = mobileServiceClient;
            LoginCommand = new Command<MobileServiceAuthenticationProvider>(Login);
        }

        private bool _workInProgress;
        public bool WorkInProgress
        {
            get => _workInProgress;
            set => SetField(ref _workInProgress, value);
        }

        #region Login

        public ICommand LoginCommand { get; private set; }

        public Action LoginSuccessfull { get; set; }

        public Action AuthenticateSuccessfull { get; set; }

        public async void Authenticate()
        {
            WorkInProgress = true;
            if (await _authenticationService.Authenticate())
            {
                AuthenticateSuccessfull?.Invoke();
            }
            WorkInProgress = false;
        }

        public async void Login(MobileServiceAuthenticationProvider provider)
        {
            WorkInProgress = true;
            _loadingService.StartLoading("Prihlasujem");
            try
            {
                if (await _authenticationService.Login(provider))
                {
                    LoginSuccessfull?.Invoke();
                }
            }
            catch (Exception e)
            {
                _dialogService.ShowDialog("Prihlásenie zlyhalo", e.Message);
            }
            _loadingService.StopLoading();
            WorkInProgress = false;
        }

        #endregion
    }
}
