using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TravelPlaningProject2.Models.Responses;

namespace TravelPlaningProject2.Services;

public class AuthService(HttpClient httpClient)
{
    private const string ApiBaseUrl = "https://your-api-domain.com/api/v1/";

    public async Task<(bool Success, string? Token, string? Error)> LoginAsync(string login, string password)
    {
        try
        {
            var request = new
            {
                login = login,
                password = password
            };

            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"{ApiBaseUrl}auth", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return authResponse != null
                    ? (true, authResponse.Token, null)
                    : (false, null, "Ошибка парсинга ответа");
            }

            // Обработка ошибок
            var errorData = JsonSerializer.Deserialize<ErrorResponse>(responseContent);
            return (false, null, errorData?.Message ?? "Ошибка сервера");
        }
        catch (HttpRequestException ex)
        {
            return (false, null, $"Ошибка сети: {ex.Message}");
        }
        catch (Exception ex)
        {
            return (false, null, $"Неизвестная ошибка: {ex.Message}");
        }
    }

    public async Task<(bool Success, string? Token, string? Error)> RegisterAsync(
        string name, string login, string password, string? surname = null)
    {
        // Аналогичная реализация для регистрации
        // ...
        return (false, null, "Not implemented");
    }
}

// Вспомогательная модель для ошибок
public class ErrorResponse
{
    public string? Message { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
}