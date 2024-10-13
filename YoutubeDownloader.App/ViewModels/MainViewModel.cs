namespace YoutubeDownloader.ViewModels;

partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    void NavigateToPlaylists()
    {
        Shell.Current.GoToAsync(nameof(PlaylistPage));
    }

    [RelayCommand]
    void NavigateToSettings()
    {
        Shell.Current.GoToAsync(nameof(SettingsPage));
    }
}
