using Autofac;
using MyJobDiary.Extensions;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            _viewModel = model;
            BindingContext = model;
            ShowsDiets = showsDiets;
        }

        private void OnEdit(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var copy = (menuItem.CommandParameter as Shift).CopyCreate(true);
            ShowShiftForm(copy, "Oprava položky");
        }

        private void OnCopy(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var copy = (menuItem.CommandParameter as Shift).CopyCreate(false);
            ShowShiftForm(copy, "Kópia položky");
        }

        private async void ShowShiftForm(Shift copy, string title)
        {
            ShiftFormViewModel viewModel = App.Container.Resolve<ShiftFormViewModel>(new TypedParameter(typeof(Shift), copy));
            ShiftFormContentPage shiftForm = new ShiftFormContentPage(viewModel)
            {
                Title = title
            };
            await Navigation.PushAsync(shiftForm);
        }

        private void OnDelete(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var original = menuItem.CommandParameter as Shift;
            _viewModel.Delete(original);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Reload();
        }
    }
}

