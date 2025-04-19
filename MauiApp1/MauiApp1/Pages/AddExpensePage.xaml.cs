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
                TitleEntry.Text = _expense.Title;
                AmountEntry.Text = _expense.Amount.ToString();
                CommentEditor.Text = _expense.Comment;
                DatePicker.Date = _expense.Date;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_expense == null)
                _expense = new Expense();

            _expense.Title = TitleEntry.Text;
            _expense.Amount = decimal.Parse(AmountEntry.Text);
            _expense.Comment = CommentEditor.Text;
            _expense.Date = DatePicker.Date;

            await App.ExpenseDatabase.SaveItemAsync(_expense);
            await Navigation.PopAsync();
        }
    }
}