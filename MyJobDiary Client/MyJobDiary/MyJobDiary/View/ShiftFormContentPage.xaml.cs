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
		public ShiftFormContentPage(ShiftFormViewModel viewModel)
		{
			InitializeComponent();
            viewModel.PickFromPlacemark = PickFromPlacemark;
            BindingContext = viewModel;
		}

        private async Task<string> PickFromPlacemark(Placemark placemark)
            => await DisplayActionSheet("Vyber aktuálnu polohu:", null, null, PrepareOptions(placemark).ToArray());

        private IEnumerable<string> PrepareOptions(Placemark placemark)
        {
            var ret = new List<string>();
            if (!string.IsNullOrWhiteSpace(placemark.Locality) && !ret.Contains(placemark.Locality))
                ret.Add(placemark.Locality);
            if (!string.IsNullOrWhiteSpace(placemark.SubLocality) && !ret.Contains(placemark.SubLocality))
                ret.Add(placemark.SubLocality);
            if (!string.IsNullOrWhiteSpace(placemark.AdminArea) && !ret.Contains(placemark.AdminArea))
                ret.Add(placemark.AdminArea);
            if (!string.IsNullOrWhiteSpace(placemark.SubAdminArea) && !ret.Contains(placemark.SubAdminArea))
                ret.Add(placemark.SubAdminArea);
            if (!string.IsNullOrWhiteSpace(placemark.Thoroughfare) && !ret.Contains(placemark.Thoroughfare))
                ret.Add(placemark.Thoroughfare);
            if (!string.IsNullOrWhiteSpace(placemark.CountryName) && !ret.Contains(placemark.CountryName))
                ret.Add(placemark.CountryName);
            return ret;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }

    }
}