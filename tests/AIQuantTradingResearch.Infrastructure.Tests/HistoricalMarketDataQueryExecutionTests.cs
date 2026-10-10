using System.Text.Json;
using System.Diagnostics;
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
        var analytics = response.RootElement.GetProperty("analytics");
        Assert.Equal("aiq-ta-v1", response.RootElement.GetProperty("provenance").GetProperty("calculationVersion").GetString());
        Assert.Equal("public-synthetic", analytics.GetProperty("context").GetString());
        Assert.Equal("independent-synthetic", analytics.GetProperty("source").GetString());
        Assert.Equal(250, analytics.GetProperty("warmup").GetInt32());
        Assert.Equal(visible + 250, analytics.GetProperty("calculationCount").GetInt32());
        Assert.Equal(SyntheticPublicationPolicy.Timestamp(SyntheticPublicationPolicy.CalculationStart(request)), analytics.GetProperty("calculationStartUtc").GetString());
        Assert.Equal(visible, analytics.GetProperty("rows").GetArrayLength());
        foreach (var row in analytics.GetProperty("rows").EnumerateArray())
        {
            Assert.Equal("11.000000000000", row.GetProperty("sma20").GetString());
            Assert.Equal("11.000000000000", row.GetProperty("ema20").GetString());
            Assert.Equal("50.000000000000", row.GetProperty("rsi14").GetString());
            Assert.Equal("0.000000000000", row.GetProperty("macdHistogram").GetString());
        }
        var cached = await fixture.Cache.RetrieveAsync(service.GetPublicKey(request));
        Assert.NotNull(cached.Entry); Assert.Null(cached.Entry.Provenance.SyntheticIdentity!.CalculationVersion);
        var secondOutput = new StringWriter();
        await execution.ExecuteAsync(new StringReader(SyntheticTestFixture.RequestJson(SyntheticPublicationPolicy.IntervalLiteral(interval), SyntheticPublicationPolicy.RangeLiteral(range))), secondOutput, new StringWriter());
        Assert.Equal(output.ToString(), secondOutput.ToString()); Assert.Equal(1, provider.Calls);
    }

    [Fact]
    public async Task HiddenWarmupActuallySeedsAllVisibleAnalytics()
    {
        using var fixture = new SyntheticTestFixture(); var request = SyntheticTestFixture.Request();
        var baseline = SyntheticTestFixture.Entry(request);
        var candles = baseline.Candles.Select((c, i) => new HistoricalCandle(c.Symbol, c.Interval, c.OpenTimeUtc,
            i < 250 ? 100m : 1m, i < 250 ? 100m : 1m, i < 250 ? 100m : 1m, i < 250 ? 100m : 1m, 0m)).ToArray();
        var fingerprint = SyntheticPublicationPolicy.WindowFingerprint(request, candles);
        var identity = baseline.Provenance.SyntheticIdentity! with { WindowFingerprint = fingerprint };
        var provenance = baseline.Provenance with { SyntheticIdentity = identity };
        var windows = Enum.GetValues<HistoricalCandleInterval>().SelectMany(i => Enum.GetValues<HistoricalCandleRange>().Select(r => (i, r)))
            .ToDictionary(k => k, k => SyntheticTestFixture.Policy.QualifiedWindowFingerprint(SyntheticTestFixture.Request(k.i, k.r)));
        windows[(request.Interval, request.Range)] = fingerprint;
        var policy = new SyntheticPublicationPolicy(Enum.GetValues<HistoricalCandleInterval>().ToDictionary(i => i, i => new string('a', 64)), windows);
        var provider = new FixedSyntheticProvider(candles, provenance);
        using var service = HistoricalMarketDataReadService.PublicSynthetic(provider, fixture.Cache, policy);
        var execution = new HistoricalMarketDataQueryExecution(service, new AtomicFileHistoricalMarketDataQueryLock(fixture.Directory));
        var output = new StringWriter(); await execution.ExecuteAsync(new StringReader(SyntheticTestFixture.RequestJson("1d", "1D")), output, new StringWriter());
        using var json = JsonDocument.Parse(output.ToString()); var rows = json.RootElement.GetProperty("analytics").GetProperty("rows");
        Assert.Equal(1, rows.GetArrayLength()); var row = rows[0];
        // Independently derived step after 250 constant-100 closes, followed by one close of 1.
        Assert.Equal("95.050000000000", row.GetProperty("sma20").GetString());
        Assert.Equal("90.571428571429", row.GetProperty("ema20").GetString());
        Assert.Equal("0.000000000000", row.GetProperty("rsi14").GetString());
        Assert.Equal("-7.897435897436", row.GetProperty("macd").GetString());
        Assert.Equal("-1.579487179487", row.GetProperty("macdSignal").GetString());
        Assert.Equal("-6.317948717949", row.GetProperty("macdHistogram").GetString());
        Assert.Equal("51.896900458948", row.GetProperty("bollingerLower").GetString());
        Assert.Equal("138.203099541052", row.GetProperty("bollingerUpper").GetString());
        Assert.Equal(SyntheticPublicationPolicy.Timestamp(candles[^1].OpenTimeUtc), row.GetProperty("openTimeUtc").GetString());
        Assert.Null(provenance.SyntheticIdentity!.CalculationVersion);
    }

    [Theory]
    [InlineData("version")] [InlineData("source")] [InlineData("fingerprint")] [InlineData("instrument")]
    public async Task ContaminatedSyntheticInputsNeverPublishAnalytics(string mutation)
    {
        using var fixture = new SyntheticTestFixture(); var entry = SyntheticTestFixture.Entry();
        var identity = entry.Provenance.SyntheticIdentity!;
        identity = mutation switch { "version" => identity with { CalculationVersion = "aiq-ta-v1" },
            "source" => identity with { Source = "Vike" }, "fingerprint" => identity with { WindowFingerprint = new string('c', 64) },
            _ => identity with { Instrument = "BTC/USD" } };
        var provider = new FixedSyntheticProvider(entry.Candles, entry.Provenance with { SyntheticIdentity = identity });
        using var service = HistoricalMarketDataReadService.PublicSynthetic(provider, fixture.Cache, SyntheticTestFixture.Policy);
        var execution = new HistoricalMarketDataQueryExecution(service, new AtomicFileHistoricalMarketDataQueryLock(fixture.Directory));
        var output = new StringWriter(); await execution.ExecuteAsync(new StringReader(SyntheticTestFixture.RequestJson("1d", "1D")), output, new StringWriter());
        using var json = JsonDocument.Parse(output.ToString()); Assert.Equal("Unavailable", json.RootElement.GetProperty("state").GetString());
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("analytics").ValueKind);
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("provenance").ValueKind);
        Assert.DoesNotContain("BTC/USD", output.ToString()); Assert.DoesNotContain("Vike", output.ToString());
    }

    private sealed class FixedSyntheticProvider(IReadOnlyList<HistoricalCandle> candles, HistoricalMarketDataProvenance provenance) : IIndependentSyntheticHistoricalMarketDataProvider
    {
        public Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken = default) =>
            Task.FromResult(HistoricalMarketDataResponse.Available(request, candles, provenance));
    }

    [Fact]
    public async Task QualifiedGeneratorWorkerAndPythonAgreeForAllTwelveSelections()
    {
        using var fixture = new SyntheticTestFixture();
        var provider = new AIQuantTradingResearch.Infrastructure.MarketData.Synthetic.SyntheticHistoricalMarketDataProvider();
        using var service = HistoricalMarketDataReadService.PublicSynthetic(provider, fixture.Cache,
            AIQuantTradingResearch.Infrastructure.MarketData.Synthetic.SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy());
        var execution = new HistoricalMarketDataQueryExecution(service, new AtomicFileHistoricalMarketDataQueryLock(fixture.Directory));
        var responses = new List<JsonElement>();
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>()) foreach (var range in Enum.GetValues<HistoricalCandleRange>())
        {
            var output = new StringWriter();
            await execution.ExecuteAsync(new StringReader(SyntheticTestFixture.RequestJson(SyntheticPublicationPolicy.IntervalLiteral(interval), SyntheticPublicationPolicy.RangeLiteral(range))), output, new StringWriter());
            using var json = JsonDocument.Parse(output.ToString());
            Assert.Equal("Available", json.RootElement.GetProperty("state").GetString());
            Assert.True(System.Text.Encoding.UTF8.GetByteCount(output.ToString()) <= 2 * 1024 * 1024);
            responses.Add(json.RootElement.Clone());
        }
        var datasets = AIQuantTradingResearch.Infrastructure.MarketData.Synthetic.SyntheticHistoricalMarketDataProvider.DatasetFingerprints
            .ToDictionary(p => SyntheticPublicationPolicy.IntervalLiteral(p.Key), p => p.Value);
        var windows = AIQuantTradingResearch.Infrastructure.MarketData.Synthetic.SyntheticHistoricalMarketDataProvider.WindowFingerprints
            .Select(p => new { interval = SyntheticPublicationPolicy.IntervalLiteral(p.Key.Interval), range = SyntheticPublicationPolicy.RangeLiteral(p.Key.Range), fingerprint = p.Value });
        var visible = AIQuantTradingResearch.Infrastructure.MarketData.Synthetic.SyntheticHistoricalMarketDataProvider.VisibleRowFingerprints
            .Select(p => new { interval = SyntheticPublicationPolicy.IntervalLiteral(p.Key.Interval), range = SyntheticPublicationPolicy.RangeLiteral(p.Key.Range), fingerprint = p.Value });
        // Protocol compatibility check, not the independent numerical oracle: CPython invokes
        // the actual bridge against .NET-generated responses using server-qualified WP02 values.
        const string script = """
            import sys,json
            from decimal import Decimal
            sys.path.insert(0,sys.argv[1])
            from historical_market_bridge import parse_response
            data=json.load(sys.stdin)
            windows={(p['interval'],p['range']):p['fingerprint'] for p in data['windows']}
            visible={(p['interval'],p['range']):p['fingerprint'] for p in data['visible']}
            counts=[]
            for payload in data['responses']:
                parsed=parse_response(json.dumps(payload),qualified_dataset_fingerprints=data['datasets'],qualified_window_fingerprints=windows,qualified_visible_fingerprints=visible)
                assert parsed.analytics.metadata['context']=='public-synthetic'
                assert len(parsed.analytics.rows)==len(parsed.candles)
                assert all(isinstance(r.sma20,Decimal) for r in parsed.analytics.rows)
                counts.append(len(parsed.candles))
            print(','.join(map(str,counts)))
            """;
        var root = new DirectoryInfo(AppContext.BaseDirectory);
        while (root is not null && !File.Exists(Path.Combine(root.FullName, "python", "presentation", "historical_market_bridge.py"))) root = root.Parent;
        Assert.NotNull(root);
        var start = new ProcessStartInfo(Environment.GetEnvironmentVariable("AIQ_WP02_REFERENCE_PYTHON") ?? "python")
        { UseShellExecute = false, CreateNoWindow = true, RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true };
        start.ArgumentList.Add("-I"); start.ArgumentList.Add("-B"); start.ArgumentList.Add("-c"); start.ArgumentList.Add(script);
        start.ArgumentList.Add(Path.Combine(root.FullName, "python", "presentation"));
        using var process = Process.Start(start)!;
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        try
        {
            var stdout = process.StandardOutput.ReadToEndAsync(timeout.Token);
            var stderr = process.StandardError.ReadToEndAsync(timeout.Token);
            await process.StandardInput.WriteAsync(JsonSerializer.Serialize(new { responses, datasets, windows, visible }).AsMemory(), timeout.Token);
            process.StandardInput.Close(); await process.WaitForExitAsync(timeout.Token);
            Assert.Equal(0, process.ExitCode); Assert.Equal("", await stderr);
            Assert.Equal("24,168,720,2160,6,42,180,540,1,7,30,90", (await stdout).Trim());
        }
        finally { if (!process.HasExited) { process.Kill(entireProcessTree: true); await process.WaitForExitAsync(); } }
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
