using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using TravelPlaningProject2.Models;
using TravelPlaningProject2.Services;

namespace TravelPlaningProject2.ViewModels.Auth;

public partial class LoginViewModel(AuthService authService) : ObservableObject
{
    private readonly AuthService _authService = authService;

    [ObservableProperty] private User? _currentUser;
    [ObservableProperty] private string? _errorMessage;
    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private string? _login;
    [ObservableProperty] private string? _password;

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            // Валидация полей
            if (string.IsNullOrWhiteSpace(Login))
            {
                ErrorMessage = "Поле 'Логин' не заполнено";
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Поле 'Пароль' не заполнено";
                return;
            }

            // Запрос к API
            var (success, token, error) = await _authService.LoginAsync(Login, Password);

            if (!success || string.IsNullOrEmpty(token))
            {
                ErrorMessage = error ?? "Неверный логин или пароль";
                return;
            }

            // Сохранение токена
            Preferences.Set("auth_token", token);

            // Переход на главную страницу
            await Shell.Current.GoToAsync("//TripsPage");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
            ErrorMessage = "Ошибка соединения с сервером";
        }
        finally
        {
            IsBusy = false;
        }
    }
}