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

        #endregion


        public ShiftFormViewModel(ShiftItemManager manager, Shift shift)
        {
            SaveCommand = new Command(Save);
            _manager = manager;
            _shift = shift;
        }

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

        private TimeSpan TruncTime(TimeSpan timeSpan)
            => TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes / 5) * 5);
    }
}
