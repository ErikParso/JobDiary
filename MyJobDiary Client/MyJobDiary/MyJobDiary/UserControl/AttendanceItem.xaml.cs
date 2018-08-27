using MyJobDiary.Model;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.UserControl
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AttendanceItem : ContentView
    {
        public static readonly BindableProperty WorkColorProperty = BindableProperty.Create(
            "WorkColor", typeof(Color), typeof(AttendanceItem), Color.Green);
        public static readonly BindableProperty NoneColorProperty = BindableProperty.Create(
            "NoneColor", typeof(Color), typeof(AttendanceItem), Color.Transparent);
        public static readonly BindableProperty TransferColorProperty = BindableProperty.Create(
            "TransferColor", typeof(Color), typeof(AttendanceItem), Color.DarkGreen);
        public static readonly BindableProperty ErrorColorProperty = BindableProperty.Create(
            "ErrorColor", typeof(Color), typeof(AttendanceItem), Color.Red);

        public Color WorkColor
        {
            get { return (Color)GetValue(WorkColorProperty); }
            set { SetValue(WorkColorProperty, value); }
        }

        public Color NoneColor
        {
            get { return (Color)GetValue(NoneColorProperty); }
            set { SetValue(NoneColorProperty, value); }
        }

        public Color TransferColor
        {
            get { return (Color)GetValue(TransferColorProperty); }
            set { SetValue(TransferColorProperty, value); }
        }

        public Color ErrorColor
        {
            get { return (Color)GetValue(ErrorColorProperty); }
            set { SetValue(ErrorColorProperty, value); }
        }

        public AttendanceItem()
        {
            InitializeComponent();
            this.BindingContextChanged += DrawAttendance;
        }

        private void DrawAttendance(object sender, EventArgs e)
        {
            Model.AttendanceItem day = BindingContext as Model.AttendanceItem;
            if (day == null)
                return;

            double fullDay = TimeSpan.FromHours(24).TotalSeconds;
            TimeSpan counter = TimeSpan.FromHours(0);

            try
            {
                foreach (var kp in day.Items)
                {
                    ColumnDefinition column = new ColumnDefinition();
                    column.Width = new GridLength(kp.Time.TotalSeconds / fullDay, GridUnitType.Star);
                    dayGrid.ColumnDefinitions.Add(column);
                    BoxView box = new BoxView();
                    box.BackgroundColor = GetColorBySection(kp.Section);
                    box.HorizontalOptions = LayoutOptions.Fill;
                    box.VerticalOptions = LayoutOptions.Fill;
                    dayGrid.Children.Add(box, dayGrid.ColumnDefinitions.Count - 1, 0);
                    counter += kp.Time;
                }
            }
            catch
            {
                dayGrid.ColumnDefinitions.Clear();
                dayGrid.Children.Clear();
                dayGrid.ColumnDefinitions.Add(new ColumnDefinition());
                Label error = new Label();
                error.Text = "chyba dochádzky";
                error.HorizontalOptions = LayoutOptions.Center;
                error.VerticalOptions = LayoutOptions.Center;
                error.TextColor = ErrorColor;
                dayGrid.Children.Add(error, 0, 0);
            }
        }

        private Color GetColorBySection(AttendanceSection section)
        {
            switch (section)
            {
                case AttendanceSection.None: return NoneColor;
                case AttendanceSection.Transfer: return TransferColor;
                case AttendanceSection.Work: return WorkColor;
                default: return Color.Red;
            }
        }
    }
}