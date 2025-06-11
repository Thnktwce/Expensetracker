using Expensetracker.Services;
using Expensetracker.Models;

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

            if (CurrencyPicker.SelectedItem == null)
            {
                CurrencyPicker.SelectedItem = CurrencyService.GetCurrency();
            }

            await UpdateUiAsync();
        }

        private async Task UpdateUiAsync()
        {
            var expenses = await App.ExpenseDatabase.GetItemsAsync();
            var incomes = await App.IncomeDatabase.GetItemsAsync();

            decimal totalExpenses = expenses.Sum(e => e.Amount);
            decimal totalIncomes = incomes.Sum(i => i.Amount);

            var selectedCurrency = CurrencyService.GetCurrency();

            TotalExpensesLabel.Text = $"{totalExpenses:F2} {selectedCurrency}";
            TotalIncomesLabel.Text = $"{totalIncomes:F2} {selectedCurrency}";
        }

        private async void CurrencyPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ЗАЩИТА ОТ СБОЯ ПРИ ПЕРВОЙ ЗАГРУЗКЕ
            if (CurrencyPicker == null || CurrencyPicker.SelectedItem == null)
            {
                return; // Ничего не делаем, если страница еще не готова
            }

            if (CurrencyPicker.SelectedItem is string currency)
            {
                CurrencyService.SetCurrency(currency);
                await UpdateUiAsync();
            }
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddExpensePage));
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddIncomePage));
        }
    }
}