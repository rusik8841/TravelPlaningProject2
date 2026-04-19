using TravelPlaningProject2.ViewModels.Auth;

namespace TravelPlaningProject2.Pages.Auth;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void ToRegisterPage(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//RegisterPage");
    }
}