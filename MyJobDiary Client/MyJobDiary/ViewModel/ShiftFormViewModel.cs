using MyJobDiary.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftFormViewModel : ObservableObject
    {
        private Shift _shift;
        private TodoItemManager _manager;

        #region Bindable

        public DateTime DateFrom
        {
            get => _shift.TimeFrom;
            set
            {
                _shift.TimeFrom = value.Date.Add(_shift.TimeFrom.TimeOfDay);
                RaisePropertyChanged("DateFrom");
            }
        }

        public DateTime DateTo
        {
            get => _shift.TimeFrom;
            set
            {
                _shift.TimeTo = value.Date.Add(_shift.TimeTo.TimeOfDay);
                RaisePropertyChanged("DateTo");
            }
        }

        public TimeSpan TimeFrom
        {
            get => _shift.TimeFrom.TimeOfDay;
            set
            {
                _shift.TimeFrom = _shift.TimeFrom.Date.Add(value);
                RaisePropertyChanged("TimeFrom");
            }
        }

        public TimeSpan TimeTo
        {
            get => _shift.TimeTo.TimeOfDay;
            set
            {
                _shift.TimeTo = _shift.TimeTo.Date.Add(value);
                RaisePropertyChanged("TimeTo");
            }
        }

        public string Location
        {
            get => _shift.Location;
            set
            {
                _shift.Location = value;
                RaisePropertyChanged("Location");
            }
        }

        public string Job
        {
            get => _shift.Job;
            set
            {
                _shift.Job = value;
                RaisePropertyChanged("Job");
            }
        }

        public bool IsNightShift
        {
            get => _shift.IsNightShift;
            set
            {
                _shift.IsNightShift = value;
                RaisePropertyChanged("IsNightShift");
            }
        }

        public bool IsClosed
        {
            get => _shift.IsClosed;
            set
            {
                _shift.IsClosed = value;
                RaisePropertyChanged("IsClosed");
            }
        }


        public ICommand SaveCommand { get; private set; }

        private bool _isProcessing;
        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetField(ref _isProcessing, value);
        }

        #endregion

        public ShiftFormViewModel(TodoItemManager manager, Shift shift)
        {
            SaveCommand = new Command(Save);
            _manager = manager;
            _shift = shift;
        }

        private async void Save(object obj)
        {
            App.LoadingService.StartLoading("odosielam");
            try
            {
                await _manager.SaveTaskAsync(_shift);
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa odoslať", e.Message);
            }
            App.LoadingService.StopLoading();
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
