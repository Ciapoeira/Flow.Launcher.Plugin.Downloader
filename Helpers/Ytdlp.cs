using System.Text.Json;
using static Flow.Launcher.Plugin.Downloader.Helpers.Cmd;

namespace Flow.Launcher.Plugin.Downloader.Helpers;

public record Video(string Title = "", string Thumbnail = "");

public static class Ytdlp {
    private static readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public static async Task DownloadVideoAsync(string exe, List<string> args, string url, bool silent, CancellationToken token = default) {
        await ExecuteAsync(exe, [.. args, url], silent, false, token);
    }

    public static async Task<Video?> GetVideoMetadataAsync(string exe, string url, CancellationToken token = default) {
        List<string> args = ["--no-warnings", "--dump-json", "--no-playlist", url];

        var json = await ExecuteAsync(exe, args, true, true, token);
        return string.IsNullOrWhiteSpace(json) ? null : JsonSerializer.Deserialize<Video>(json, options);
    }
}
