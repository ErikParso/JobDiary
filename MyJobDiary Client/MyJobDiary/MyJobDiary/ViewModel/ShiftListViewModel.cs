using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftListViewModel : ObservableObject
    {
        private readonly CachedTableManager<Shift> _shiftManager;
        private IEnumerable<Shift> _allShiftItems;
        private IEnumerable<DietPaymentItem> _dietPaymentItems;

        public ShiftListViewModel(
            CachedTableManager<Shift> shiftManager,
            IEnumerable<DietPaymentItem> dietPaymentItems)
        {
            _shiftManager = shiftManager;
            _dietPaymentItems = dietPaymentItems;
            _allShiftItems = new List<Shift>();
            MonthNavigationViewModel = new MonthNavigationViewModel();
            MonthNavigationViewModel.MonthChanged += ReloadItems;
            ReloadItems();
        }

        public IEnumerable<Shift> ShiftItems
        {
            get => RecalculateSum(Filter(_allShiftItems));
            set => SetField(ref _allShiftItems, value);
        }

        public MonthNavigationViewModel MonthNavigationViewModel { get; private set; }

        public async void ReloadItems()
        {
            ShiftItems = await _shiftManager.GetAsync();
        }

        #region private helpers

        private IEnumerable<Shift> Filter(IEnumerable<Shift> allItems)
            => allItems.Where(i => i.TimeFrom.Year == MonthNavigationViewModel.YearPicker.Value &&
                                   i.TimeFrom.Month == MonthNavigationViewModel.MonthPicker.Value)
                       .OrderBy(i => i.TimeFrom);

        private IEnumerable<Shift> RecalculateSum(IEnumerable<Shift> items)
            => items.Select(i =>
            {
                i.DietSum = _dietPaymentItems
                    .Where(d => d.Country == i.Country && d.Hours <= i.DietTime.TotalHours)
                    .OrderByDescending(d => d.Hours)
                    .FirstOrDefault()?.Reward ?? 0;
                return i;
            });

        #endregion

    }

}
