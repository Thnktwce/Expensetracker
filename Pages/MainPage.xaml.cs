using Expensetracker.Services;
using Expensetracker.Views;

// Оборачиваем класс в правильное пространство имен
namespace Expensetracker.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateUiAsync();
        }

        private async Task UpdateUiAsync()
        {
            var expenses = await App.ExpenseDatabase.GetItemsAsync();
            var incomes = await App.IncomeDatabase.GetItemsAsync();

            // Используем оператор ?? 0m, чтобы обработать возможный null
            decimal totalExpenses = expenses.Sum(e => e.Amount) ?? 0m;
            decimal totalIncomes = incomes.Sum(i => i.Amount) ?? 0m;

            var selectedCurrency = CurrencyService.GetCurrency();

            TotalIncomesLabel.Text = $"+{totalIncomes:F2} {selectedCurrency}";
            TotalExpensesLabel.Text = $"-{totalExpenses:F2} {selectedCurrency}";
        }

        // ... все остальные ваши методы OnClicked ...
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddExpensePage));
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddIncomePage));
        }

        private async void OnViewExpensesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ExpensesPage));
        }

        private async void OnViewIncomesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(IncomesPage));
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
        private async void OnViewChartsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ChartsPage));
        }
    }
}