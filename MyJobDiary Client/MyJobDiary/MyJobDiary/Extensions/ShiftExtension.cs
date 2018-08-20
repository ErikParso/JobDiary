using MyJobDiary.Model;

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
                TimeTo = original.TimeTo,
                ArrivalLocation = original.ArrivalLocation,
                ArrivalTime = original.ArrivalTime,
                DepartureLocation = original.DepartureLocation,
                DepartureTime = original.DepartureTime,
                WithDiets = original.WithDiets
            };
        }
    }
}
