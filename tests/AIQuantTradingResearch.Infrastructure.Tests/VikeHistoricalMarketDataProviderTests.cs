using System.Net;
using System.Text;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Vike;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class VikeHistoricalMarketDataProviderTests
{
    private static readonly DateTimeOffset Now =
        new(2026, 9, 19, 12, 0, 0, TimeSpan.Zero);

    [Theory]
    [InlineData(CanonicalMarketSymbol.BtcUsd, "BTC")]
    [InlineData(CanonicalMarketSymbol.EthUsd, "ETH")]
    public async Task GetHistoricalCandlesAsyncTranslatesCanonicalSymbols(
        CanonicalMarketSymbol symbol,
        string expectedSymbol)
    {
        using var handler = RespondWith(HttpStatusCode.OK, ValidPayload());
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest(symbol: symbol));

        Assert.True(result.IsSuccess);
        Assert.Contains($"symbol={expectedSymbol}", handler.RequestUri, StringComparison.Ordinal);
        Assert.DoesNotContain("USD", handler.RequestUri, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(HistoricalCandleInterval.OneHour, "1h")]
    [InlineData(HistoricalCandleInterval.FourHours, "4h")]
    [InlineData(HistoricalCandleInterval.OneDay, "1d")]
    public async Task GetHistoricalCandlesAsyncTranslatesCanonicalIntervals(
        HistoricalCandleInterval interval,
        string expectedInterval)
    {
        using var handler = RespondWith(HttpStatusCode.OK, ValidPayload());
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest(interval: interval));

        Assert.True(result.IsSuccess);
        Assert.Contains($"interval={expectedInterval}", handler.RequestUri, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(HistoricalCandleRange.OneDay, "2026-09-18")]
    [InlineData(HistoricalCandleRange.SevenDays, "2026-09-12")]
    [InlineData(HistoricalCandleRange.ThirtyDays, "2026-08-20")]
    [InlineData(HistoricalCandleRange.NinetyDays, "2026-06-21")]
    public async Task GetHistoricalCandlesAsyncBoundsEachRange(
        HistoricalCandleRange range,
        string expectedStart)
    {
        using var handler = RespondWith(HttpStatusCode.OK, ValidPayload());
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest(range: range));

        Assert.True(result.IsSuccess);
        Assert.Contains($"start={expectedStart}&end=2026-09-19&limit=5000", handler.RequestUri, StringComparison.Ordinal);
        Assert.DoesNotContain("include_partial", handler.RequestUri, StringComparison.Ordinal);
        Assert.Equal(1, handler.CallCount);
    }

    [Fact]
    public async Task GetHistoricalCandlesAsyncSendsApiKeyInHeaderOnlyAndReturnsCanonicalCandles()
    {
        using var handler = RespondWith(HttpStatusCode.OK, ValidPayload());
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest());

        Assert.True(result.IsSuccess);
        Assert.Equal("wp03-test-key", handler.ApiKey);
        Assert.DoesNotContain("wp03-test-key", handler.RequestUri, StringComparison.Ordinal);
        Assert.Equal("Vike", result.Provenance!.Provider);
        Assert.Equal(Now, result.Provenance.AcquiredAtUtc);
        Assert.True(result.Provenance.ContainsOnlyClosedCandles);
        var candles = Assert.IsAssignableFrom<IReadOnlyList<HistoricalCandle>>(result.Candles);
        Assert.Equal(2, candles.Count);
        Assert.Equal(1.1234567890123456789012345678m, candles[0].Open);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1767225600000), candles[0].OpenTimeUtc);
        Assert.True(candles[0].OpenTimeUtc.Offset == TimeSpan.Zero);
    }

    [Fact]
    public async Task GetHistoricalCandlesAsyncAcceptsDocumentedCompactObjectCandles()
    {
        using var handler = RespondWith(HttpStatusCode.OK, CompactObjectPayload());
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest(CanonicalMarketSymbol.EthUsd));

        Assert.True(result.IsSuccess);
        var candles = Assert.IsAssignableFrom<IReadOnlyList<HistoricalCandle>>(result.Candles);
        Assert.Equal(2, candles.Count);
        Assert.All(candles, candle => Assert.Equal(CanonicalMarketSymbol.EthUsd, candle.Symbol));
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1767225600000), candles[0].OpenTimeUtc);
        Assert.Equal(1.1234567890123456789012345678m, candles[0].Open);
    }

    [Fact]
    public async Task GetHistoricalCandlesAsyncAcceptsVikeNamedObjectCandles()
    {
        using var handler = RespondWith(HttpStatusCode.OK, NamedObjectPayload());
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest());

        Assert.True(result.IsSuccess);
        var candles = Assert.IsAssignableFrom<IReadOnlyList<HistoricalCandle>>(result.Candles);
        Assert.Equal(2, candles.Count);
        Assert.Equal(DateTimeOffset.FromUnixTimeMilliseconds(1767225600000), candles[0].OpenTimeUtc);
        Assert.Equal(1.1234567890123456789012345678m, candles[0].Open);
    }

    [Theory]
    [InlineData(HttpStatusCode.TooManyRequests, HistoricalMarketDataFailure.RateLimited)]
    [InlineData(HttpStatusCode.Unauthorized, HistoricalMarketDataFailure.AuthenticationOrConfiguration)]
    [InlineData(HttpStatusCode.Forbidden, HistoricalMarketDataFailure.AuthenticationOrConfiguration)]
    [InlineData(HttpStatusCode.ServiceUnavailable, HistoricalMarketDataFailure.Unavailable)]
    public async Task GetHistoricalCandlesAsyncMapsProviderStatusWithoutSurfacingPayload(
        HttpStatusCode statusCode,
        HistoricalMarketDataFailure expectedFailure)
    {
        using var handler = RespondWith(statusCode, "{ \"error\": \"secret-free synthetic failure\" }");
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest());

        Assert.False(result.IsSuccess);
        Assert.Equal(expectedFailure, result.Failure);
        Assert.Null(result.Candles);
        Assert.Null(result.Provenance);
    }

    [Theory]
    [InlineData("{not-json")]
    [InlineData("{ \"candles\": [[1767225600000, \"10\", \"12\", \"9\", \"11\"]] }")]
    [InlineData("{ \"candles\": [[1767225600000, \"10\", \"9\", \"11\", \"11\", \"100\"]] }")]
    [InlineData("{ \"candles\": [[1767229200000, \"10\", \"12\", \"9\", \"11\", \"100\"], [1767225600000, \"10\", \"12\", \"9\", \"11\", \"100\"]] }")]
    [InlineData("{ \"candles\": [[1767225600000, \"10\", \"12\", \"9\", \"11\", \"100\"], [1767225600000, \"10\", \"12\", \"9\", \"11\", \"100\"]] }")]
    [InlineData("{ \"candles\": [{ \"ts\": 1767225600000, \"open\": \"10\", \"high\": \"12\", \"low\": \"9\", \"close\": \"11\" }] }")]
    public async Task GetHistoricalCandlesAsyncRejectsMalformedOrNonCanonicalPayload(string payload)
    {
        using var handler = RespondWith(HttpStatusCode.OK, payload);
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest());

        Assert.False(result.IsSuccess);
        Assert.Equal(HistoricalMarketDataFailure.MalformedResponse, result.Failure);
    }

    [Fact]
    public async Task GetHistoricalCandlesAsyncRejectsUnexpectedContinuationCursorForBoundedMatrixRequest()
    {
        using var handler = RespondWith(HttpStatusCode.OK, "{ \"candles\": [], \"next_cursor\": \"synthetic\" }");
        var provider = CreateProvider(handler);

        var result = await provider.GetHistoricalCandlesAsync(CreateRequest(range: HistoricalCandleRange.NinetyDays));

        Assert.Equal(HistoricalMarketDataFailure.MalformedResponse, result.Failure);
        Assert.Equal(1, handler.CallCount);
    }

    [Fact]
    public async Task GetHistoricalCandlesAsyncMapsCallerCancellationAndDeadline()
    {
        using var cancelledHandler = new RecordingHandler(
            static (_, token) => Task.FromCanceled<HttpResponseMessage>(token));
        var cancelledProvider = CreateProvider(cancelledHandler);
        using var callerCancellation = new CancellationTokenSource();
        callerCancellation.Cancel();

        var cancelled = await cancelledProvider.GetHistoricalCandlesAsync(CreateRequest(), callerCancellation.Token);

        Assert.Equal(HistoricalMarketDataFailure.Cancelled, cancelled.Failure);

        using var timeoutHandler = new RecordingHandler(
            static (_, token) =>
            {
                var completion = new TaskCompletionSource<HttpResponseMessage>(
                    TaskCreationOptions.RunContinuationsAsynchronously);
                token.Register(
                    static state => ((TaskCompletionSource<HttpResponseMessage>)state!).TrySetCanceled(),
                    completion);
                return completion.Task;
            });
        var timeoutProvider = CreateProvider(timeoutHandler);
        var timedOut = await timeoutProvider.GetHistoricalCandlesAsync(
            CreateRequest(deadline: TimeSpan.FromSeconds(1)));

        Assert.Equal(HistoricalMarketDataFailure.Timeout, timedOut.Failure);
    }

    private static VikeHistoricalMarketDataProvider CreateProvider(RecordingHandler handler) =>
        new(
            new HttpClient(handler),
            new VikeConfiguration("wp03-test-key"),
            new FixedTimeProvider(Now));

    private static HistoricalMarketDataRequest CreateRequest(
        CanonicalMarketSymbol symbol = CanonicalMarketSymbol.BtcUsd,
        HistoricalCandleInterval interval = HistoricalCandleInterval.OneHour,
        HistoricalCandleRange range = HistoricalCandleRange.ThirtyDays,
        TimeSpan? deadline = null) =>
        new(symbol, interval, range, deadline ?? TimeSpan.FromSeconds(10));

    private static RecordingHandler RespondWith(HttpStatusCode statusCode, string payload) =>
        new(
            (_, _) => Task.FromResult(
                new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                }));

    private static string ValidPayload() =>
        """
        {
          "exchange": "merged",
          "candles": [
            [1767225600000, "1.1234567890123456789012345678", "1.3", "1.0", "1.2", "100"],
            [1767229200000, "1.2", "1.4", "1.1", "1.3", "120"]
          ]
        }
        """;

    private static string CompactObjectPayload() =>
        """
        {
          "exchange": "merged",
          "candles": [
            { "ts": 1767225600000, "o": "1.1234567890123456789012345678", "h": "1.3", "l": "1.0", "c": "1.2", "v": "100" },
            { "ts": 1767229200000, "o": "1.2", "h": "1.4", "l": "1.1", "c": "1.3", "v": "120" }
          ]
        }
        """;

    private static string NamedObjectPayload() =>
        """
        {
          "exchange": "merged",
          "candles": [
            { "ts": 1767225600000, "open": "1.1234567890123456789012345678", "high": "1.3", "low": "1.0", "close": "1.2", "volume": "100" },
            { "ts": 1767229200000, "open": "1.2", "high": "1.4", "low": "1.1", "close": "1.3", "volume": "120" }
          ]
        }
        """;

    private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }

    private sealed class RecordingHandler(
        Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> responseFactory)
        : HttpMessageHandler
    {
        public int CallCount { get; private set; }

        public string? RequestUri { get; private set; }

        public string? ApiKey { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            CallCount++;
            RequestUri = request.RequestUri?.OriginalString;
            ApiKey = request.Headers.TryGetValues("X-API-Key", out var values)
                ? values.SingleOrDefault()
                : null;
            return responseFactory(request, cancellationToken);
        }
    }
}
