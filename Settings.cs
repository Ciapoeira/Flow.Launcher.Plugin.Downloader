using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Flow.Launcher.Plugin.Downloader;

public partial class Settings : ObservableObject {
  public readonly string[] supportedPresets = ["mp3", "aac", "mp4", "mkv"];
  public readonly string[] supportedBrowsers = ["disabled", "brave", "chrome", "chromium", "edge", "firefox", "opera", "safari", "vivaldi", "whale"];
  public readonly string[] supportedRuntimes = ["disabled", "deno", "node", "bun", "quickjs"];

  [ObservableProperty] private bool _copyToClipboard = false;
  [ObservableProperty] private bool _silent = false;
  [ObservableProperty] private bool _usePresets = true;
  [ObservableProperty] private string _concurrentFragments = "3";
  [ObservableProperty] private string _downloadDir = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);
  [ObservableProperty] private string _exe = "yt-dlp";
  [ObservableProperty] private string _fileName = "%(title)s [%(id)s].%(ext)s";
  [ObservableProperty] private string _runtime = "disabled";
  [ObservableProperty] private string _browser = "disabled";

  public List<string> Args => GetArgs();

  private List<string> GetArgs() {
    List<string> args = [
        "--output", Path.Combine(DownloadDir, FileName),
            "--no-playlist",
            "-N", ConcurrentFragments
    ];

    if (Runtime != "disabled")
      args.AddRange(["--js-runtimes", Runtime,]);

    if (Browser != "disabled")
      args.AddRange(["--cookies-from-browser", Browser]);

    if (CopyToClipboard)
      args.AddRange(["--exec", "powershell -NoProfile -Command Set-Clipboard -LiteralPath '{}'"]);

    return args;
  }
}
