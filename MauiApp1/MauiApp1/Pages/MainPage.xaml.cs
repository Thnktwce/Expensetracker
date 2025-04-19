using MauiApp1.Models;
using System.Linq;

namespace MauiApp1.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent(); // ������������� ��������� ���������� �� XAML
        }

        // �����, ������� ���������� ��� ����������� ��������
        protected override async void OnAppearing()
        {
            base.OnAppearing(); // �������� ������� ����������

            // �������� ��� ������� �� ���� ������
            var expenses = await App.ExpenseDatabase.GetItemsAsync();

            // �������� ��� ������ �� ���� ������
            var incomes = await App.IncomeDatabase.GetItemsAsync();

            // ��������� ��� ������� � ������
            decimal totalExpenses = expenses.Sum(expense => expense.Amount);
            decimal totalIncomes = incomes.Sum(income => income.Amount);

            // ��������� ����� ������� �� ������
            TotalExpensesLabel.Text = $"����� ����� ��������: {totalExpenses:C}";
            TotalIncomesLabel.Text = $"����� ����� �������: {totalIncomes:C}";
        }

        // ����� ��� �������� �� ������� "�������"
        private async void OnViewExpensesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//�������");
        }

        // ����� ��� �������� �� ������� "������"
        private async void OnViewIncomesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//������");
        }
    }
}
