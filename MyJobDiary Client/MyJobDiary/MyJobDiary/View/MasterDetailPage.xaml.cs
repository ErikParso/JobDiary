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

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item == null)
                return;

            var page = await CreateDetailPage(item.Page);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }

        private async Task<Page> CreateDetailPage(Pages page)
        {
            switch (page)
            {
                case Pages.MainPage: return new MainPage();
                case Pages.Shifts:
                    var manager = CachedTableManager<Shift>.Current.Value;
                    var dietItems = await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync();
                    var calc = new DietCalculationService(dietItems);
                    ShiftListViewModel viewModel = new ShiftListViewModel(manager, calc);
                    return new ShiftListContentPage(viewModel, false);
                case Pages.Diets:
                    manager = CachedTableManager<Shift>.Current.Value;
                    dietItems = await CachedTableManager<DietPaymentItem>.Current.Value.GetAsync();
                    calc = new DietCalculationService(dietItems);
                    viewModel = new ShiftListViewModel(manager, calc);
                    return new ShiftListContentPage(viewModel, true);
                case Pages.Attendance:
                    manager = CachedTableManager<Shift>.Current.Value;
                    var items = await manager.GetAsync();
                    return new AttendanceList(new AttendanceListViewModel(items));
                case Pages.Settings:
                    var dietManager = CachedTableManager<DietPaymentItem>.Current.Value;
                    return new DietPaymentItemList(new DietsPaymentViewModel(dietManager));
                default: throw new NotImplementedException($"Page {page} is not implemented.");
            }
        }
    }
}