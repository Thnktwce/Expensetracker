using MauiApp1.Models;

namespace MauiApp1.Views
{
    public partial class IncomePage : ContentPage
    {
        public IncomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadIncomes();
        }

        private async Task LoadIncomes()
        {
            var incomes = await App.IncomeDatabase.GetItemsAsync();
            IncomeCollectionView.ItemsSource = incomes;
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddIncomePage());
        }

        private async void OnEditIncomeClicked(object sender, EventArgs e)
        {
            var income = (sender as Button)?.BindingContext as Income;
            if (income != null)
            {
                await Navigation.PushAsync(new AddIncomePage(income));
            }
        }

        private async void OnDeleteIncomeClicked(object sender, EventArgs e)
        {
            var income = (sender as Button)?.BindingContext as Income;
            if (income != null)
            {
                await App.IncomeDatabase.DeleteItemAsync(income);
                await LoadIncomes();
            }
        }
    }
}
