using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;
using MyJobDiary.Services;

namespace MyJobDiary.Managers
{
    public partial class CachedTableManager<T>
        where T : ICachedTableItem
    {
        private readonly IMobileServiceTable<T> _table;
        private readonly ILoadingService _loadingService;
        private readonly IDialogService _dialogService;

        private List<T> _cachedItems;

        public CachedTableManager(
            MobileServiceClient mobileServiceClient,
            ILoadingService loadingService,
            IDialogService dialogservice)
        {
            _table = mobileServiceClient.GetTable<T>();
            _loadingService = loadingService;
            _dialogService = dialogservice;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            await LoadIfNotCached();
            return _cachedItems;
        }

        public async Task SaveAsync(T item)
        {
            await LoadIfNotCached();
            _loadingService.StartLoading("Odosielam položku");
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
                _dialogService.ShowDialog("Chyba spojenia", e.Message);
            }
            _loadingService.StopLoading();
        }

        public async Task DeleteAsync(T item)
        {
            await LoadIfNotCached();
            _loadingService.StartLoading("Mažem položky");
            try
            {
                await _table.DeleteAsync(item);
                _cachedItems.RemoveAll(i => i.Id == item.Id);
            }
            catch (Exception e)
            {
                _dialogService.ShowDialog("Chyba spojenia", e.Message);
            }
            _loadingService.StopLoading();
        }

        private async Task LoadIfNotCached()
        {
            if (_cachedItems == null)
            {
                _loadingService.StartLoading("Naèítavam zoznam");
                try
                {
                    _cachedItems = (await _table.ToEnumerableAsync()).ToList();
                }
                catch (Exception e)
                {
                    _dialogService.ShowDialog("Chyba spojenia", e.Message);
                }
                _loadingService.StopLoading();
            }
        }
    }
}
