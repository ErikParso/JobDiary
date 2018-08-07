using MyJobDiary.Extensions;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftListViewModel: ObservableObject
    {
        private readonly ShiftItemManager _manager;

        private List<Shift> _shiftItems;
        public IEnumerable<Shift> ShiftItems
        {
            get => _shiftItems.Where(i => i.TimeFrom.Year == YearPicker.Value &&
                                          i.TimeFrom.Month == MonthPicker.Value);
            set => SetField(ref _shiftItems, value.ToList());
        }

        public ShiftListViewModel(ShiftItemManager manager)
        {
            _manager = manager;
            _shiftItems = new List<Shift>();
            InitFilter();
            LoadItems();
        }

        internal Action Save(Shift item)
        {
            throw new NotImplementedException();
        }


        #region Filter

        public SidePickerViewModel YearPicker { get; private set; }

        public SidePickerViewModel MonthPicker { get; private set; }

        private void InitFilter()
        {
            YearPicker = new SidePickerViewModel()
            {
                Value = DateTime.Now.Year,
                LeftCommand = new Command(YearLeft),
                RightCommand = new Command(YearRight)
            };
            MonthPicker = new SidePickerViewModel()
            {
                Value = DateTime.Now.Month,
                LeftCommand = new Command(MonthLeft),
                RightCommand = new Command(MonthRight)
            };
        }

        private void YearLeft()
        {
            YearPicker.Value--;
            RaisePropertyChanged("ShiftItems");
        }

        private void MonthLeft()
        {
            if (MonthPicker.Value == 1)
            {
                MonthPicker.Value = 12;
                YearPicker.Value--;
            }
            else
            {
                MonthPicker.Value--;
                RaisePropertyChanged("ShiftItems");
            }

        }

        private void YearRight()
        {
            YearPicker.Value++;
            RaisePropertyChanged("ShiftItems");
        }

        private void MonthRight()
        {
            if (MonthPicker.Value == 12)
            {
                MonthPicker.Value = 1;
                YearPicker.Value++;
            }
            else
            {
                MonthPicker.Value++;
                RaisePropertyChanged("ShiftItems");
            }
        }

        #endregion


        public async void LoadItems()
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

        public async void Delete(Shift item)
        {
            App.LoadingService.StartLoading("Mažem záznam");
            try
            {
                await _manager.DeleteAsync(item);
                _shiftItems.Remove(item);
                RaisePropertyChanged("ShiftItems");
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Položku sa nepodarilo odstrániť", e.Message);
            }
            App.LoadingService.StopLoading();
        }
    }

}
