using MyJobDiary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShiftFormContentPage : ContentPage
	{
        private readonly ShiftFormViewModel _viewModel;

        public ShiftFormContentPage(ShiftFormViewModel viewModel)
		{
			InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
            AddHandlers();
		}

        private void AddHandlers()
        {
            _viewModel.OnSaved += async () => await Navigation.PopAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Reload();
        }

    }
}