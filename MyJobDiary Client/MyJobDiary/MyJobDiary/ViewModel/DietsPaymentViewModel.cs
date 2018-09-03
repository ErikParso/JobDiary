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

        private string _country;
        private double _hours;
        private double _reward;

        public DietsPaymentViewModel(CachedTableManager<DietPaymentItem> manager)
        {
            _manager = manager;
            _payments = new List<DietPaymentItem>();
            AddPaymentItemCommand = new Command(AddPaymentItem);
            Country = "SK";
            Reward = 0;
            Hours = 0;
            ReloadItems();
        }


        #region Bindable properties

        public string Country
        {
            get => _country;
            set => SetField(ref _country, value.ToUpper());
        }

        public double Hours
        {
            get => _hours;
            set => SetField(ref _hours, value);
        }

        public double Reward
        {
            get => _reward;
            set => SetField(ref _reward, value);
        }

        public IEnumerable<DietPaymentItem> Payments
        {
            get => _payments.OrderBy(p => p.Country).ThenBy(p => p.Hours);
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
                Country = _country,
                Reward = _reward,
                Hours = _hours,
            });
            ReloadItems();
        }

        private async void ReloadItems()
        {
            Payments = await _manager.GetAsync();
        }
    }
}
