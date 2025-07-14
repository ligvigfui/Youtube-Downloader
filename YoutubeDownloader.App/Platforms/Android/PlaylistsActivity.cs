using Android.OS;

namespace YoutubeDownloader.Platforms.Android;

[Activity(Name = "myamazing.shareactivity", Exported = true, LaunchMode = LaunchMode.SingleTop)]
[IntentFilter(["android.intent.action.SEND"],
    Categories = ["android.intent.category.DEFAULT"],
    DataMimeType = "text/plain")]
class PlaylistsActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState); //throws Exception  

        var uri = Intent?.Data;
    }
}
