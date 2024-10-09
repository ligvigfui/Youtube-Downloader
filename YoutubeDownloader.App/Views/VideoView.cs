namespace YoutubeDownloader.Views;

public class VideoView(IVideo video)
{
    public string ThumbnailUrl = video.Thumbnails.GetWithHighestResolution().Url;
    public string Title = video.Title;
    public IVideo Video = video;
}
