using MyJobDiary.Model;
using MyJobDiary.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietPaymentItemList : ContentPage
    {
        private readonly DietsPaymentViewModel _viewModel;

        public DietPaymentItemList(DietsPaymentViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        private void OnDelete(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var item = (menuItem.CommandParameter as DietPaymentItem);
            _viewModel.DeleteItem(item);
        }
    }
}