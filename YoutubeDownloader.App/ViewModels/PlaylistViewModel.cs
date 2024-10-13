namespace YoutubeDownloader.ViewModels;

public partial class PlaylistViewModel : ObservableObject
{
    YoutubeClient Youtube;

    [ObservableProperty]
    string playlistUrl;

    [ObservableProperty]
    ObservableCollection<VideoViewModel> videos = [];

    [ObservableProperty]
    int? start;

    [ObservableProperty]
    int? end;

    Range? Range => Start is null || End is null ? null : Start..End;

    public string DownloadRangeText => Range is null ? "" : "Download Selected";

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

    partial void OnPlaylistUrlChanged(string? oldValue, string newValue)
    {
        try
        {
            _ = LoadPlaylist();
        }
        catch {}
    }

    async Task LoadPlaylist()
    {
        Videos.Clear();
        var playlist = await Youtube.Playlists.GetAsync(PlaylistUrl);
        var videos = Youtube.Playlists.GetVideosAsync(playlist.Id);
        await foreach (var video in videos)
        {
            Videos.Add(new VideoViewModel(Youtube, video));
        }
    }

    [RelayCommand]
    void RangeStart(string title)
    {
        var index = Videos.IndexOf(Videos.First(v => v.Video.Title == title));
        Start = index;
        End ??= Videos.Count;
        if (End < Start)
            End = null;
    }

    [RelayCommand]
    void RangeEnd(string title)
    {
        var index = Videos.IndexOf(Videos.First(v => v.Video.Title == title));
        End = index;
        Start ??= 0;
        if (Start > End)
            Start = null;
    }

    [RelayCommand]
    void DownloadRange()
    {
        _ = DownloadRangeInner();
    }

    async Task DownloadRangeInner()
    {
        if (Range is null)
            return;
        foreach (var video in Videos.ToList()[Range.Value])
        {
            await video.DownloadInner();
        }
    }
}
