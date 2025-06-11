﻿using SQLite;
using System;

namespace Expensetracker.Models
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal? Amount { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        // НОВОЕ СВОЙСТВО ДЛЯ ОТОБРАЖЕНИЯ ИМЕНИ КАТЕГОРИИ
        // Атрибут [Ignore] говорит базе данных не пытаться сохранить это поле
        [Ignore]
        public string CategoryName { get; set; } = string.Empty;
    }
}