namespace winui_cooler;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(StartPage), typeof(StartPage));
		Routing.RegisterRoute(nameof(RegPage), typeof(RegPage));
	}
}
