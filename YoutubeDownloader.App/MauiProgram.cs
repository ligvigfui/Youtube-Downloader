namespace YoutubeDownloader;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<Pages.App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<YoutubeClient>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

        builder.Services.AddSingleton<PlaylistPage>();
        builder.Services.AddSingleton<PlaylistViewModel>();

        builder.Services.AddTransient<VideoView>();

        builder.Services.AddSingleton<SettingsPage>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton(FileSaver.Default);
#if DEBUG

        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
