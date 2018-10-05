using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public class ValidationService: IValidationService
    {
        private readonly CachedTableManager<Shift> _shiftManager;

        public ValidationService(CachedTableManager<Shift> shiftManager)
        {
            _shiftManager = shiftManager;
        }

        public async Task<bool> IsShiftOverlapped(Shift shift)
        {
            var allShifts = await _shiftManager.GetAsync();
            return allShifts.Any(s => s.Id != shift.Id && CheckOverlapping(s, shift));
        }

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
