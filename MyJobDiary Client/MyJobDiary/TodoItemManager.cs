/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=620342
 */
//#define OFFLINE_SYNC_ENABLED

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;

namespace MyJobDiary
{
    public partial class TodoItemManager
    {
        IMobileServiceTable<Shift> todoTable;

        private TodoItemManager()
        {
            this.Client = new MobileServiceClient(Constants.ApplicationURL);
            this.todoTable = Client.GetTable<Shift>();
        }

        public static TodoItemManager DefaultManager
        {
            get
            {
                return DefaultInstance;
            }
            private set
            {
                DefaultInstance = value;
            }
        }

        public MobileServiceClient CurrentClient
        {
            get { return Client; }
        }

        public bool IsOfflineEnabled
        {
            get { return todoTable is Microsoft.WindowsAzure.MobileServices.Sync.IMobileServiceSyncTable<Shift>; }
        }

        public static TodoItemManager DefaultInstance { get; set; } = new TodoItemManager();
        public MobileServiceClient Client { get; set; }

        public async Task<ObservableCollection<Shift>> GetTodoItemsAsync(bool syncItems = false)
        {
            try
            {
                IEnumerable<Shift> items = await todoTable.ToEnumerableAsync();
                return new ObservableCollection<Shift>(items);
            }
            catch (MobileServiceInvalidOperationException msioe)
            {
                Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            }
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
            }
            return null;
        }

        public async Task SaveTaskAsync(Shift item)
        {
            if (item.Id == null)
            {
                await todoTable.InsertAsync(item);
            }
            else
            {
                await todoTable.UpdateAsync(item);
            }
        }
    }
}
