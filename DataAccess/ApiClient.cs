using System.Net.Http.Headers;
using System.Text.Json;

namespace DataAccess
{
    public class ApiClient
    {
        private HttpClient _client = new HttpClient();
        private string? _username;
        private string? _password;

        public string? Username { get { return _username; }}
        public string? Password { get { return _password; }}

        public ApiClient()
        {
            _client.BaseAddress = new Uri("https://localhost:7048/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ApiClient(ApiClient other)
        {
            this._client = other._client;
            this._username = other._username;
            this._password = other._password;
        }
        
        public async Task<bool> CheckUserExistsAsync(string username) =>
            (await _client.GetAsync($"api/User/{username}")).IsSuccessStatusCode;

        public async Task<bool> CheckPasswordCorrectAsync(string username, string password)
        {
            string path = $"/auth";
            _client.DefaultRequestHeaders.Add("Authorization", $"{username}_{password}");
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

        public async Task<List<Product>?> GetProductsAsync()
        {
            if (_username != null && _password != null)
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
            else
            {
                return null;
            }
        }
    }
}
