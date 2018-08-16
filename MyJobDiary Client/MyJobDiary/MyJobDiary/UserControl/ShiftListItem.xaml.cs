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
    public partial class ShiftListItem : ContentView
    {
        public ShiftListItem()
        {
            InitializeComponent();
        }
    }
}