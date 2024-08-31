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
    IHttpRequestMessageBuilderFinal WithMethod(EHttpMethods method);
}

internal class HttpRequestMessageRequiresMethod : IHttpRequestMessageRequiresMethod
{
    private HttpRequestMessage _requestMessage;

    public HttpRequestMessageRequiresMethod(HttpRequestMessage requestMessage)
    {
        _requestMessage = requestMessage;
    }

    public IHttpRequestMessageBuilderFinal WithMethod(EHttpMethods method)
    {
        var httpMethod = new HttpMethod(method.ToString());

        _requestMessage.Method = httpMethod;

        return new HttpRequestMessageBuilderFinal(_requestMessage);
    }
}
