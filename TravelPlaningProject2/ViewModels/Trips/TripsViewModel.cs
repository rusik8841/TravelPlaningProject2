using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TravelPlaningProject2.Models;
using TravelPlaningProject2.Services;
using CommunityToolkit.Mvvm.Input;

namespace TravelPlaningProject2.ViewModels.Trips;

public partial class TripsViewModel(TripService tripService) : ObservableObject
{
    private readonly TripService _tripService = tripService;

    [ObservableProperty]
    private ObservableCollection<Trip> _trips = [];
    [ObservableProperty] private bool _isBusy;
    [ObservableProperty] private string? _errorMessage;

    [RelayCommand]
    private async Task LoadTripsAsync()
    {
        if (IsBusy) return;

        IsBusy = true;
        ErrorMessage = string.Empty;

        try
        {
            var (success, tripsList, error) = await _tripService.GetTripsAsync();

            if (success && tripsList != null)
            {
                Trips.Clear();
                foreach (var trip in tripsList) Trips.Add(trip);
            }
            else
            {
                ErrorMessage = error ?? "Не удалось загрузить поездки";
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Load trips error: {ex.Message}");
            ErrorMessage = "Ошибка соединения";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectTripAsync(Trip trip)
    {
        if (trip?.Id == null) return;
        // Передаём ID поездки в страницу деталей
        await Shell.Current.GoToAsync($"//TripDetailsPage?tripId={trip.Id}");
    }

    [RelayCommand]
    private async Task AddTripAsync()
    {
        await Shell.Current.GoToAsync("//CreateTripPage");
    }

    [RelayCommand]
    private async Task GoToProfileAsync()
    {
        await Shell.Current.GoToAsync("//ProfilePage");
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        var confirmed = await Application.Current?.MainPage?.DisplayAlert(
            "Выход", "Вы действительно хотите выйти?", "Да", "Нет");

        if (confirmed == true)
        {
            Preferences.Remove("auth_token");
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}