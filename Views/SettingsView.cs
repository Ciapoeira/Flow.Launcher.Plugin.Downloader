using System.Windows;
using System.Windows.Controls;
using static Flow.Launcher.Plugin.Downloader.Helpers.UI;
using Flow.Launcher.Plugin.Downloader.Helpers;

namespace Flow.Launcher.Plugin.Downloader.Views;

public class SettingsView : UserControl {
    internal StackPanel Layout = new() { Margin = new Thickness(15) };

    private void AddSetting(string label, FrameworkElement control, Button? button = null) {
        var wrapper = new StackPanel { };

        if (label != "")
            wrapper.Children.Add(new TextBlock {
                Text = label,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 5)
            });

        var inputRow = new DockPanel();
        if (button != null) {
            button.Width = 80;
            DockPanel.SetDock(button, Dock.Right);
            inputRow.Children.Add(button);
        }

        inputRow.Children.Add(control);
        wrapper.Children.Add(inputRow);

        Layout.Children.Add(wrapper);
    }

    public SettingsView(Settings Settings) {
        AddSetting("Path to yt-dlp", CreateTextBox(Settings, nameof(Settings.Exe), false),
            CreateButton("Browse", (s, e) => Settings.Exe = BrowseForFile() ?? Settings.Exe));
        AddSetting("Download Directory", CreateTextBox(Settings, nameof(Settings.DownloadDir), false),
            CreateButton("Browse", (s, e) => Settings.DownloadDir = BrowseForFolder() ?? Settings.DownloadDir));
        AddSetting("File Name", CreateTextBox(Settings, nameof(Settings.FileName), true));
        AddSetting("Browser (Empty to disable)", CreateComboBox(Settings, Settings.supportedBrowsers, nameof(Settings.Browser)));
        AddSetting("JS Runtime (Empty to disable)", CreateComboBox(Settings, Settings.supportedRuntimes, nameof(Settings.Runtime)));
        AddSetting("Number of concurrent fragments", CreateComboBox(Settings, ["1", "2", "3", "4", "5"], nameof(Settings.ConcurrentFragments)));
        AddSetting("", CreateCheckBox(Settings, "Use presets", nameof(Settings.UsePresets)));
        AddSetting("", CreateCheckBox(Settings, "Silent Mode", nameof(Settings.Silent)));
        AddSetting("", CreateCheckBox(Settings, "Copy to clipboard", nameof(Settings.CopyToClipboard)));
        AddSetting("", CreateButton("Update", async (s, e) => await Ytdlp.Update(Settings.Exe)));

        Content = Layout;
    }
}
