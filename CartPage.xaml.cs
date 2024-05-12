
using InputKit.Shared.Controls;
using Mopups.Services;
using Windows.Storage.AccessCache;
using Windows.UI.Input;
using TappedEventArgs = Microsoft.Maui.Controls.TappedEventArgs;

namespace winui_cooler;

public partial class CartPage : ContentPage
{
    public static int id;
    public static Random imageGenerator;
    private Color dirtyYellow = MyColors.dirtyYellow;
    private Color darkGray = MyColors.darkGray;
    private List<MedicineShoppingCartView> Cart;
    private PhoneValidation phone_check;
    public static EventHandler ItemRemoved;
    private int cost;
    private double allprice;
    private EventHandler UpdateRaised;
    private static Label lbl;
    private bool _update;
    private bool Update
    {
        get
        {
            return _update;
        }
        set
        {
            if (_update != value)
            {
                UpdateRaised?.Invoke(lbl, EventArgs.Empty);
            }
            _update = value;

        }
    }
    public CartPage()
    {
        imageGenerator = new Random();
        InitializeComponent();  
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        await InitializeAsync();
    }

    public async Task<List<MedicineShoppingCartView>> GetCart()
    {
        var _ = await ConnectionManager.GetShoppingCart();
        return _.Content == null ? new List<MedicineShoppingCartView>() : _.Content;
    }

    private async Task InitializeAsync()
    {
        Cart = await GetCart();
        await HideIfEmpty();
        DrawShoppingCart();
        ItemRemoved += OnItemRemoved;
        UpdateRaised += OnUpdateRaised;
        startc.IsChecked = true;
        cost = 20;
        shippingcost.Text = $"${cost}.00";
        total_price.Text = (Math.Round(allprice + cost, 2)).ToString();
        phone_check = new PhoneValidation();
        invalid_phone.Text = phone_check.Message;
        
    }

    private async void OnUpdateRaised(object sender, EventArgs e)
    {
        Cart = await GetCart();
        RemoveIfDeleted(int.Parse((sender as Label).ClassId));
        await HideIfEmpty();
        Update = false;
    }

    private void OnItemRemoved(object sender, EventArgs e)
    {
        Update = true;
    }
    private void OnRadioButtonChosen(object sender, EventArgs e)
    {
        // shippingcost.Text = $"${cost}.00";
        // total_price.Text = (Math.Round(allprice + cost, 2)).ToString(); гениальное решение
        var rb = sender as InputKit.Shared.Controls.RadioButton;
        if(rb.IsChecked)
        {
            switch(rb.ClassId)
            {
                case "pickup":
                    cost = 0;
                    break;
                case "standart":
                    cost = 10;
                    break;
                case "speed":
                    cost = 20;
                    break;
            }
            shippingcost.Text = $"${cost}.00";
            total_price.Text = (Math.Round(allprice + cost, 2)).ToString();
        }
    }


    private async Task HideIfEmpty()
    {
        if (Cart.Count > 0)
        {
            foreach(var item in Cart)
            {
                var medicine = await ConnectionManager.GetItemById(item.Id);
                if(!medicine.Prescription)
                {
                    reciepe.IsVisible = false;
                }
            }
        }
        else
        {
            reciepe.IsVisible = false;
        }
        empty.IsVisible = Cart.Count == 0;
        itemcount.Text = $"You have {Cart.Count} items in your shopping cart";
        shipping.IsVisible = Cart.Count > 0;
        total.IsVisible = Cart.Count > 0;
        receipt.IsVisible = Cart.Count > 0;
        phone_entry.IsVisible = Cart.Count > 0;
        confirm.IsVisible = Cart.Count > 0;
        contact.IsVisible = Cart.Count > 0;
        
    }


