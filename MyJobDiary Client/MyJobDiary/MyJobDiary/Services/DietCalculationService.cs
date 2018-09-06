using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyJobDiary.Services
{
    public class DietCalculationService
    {
        public IEnumerable<DietPaymentItem> _dietPaymentItems;

        public DietCalculationService(IEnumerable<DietPaymentItem> dietPaymentItems)
        {
            _dietPaymentItems = dietPaymentItems;
        }

        public IEnumerable<DietCalculationItem> GetDietCalculation(Shift shift)
        {
            var daysSplit = SplitShift(shift).ToList();
            for (int i = 0; i < daysSplit.Count() - 1; i++)
            {
                var validItem =_dietPaymentItems
                    .Where(d => d.Country == shift.Country &&
                           d.Hours <= (daysSplit[i + 1] - daysSplit[i]).TotalHours)
                    .OrderByDescending(d => d.Hours)
                    .DefaultIfEmpty(new DietPaymentItem())
                    .First();
                yield return new DietCalculationItem()
                {
                    TimeTo = daysSplit[i + 1].TimeOfDay,
                    TimeFrom = daysSplit[i].TimeOfDay,
                    Reward = validItem.Reward,
                    Currency = validItem.Currency
                };
            }
        }

        private IEnumerable<DateTime> SplitShift(Shift shift)
        {
            DateTime last = shift.DepartureTime;
            while (last < shift.ArrivalTime)
            {
                yield return last;
                last = last.Date.AddDays(1);
            }
            yield return shift.ArrivalTime;
        }
    }
}