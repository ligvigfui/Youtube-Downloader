namespace YoutubeDownloader.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    string? downloadPath;
    public static string GlobalDownloadPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

    [RelayCommand]
    public async Task SelectFolder()
    {
        var result = await FolderPicker.PickAsync(DownloadPath ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        if (result == null || !result.IsSuccessful)
            return;
        DownloadPath = result.Folder.Path;
        GlobalDownloadPath = result.Folder.Path;
    }
}
