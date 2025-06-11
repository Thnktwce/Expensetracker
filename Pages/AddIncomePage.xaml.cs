using Expensetracker.Models;

namespace Expensetracker.Views;

[QueryProperty(nameof(Income), "Income")]
public partial class AddIncomePage : ContentPage
{
    public Income Income { get; set; }

    public AddIncomePage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        Income ??= new Income();
        BindingContext = Income;

        DeleteButton.IsVisible = Income.Id != 0;

        if (Income.Id == 0)
        {
            Income.Date = DateTime.Now;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var incomeToSave = BindingContext as Income;
        if (incomeToSave == null) return;

        // ��������� ��������: Amount �� ������ ���� null � ������ ���� ������ ����
        if (string.IsNullOrWhiteSpace(incomeToSave.Name) || !incomeToSave.Amount.HasValue || incomeToSave.Amount.Value <= 0)
        {
            await DisplayAlert("������", "����������, ������� �������� � ����� ������ ����.", "��");
            return;
        }

        await App.IncomeDatabase.SaveItemAsync(incomeToSave);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var incomeToDelete = BindingContext as Income;
        if (incomeToDelete == null) return;

        bool isConfirmed = await DisplayAlert("�������������", "�� �������, ��� ������ ������� ��� ������?", "��", "���");

        if (isConfirmed)
        {
            await App.IncomeDatabase.DeleteItemAsync(incomeToDelete);
            await Shell.Current.GoToAsync("..");
        }
    }
}