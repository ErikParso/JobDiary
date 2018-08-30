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
        private readonly ShiftItemManager _manager;
        private IEnumerable<Shift> _allItems;

        public ShiftListViewModel(ShiftItemManager manager)
        {
            _manager = manager;
            _allItems = new List<Shift>();
            MonthNavigationViewModel = new MonthNavigationViewModel();
            MonthNavigationViewModel.MonthChanged += ReloadItems;
            ReloadItems();
        }

        public IEnumerable<Shift> ShiftItems
        {
            get => Filter(_allItems);
            set => SetField(ref _allItems, value);
        }

        public MonthNavigationViewModel MonthNavigationViewModel { get; private set; }

        public async void ReloadItems()
        {
            ShiftItems = await _manager.GetTodoItemsAsync();
        }


        #region private helpers

        private IEnumerable<Shift> Filter(IEnumerable<Shift> allItems)
            => allItems.Where(i => i.TimeFrom.Year == MonthNavigationViewModel.YearPicker.Value &&
                                   i.TimeFrom.Month == MonthNavigationViewModel.MonthPicker.Value)
                       .OrderBy(i => i.TimeFrom);

        #endregion

    }

}
