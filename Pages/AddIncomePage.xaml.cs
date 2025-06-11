using Expensetracker.Models;

namespace Expensetracker.Views
{
    public partial class AddIncomePage : ContentPage
    {
        // ������ ������������ ����� ��� ��������������
        private readonly Income? _existingIncome;

        public AddIncomePage(Income? income = null)
        {
            InitializeComponent();

            _existingIncome = income;

            // ���� �� �����������, ��������� ���� �� ������
            if (_existingIncome != null)
            {
                NameEntry.Text = _existingIncome.Name;
                AmountEntry.Text = _existingIncome.Amount.ToString();
                DatePicker.Date = _existingIncome.Date;
                DescriptionEditor.Text = _existingIncome.Description;
                CommentEditor.Text = _existingIncome.Comment;
            }
            // ���� ������� �����, ������ ����������� ����
            else
            {
                DatePicker.Date = DateTime.Now;
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. ��������� ��������
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("������", "����������, ������� �������� ������.", "��");
                return;
            }

            // 2. ��������� ��������� �����
            if (!decimal.TryParse(AmountEntry.Text, out decimal amount) || amount <= 0)
            {
                await DisplayAlert("������", "����������, ������� ���������� ����� ������ ����.", "��");
                return;
            }

            // 3. ������� ����� ������ ��� ���������� ������������ ��� ����������
            Income incomeToSave = _existingIncome ?? new Income();

            // 4. ��������� ������ ������� � ������
            incomeToSave.Name = NameEntry.Text;
            incomeToSave.Amount = amount;
            incomeToSave.Date = DatePicker.Date;
            incomeToSave.Description = DescriptionEditor.Text;
            incomeToSave.Comment = CommentEditor.Text;

            // 5. ��������� � ���� ������
            await App.IncomeDatabase.SaveItemAsync(incomeToSave);

            // 6. ������������ �����
            await Shell.Current.GoToAsync("..");
        }
    }
}