using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using TravelPlaningProject2.Pages.Auth;
using TravelPlaningProject2.Pages.History;
using TravelPlaningProject2.Pages.Profile;
using TravelPlaningProject2.Pages.Trips;
using TravelPlaningProject2.Services;
using TravelPlaningProject2.ViewModels.Auth;
using TravelPlaningProject2.ViewModels.Profile;
using TravelPlaningProject2.ViewModels.Trips;
using Microsoft.Extensions.DependencyInjection; 

namespace TravelPlaningProject2;

public static class MauiProgram
{
    // 🔑 Замените на реальный адрес вашего API
    public const string ApiBaseUrl = "https://travel.local/api";

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 🌐 Настройка HttpClient и сервисов
        builder.Services.AddHttpClient<AuthService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        builder.Services.AddHttpClient<TripService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        builder.Services.AddHttpClient<ProfileService>(client =>
        {
            client.BaseAddress = new Uri(ApiBaseUrl);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // 🧠 Регистрация ViewModel (Transient = новый экземпляр при навигации)
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterViewModel>();

        // 🔥 РЕГИСТРАЦИЯ СТРАНИЦ
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();


        // ⚙️ Настройки JSON сериализации
        builder.Services.AddSingleton(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters =
            {
                new JsonStringEnumConverter(),
                // new DateConverter(), // Раскомментируйте, если используете свой конвертер
            }
        });

        // 🔍 Логирование для отладки
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}