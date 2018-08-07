using MyJobDiary.Managers;
using MyJobDiary.Model;
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

        public ICommand LoginCommand { get; private set; }

        public MainPageViewModel()
        {
            LoginCommand = new Command(Login);
        }

        public async void Login()
        {
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
        }

        public async void SaveAsync(Shift model)
        {
            App.LoadingService.StartLoading("odosielam");
            try
            {
                await ShiftItemManager.Current.Value.SaveTaskAsync(model);
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa odoslať", e.Message);
            }
            App.LoadingService.StopLoading();
        }
    }
}
