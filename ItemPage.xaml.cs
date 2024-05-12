using Mopups.Services;
using Windows.Graphics.Printing;

namespace winui_cooler;

public partial class ItemPage : ContentPage
{
    private Color dirtyYellow = MyColors.dirtyYellow;
	private Color darkGray = MyColors.darkGray;
	public ItemPage()
	{
		InitializeComponent();	
        ItemViewModel model = new ItemViewModel();
        this.BindingContext = model;
        catbrd.Text = MedicineManager.ChosenMedicine.Category;
        brdcrmb.Text = MedicineManager.ChosenMedicine.Name;
        title.Text = MedicineManager.ChosenMedicine.Name;
        pic.Source = $"med{CategoryPage.imageGenerator.Next(1,6)}.jpg";
        price.Text = $"${MedicineManager.ChosenMedicine.Price}";
        if (MedicineManager.ChosenMedicine.Prescription)
        {
            prescription_icon.TextColor = Colors.Red;
            prescription_text.TextColor = Colors.Red;
            prescription_text.Text = "Prescription Required";
        }
        else
        {
            prescription_icon.TextColor = Colors.Green;
            prescription_text.TextColor = Colors.Green;
            prescription_text.Text = "No Prescription Required";
        }
        also_pic1.Source = $"med{CategoryPage.imageGenerator.Next(1,6)}.jpg";
        also_text1.Text = "Lorem Ipsum";
        also_price1.Text = "$0.99";

        also_pic2.Source = $"med{CategoryPage.imageGenerator.Next(1,6)}.jpg";
        also_text2.Text = "Lorem Ipsum";;
        also_price2.Text = "$0.99";

        also_pic3.Source = $"med{CategoryPage.imageGenerator.Next(1,6)}.jpg";
        also_text3.Text = "Lorem Ipsum";
        also_price3.Text = "$0.99";

        also_pic4.Source = $"med{CategoryPage.imageGenerator.Next(1,6)}.jpg";
        also_text4.Text = "Lorem Ipsum";
        also_price4.Text = "$0.99";

        also_pic5.Source = $"med{CategoryPage.imageGenerator.Next(1,6)}.jpg";
        also_text5.Text = "Lorem Ipsum";
        also_price5.Text = "$0.99";



        // description = new DataTemplate(() =>
        // {
        //     var grid = new Grid();
        //     var label = new Label
        //     {
        //         FontFamily = "Battambang-Regular",
        //         LineBreakMode = LineBreakMode.WordWrap,
        //         Text = "AMOGUS",
        //         FontSize = 20
        //     };
        //     label.SetBinding(Label.TextProperty, "Description"); // Устанавливаем привязку к свойству Description
        //     grid.Children.Add(label);

        //     // Возвращаем ViewCell с содержимым
        //     return new ViewCell { View = grid };
        // });

        
        //Animations.AnimateGradient(buybtn, Colors.LightSkyBlue, Colors.Magenta);
        //title_icon.Source = new FontImageSource() { Color=Colors.White, Glyph="&#xF167;", FontFamily="Segoe MDL2 Assets", Size=24 };
	}
	public static int GenerateRandomNumberExcluding(int min, int max, int excludedNumber)
    {
        Random random = new Random();
        int randomNumber;
        do
        {
            randomNumber = random.Next(min, max);
        } while (randomNumber == excludedNumber);
        return randomNumber;
    }
	private void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
    }
    private void OnCartTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        Navigation.PushAsync(new CartPage());
    }
    private void OnNewsLabelTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
    }
    private void OnShopLabelTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        Navigation.PushAsync(new Shop());
    }
    private void OnCatLabelTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        Navigation.PushAsync(new CategoryPage());
    }
    private async void OnLabelTapped(object sender, TappedEventArgs e)
    {
        await Animations.TextColorBlink(sender as Label, darkGray, dirtyYellow);
    }
    private void OnPersonTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
    }
    private void OnHomeTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        Navigation.PushAsync(new MainPage());
    }
    private async void OnGlyphTapped(object sender, TappedEventArgs e)
    {
        await Animations.GlyphColorBlink(sender as Image, darkGray, dirtyYellow);
    }

    private async void OnBuyTapped(object sender, TappedEventArgs e)
    {
        await Animations.BackColorAndScaleBlink(buybtn, Colors.DarkCyan, Colors.LawnGreen, 1.0, 1.4);
        if (await ConnectionManager.TryAddToCart(MedicineManager.ChosenMedicine.Id))
        {
            await MopupService.Instance.PushAsync(new AddedToCartAlert());
        }
        else
        {
           await DisplayAlert("ERROR", "An error occured", "fuck maui");
        }
    }
}

public class ItemViewModel : BindableObject
{
    
        private string _description;
        private string _expiration;
        private string _distributer;
        private string _releaseform;
        private string _activecomponent;

        // Свойство Name для привязки к Label
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(); // Уведомляем об изменении свойства
            }
        }
        public string ReleaseForm
        {
            get { return _releaseform; }
            set
            {
                _releaseform = value;
                OnPropertyChanged(); // Уведомляем об изменении свойства
            }
        }
        public string Distributer
        {
            get { return _distributer; }
            set
            {
                _distributer = value;
                OnPropertyChanged(); // Уведомляем об изменении свойства
            }
        }
        public string ExpirationDate
        {
            get { return _expiration; }
            set
            {
                _expiration = value;
                OnPropertyChanged(); // Уведомляем об изменении свойства
            }
        }
        public string ActiveComponent
        {
            get { return _activecomponent; }
            set
            {
                _activecomponent = value;
                OnPropertyChanged(); // Уведомляем об изменении свойства
            }
        }

        // Конструктор
        public ItemViewModel()
        {
            // Устанавливаем начальное значение свойства Name
            Description = MedicineManager.ChosenMedicine.Description;
            ActiveComponent = MedicineManager.ChosenMedicine.ActiveComponent;
            ReleaseForm = MedicineManager.ChosenMedicine.ReleaseForm;
            Distributer = MedicineManager.ChosenMedicine.Distributer;
            ExpirationDate = $"{MedicineManager.ChosenMedicine.Expiration} years";
        }
    
}