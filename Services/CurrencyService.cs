using Microsoft.Maui.Storage;

public class CurrencyService
{
    // Устанавливаем валюту по умолчанию как "USD"
    public static string SelectedCurrency { get; set; } = "USD";

    // Метод для установки валюты
    public static void SetCurrency(string currency)
    {
        SelectedCurrency = currency;
        // Сохраняем в Preferences для постоянства
        Preferences.Set("SelectedCurrency", currency);
    }

    // Метод для получения текущей валюты
    public static string GetCurrency()
    {
        return Preferences.Get("SelectedCurrency", "USD"); // Возвращаем валюту из настроек
    }
}
