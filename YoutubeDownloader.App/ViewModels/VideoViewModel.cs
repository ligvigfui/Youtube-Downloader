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
        ThumbnailUrl = video?.Thumbnails.GetWithHighestResolution().Url;
        DownloadButtonImage = "download.jpg";
    }

    [RelayCommand]
    async Task Download()
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
