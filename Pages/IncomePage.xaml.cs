using Expensetracker.Models; // <-- ��� ����������� using!

namespace Expensetracker.Views;

// ��� ������������ ����� ������� ����� IncomesPage
public partial class IncomesPage : ContentPage
{
    public IncomesPage()
    {
        InitializeComponent();
    }

    // ���� ����� ���������� ������ ���, ����� �������� ���������� �� ������
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // ��������� ��� ������ �� ���� ������ � ���������� �� � ������
        incomesCollectionView.ItemsSource = await App.IncomeDatabase.GetItemsAsync();
    }

    // ���� ����� ����� ������ ��� ������� �� ������ "��������"
    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        // ��������� �� �������� ���������� ������ ������
        await Shell.Current.GoToAsync(nameof(AddIncomePage));
    }
}