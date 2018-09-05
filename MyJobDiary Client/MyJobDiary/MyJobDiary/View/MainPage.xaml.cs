using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using MyJobDiary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var manager = CachedTableManager<Shift>.Current.Value;
            var dietItems = await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync();
            ShiftListViewModel viewModel = new ShiftListViewModel(manager, dietItems);
            ShiftListContentPage shiftList = new ShiftListContentPage(viewModel, false);
            await Navigation.PushAsync(shiftList);
        }

        private async void Diets_Clicked(object sender, EventArgs e)
        {
            var manager = CachedTableManager<Shift>.Current.Value;
            var dietItems = await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync();
            ShiftListViewModel viewModel = new ShiftListViewModel(manager, dietItems);
            ShiftListContentPage shiftList = new ShiftListContentPage(viewModel, true);
            await Navigation.PushAsync(shiftList);
        }

        private async void ShiftForm_Clicked(object sender, EventArgs e)
        {
            var shiftsManager = CachedTableManager<Shift>.Current.Value;
            var countries = (await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync())
                .Select(c => c.Country).Distinct().ToList();
            var shiftFormViewModel = new ShiftFormViewModel(shiftsManager, countries, new Shift
            {
                DepartureTime = DateTime.Now,
                TimeFrom = DateTime.Now,
                TimeTo = DateTime.Now.AddHours(8),
                ArrivalTime = DateTime.Now.AddHours(8),
                IsNightShift = DateTime.Now.Hour < 24 && DateTime.Now.Hour > 18,
                Country = countries.FirstOrDefault() ?? "",
                IsClosed = true,
                WithDiets = true,
            });
            await Navigation.PushAsync(new ShiftFormContentPage(shiftFormViewModel));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void Attendance_Click(object sender, EventArgs e)
        {
            var manager = CachedTableManager<Shift>.Current.Value;
            var items = await manager.GetAsync();
            AttendanceListViewModel viewModel = new AttendanceListViewModel(items);
            await Navigation.PushAsync(new AttendanceList(viewModel));
        }

        private async void DietsSettings_Clicked(object sender, EventArgs e)
        {
            var manager = CachedTableManager<DietPaymentItem>.Current.Value;
            var viewModel = new DietsPaymentViewModel(manager);
            await Navigation.PushAsync(new DietPaymentItemList(viewModel));
        }
    }
}