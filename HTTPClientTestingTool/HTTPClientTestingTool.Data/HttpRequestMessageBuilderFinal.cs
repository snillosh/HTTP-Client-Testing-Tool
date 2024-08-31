﻿using System.Net.Http;

namespace HTTPClientTestingTool.Data;

internal class HttpRequestMessageBuilderFinal : IHttpRequestMessageBuilderFinal
{
    private HttpRequestMessage _requestMessage;

    public HttpRequestMessageBuilderFinal(HttpRequestMessage requestMessage)
    {
        _requestMessage = requestMessage;
    }

    public IHttpRequestMessageBuilderFinal WithHeaders(string headers)
    {
        if (_requestMessage == null)
        {
            throw new InvalidOperationException("Method must be set before adding headers.");
        }

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
        if (_requestMessage == null)
        {
            throw new InvalidOperationException("Method must be set before setting content.");
        }
        _requestMessage.Content = content;
        return this;
    }

    public HttpRequestMessage Build()
    {
        if (_requestMessage == null || _requestMessage.Method == null || string.IsNullOrEmpty(_requestMessage.RequestUri.ToString()))
        {
            throw new InvalidOperationException("Method and URI must be set before building.");
        }
        return _requestMessage;
    }
}

public interface IHttpRequestMessageBuilderFinal
{
    IHttpRequestMessageBuilderFinal WithContent(HttpContent content);
    IHttpRequestMessageBuilderFinal WithHeaders(string headers);

    HttpRequestMessage Build();
}
