using Expensetracker.Models;
using Microcharts;
using SkiaSharp;
using System.Globalization;

namespace Expensetracker.Views;

public partial class ChartsPage : ContentPage
{
    public ChartsPage()
    {
        InitializeComponent();
    }

    // Этот метод теперь вызывается только по событию, а не из OnAppearing
    private async void ChartTypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        // ЗАЩИТА ОТ СБОЯ: Если обработчик сработал до того, как страница
        // и ее элементы полностью загрузились, просто выходим из метода.
        if (ChartTypePicker == null)
        {
            return;
        }

        int selectedIndex = ChartTypePicker.SelectedIndex;

        if (selectedIndex == 0)
        {
            await CreateExpensesPieChartAsync();
        }
        else if (selectedIndex == 1)
        {
            await CreateIncomeVsExpenseBarChartAsync();
        }
    }

    private async Task CreateExpensesPieChartAsync()
    {
        var expenses = await App.ExpenseDatabase.GetItemsAsync();
        if (!expenses.Any())
        {
            chartView.Chart = null; // Очищаем график
            await DisplayAlert("Нет данных", "Нет расходов для построения диаграммы.", "OK");
            return;
        }

        var categories = await App.CategoryDatabase.GetCategoriesAsync();
        var categoryMap = categories.ToDictionary(c => c.Id, c => c.Name);

        var chartEntries = expenses
            .GroupBy(e => e.CategoryId)
            .Select(group => new ChartEntry((float)group.Sum(e => e.Amount ?? 0))
            {
                Label = categoryMap.GetValueOrDefault(group.Key, "Без категории"),
                ValueLabel = group.Sum(e => e.Amount ?? 0).ToString("F2"),
                Color = new SKColor((byte)new Random().Next(256), (byte)new Random().Next(256), (byte)new Random().Next(256))
            })
            .ToList();

        chartView.Chart = new DonutChart
        {
            Entries = chartEntries,
            LabelTextSize = 28f,
            LabelMode = LabelMode.LeftAndRight,
            IsAnimated = true
        };
    }

    private async Task CreateIncomeVsExpenseBarChartAsync()
    {
        var incomes = await App.IncomeDatabase.GetItemsAsync();
        var expenses = await App.ExpenseDatabase.GetItemsAsync();

        if (!incomes.Any() && !expenses.Any())
        {
            chartView.Chart = null; // Очищаем график
            await DisplayAlert("Нет данных", "Нет доходов и расходов для построения графика.", "OK");
            return;
        }

        var monthlyData = Enumerable.Range(0, 6)
            .Select(i => DateTime.Now.AddMonths(-i))
            .Select(date => new
            {
                Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(date.Month) + date.ToString(" yy"),
                Incomes = incomes.Where(inc => inc.Date.Year == date.Year && inc.Date.Month == date.Month).Sum(inc => inc.Amount ?? 0),
                Expenses = expenses.Where(exp => exp.Date.Year == date.Year && exp.Date.Month == date.Month).Sum(exp => exp.Amount ?? 0)
            })
            .Reverse()
            .ToList();

        var incomeEntries = monthlyData.Select(data => new ChartEntry((float)data.Incomes)
        {
            Label = data.Month,
            ValueLabel = data.Incomes.ToString("F0"),
            Color = SKColor.Parse("#2ECC71") // Зеленый
        }).ToList();

        var expenseEntries = monthlyData.Select(data => new ChartEntry((float)data.Expenses)
        {
            Label = data.Month,
            ValueLabel = data.Expenses.ToString("F0"),
            Color = SKColor.Parse("#E74C3C") // Красный
        }).ToList();

        chartView.Chart = new BarChart
        {
            Entries = expenseEntries.Concat(incomeEntries),
            LabelTextSize = 25f,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelOrientation = Orientation.Horizontal,
            IsAnimated = true
        };
    }
}