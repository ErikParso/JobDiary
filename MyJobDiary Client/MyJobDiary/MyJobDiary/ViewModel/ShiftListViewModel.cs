using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System.Collections.Generic;
using System.Linq;

namespace MyJobDiary.ViewModel
{
    public class ShiftListViewModel : ObservableObject
    {
        #region Private fields

        private readonly CachedTableManager<Shift> _shiftManager;
        private readonly CachedTableManager<DietPaymentItem> _dietManager;
        private readonly IDietCalculationService _dietCalculationService;

        private IEnumerable<Shift> _allShiftItems;

        #endregion


        #region Constructors

        public ShiftListViewModel(
            CachedTableManager<Shift> shiftManager,
            CachedTableManager<DietPaymentItem> dietManager,
            IDietCalculationService dietCalculationService)
        {
            _shiftManager = shiftManager;
            _dietManager = dietManager;
            _dietCalculationService = dietCalculationService;
            _allShiftItems = new List<Shift>();
            MonthNavigationViewModel = new MonthNavigationViewModel();
            MonthNavigationViewModel.MonthChanged += Reload;
        }

        #endregion


        #region Properties

        public IEnumerable<Shift> ShiftItems
        {
            get => _allShiftItems;
            set => SetField(ref _allShiftItems, value);
        }

        public MonthNavigationViewModel MonthNavigationViewModel { get; private set; }

        #endregion


        #region Public methods

        public async void Reload()
        {
            var newShifts = Filter(await _shiftManager.GetAsync())
                .OrderBy(s => s.TimeFrom);
            await _dietCalculationService.RecalculateDiets(newShifts);
            ShiftItems = newShifts;
        }

        public async void Delete(Shift shift)
        {
            await _shiftManager.DeleteAsync(shift);
            Reload();
        }

        #endregion


        #region private helpers

        private IEnumerable<Shift> Filter(IEnumerable<Shift> allItems)
            => allItems.Where(i => i.TimeFrom.Year == MonthNavigationViewModel.YearPicker.Value &&
                                   i.TimeFrom.Month == MonthNavigationViewModel.MonthPicker.Value);

        #endregion

    }

}
