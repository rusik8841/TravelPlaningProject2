using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TravelPlaningProject2.Models;
using TravelPlaningProject2.Helpers;

namespace TravelPlaningProject2.Services;

public class ProfileService(HttpClient httpClient, JsonSerializerOptions options)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly JsonSerializerOptions _options = options;

    // Получение данных профиля
    public async Task<(bool Success, User? User, string? Error)> GetProfileAsync()
    {
        try
        {
            var token = AuthHelper.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("profile");
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var user = JsonSerializer.Deserialize<User>(responseJson, _options);
                return (true, user, null);
            }

            return (false, null, "Не удалось загрузить профиль");
        }
        catch (Exception ex)
        {
            return (false, null, $"Ошибка: {ex.Message}");
        }
    }

    // Обновление профиля
    public async Task<(bool Success, User? User, string? Error)> UpdateProfileAsync(User updatedUser)
    {
        try
        {
            var token = AuthHelper.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Отправляем только изменяемые поля, чтобы не перезаписать лишнего
            var payload = new
            {
                name = updatedUser.Name,
                surname = updatedUser.Surname,
                currency = updatedUser.Currency,
                country = updatedUser.Country
            };

            var jsonBody = JsonSerializer.Serialize(payload, _options);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("profile", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var user = JsonSerializer.Deserialize<User>(responseJson, _options);
                return (true, user, null);
            }

            return (false, null, "Ошибка обновления профиля");
        }
        catch (Exception ex)
        {
            return (false, null, $"Ошибка: {ex.Message}");
        }
    }
}