using System.Net.Http.Json;
using static AMASSKWeb.Pages.Auth.Login;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace AMASSKWeb.Repo
{
    public class AdminRepo : IAdminRepo
    {
        private readonly HttpClient httpClient;

        public AdminRepo(HttpClient _httpClient)
        {
            httpClient = _httpClient;
        }

        public async Task<ReadRepoResult> Login(string email, string password)
        {
            var loginUrl = "api/SSKAdminLogin?code=W359jEGwDqR_WYHlaWe9n-XV8xriTI8sRQ70GNQ7bkdhAzFu6kC1Dg==";

            var loginData = new LoginDTO
            {
                Email = email,
                Password = password
            };

            var response = await httpClient.PostAsJsonAsync(loginUrl, loginData);

            var rawResponse = await response.Content.ReadAsStringAsync();

            Console.WriteLine($"API RESPONSE: {rawResponse}");

            if (string.IsNullOrWhiteSpace(rawResponse))
            {
                return new ReadRepoResult
                {
                    IsSuccess = false,
                    ErrorMessage = "Server returned empty response"
                };
            }

            return JsonSerializer.Deserialize<ReadRepoResult>(
                rawResponse,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }


    public class ReadRepoResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public User? User { get; set; }
        public string? AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }

    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class LoginDTO
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}