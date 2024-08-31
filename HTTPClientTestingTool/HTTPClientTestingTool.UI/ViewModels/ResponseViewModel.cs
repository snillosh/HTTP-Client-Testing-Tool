namespace HTTPClientTestingTool.UI.ViewModels;

class ResponseViewModel : ViewModelBase
{
    public ResponseViewModel()
    {
        Output = string.Empty;
    }

    public void GetResponse(object sender, string response)
    {
        Output = response;
    }

    private string _output;

    public string Output
    {
        get => _output;
        set
        {
            _output = value;
            OnPropertyChanged();
        }
    }
}
