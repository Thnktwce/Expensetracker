using MauiApp1.Services;

namespace MauiApp1
{
    public partial class App : Application
    {
        public static ExpenseDatabase ExpenseDatabase { get; private set; }

        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "expenses.db3");
            ExpenseDatabase = new ExpenseDatabase(dbPath);

            MainPage = new AppShell();
        }
    }
}
