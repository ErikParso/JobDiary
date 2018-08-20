using MyJobDiary.Managers;
using MyJobDiary.Model;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftFormViewModel : ObservableObject
    {
        private Shift _shift;
        private ShiftItemManager _manager;

        public ShiftFormViewModel(ShiftItemManager manager, Shift shift)
        {
            SaveCommand = new Command(Save);
            _manager = manager;
            _shift = shift;
        }


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
            get => _shift.TimeTo;
            set
            {
                _shift.TimeTo = value.Date.Add(_shift.TimeTo.TimeOfDay);
                RaisePropertyChanged("DateTo");
            }
        }

        public DateTime DepartureDate
        {
            get => _shift.DepartureTime;
            set
            {
                _shift.DepartureTime = value.Date.Add(_shift.DepartureTime.TimeOfDay);
                RaisePropertyChanged("DepartureDate");
            }
        }

        public DateTime ArrivalDate
        {
            get => _shift.ArrivalTime;
            set
            {
                _shift.ArrivalTime = value.Date.Add(_shift.ArrivalTime.TimeOfDay);
                RaisePropertyChanged("ArrivalDate");
            }
        }

        public TimeSpan TimeFrom
        {
            get => _shift.TimeFrom.TimeOfDay;
            set
            {
                _shift.TimeFrom = _shift.TimeFrom.Date.Add(TruncTime(value));
                RaisePropertyChanged("TimeFrom");
            }
        }

        public TimeSpan TimeTo
        {
            get => _shift.TimeTo.TimeOfDay;
            set
            {
                _shift.TimeTo = _shift.TimeTo.Date.Add(TruncTime(value));
                RaisePropertyChanged("TimeTo");
            }
        }

        public TimeSpan DepartureTime
        {
            get => _shift.DepartureTime.TimeOfDay;
            set
            {
                _shift.DepartureTime = _shift.DepartureTime.Date.Add(TruncTime(value));
                RaisePropertyChanged("DepartureTime");
            }
        }

        public TimeSpan ArrivalTime
        {
            get => _shift.ArrivalTime.TimeOfDay;
            set
            {
                _shift.ArrivalTime = _shift.ArrivalTime.Date.Add(TruncTime(value));
                RaisePropertyChanged("ArrivalTime");
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

        public string DepartureLocation
        {
            get => _shift.DepartureLocation;
            set
            {
                _shift.DepartureLocation = value;
                RaisePropertyChanged("DepartureLocation");
            }
        }

        public string ArrivalLocation
        {
            get => _shift.ArrivalLocation;
            set
            {
                _shift.ArrivalLocation = value;
                RaisePropertyChanged("ArrivalLocation");
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

        public bool WithDiets
        {
            get => _shift.WithDiets;
            set
            {
                _shift.WithDiets = value;
                RaisePropertyChanged("WithDiets");
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

        #endregion


        public Action OnSucces { get; set; }

        private async void Save(object obj)
        {
            bool result;
            App.LoadingService.StartLoading("odosielam");
            try
            {
                await _manager.SaveTaskAsync(_shift);
                result = true;
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Nepodarilo sa odoslať", e.Message);
                result = false;
            }
            App.LoadingService.StopLoading();
            await Application.Current.MainPage.Navigation.PopAsync();
            if (result)
            {
                OnSucces?.Invoke();
            }
        }


        #region private helpers

        private TimeSpan TruncTime(TimeSpan timeSpan)
            => TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes / 5) * 5);

        #endregion
    }
}
