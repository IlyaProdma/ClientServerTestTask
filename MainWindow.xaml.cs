using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserApplication;

namespace ClientServerTestTask
{
    public partial class MainWindow : Window
    {
        private readonly ApiClient _client;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(ApiClient client)
        {
            _client = new ApiClient(client);
            InitializeComponent();
            DataTable.Loaded += table_loaded;
        }

        private async void table_loaded(object sender, RoutedEventArgs e)
        {
            var data = await _client.GetProductsAsync();
            if (data != null)
            {
                DataTable.ItemsSource = data;
            }
        }


    }
}
