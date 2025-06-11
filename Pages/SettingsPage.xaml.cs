using Expensetracker.Services;

namespace Expensetracker.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            CurrencyPicker.SelectedItem = CurrencyService.GetCurrency();
        }

        private void OnCurrencyChanged(object sender, EventArgs e)
        {
            if (CurrencyPicker.SelectedItem is string selectedCurrency)
            {
                CurrencyService.SetCurrency(selectedCurrency);
            }
        }
    }
}