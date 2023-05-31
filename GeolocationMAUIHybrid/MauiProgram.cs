using GeolocationMAUIHybrid.Data;
using Microsoft.Extensions.Logging;
using GeolocationUtils.Services;
using GeolocationUtils.Services.Interfaces;

namespace GeolocationMAUIHybrid
{
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
                });

            builder.Services.AddMauiBlazorWebView();

            
#if ANDROID
			builder.Services.AddSingleton<IBackgroundService, BackgroundLocationServiceAndroid>();
#endif

#if IOS
            builder.Services.AddScoped<GeolocationUtils.Platforms.iOS.LocationManageriOS>();
            builder.Services.AddSingleton<IBackgroundService, GeolocationUtils.Platforms.iOS.LocationBackgroundServiceiOS>();
#endif


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}