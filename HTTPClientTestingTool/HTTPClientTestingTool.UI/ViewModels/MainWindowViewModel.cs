namespace HTTPClientTestingTool.UI.ViewModels;

class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(InputViewModel inputViewModel)
    {
        UpperContent = inputViewModel;
    }

    public string Title => "HTTP Tester";

    private ViewModelBase _upperContent;

    public ViewModelBase UpperContent
    {
        get => _upperContent;
        set
        {
            _upperContent = value;
            OnPropertyChanged(nameof(UpperContent));
        }
    }
}
