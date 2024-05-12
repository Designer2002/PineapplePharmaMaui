
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;

class Animations
{
    public static async Task BackColorBlink(ContentView element, Color c1, Color c2)
    {
        await Task.WhenAll
        (
            element.ColorTo(c1, c2, c => element.BackgroundColor = c, 500, Easing.Linear),
            element.ColorTo(c2, c1, c => element.BackgroundColor = c, 500, Easing.Linear)
        );
    }

    public static async Task TextColorBlink(ContentView element, Color c1, Color c2)
    {
        var lbl = element.Content as Label;
        await Task.WhenAll
        (
            element.ColorTo(c1, c2, c => lbl.TextColor = c, 500, Easing.Linear),
            element.ColorTo(c2, c1, c => lbl.TextColor = c, 500, Easing.Linear)
        );
    }


    public static async Task GlyphColorBlink(Image img, Color c1, Color c2)
    {
        if (img.Source is FontImageSource fontImageSource)
        {
            await Task.WhenAll
            (
                img.ColorTo(c1, c2, async c => 
                {
                    fontImageSource.Color = c;
                    img.Source = fontImageSource;
                    await Task.Delay(250); // Для плавности анимации
                }, 500, Easing.Linear),
                img.ColorTo(c2, c1, async c => 
                {
                    fontImageSource.Color = c;
                    img.Source = fontImageSource;
                    await Task.Delay(250); // Для плавности анимации
                }, 500, Easing.Linear)
            );
        }
    }
    

    public static async Task TextColorBlink(Label element, Color c1, Color c2)
    {
        await Task.WhenAll
        (
            element.ColorTo(c1, c2, c => element.TextColor = c, 500, Easing.Linear),
            element.ColorTo(c2, c1, c => element.TextColor = c, 500, Easing.Linear)
        );
    }

    

   public static async Task BackColorAndScaleBlink(ContentView element, Color c1, Color c2, double scale1, double scale2)
    {
        await Task.WhenAll(
            element.ColorTo(c1, c2, c => element.BackgroundColor = c, 500, Easing.Linear),
            element.ChangeSize(scale1, scale2, c => element.Scale = c, 500, Easing.Linear),
            element.ColorTo(c2, c1, c => element.BackgroundColor = c, 500, Easing.Linear),
            element.ChangeSize(scale2, scale1, c => element.Scale = c, 500, Easing.Linear)
        );
    }

    public async static Task ButtonBorderColorBlink(Border border, Color c1, Color c2)
    {
        await Task.WhenAll
        (
        border.ColorTo(c1, c2, c => border.Stroke = c, 500, Easing.Linear),
        border.ColorTo(c2, c1, c => border.Stroke = c, 500, Easing.Linear)
       );
    }

    public async static Task ButtonFrameColorBlink(Frame frame, Color c1, Color c2)
    {
        await Task.WhenAll
        (
        frame.ColorTo(c1, c2, c => frame.BorderColor = c, 500, Easing.Linear),
        frame.ColorTo(c2, c1, c => frame.BorderColor = c, 500, Easing.Linear)
       );
    }

     public async static Task FrameBgColorBlink(Frame frame, Color c1, Color c2)
    {
        await Task.WhenAll
        (
        frame.BackgroundColorTo(c2, length: 500, easing: Easing.Linear),
        frame.BackgroundColorTo(c1, length: 500, easing: Easing.Linear)
       );
    }

    public static async void AnimateGradient(View view, Color c1, Color c2)
        {
             bool forward = true; // Переменная, указывающая направление анимации (true - вперед, false - назад)
            double progress = 0;
            while (true)
            {
                await Task.Delay(2000); // Задержка между изменениями цветов в градиенте

                Color startColor = c1; // Начальный цвет градиента
                Color endColor = c2; // Конечный цвет градиента

                // Создание анимации градиента
                var animation = new Microsoft.Maui.Controls.Animation(callback: d =>
                {
                    if (forward)
                        progress += 0.05;
                    else
                        progress -= 0.05;

                     if (progress >= 1)
                        forward = false;
                    else if (progress <= 0)
                        forward = true;


                    var color = AnimationLerpingExtensions.Lerp(startColor, endColor, progress);
                    // Применение нового цвета градиента к элементу, например, к фону страницы
                    view.Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(0, 1),
                        GradientStops =
                        {
                            new GradientStop {Color = color, Offset = 0.0f},
                            new GradientStop {Color = endColor, Offset = 1.0f}
                        }
                    };
                }, start: 0, end: 1);

                animation.Commit(view, "GradientAnimation", length: 8000, repeat: () => true);
            }
        }



}