using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyJobDiary.Services
{
    public class DietCalculationService : IDietCalculationService
    {
        private readonly CachedTableManager<DietPaymentItem> _dietManager;
        private readonly CachedTableManager<Shift> _shiftManager;

        public DietCalculationService(
            CachedTableManager<DietPaymentItem> dietManager,
            CachedTableManager<Shift> shiftManager)
        {
            _dietManager = dietManager;
            _shiftManager = shiftManager;
        }

        public async Task RecalculateDiets(IEnumerable<Shift> shifts)
        {
            foreach (Shift shift in shifts)
                shift.DietCalculationItems = await GetDietCalculation(shift);
        }

        public async Task<Dictionary<string, double>> GetMonthDiets(int year, int month)
        {
            var ret = new Dictionary<string, double>();
            var shiftItems = (await _shiftManager.GetAsync())
                .Where(s => s.TimeFrom.Year == year && s.TimeTo.Month == month);
            foreach (Shift shift in shiftItems)
            {
                var dietParts = await GetDietCalculation(shift);
                foreach (DietCalculationItem dietPart in dietParts)
                {
                    if (!string.IsNullOrWhiteSpace(dietPart.Currency))
                    {
                        if (!ret.ContainsKey(dietPart.Currency))
                            ret.Add(dietPart.Currency, 0);
                        ret[dietPart.Currency] += dietPart.Reward;
                    }
                }
            }
            return ret;
        }


        public async Task<IEnumerable<DietCalculationItem>> GetDietCalculation(Shift shift)
        {
            if (shift == null)
                return Enumerable.Empty<DietCalculationItem>();
            var ret = new List<DietCalculationItem>();
            var dietPaymentItems = await _dietManager.GetAsync();
            var daysSplit = SplitShift(shift).ToList();
            for (int i = 0; i < daysSplit.Count() - 1; i++)
            {
                var validItem = dietPaymentItems
                    .Where(d => d.Country == shift.Country &&
                           d.Hours <= (daysSplit[i + 1] - daysSplit[i]).TotalHours)
                    .OrderByDescending(d => d.Hours)
                    .DefaultIfEmpty(new DietPaymentItem())
                    .First();
                ret.Add(new DietCalculationItem()
                {
                    TimeTo = daysSplit[i + 1].TimeOfDay,
                    TimeFrom = daysSplit[i].TimeOfDay,
                    Reward = validItem.Reward,
                    Currency = validItem.Currency
                });
            }
            return ret;
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