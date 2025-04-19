using MauiApp1.Services;

namespace MauiApp1
{
    public partial class App : Application
    {
        static ExpenseDatabase expenseDatabase;
        static IncomeDatabase incomeDatabase;

        public static ExpenseDatabase ExpenseDatabase => expenseDatabase ??= new ExpenseDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.db3"));
        public static IncomeDatabase IncomeDatabase => incomeDatabase ??= new IncomeDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "income.db3"));

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}