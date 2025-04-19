using MauiApp1.Models;
using MauiApp1.Services;

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
            // �������� ������ ������� �� ���� ������
            var incomes = await App.IncomeDatabase.GetItemsAsync();
            IncomeCollection.ItemsSource = incomes;
        }

        private async void OnAddIncomeClicked(object sender, EventArgs e)
        {
            // ��������� �� �������� ���������� ������
            await Navigation.PushAsync(new AddIncomePage());
        }
    }
}
