using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using MyJobDiary.Model;

namespace MyJobDiary.Managers
{
    public partial class ShiftItemManager
    {
        public static Lazy<ShiftItemManager> Current = new Lazy<ShiftItemManager>(() => new ShiftItemManager());

        private IMobileServiceTable<Shift> todoTable;

        public MobileServiceClient CurrentClient { get; private set; }

        private ShiftItemManager()
        {
            this.CurrentClient = new MobileServiceClient(Constants.ApplicationURL);
            this.todoTable = CurrentClient.GetTable<Shift>();
        }

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
