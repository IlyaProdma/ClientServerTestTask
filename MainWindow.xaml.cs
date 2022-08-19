using DataAccess;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace ClientServerTestTask
{
    /// <summary>
    /// Класс основного окна приложения
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Клиент для работы с API
        /// </summary>
        private readonly ApiClient _client;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MainWindow()
        {
            _client = new ApiClient();
            InitializeComponent();
            ClearTextBoxes();
        }

        /// <summary>
        /// Конструктор для вызова после окна авторизации с передаваемым клиентом
        /// </summary>
        /// <param name="client">передаваемый клиент для работы с API</param>
        public MainWindow(ApiClient client)
        {
            _client = new ApiClient(client);
            InitializeComponent();
            ClearTextBoxes();
            DataTable.Loaded += table_loaded;
        }

        /// <summary>
        /// Обработчик события загрузки и рендера DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void table_loaded(object sender, RoutedEventArgs e)
        {
            await LoadProducts();
        }

        /// <summary>
        /// Установка источника данных для DataGrid через клиент
        /// </summary>
        /// <returns></returns>
        private async Task LoadProducts()
        {
            var data = await _client.GetProductsAsync();
            if (data != null)
            {
                DataTable.ItemsSource = data;
            }
        }


        /// <summary>
        /// Обработчик нажатия на кнопку добавления нового продукта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product? product = Product.CheckInputProperties(txtVendor.Text, txtName.Text,
                                                               txtPrice.Text, txtDescription.Text);
                if (product != null)
                {
                    bool op = await _client.AddProductAsync(product);
                    if (op)
                    {
                        ClearTextBoxes();
                        await LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Продукт с таким артикулом уже есть");
                    }
                }
                else
                {
                    MessageBox.Show("Проверьте ввод!");
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка.");
            }
        }

        /// <summary>
        /// Очистка полей для ввода
        /// </summary>
        private void ClearTextBoxes()
        {
            txtVendor.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPrice.Text = String.Empty;
            txtDescription.Text = String.Empty;
        }
    }
}
