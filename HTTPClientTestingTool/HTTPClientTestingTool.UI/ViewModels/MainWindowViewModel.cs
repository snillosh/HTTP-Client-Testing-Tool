
using HTTPClientTestingTool.UI.Utilities;
using MahApps.Metro.IconPacks;
using System.Windows.Input;

namespace HTTPClientTestingTool.UI.ViewModels;

class MainWindowViewModel : ViewModelBase
{
    private readonly ResponseViewModel _outputViewModel;
    private readonly RequestViewModel _inputViewModel;
    public MainWindowViewModel(RequestViewModel inputViewModel, ResponseViewModel outputViewModel)
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
        LowerContent = LowerContent.GetType() == typeof(ResponseViewModel) ? new SettingsViewModel() : _outputViewModel;

        OnPropertyChanged(nameof(SettingsButtonContent));
        OnPropertyChanged(nameof(SettingsButtonTooltip));
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

    private PackIconModernKind _settingsButtonContent;

    public PackIconModernKind SettingsButtonContent => LowerContent.GetType() == typeof(ResponseViewModel) ? PackIconModernKind.Settings : PackIconModernKind.Home;

    private string _settingsButtonTooltip;

    public string SettingsButtonTooltip => LowerContent.GetType() == typeof(ResponseViewModel) ? "Settings" : "Home";



    public ICommand SettingsButtonCommand { get; }
}
