using MauiApp1.Services;
using MauiApp1.Models;

namespace MauiApp1
{
    public partial class App : Application
    {
        // Добавляем свойства для баз данных
        public static ExpenseDatabase ExpenseDatabase { get; private set; }
        public static IncomeDatabase IncomeDatabase { get; private set; }

        public App()
        {
            InitializeComponent();

            // Инициализируем базу данных
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "expenses.db3");

            // Инициализация баз данных
            ExpenseDatabase = new ExpenseDatabase(dbPath);
            IncomeDatabase = new IncomeDatabase(dbPath);

            // Устанавливаем главную страницу через AppShell
            MainPage = new AppShell();
        }
    }
}
