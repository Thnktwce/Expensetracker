using Expensetracker.Services;

namespace Expensetracker
{
    public partial class App : Application
    {
        // Свойство для доступа к базе расходов
        private static ExpenseDatabase? _expenseDatabase;
        public static ExpenseDatabase ExpenseDatabase => _expenseDatabase ??= new ExpenseDatabase();

        // Свойство для доступа к базе доходов
        private static IncomeDatabase? _incomeDatabase;
        public static IncomeDatabase IncomeDatabase => _incomeDatabase ??= new IncomeDatabase();

        // Свойство для доступа к базе категорий
        private static CategoryDatabase? _categoryDatabase;
        public static CategoryDatabase CategoryDatabase => _categoryDatabase ??= new CategoryDatabase();

        public App()
        {
            InitializeComponent();

            // Инициализируем сервис для работы с валютой
            CurrencyService.Init();

            // Устанавливаем стартовую страницу
            MainPage = new AppShell();
        }
    }
}