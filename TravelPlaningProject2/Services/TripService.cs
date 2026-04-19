using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TravelPlaningProject2.Models;
using TravelPlaningProject2.Helpers;

namespace TravelPlaningProject2.Services;

public class TripService(HttpClient httpClient, JsonSerializerOptions options)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly JsonSerializerOptions _options = options;

    // Получение списка поездок пользователя
    public async Task<(bool Success, List<Trip>? Trips, string? Error)> GetTripsAsync()
    {
        try
        {
            var token = AuthHelper.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync("trips");
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var trips = JsonSerializer.Deserialize<List<Trip>>(responseJson, _options);
                return (true, trips, null);
            }

            return (false, null, "Не удалось загрузить поездки");
        }
        catch (Exception ex)
        {
            return (false, null, $"Ошибка: {ex.Message}");
        }
    }

    // Получение конкретной поездки по ID
    public async Task<(bool Success, Trip? Trip, string? Error)> GetTripByIdAsync(int id)
    {
        try
        {
            var token = AuthHelper.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"trips/{id}");
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var trip = JsonSerializer.Deserialize<Trip>(responseJson, _options);
                return (true, trip, null);
            }

            return (false, null, "Поездка не найдена");
        }
        catch (Exception ex)
        {
            return (false, null, $"Ошибка: {ex.Message}");
        }
    }

    // Создание новой поездки
    public async Task<(bool Success, Trip? Trip, string? Error)> CreateTripAsync(Trip newTrip)
    {
        try
        {
            var token = AuthHelper.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var jsonBody = JsonSerializer.Serialize(newTrip, _options);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("trips", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var trip = JsonSerializer.Deserialize<Trip>(responseJson, _options);
                return (true, trip, null);
            }

            return (false, null, "Ошибка создания поездки");
        }
        catch (Exception ex)
        {
            return (false, null, $"Ошибка: {ex.Message}");
        }
    }

    // Удаление поездки
    public async Task<(bool Success, string? Error)> DeleteTripAsync(int id)
    {
        try
        {
            var token = AuthHelper.GetToken();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.DeleteAsync($"trips/{id}");
            return response.IsSuccessStatusCode ? (true, null) : (false, "Не удалось удалить");
        }
        catch (Exception ex)
        {
            return (false, $"Ошибка: {ex.Message}");
        }
    }
}