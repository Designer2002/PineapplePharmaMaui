namespace winui_cooler;

public partial class AddElement : ContentPage
{
    private static double price;
    private static int expiration;
    public OverrideLenValidation descriptionValidation;
    public OverrideRequiredValidation nameValidation;
    public OverrideRequiredValidation releaseValidation;
    public OverrideRequiredValidation distValidation;
    public AddElement()
    {
        InitializeComponent();
        descriptionValidation = new OverrideLenValidation() { MinLength = 10 };
        nameValidation = new OverrideRequiredValidation();
        releaseValidation = new OverrideRequiredValidation();
        distValidation = new OverrideRequiredValidation();
        cat.ItemsSource = new List<string>(MedicineManager.categories.Values.Select(c => c.Name));
        invalid_name.Text = "Naming can't be empty!";
        invalid_release.Text = "Release form can't be empty";
        invalid_description.Text = "Description must be longer than 9 letters.";
        dist_release.Text = "Please enter a country!";
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
    }

    private async void OnEditTapped(object sender, TappedEventArgs e)
    {
        await Animations.ButtonFrameColorBlink(sender as Frame, Colors.Black, Colors.Yellow);
    }

    private async void OnDeleteTapped(object sender, TappedEventArgs e)
    {
        await Animations.ButtonFrameColorBlink(sender as Frame, Colors.Black, Colors.Red);
    }

    private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
    {
        var s = sender as Slider;
        price = s.Value;
        value.Text = price.ToString();
    }

    private async void Add_Tapped(object sender, TappedEventArgs e)
    {
        await Animations.BackColorAndScaleBlink(confirm, Color.FromArgb("333333"), Colors.LimeGreen, 1.0, 1.4);
        
        if(!nameValidation.Validate(name_entry.Text))
        {
            invalid_name.IsVisible = true;
            name_invalid_image.IsVisible = true;
        }
        else
        {
            invalid_name.IsVisible = false;
            name_invalid_image.IsVisible = false;
        }

        if (!descriptionValidation.Validate(description_entry.Text))
        {
            invalid_description.IsVisible = true;
            description_invalid_image.IsVisible = true;
        }
        else
        {
            invalid_description.IsVisible = false;
            description_invalid_image.IsVisible = false;
        }

        if (!releaseValidation.Validate(release_entry.Text))
        {
            release_invalid_image.IsVisible = true;
            invalid_release.IsVisible = true;
        }
        else
        {
            invalid_release.IsVisible = false;
            release_invalid_image.IsVisible = false;
        }

        if (!distValidation.Validate(dist_entry.Text))
        {
            dist_release.IsVisible = true;
            dist_invalid_image.IsVisible = true;
        }
        else
        {
            dist_invalid_image.IsVisible = false;
            dist_release.IsVisible = false;
        }

        if(cat.SelectedIndex != -1 && distValidation.Valid && nameValidation.Valid && descriptionValidation.Valid &&  releaseValidation.Valid)
        {
            var name = name_entry.Text;
            var prescription = Prescription.IsChecked;
            var dist = dist_entry.Text;
            var rel = release_entry.Text;
            var pri = price;
            var exp = expiration;
            var descr = description_entry.Text;
            var act = comp_entry.Text == null ? "N/A" : comp_entry.Text;
            Medicine medicine = new Medicine()
            {
                ActiveComponent = act,
                Description = descr,
                Name = name,
                Price = pri,
                Expiration = expiration,
                Category = cat.SelectedItem.ToString(),
                Distributer = dist,
                ReleaseForm = rel,
                Prescription = prescription
            };
            var res = await ConnectionManager.TryAddToCatalog(medicine);
            if (!res) await DisplayAlert("ERROR!", "shit in pants", "maui = shit"!);
            else await DisplayAlert("SUCCESS!", "shit in pants", "maui = shit"!);

        }

    }

    private void Slider_ValueChanged_1(object sender, ValueChangedEventArgs e)
    {
        var s = sender as Slider;
        expiration = (int)s.Value;
        value2.Text = expiration.ToString();
    }
}