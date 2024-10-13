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
        
        // Ensure the directory exists
        Directory.CreateDirectory(SettingsViewModel.GlobalDownloadPath);

        // Combine the directory path and file name
        var filePath = Path.Combine(SettingsViewModel.GlobalDownloadPath, fileName);

        // Save the file directly to the specified path
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        
        await stream.CopyToAsync(fileStream);

        return fileName;
    }
}