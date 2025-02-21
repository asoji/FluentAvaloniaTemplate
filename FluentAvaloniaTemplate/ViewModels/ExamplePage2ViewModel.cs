using CommunityToolkit.Mvvm.ComponentModel;

namespace FluentAvaloniaTemplate.ViewModels;

public partial class ExamplePage2ViewModel : ViewModelBase {
    [ObservableProperty] private string _greetingText = "Howdy! This is Example Page 2";
}