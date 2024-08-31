namespace HTTPClientTestingTool.HTTPMessages;

public static class HttpRequestMessageBuilder
{
    public static IHttpRequestMessageRequiresMethod WithUrl(Uri uri)
    {
        return new HttpRequestMessageRequiresMethod(new HttpRequestMessage(HttpMethod.Get, uri));
    }
}

public interface IHttpRequestMessageRequiresMethod
{
    IHttpRequestMessageBuilderFinal WithMethod(EHttpMethod method);
}

internal class HttpRequestMessageRequiresMethod : IHttpRequestMessageRequiresMethod
{
    private HttpRequestMessage _requestMessage;

    public HttpRequestMessageRequiresMethod(HttpRequestMessage requestMessage)
    {
        _requestMessage = requestMessage;
    }

    public IHttpRequestMessageBuilderFinal WithMethod(EHttpMethod method)
    {
        var httpMethod = new HttpMethod(method.ToString());

        _requestMessage.Method = httpMethod;

        return new HttpRequestMessageBuilderFinal(_requestMessage);
    }
}

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
