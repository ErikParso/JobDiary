using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyJobDiary.Model
{
    public class AttendanceDayModel
    {
        public IEnumerable<(TimeSpan Time, Color Color)> Items { get; set; }

        public DateTime Day { get; set; }

    }
}
