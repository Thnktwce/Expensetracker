using SQLite;

// Правильное пространство имен
namespace Expensetracker.Models
{
    public class Income
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? Comment { get; set; }
        public DateTime Date { get; set; }
    }
}