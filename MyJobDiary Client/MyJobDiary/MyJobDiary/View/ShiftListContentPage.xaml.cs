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
            var original = menuItem.CommandParameter as Shift;
            var editCopy = original.CopyCreate();
            editCopy.Id = original.Id;
            ShiftFormViewModel viewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, editCopy);
            viewModel.OnSucces = () =>
            {
                _viewModel.ItemEdited(original, editCopy);
            };
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var original = menuItem.CommandParameter as Shift;
            _viewModel.ItemDeleted(original);
        }

        private async void OnCopy(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var original = menuItem.CommandParameter as Shift;
            var copy = original.CopyCreate();
            int dayDifference = (DateTime.Now - original.TimeFrom).Days;
            copy.TimeFrom = original.TimeFrom.AddDays(dayDifference);
            copy.TimeTo = original.TimeTo.AddDays(dayDifference);
            copy.DepartureTime = original.DepartureTime.AddDays(dayDifference);
            copy.ArrivalTime = original.ArrivalTime.AddDays(dayDifference);
            ShiftFormViewModel viewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, copy);
            viewModel.OnSucces = () =>
            {
                _viewModel.CopyCreated(copy);
            };
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}

