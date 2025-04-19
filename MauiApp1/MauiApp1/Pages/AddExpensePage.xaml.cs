using MauiApp1.Models;

namespace MauiApp1.Views
{
    public partial class AddExpensePage : ContentPage
    {
        private Expense _expense;

        public AddExpensePage(Expense expense = null)
        {
            InitializeComponent();
            _expense = expense;

            if (_expense != null)
            {
                NameEntry.Text = _expense.Name;
                AmountEntry.Text = _expense.Amount.ToString();
                DatePicker.Date = _expense.Date;
                DescriptionEditor.Text = _expense.Description;
                CommentEditor.Text = _expense.Comment;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_expense == null)
            {
                _expense = new Expense
                {
                    Name = NameEntry.Text,
                    Amount = Convert.ToDecimal(AmountEntry.Text),
                    Date = DatePicker.Date,
                    Description = DescriptionEditor.Text,
                    Comment = CommentEditor.Text
                };

                await App.ExpenseDatabase.SaveItemAsync(_expense);
            }
            else
            {
                _expense.Name = NameEntry.Text;
                _expense.Amount = Convert.ToDecimal(AmountEntry.Text);
                _expense.Date = DatePicker.Date;
                _expense.Description = DescriptionEditor.Text;
                _expense.Comment = CommentEditor.Text;

                await App.ExpenseDatabase.UpdateItemAsync(_expense);
            }

            await Navigation.PopAsync();
        }
    }
}
