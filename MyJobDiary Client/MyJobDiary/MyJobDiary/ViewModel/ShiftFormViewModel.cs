using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class ShiftFormViewModel : ObservableObject
    {
        #region Private fields

        private readonly CachedTableManager<Shift> _shiftManager;
        private readonly CachedTableManager<DietPaymentItem> _dietManager;
        private readonly IValidationService _validationService;
        private readonly IDialogService _dialogService;
        private readonly ILocationService _locationService;

        private readonly Shift _shift;
        private List<string> _countries;

        #endregion


        #region Constructors and Initialization

        public ShiftFormViewModel(
            CachedTableManager<Shift> shiftManager,
            CachedTableManager<DietPaymentItem> dietManager,
            IValidationService validationService,
            IDialogService dialogService,
            ILocationService locationService,
            Shift shift)
        {
            _shiftManager = shiftManager;
            _dietManager = dietManager;
            _validationService = validationService;
            _dialogService = dialogService;
            _locationService = locationService;
            _shift = shift;

            InitCountries();
            InitCommands();
            AdjustDates();
        }

        private void InitCountries()
        {
            _countries = new List<string>();
            if (!string.IsNullOrWhiteSpace(_shift.Country))
                _countries.Add(_shift.Country);
        }

        private void InitCommands()
        {
            SaveCommand = new Command(Save);
            SetCountryCommand = new Command(SetCountry);
            SetLocationCommand = new Command(SetLocation);
            SetDepartureLocationCommand = new Command(SetDepartureLocation);
            SetArrivalLocationCommand = new Command(SetArrivalLocation);
        }

        public async void Reload()
        {
            var countries = (await _dietManager.GetAsync()).Select(d => d.Country);
            Countries = Countries.Union(countries).Distinct().ToList();
            RaisePropertyChanged(nameof(Country));
        }

        #endregion


        #region Bindable

        public DateTime DateFrom
        {
            get => _shift.TimeFrom;
            set
            {
                _shift.TimeFrom = value.Date.Add(_shift.TimeFrom.TimeOfDay);
                AdjustDates();
            }
        }

        public DateTime DateTo
        {
            get => _shift.TimeTo;
        }

        public DateTime DepartureDate
        {
            get => _shift.DepartureTime;
        }

        public DateTime ArrivalDate
        {
            get => _shift.ArrivalTime;
        }

        public TimeSpan TimeFrom
        {
            get => _shift.TimeFrom.TimeOfDay;
            set
            {
                TimeSpan truncVal = TruncTime(value);
                DateTime newTime = _shift.TimeFrom.Date.Add(truncVal);
                if (TimeFrom != truncVal)
                    _shift.DepartureTime = newTime;
                _shift.TimeFrom = newTime;
                AdjustDates();
            }
        }

        public TimeSpan TimeTo
        {
            get => _shift.TimeTo.TimeOfDay;
            set
            {
                TimeSpan truncVal = TruncTime(value);
                DateTime newTime = _shift.TimeTo.Date.Add(truncVal);
                if (TimeTo != truncVal)
                    _shift.ArrivalTime = newTime;
                _shift.TimeTo = newTime;
                AdjustDates();
            }
        }

        public TimeSpan DepartureTime
        {
            get => _shift.DepartureTime.TimeOfDay;
            set
            {
                _shift.DepartureTime = _shift.DepartureTime.Date.Add(TruncTime(value));
                AdjustDates();
            }
        }

        public TimeSpan ArrivalTime
        {
            get => _shift.ArrivalTime.TimeOfDay;
            set
            {
                _shift.ArrivalTime = _shift.ArrivalTime.Date.Add(TruncTime(value));
                AdjustDates();
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

        public string Country
        {
            get => _shift.Country;
            set
            {
                if (value != null)
                {
                    _shift.Country = value?.ToUpper();
                    RaisePropertyChanged(nameof(Country));
                }
            }
        }

        public List<string> Countries
        {
            get => _countries;
            set => SetField(ref _countries, value);
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

        #endregion


        #region Save

        public ICommand SaveCommand { get; private set; }

        public Action OnSaved { get; set; }

        private async void Save(object obj)
        {
            var items = await _shiftManager.GetAsync();
            if (await _validationService.IsShiftOverlapped(_shift))
                ShowOverlappingError(_shift.TimeFrom);
            await _shiftManager.SaveAsync(_shift);
            OnSaved?.Invoke();
        }

        #endregion


        #region Navigation and Location

        public ICommand SetCountryCommand { get; private set; }

        public ICommand SetLocationCommand { get; private set; }

        public ICommand SetArrivalLocationCommand { get; private set; }

        public ICommand SetDepartureLocationCommand { get; private set; }

        private async void SetCountry(object obj)
        {
            var placemark = await _locationService.GetLocation();
            if (placemark != null)
            {
                if (!Countries.Contains(placemark.CountryCode))
                {
                    Countries.Add(placemark.CountryCode);
                    Countries = Countries.ToList();
                }
                Country = placemark.CountryCode;
            }
        }

        private async void SetLocation(object obj)
        {
            var placemark = await _locationService.GetLocation();
            if (placemark != null)
            {
                string newLoc = placemark.SubLocality;
                if (!string.IsNullOrWhiteSpace(newLoc))
                    Location = newLoc;
            }
        }

        private async void SetDepartureLocation(object obj)
        {
            var placemark = await _locationService.GetLocation();
            if (placemark != null)
            {
                string newLoc = placemark.SubLocality;
                if (!string.IsNullOrWhiteSpace(newLoc))
                    DepartureLocation = newLoc;
            }
        }

        private async void SetArrivalLocation(object obj)
        {
            var placemark = await _locationService.GetLocation();
            if (placemark != null)
            {
                string newLoc = placemark.SubLocality;
                if (!string.IsNullOrWhiteSpace(newLoc))
                    ArrivalLocation = newLoc;
            }
        }

        #endregion


        #region private helpers

        private TimeSpan TruncTime(TimeSpan timeSpan)
            => TimeSpan.FromMinutes(Math.Round(timeSpan.TotalMinutes / 5) * 5);

        private void ShowOverlappingError(DateTime day)
        {
            var detail = $"Časový interval sa prekrýva. Skontrolujte a opravte položky z dňa {day.ToString("dd.mm.yyyy")}. Prekrývajúce sa obdobia môžu spôsobiť problémy v dochádzke a výpočtoch.";
            _dialogService.ShowDialog("Prekrývanie období", detail);
        }

        private void AdjustDates()
        {
            if (_shift.DepartureTime.TimeOfDay > _shift.TimeFrom.TimeOfDay)
                _shift.DepartureTime = _shift.TimeFrom.Date.AddDays(-1).Add(_shift.DepartureTime.TimeOfDay);
            else
                _shift.DepartureTime = _shift.TimeFrom.Date.Add(_shift.DepartureTime.TimeOfDay);

            if (_shift.TimeFrom.TimeOfDay > _shift.TimeTo.TimeOfDay)
                _shift.TimeTo = _shift.TimeFrom.Date.AddDays(1).Add(_shift.TimeTo.TimeOfDay);
            else
                _shift.TimeTo = _shift.TimeFrom.Date.Add(_shift.TimeTo.TimeOfDay);

            if (_shift.TimeTo.TimeOfDay > _shift.ArrivalTime.TimeOfDay)
                _shift.ArrivalTime = _shift.TimeTo.Date.AddDays(1).Add(_shift.ArrivalTime.TimeOfDay);
            else
                _shift.ArrivalTime = _shift.TimeTo.Date.Add(_shift.ArrivalTime.TimeOfDay);

            RaisePropertyChanged(nameof(DepartureDate));
            RaisePropertyChanged(nameof(DateFrom));
            RaisePropertyChanged(nameof(DateTo));
            RaisePropertyChanged(nameof(ArrivalDate));

            RaisePropertyChanged(nameof(DepartureTime));
            RaisePropertyChanged(nameof(TimeFrom));
            RaisePropertyChanged(nameof(TimeTo));
            RaisePropertyChanged(nameof(ArrivalTime));
        }

        #endregion
    }
}
