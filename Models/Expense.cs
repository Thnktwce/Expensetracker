using SQLite;

namespace Expensetracker.Models
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string? Description { get; set; } // <-- ДОБАВЛЕНО ЭТО ПОЛЕ
        public string? Comment { get; set; }
    }
}