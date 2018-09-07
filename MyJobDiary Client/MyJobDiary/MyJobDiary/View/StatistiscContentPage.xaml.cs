
using Microcharts;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StatistiscContentPage : ContentPage
	{
		public StatistiscContentPage ()
		{
			InitializeComponent();
        }



        protected async override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);

            var shifts = await CachedTableManager<Shift>.Current.Value.GetAsync();
            var chart = new BarChart()
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
            chart.BackgroundColor = SKColor.Parse("#000000");
            this.chartView.Chart = chart;
        }
    }
}