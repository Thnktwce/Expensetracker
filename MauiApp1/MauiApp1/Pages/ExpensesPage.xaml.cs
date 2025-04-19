using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Controls;

namespace MauiApp1.Views
{
    public partial class ExpensesPage : ContentPage
    {
        public ObservableCollection<Expense> Expenses { get; set; }

        public ExpensesPage()
        {
            InitializeComponent();
            LoadExpenses(); // �������� �������� ��� �������������
        }

        // �������� ���� �������� �� ���� ������
        private async void LoadExpenses()
        {
            var expenses = await App.ExpenseDatabase.GetItemsAsync();
            Expenses = new ObservableCollection<Expense>(expenses);
            ExpensesCollectionView.ItemsSource = Expenses; // ��������� �������� ������ ��� CollectionView
        }

        // ���������� ��� ���������� ������ �������
        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddExpensePage());
        }

        // ���������� ��� �������������� �������
        private async void OnEditExpenseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var expense = button?.BindingContext as Expense;

            if (expense != null)
            {
                await Navigation.PushAsync(new AddExpensePage(expense));
            }
        }

        // ���������� ��� �������� �������
        private async void OnDeleteExpenseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var expense = button?.BindingContext as Expense;

            if (expense != null)
            {
                var result = await DisplayAlert("�����������", "�� �������, ��� ������ ������� ���� ������?", "��", "���");
                if (result)
                {
                    await App.ExpenseDatabase.DeleteItemAsync(expense.Id); // �������� ������� �� ���� ������
                    Expenses.Remove(expense); // ������� ������ �� ObservableCollection
                }
            }
        }

        // ���������� ����� ����������� � ���������� ������ �������
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadExpenses(); // ������������� ������ ��� ����������� �� ��������
        }
    }
}
