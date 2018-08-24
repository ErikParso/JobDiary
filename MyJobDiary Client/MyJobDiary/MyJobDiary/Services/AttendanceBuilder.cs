using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyJobDiary.Services
{
    public class AttendanceBuilder
    {
        public static IEnumerable<AttendanceItem> BuildAttendance(int year, int month, IEnumerable<Shift> shifts)
        {
            List<AttendanceItem> ret = new List<AttendanceItem>();
            int days = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= days; i++)
            {
                ret.Add(CreateForDay(new DateTime(year, month, i), shifts));
            }
            return ret;
        }

        private static AttendanceItem CreateForDay(DateTime day, IEnumerable<Shift> shifts)
        {
            TimeSpan sum = TimeSpan.FromHours(0);
            var items = shifts.Where(s => s.IsClosed).OrderBy(s => s.TimeFrom).SelectMany(s => GetTimes(s, day, ref sum)).ToList();

            if (sum < TimeSpan.FromHours(24))
            {
                items.Add((
                    TimeSpan.FromHours(24) - sum,
                    items.Count == 0 ? AttendanceSection.None : items.Last().Item3,
                    AttendanceSection.None
                ));
            }

            return new AttendanceItem()
            {
                Day = day,
                Items = items.Select(i => (i.Item1, i.Item2))
            };
        }

        private static IEnumerable<(TimeSpan, AttendanceSection, AttendanceSection)> GetTimes(Shift shift, DateTime date, ref TimeSpan sum)
        {
            var ret = new List<(TimeSpan, AttendanceSection, AttendanceSection)>();

            if (DateTime.Compare(shift.DepartureTime.Date, date) == 0 && shift.WithDiets)
            {
                ret.Add((shift.DepartureTime.TimeOfDay - sum, AttendanceSection.None, AttendanceSection.Transfer));
                sum = shift.DepartureTime.TimeOfDay;
            }
            if (DateTime.Compare(shift.TimeFrom.Date, date) == 0)
            {
                AttendanceSection section = shift.WithDiets ? AttendanceSection.Transfer : AttendanceSection.None;
                ret.Add((shift.TimeFrom.TimeOfDay - sum, section, AttendanceSection.Work));
                sum = shift.TimeFrom.TimeOfDay;
            }
            if (DateTime.Compare(shift.TimeTo.Date, date) == 0)
            {
                AttendanceSection nextSection = shift.WithDiets ? AttendanceSection.Transfer : AttendanceSection.None;
                ret.Add((shift.TimeTo.TimeOfDay - sum, AttendanceSection.Work, nextSection));
                sum = shift.TimeTo.TimeOfDay;
            }
            if (DateTime.Compare(shift.ArrivalTime.Date, date) == 0 && shift.WithDiets)
            {
                ret.Add((shift.ArrivalTime.TimeOfDay - sum, AttendanceSection.Transfer, AttendanceSection.None));
                sum = shift.ArrivalTime.TimeOfDay;
            }

            return ret;
        }
    }
}
