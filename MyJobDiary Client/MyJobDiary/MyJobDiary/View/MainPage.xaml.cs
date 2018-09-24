using Microcharts;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using MyJobDiary.ViewModel;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel = new MainPageViewModel();

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
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
            shiftFormViewModel.OnSaved += async () => await Navigation.PopAsync();
            await Navigation.PushAsync(new ShiftFormContentPage(shiftFormViewModel));
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var shifts = await CachedTableManager<Shift>.Current.Value.GetAsync();
            var chart = new LineChart()
            {
                Entries = Enumerable.Range(-12, 13)
                    .Select(i => DateTime.Now.AddMonths(i).Date)
                    .Select(d => (
                        Date: d,
                        TimeWorked: shifts.Where(s => s.TimeFrom.Year == d.Year && s.TimeFrom.Month == d.Month)
                            .Sum(s => (s.TimeTo - s.TimeFrom).TotalHours)
                    ))
                    .Select(d => new Microcharts.Entry((float)d.TimeWorked)
                    {
                        Color = SKColor.Parse("#0097E7"),
                        Label = d.Date.ToString("MMM"),
                        ValueLabel = string.Format("{0:N2} H", d.TimeWorked),
                    })
            };
            chart.BackgroundColor = SKColors.Transparent;
            chartView.BackgroundColor = Color.Transparent;
            chart.PointMode = PointMode.Circle;
            this.chartView.Chart = chart;
        }

    }
}