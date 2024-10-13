namespace YoutubeDownloader.ViewModels;

partial class MainViewModel : ObservableObject
{
    [RelayCommand]
    async Task NavigateToPlaylists()
    {
        await Shell.Current.GoToAsync(nameof(PlaylistPage));
    }

    [RelayCommand]
    async Task NavigateToSettings()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }
}
