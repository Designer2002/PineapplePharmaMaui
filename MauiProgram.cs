using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;
using InputKit.Handlers;

namespace winui_cooler;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddInputKitHandlers();
			})
			.ConfigureMopups()
			.UseSkiaSharp()
			.UseUraniumUI()
			.UseUraniumUIBlurs()
			.UseMauiCommunityToolkitMediaElement()
			.UseUraniumUIMaterial()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Battambang-Regular.ttf", "BattambangRegular");
				fonts.AddFont("Battambang-Black.ttf", "BattambangBlack");
				fonts.AddFont("Battambang-Thin.ttf", "BattambangThin");
				fonts.AddFluentIconFonts();
			});

		builder.UseMauiCommunityToolkit();
		builder.Services.AddTransient<MainPage>();
    	builder.Services.AddTransient<StartPage>();
		builder.Services.AddTransient<RegPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
