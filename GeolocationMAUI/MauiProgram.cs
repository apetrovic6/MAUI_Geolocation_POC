using Microsoft.Extensions.Logging;
using GeolocationUtils.Services;
using GeolocationUtils.Services.Interfaces;

namespace GeolocationMAUI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

       
        #if ANDROID
			builder.Services.AddSingleton<IBackgroundService, BackgroundLocationServiceAndroid>();
        #endif

        #if IOS
            builder.Services.AddScoped<GeolocationUtils.Platforms.iOS.LocationManageriOS>();
            builder.Services.AddSingleton<IBackgroundService, GeolocationUtils.Platforms.iOS.LocationBackgroundServiceiOS>();
        #endif


        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<MainPage>();
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
