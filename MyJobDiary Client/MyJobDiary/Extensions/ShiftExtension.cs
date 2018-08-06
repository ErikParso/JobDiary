using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobDiary.Extensions
{
    public static class ShiftExtension
    {
        public static Shift CopyCreate(this Shift original)
        {
            return new Shift()
            {
                Id = null,
                IsClosed = original.IsClosed,
                IsNightShift = original.IsNightShift,
                Job = original.Job,
                Location = original.Location,
                TimeFrom = original.TimeFrom,
                TimeTo = original.TimeTo
            };
        }
    }
}
