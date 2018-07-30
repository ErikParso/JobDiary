﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class MainPageViewModel: ObservableObject
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
            IsAuthenticated = await App.LoginService.Authenticate();
            App.LoadingService.StopLoading();
        }
    }
}
