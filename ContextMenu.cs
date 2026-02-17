using static Flow.Launcher.Plugin.Downloader.Helpers.Ytdlp;

namespace Flow.Launcher.Plugin.Downloader;

public class ContextMenu(Settings Settings) : IContextMenu {

  public List<Result> LoadContextMenus(Result selectedResult) {
    List<Result> contextMenus = [];

    var url = selectedResult.ContextData.ToString()!;

    foreach (var preset in Settings.supportedPresets) {
      contextMenus.Add(new Result {
        Title = selectedResult.Title,
        SubTitle = preset,
        AsyncAction = async c => {
          await DownloadVideoAsync(Settings.Exe, [.. Settings.Args, "-t", preset], url, Settings.Silent);

          return true;
        },
        IcoPath = selectedResult.IcoPath,
      });
    }
    return contextMenus;
  }
}
