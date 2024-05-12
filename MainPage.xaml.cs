

using CommunityToolkit.Maui.Views;

namespace winui_cooler;

public partial class MainPage : ContentPage
{
	private Color dirtyYellow = MyColors.dirtyYellow;
	private Color darkGray = MyColors.darkGray;
	public static bool isOnNews;
	private Random r;
	public MainPage()
	{
		r = new Random();
		
		
		InitializeComponent();
		
		carousel.ItemsSource = new List<TrendingMedicine>()
		{
			new TrendingMedicine() {Name = "Aspirin", Price = "$0.99"},
			new TrendingMedicine() {Name = "Bromelain", Price = "$2.99"},
			new TrendingMedicine() {Name = "Cocaine", Price = "$19.99"},
			new TrendingMedicine() {Name = "Medical Marijuana", Price = "$16.99"},
			new TrendingMedicine() {Name = "Heroine", Price = "$12.99"},
			new TrendingMedicine() {Name = "Caffeine", Price = "$7.99"},
		};
		AnimateNumber(r.Next(12345), l1);
		AnimateNumber(r.Next(6666), l2);
		AnimateNumber(r.Next(12345), l3);
		AnimateNumber(r.Next(1234), l4);
		//CarouselLoaded();
		
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();

		// Выполняем инициализацию страницы

		// Если страница открыта с условием прокрутки, то выполняем прокрутку
		if (isOnNews)
		{
			ScrollToNewsLabel();
		}
	}
	async void AnimateNumber(int value, Label label)
        {
            int targetValue = value;
            int steps = 100;
            int delay = 30; // Delay between steps in milliseconds

            for (int i = 0; i <= steps; i++)
            {
                int newValue = (int)Math.Round((double)targetValue * i / steps);
                label.Text = newValue.ToString();
                await Task.Delay(delay);
            }
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
	private void OnNewsLabelTapped(object sender, TappedEventArgs e)
	{
		OnLabelTapped(sender, e);
		ScrollToNewsLabel();
	}

	private void OnHomeLabelTapped(object sender, TappedEventArgs e)
	{
		OnLabelTapped(sender, e);
		ScrollToNavbar();
	}
	public void ScrollToNewsLabel()
	{
		// Прокручиваем до элемента "news"
		scroll.ScrollToAsync(news, ScrollToPosition.Start, true);
		isOnNews = false;
	}
	private void ScrollToNavbar()
	{
		// Прокручиваем до элемента "news"
		scroll.ScrollToAsync(covid, ScrollToPosition.Start, true);
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
	private async void OnGlyphTapped(object sender, TappedEventArgs e)
	{
		await Animations.GlyphColorBlink(sender as Image, darkGray, dirtyYellow);
	}

	// async Task StartAutoScroll()
    //     {
    //         while (true)
    //         {
    //             await Task.Delay(TimeSpan.FromSeconds(2)); // Ждать 3 секунды

    //             int currentPosition = carousel.Position;

	// 			// Получить количество элементов в карусели
	// 			int itemCount = carousel.ItemsSource.Cast<object>().Count();

	// 			// Вычислить индекс следующего элемента
	// 			int nextPosition = (currentPosition + 1) % itemCount;

	// 			// Прокрутить карусель на следующий элемент
	// 			carousel.ScrollTo(nextPosition);
    //     }
	// 	}
	// async void CarouselLoaded()
	// {
	// 	await StartAutoScroll();
	// }

		

}
  public class TrendingMedicine
    {
        public string Name {get; set;}
        public string Price {get; set;}
    }
