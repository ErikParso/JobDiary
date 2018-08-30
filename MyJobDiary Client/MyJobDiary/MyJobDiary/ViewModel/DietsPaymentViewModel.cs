using MyJobDiary.Managers;
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
        private IEnumerable<DietPaymentItem> _payments;
        private readonly CachedTableManager<DietPaymentItem> _manager;

        private string _newLocation;
        private TimeSpan _newTime;
        private double _newPayment;

        public DietsPaymentViewModel(CachedTableManager<DietPaymentItem> manager)
        {
            _manager = manager;
            _payments = new List<DietPaymentItem>();
            AddPaymentItemCommand = new Command(AddPaymentItem);
            NewLocation = "SK";
            NewPayment = 1;
            NewTime = TimeSpan.Zero;
            ReloadItems();
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
            set => SetField(ref _payments, value);
        }

        public ICommand AddPaymentItemCommand { get; private set; }

        #endregion

        public async void DeleteItem(DietPaymentItem item)
        {
            await _manager.DeleteAsync(item);
            ReloadItems();
        }

        private async void AddPaymentItem()
        {
            await _manager.SaveAsync(new DietPaymentItem()
            {
                Location = _newLocation,
                Payment = _newPayment,
                Time = _newTime,
            });
            ReloadItems();
        }

        private async void ReloadItems()
        {
            Payments = await _manager.GetAsync();
        }
    }
}
