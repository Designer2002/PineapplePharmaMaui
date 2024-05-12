using Microsoft.Maui.Platform;
using Mopups.Services;

namespace winui_cooler;

public partial class AddedToCartAlert : Mopups.Pages.PopupPage
{

	public AddedToCartAlert()
	{
		InitializeComponent();
        alerttext.Text = $"{MedicineManager.ChosenMedicine.Name} has been added to your cart!";
	}

    private void CloseOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
    {
        MopupService.Instance.PopAsync();
    }


    private async void OnCTapped(object sender, TappedEventArgs e)
    {
        await Animations.BackColorAndScaleBlink(sender as ContentView, Colors.DarkCyan, Colors.LawnGreen, 1.0, 1.2);
        await MopupService.Instance.PopAsync();
    }
     private async void OnMTapped(object sender, TappedEventArgs e)
    {
        await Animations.BackColorAndScaleBlink(sender as ContentView, Colors.OrangeRed, Colors.LawnGreen, 1.0, 1.2);
        await Navigation.PushAsync(new CartPage());
        await MopupService.Instance.PopAsync();


    }
}