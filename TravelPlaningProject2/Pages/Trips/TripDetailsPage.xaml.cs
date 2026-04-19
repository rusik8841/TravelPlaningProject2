using TravelPlaningProject2.ViewModels.Trips;

namespace TravelPlaningProject2.Pages.Trips;

public partial class TripDetailsPage : ContentPage  // ← partial обязателен!
{
    public TripDetailsPage(TripDetailsViewModel viewModel)  // ← ViewModel должен быть public
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}