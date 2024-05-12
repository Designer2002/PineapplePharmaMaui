namespace winui_cooler;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

	}
	protected override void OnStart()
    {
        MainPage = new NavigationPage(new StartPage());

    }
	protected override Window CreateWindow(IActivationState activationState)
    {
        var window = new Window(new NavigationPage(new StartPage()));
        
        const int newWidth = 1366;
        const int newHeight = 768;
        
        window.Width = newWidth;
        window.Height = newHeight;

        return window;
    }
	
}
