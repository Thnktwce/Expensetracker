using Expensetracker.Services;

namespace Expensetracker;

public partial class App : Application
{
    private static ExpenseDatabase? _expenseDatabase;
    public static ExpenseDatabase ExpenseDatabase
    {
        get
        {
            _expenseDatabase ??= new ExpenseDatabase();
            return _expenseDatabase;
        }
    }

    private static IncomeDatabase? _incomeDatabase;
    public static IncomeDatabase IncomeDatabase
    {
        get
        {
            _incomeDatabase ??= new IncomeDatabase();
            return _incomeDatabase;
        }
    }

    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}