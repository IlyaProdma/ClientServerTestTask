using System;
using System.Threading.Tasks;
using System.Windows;
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
                            MessageBox.Show("Ok");
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInputIsNotEmpty())
            {
                try
                {
                    if (await CheckUserExists() == true)
                    {
                        MessageBox.Show("Этот логин уже занят");
                    }
                    else
                    {
                        if (await AddNewUser() == true)
                        {
                            MessageBox.Show("Успешная регистрация");
                        }
                        else
                        {
                            MessageBox.Show("Не удалось зарегистрировать");
                        }
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async Task<bool> CheckUserExists() =>
            await _client.CheckUserExistsAsync(textUsername.Text);

        private async Task<bool> CheckPasswordCorrect() =>
            await _client.CheckPasswordCorrect(textUsername.Text, textPassword.Password);

        private async Task<bool> AddNewUser() =>
            await _client.AddNewUser(textUsername.Text, textPassword.Password);
    }
}
