using TravelPlaningProject2.ViewModels.Trips;

namespace TravelPlaningProject2.Pages.Trips;

public partial class TripsPage : ContentPage
{
    private readonly TripsViewModel _viewModel;

    public TripsPage(TripsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Загружаем данные при каждом появлении страницы
        _viewModel.LoadTripsCommand.Execute(null);
    }
}