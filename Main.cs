using static Flow.Launcher.Plugin.Downloader.Helpers.Ytdlp;
using System.Windows.Controls;
using Flow.Launcher.Plugin.Downloader.Views;

namespace Flow.Launcher.Plugin.Downloader;

public class Main : IAsyncPlugin, ISettingProvider, IContextMenu {
  private PluginInitContext? Context;
  private ContextMenu? ContextMenu;
  private Settings Settings = new();

  private Result? NewResult;
  private string PendingUrl = "";

  public Control CreateSettingPanel() {
    return new SettingsView(Settings);
  }

  public Task InitAsync(PluginInitContext context) {
    Settings = context.API.LoadSettingJsonStorage<Settings>();

    Context = context;

    ContextMenu = new(Settings);

    return Task.CompletedTask;
  }

  public List<Result> LoadContextMenus(Result selectedResult) {
    return ContextMenu!.LoadContextMenus(selectedResult);
  }

  public async Task<List<Result>> QueryAsync(Query query, CancellationToken token) {
    if (string.IsNullOrWhiteSpace(query.Search)) return [];

    await Task.Delay(200, token);

    var url = query.Search.Trim();

    var Result = new Result {
      Title = "Fetching metadata...",
      SubTitle = url,
      IcoPath = "Resources/download.png",
      AsyncAction = async _ => {
        await DownloadVideoAsync(Settings.Exe, Settings.Args, url, Settings.Silent);
        return true;
      }
    };

    if (url != PendingUrl) {
      PendingUrl = url;
      NewResult = null;

      if (Uri.IsWellFormedUriString(url, UriKind.Absolute)) {
        _ = PrepareVideoMetadata(url, token);
      }
    }

    return (NewResult != null && PendingUrl == url) ? [NewResult] : [Result];
  }

  private async Task PrepareVideoMetadata(string url, CancellationToken token) {
    var video = await GetVideoMetadataAsync(Settings.Exe, url, token);

    if (url != PendingUrl || video == null) return;

    NewResult = new() {
      Title = video?.Title,
      ContextData = (url, video?.Formats),
      IcoPath = video?.Thumbnail,
      AsyncAction = async _ => {
        await DownloadVideoAsync(Settings.Exe, Settings.Args, url, Settings.Silent);

        return true;
      }
    };

    Context!.API.ReQuery();
  }
};
