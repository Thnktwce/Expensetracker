using Expensetracker.Views;

namespace Expensetracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Регистрируем маршруты для ВСЕХ страниц, на которые будем переходить
            Routing.RegisterRoute(nameof(ExpensesPage), typeof(ExpensesPage));
            Routing.RegisterRoute(nameof(IncomesPage), typeof(IncomesPage));
            Routing.RegisterRoute(nameof(AddExpensePage), typeof(AddExpensePage));
            Routing.RegisterRoute(nameof(AddIncomePage), typeof(AddIncomePage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }
    }
}