using MauiApp1.Models;

namespace MauiApp1.Views
{
    public partial class AddIncomePage : ContentPage
    {
        private Income currentIncome;

        public AddIncomePage(Income income = null)
        {
            InitializeComponent();

            currentIncome = income;

            if (currentIncome != null)
            {
                NameEntry.Text = currentIncome.Name;
                AmountEntry.Text = currentIncome.Amount.ToString();
                DescriptionEditor.Text = currentIncome.Description;
                CommentEditor.Text = currentIncome.Comment;
                DatePicker.Date = currentIncome.Date;
            }
            else
            {
                DatePicker.Date = DateTime.Now;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(AmountEntry.Text, out decimal amount))
            {
                if (currentIncome == null)
                    currentIncome = new Income();

                currentIncome.Name = NameEntry.Text;
                currentIncome.Amount = amount;
                currentIncome.Description = DescriptionEditor.Text;
                currentIncome.Comment = CommentEditor.Text;
                currentIncome.Date = DatePicker.Date;

                await App.IncomeDatabase.SaveItemAsync(currentIncome);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Ошибка", "Введите корректную сумму", "ОК");
            }
        }
    }
}
