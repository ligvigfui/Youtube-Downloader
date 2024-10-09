namespace YoutubeDownloader;

public static class VideoExtensions
{
    public static string Describe(this Video video)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Title: {video.Title}");
        sb.AppendLine($"Description: {video.Description}");
        sb.AppendLine($"Author: {video.Author}");
        sb.AppendLine($"Duration: {video.Duration}");
        return sb.ToString();
    }
    public static string Describe(this PlaylistVideo video)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Title: {video.Title}");
        sb.AppendLine($"Author: {video.Author}");
        sb.AppendLine($"Duration: {video.Duration}");
        return sb.ToString();
    }
}
