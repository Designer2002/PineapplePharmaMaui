using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using UraniumUI.Material.Controls;
using static System.Net.Mime.MediaTypeNames;
using Image = Microsoft.Maui.Controls.Image;

namespace winui_cooler;

public partial class CategoryPage : ContentPage
{
    private Color dirtyYellow = MyColors.dirtyYellow;
	private Color darkGray = MyColors.darkGray;
    public static Random imageGenerator;
    private const double max_threshold = 500;
    private const double min_threshold = 330;
    public CategoryPage()
    {
        imageGenerator = new Random();
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Ваша асинхронная инициализация здесь
        await CountAsync();

        // Заполнение компонентов и прочее
        FillGridWithItems();
        brdcrmb.Text = MedicineManager.Current.Name;
        title.Text = MedicineManager.Current.Name;
        decr.Text = MedicineManager.Current.Description;
        pic.Source = ImageSource.FromFile($"{MedicineManager.Current.ImagePath}.jpg");
        found.Text = $"In category {title.Text} found {MedicineManager.Current.ItemsCount} items:";
    }

    private async Task CountAsync()
    {
        MedicineManager.Current.ItemsCount = await MedicineManager.CategoryCountAsync(MedicineManager.Current.Name);
    }
    private async void OnLogoutTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        await Navigation.PushAsync(new StartPage());
    }
    private void OnCartTapped(object sender, TappedEventArgs e)
    {
        OnGlyphTapped(sender, e);
        Navigation.PushAsync(new CartPage());
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

    private async void OnBtnTapped(object sender, TappedEventArgs e)
    {
        var tapInfo = int.Parse((sender as View).ClassId);
        await Animations.ButtonFrameColorBlink(sender as Frame, Colors.Black, Colors.Magenta);
        var cur = await MedicineManager.CategoryMedicines(MedicineManager.Current.Name);
        MedicineManager.ChosenMedicine = cur[tapInfo];
        await Navigation.PushAsync(new ItemPage());
    }

    private async void FillGridWithItems()
    {
        //cols = 4
        int totalCards = MedicineManager.Current.ItemsCount; // Получаем общее количество карточек
        int rows = MedicineManager.Current.ItemsCount % 4 == 0 ? MedicineManager.Current.ItemsCount / 4 : MedicineManager.Current.ItemsCount / 4 + 1; // Рассчитываем количество рядов
        int cardIndex = 0;
        for (int i = 0; i < rows; i++)
        {
            main_grd.RowDefinitions.Add(new RowDefinition()); // Добавляем определение строки
        }
        for (int i = 0; i < 4; i++)
        {
            main_grd.ColumnDefinitions.Add(new ColumnDefinition()); // Добавляем определение строки
        }
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < 4; col++) // Проходим по каждой колонке
            {
                if (cardIndex < totalCards)
                {
                    main_grd.Children.Add(await MakeItem(col, row, main_grd, cardIndex)); // Добавляем элемент в сетку
                    cardIndex++;
                }
            }
        }

    }

    private async Task<Medicine> InitMedicine(int i)
    {
        var list = await MedicineManager.CategoryMedicines(MedicineManager.Current.Name);
        return list[i];
    }

    private async Task<Frame> MakeItem(int col, int row, Grid grd, int card_index)
    {
        var medicine = await InitMedicine(card_index);
        VerticalStackLayout vert = new VerticalStackLayout()
        {
            HeightRequest = 350
        };
        var img = new Image()
        {
            Source = $"med{imageGenerator.Next(1, 6)}.jpg",
            HeightRequest = 270,
            VerticalOptions = LayoutOptions.Start
        };
        vert.Add(img);
        var namelbl = new Label()
        {
            Text = medicine.Name.Length > 12 ? $"{medicine.Name.Substring(0, 12)}..." : medicine.Name,
            FontFamily = "Battambang-Regular",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Center,
            ClassId = "na"
        };
        vert.Add(namelbl);
        vert.Add(new Label(){
            Text = $"${medicine.Price}",
            FontFamily="Battambang-Regular",
            FontSize= 16,
            TextColor=Colors.DarkGray,
            HorizontalOptions=LayoutOptions.Center
        });
        Frame view = new Frame
        {
            BackgroundColor = Colors.White,
            HeightRequest = 370,
            CornerRadius = 10,
            Content = vert,
            Margin = new Thickness(20,20,20,40),
            Padding = new Thickness(10,10,10,10),
            ClassId=card_index.ToString()
        };
        var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnBtnTapped;
        var pointerRecognizer = new PointerGestureRecognizer();
        
        pointerRecognizer.PointerEntered += OnHover;
        pointerRecognizer.PointerExited += OnUnhover;
        view.GestureRecognizers.Add(tapGestureRecognizer);
        view.GestureRecognizers.Add(pointerRecognizer);
        grd.SetColumn(view, col);
        grd.SetRow(view, row);
        
        return view;
    }

    private async void OnHover(object sender, PointerEventArgs e)
    {
        await UpScale(sender);
    }

    private async void OnUnhover(object sender, PointerEventArgs e)
    {
        await DownScale(sender);

    }

    private async Task UpScale(object sender)
    {
        var view = sender as Frame;
        var m = await InitMedicine(int.Parse(view.ClassId));
        var fullName = m.Name;
        view.BorderColor = Colors.Black;
        float progress = 0;
        var delta = 0.05f;
        while (progress < 1)
        {
            view.HeightRequest = AnimationLerpingExtensions.Lerp(view.HeightRequest, max_threshold, progress: delta);
            progress += delta;
            await Task.Delay(16);
        }
        var chi = view.Content as VerticalStackLayout;
        foreach (var c in chi.Children)
        {
            if (c is Label)
            {
                var cc = c as Label;
                if (cc.ClassId == "na")
                {
                    cc.Text = fullName;
                }
            }
        }
    }

    private async Task DownScale(object sender)
    {
        var view = sender as Frame;
        view.BorderColor = Colors.Transparent;
        var m = await InitMedicine(int.Parse(view.ClassId));
        var fullName = m.Name;
        float progress = 0;
        var delta = 0.05f;
        while (progress < 1)
        {
            view.HeightRequest = AnimationLerpingExtensions.Lerp(view.HeightRequest, min_threshold, progress: delta);
            progress += delta;
            await Task.Delay(16);
        }
        var chi = view.Content as VerticalStackLayout;
        foreach (var c in chi.Children)
        {
            if (c is Label)
            {
                var cc = c as Label;
                if (cc.ClassId == "na")
                {
                    cc.Text = fullName.Length > 12 ? $"{fullName.Substring(0, 12)}..." : fullName;
                }
            }
        }
        view.Padding = new Thickness(10, 10, 10, 10);
        //view.HeightRequest = min_threshold;
    }
}
