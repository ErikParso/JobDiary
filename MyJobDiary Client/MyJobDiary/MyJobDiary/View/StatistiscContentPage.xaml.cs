
using Microcharts;
using SkiaSharp;
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
            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SKColor.Parse("#0097E7"),
                },
                new Microcharts.Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SKColor.Parse("#0097F7")
                },
                new Microcharts.Entry(-100)
                {
                    Label = "March",
                    ValueLabel = "-100",
                    Color = SKColor.Parse("#0097E7")
                },
                new Microcharts.Entry(100)
                {
                    Label = "April",
                    ValueLabel = "100",
                    Color = SKColor.Parse("#5597F7")
                }
            };
            var chart = new BarChart() { Entries = entries };
            chart.BackgroundColor = SKColor.Parse("#000000");
            this.chartView.Chart = chart;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}