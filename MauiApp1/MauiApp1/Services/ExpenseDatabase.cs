using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class ExpenseDatabase
    {
        private readonly SQLiteAsyncConnection _database;
        public ExpenseDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Expense>().Wait();
        }
        public Task<List<Expense>> GetAllExpensesAsync() =>
            _database.Table<Expense>().OrderByDescending(e => e.Date).ToListAsync();
        public Task<int> SaveExpenseAsync(Expense expense) =>
            _database.InsertAsync(expense);

        public Task<int> DeleteExpenseAsync(Expense expense) =>
            _database.DeleteAsync(expense);
    }
}