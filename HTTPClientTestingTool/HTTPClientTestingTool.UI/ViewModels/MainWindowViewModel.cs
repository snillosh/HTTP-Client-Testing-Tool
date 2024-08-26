namespace HTTPClientTestingTool.UI.ViewModels;

class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Content = new InputViewModel();
    }

    public string Title => "HTTP Tester";

    private ViewModelBase _content;

    public ViewModelBase Content
    {
        get => _content;
        set
        {
            _content = value;
            OnPropertyChanged(nameof(Content));
        }
    }
}
