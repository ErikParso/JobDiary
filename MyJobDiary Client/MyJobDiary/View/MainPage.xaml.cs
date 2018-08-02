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
            await Navigation.PushAsync(new ShiftListContentPage());
        }

        private async void ShiftForm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShiftFormContentPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}