using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class IncomeDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public IncomeDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Income>().Wait();
        }

        public Task<List<Income>> GetItemsAsync() => _database.Table<Income>().ToListAsync();
        public Task<Income> GetItemAsync(int id) => _database.Table<Income>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SaveItemAsync(Income item) => item.Id != 0 ? _database.UpdateAsync(item) : _database.InsertAsync(item);
        public Task<int> DeleteItemAsync(Income item) => _database.DeleteAsync(item);
    }
}