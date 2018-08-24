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
        private List<Shift> _allItems;

        public ShiftListViewModel(ShiftItemManager manager)
        {
            _manager = manager;
            _allItems = new List<Shift>();
            MonthNavigationViewModel = new MonthNavigationViewModel();
            MonthNavigationViewModel.MonthChanged += RefreshCollection;
            LoadItems();
        }

        public IEnumerable<Shift> ShiftItems
        {
            get => Filter(_allItems);
            set => SetField(ref _allItems, value.ToList());
        }

        public MonthNavigationViewModel MonthNavigationViewModel { get; private set; }


        #region item manipulation

        public void ItemEdited(Shift original, Shift editCopy)
        {
            _allItems.Remove(original);
            _allItems.Add(editCopy);
            RefreshCollection();
        }

        internal void CopyCreated(Shift copy)
        {
            _allItems.Add(copy);
            RefreshCollection();
        }

        public async void ItemDeleted(Shift item)
        {
            App.LoadingService.StartLoading("Mažem záznam");
            try
            {
                await _manager.DeleteAsync(item);
                _allItems.Remove(item);
                RefreshCollection();
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Položku sa nepodarilo odstrániť", e.Message);
            }
            App.LoadingService.StopLoading();
        }

        #endregion


        #region private helpers

        private async void LoadItems()
        {
            App.LoadingService.StartLoading("Aktualizujem zoznam");
            try
            {
                ShiftItems = await _manager.GetTodoItemsAsync();
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Načítanie zlyhalo", e.Message);
            }
            App.LoadingService.StopLoading();
        }

        private void RefreshCollection()
            => RaisePropertyChanged(nameof(ShiftItems));

        private IEnumerable<Shift> Filter(IEnumerable<Shift> allItems)
            => allItems.Where(i => i.TimeFrom.Year == MonthNavigationViewModel.YearPicker.Value &&
                                   i.TimeFrom.Month == MonthNavigationViewModel.MonthPicker.Value)
                       .OrderBy(i => i.TimeFrom);

        #endregion

    }

}
