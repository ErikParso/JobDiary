using System;
using Xamarin.Forms;

namespace MyJobDiary.View
{
    public class TimeWorkedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Math.Floor(((TimeSpan)value).TotalHours) + "H " + (((TimeSpan)value).Minutes) + "m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
