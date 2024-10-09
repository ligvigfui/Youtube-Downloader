namespace YoutubeDownloader;

public static class PlaylistExtenstions
{
    public static string Describe(this Playlist playlist)
    {
        var description = new StringBuilder();
        description.AppendLine($"Title: {playlist.Title}");
        description.AppendLine($"Author: {playlist.Author}");
        description.AppendLine($"Description: {playlist.Description}");
        description.AppendLine($"Video count: {playlist.Count}");
        return description.ToString();
    }
}
