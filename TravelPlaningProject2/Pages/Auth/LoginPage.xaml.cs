namespace TravelPlaningProject2.Pages.Auth
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void OnRegisterTapped(object sender, TappedEventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Вход", "Добро пожаловать!", "OK");
            await Navigation.PushAsync(new TravelPlaningProject2.Pages.HomePage());
        }
    }
}