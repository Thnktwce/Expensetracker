using SQLite;
using System;

namespace MauiApp1.Models
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Name { get; set; } // Название

        public string Description { get; set; } // ← Добавляем описание

        public string Comment { get; set; }

        public DateTime Date { get; set; }
    }
}
