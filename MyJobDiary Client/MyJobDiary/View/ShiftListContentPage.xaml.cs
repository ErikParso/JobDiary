using MyJobDiary.Extensions;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyJobDiary.View
{
    public partial class ShiftListContentPage : ContentPage
    {
        private ShiftListViewModel _model;

        public ShiftListContentPage(ShiftListViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
            _model = model;
        }

        private async void OnEdit(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var item = menuItem.CommandParameter as Shift;
            ShiftFormViewModel viewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, item);
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        private async void OnDelete(object sender, EventArgs e)
        {
            App.LoadingService.StartLoading("Mažem záznam");
            var menuItem = sender as MenuItem;
            var item = menuItem.CommandParameter as Shift;
            await _model.Manager.DeleteAsync(item);
            App.LoadingService.StopLoading();
            _model.Refresh();
        }

        private async void OnCopy(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var item = menuItem.CommandParameter as Shift;
            var copy = item.CopyCreate();
            copy.TimeFrom = DateTime.Now.Date.Add(item.TimeFrom.TimeOfDay);
            copy.TimeTo = DateTime.Now.Date.Add(item.TimeTo.TimeOfDay);
            ShiftFormViewModel viewModel = new ShiftFormViewModel(ShiftItemManager.Current.Value, copy);
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel);
            await Navigation.PushAsync(shiftForm);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
            _model.Refresh();
        }
    }
}

