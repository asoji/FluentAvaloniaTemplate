using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Metadata;
using CommunityToolkit.Mvvm.ComponentModel;
using FluentAvalonia.UI.Controls;
using FluentAvaloniaTemplate.Views;

namespace FluentAvaloniaTemplate.ViewModels;

public partial class MainViewModel : ViewModelBase {
    private object _selectedItem;
    private Control _currentPage = new HomePageView();
    private object _headerName;

    [ObservableProperty]
    private string _paneName = "";

    public MainViewModel() {
        MenuItems = new List<ItemBase>();

        MenuItems.Add(new Item {
            Name = "Home",
            ViewName = "HomePageView",
            ViewType = typeof(HomePageViewModel),
            Icon = Symbol.Home,
            ToolTip = "Welcome home"
        });
        MenuItems.Add(new Seperator());
        MenuItems.Add(new Header { Name = "Example Group" });
        MenuItems.Add(new Item {
            Name = "Example Page 1",
            ViewName = "ExamplePage1View",
            ViewType = typeof(ExamplePage1ViewModel),
            Icon = Symbol.Page,
            ToolTip = "go, do a crime"
        });
        MenuItems.Add(new Item {
            Name = "Example Page 2",
            ViewName = "ExamplePage2View",
            ViewType = typeof(ExamplePage2ViewModel),
            Icon = Symbol.Clipboard,
            ToolTip = "go, become the NSA"
        });
        MenuItems.Add(new Header { Name = "Example Group 2" });
        MenuItems.Add(new Item {
            Name = "Example Page 3",
            ViewName = "ExamplePage3View",
            ViewType = typeof(ExamplePage3ViewModel),
            Icon = Symbol.Keyboard
        });

        SelectedItem = MenuItems[0];
    }

    public List<ItemBase> MenuItems { get; }


    public object SelectedItem {
        get => _selectedItem;
        set {
            SetProperty(ref _selectedItem, value);
            SetCurrentPage();
        }
    }

    public Control CurrentPage {
        get => _currentPage;
        set => SetProperty(ref _currentPage, value);
    }

    public object HeaderName {
        get => _headerName;
        set => SetProperty(ref _headerName, value);
    }

    private void SetCurrentPage() {
        if (SelectedItem is Item cat) {
            var pageName = $"FluentAvaloniaTemplate.Views.{cat.ViewName}";
            var page = Activator.CreateInstance(Type.GetType(pageName)!)!;
            (page as Control).DataContext = cat.ViewType != null ? Activator.CreateInstance(cat.ViewType)! : this;
            CurrentPage = page as Control;
            HeaderName = cat.Name;
        }

        else if (SelectedItem is NavigationViewItem nvi) {
            var page = Activator.CreateInstance(Type.GetType($"FluentAvaloniaTemplate.Views.SettingsView"));
            (page as Control).DataContext = Activator.CreateInstance(typeof(SettingsViewModel))!; // i hate this
            CurrentPage = page as Control;
            HeaderName = "Settings";
        }
    }
}

public abstract class ItemBase { }

public class Item : ItemBase {
    public required string Name { get; set; }
    public required string ViewName { get; set; }
    public required Type ViewType { get; set; }
    public string? ToolTip { get; set; }
    public required Symbol Icon { get; set; }
}

public class Seperator : ItemBase { }

public class Header : ItemBase {
    public string Name { get; set; }
}

public class MenuItemTemplateSelector : DataTemplateSelector {
    [Content]
    public IDataTemplate ItemTemplate { get; set; }
    public IDataTemplate SeperatorTemplate { get; set; }
    public IDataTemplate HeaderTemplate { get; set; }

    protected override IDataTemplate SelectTemplateCore(object item) {
        return item is Seperator ? SeperatorTemplate
            : item is Header ? HeaderTemplate
            : ItemTemplate;
    }
}