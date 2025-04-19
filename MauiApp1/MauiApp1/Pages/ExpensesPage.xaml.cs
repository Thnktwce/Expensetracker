using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1.Views
{
    public partial class ExpensesPage : ContentPage
    {
        public ObservableCollection<Expense> Expenses { get; set; }

        public ExpensesPage()
        {
            InitializeComponent();
            LoadExpenses(); // Загрузка расходов при инициализации
        }

        // Загрузка всех расходов из базы данных
        private async void LoadExpenses()
        {
            var expenses = await App.ExpenseDatabase.GetItemsAsync();
            Expenses = new ObservableCollection<Expense>(expenses);
            ExpensesCollectionView.ItemsSource = Expenses; // Обновляем источник данных для CollectionView
        }

        // Обработчик для добавления нового расхода
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpensePage());
        }

        // Обработчик для редактирования расхода
        private async void OnEditExpenseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var expense = button?.BindingContext as Expense;

            if (expense != null)
            {
                await Navigation.PushAsync(new AddExpensePage(expense));
            }
        }

        // Обработчик для удаления расхода
        private async void OnDeleteExpenseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var expense = button?.BindingContext as Expense;

            if (expense != null)
            {
                var result = await DisplayAlert("Подтвердите", "Вы уверены, что хотите удалить этот расход?", "Да", "Нет");
                if (result)
                {
                    await App.ExpenseDatabase.DeleteItemAsync(expense.Id); // Удаление расхода из базы данных
                    Expenses.Remove(expense); // Удаляем расход из ObservableCollection
                }
            }
        }

        // Обработчик после возвращения с добавления нового расхода
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadExpenses(); // Перезагружаем список при возвращении на страницу
        }
    }
}
