using CommunityToolkit.Maui.Extensions;
using static Microsoft.Maui.Animations.AnimationLerpingExtensions;

namespace winui_cooler
{
    using CommunityToolkit.Maui;
    using Mopups.Services;

    public partial class StartPage : ContentPage
    {       
        private Color dirtyYellow = MyColors.dirtyYellow;
	    private Color darkGray = MyColors.darkGray;
        public StartPage()
        {
            
            InitializeComponent();

            g_logo.Source = ImageSource.FromFile("google.png");
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (sender, e) =>
            {
                var animation = new Animation();
                animation.Name = "GrayAndBack";
        
                animation.Add(0, 0.5, new Animation((d) =>
                {
                    var color = Color.FromRgb(1 - d, 1 - d, 1 - d); // Создаем цвет с использованием текущего значения анимации (от 1 до 0)
                    gogle.BackgroundColor = color; // Применяем цвет к фону сетки
                    gogle_text.TextColor = Color.FromHex("#fff9a1");
                }));

                animation.Add(0.5, 1, new Animation((d) =>
                {
                    var color = Color.FromRgb(d, d, d); // Создаем цвет с использованием текущего значения анимации (от 0 до 1)
                    gogle.BackgroundColor = color; // Применяем цвет к фону сетки
                    gogle_text.TextColor = Color.FromHex("#666666");
                }));
                gogle.Animate(name:animation.Name, animation:animation, length:1000);
            };

            gogle.GestureRecognizers.Add(tapGestureRecognizer);

        }

        private async void ForgotOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            await Animations.TextColorBlink(forgot_btn, dirtyYellow, darkGray);
        }

        private async void RegOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            await Animations.TextColorBlink(reg_btn, dirtyYellow, darkGray);
            await Navigation.PushAsync(new RegPage());
        }
        private async void LoginOnTapGestureRecognizerTapped(object sender, TappedEventArgs args)
        {
            await Animations.BackColorAndScaleBlink(login_btn, dirtyYellow, darkGray, 1.0, 1.4);
            if(!await ConnectionManager.TryLogin(email_entry.Text, password_entry.Text)) await MopupService.Instance.PushAsync(new CustomMopupAlert());
            else await Navigation.PushAsync(new MainPage());
            
            
        }


    }

  
}