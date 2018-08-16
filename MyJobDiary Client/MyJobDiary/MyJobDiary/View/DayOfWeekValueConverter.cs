using System;
using Xamarin.Forms;

namespace MyJobDiary.View
{
    public class DayOfWeekValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (((DateTime)value).DayOfWeek)
            {
                case DayOfWeek.Monday: return "Po";
                case DayOfWeek.Tuesday: return "Ut";
                case DayOfWeek.Wednesday: return "St";
                case DayOfWeek.Thursday: return "Št";
                case DayOfWeek.Friday: return "Pi";
                case DayOfWeek.Saturday: return "So";
                case DayOfWeek.Sunday: return "Ne";
                default: return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
