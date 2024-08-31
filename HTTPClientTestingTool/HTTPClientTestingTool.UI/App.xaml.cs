using ControlzEx.Theming;
using HTTPClientTestingTool.UI.ViewModels;
using HTTPClientTestingTool.UI.Views;
using System.Windows;

namespace HTTPClientTestingTool.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private RequestViewModel _inputViewModel;

    public App() => _inputViewModel = new RequestViewModel();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ThemeManager.Current.ChangeTheme(this, "Dark.Green");

        // Create and configure the MainWindow
        var mainWindowViewModel = new MainWindowViewModel(_inputViewModel);
        var mainWindow = new MainWindow
        {
            DataContext = mainWindowViewModel
        };

        // Show the MainWindow
        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        base.OnExit(e);

        _inputViewModel.Dispose();
    }
}
