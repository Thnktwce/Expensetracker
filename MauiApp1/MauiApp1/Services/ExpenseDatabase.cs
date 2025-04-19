using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class ExpenseDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public ExpenseDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
        }

        public Task<List<Expense>> GetItemsAsync() => _database.Table<Expense>().ToListAsync();
        public Task<Expense> GetItemAsync(int id) => _database.Table<Expense>().Where(i => i.Id == id).FirstOrDefaultAsync();
        public Task<int> SaveItemAsync(Expense item) => item.Id != 0 ? _database.UpdateAsync(item) : _database.InsertAsync(item);
        public Task<int> DeleteItemAsync(Expense item) => _database.DeleteAsync(item);
    }
}