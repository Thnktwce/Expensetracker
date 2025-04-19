using MauiApp1.Models;
using System.Linq;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent(); // Инициализация элементов интерфейса из XAML
        }

        // Метод, который вызывается при отображении страницы
        protected override async void OnAppearing()
        {
            base.OnAppearing(); // Вызываем базовую реализацию

            // Получаем все расходы из базы данных
            var expenses = await App.ExpenseDatabase.GetItemsAsync();

            // Получаем все доходы из базы данных
            var incomes = await App.IncomeDatabase.GetItemsAsync();

            // Суммируем все расходы и доходы
            decimal totalExpenses = expenses.Sum(expense => expense.Amount);
            decimal totalIncomes = incomes.Sum(income => income.Amount);

            // Обновляем текст лейблов на экране
            TotalExpensesLabel.Text = $"Общая сумма расходов: {totalExpenses:C}";
            TotalIncomesLabel.Text = $"Общая сумма доходов: {totalIncomes:C}";
        }

        // Метод для перехода на вкладку "Расходы"
        private async void OnViewExpensesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Расходы");
        }

        // Метод для перехода на вкладку "Доходы"
        private async void OnViewIncomesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Доходы");
        }
    }
}
