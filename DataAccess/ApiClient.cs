using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Http.Json;

namespace DataAccess
{
    public class ApiClient
    {
        private HttpClient _client = new HttpClient();

        public ApiClient()
        {
            _client.BaseAddress = new Uri("https://localhost:7048/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> CheckUserExistsAsync(string username) =>
            (await _client.GetAsync($"api/User/{username}")).IsSuccessStatusCode;

        public async Task<bool> CheckPasswordCorrect(string username, string password)
        {
            User user;
            string path = $"api/User/{username}";
            HttpResponseMessage response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = JsonSerializer.Deserialize<User>(await response.Content.ReadAsStringAsync())!;
                return (user!.login_.Equals(username) && user!.password_.Equals(password));
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddNewUser(string username, string password)
        {
            string path = $"api/User";
            HttpResponseMessage response =
                await _client.PostAsJsonAsync(path, new User(username, password));
            return response.IsSuccessStatusCode;
        }
    }
}
