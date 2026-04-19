using TravelPlaningProject2.ViewModels.Trips;

namespace TravelPlaningProject2.Pages.Trips;

public partial class TripDetailsPage : ContentPage  // ← partial обязательно!
{
    public TripDetailsPage(TripDetailsViewModel viewModel)
    {
        InitializeComponent();  // ← Должно работать
        BindingContext = viewModel;
    }
}