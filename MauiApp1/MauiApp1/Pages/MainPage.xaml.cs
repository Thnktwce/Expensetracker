using MauiApp1.Models;

namespace MauiApp1.Views
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

            // Получаем все расходы из базы данных
            var expenses = await App.ExpenseDatabase.GetItemsAsync();
            ExpensesCollection.ItemsSource = expenses;

            // Суммируем все расходы
            decimal totalAmount = expenses.Sum(expense => expense.Amount);
            TotalAmountLabel.Text = $"Общая сумма расходов: {totalAmount:C}";
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpensePage());
        }

        private async void OnEditExpenseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var expense = button?.BindingContext as Expense;
            if (expense != null)
                await Navigation.PushAsync(new AddExpensePage(expense));
        }
    }
}
