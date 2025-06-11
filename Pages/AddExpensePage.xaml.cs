using Expensetracker.Models;
using System.Collections.ObjectModel; // ��������� ��� ObservableCollection

namespace Expensetracker.Views;

[QueryProperty(nameof(CurrentExpense), "Expense")]
public partial class AddExpensePage : ContentPage
{
    public Expense CurrentExpense { get; set; }

    // ��������� ��� �������� ������ ��������� ��� Picker'a
    public ObservableCollection<Category> Categories { get; set; }

    public AddExpensePage()
    {
        InitializeComponent();
        Categories = new ObservableCollection<Category>();
        // ������������� �������� �������� �� ���� ��������
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // ��������� ������ ���������
        await LoadCategories();

        CurrentExpense ??= new Expense { Date = DateTime.Now };

        // ���� �� ����������� ������������ ������, ������� � ������������� ��� ���������
        if (CurrentExpense.Id != 0)
        {
            var selectedCategory = Categories.FirstOrDefault(c => c.Id == CurrentExpense.CategoryId);
            if (selectedCategory != null)
            {
                CategoryPicker.SelectedItem = selectedCategory;
            }
        }

        // ��������� BindingContext, ����� XAML ������ ���������
        OnPropertyChanged(nameof(CurrentExpense));
        DeleteButton.IsVisible = CurrentExpense.Id != 0;
    }

    private async Task LoadCategories()
    {
        var categoriesFromDb = await App.CategoryDatabase.GetCategoriesAsync();
        Categories.Clear();
        foreach (var category in categoriesFromDb)
        {
            Categories.Add(category);
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        // ����������� ID ��������� ��������� ������ �������
        if (CategoryPicker.SelectedItem is Category selectedCategory)
        {
            CurrentExpense.CategoryId = selectedCategory.Id;
        }

        if (string.IsNullOrWhiteSpace(CurrentExpense.Name) || CurrentExpense.Amount is null or <= 0)
        {
            await DisplayAlert("������", "������� �������� � ����� ������ ����.", "��");
            return;
        }

        if (CurrentExpense.CategoryId == 0)
        {
            await DisplayAlert("������", "����������, �������� ���������.", "��");
            return;
        }

        await App.ExpenseDatabase.SaveItemAsync(CurrentExpense);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (CurrentExpense == null) return;

        bool isConfirmed = await DisplayAlert("�������������", "�� �������?", "��", "���");

        if (isConfirmed)
        {
            await App.ExpenseDatabase.DeleteItemAsync(CurrentExpense);
            await Shell.Current.GoToAsync("..");
        }
    }
}