namespace YoutubeDownloader.ViewModels;

partial class MainViewModel : ObservableObject
{
    public IAsyncRelayCommand NavigateToPlaylistsCommand =>
        new AsyncRelayCommand(NavigateToPlaylists);
    
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
