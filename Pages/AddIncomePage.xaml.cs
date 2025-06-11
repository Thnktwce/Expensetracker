using Expensetracker.Models;

namespace Expensetracker.Views
{
    public partial class AddIncomePage : ContentPage
    {
        // Храним существующий доход для редактирования
        private readonly Income? _existingIncome;

        public AddIncomePage(Income? income = null)
        {
            InitializeComponent();

            _existingIncome = income;

            // Если мы редактируем, заполняем поля на экране
            if (_existingIncome != null)
            {
                NameEntry.Text = _existingIncome.Name;
                AmountEntry.Text = _existingIncome.Amount.ToString();
                DatePicker.Date = _existingIncome.Date;
                DescriptionEditor.Text = _existingIncome.Description;
                CommentEditor.Text = _existingIncome.Comment;
            }
            // Если создаем новый, ставим сегодняшнюю дату
            else
            {
                DatePicker.Date = DateTime.Now;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. Проверяем название
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите название дохода.", "ОК");
                return;
            }

            // 2. Безопасно проверяем сумму
            if (!decimal.TryParse(AmountEntry.Text, out decimal amount) || amount <= 0)
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите корректную сумму больше нуля.", "ОК");
                return;
            }

            // 3. Создаем новый объект или используем существующий для обновления
            Income incomeToSave = _existingIncome ?? new Income();

            // 4. Заполняем объект данными с экрана
            incomeToSave.Name = NameEntry.Text;
            incomeToSave.Amount = amount;
            incomeToSave.Date = DatePicker.Date;
            incomeToSave.Description = DescriptionEditor.Text;
            incomeToSave.Comment = CommentEditor.Text;

            // 5. Сохраняем в базу данных
            await App.IncomeDatabase.SaveItemAsync(incomeToSave);

            // 6. Возвращаемся назад
            await Shell.Current.GoToAsync("..");
        }
    }
}