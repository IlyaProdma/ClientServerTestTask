using System;
using System.Threading.Tasks;
using System.Windows;
using ClientServerTestTask;
using DataAccess;

namespace UserApplication
{
    public partial class LoginWindow : Window
    {
        private ApiClient _client;

        public LoginWindow()
        {
            InitializeComponent();
            _client = new ApiClient();

        }

        private bool CheckInputIsNotEmpty()
        {
            if (String.IsNullOrEmpty(textUsername.Text) ||
                String.IsNullOrEmpty(textPassword.Password))
            {
                MessageBox.Show("Проверьте ввод!");
                return false;
            }
            return true;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputIsNotEmpty())
            {
                try
                {
                    if (await CheckUserExists() == false)
                    {
                        MessageBox.Show("Такого пользователя нет");
                    }
                    else
                    {
                        if (await CheckPasswordCorrect() == false)
                        {
                            MessageBox.Show("Неверный логин или пароль!");
                        }
                        else
                        {
                            MainWindow window = new MainWindow(_client);
                            window.Show();
                            this.Close();
                            return;
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message + "\n" + ex.StackTrace);
                }
            }
        }

        private async Task<bool> CheckUserExists() =>
            await _client.CheckUserExistsAsync(textUsername.Text);

        private async Task<bool> CheckPasswordCorrect() =>
            await _client.CheckPasswordCorrectAsync(textUsername.Text, textPassword.Password);
    }
}
