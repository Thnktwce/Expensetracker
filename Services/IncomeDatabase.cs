using Expensetracker.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expensetracker.Services
{
    public class IncomeDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public IncomeDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "incomes.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Income>().Wait();
        }

        public Task<List<Income>> GetItemsAsync()
        {
            return _database.Table<Income>().OrderByDescending(i => i.Date).ToListAsync();
        }

        public Task<int> SaveItemAsync(Income item)
        {
            if (item.Id != 0)
            {
                return _database.UpdateAsync(item);
            }
            else
            {
                return _database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Income item)
        {
            return _database.DeleteAsync(item);
        }
    }
}