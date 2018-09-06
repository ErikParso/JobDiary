using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
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
        private DietCalculationService _dietCalculationService;

        public ShiftListViewModel(
            CachedTableManager<Shift> shiftManager,
            DietCalculationService dietCalculationService)
        {
            _shiftManager = shiftManager;
            _dietCalculationService = dietCalculationService;
            _allShiftItems = new List<Shift>();
            MonthNavigationViewModel = new MonthNavigationViewModel();
            MonthNavigationViewModel.MonthChanged += ReloadItems;
            ReloadItems();
        }

        public IEnumerable<Shift> ShiftItems
        {
            get => RecalculateDiets(Filter(_allShiftItems));
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

        private IEnumerable<Shift> RecalculateDiets(IEnumerable<Shift> items)
            => items.Select(i =>
            {
                i.DietCalculationItems = _dietCalculationService.GetDietCalculation(i);
                return i;
            });

        #endregion

    }

}
