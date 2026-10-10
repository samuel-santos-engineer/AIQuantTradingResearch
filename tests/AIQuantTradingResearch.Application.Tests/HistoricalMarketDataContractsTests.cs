using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class HistoricalMarketDataContractsTests
{
    [Fact]
    public void SyntheticWindowsCoverAllTwelveSelectionsAndEarliestDailyWarmup()
    {
        var counts = new[] { 24, 168, 720, 2160, 6, 42, 180, 540, 1, 7, 30, 90 }; var position = 0;
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>())
        foreach (var range in Enum.GetValues<HistoricalCandleRange>())
        {
            var entry = PublicContractTestData.Entry(interval, range);
            Assert.Equal(counts[position++] + 250, entry.Candles.Count);
            PublicContractTestData.Policy.Validate(entry.Request, entry.Candles, entry.Provenance);
        }
        Assert.Equal(SyntheticPublicationPolicy.CoverageStart, PublicContractTestData.Entry(HistoricalCandleInterval.OneDay, HistoricalCandleRange.NinetyDays).Candles[0].OpenTimeUtc);
    }

    [Fact]
    public void PublicPublicationRejectsMetadataOverridesAndWindowTampering()
    {
        var entry = PublicContractTestData.Entry(); var identity = entry.Provenance.SyntheticIdentity!;
        var invalid = new[] { identity with { Source = "Vike" }, identity with { Status = "observed" }, identity with { Venue = "exchange" }, identity with { Base = "BTC" }, identity with { Quote = "USD" }, identity with { Settlement = "USD" }, identity with { MarketType = "spot" }, identity with { PriceUnit = "USD" }, identity with { VolumeUnit = "BTC" }, identity with { Instrument = "BTC/USD" }, identity with { CanonicalVersion = "aiq-candles-v1" }, identity with { Seed = 115 }, identity with { GeneratorVersion = "other" }, identity with { CalculationVersion = "aiq-ta-v1" }, identity with { DatasetFingerprint = "bad" }, identity with { WindowFingerprint = new string('b', 64) }, identity with { CoverageStartUtc = SyntheticPublicationPolicy.CoverageStart.AddDays(1) } };
        foreach (var bad in invalid) Assert.Throws<ArgumentException>(() => PublicContractTestData.Policy.Validate(entry.Request, entry.Candles, entry.Provenance with { SyntheticIdentity = bad }));
        Assert.Throws<ArgumentException>(() => PublicContractTestData.Policy.Validate(entry.Request, entry.Candles.Skip(1).ToArray(), entry.Provenance));
        var rows = entry.Candles.ToArray(); rows[1] = rows[0];
        Assert.Throws<ArgumentException>(() => PublicContractTestData.Policy.Validate(entry.Request, rows, entry.Provenance));
        Assert.Throws<ArgumentException>(() => PublicContractTestData.Policy.Validate(entry.Request, entry.Candles, entry.Provenance with { SyntheticIdentity = identity with { DatasetFingerprint = new string('b', 64) } }));
    }

    [Fact]
    public void PublicCacheIdentityCannotCollideWithPrivateOrAnotherDataset()
    {
        var entry = PublicContractTestData.Entry();
        var otherPolicy = new SyntheticPublicationPolicy(Enum.GetValues<HistoricalCandleInterval>().ToDictionary(i => i, _ => new string('b', 64)), PublicContractTestData.Windows);
        Assert.NotEqual(entry.Key.Fingerprint, HistoricalMarketDataCacheKey.Public(entry.Request, otherPolicy).Fingerprint);
        Assert.NotEqual(entry.Key.Fingerprint, new HistoricalMarketDataCacheKey(CanonicalMarketSymbol.BtcUsd, entry.Request.Interval, entry.Request.Range, "Vike", HistoricalMarketDataReadService.CacheContractVersion).Fingerprint);
        Assert.Throws<ArgumentException>(() => (entry.Key with { Context = "private-research" }).Fingerprint);
    }

    [Fact]
    public void PublicGridRejectsMisalignmentAndExcessPrecision()
    {
        var entry = PublicContractTestData.Entry(); var rows = entry.Candles.ToArray(); var first = rows[0];
        rows[0] = new(first.Symbol, first.Interval, first.OpenTimeUtc.AddSeconds(1), 10m, 12m, 9m, 11m, 20m);
        Assert.Throws<ArgumentException>(() => PublicContractTestData.Policy.Validate(entry.Request, rows, entry.Provenance));
        rows[0] = new(first.Symbol, first.Interval, first.OpenTimeUtc, 10.00001m, 12m, 9m, 11m, 20m);
        Assert.Throws<ArgumentException>(() => PublicContractTestData.Policy.Validate(entry.Request, rows, entry.Provenance));
    }

    [Fact]
    public void CanonicalSerializationMatchesIndependentPythonConstantFixtureHashes()
    {
        // Independent Python byte-layout oracle, arbitrary constant candles; not generator goldens.
        var entry = PublicContractTestData.Entry(HistoricalCandleInterval.OneDay, HistoricalCandleRange.NinetyDays);
        Assert.Equal("053da98a8cf82c278e46648833190098f551172f38ff51c2aecbc1201c446a49", SyntheticPublicationPolicy.DatasetFingerprint(entry.Request.Interval, entry.Candles));
        Assert.Equal("50a3e2064b2b88fea04b5c3721d5a65a72a109c27d0535a4712712aa4521f84e", SyntheticPublicationPolicy.WindowFingerprint(entry.Request, entry.Candles));
    }
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

