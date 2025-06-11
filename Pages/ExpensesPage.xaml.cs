using Expensetracker.Models;

namespace Expensetracker.Views;

public partial class ExpensesPage : ContentPage
{
    public ExpensesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // 1. «агружаем все категории
        var categories = await App.CategoryDatabase.GetCategoriesAsync();
        // ѕреобразуем в словарь дл€ быстрого поиска: {Id => Name}
        var categoryMap = categories.ToDictionary(c => c.Id, c => c.Name);

        // 2. «агружаем все расходы
        var expenses = await App.ExpenseDatabase.GetItemsAsync();

        // 3. ƒл€ каждого расхода находим и присваиваем им€ категории
        foreach (var expense in expenses)
        {
            // »спользуем .GetValueOrDefault() на случай, если категори€ была удалена
            expense.CategoryName = categoryMap.GetValueOrDefault(expense.CategoryId, "Ѕез категории");
        }

        // 4. ќтображаем обновленный список в CollectionView
        expensesCollectionView.ItemsSource = expenses;
    }

    // ... остальные методы (OnAddExpenseClicked, OnSelectionChanged) остаютс€ без изменений ...
    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddExpensePage));
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Expense selectedExpense)
        {
            await Shell.Current.GoToAsync(nameof(AddExpensePage), true, new Dictionary<string, object>
            {
                { "Expense", selectedExpense }
            });
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}