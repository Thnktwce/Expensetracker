using SQLite;

namespace Expensetracker.Models
{
    public class Income
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Позволяем свойству быть null
        public decimal? Amount { get; set; }

        public string? Description { get; set; }
        public DateTime Date { get; set; }
    }
}