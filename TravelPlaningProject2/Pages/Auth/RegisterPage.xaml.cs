namespace TravelPlaningProject2.Pages.Auth
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void OnCurrencyClicked(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                foreach (var child in ((Grid)btn.Parent).Children)
                {
                    if (child is Button b)
                    {
                        b.BackgroundColor = Colors.White;
                        b.TextColor = Colors.Gray;
                    }
                }

                btn.BackgroundColor = Color.FromArgb("#EFF6FF");
                btn.TextColor = Color.FromArgb("#2563EB");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new TravelPlaningProject2.Pages.HomePage());

        }
    }
}