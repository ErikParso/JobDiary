﻿using MyJobDiary.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendanceList : ContentPage
	{
		public AttendanceList (AttendanceListViewModel viewModel)
		{
			InitializeComponent();
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}