using Expensetracker.Models;
using Expensetracker.Views; // Убедитесь, что этот using есть

namespace Expensetracker.Views
{
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Обновляем список расходов при каждом открытии страницы
            expensesCollectionView.ItemsSource = await App.ExpenseDatabase.GetItemsAsync();
        }

        // ВОТ НЕДОСТАЮЩИЙ МЕТОД, КОТОРЫЙ ИЩЕТ ВАШ XAML
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            // Переходим на страницу добавления нового расхода
            await Shell.Current.GoToAsync(nameof(AddExpensePage));
        }
    }
}