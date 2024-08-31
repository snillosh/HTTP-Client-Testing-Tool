﻿using HTTPClientTestingTool.Data;
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
        SendRequestCommand = new RelayCommand(SendRequestAction, CanSendRequestCommand);

        httpClient = new HttpClient();
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
            var request = HttpRequestMessageBuilder
                .WithUrl(new Uri(URL))
                .WithMethod(SelectedMethod)
                .WithContent(new StringContent(RequestBody, Encoding.UTF8, "application/json"))
                .WithHeaders(Headers)
                .Build();

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

    public EventHandler<string>? ResponseChanged;

    private string _url = "https://localhost:7061/";

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

    private EHttpMethods _selectedMethod = EHttpMethods.Get;

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
