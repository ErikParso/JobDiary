using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        private async void ShiftList_Clicked(object sender, EventArgs e)
        {
            ShiftListViewModel viewModel = new ShiftListViewModel(ShiftItemManager.Current.Value);
            ShiftListContentPage shiftList = new ShiftListContentPage(viewModel, false);
            await Navigation.PushAsync(shiftList);
        }

        private async void Diets_Clicked(object sender, EventArgs e)
        {
            ShiftListViewModel viewModel = new ShiftListViewModel(ShiftItemManager.Current.Value);
            ShiftListContentPage shiftList = new ShiftListContentPage(viewModel, true);
            await Navigation.PushAsync(shiftList);
        }

        private async void ShiftForm_Clicked(object sender, EventArgs e)
        {
            var shiftFormViewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, new Shift
            {
                DepartureTime = DateTime.Now,
                TimeFrom = DateTime.Now,
                TimeTo = DateTime.Now.AddHours(8),
                ArrivalTime = DateTime.Now.AddHours(8),
                IsNightShift = DateTime.Now.Hour < 24 && DateTime.Now.Hour > 18
            });
            await Navigation.PushAsync(new ShiftFormContentPage(shiftFormViewModel));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}