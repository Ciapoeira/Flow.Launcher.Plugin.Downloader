using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Flow.Launcher.Plugin.Downloader;

public partial class Settings : ObservableObject {
    public readonly string[] supportedPresets = ["mp3", "aac", "mp4", "mkv"];
    public readonly string[] supportedBrowsers = ["", "brave", "chrome", "chromium", "edge", "firefox", "opera", "safari", "vivaldi", "whale"];
    public readonly string[] supportedRuntimes = ["", "deno", "node", "bun", "quickjs"];

    [ObservableProperty] private bool _copyToClipboard = false;
    [ObservableProperty] private bool _silent = false;
    [ObservableProperty] private bool _usePresets = true;
    [ObservableProperty] private string _concurrentFragments = "1";
    [ObservableProperty] private string _downloadDir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
    [ObservableProperty] private string _exe = "yt-dlp";
    [ObservableProperty] private string _fileName = "%(title)s [%(id)s].%(ext)s";
    [ObservableProperty] private string _runtime = "";
    [ObservableProperty] private string _browser = "";

    public List<string> Args => GetArgs();

    private List<string> GetArgs() {
        List<string> args = [
            "--output", Path.Combine(DownloadDir, FileName),
            "--no-playlist",
            "-N", ConcurrentFragments
        ];

        if (Runtime != null)
            args.AddRange(["--js-runtimes", Runtime,]);

        if (Browser != null)
            args.AddRange(["--cookies-from-browser", Browser]);

        if (CopyToClipboard)
            args.AddRange(["--exec", "powershell -NoProfile -Command Set-Clipboard -LiteralPath '{}'"]);

        return args;
    }
}
