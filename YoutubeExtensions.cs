using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader;

public static class YoutubeExtensions
{
    public static async Task<string> Download(this YoutubeClient youtube, IVideo video, string downloadFolder)
    {
        var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(video.Id);
        var streamInfo = streamInfoSet.GetAudioOnlyStreams().GetWithHighestBitrate();
        var validFileNameTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{validFileNameTitle}.mp3";
        var filePath = Path.Combine(downloadFolder, fileName);
        await youtube.Videos.Streams.DownloadAsync(streamInfo, filePath);
        return fileName;
    }
}