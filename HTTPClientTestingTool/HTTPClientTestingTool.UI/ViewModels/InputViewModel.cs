using HTTPClientTestingTool.Data;
using HTTPClientTestingTool.UI.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using JsonException = System.Text.Json.JsonException;

namespace HTTPClientTestingTool.UI.ViewModels;

class InputViewModel : ViewModelBase
{
    private readonly HttpClient httpClient;

    public InputViewModel()
    {
        SelectedMethod = EHttpMethods.Get;

        SendRequestCommand = new RelayCommand(SendRequestAction, CanSendRequestCommand);

        httpClient = new HttpClient();
    }

    private async void SendRequestAction(object obj)
    {
        if (SelectedMethod != EHttpMethods.Get)
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

            var response = SelectedMethod switch
            {
                EHttpMethods.Get => await httpClient.GetAsync(URL),
                EHttpMethods.Post => await httpClient.PostAsync(URL, content),
                EHttpMethods.Put => await httpClient.PutAsync(URL, content),
                EHttpMethods.Delete => await httpClient.DeleteAsync(URL),
                _ => throw new InvalidOperationException("Unsupported HTTP method.")
            };

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


    private string _headers;

    public string Headers { get => _headers; set { _headers = value; OnPropertyChanged(); } }

    private string _requestBody;

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

    private string _responseBody;

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
        catch (JsonException)
        {
            return false;
        }
    }
}

public record Fruit(string Name, int Stock);
