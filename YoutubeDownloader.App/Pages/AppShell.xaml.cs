namespace YoutubeDownloader.Pages;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Register routes
        Routing.RegisterRoute(nameof(PlaylistPage), typeof(PlaylistPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}
