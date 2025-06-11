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

        // 1. ��������� ��� ���������
        var categories = await App.CategoryDatabase.GetCategoriesAsync();
        // ����������� � ������� ��� �������� ������: {Id => Name}
        var categoryMap = categories.ToDictionary(c => c.Id, c => c.Name);

        // 2. ��������� ��� �������
        var expenses = await App.ExpenseDatabase.GetItemsAsync();

        // 3. ��� ������� ������� ������� � ����������� ��� ���������
        foreach (var expense in expenses)
        {
            // ���������� .GetValueOrDefault() �� ������, ���� ��������� ���� �������
            expense.CategoryName = categoryMap.GetValueOrDefault(expense.CategoryId, "��� ���������");
        }

        // 4. ���������� ����������� ������ � CollectionView
        expensesCollectionView.ItemsSource = expenses;
    }

    // ... ��������� ������ (OnAddExpenseClicked, OnSelectionChanged) �������� ��� ��������� ...
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