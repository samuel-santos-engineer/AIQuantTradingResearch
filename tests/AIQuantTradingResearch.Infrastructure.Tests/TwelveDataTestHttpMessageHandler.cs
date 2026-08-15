namespace AIQuantTradingResearch.Infrastructure.Tests;

internal sealed class TwelveDataTestHttpMessageHandler(
    Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responseFactory)
    : HttpMessageHandler
{
    public int CallCount { get; private set; }

    public string? RequestUri { get; private set; }

    public string? AuthorizationScheme { get; private set; }

    public string? AuthorizationParameter { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        CallCount++;
        RequestUri = request.RequestUri?.OriginalString;
        AuthorizationScheme = request.Headers.Authorization?.Scheme;
        AuthorizationParameter = request.Headers.Authorization?.Parameter;

        return responseFactory(request, cancellationToken);
    }
}
