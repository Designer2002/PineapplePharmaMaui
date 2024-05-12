using Microsoft.Maui.Platform;
using Mopups.Services;

namespace winui_cooler;

public partial class ConfirmDeleteAlert : Mopups.Pages.PopupPage
{

	public ConfirmDeleteAlert()
	{
		InitializeComponent();
        alerttext.Text = $"{MedicineManager.MedicineToDelete.Name} will be removed from your cart. Do you want to delete it or decided to go back?";
	}

    private void CloseOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
    {
        MopupService.Instance.PopAsync();
    }


    private async void OnBTapped(object sender, TappedEventArgs e)
    {
        await Animations.BackColorAndScaleBlink(sender as ContentView, Colors.Purple, Colors.LawnGreen, 1.0, 1.2);
        await MopupService.Instance.PopAsync();
    }
    private async void OnDTapped(object sender, TappedEventArgs e)
    {
        await Animations.BackColorAndScaleBlink(sender as ContentView, Colors.OrangeRed, Colors.LawnGreen, 1.0, 1.2);
        await ConnectionManager.TryDeleteFromCart(CartPage.id);
        CartPage.ItemRemoved?.Invoke(sender, null);
        await MopupService.Instance.PopAsync();


    }
}