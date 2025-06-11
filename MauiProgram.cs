using Expensetracker.Views; // <-- 2. ДОБАВЛЯЕМ ЭТОТ USING ДЛЯ СТРАНИЦ
using Microcharts.Maui; // <-- 1. ДОБАВЛЯЕМ ЭТОТ USING
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Expensetracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseSkiaSharp() // <-- Теперь эта строка будет работать
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // 3. ЯВНО РЕГИСТРИРУЕМ ВСЕ НАШИ СТРАНИЦЫ
            // Это самый надежный способ "познакомить" приложение со страницами
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<ExpensesPage>();
            builder.Services.AddTransient<IncomesPage>();
            builder.Services.AddTransient<AddExpensePage>();
            builder.Services.AddTransient<AddIncomePage>();
            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<ChartsPage>(); // <-- Регистрируем и новую страницу

            return builder.Build();
        }
    }
}