using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftListViewModel: ObservableObject
    {
        public readonly ShiftItemManager Manager;

        private ObservableCollection<Shift> _shiftItems;
        public ObservableCollection<Shift> ShiftItems
        {
            get => _shiftItems;
            set => SetField(ref _shiftItems, value);
        }

        public ShiftListViewModel(ShiftItemManager manager)
        {
            Manager = manager;
            _shiftItems = new ObservableCollection<Shift>();
        }

        public async void Refresh()
        {
            App.LoadingService.StartLoading("Aktualizujem zoznam");
            ShiftItems = await Manager.GetTodoItemsAsync();
            App.LoadingService.StopLoading();
        }
    }
}
