namespace Expensetracker.Services
{
    public static class CurrencyService
    {
        private static string _selectedCurrency = "USD";

        public static void Init()
        {
            _selectedCurrency = Preferences.Get("SelectedCurrency", "USD");
        }

        public static void SetCurrency(string currency)
        {
            if (!string.IsNullOrEmpty(currency))
            {
                _selectedCurrency = currency;
                Preferences.Set("SelectedCurrency", currency);
            }
        }

        public static string GetCurrency()
        {
            return _selectedCurrency;
        }
    }
}