namespace YoutubeDownloader.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    async Task DownloadFromPlaylist()
    {
        try
        {
            Console.WriteLine("Enter the playlist URL:");
            var playlistUrl = Console.ReadLine();
            var playlist = await Youtube.Playlists.GetAsync(playlistUrl);
            var videos = await Youtube.Playlists.GetVideosAsync(playlist.Id).CollectAsync();
            for (int i = 0; i < playlistDownloadOptions.Count; i++)
            {
                Console.WriteLine($"{i}. {playlistDownloadOptions[i]}");
            }
            var downloadMethod = Console.ReadKey().Key;
            Console.WriteLine();
            Range range = downloadMethod switch
            {
                ConsoleKey.D0 or
                ConsoleKey.NumPad0 =>
                    0..videos.Count,
                ConsoleKey.D1 or
                ConsoleKey.NumPad1 =>
                    GetRange(videos.Count),
                _ => throw new NotImplementedException()
            };
            foreach (var video in videos.ToList()[range])
            {
                var downloadedFileName = await Youtube.Download(video, downloadFolder);
                Console.WriteLine($"Downloaded {downloadedFileName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred {ex.Message}");
        }
    }

    Range GetRange(int count)
    {
        Console.WriteLine("Enter the start index:");
        var start = int.Parse(Console.ReadLine() ?? "0");
        return start..count;
    }

    async Task DownloadVideo()
    {
        try
        {
            Console.WriteLine("Enter the video URL:");
            var videoUrl = Console.ReadLine();
            var video = await Youtube.Videos.GetAsync(videoUrl);
            var downloadedFileName = await Youtube.Download(video, downloadFolder);
            Console.WriteLine($"Downloaded {downloadedFileName}");
        }
        catch
        {
            Console.WriteLine("An error occurred");
        }
    }
}
