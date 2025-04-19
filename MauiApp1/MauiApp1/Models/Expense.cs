using SQLite;
using System;

namespace MauiApp1.Models
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; } // Название

        public decimal Amount { get; set; } // Сумма

        public DateTime Date { get; set; } // Дата

        public string Comment { get; set; } // Комментарий
    }
}
