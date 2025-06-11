using Expensetracker.Models;

namespace Expensetracker.Views;

public partial class IncomesPage : ContentPage
{
    public IncomesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        incomesCollectionView.ItemsSource = await App.IncomeDatabase.GetItemsAsync();
    }

    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddIncomePage));
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Income selectedIncome)
        {
            await Shell.Current.GoToAsync(nameof(AddIncomePage), true, new Dictionary<string, object>
            {
                { "Income", selectedIncome }
            });
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}