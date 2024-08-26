namespace HTTPClientTestingTool.UI.ViewModels;

class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel(InputViewModel inputViewModel, OutputViewModel outputViewModel)
    {
        UpperContent = inputViewModel;
        LowerContent = outputViewModel;

        UpperContent.ResponseChanged += LowerContent.GetResponse;
    }

    public string Title => "HTTP Tester";

    private InputViewModel _upperContent;

    public InputViewModel UpperContent
    {
        get => _upperContent;
        set
        {
            _upperContent = value;
            OnPropertyChanged(nameof(UpperContent));
        }
    }

    private OutputViewModel _lowerContent;

    public OutputViewModel LowerContent
    {
        get { return _lowerContent; }
        set { _lowerContent = value; }
    }

}
