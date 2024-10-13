namespace YoutubeDownloader.ViewModels;

public partial class VideoViewModel : ObservableObject
{
    YoutubeClient Youtube;

    public IVideo Video;

    [ObservableProperty]
    string? thumbnailUrl;

    [ObservableProperty]
    string? title;

    [ObservableProperty]
    string? error;

    [ObservableProperty]
    ImageSource downloadButtonImage;

    public VideoViewModel(YoutubeClient youtube, IVideo video)
    {
        Youtube = youtube;
        Video = video;
        Title = video?.Title;
        ThumbnailUrl = video?.Thumbnails.First(x => x.Resolution.Equals(new Resolution(120, 90))).Url;
        DownloadButtonImage = "download.jpg";
    }

    [RelayCommand]
    void Download()
    {
        _ = DownloadInner();
    }

    public async Task DownloadInner()
    {
        try
        {
            await Youtube.Download(Video);
            DownloadButtonImage = "tick.png";
        }
        catch (Exception ex)
        {
            Error = $"An error occurred {ex.Message}";
            DownloadButtonImage = "retry.png";
        }
    }
}
