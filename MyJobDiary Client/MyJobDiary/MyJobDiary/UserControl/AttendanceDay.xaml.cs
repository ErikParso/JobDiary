using MyJobDiary.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.UserControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceDay : ContentView
    {
        public AttendanceDay()
        {
            InitializeComponent();
            this.BindingContextChanged += DrawAttendance;
        }

        private void DrawAttendance(object sender, EventArgs e)
        {
            AttendanceDayModel day = BindingContext as AttendanceDayModel;

            double fullDay = TimeSpan.FromHours(24).TotalSeconds;
            TimeSpan counter = TimeSpan.FromHours(0);

            foreach (var kp in day.Items)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(kp.Time.TotalSeconds / fullDay, GridUnitType.Star);
                dayGrid.ColumnDefinitions.Add(column);
                BoxView box = new BoxView();
                box.BackgroundColor = kp.Color;
                box.HorizontalOptions = LayoutOptions.Fill;
                box.VerticalOptions = LayoutOptions.Fill;
                dayGrid.Children.Add(box, dayGrid.ColumnDefinitions.Count - 1, 0);
                counter += kp.Time;
            }
        }
    }
}