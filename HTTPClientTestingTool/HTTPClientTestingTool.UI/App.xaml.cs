using HTTPClientTestingTool.UI.ViewModels;
using HTTPClientTestingTool.UI.Views;
using System.Windows;

namespace HTTPClientTestingTool.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private InputViewModel _inputViewModel;

    public App() => _inputViewModel = new InputViewModel();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var outputViewModel = new OutputViewModel();

        // Create and configure the MainWindow
        var mainWindowViewModel = new MainWindowViewModel(_inputViewModel, outputViewModel);
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
