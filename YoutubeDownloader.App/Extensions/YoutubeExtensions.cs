using YoutubeExplode.Videos.Streams;

namespace YoutubeDownloader.Extensions;

public static class YoutubeExtensions
{
    public static async Task<string> Download(this YoutubeClient youtube, IVideo video)
    {
        var streamInfoSet = await youtube.Videos.Streams.GetManifestAsync(video.Id);
        var streamInfo = streamInfoSet.GetAudioOnlyStreams().GetWithHighestBitrate();
        var validFileNameTitle = string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{validFileNameTitle}.mp3";
        var stream = await youtube.Videos.Streams.GetAsync(streamInfo);
        var result = await FileSaver.Default.SaveAsync(SettingsViewModel.GlobalDownloadPath, fileName, stream);
        result.EnsureSuccess();
        return fileName;
    }
}