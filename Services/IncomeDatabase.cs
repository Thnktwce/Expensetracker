using Expensetracker.Models; // <-- Исправлено с MauiApp1 на Expensetracker
using SQLite;

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
            return _database.Table<Income>().ToListAsync();
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
    }
}