using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MasterViewModel : ObservableObject
    {
        #region User

        private readonly ILoginService _loginService;
        private readonly ILoadingService _loadingService;
        private readonly IDialogService _dialogService;
        private readonly CachedTableManager<Shift> _shiftManager;
        private readonly CachedTableManager<DietPaymentItem> _dietManager;
        private readonly MobileServiceClient _mobileServiceClient;

        private string _userName;
        private string _photo;

        public MasterViewModel(
            ILoginService loginService,
            ILoadingService loadingService,
            IDialogService dialogService,
            MobileServiceClient mobileServiceClient,
            CachedTableManager<Shift> shiftManager,
            CachedTableManager<DietPaymentItem> dietManager)
        {
            _loginService = loginService;
            _loadingService = loadingService;
            _dialogService = dialogService;
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
            var info = await _loginService.GetUserInformation(_mobileServiceClient);
            UserName = info.UserClaims[4].Value;
            Photo = info.UserClaims[8].Value;
        }

        public Action LogoutSuccessfull;

        public ICommand LogoutCommand { get; private set; }

        private async void Logout()
        {
            _loadingService.StartLoading("Odhlasujem");
            try
            {
                await _loginService.Logout(_mobileServiceClient);
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
