using System;
using System.IO;
using MauiApp1.Services; // Подключаем наш сервис
using MauiApp1.Models;   // Подключаем модель, если понадобится

namespace MauiApp1
{
    public partial class App : Application
    {
        private static ExpenseDatabase database;

        public static ExpenseDatabase Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.db3");
                    database = new ExpenseDatabase(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
