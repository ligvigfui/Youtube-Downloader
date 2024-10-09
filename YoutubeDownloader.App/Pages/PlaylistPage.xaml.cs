namespace YoutubeDownloader.Pages;

public partial class PlaylistPage : ContentPage
{
    public PlaylistPage(PlaylistViewModel playlistViewModel)
	{
		InitializeComponent();
        BindingContext = playlistViewModel;
    }
}