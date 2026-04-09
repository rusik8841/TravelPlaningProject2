namespace TravelPlaningProject2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnStartClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            // Анимация
            if (button != null)
            {
                await button.ScaleTo(0.95, 50, Easing.CubicIn);
                await button.ScaleTo(1, 50, Easing.CubicOut);
            }

            // Переход на регистрацию
            // Убедись, что namespace здесь совпадает с реальным расположением файлов!
            Application.Current.MainPage = new NavigationPage(new TravelPlaningProject2.Pages.Auth.RegisterPage());
        }

        private void OnLoginClicked(object sender, EventArgs e) =>
            // Переход на вход
            Application.Current.MainPage = new NavigationPage(new TravelPlaningProject2.Pages.Auth.LoginPage());

    }
}
