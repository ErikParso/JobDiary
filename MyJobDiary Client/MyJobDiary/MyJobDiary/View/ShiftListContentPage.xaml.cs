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

        public ShiftListContentPage(ShiftListViewModel model)
        {
            InitializeComponent();
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
            copy.TimeFrom = DateTime.Now.Date.Add(original.TimeFrom.TimeOfDay);
            copy.TimeTo = DateTime.Now.Date.Add(original.TimeTo.TimeOfDay);
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

        private void shiftList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            shiftList.SelectedItem = null;
        }
    }
}

