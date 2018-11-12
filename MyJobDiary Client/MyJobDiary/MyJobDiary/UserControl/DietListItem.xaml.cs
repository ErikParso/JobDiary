using Autofac;
using MyJobDiary.Model;
using MyJobDiary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.UserControl
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DietListItem : ContentView
	{
		public DietListItem ()
		{
			InitializeComponent ();
		}

        private void OnDietSumTapped(object sender, EventArgs e)
        {
            var item = BindingContext as Shift;
            IDialogService dialogService = App.CurrentAppContainer.Resolve<IDialogService>();
            dialogService.ShowDialog(item.DietSumString, BuildcalculationInfo(item));
        }

        private string BuildcalculationInfo(Shift item)
            => string.Join(Environment.NewLine,
               item.DietCalculationItems.Select(c => string.Format("{0:hh\\:mm} - {1:hh\\:mm}   {2:N2}{3}",
               c.TimeFrom, c.TimeTo, c.Reward, c.Currency)));
    }
}