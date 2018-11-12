using Autofac;
using MyJobDiary.Managers;
using MyJobDiary.Model;
using MyJobDiary.Services;
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
    public partial class MasterDetailPage : Xamarin.Forms.MasterDetailPage
    {

        public MasterDetailPage(MasterViewModel masterViewModel)
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            MasterPage.BindingContext = masterViewModel;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item == null)
                return;

            var page = CreateDetailPage(item.Page);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        private Page CreateDetailPage(Pages page)
        {
            switch (page)
            {
                case Pages.MainPage:
                    return new MainPage();
                case Pages.Shifts:
                    return new ShiftListContentPage(App.CurrentAppContainer.Resolve<ShiftListViewModel>(), false);
                case Pages.Diets:
                    return new ShiftListContentPage(App.CurrentAppContainer.Resolve<ShiftListViewModel>(), true);
                case Pages.Attendance:
                    return new AttendanceList(App.CurrentAppContainer.Resolve<AttendanceListViewModel>());
                case Pages.Settings:
                    return new DietPaymentItemList(App.CurrentAppContainer.Resolve<DietsPaymentViewModel>());
                default:
                    throw new NotImplementedException($"Page {page} is not implemented.");
            }
        }
    }
}