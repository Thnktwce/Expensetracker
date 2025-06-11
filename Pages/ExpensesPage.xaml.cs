using Expensetracker.Models;
using Expensetracker.Views; // ���������, ��� ���� using ����

namespace Expensetracker.Views
{
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // ��������� ������ �������� ��� ������ �������� ��������
            expensesCollectionView.ItemsSource = await App.ExpenseDatabase.GetItemsAsync();
        }

        // ��� ����������� �����, ������� ���� ��� XAML
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            // ��������� �� �������� ���������� ������ �������
            await Shell.Current.GoToAsync(nameof(AddExpensePage));
        }
    }
}