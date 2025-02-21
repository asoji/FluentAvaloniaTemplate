using FluentAvalonia.UI.Windowing;

namespace FluentAvaloniaTemplate.Views;

public partial class MainWindow : AppWindow {
    public MainWindow() {
        TitleBar.ExtendsContentIntoTitleBar = false;
        InitializeComponent();
    }
}