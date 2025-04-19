using SQLite;
using MauiApp1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public Task<int> SaveItemAsync(Expense item)
        {
            if (item.Id != 0)
                return _database.UpdateAsync(item);
            else
                return _database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Expense item) => _database.DeleteAsync(item);
        public Task<int> UpdateItemAsync(Expense item) => _database.UpdateAsync(item);

        public async Task DeleteItemAsync(int id)
        {
            var expense = await GetItemAsync(id);
            if (expense != null)
            {
                await _database.DeleteAsync(expense);
            }
        }
    }
}
