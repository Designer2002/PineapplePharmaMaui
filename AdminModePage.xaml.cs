namespace winui_cooler;

public partial class AdminModePage : ContentPage
{
	public AdminModePage()
	{
		InitializeComponent();
	}

    private async void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        await Navigation.PushAsync(new StartPage());
    }
    private void OnPersonTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
    }
    private async void OnGlyphTapped(object sender, TappedEventArgs e)
    {
        await Animations.GlyphColorBlink(sender as Image, Colors.Yellow, Colors.White);
    }

    private async void OnNewTapped(object sender, TappedEventArgs e)
    {
        await Animations.ButtonFrameColorBlink(sender as Frame, Colors.Black, Colors.LawnGreen);
        await Navigation.PushAsync(new AddElement());
    }

    private async void OnEditTapped(object sender, TappedEventArgs e)
    {
        await Animations.ButtonFrameColorBlink(sender as Frame, Colors.Black, Colors.Yellow);
    }

    private async void OnDeleteTapped(object sender, TappedEventArgs e)
    {
        await Animations.ButtonFrameColorBlink(sender as Frame, Colors.Black, Colors.Red);
    }
}