using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class HistoricalMarketDataContractsTests
{
    private static readonly DateTimeOffset AcquisitionTime =
        new(2026, 9, 19, 0, 0, 0, TimeSpan.Zero);

    [Theory]
    [InlineData(CanonicalMarketSymbol.BtcUsd)]
    [InlineData(CanonicalMarketSymbol.EthUsd)]
    public void RequestAcceptsOnlySupportedCanonicalSymbols(CanonicalMarketSymbol symbol) =>
        Assert.Equal(symbol, CreateRequest(symbol: symbol).Symbol);

    [Theory]
    [InlineData(HistoricalCandleInterval.OneHour)]
    [InlineData(HistoricalCandleInterval.FourHours)]
    [InlineData(HistoricalCandleInterval.OneDay)]
    public void RequestAcceptsOnlySupportedIntervals(HistoricalCandleInterval interval) =>
        Assert.Equal(interval, CreateRequest(interval: interval).Interval);

    [Theory]
    [InlineData(HistoricalCandleRange.OneDay)]
    [InlineData(HistoricalCandleRange.SevenDays)]
    [InlineData(HistoricalCandleRange.ThirtyDays)]
    [InlineData(HistoricalCandleRange.NinetyDays)]
    public void RequestAcceptsOnlySupportedRanges(HistoricalCandleRange range) =>
        Assert.Equal(range, CreateRequest(range: range).Range);

    [Fact]
    public void RequestRejectsUnsupportedCanonicalValuesAndUnboundedDeadlines()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateRequest(symbol: (CanonicalMarketSymbol)99));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateRequest(interval: (HistoricalCandleInterval)99));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateRequest(range: (HistoricalCandleRange)99));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateRequest(deadline: TimeSpan.Zero));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreateRequest(deadline: TimeSpan.FromSeconds(31)));
    }

    [Fact]
    public void CandleUsesDecimalOhlcvAndRequiresUtcOpenTime()
    {
        var candle = CreateCandle(
            open: 1.1234567890123456789012345678m,
            high: 1.2m,
            low: 1m,
            close: 1.15m);

        Assert.Equal(1.1234567890123456789012345678m, candle.Open);
        Assert.Throws<ArgumentException>(() => CreateCandle(openTime: AcquisitionTime.ToOffset(TimeSpan.FromHours(-3))));
    }

    [Fact]
    public void CandleRejectsInconsistentOhlcv()
    {
        Assert.Throws<ArgumentException>(() => CreateCandle(high: 9m));
        Assert.Throws<ArgumentException>(() => CreateCandle(low: 11m));
        Assert.Throws<ArgumentException>(() => CreateCandle(volume: -1m));
    }

    [Fact]
    public void AvailableResponseSeparatesProvenanceAndEnforcesOrderingAndUniqueness()
    {
        var request = CreateRequest();
        var provenance = CreateProvenance();
        var first = CreateCandle();
        var second = CreateCandle(openTime: AcquisitionTime.AddHours(1));

        var response = HistoricalMarketDataResponse.Available(request, [first, second], provenance);

        Assert.True(response.IsSuccess);
        Assert.Same(provenance, response.Provenance);
        Assert.Null(response.Failure);
        Assert.Equal([first, second], response.Candles);
        Assert.Throws<ArgumentException>(() => HistoricalMarketDataResponse.Available(request, [second, first], provenance));
        Assert.Throws<ArgumentException>(() => HistoricalMarketDataResponse.Available(request, [first, first], provenance));
    }

    [Fact]
    public void AvailableResponseRejectsCandlesForAnotherCanonicalRequest()
    {
        var request = CreateRequest();
        var mismatched = CreateCandle(symbol: CanonicalMarketSymbol.EthUsd);

        Assert.Throws<ArgumentException>(() => HistoricalMarketDataResponse.Available(request, [mismatched], CreateProvenance()));
    }

    [Theory]
    [InlineData(HistoricalMarketDataFailure.InvalidRequest)]
    [InlineData(HistoricalMarketDataFailure.Unavailable)]
    [InlineData(HistoricalMarketDataFailure.RateLimited)]
    [InlineData(HistoricalMarketDataFailure.AuthenticationOrConfiguration)]
    [InlineData(HistoricalMarketDataFailure.MalformedResponse)]
    [InlineData(HistoricalMarketDataFailure.Timeout)]
    [InlineData(HistoricalMarketDataFailure.Cancelled)]
    public void FailedResponsePreservesEachTypedFailure(HistoricalMarketDataFailure failure)
    {
        var response = HistoricalMarketDataResponse.Failed(failure);

        Assert.False(response.IsSuccess);
        Assert.Equal(failure, response.Failure);
        Assert.Null(response.Candles);
        Assert.Null(response.Provenance);
    }

    [Fact]
    public void ProvenanceRequiresUtcOrderedMetadataAndClosedCandleState()
    {
        var provenance = CreateProvenance(isStale: true, containsOnlyClosedCandles: false);

        Assert.True(provenance.IsStale);
        Assert.False(provenance.ContainsOnlyClosedCandles);
        Assert.Throws<ArgumentException>(() => new HistoricalMarketDataProvenance("provider", AcquisitionTime, AcquisitionTime.AddMinutes(-1), false, true));
        Assert.Throws<ArgumentException>(() => new HistoricalMarketDataProvenance("provider", AcquisitionTime.ToOffset(TimeSpan.FromHours(1)), AcquisitionTime, false, true));
    }

    private static HistoricalMarketDataRequest CreateRequest(
        CanonicalMarketSymbol symbol = CanonicalMarketSymbol.BtcUsd,
        HistoricalCandleInterval interval = HistoricalCandleInterval.OneHour,
        HistoricalCandleRange range = HistoricalCandleRange.ThirtyDays,
        TimeSpan? deadline = null) =>
        new(symbol, interval, range, deadline ?? TimeSpan.FromSeconds(10));

    private static HistoricalCandle CreateCandle(
        CanonicalMarketSymbol symbol = CanonicalMarketSymbol.BtcUsd,
        DateTimeOffset? openTime = null,
        decimal open = 10m,
        decimal? high = null,
        decimal? low = null,
        decimal close = 11m,
        decimal volume = 100m) =>
        new(symbol, HistoricalCandleInterval.OneHour, openTime ?? AcquisitionTime, open, high ?? 12m, low ?? 9m, close, volume);

    private static HistoricalMarketDataProvenance CreateProvenance(
        bool isStale = false,
        bool containsOnlyClosedCandles = true) =>
        new("Synthetic provider", AcquisitionTime, AcquisitionTime.AddMinutes(1), isStale, containsOnlyClosedCandles);
}
