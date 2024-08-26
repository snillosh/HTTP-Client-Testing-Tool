using HTTPClientTestingTool.UI.ViewModels;
using HTTPClientTestingTool.UI.Views;
using System.Windows;

namespace HTTPClientTestingTool.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Create and configure the MainWindow
        var mainWindowViewModel = new MainWindowViewModel();
        var mainWindow = new MainWindow
        {
            DataContext = mainWindowViewModel
        };

        // Show the MainWindow
        mainWindow.Show();
    }
}
