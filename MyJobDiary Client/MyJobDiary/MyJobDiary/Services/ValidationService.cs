using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyJobDiary.Services
{
    public class ValidationService
    {
        public IEnumerable<Shift> CheckOverlapping(IEnumerable<Shift> shifts, Shift newShift)
            => shifts.Where(s => s.Id != newShift.Id &&
               (CheckOverlapping(s, newShift) || CheckOverlapping(newShift, s)));

        private bool CheckOverlapping(Shift shift1, Shift shift2)
        {
            if (!shift1.IsClosed || !shift2.IsClosed)
            {
                return false;
            }
            else
            {
                var interval1 = GetInterval(shift1);
                var interval2 = GetInterval(shift2);
                return (interval1.from < interval2.to && interval2.from < interval1.to);
            }
        }

        private (DateTime from, DateTime to) GetInterval(Shift shift)
            => (from: shift.WithDiets ? shift.DepartureTime : shift.TimeFrom,
                to: shift.WithDiets ? shift.ArrivalTime : shift.TimeTo);
    }
}
