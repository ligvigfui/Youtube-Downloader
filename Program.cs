YoutubeClient Youtube = new();
List<string> availableActions =
[
    "Get video information",
    "Get playlist information",
    "Download video",
    "Download from playlist",
    "Exit"
];

List<string> playlistDownloadOptions =
[
    "Download all videos",
    "Download after specific video",
    "Download specified range"
];

Console.WriteLine("Enter the download folder:");
var downloadFolder = Console.ReadLine();
if (string.IsNullOrWhiteSpace(downloadFolder))
    // get the default windows download folder
    downloadFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";

while (true)
{
    Console.WriteLine("Choose an action:");
    for (int i = 0; i < availableActions.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {availableActions[i]}");
    }

    var key = Console.ReadKey().Key;
    Console.Clear();
    switch (key)
    {
        case ConsoleKey.NumPad1:
        case ConsoleKey.D1:
            await GetVideoInformation();
            break;
        case ConsoleKey.NumPad2:
        case ConsoleKey.D2:
            await GetPlaylistInformation();
            break;
        case ConsoleKey.NumPad3:
        case ConsoleKey.D3:
            await DownloadVideo();
            break;
        case ConsoleKey.NumPad4:
        case ConsoleKey.D4:
            await DownloadFromPlaylist();
            break;
        case ConsoleKey.NumPad5:
        case ConsoleKey.D5:
            return;
        default:
            Console.WriteLine("Invalid input");
            break;
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
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
                AfterVideoToEnd(videos.Count),
            ConsoleKey.D2 or
            ConsoleKey.NumPad2 =>
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

Range AfterVideoToEnd(int count)
{
    Console.WriteLine("Enter the start index:");
    var start = int.Parse(Console.ReadLine() ?? "0");
    return start..count;
}

Range GetRange(int count)
{
    Console.WriteLine("Enter the range:");
    Console.WriteLine("Format: start?..end?");
    var rangeInput = Console.ReadLine();
    var parts = rangeInput?.Split("..") ?? [];
    var start = string.IsNullOrWhiteSpace(parts.ElementAtOrDefault(0)) ? 0 : int.Parse(parts[0]);
    var end = string.IsNullOrWhiteSpace(parts.ElementAtOrDefault(1)) ? count : int.Parse(parts[1]);
    return start..end;
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

async Task GetPlaylistInformation()
{
    try
    {
        Console.WriteLine("Enter the playlist URL:");
        var playlistUrl = Console.ReadLine();
        var playlist = await Youtube.Playlists.GetAsync(playlistUrl);
        var videos = Youtube.Playlists.GetVideosAsync(playlist.Id);
        var videoCount = 0;
        await foreach (var video in videos)
        {
            Console.WriteLine($"Video {videoCount++}");
            Console.WriteLine(video.Describe());
            Console.WriteLine("-----------");
        }
    }
    catch
    {
        Console.WriteLine("An error occurred");
    }
}

async Task GetVideoInformation()
{
    try
    {
        Console.WriteLine("Enter the video URL:");
        var videoUrl = Console.ReadLine();
        var video = await Youtube.Videos.GetAsync(videoUrl);
        Console.WriteLine(video.Describe());
    }
    catch
    {
        Console.WriteLine("An error occurred");
    }
}