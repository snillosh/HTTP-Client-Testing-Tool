
using HTTPClientTestingTool.UI.Utilities;
using System.Windows.Input;

namespace HTTPClientTestingTool.UI.ViewModels;

class MainWindowViewModel : ViewModelBase
{
    private readonly OutputViewModel _outputViewModel;
    private readonly InputViewModel _inputViewModel;
    public MainWindowViewModel(InputViewModel inputViewModel, OutputViewModel outputViewModel)
    {
        _inputViewModel = inputViewModel;
        _outputViewModel = outputViewModel;

        UpperContent = _inputViewModel;
        LowerContent = _outputViewModel;

        SettingsButtonCommand = new RelayCommand(SettingsButtonClicked);

        _inputViewModel.ResponseChanged += _outputViewModel.GetResponse;
    }

    private void SettingsButtonClicked(object obj)
    {
        LowerContent = LowerContent.GetType() == typeof(OutputViewModel) ? new SettingsViewModel() : _outputViewModel;

        OnPropertyChanged(nameof(SettingsButtonContent));
    }

    public string Title => "HTTP Client Test Tool";

    private ViewModelBase _upperContent;

    public ViewModelBase UpperContent
    {
        get => _upperContent;
        set
        {
            _upperContent = value;
            OnPropertyChanged();
        }
    }

    private ViewModelBase _lowerContent;

    public ViewModelBase LowerContent
    {
        get => _lowerContent;
        set
        {
            _lowerContent = value;
            OnPropertyChanged();
        }
    }

    private string _settingsButtonContent;

    public string SettingsButtonContent
    {
        get
        {
            return LowerContent.GetType() == typeof(OutputViewModel) ? Strings.SettingsButton_Settings : Strings.SettingsButton_Home;
        }
    }


    public ICommand SettingsButtonCommand { get; }
}
