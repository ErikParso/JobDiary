using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;

namespace MyJobDiary.Managers
{
    public partial class CachedTableManager<T>
        where T : ICachedTableItem
    {
        public static Lazy<CachedTableManager<T>> Current =
            new Lazy<CachedTableManager<T>>(() => new CachedTableManager<T>());

        private IMobileServiceTable<T> _table;

        private List<T> _cachedItems;

        private CachedTableManager()
        {
            _table = MyClient.Current.Value.GetTable<T>();
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            await LoadIfNotCached();
            return _cachedItems;
        }

        public async Task SaveAsync(T item)
        {
            await LoadIfNotCached();
            App.LoadingService.StartLoading("Odosielam položku");
            try
            {
                if (item.Id == null)
                {
                    await _table.InsertAsync(item);
                    _cachedItems.Add(item);
                }
                else
                {
                    await _table.UpdateAsync(item);
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

        public async Task DeleteAsync(T item)
        {
            await LoadIfNotCached();
            App.LoadingService.StartLoading("Mažem položky");
            try
            {
                await _table.DeleteAsync(item);
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
                    _cachedItems = (await _table.ToEnumerableAsync()).ToList();
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
