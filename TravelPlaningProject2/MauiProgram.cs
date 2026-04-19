using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using TravelPlaningProject2.Services;
using TravelPlaningProject2.ViewModels.Auth;
using TravelPlaningProject2.ViewModels.Trips;
using TravelPlaningProject2.ViewModels.Profile;
using TravelPlaningProject2.Pages.Auth;
using TravelPlaningProject2.Pages.Trips;
using TravelPlaningProject2.Pages.Profile;
using TravelPlaningProject2.Helpers;

namespace TravelPlaningProject2;

public static class MauiProgram
{
    // 🔑 Замените на реальный адрес вашего Laravel API
    public const string ApiBaseUrl = "https://your-api-domain.com/api/v1/";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // 📱 Базовая конфигурация MAUI
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 🌐 Настройка HttpClient и сервисов
        // AuthService
        builder.Services.AddHttpClient<AuthService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "TravelPlanner/1.0");
        });

        // TripService
        builder.Services.AddHttpClient<TripService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "TravelPlanner/1.0");
        });

        // ProfileService
        builder.Services.AddHttpClient<ProfileService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "TravelPlanner/1.0");
        });

        // 🔥 Регистрация ViewModel (Transient = новый экземпляр при навигации)
        // Auth
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();

        // Trips
        builder.Services.AddTransient<TripsViewModel>();
        builder.Services.AddTransient<TripDetailsViewModel>();

        // Profile
        builder.Services.AddTransient<ProfileViewModel>();

        // 🖼️ Регистрация Страниц (Views) — ОБЯЗАТЕЛЬНО для DI
        // Auth
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();

        // Trips
        builder.Services.AddTransient<TripsPage>();
        builder.Services.AddTransient<TripDetailsPage>();

        // Profile
        builder.Services.AddTransient<ProfilePage>();

        // ⚙️ Настройки JSON сериализации для Laravel API
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(),
                new DateConverter(),              // Для дат (DateTime?)
                new CurrencyToStringConverter(),  // Для валют (decimal?)
                new StringNotEmptyConverter()     // Для строк (string)
            }
        };
        builder.Services.AddSingleton(jsonOptions);

        // 🔍 Логирование для отладки
#if DEBUG
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Debug);
#endif

        return builder.Build();
    }
}