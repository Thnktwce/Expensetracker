using Expensetracker.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Expensetracker.Services
{
    public class CategoryDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public CategoryDatabase()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "categories.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            // Создаем таблицу асинхронно, не блокируя конструктор
            InitAsync();
        }

        private async Task InitAsync()
        {
            await _database.CreateTableAsync<Category>();

            // Если категорий еще нет, добавляем несколько по умолчанию
            if (await _database.Table<Category>().CountAsync() == 0)
            {
                var defaultCategories = new List<Category>
                {
                    new Category { Name = "Еда" },
                    new Category { Name = "Транспорт" },
                    new Category { Name = "Жилье" },
                    new Category { Name = "Развлечения" },
                    new Category { Name = "Здоровье" },
                    new Category { Name = "Другое" },
                };
                await _database.InsertAllAsync(defaultCategories);
            }
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            return _database.Table<Category>().OrderBy(c => c.Name).ToListAsync();
        }

        public Task<int> SaveCategoryAsync(Category item)
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

        public Task<int> DeleteCategoryAsync(Category item)
        {
            return _database.DeleteAsync(item);
        }
    }
}