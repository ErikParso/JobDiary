using Autofac;
using MyJobDiary.ViewModel;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyJobDiary.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        public readonly ListView ListView;

        private readonly MasterViewModel _viewModel;

        public MasterPage()
        {
            InitializeComponent();
            _viewModel = App.CurrentAppContainer.Resolve<MasterViewModel>();
            _viewModel.LogoutSuccessfull = LogoutSuccessfull;
            BindingContext = _viewModel;
            ListView = listView;
        }

        private void LogoutSuccessfull()
        {
            App.Current.MainPage = App.CurrentAppContainer.Resolve<LoginPage>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.ReloadUserInformation();
        }
    }
}