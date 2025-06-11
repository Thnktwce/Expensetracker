using Expensetracker.Models;

namespace Expensetracker.Views
{
    public partial class AddExpensePage : ContentPage
    {
        // ������ ������� ������ ��� ��������������
        private readonly Expense? _existingExpense;

        // ����������� ��������� ������������ ������ (���� �� �����������) ��� null (���� ������� �����)
        public AddExpensePage(Expense? expense = null)
        {
            InitializeComponent();

            _existingExpense = expense;

            // ���� �� ����������� ������������ ������, ��������� ���� ��� �������
            if (_existingExpense != null)
            {
                NameEntry.Text = _existingExpense.Name;
                AmountEntry.Text = _existingExpense.Amount.ToString();
                DatePicker.Date = _existingExpense.Date;
                DescriptionEditor.Text = _existingExpense.Description;
                CommentEditor.Text = _existingExpense.Comment;
            }
            // ���� ������� �����, ������ ������ ����������� ����
            else
            {
                DatePicker.Date = DateTime.Now;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. ���������, ��� �������� �� ������
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("������", "����������, ������� �������� �������.", "��");
                return;
            }

            // 2. ��������� ��������� � ����������� �����
            if (!decimal.TryParse(AmountEntry.Text, out decimal amount) || amount <= 0)
            {
                await DisplayAlert("������", "����������, ������� ���������� ����� ������ ����.", "��");
                return;
            }

            // 3. ������� ��� ��������� ������
            Expense expenseToSave;

            if (_existingExpense != null)
            {
                // ���� �� �������������, ���������� ������������ ������
                expenseToSave = _existingExpense;
            }
            else
            {
                // ���� ��������� �����, ������� ����� ������
                expenseToSave = new Expense();
            }

            // ��������� ������ ������� �� �����
            expenseToSave.Name = NameEntry.Text;
            expenseToSave.Amount = amount;
            expenseToSave.Date = DatePicker.Date;
            expenseToSave.Description = DescriptionEditor.Text;
            expenseToSave.Comment = CommentEditor.Text;

            // 4. ��������� � ���� ������
            await App.ExpenseDatabase.SaveItemAsync(expenseToSave);

            // 5. ������������ �� ���������� ��������
            await Shell.Current.GoToAsync("..");
        }
    }
}