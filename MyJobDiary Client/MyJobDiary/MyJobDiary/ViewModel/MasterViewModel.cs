﻿using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Utils.Services;

namespace MyJobDiary.ViewModel
{
    public class MasterViewModel : ObservableObject
    {
        #region User

        private readonly IAuthenticationService _authenticationService;
        private readonly ILoadingService _loadingService;
        private readonly IDialogService _dialogService;
        private readonly IAccountInformationService _accoutInformationService;
        private readonly CachedTableManager<Shift> _shiftManager;
        private readonly CachedTableManager<DietPaymentItem> _dietManager;
        private readonly MobileServiceClient _mobileServiceClient;

        private string _userName;
        private string _photo;

        public MasterViewModel(
            IAuthenticationService authenticationService,
            ILoadingService loadingService,
            IDialogService dialogService,
            IAccountInformationService accountInformationService,
            MobileServiceClient mobileServiceClient,
            CachedTableManager<Shift> shiftManager,
            CachedTableManager<DietPaymentItem> dietManager)
        {
            _authenticationService = authenticationService;
            _loadingService = loadingService;
            _dialogService = dialogService;
            _accoutInformationService = accountInformationService;
            _shiftManager = shiftManager;
            _dietManager = dietManager;
            _mobileServiceClient = mobileServiceClient;
            LogoutCommand = new Command(Logout);
        }

        public string UserName
        {
            get => _userName;
            private set => SetField(ref _userName, value);
        }

        public string Photo
        {
            get => _photo;
            private set => SetField(ref _photo, value);
        }

        public async void ReloadUserInformation()
        {
            await _accoutInformationService.LoadInformation();
            Photo = _accoutInformationService.PhotoUrl;
            UserName = _accoutInformationService.Email;
        }

        public Action LogoutSuccessfull;

        public ICommand LogoutCommand { get; private set; }

        private async void Logout()
        {
            _loadingService.StartLoading("Odhlasujem");
            try
            {
                await _authenticationService.Logout();
                _shiftManager.ClearCache();
                _dietManager.ClearCache();
                LogoutSuccessfull?.Invoke();
            }
            catch (Exception e)
            {
                _dialogService.ShowDialog("Odhlásenie zlyhalo", e.Message);
            }
            _loadingService.StopLoading();
        }

        #endregion
    }
}
