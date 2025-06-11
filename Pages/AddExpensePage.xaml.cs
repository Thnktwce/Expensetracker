using Expensetracker.Models;

namespace Expensetracker.Views
{
    public partial class AddExpensePage : ContentPage
    {
        // Храним текущий расход для редактирования
        private readonly Expense? _existingExpense;

        // Конструктор принимает существующий расход (если мы редактируем) или null (если создаем новый)
        public AddExpensePage(Expense? expense = null)
        {
            InitializeComponent();

            _existingExpense = expense;

            // Если мы редактируем существующий расход, заполняем поля его данными
            if (_existingExpense != null)
            {
                NameEntry.Text = _existingExpense.Name;
                AmountEntry.Text = _existingExpense.Amount.ToString();
                DatePicker.Date = _existingExpense.Date;
                DescriptionEditor.Text = _existingExpense.Description;
                CommentEditor.Text = _existingExpense.Comment;
            }
            // Если создаем новый, просто ставим сегодняшнюю дату
            else
            {
                DatePicker.Date = DateTime.Now;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. Проверяем, что название не пустое
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите название расхода.", "ОК");
                return;
            }

            // 2. Безопасно проверяем и преобразуем сумму
            if (!decimal.TryParse(AmountEntry.Text, out decimal amount) || amount <= 0)
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите корректную сумму больше нуля.", "ОК");
                return;
            }

            // 3. Создаем или обновляем объект
            Expense expenseToSave;

            if (_existingExpense != null)
            {
                // Если мы редактировали, используем существующий объект
                expenseToSave = _existingExpense;
            }
            else
            {
                // Если создавали новый, создаем новый объект
                expenseToSave = new Expense();
            }

            // Заполняем объект данными из полей
            expenseToSave.Name = NameEntry.Text;
            expenseToSave.Amount = amount;
            expenseToSave.Date = DatePicker.Date;
            expenseToSave.Description = DescriptionEditor.Text;
            expenseToSave.Comment = CommentEditor.Text;

            // 4. Сохраняем в базу данных
            await App.ExpenseDatabase.SaveItemAsync(expenseToSave);

            // 5. Возвращаемся на предыдущую страницу
            await Shell.Current.GoToAsync("..");
        }
    }
}