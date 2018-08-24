using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyJobDiary.Model
{
    public class AttendanceItem
    {
        public IEnumerable<(TimeSpan Time, AttendanceSection Section)> Items { get; set; }

        public DateTime Day { get; set; }

    }
}
