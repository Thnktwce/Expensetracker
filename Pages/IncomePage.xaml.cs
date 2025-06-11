using Expensetracker.Models; // <-- Вот необходимый using!

namespace Expensetracker.Views;

// Для единообразия лучше назвать класс IncomesPage
public partial class IncomesPage : ContentPage
{
    public IncomesPage()
    {
        InitializeComponent();
    }

    // Этот метод вызывается каждый раз, когда страница появляется на экране
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Загружаем все доходы из базы данных и отображаем их в списке
        incomesCollectionView.ItemsSource = await App.IncomeDatabase.GetItemsAsync();
    }

    // Этот метод будет вызван при нажатии на кнопку "Добавить"
    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        // Переходим на страницу добавления нового дохода
        await Shell.Current.GoToAsync(nameof(AddIncomePage));
    }
}