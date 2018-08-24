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
	public partial class MonthNavigation : ContentView
	{
		public MonthNavigation ()
		{
			InitializeComponent ();
		}
	}
}