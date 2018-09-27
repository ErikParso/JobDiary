using MyJobDiary.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendanceList : ContentPage
	{
        private readonly AttendanceListViewModel _viewModel;

        public AttendanceList (AttendanceListViewModel viewModel)
		{
			InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.Reload();
        }
    }
}