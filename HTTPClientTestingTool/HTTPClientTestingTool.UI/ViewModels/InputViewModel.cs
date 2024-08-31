using HTTPClientTestingTool.Data;
using HTTPClientTestingTool.UI.Utilities;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace HTTPClientTestingTool.UI.ViewModels;

public sealed class InputViewModel : ViewModelBase, IDisposable
{
    private readonly HttpClient httpClient;

    public InputViewModel()
    {
        SelectedMethod = EHttpMethods.Get;

        SendRequestCommand = new RelayCommand(SendRequestAction, CanSendRequestCommand);

        httpClient = new HttpClient();

        URL = "https://localhost:7061/";
    }

    private async void SendRequestAction(object obj)
    {
        if (SelectedMethod != EHttpMethods.Get && SelectedMethod != EHttpMethods.Delete)
        {
            if (!IsJsonValid())
            {
                _ = MessageBox.Show("Invalid JSON. Please check and try again");
                return;
            }
        }


        try
        {
            var content = new StringContent(RequestBody, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = SelectedMethod switch
                {
                    EHttpMethods.Get => HttpMethod.Get,
                    EHttpMethods.Post => HttpMethod.Post,
                    EHttpMethods.Put => HttpMethod.Put,
                    EHttpMethods.Delete => HttpMethod.Delete,
                    _ => throw new InvalidOperationException("Unsupported HTTP method.")
                },

                RequestUri = new Uri(URL),
                Content = content
            };

            // Set headers on the request
            var headers = Headers.Split(",");
            foreach (var split in headers)
            {
                var sections = split.Split(":");
                if (sections.Length == 2)
                {
                    request.Headers.Add(sections[0].Trim(), sections[1].Trim());
                }
            }

            var response = await httpClient.SendAsync(request);

            ResponseBody = await response.Content.ReadAsStringAsync();
        }
        catch (Exception ex)
        {
            ResponseBody = $"Error: {ex.Message}";
        }
    }

    private bool CanSendRequestCommand(object obj)
    {
        return true;
    }

    public EventHandler<string> ResponseChanged;

    private string _url;

    public string URL
    {
        get => _url;
        set
        {
            _url = value;
            OnPropertyChanged();
        }
    }

    private string _headers = string.Empty;

    public string Headers { get => _headers; set { _headers = value; OnPropertyChanged(); } }

    private string _requestBody = string.Empty;

    public string RequestBody { get => _requestBody; set { _requestBody = value; OnPropertyChanged(); } }

    private EHttpMethods _selectedMethod;

    public EHttpMethods SelectedMethod
    {
        get => _selectedMethod;
        set
        {
            _selectedMethod = value;
            OnPropertyChanged(nameof(SelectedMethod));
        }
    }

    public IEnumerable<EHttpMethods> Methods
    {
        get
        {
            return Enum.GetValues(typeof(EHttpMethods))
                .Cast<EHttpMethods>();
        }
    }

    public ICommand SendRequestCommand { get; }

    private string _responseBody = string.Empty;

    public string ResponseBody
    {
        get => _responseBody;
        private set
        {
            _responseBody = value;
            OnPropertyChanged();
            OnResponseChanged();
        }
    }


    private void OnResponseChanged()
    {
        ResponseChanged?.Invoke(this, ResponseBody);
    }

    private bool IsJsonValid()
    {
        if (string.IsNullOrEmpty(RequestBody))
        {
            return false;
        }

        try
        {
            using var jsonDoc = JsonDocument.Parse(RequestBody);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Dispose() => httpClient.Dispose();
}

public record Fruit(string Name, int Stock);
