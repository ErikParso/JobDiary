﻿using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShiftFormContentPage : ContentPage
	{
		public ShiftFormContentPage(ShiftFormViewModel viewModel)
		{
			InitializeComponent ();
            BindingContext = viewModel;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}