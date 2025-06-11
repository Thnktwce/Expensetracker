using Expensetracker.Models;
using System.Collections.ObjectModel; // Необходим для ObservableCollection

namespace Expensetracker.Views;

[QueryProperty(nameof(CurrentExpense), "Expense")]
public partial class AddExpensePage : ContentPage
{
    public Expense CurrentExpense { get; set; }

    // Коллекция для хранения списка категорий для Picker'a
    public ObservableCollection<Category> Categories { get; set; }

    public AddExpensePage()
    {
        InitializeComponent();
        Categories = new ObservableCollection<Category>();
        // Устанавливаем контекст привязки на саму страницу
        BindingContext = this;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        // Загружаем список категорий
        await LoadCategories();

        CurrentExpense ??= new Expense { Date = DateTime.Now };

        // Если мы редактируем существующий расход, находим и устанавливаем его категорию
        if (CurrentExpense.Id != 0)
        {
            var selectedCategory = Categories.FirstOrDefault(c => c.Id == CurrentExpense.CategoryId);
            if (selectedCategory != null)
            {
                CategoryPicker.SelectedItem = selectedCategory;
            }
        }

        // Обновляем BindingContext, чтобы XAML увидел изменения
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
        // Присваиваем ID выбранной категории нашему расходу
        if (CategoryPicker.SelectedItem is Category selectedCategory)
        {
            CurrentExpense.CategoryId = selectedCategory.Id;
        }

        if (string.IsNullOrWhiteSpace(CurrentExpense.Name) || CurrentExpense.Amount is null or <= 0)
        {
            await DisplayAlert("Ошибка", "Введите название и сумму больше нуля.", "ОК");
            return;
        }

        if (CurrentExpense.CategoryId == 0)
        {
            await DisplayAlert("Ошибка", "Пожалуйста, выберите категорию.", "ОК");
            return;
        }

        await App.ExpenseDatabase.SaveItemAsync(CurrentExpense);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (CurrentExpense == null) return;

        bool isConfirmed = await DisplayAlert("Подтверждение", "Вы уверены?", "Да", "Нет");

        if (isConfirmed)
        {
            await App.ExpenseDatabase.DeleteItemAsync(CurrentExpense);
            await Shell.Current.GoToAsync("..");
        }
    }
}