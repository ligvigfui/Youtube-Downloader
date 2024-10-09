namespace YoutubeDownloader.ViewModels;

public partial class PlaylistViewModel : ObservableObject
{
    YoutubeClient Youtube;

    [ObservableProperty]
    string playlistUrl;

    [ObservableProperty]
    ObservableCollection<VideoView> videos;

    public PlaylistViewModel(YoutubeClient youtube)
    {
        Youtube = youtube;
        PlaylistUrl = string.Empty;
        Videos = [];
    }

    [RelayCommand]
    public async Task Paste()
    {
        var paste = await Clipboard.GetTextAsync();
        if (!string.IsNullOrEmpty(paste))
            PlaylistUrl = paste;
    }
}
