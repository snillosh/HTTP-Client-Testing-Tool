using HTTPClientTestingTool.UI.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace HTTPClientTestingTool.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        var mainWindowViewModel = new MainWindowViewModel();

        var mainWindow = new MainWindow
        {
            DataContext = mainWindowViewModel
        };

        mainWindow.Show();
    }
}
