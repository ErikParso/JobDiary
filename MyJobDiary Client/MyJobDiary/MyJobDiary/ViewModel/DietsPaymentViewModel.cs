using MyJobDiary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyJobDiary.ViewModel
{
    public class DietsPaymentViewModel : ObservableObject
    {
        private List<DietPaymentItem> _payments;

        private string _newLocation;
        private TimeSpan _newTime;
        private double _newPayment;

        public DietsPaymentViewModel()
        {
            Payments = new List<DietPaymentItem>();
            AddPaymentItemCommand = new Command(AddPaymentItem);
            NewLocation = "SK";
            NewPayment = 1;
            NewTime = TimeSpan.Zero;
        }


        #region Bindable properties

        public string NewLocation
        {
            get => _newLocation;
            set => SetField(ref _newLocation, value.ToUpper());
        }

        public TimeSpan NewTime
        {
            get => _newTime;
            set => SetField(ref _newTime, value);
        }

        public double NewPayment
        {
            get => _newPayment;
            set => SetField(ref _newPayment, value);
        }

        public IEnumerable<DietPaymentItem> Payments
        {
            get => _payments.OrderBy(p => p.Location).ThenBy(p => p.Time);
            set => SetField(ref _payments, value.ToList());
        }

        public ICommand AddPaymentItemCommand { get; private set; }

        #endregion


        private void AddPaymentItem()
        {
            _payments.Add(new DietPaymentItem()
            {
                Location = _newLocation,
                Payment = _newPayment,
                Time = _newTime,
            });
            RaisePropertyChanged(nameof(Payments));
        }
    }
}
