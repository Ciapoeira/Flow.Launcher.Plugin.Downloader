using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Microsoft.Win32;

namespace Flow.Launcher.Plugin.Downloader.Helpers;

public static class UI {
  // UI Helpers
  static public TextBox CreateTextBox(Settings settings, string propertyName, bool isEnabled = true) {
    var tb = new TextBox() { IsEnabled = isEnabled };
    tb.SetBinding(TextBox.TextProperty, new Binding(propertyName) {
      Source = settings,
      UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
      Mode = BindingMode.TwoWay,
    });
    return tb;
  }

  static public CheckBox CreateCheckBox(Settings settings, string content, string propertyName) {
    var cb = new CheckBox { Content = content };
    cb.SetBinding(CheckBox.IsCheckedProperty, new Binding(propertyName) {
      Source = settings,
      Mode = BindingMode.TwoWay
    });
    return cb;
  }

  static public Button CreateButton(string content, RoutedEventHandler onClick) {
    var btn = new Button { Content = content };
    btn.Click += onClick;
    return btn;
  }

  public static ComboBox CreateComboBox(Settings settings, string[] itemsSource, string selectedItemProperty) {
    var combo = new ComboBox {
      ItemsSource = itemsSource
    };

    combo.SetBinding(Selector.SelectedItemProperty, new Binding(selectedItemProperty) {
      Source = settings,
      Mode = BindingMode.TwoWay
    });

    return combo;
  }


  // Functionality
  static public string BrowseForFile() {
    var dialog = new OpenFileDialog { Filter = "Executable Files (*.exe)|*.exe|All files (*.*)|*.*" };
    return dialog.ShowDialog() == true ? dialog.FileName : "";
  }

  static public string BrowseForFolder() {
    var dialog = new OpenFolderDialog { Title = "Select Download Directory" };
    return dialog.ShowDialog() == true ? dialog.FolderName : "";
  }
}