internal static class PublicContractTestData
{
    internal static readonly Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string> Windows = BuildWindows();
    internal static readonly SyntheticPublicationPolicy Policy = new(Enum.GetValues<HistoricalCandleInterval>().ToDictionary(i => i, _ => new string('a', 64)), Windows);
    private static Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string> BuildWindows()
    {
        var windows = new Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string>();
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>()) foreach (var range in Enum.GetValues<HistoricalCandleRange>())
        {
            var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation, interval, range, TimeSpan.FromSeconds(10));
            windows[(interval, range)] = SyntheticPublicationPolicy.WindowFingerprint(request, Rows(request));
        }
        return windows;
    }
    private static HistoricalCandle[] Rows(HistoricalMarketDataRequest request) => Enumerable.Range(0, SyntheticPublicationPolicy.VisibleCount(request) + 250).Select(i => new HistoricalCandle(request.Symbol, request.Interval, SyntheticPublicationPolicy.CalculationStart(request).AddHours(i * SyntheticPublicationPolicy.Hours(request.Interval)), 10m, 12m, 9m, 11m, 20m)).ToArray();
    internal static HistoricalMarketDataCacheEntry Entry(HistoricalCandleInterval interval = HistoricalCandleInterval.OneDay, HistoricalCandleRange range = HistoricalCandleRange.OneDay)
    {
        var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation, interval, range, TimeSpan.FromSeconds(10));
        var rows = Enumerable.Range(0, SyntheticPublicationPolicy.VisibleCount(request) + 250).Select(i => new HistoricalCandle(request.Symbol, interval, SyntheticPublicationPolicy.CalculationStart(request).AddHours(i * SyntheticPublicationPolicy.Hours(interval)), 10m, 12m, 9m, 11m, 20m)).ToArray();
        var identity = new SyntheticMarketDataIdentity("aiq-candles-v2", "btc-like-simulation", "simulation", "independent-synthetic", "simulated", null, null, null, null, "illustrative-price-unit", "illustrative-activity-unit", "aiq-synthetic-ohlcv-v1", 114, SyntheticPublicationPolicy.CoverageStart, SyntheticPublicationPolicy.CoverageEnd, new string('a', 64), SyntheticPublicationPolicy.WindowFingerprint(request, rows));
        var now = DateTimeOffset.UtcNow;
        return new(HistoricalMarketDataCacheKey.Public(request, Policy), request, rows, new HistoricalMarketDataProvenance("independent-synthetic", now, now, false, true) { SyntheticIdentity = identity }, now);
    }
}
