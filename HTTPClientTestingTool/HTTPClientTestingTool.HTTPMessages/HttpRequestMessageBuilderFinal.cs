namespace HTTPClientTestingTool.HTTPMessages;

internal class HttpRequestMessageBuilderFinal : IHttpRequestMessageBuilderFinal
{
    private HttpRequestMessage _requestMessage;

    public HttpRequestMessageBuilderFinal(HttpRequestMessage requestMessage) => _requestMessage = requestMessage;

    public IHttpRequestMessageBuilderFinal WithHeaders(string headers)
    {
        // Set headers on the request
        var individualHeader = headers.Split(",");
        foreach (var header in individualHeader)
        {
            var keyValuePair = header.Split(":");
            if (keyValuePair.Length == 2)
            {
                _requestMessage.Headers.TryAddWithoutValidation(keyValuePair[0].Trim(), keyValuePair[1].Trim());
            }
        }

        return this;
    }

    public IHttpRequestMessageBuilderFinal WithContent(HttpContent content)
    {
        _requestMessage.Content = content;
        return this;
    }

    public HttpRequestMessage Build() => _requestMessage;
}

public interface IHttpRequestMessageBuilderFinal
{
    IHttpRequestMessageBuilderFinal WithContent(HttpContent content);
    IHttpRequestMessageBuilderFinal WithHeaders(string headers);

    HttpRequestMessage Build();
}
