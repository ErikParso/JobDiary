using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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

        public async Task<IEnumerable<Shift>> GetTodoItemsAsync()
        {
            return await todoTable.ToEnumerableAsync();
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

        public async Task DeleteAsync(Shift item)
        {
            await todoTable.DeleteAsync(item);
        }
    }
}