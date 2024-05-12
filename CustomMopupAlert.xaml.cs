using Microsoft.Maui.Platform;
using Mopups.Services;

namespace winui_cooler;

public partial class CustomMopupAlert : Mopups.Pages.PopupPage
{

	public CustomMopupAlert()
	{
		InitializeComponent();
	}

    private void CloseOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
    {
        MopupService.Instance.PopAsync();
    }


}