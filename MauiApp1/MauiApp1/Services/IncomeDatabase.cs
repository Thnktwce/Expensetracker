using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class IncomeDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public IncomeDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Income>().Wait();
        }

        public Task<List<Income>> GetItemsAsync()
        {
            return _database.Table<Income>().OrderByDescending(i => i.Date).ToListAsync();
        }

        public Task<Income> GetItemAsync(int id)
        {
            return _database.Table<Income>().FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<int> SaveItemAsync(Income income)
        {
            if (income.Id != 0)
                return _database.UpdateAsync(income);
            return _database.InsertAsync(income);
        }

        public Task<int> DeleteItemAsync(Income income)
        {
            return _database.DeleteAsync(income);
        }
    }
}
