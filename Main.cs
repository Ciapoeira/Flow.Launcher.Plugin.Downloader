using static Flow.Launcher.Plugin.Downloader.Helpers.Ytdlp;
using System.Windows.Controls;
using Flow.Launcher.Plugin.Downloader.Views;

namespace Flow.Launcher.Plugin.Downloader;

public class Main : IAsyncPlugin, ISettingProvider, IContextMenu {
    internal Settings Settings;

    internal ContextMenu ContextMenu;

    public Control CreateSettingPanel() {
        return new SettingsView(Settings);
    }

    public Task InitAsync(PluginInitContext context) {
        Settings = context.API.LoadSettingJsonStorage<Settings>();

        ContextMenu = new(Settings);

        return Task.CompletedTask;
    }

    public List<Result> LoadContextMenus(Result selectedResult) {
        return ContextMenu.LoadContextMenus(selectedResult);
    }

    public async Task<List<Result>> QueryAsync(Query query, CancellationToken token) {
        if (string.IsNullOrWhiteSpace(query.Search)) return [];

        try {
            await Task.Delay(200, token);

            var url = query.Search.Trim();

            var video = await GetVideoMetadataAsync(Settings.Exe, url, token);

            return [
                new Result {
                    Title = video?.Title,
                    ContextData = url,
                    AsyncAction = async c => {
                        await DownloadVideoAsync(Settings.Exe, Settings.Args, url, Settings.Silent);

                        return true;
                    },
                    IcoPath = video?.Thumbnail ?? "Resources/download.png",
                }
            ];
        } catch (OperationCanceledException) {
            return [];
        }
    }
};
