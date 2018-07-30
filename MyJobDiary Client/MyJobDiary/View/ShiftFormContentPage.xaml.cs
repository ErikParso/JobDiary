using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShiftFormContentPage : ContentPage
	{
		public ShiftFormContentPage ()
		{
			InitializeComponent ();
            BindingContext = new ShiftFormViewModel(TodoItemManager.Current.Value, new Shift()
            {
                TimeFrom = DateTime.Now,
                TimeTo = DateTime.Today
            });
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}