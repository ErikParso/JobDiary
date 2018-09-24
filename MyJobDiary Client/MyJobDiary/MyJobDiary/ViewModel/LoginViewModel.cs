using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {
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
            App.LoadingService.StartLoading("prebieha prihlasovanie");
            try
            {
                if (await App.LoginService.Login())
                {
                    LoggedIn?.Invoke(await GetUserInformation());
                }
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa prihlásiť", e.Message);
            }
            App.LoadingService.StopLoading();
            WorkInProgress = false;
        }

        public async Task<(string, string)> GetUserInformation()
        {
            var res = await MyClient.Current.Value.InvokeApiAsync<List<AppServiceIdentity>>("/.auth/me");
            return (res[0].UserClaims[4].Value, res[0].UserClaims[8].Value);
        }

        #endregion
    }
}
