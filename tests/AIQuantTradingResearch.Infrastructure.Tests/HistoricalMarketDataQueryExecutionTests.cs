using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;
using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class HistoricalMarketDataQueryExecutionTests
{
    [Fact]
    public async Task LegacyObservedRequestIsRejectedBeforeProviderSelection()
    {
        using var fixture = new Fixture();
        var output = new StringWriter();
        var exit = await fixture.Execution.ExecuteAsync(new StringReader("{\"contractVersion\":\"aiq-historical-query-v1\",\"symbol\":\"BTC/USD\",\"interval\":\"1h\",\"range\":\"30D\",\"deadlineSeconds\":10}"), output, new StringWriter());
        using var json = JsonDocument.Parse(output.ToString());
        Assert.Equal(0, exit); Assert.Equal("InvalidRequest", json.RootElement.GetProperty("failure").GetString()); Assert.Equal(0, fixture.Provider.Calls); Assert.DoesNotContain("secret", output.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task MalformedRequestCannotFallThroughToLegacyOutput()
    {
        using var fixture = new Fixture(); var output = new StringWriter();
        await fixture.Execution.ExecuteAsync(new StringReader("not-json"), output, new StringWriter());
        using var json = JsonDocument.Parse(output.ToString());
        Assert.Equal("Unavailable", json.RootElement.GetProperty("state").GetString()); Assert.Equal("InvalidRequest", json.RootElement.GetProperty("failure").GetString()); Assert.Equal(0, fixture.Provider.Calls);
    }

    [Theory]
    [InlineData("{\"contractVersion\":\"aiq-historical-query-v2\",\"instrument\":\"btc-like-simulation\",\"interval\":\"1h\",\"range\":\"30D\",\"deadlineSeconds\":10,\"source\":\"Vike\"}")]
    [InlineData("{\"contractVersion\":\"aiq-historical-query-v2\",\"instrument\":\"btc-like-simulation\",\"interval\":\"1h\",\"range\":\"30D\",\"deadlineSeconds\":10,\"deadlineSeconds\":10}")]
    [InlineData("{} {}")]
    [InlineData("[]")]
    [InlineData("{\"contractVersion\":1}")]
    [InlineData("{\"context\":\"private-research\"}")]
    public async Task RawRequestNegativesNeverTouchPrivateProvider(string raw)
    {
        using var fixture = new Fixture(); var output = new StringWriter();
        await fixture.Execution.ExecuteAsync(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(raw)), output, new StringWriter());
        Assert.Contains("InvalidRequest", output.ToString()); Assert.Equal(0, fixture.Provider.Calls);
    }

    [Theory]
    [InlineData(new byte[] { 0xc0, 0xaf })]
    [InlineData(new byte[] { 0xed, 0xa0, 0x80 })]
    [InlineData(new byte[] { 0xf4, 0x90, 0x80, 0x80 })]
    [InlineData(new byte[] { 0xe2, 0x82 })]
    public async Task MalformedUtf8IsRejectedBeforeParsing(byte[] bytes)
    {
        using var fixture = new Fixture(); var output = new StringWriter();
        await fixture.Execution.ExecuteAsync(new MemoryStream(bytes), output, new StringWriter());
        Assert.Contains("InvalidRequest", output.ToString()); Assert.Equal(0, fixture.Provider.Calls);
    }

    [Fact]
    public async Task FloodStopsAfter4097BytesWithoutReadingRemainder()
    {
        using var fixture = new Fixture(); using var stream = new CountingStream(new byte[1_000_000]); var output = new StringWriter();
        await fixture.Execution.ExecuteAsync(stream, output, new StringWriter());
        Assert.Equal(4097, stream.BytesRead); Assert.Contains("InvalidRequest", output.ToString()); Assert.Equal(0, fixture.Provider.Calls);
    }

    [Theory]
    [InlineData(4096, "Unavailable")]
    [InlineData(4097, "InvalidRequest")]
    public async Task ExactByteBoundaryWithFragmentedReads(int length, string expected)
    {
        using var fixture = new Fixture(); var raw = SyntheticTestFixture.RequestJson("1d", "1D").PadRight(length); var output = new StringWriter();
        using var stream = new CountingStream(System.Text.Encoding.UTF8.GetBytes(raw), 1);
        await fixture.Execution.ExecuteAsync(stream, output, new StringWriter());
        using var response = JsonDocument.Parse(output.ToString()); Assert.Equal(expected, response.RootElement.GetProperty("failure").GetString()); Assert.Equal(0, fixture.Provider.Calls);
    }

    [Theory]
    [InlineData(HistoricalCandleInterval.OneHour, HistoricalCandleRange.OneDay, 24)]
    [InlineData(HistoricalCandleInterval.OneHour, HistoricalCandleRange.SevenDays, 168)]
    [InlineData(HistoricalCandleInterval.OneHour, HistoricalCandleRange.ThirtyDays, 720)]
    [InlineData(HistoricalCandleInterval.OneHour, HistoricalCandleRange.NinetyDays, 2160)]
    [InlineData(HistoricalCandleInterval.FourHours, HistoricalCandleRange.OneDay, 6)]
    [InlineData(HistoricalCandleInterval.FourHours, HistoricalCandleRange.SevenDays, 42)]
    [InlineData(HistoricalCandleInterval.FourHours, HistoricalCandleRange.ThirtyDays, 180)]
    [InlineData(HistoricalCandleInterval.FourHours, HistoricalCandleRange.NinetyDays, 540)]
    [InlineData(HistoricalCandleInterval.OneDay, HistoricalCandleRange.OneDay, 1)]
    [InlineData(HistoricalCandleInterval.OneDay, HistoricalCandleRange.SevenDays, 7)]
    [InlineData(HistoricalCandleInterval.OneDay, HistoricalCandleRange.ThirtyDays, 30)]
    [InlineData(HistoricalCandleInterval.OneDay, HistoricalCandleRange.NinetyDays, 90)]
    public async Task AllPublicWindowsExposeOnlyVisibleCandles(HistoricalCandleInterval interval, HistoricalCandleRange range, int visible)
    {
        using var fixture = new SyntheticTestFixture(); var request = SyntheticTestFixture.Request(interval, range);
        var output = new StringWriter(); var provider = new SyntheticTestFixture.Provider();
        var service = HistoricalMarketDataReadService.PublicSynthetic(provider, fixture.Cache, SyntheticTestFixture.Policy);
        var execution = new HistoricalMarketDataQueryExecution(service, new AtomicFileHistoricalMarketDataQueryLock(fixture.Directory));
        await execution.ExecuteAsync(new MemoryStream(System.Text.Encoding.UTF8.GetBytes(SyntheticTestFixture.RequestJson(SyntheticPublicationPolicy.IntervalLiteral(interval), SyntheticPublicationPolicy.RangeLiteral(range)))), output, new StringWriter());
        using var response = JsonDocument.Parse(output.ToString()); Assert.Equal("Available", response.RootElement.GetProperty("state").GetString());
        Assert.Equal(visible, response.RootElement.GetProperty("candles").GetArrayLength()); Assert.DoesNotContain("Vike", output.ToString());
        Assert.Equal(SyntheticPublicationPolicy.Timestamp(SyntheticPublicationPolicy.CoverageEnd.AddDays(-SyntheticPublicationPolicy.Days(range))), response.RootElement.GetProperty("candles")[0].GetProperty("openTimeUtc").GetString());
    }

    private sealed class CountingStream(byte[] bytes, int fragment = 4097) : MemoryStream(bytes)
    {
        public int BytesRead { get; private set; }
        public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
        { var count = await base.ReadAsync(buffer[..Math.Min(buffer.Length, fragment)], cancellationToken); BytesRead += count; return count; }
    }

    [Fact]
    public async Task CancelledRawReadIsControlledWithoutProviderOrStreamDisposal()
    {
        using var fixture = new Fixture(); using var stream = new MemoryStream(); using var cancellation = new CancellationTokenSource(); cancellation.Cancel();
        var output = new StringWriter(); await fixture.Execution.ExecuteAsync(stream, output, new StringWriter(), cancellation.Token);
        Assert.Contains("Timeout", output.ToString()); Assert.Equal(0, fixture.Provider.Calls); Assert.True(stream.CanRead);
    }

    [Fact]
    public void ObservedVikeAdapterCannotBeComposedAsIndependentSyntheticProvider()
    {
        Assert.False(typeof(IIndependentSyntheticHistoricalMarketDataProvider).IsAssignableFrom(typeof(AIQuantTradingResearch.Infrastructure.MarketData.Vike.VikeHistoricalMarketDataProvider)));
    }

    private sealed class Fixture : IDisposable
    {
        private readonly string directory = Path.Combine(Path.GetTempPath(), "aiq-wp05-query-" + Guid.NewGuid().ToString("N"));
        public Fixture() { Directory.CreateDirectory(directory); Provider = new Provider(); var cache = new Cache(); Execution = new HistoricalMarketDataQueryExecution(new HistoricalMarketDataReadService(Provider, cache, "Vike"), new AtomicFileHistoricalMarketDataQueryLock(directory)); }
        public Provider Provider { get; } public HistoricalMarketDataQueryExecution Execution { get; }
        public void Dispose() { if (Directory.Exists(directory)) Directory.Delete(directory, true); }
    }
    private sealed class Cache : IHistoricalMarketDataCache { public Task<HistoricalMarketDataCacheLookup> RetrieveAsync(HistoricalMarketDataCacheKey key, CancellationToken ct = default) => Task.FromResult(HistoricalMarketDataCacheLookup.Missing()); public Task StoreAsync(HistoricalMarketDataCacheEntry entry, CancellationToken ct = default) => Task.CompletedTask; }
    private sealed class Provider : IHistoricalMarketDataProvider { public int Calls { get; private set; } public Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(HistoricalMarketDataRequest request, CancellationToken ct = default) { Calls++; var now = DateTimeOffset.UnixEpoch; return Task.FromResult(HistoricalMarketDataResponse.Available(request, [new HistoricalCandle(request.Symbol, request.Interval, now, 1m, 2m, 1m, 2m, 3m)], new HistoricalMarketDataProvenance("Vike", now, now, false, true))); } }
}

// Constant, arbitrary test candles, not the approved generator or release golden fixtures.
internal sealed class SyntheticTestFixture : IDisposable
{
    public static readonly SyntheticPublicationPolicy Policy = new(Enum.GetValues<HistoricalCandleInterval>().ToDictionary(i => i, _ => new string('a', 64)), BuildWindows());
    private static Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string> BuildWindows()
    {
        var windows = new Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string>();
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>()) foreach (var range in Enum.GetValues<HistoricalCandleRange>())
        {
            var request = Request(interval, range);
            var rows = Enumerable.Range(0, SyntheticPublicationPolicy.VisibleCount(request) + 250).Select(i => new HistoricalCandle(request.Symbol, interval, SyntheticPublicationPolicy.CalculationStart(request).AddHours(i * SyntheticPublicationPolicy.Hours(interval)), 10m, 12m, 9m, 11m, 20m)).ToArray();
            windows[(interval, range)] = SyntheticPublicationPolicy.WindowFingerprint(request, rows);
        }
        return windows;
    }
    public SyntheticTestFixture() { Directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"), "aiq-public-synthetic-cache"); System.IO.Directory.CreateDirectory(Directory); Cache = new AtomicFileHistoricalMarketDataCache(Directory, publicSynthetic: true); }
    public string Directory { get; }
    public AtomicFileHistoricalMarketDataCache Cache { get; }
    public static HistoricalMarketDataRequest Request(HistoricalCandleInterval interval = HistoricalCandleInterval.OneDay, HistoricalCandleRange range = HistoricalCandleRange.OneDay) => new(CanonicalMarketSymbol.BtcLikeSimulation, interval, range, TimeSpan.FromSeconds(10));
    public static string RequestJson(string interval, string range) => $"{{\"contractVersion\":\"aiq-historical-query-v2\",\"instrument\":\"btc-like-simulation\",\"interval\":\"{interval}\",\"range\":\"{range}\",\"deadlineSeconds\":10}}";
    public static HistoricalMarketDataCacheEntry Entry(HistoricalMarketDataRequest? request = null)
    {
        request ??= Request(); var start = SyntheticPublicationPolicy.CalculationStart(request);
        var rows = Enumerable.Range(0, SyntheticPublicationPolicy.VisibleCount(request) + 250).Select(i => new HistoricalCandle(request.Symbol, request.Interval, start.AddHours(i * SyntheticPublicationPolicy.Hours(request.Interval)), 10m, 12m, 9m, 11m, 20m)).ToArray();
        var identity = new SyntheticMarketDataIdentity("aiq-candles-v2", "btc-like-simulation", "simulation", "independent-synthetic", "simulated", null, null, null, null, "illustrative-price-unit", "illustrative-activity-unit", "aiq-synthetic-ohlcv-v1", 114, SyntheticPublicationPolicy.CoverageStart, SyntheticPublicationPolicy.CoverageEnd, new string('a', 64), SyntheticPublicationPolicy.WindowFingerprint(request, rows));
        var now = DateTimeOffset.UtcNow;
        return new(HistoricalMarketDataCacheKey.Public(request, Policy), request, rows, new HistoricalMarketDataProvenance("independent-synthetic", now, now, false, true) { SyntheticIdentity = identity }, now);
    }
    public sealed class Provider : IIndependentSyntheticHistoricalMarketDataProvider
    {
        public int Calls { get; private set; }
        public Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken = default)
        { Calls++; var entry = Entry(request); return Task.FromResult(HistoricalMarketDataResponse.Available(request, entry.Candles, entry.Provenance)); }
    }
    public void Dispose() { System.IO.Directory.Delete(Path.GetDirectoryName(Directory)!, true); }
}
