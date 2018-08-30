using MyJobDiary.Extensions;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;
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
            ShiftFormViewModel viewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, copy);
            viewModel.OnSaved = _viewModel.ReloadItems;
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }
        private async void OnCopy(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var copy = (menuItem.CommandParameter as Shift).CopyCreate(false);
            ShiftFormViewModel viewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, copy);
            viewModel.OnSaved = _viewModel.ReloadItems;
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var original = menuItem.CommandParameter as Shift;
            await ShiftItemManager.Current.Value.DeleteAsync(original);
            _viewModel.ReloadItems();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}

