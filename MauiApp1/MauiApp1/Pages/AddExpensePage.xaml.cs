using MauiApp1.Models;
using System;
using Microsoft.Maui.Controls;

namespace MauiApp1.Pages
{
    public partial class AddExpensePage : ContentPage
    {
        private Expense _expense;

        public AddExpensePage(Expense expense = null)
        {
            InitializeComponent();
            _expense = expense ?? new Expense();
            BindingContext = _expense;

            if (expense != null)
            {
                TitleEntry.Text = _expense.Title;
                AmountEntry.Text = _expense.Amount.ToString();
                DatePicker.Date = _expense.Date;
                CommentEditor.Text = _expense.Comment;
            }
            else
            {
                DatePicker.Date = DateTime.Now;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text) || string.IsNullOrWhiteSpace(AmountEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все поля.", "OK");
                return;
            }

            if (!decimal.TryParse(AmountEntry.Text, out decimal amount))
            {
                await DisplayAlert("Ошибка", "Сумма должна быть числом.", "OK");
                return;
            }

            _expense.Title = TitleEntry.Text;
            _expense.Amount = amount;
            _expense.Date = DatePicker.Date;
            _expense.Comment = CommentEditor.Text;

            await App.ExpenseDatabase.SaveExpenseAsync(_expense);
            await Navigation.PopAsync();
        }
    }
}
