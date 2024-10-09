namespace YoutubeDownloader.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel settingsViewModel)
    {
		InitializeComponent();
        BindingContext = settingsViewModel;
    }
}