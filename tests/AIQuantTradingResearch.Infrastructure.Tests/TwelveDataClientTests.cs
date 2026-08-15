using System.Net;
using System.Text;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class TwelveDataClientTests
{
    private const string PlaceholderApiKey = "wp13-placeholder-key";

    [Fact]
    public async Task GetTimeSeriesAsyncConstructsAuthorizedRequestAndDeserializesSuccess()
    {
        const string payload = """
            {
              "status": "ok",
              "meta": {
                "symbol": "BRK B/A",
                "interval": "1day",
                "currency": "USD",
                "exchange_timezone": "America/New_York",
                "exchange": "NYSE",
                "mic_code": "XNYS",
                "type": "Common Stock"
              },
              "values": [
                {
                  "datetime": "2026-01-15",
                  "open": "100.10",
                  "high": "103.20",
                  "low": "99.80",
                  "close": "102.30",
                  "volume": "12345"
                }
              ]
            }
            """;
        using var handler = RespondWith(HttpStatusCode.OK, payload);
        using var httpClient = CreateHttpClient(handler);
        var client = new TwelveDataClient(httpClient, PlaceholderApiKey);

        var result = await client.GetTimeSeriesAsync("BRK B/A", 7);

        Assert.Equal(1, handler.CallCount);
        Assert.Equal(
            "https://api.twelvedata.com/time_series?symbol=BRK%20B%2FA&interval=1day&outputsize=7&adjust=splits",
            handler.RequestUri);
        Assert.Equal("apikey", handler.AuthorizationScheme);
        Assert.Equal(PlaceholderApiKey, handler.AuthorizationParameter);
        Assert.DoesNotContain(PlaceholderApiKey, handler.RequestUri, StringComparison.Ordinal);
        Assert.Null(result.TransportException);
        Assert.False(result.IsPayloadUnreadable);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Null(result.Error);
        Assert.NotNull(result.Response);
        Assert.Equal("ok", result.Response.Status);
        Assert.NotNull(result.Response.Metadata);
        Assert.Equal("BRK B/A", result.Response.Metadata.Symbol);
        Assert.Equal("1day", result.Response.Metadata.Interval);
        Assert.Equal("USD", result.Response.Metadata.Currency);
        Assert.Equal("America/New_York", result.Response.Metadata.ExchangeTimezone);
        Assert.Equal("NYSE", result.Response.Metadata.Exchange);
        Assert.Equal("XNYS", result.Response.Metadata.MicCode);
        Assert.Equal("Common Stock", result.Response.Metadata.Type);
        var value = Assert.Single(Assert.IsAssignableFrom<IReadOnlyList<TwelveDataTimeSeriesValue>>(
            result.Response.Values));
        Assert.Equal("2026-01-15", value.DateTime);
        Assert.Equal("100.10", value.Open);
        Assert.Equal("103.20", value.High);
        Assert.Equal("99.80", value.Low);
        Assert.Equal("102.30", value.Close);
        Assert.Equal("12345", value.Volume);
    }

    [Fact]
    public async Task GetTimeSeriesAsyncDeserializesStructuredProviderError()
    {
        const string payload = """
            { "status": "error", "code": 429, "message": "Synthetic quota response." }
            """;
        using var handler = RespondWith(HttpStatusCode.BadRequest, payload);
        using var httpClient = CreateHttpClient(handler);
        var client = new TwelveDataClient(httpClient, PlaceholderApiKey);

        var result = await client.GetTimeSeriesAsync("SYNTHETIC", 3);

        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Null(result.Response);
        Assert.NotNull(result.Error);
        Assert.Equal("error", result.Error.Status);
        Assert.Equal(429, result.Error.Code);
        Assert.Equal("Synthetic quota response.", result.Error.Message);
        Assert.False(result.IsPayloadUnreadable);
        Assert.Null(result.TransportException);
    }

    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.BadRequest)]
    public async Task GetTimeSeriesAsyncPreservesUnreadablePayloadEvidence(HttpStatusCode statusCode)
    {
        using var handler = RespondWith(statusCode, "{not-json");
        using var httpClient = CreateHttpClient(handler);
        var client = new TwelveDataClient(httpClient, PlaceholderApiKey);

        var result = await client.GetTimeSeriesAsync("SYNTHETIC", 3);

        Assert.Equal(statusCode, result.StatusCode);
        Assert.Null(result.Response);
        Assert.Null(result.Error);
        Assert.True(result.IsPayloadUnreadable);
        Assert.Null(result.TransportException);
    }

    [Fact]
    public async Task GetTimeSeriesAsyncPreservesHttpRequestException()
    {
        using var handler = new TwelveDataTestHttpMessageHandler(
            static (_, _) => throw new HttpRequestException("Synthetic transport failure."));
        using var httpClient = CreateHttpClient(handler);
        var client = new TwelveDataClient(httpClient, PlaceholderApiKey);

        var result = await client.GetTimeSeriesAsync("SYNTHETIC", 3);

        Assert.Equal(1, handler.CallCount);
        Assert.Null(result.StatusCode);
        Assert.Null(result.Response);
        Assert.Null(result.Error);
        Assert.False(result.IsPayloadUnreadable);
        Assert.IsType<HttpRequestException>(result.TransportException);
    }

    [Fact]
    public async Task GetTimeSeriesAsyncPropagatesCallerCancellation()
    {
        using var handler = new TwelveDataTestHttpMessageHandler(
            static (_, cancellationToken) =>
                Task.FromCanceled<HttpResponseMessage>(cancellationToken));
        using var httpClient = CreateHttpClient(handler);
        var client = new TwelveDataClient(httpClient, PlaceholderApiKey);
        using var cancellationSource = new CancellationTokenSource();
        cancellationSource.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => client.GetTimeSeriesAsync("SYNTHETIC", 3, cancellationSource.Token));
    }

    internal static HttpClient CreateHttpClient(HttpMessageHandler handler) =>
        new(handler)
        {
            BaseAddress = new Uri("https://api.twelvedata.com/"),
        };

    internal static TwelveDataTestHttpMessageHandler RespondWith(
        HttpStatusCode statusCode,
        string content) =>
        new(
            (_, _) => Task.FromResult(
                new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json"),
                }));
}
