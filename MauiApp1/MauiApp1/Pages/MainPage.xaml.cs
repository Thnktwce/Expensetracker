using MauiApp1.Models;

namespace MauiApp1.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ExpenseListView.ItemsSource = await App.ExpenseDatabase.GetExpensesAsync();
            TotalExpenseLabel.Text = $"—ÛÏÏ‡: {(await App.ExpenseDatabase.GetTotalAmountAsync()):C}";
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpensePage());
        }
    }
}
