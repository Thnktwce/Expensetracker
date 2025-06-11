using Expensetracker.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expensetracker.Services
{
    public class ExpenseDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public ExpenseDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "expenses.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
        }

        public Task<List<Expense>> GetItemsAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<int> SaveItemAsync(Expense item)
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

        public Task<int> DeleteItemAsync(Expense item)
        {
            return _database.DeleteAsync(item);
        }
    }
}