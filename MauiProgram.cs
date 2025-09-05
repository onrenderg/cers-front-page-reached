using Microsoft.Extensions.Logging;

namespace CERS
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register platform-specific SQLite implementation
#if ANDROID
            builder.Services.AddSingleton<ISQLite, Platforms.Android.MauiSQLite>();
#elif IOS
            builder.Services.AddSingleton<ISQLite, Platforms.iOS.MauiSQLite>();
#elif WINDOWS
            builder.Services.AddSingleton<ISQLite, Platforms.Windows.MauiSQLite>();
#endif

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
