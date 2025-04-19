using SQLite;
using MauiApp1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<List<Expense>> GetExpensesAsync()
        {
            return _database.Table<Expense>().ToListAsync();
        }

        public Task<Expense> GetExpenseByIdAsync(int id)
        {
            return _database.Table<Expense>().FirstOrDefaultAsync(e => e.Id == id);
        }

        public Task<int> SaveExpenseAsync(Expense expense)
        {
            return expense.Id != 0
                ? _database.UpdateAsync(expense)
                : _database.InsertAsync(expense);
        }

        public Task<int> DeleteExpenseAsync(Expense expense)
        {
            return _database.DeleteAsync(expense);
        }

        public async Task<decimal> GetTotalAmountAsync()
        {
            var expenses = await GetExpensesAsync();
            return expenses.Sum(e => e.Amount);
        }
    }
}
