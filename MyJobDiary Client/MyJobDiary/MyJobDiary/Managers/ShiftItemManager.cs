using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;

namespace MyJobDiary.Managers
{
    public partial class ShiftItemManager
    {
        public static Lazy<ShiftItemManager> Current =
            new Lazy<ShiftItemManager>(() => new ShiftItemManager());

        private IMobileServiceTable<Shift> todoTable;

        private List<Shift> _cachedItems;

        private ShiftItemManager()
        {
            todoTable = MyClient.Current.Value.GetTable<Shift>();
        }

        public async Task<IEnumerable<Shift>> GetTodoItemsAsync()
        {
            await LoadIfNotCached();
            return _cachedItems;
        }

        public async Task SaveTaskAsync(Shift item)
        {
            await LoadIfNotCached();
            App.LoadingService.StartLoading("Odosielam položku");
            try
            {
                if (item.Id == null)
                {
                    await todoTable.InsertAsync(item);
                    _cachedItems.Add(item);
                }
                else
                {
                    await todoTable.UpdateAsync(item);
                    _cachedItems.RemoveAll(i => i.Id == item.Id);
                    _cachedItems.Add(item);
                }
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Chyba spojenia", e.Message);
            }
            App.LoadingService.StopLoading();
        }

        public async Task DeleteAsync(Shift item)
        {
            await LoadIfNotCached();
            App.LoadingService.StartLoading("Mažem položky");
            try
            {
                await todoTable.DeleteAsync(item);
                _cachedItems.RemoveAll(i => i.Id == item.Id);
            }
            catch (Exception e)
            {
                App.DialogService.ShowDialog("Chyba spojenia", e.Message);
            }
            App.LoadingService.StopLoading();
        }

        private async Task LoadIfNotCached()
        {
            if (_cachedItems == null)
            {
                App.LoadingService.StartLoading("Naèítavam zoznam");
                try
                {
                    _cachedItems = (await todoTable.ToEnumerableAsync()).ToList();
                }
                catch (Exception e)
                {
                    App.DialogService.ShowDialog("Chyba spojenia", e.Message);
                }
                App.LoadingService.StopLoading();
            }
        }
    }
}
