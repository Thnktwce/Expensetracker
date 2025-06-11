using Microsoft.Maui.Storage;

// Добавлено пространство имен
namespace Expensetracker.Services
{
    public class CurrencyService
    {
        public static string SelectedCurrency { get; set; } = "USD";

        public static void SetCurrency(string currency)
        {
            SelectedCurrency = currency;
            Preferences.Set("SelectedCurrency", currency);
        }

        public static string GetCurrency()
        {
            return Preferences.Get("SelectedCurrency", "USD");
        }
    }
}