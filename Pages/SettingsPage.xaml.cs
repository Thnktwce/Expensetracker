using Expensetracker.Services; // <-- Правильный using

namespace Expensetracker.Views // <-- Правильное пространство имен
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
            // Сразу сохраняем при выборе, кнопка не нужна
            if (CurrencyPicker.SelectedItem is string selectedCurrency)
            {
                CurrencyService.SetCurrency(selectedCurrency);
            }
        }
    }
}