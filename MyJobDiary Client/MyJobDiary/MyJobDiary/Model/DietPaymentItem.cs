using System;
using System.Collections.Generic;
using System.Text;

namespace MyJobDiary.Model
{
    public class DietPaymentItem
    {
        public string Location { get; set; }

        public TimeSpan Time { get; set; }

        public double Payment { get; set; }

        public string TimeString { get => Time.ToString(@"hh\:mm"); }

    }
}
