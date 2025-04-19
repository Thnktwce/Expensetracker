using MauiApp1.Models;

namespace MauiApp1.Views
{
    public partial class AddIncomePage : ContentPage
    {
        private Income _income;

        public AddIncomePage(Income income = null)
        {
            InitializeComponent();
            _income = income;

            if (_income != null)
            {
                TitleEntry.Text = _income.Title;
                AmountEntry.Text = _income.Amount.ToString();
                CommentEditor.Text = _income.Comment;
                DatePicker.Date = _income.Date;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (_income == null)
                _income = new Income();

            _income.Title = TitleEntry.Text;
            _income.Amount = decimal.Parse(AmountEntry.Text);
            _income.Comment = CommentEditor.Text;
            _income.Date = DatePicker.Date;

            await App.IncomeDatabase.SaveItemAsync(_income);
            await Navigation.PopAsync();
        }
    }
}
