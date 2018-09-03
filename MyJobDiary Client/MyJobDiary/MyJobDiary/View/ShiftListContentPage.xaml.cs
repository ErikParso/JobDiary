using MyJobDiary.Extensions;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;
using System.Linq;
using Xamarin.Forms;

namespace MyJobDiary.View
{
    public partial class ShiftListContentPage : ContentPage
    {
        private ShiftListViewModel _viewModel;

        public bool ShowsDiets { get; private set; }

        public ShiftListContentPage(ShiftListViewModel model, bool showsDiets)
        {
            InitializeComponent();
            ShowsDiets = showsDiets;
            BindingContext = model;
            _viewModel = model;
        }

        private async void OnEdit(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var copy = (menuItem.CommandParameter as Shift).CopyCreate(true);
            var shiftsManager = CachedTableManager<Shift>.Current.Value;
            var countries = (await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync())
                .Select(c => c.Country).Distinct().ToList();
            ShiftFormViewModel viewModel = new ShiftFormViewModel(shiftsManager, countries, copy);
            viewModel.OnSaved = _viewModel.ReloadItems;
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        private async void OnCopy(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var copy = (menuItem.CommandParameter as Shift).CopyCreate(false);
            var shiftsManager = CachedTableManager<Shift>.Current.Value;
            var countries = (await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync())
                .Select(c => c.Country).Distinct().ToList();
            ShiftFormViewModel viewModel = new ShiftFormViewModel(shiftsManager, countries, copy);
            viewModel.OnSaved = _viewModel.ReloadItems;
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var original = menuItem.CommandParameter as Shift;
            var manager = CachedTableManager<Shift>.Current.Value;
            await manager.DeleteAsync(original);
            _viewModel.ReloadItems();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}

