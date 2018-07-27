using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftFormViewModel : ObservableObject
    {
        public FormMode FormMode
        {
            get; private set;
        }

        private DateTime _dateFrom = DateTime.Now;
        public DateTime DateFrom
        {
            get => _dateFrom;
            set => SetField(ref _dateFrom, value.Date.Add(_dateFrom.TimeOfDay));
        }

        private DateTime _dateTo = DateTime.Now.AddHours(8);
        public DateTime DateTo
        {
            get => _dateTo;
            set => SetField(ref _dateTo, value.Date.Add(_dateTo.TimeOfDay));
        }

        public TimeSpan TimeFrom
        {
            get => _dateFrom.TimeOfDay;
            set => SetField(ref _dateFrom, _dateFrom.Date.Add(value));
        }

        public TimeSpan TimeTo
        {
            get => _dateTo.TimeOfDay;
            set => SetField(ref _dateTo, _dateTo.Date.Add(value));
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => SetField(ref _location, value);
        }

        private string _job;
        public string Job
        {
            get => _job;
            set => SetField(ref _job, value);
        }

        private bool _isNightShift;
        public bool IsNightShift
        {
            get => _isNightShift;
            set => SetField(ref _isNightShift, value);
        }

        private bool _isClosed;
        public bool IsClosed
        {
            get => _isClosed;
            set => SetField(ref _isClosed, value);
        }

        public ICommand SaveCommand { get; private set; }

        public ShiftFormViewModel(FormMode mode)
        {
            SaveCommand = new Command(Save);
            FormMode = mode;
        }

        private void Save(object obj)
        {
            
        }
    }
}
