using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace DataAccess
{
    /// <summary>
    /// Класс клиента, работающего с API
    /// </summary>
    public class ApiClient
    {
        /// <summary>
        /// HttpClient для работы с запросами
        /// </summary>
        private HttpClient _client = new HttpClient();

        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string? _username;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        private string? _password;

        /// <summary>
        /// Свойство для получения логина
        /// </summary>
        public string? Username { get { return _username; }}

        /// <summary>
        /// Свойство для получения пароля
        /// </summary>
        public string? Password { get { return _password; }}

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ApiClient()
        {
            _client.BaseAddress = new Uri("https://localhost:7048/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Конструктор копирования
        /// </summary>
        /// <param name="other"></param>
        public ApiClient(ApiClient other)
        {
            this._client = other._client;
            this._username = other._username;
            this._password = other._password;
        }
        
        /// <summary>
        /// Метод для проверки существования пользователя с данным логином
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> CheckUserExistsAsync(string username) =>
            (await _client.GetAsync($"api/User/{username}")).IsSuccessStatusCode;

        /// <summary>
        /// Метод для проверки пароля по данному логину
        /// </summary>
        /// <param name="username">логин пользователя</param>
        /// <param name="password">пароль пользователя</param>
        /// <returns></returns>
        public async Task<bool> CheckPasswordCorrectAsync(string username, string password)
        {
            string path = $"/auth";
            _client.DefaultRequestHeaders.Add("Authorization", $"{username}_{User.ComputeMD5(password)}");
            HttpResponseMessage response = await _client.GetAsync(path);
            _client.DefaultRequestHeaders.Remove("Authorization");
            if (response.IsSuccessStatusCode)
            {
                _username = username;
                _password = password;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Метод для получения списка продуктов из базы
        /// </summary>
        /// <returns>Список продуктов, если запрос был успешный; null в обратном случае</returns>
        public async Task<List<Product>?> GetProductsAsync()
        {
            string path = $"/api/Data/products";
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
                return JsonSerializer
                    .Deserialize<List<Product>>(
                    await response.Content.ReadAsStringAsync())!;
            else
                return null;
        }

        /// <summary>
        /// Метод для добавления нового продукта в базу
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<bool> AddProductAsync(Product product)
        {
            string path = $"/api/Data/products/new";
            _client.DefaultRequestHeaders.Add("Authorization", $"{_username}_{User.ComputeMD5(_password!)}");
            HttpResponseMessage response = await _client.PostAsJsonAsync(path, product);
            _client.DefaultRequestHeaders.Remove("Authorization");
            return response.IsSuccessStatusCode;
        }
    }
}
