using MyJobDiary.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DietPaymentItemList : ContentPage
    {
        public DietPaymentItemList()
        {
            InitializeComponent();
            BindingContext = new DietsPaymentViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}