    private async void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        await Navigation.PushAsync(new StartPage());
    }
    private void OnCartTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
    }
    private async void OnNewsLabelTapped(object sender, TappedEventArgs e)
    {
        OnLabelTapped(sender, e);
        MainPage.isOnNews = true;
        var mp = new MainPage();
        await Navigation.PushAsync(mp);
        mp.ScrollToNewsLabel();
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

    private void RemoveIfDeleted(int id)
    {
        cart_items.Children.RemoveAt(id);
    }

    public async void DrawShoppingCart()
    {
        for (int i = 0; i < Cart.Count; i++)
        {
            var item = await ConnectionManager.GetItemById(Cart[i].Id);
            allprice += item.Price;
            
            var grd = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition() {Width = 75},
                        new ColumnDefinition() {Width = 120},
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition() {Width = 100},
                        new ColumnDefinition() {Width = 100},
                        new ColumnDefinition() {Width = 25}
                    },
            };
            var img = new Image() { HeightRequest = 150, Source = $"med{imageGenerator.Next(1, 6)}.jpg" };
            grd.Children.Add(img);
            grd.SetColumn(img, 1);
            var name = new VerticalStackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20, 0, 0, 0),
                Children =
                {
                    new Label()
                    {
                        Text = item.Name,
                        FontFamily="Battambang-Regular",
                        FontSize=18
                    },
                    new HorizontalStackLayout()
                    {
                        Children =
                        {
                            new Label()
                            {
                                TextColor=item.Prescription ? Colors.Red : Colors.Green,
                                VerticalOptions=LayoutOptions.Center,
                                Text=item.Prescription ? "\uF0E3" : "\uE73A",
                                FontFamily="Segoe MDL2 Assets",
                                FontSize=20
                            },
                            new Label()
                            {
                                Text = item.Prescription ? "Prescription Required" : "No Prescription Required",
                                FontFamily="Battambang-Regular",
                                FontSize=18,
                                TextColor=item.Prescription ? Colors.Red : Colors.Green,
                                Margin=new Thickness(10,0,0,0),
                                VerticalOptions=LayoutOptions.Center,
                            },
                        }
                    }
                }
            };
            grd.Children.Add(name);
            grd.SetColumn(name, 2);
            var count = new Label()
            {
                Text = Cart[i].Count.ToString(),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = "Battambang-Regular",
                FontSize = 20
            };

            var labelUp = new Label()
            {
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(0, 5, 0, 5),
                Text = "\uE935",
                FontFamily = "Segoe MDL2 Assets",
                FontSize = 24
            };
            var labelDown = new Label()
            {
                VerticalOptions = LayoutOptions.Start,
                Text = "\uE936",
                FontFamily = "Segoe MDL2 Assets",
                FontSize = 24
            };
            var tappedUp = new TapGestureRecognizer();
            tappedUp.Tapped += async (sender, e) =>
            {
                int.TryParse(count.Text, out int counter);
                await Animations.TextColorBlink(sender as Label, Colors.Black, Colors.Gray);
                counter += 1;
                count.Text = counter.ToString();
                await ConnectionManager.SetItemCount(item.Id, counter);
                CheckTotal();
            };
            var tappedDown = new TapGestureRecognizer();
            tappedDown.Tapped += async (sender, e) =>
            {
                int.TryParse(count.Text, out int counter);
                if (counter > 1)
                {
                    await Animations.TextColorBlink(sender as Label, Colors.Black, Colors.Gray);
                    counter -= 1;
                    count.Text = counter.ToString();
                    await ConnectionManager.SetItemCount(item.Id, counter);
                    CheckTotal();
                }
            };
            labelDown.GestureRecognizers.Add(tappedDown);
            labelUp.GestureRecognizers.Add(tappedUp);
            var count_btns = new HorizontalStackLayout()
            {
                Children =
                {
                    count,
                    new VerticalStackLayout()
                    {
                        Margin = new Thickness(10,0,0,0),
                        VerticalOptions=LayoutOptions.Center,
                        HorizontalOptions=LayoutOptions.Center,
                        Children =
                        {
                           labelUp,
                           labelDown
                        }
                    }
                }
            };
            grd.Children.Add(count_btns);
            grd.SetColumn(count_btns, 3);
            var price = new Label()
            {
                Text = $"${item.Price*int.Parse(count.Text)}",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                FontFamily = "Battambang-Regular",
                TextColor = Color.FromRgb(33, 33, 33),
                LineBreakMode = LineBreakMode.NoWrap,
                FontSize = 20
            };
            grd.Children.Add(price);
            grd.SetColumn(price, 4);
            var del = new Label()
            {
                TextColor = Colors.Maroon,
                VerticalOptions = LayoutOptions.Center,
                Text = "\uE74D",
                FontFamily = "Segoe MDL2 Assets",
                ClassId = i.ToString(),
                FontSize = 24
            };
            var delTap = new TapGestureRecognizer();
            delTap.Tapped += async (sender, e) => 
            {
                lbl = del;
                MedicineManager.MedicineToDelete = item;
                await Animations.TextColorBlink(sender as Label, Colors.Maroon, Colors.LightCoral);
                id = item.Id;
                await MopupService.Instance.PushAsync(new ConfirmDeleteAlert());
            };
            del.GestureRecognizers.Add(delTap);
            grd.Children.Add(del);
            grd.SetColumn(del, 5);


            Frame frm = new Frame()
            {
                BorderColor = Colors.Navy,
                BackgroundColor = Color.FromArgb("66FFFFFF"),
                Content = grd,
                Margin = new Thickness(40,10,40,0),
            };
            cart_items.Children.Add(frm);
        }
        CheckTotal();

    }

    private void CheckTotal()
    {
        total_price.Text = $"${allprice.ToString()}";
    }

    private async void OnTappedConfirm(object sender, TappedEventArgs e)
    {
        
        await Animations.ButtonFrameColorBlink(confirm, Color.FromArgb("333333"), Colors.LimeGreen);
        if (!phone_check.Validate(phone_entry.Text))
            {
                phone_invalid_image.IsVisible = true;
                invalid_phone.IsVisible = true;
            }
        else 
        {
            phone_invalid_image.IsVisible = false;
                invalid_phone.IsVisible = false;
            await DisplayAlert("MAUI - ГАВНО!", "Я блин наконец-то это поняла", "Гавно!!!!!!!");
        }
    }



}