using CommunityToolkit.Mvvm.ComponentModel;

namespace FluentAvaloniaTemplate.ViewModels;

public partial class ExamplePage3ViewModel : ViewModelBase {
    [ObservableProperty] private string _greetingText = "Howdy! This is Example Page 3";
}