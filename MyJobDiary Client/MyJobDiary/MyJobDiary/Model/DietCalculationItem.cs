using System;
using System.Collections.Generic;
using System.Text;

namespace MyJobDiary.Model
{
    public class DietCalculationItem
    {
        public TimeSpan TimeFrom { get; set; }

        public TimeSpan TimeTo { get; set; }

        public string Currency { get; set; }

        public double Reward { get; set; }
    }
}
