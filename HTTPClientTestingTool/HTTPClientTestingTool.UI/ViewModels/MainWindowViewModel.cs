
using HTTPClientTestingTool.UI.Utilities;
using MahApps.Metro.IconPacks;
using System.Collections.ObjectModel;

namespace HTTPClientTestingTool.UI.ViewModels;

class MainWindowViewModel : ViewModelBase
{
    private readonly RequestViewModel _inputViewModel;
    public MainWindowViewModel(RequestViewModel inputViewModel)
    {
        _inputViewModel = inputViewModel;

        Menu = new ObservableCollection<MenuItem>
        {
            new MenuItem
                    {
                      Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.AccessPointNetwork },
                      Label = "HTTP Request / Response",
                      Command = new RelayCommand(HttpButtonClicked),
                    },
            new MenuItem
                    {
                      Icon = new PackIconForkAwesome() { Kind = PackIconForkAwesomeKind.Cog},
                      Label = "Settings",
                      Command = new RelayCommand(SettingsButtonClicked),
                    },
        };

        Content = _inputViewModel;
    }

    private void HttpButtonClicked(object obj)
    {
        Content = _inputViewModel;
    }

    private void SettingsButtonClicked(object obj)
    {
        Content = new SettingsViewModel();
    }

    public string Title => "HTTP Client Test Tool";

    private ViewModelBase _content;

    public ViewModelBase Content
    {
        get => _content;
        set
        {
            _content = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<MenuItem> _menu;

    public ObservableCollection<MenuItem> Menu
    {
        get => _menu;
        set
        {
            _menu = value;
            OnPropertyChanged();
        }
    }

}
