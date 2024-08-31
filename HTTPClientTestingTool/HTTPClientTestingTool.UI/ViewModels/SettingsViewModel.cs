using HTTPClientTestingTool.UI.Utilities;
using System.Windows.Input;

namespace HTTPClientTestingTool.UI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            SaveSettingsCommand = new RelayCommand(SaveSettingsAction);
        }

        private void SaveSettingsAction(object obj)
        {
            UserSettings.Default.Save();
        }

        private string _defaultUri;

        public string DefaultUri
        {
            get => UserSettings.Default.DefaultUri;
            set
            {
                UserSettings.Default.DefaultUri = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveSettingsCommand { get; }

    }
}
