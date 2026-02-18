using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Flow.Launcher.Plugin.Downloader;

public partial class Settings : ObservableObject {
    public readonly string[] supportedPresets = ["mp3", "aac", "mp4", "mkv"];
    public readonly string[] supportedBrowsers = ["", "brave", "chrome", "chromium", "edge", "firefox", "opera", "safari", "vivaldi", "whale"];
    public readonly string[] supportedRuntimes = ["", "deno", "node", "bun", "quickjs"];

    // ? Should it be forced? Using a plugin for a whole playlists seems dumb
    [ObservableProperty] private bool _disablePlaylist = true;
    [ObservableProperty] private bool _silent = false;
    [ObservableProperty] private bool _usePresets = false;
    [ObservableProperty] private string _downloadDir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
    [ObservableProperty] private string _exe = "yt-dlp";
    [ObservableProperty] private string _fileName = "%(title)s [%(id)s].%(ext)s";
    [ObservableProperty] private string _runtime = "";
    [ObservableProperty] private string? _browser = null;

    public List<string> Args => GetArgs();

    private List<string> GetArgs() {
        List<string> args = [
            "--output", Path.Combine(DownloadDir, FileName)
        ];

        if (!string.IsNullOrEmpty(Runtime))
            args.AddRange(["--js-runtimes", Runtime,]);

        if (!string.IsNullOrEmpty(Browser))
            args.AddRange(["--cookies-from-browser", Browser]);

        if (DisablePlaylist)
            args.Add("--no-playlist");

        // ! For safety (otherwise requires task manager)
        if (Silent && !DisablePlaylist)
            args.Add("--no-playlist");

        return args;
    }
}
