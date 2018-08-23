using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MyJobDiary.Services
{
    public class AttendanceBuilder
    {
        private IEnumerable<Shift> _shifts;

        private Color workColor = Color.FromHex("0097F7");
        private Color travelColor = Color.FromHex("#004c7c");

        public AttendanceBuilder(IEnumerable<Shift> shifts)
        {
            _shifts = shifts;
        }

        public IEnumerable<AttendanceDayModel> BuildAttendance(int year, int month)
        {
            List<AttendanceDayModel> ret = new List<AttendanceDayModel>();
            int days = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= days; i++)
            {
                ret.Add(CreateForDay(new DateTime(year, month, i)));
            }
            return ret;
        }

        private AttendanceDayModel CreateForDay(DateTime day)
        {
            TimeSpan sum = TimeSpan.FromHours(0);
            var items = _shifts.Where(s => s.IsClosed).OrderBy(s => s.TimeFrom).SelectMany(s => GetTimes(s, day, ref sum)).ToList();

            if (sum < TimeSpan.FromHours(24))
            {
                items.Add((TimeSpan.FromHours(24) - sum,
                    items.Count == 0 ? Color.Transparent : items.Last().Item3,
                    Color.Red));
            }

            return new AttendanceDayModel()
            {
                Day = day,
                Items = items.Select(i => (i.Item1, i.Item2))
            };
        }

        private IEnumerable<(TimeSpan, Color, Color)> GetTimes(Shift shift, DateTime date, ref TimeSpan sum)
        {
            var ret = new List<(TimeSpan, Color, Color)>();

            if (DateTime.Compare(shift.DepartureTime.Date, date) == 0 && shift.WithDiets)
            {
                ret.Add((shift.DepartureTime.TimeOfDay - sum, Color.Transparent, travelColor));
                sum = shift.DepartureTime.TimeOfDay;
            }
            if (DateTime.Compare(shift.TimeFrom.Date, date) == 0)
            {
                Color color = shift.WithDiets ? travelColor : Color.Transparent;
                ret.Add((shift.TimeFrom.TimeOfDay - sum, color, workColor));
                sum = shift.TimeFrom.TimeOfDay;
            }
            if (DateTime.Compare(shift.TimeTo.Date, date) == 0)
            {
                Color color = shift.WithDiets ? travelColor : Color.Transparent;
                ret.Add((shift.TimeTo.TimeOfDay - sum, workColor, color));
                sum = shift.TimeTo.TimeOfDay;
            }
            if (DateTime.Compare(shift.ArrivalTime.Date, date) == 0 && shift.WithDiets)
            {
                ret.Add((shift.ArrivalTime.TimeOfDay - sum, travelColor, Color.Transparent));
                sum = shift.ArrivalTime.TimeOfDay;
            }

            return ret;
        }
    }
}
