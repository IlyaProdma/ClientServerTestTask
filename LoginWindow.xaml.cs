using System;
using System.Threading.Tasks;
using System.Windows;
using ClientServerTestTask;
using DataAccess;

namespace UserApplication
{
    /// <summary>
    /// Класс окна авторизации
    /// </summary>
    public partial class LoginWindow : Window
    {
        /// <summary>
        /// Клиент для работы с API
        /// </summary>
        private ApiClient _client;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LoginWindow()
        {
            InitializeComponent();
            _client = new ApiClient();

        }

        /// <summary>
        /// Проверка ввода данных на пустоту
        /// </summary>
        /// <returns></returns>
        private bool CheckInputIsNotEmpty()
        {
            if (String.IsNullOrWhiteSpace(textUsername.Text) ||
                String.IsNullOrWhiteSpace(textPassword.Password))
            {
                MessageBox.Show("Проверьте ввод!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку входа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                } catch
                {
                    MessageBox.Show("Произошла ошибка.");
                }
            }
        }

        /// <summary>
        /// Вызов проверки существования пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckUserExists() =>
            await _client.CheckUserExistsAsync(textUsername.Text);

        /// <summary>
        /// Вызов проверки данных пользователя
        /// </summary>
        /// <returns></returns>
        private async Task<bool> CheckPasswordCorrect() =>
            await _client.CheckPasswordCorrectAsync(textUsername.Text, textPassword.Password);
    }
}
