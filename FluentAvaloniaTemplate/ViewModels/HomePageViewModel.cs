using CommunityToolkit.Mvvm.ComponentModel;

namespace FluentAvaloniaTemplate.ViewModels;

public partial class HomePageViewModel : ViewModelBase {
    [ObservableProperty] private string _greetingText = "Howdy!";
}