using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Synthetic;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

/// <summary>C2B-15 test-only proposal: not compiled/run or independently accepted at authoring.</summary>
// No exporter, files of rows, providers, runtime wiring or production Python port.
[CollectionDefinition("E4 sequential", DisableParallelization = true)]
public sealed class E4SequentialCollection { }

[Collection("E4 sequential")]
public sealed class SyntheticSnapshotQualificationTests
{
    private const int StdoutLimit = 16 * 1024 * 1024;
    private const int StderrLimit = 16 * 1024;
    private static readonly DateTimeOffset Start = new(2025, 10, 26, 0, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset End = new(2026, 10, 1, 0, 0, 0, TimeSpan.Zero);
    private static readonly UTF8Encoding StrictUtf8 = new(false, true);
    private static readonly HistoricalCandleInterval[] Intervals =
        [HistoricalCandleInterval.OneHour, HistoricalCandleInterval.FourHours, HistoricalCandleInterval.OneDay];
    private static readonly HistoricalCandleRange[] Ranges =
        [HistoricalCandleRange.OneDay, HistoricalCandleRange.SevenDays, HistoricalCandleRange.ThirtyDays, HistoricalCandleRange.NinetyDays];

    [Fact]
    public async Task CompleteIndependentBytesAndEveryAlignedIndicatorMatchOnTwoPasses()
    {
        // Exact executable/version and outer supervised 120s batch must be separately authorized.
        // Only independent selectors enter Python, never these production results.
        byte[]? previous = null;
        for (var pass = 0; pass < 2; pass++)
        {
            var bytes = await RunOracleAsync();
            if (previous is not null) Assert.True(previous.AsSpan().SequenceEqual(bytes), "Oracle repeat mismatch; payload suppressed.");
            previous = bytes;
            using var document = ParseOracle(bytes);
            var root = document.RootElement;
            Assert.Equal("aiq-synthetic-ohlcv-v1", root.GetProperty("generator").GetString());
            Assert.Equal(114, root.GetProperty("seed").GetInt32());
            Assert.Equal("2025-10-26T00:00:00Z", root.GetProperty("coverageStart").GetString());
            Assert.Equal("2026-10-01T00:00:00Z", root.GetProperty("coverageEnd").GetString());
            Assert.Equal("[\"aiq-ta-v1\",12,20,20,14,12,26,9,20,2]", root.GetProperty("parameters").GetRawText());

            var complete = SyntheticHistoricalMarketDataProvider.GenerateCompleteDataset(Budget());
            var datasets = root.GetProperty("datasets");
            var windows = root.GetProperty("windows");
            for (var i = 0; i < Intervals.Length; i++)
            {
                var interval = Intervals[i];
                var rows = complete.For(interval);
                var dataset = datasets[i];
                var expectedCount = new[] { 8160, 2040, 340 }[i];
                Assert.Equal(expectedCount, rows.Count);
                Assert.Equal(expectedCount, dataset.GetProperty("count").GetInt32());
                Assert.Equal(SyntheticPublicationPolicy.IntervalLiteral(interval), dataset.GetProperty("interval").GetString());
                var canonical = CompareEnvelope(dataset.GetProperty("canonical"),
                    SyntheticHistoricalMarketDataProvider.SerializeDataset(interval, rows, Budget()),
                    SyntheticHistoricalMarketDataProvider.DatasetFingerprints[interval]);
                CompareCandleRows(canonical, rows, header: true);
                Assert.Equal(Start, rows[0].OpenTimeUtc);
                Assert.Equal(End, rows[^1].OpenTimeUtc.AddHours(SyntheticPublicationPolicy.Hours(interval)));
                CompareAggregation(complete.For(HistoricalCandleInterval.OneHour), rows, interval);
                var selected = new List<IReadOnlyList<HistoricalCandle>>();
                for (var j = 0; j < Ranges.Length; j++)
                {
                    var request = Request(interval, Ranges[j]);
                    var calculation = SyntheticHistoricalMarketDataProvider.SelectCalculationWindow(request, rows);
                    selected.Add(calculation);
                    var window = windows[4 * i + j];
                    Assert.Equal(SyntheticPublicationPolicy.IntervalLiteral(interval), window.GetProperty("interval").GetString());
                    Assert.Equal(SyntheticPublicationPolicy.RangeLiteral(Ranges[j]), window.GetProperty("range").GetString());
                    Assert.Equal(calculation.Count, window.GetProperty("count").GetInt32());
                    Assert.Equal(SyntheticPublicationPolicy.VisibleCount(request), window.GetProperty("visibleCount").GetInt32());
                    Assert.Equal(250, calculation.Count - SyntheticPublicationPolicy.VisibleCount(request));
                    Assert.Equal(SyntheticPublicationPolicy.Timestamp(calculation[0].OpenTimeUtc), window.GetProperty("calculationStart").GetString());
                    Assert.Equal(SyntheticPublicationPolicy.Timestamp(calculation[250].OpenTimeUtc), window.GetProperty("visibleStart").GetString());
                    CompareCandleRows(CompareEnvelope(window.GetProperty("canonical"),
                        SyntheticHistoricalMarketDataProvider.SerializeWindow(request, calculation, Budget()),
                        SyntheticHistoricalMarketDataProvider.WindowFingerprints[(interval, Ranges[j])]), calculation, true);
                    CompareCandleRows(CompareEnvelope(window.GetProperty("visible"),
                        SyntheticHistoricalMarketDataProvider.SerializeVisibleRows(request, calculation, Budget()),
                        SyntheticHistoricalMarketDataProvider.VisibleRowFingerprints[(interval, Ranges[j])]), calculation.Skip(250).ToArray(), false);
                    var provenance = Provenance(request);
                    SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy().Validate(request, calculation, provenance);
                    var analytics = TechnicalIndicatorCalculator.CalculatePublic(request, calculation, provenance);
                    Assert.Equal("public-synthetic", analytics.Identity.Context);
                    Assert.Equal("independent-synthetic", analytics.Identity.Source);
                    Assert.Equal(CanonicalMarketSymbol.BtcLikeSimulation, analytics.Identity.Symbol);
                    Assert.Equal(interval, analytics.Identity.Interval);
                    Assert.Equal("aiq-candles-v2", analytics.Identity.CanonicalVersion);
                    Assert.Equal(calculation[0].OpenTimeUtc, analytics.Identity.CalculationStartUtc);
                    Assert.Equal(provenance.SyntheticIdentity!.DatasetFingerprint, analytics.Identity.DatasetFingerprint);
                    Assert.Equal(provenance.SyntheticIdentity.WindowFingerprint, analytics.Identity.WindowFingerprint);
                    AssertParameters(analytics.Parameters);
                    CompareIndicators(window.GetProperty("indicators"), analytics.Rows, calculation);
                    CompareIndicators(window.GetProperty("visibleIndicators"), analytics.Rows.Skip(250).ToArray(), calculation.Skip(250).ToArray(), checkInitialNulls: false);
                    Assert.Equal(calculation[250].OpenTimeUtc, analytics.Rows.Skip(250).First().OpenTimeUtc);
                }
                // Every overlapping candle, not range-independent recursive analytics.
                foreach (var small in selected)
                    Assert.Equal(small, selected[^1].TakeLast(small.Count).ToArray());
            }
            foreach (var edge in root.GetProperty("edges").EnumerateArray())
            {
                var closes = edge.GetProperty("closes").EnumerateArray().Select(ExactDecimal).ToArray();
                var candles = PrivateCandles(closes);
                var result = TechnicalIndicatorCalculator.Calculate(candles, PrivateIdentity(), PrivateProvenance());
                CompareIndicators(edge.GetProperty("indicators"), result.Rows, candles);
            }
        }
    }

    private static void AssertParameters(TechnicalIndicatorParameters p)
    {
        Assert.Equal("aiq-ta-v1", p.Version); Assert.Equal(12, p.Scale);
        Assert.Equal(20, p.SmaPeriod); Assert.Equal(20, p.EmaPeriod); Assert.Equal(14, p.RsiPeriod);
        Assert.Equal(12, p.MacdFastPeriod); Assert.Equal(26, p.MacdSlowPeriod); Assert.Equal(9, p.MacdSignalPeriod);
        Assert.Equal(20, p.BollingerPeriod); Assert.Equal(2, p.BollingerMultiplier);
    }

    private static byte[] CompareEnvelope(JsonElement envelope, byte[] actual, string frozenHash)
    {
        var encoded = envelope.GetProperty("bytes").GetString()!;
        if (encoded.Length > StdoutLimit) throw new InvalidDataException("Encoded oracle result too large.");
        var expected = Convert.FromBase64String(encoded);
        Assert.Equal(envelope.GetProperty("length").GetInt32(), expected.Length);
        var hash = Convert.ToHexString(SHA256.HashData(expected)).ToLowerInvariant();
        Assert.Equal(frozenHash, hash); Assert.Equal(hash, envelope.GetProperty("sha256").GetString());
        Assert.True(expected.AsSpan().SequenceEqual(actual), "Canonical byte mismatch; payload suppressed.");
        Assert.DoesNotContain((byte)'\r', expected); Assert.Equal((byte)'\n', expected[^1]);
        Assert.False(expected.AsSpan().StartsWith(new byte[] { 0xef, 0xbb, 0xbf }));
        return expected;
    }

    private static void CompareCandleRows(byte[] canonical, IReadOnlyList<HistoricalCandle> actual, bool header)
    {
        var text = StrictUtf8.GetString(canonical);
        var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var offset = header ? Array.FindIndex(lines, x => x == "columns=openTimeUtc,open,high,low,close,volume") + 1 : 0;
        Assert.True(!header || offset > 0);
        Assert.Equal(actual.Count, lines.Length - offset);
        for (var i = 0; i < actual.Count; i++)
        {
            var fields = lines[offset + i].Split(','); Assert.Equal(6, fields.Length);
            var candle = actual[i];
            Assert.Equal(SyntheticPublicationPolicy.Timestamp(candle.OpenTimeUtc), fields[0]);
            var numbers = new[] { candle.Open, candle.High, candle.Low, candle.Close, candle.Volume };
            for (var f = 0; f < numbers.Length; f++)
                Assert.Equal(numbers[f], decimal.Parse(fields[f + 1], CultureInfo.InvariantCulture));
            Assert.True(candle.Low <= Math.Min(candle.Open, candle.Close));
            Assert.True(candle.High >= Math.Max(candle.Open, candle.Close)); Assert.True(candle.Volume >= 0);
        }
    }

    private static void CompareAggregation(IReadOnlyList<HistoricalCandle> hourly, IReadOnlyList<HistoricalCandle> rows, HistoricalCandleInterval interval)
    {
        var hours = SyntheticPublicationPolicy.Hours(interval);
        for (var i = 0; i < rows.Count; i++)
        {
            var group = hourly.Skip(i * hours).Take(hours).ToArray(); var row = rows[i];
            Assert.Equal(group[0].OpenTimeUtc, row.OpenTimeUtc); Assert.Equal(group[0].Open, row.Open);
            Assert.Equal(group.Max(c => c.High), row.High); Assert.Equal(group.Min(c => c.Low), row.Low);
            Assert.Equal(group[^1].Close, row.Close); Assert.Equal(group.Sum(c => c.Volume), row.Volume);
        }
    }

    private static void CompareIndicators(JsonElement expected, IReadOnlyList<TechnicalIndicatorRow> actual,
        IReadOnlyList<HistoricalCandle> input, bool checkInitialNulls = true)
    {
        Assert.Equal(input.Count, actual.Count); Assert.Equal(actual.Count, expected.GetArrayLength());
        int[] first = [20, 20, 15, 26, 34, 34, 20, 20, 20];
        for (var i = 0; i < actual.Count; i++)
        {
            var row = actual[i]; Assert.Equal(input[i].OpenTimeUtc, row.OpenTimeUtc);
            decimal?[] values = [row.Sma20, row.Ema20, row.Rsi14, row.Macd, row.MacdSignal,
                row.MacdHistogram, row.BollingerLower, row.BollingerMiddle, row.BollingerUpper];
            Assert.Equal(9, expected[i].GetArrayLength());
            for (var f = 0; f < values.Length; f++)
            {
                decimal? reference = expected[i][f].ValueKind == JsonValueKind.Null ? null : ExactDecimal(expected[i][f]);
                Assert.Equal(reference, values[f]);
                if (checkInitialNulls) Assert.Equal(i + 1 < first[f], values[f] is null);
            }
        }
    }

    private static decimal ExactDecimal(JsonElement value)
    {
        var text = value.GetString() ?? throw new InvalidDataException("Missing decimal string.");
        if (text.Length > 40 || !Regex.IsMatch(text, @"^-?(0|[1-9][0-9]*)\.[0-9]{12}$", RegexOptions.CultureInvariant))
            throw new InvalidDataException("Invalid exact decimal.");
        return decimal.Parse(text, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
    }

    [Fact]
    public void IndependentSpecificationHandVectorsAndNoLookahead()
    {
        var constant = PrivateCalculate(Enumerable.Repeat(100m, 40));
        Assert.Equal(100m, constant.Rows[19].Sma20); Assert.Equal(50m, constant.Rows[14].Rsi14);
        Assert.Equal(0m, constant.Rows[33].MacdHistogram);
        var rising = PrivateCalculate(Enumerable.Range(1, 40).Select(i => (decimal)i));
        Assert.Equal(10.5m, rising.Rows[19].Sma20); Assert.Equal(11.5m, rising.Rows[20].Ema20);
        Assert.Equal(-1.032562594670m, rising.Rows[19].BollingerLower);
        Assert.Equal(22.032562594670m, rising.Rows[19].BollingerUpper);
        Assert.Equal(7m, rising.Rows[25].Macd); Assert.Equal(7m, rising.Rows[33].MacdSignal);
        var falling = PrivateCalculate(Enumerable.Range(1, 40).Select(i => 41m - i));
        Assert.Equal(0m, falling.Rows[14].Rsi14); Assert.Equal(-7m, falling.Rows[33].MacdSignal);
        var alternating = PrivateCalculate(Enumerable.Range(0, 40).Select(i => 100m + i % 2));
        Assert.Equal(53.571428571400m, alternating.Rows[15].Rsi14);
        var prefix = Enumerable.Range(1, 40).Select(i => (decimal)i).ToArray();
        var extended = PrivateCalculate(prefix.Concat([999m]));
        Assert.Equal(rising.Rows, extended.Rows.Take(40).ToArray());
    }

    [Theory]
    [InlineData(1, -1, 0)]
    [InlineData(1, 0, 0)]
    [InlineData(1, 1, 1)]
    [InlineData(3, -1, 1)]
    [InlineData(3, 0, 2)]
    [InlineData(3, 1, 2)]
    public void PublicCalculatorRoundsPopulationSquareRootAroundEvenAndOddMidpoints(int delta, int perturbation, int expectedSigma)
    {
        // Ten closes at a and ten at a+d scaled units give A=100*d^2,
        // sigma=d/2. d=1 ties to even 0; d=3 ties to even 2. No internal seam.
        const decimal unit = 0.000000000001m;
        var closes = Enumerable.Repeat(100m, 10).Concat(Enumerable.Repeat(100m + delta * unit, 10)).ToArray();
        closes[^1] += perturbation * unit;
        var row = PrivateCalculate(closes).Rows[19];
        Assert.Equal(2 * expectedSigma * unit, row.BollingerUpper!.Value - row.BollingerMiddle!.Value);
        Assert.Equal(2 * expectedSigma * unit, row.BollingerMiddle!.Value - row.BollingerLower!.Value);
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(1, 10, 2)]
    [InlineData(0, -10, 0)]
    [InlineData(1, -10, 0)]
    public void PublicSmaRoundsBothDirectionsAroundEvenAndOddMidpoints(int offset, int delta, int expectedOffset)
    {
        const decimal unit = 0.000000000001m;
        var closes = Enumerable.Repeat(100m + offset * unit, 20).ToArray();
        closes[^1] += delta * unit;
        Assert.Equal(100m + expectedOffset * unit, PrivateCalculate(closes).Rows[19].Sma20);
    }

    [Fact]
    public void PublicValidatorsRejectExplicitGridAndProvenanceMutations()
    {
        var request = Request(HistoricalCandleInterval.OneHour, HistoricalCandleRange.OneDay);
        var complete = SyntheticHistoricalMarketDataProvider.GenerateCompleteDataset(Budget());
        var valid = SyntheticHistoricalMarketDataProvider.SelectCalculationWindow(request, complete.For(request.Interval)).ToArray();
        var provenance = Provenance(request);
        var policy = SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy();
        policy.Validate(request, valid, provenance); // Positive control before every negative family.
        foreach (var bad in new[] { valid.Take(valid.Length - 1).ToArray(), valid.Reverse().ToArray(),
            valid.Select((c, i) => i == 5 ? valid[4] : c).ToArray(),
            valid.Select((c, i) => i == 5 ? Move(c, c.OpenTimeUtc.AddHours(1)) : c).ToArray(),
            valid.Select((c, i) => i == 5 ? Move(c, c.OpenTimeUtc.AddMinutes(1)) : c).ToArray(),
            valid.Select((c, i) => i == 5 ? null! : c).ToArray() })
            Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.CalculatePublic(request, bad, provenance));
        var identity = provenance.SyntheticIdentity!;
        SyntheticMarketDataIdentity[] mutations = [identity with { Source = "invalid" }, identity with { GeneratorVersion = "invalid" },
            identity with { Seed = 115 }, identity with { Instrument = "invalid" }, identity with { PriceUnit = "USD" },
            identity with { VolumeUnit = "BTC" }, identity with { CoverageStartUtc = Start.AddHours(1) },
            identity with { CoverageEndExclusiveUtc = End.AddHours(1) }, identity with { DatasetFingerprint = new string('a', 64) },
            identity with { WindowFingerprint = new string('b', 64) }, identity with { CalculationVersion = "aiq-ta-v1" }];
        foreach (var mutation in mutations)
            Assert.Throws<ArgumentException>(() => policy.Validate(request, valid, provenance with { SyntheticIdentity = mutation }));
        foreach (var bad in new[] { new HistoricalMarketDataProvenance("invalid", Start, Start, false, true),
            new HistoricalMarketDataProvenance("independent-synthetic", Start, Start, true, true),
            new HistoricalMarketDataProvenance("independent-synthetic", Start, Start, false, false) })
            Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.CalculatePublic(request, valid, bad with { SyntheticIdentity = identity }));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(valid,
            new("aiq-candles-v2", "private-research", "independent-synthetic", CanonicalMarketSymbol.BtcLikeSimulation,
                request.Interval, valid[0].OpenTimeUtc, identity.DatasetFingerprint, identity.WindowFingerprint), provenance));
    }

    [Fact]
    public void CanonicalConstructorsAndCalculatorRejectMalformedInputs()
    {
        foreach (var close in new[] { 0m, -1m })
            Assert.Throws<ArgumentException>(() => PrivateCandles([close]));
        Assert.Throws<ArgumentException>(() => new HistoricalCandle(CanonicalMarketSymbol.BtcUsd,
            HistoricalCandleInterval.OneHour, Start.ToOffset(TimeSpan.FromHours(1)), 1, 1, 1, 1, 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => Request((HistoricalCandleInterval)99, HistoricalCandleRange.OneDay));
        Assert.Throws<ArgumentOutOfRangeException>(() => Request(HistoricalCandleInterval.OneHour, (HistoricalCandleRange)99));
        Assert.Throws<ArgumentOutOfRangeException>(() => new HistoricalMarketDataRequest((CanonicalMarketSymbol)99,
            HistoricalCandleInterval.OneHour, HistoricalCandleRange.OneDay, TimeSpan.FromSeconds(10)));
        foreach (var values in new[] { Array.Empty<decimal>(), new[] { 1_000_000_001m }, new[] { 1.0000000000001m }, Enumerable.Repeat(1m, 2411).ToArray() })
            Assert.Throws<ArgumentException>(() => PrivateCalculate(values));
        var rows = PrivateCandles(Enumerable.Repeat(100m, 40));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(rows, PrivateIdentity() with { Context = "invalid" }, PrivateProvenance()));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(rows, PrivateIdentity() with { CalculationStartUtc = Start.AddHours(1) }, PrivateProvenance()));
    }

    [Fact]
    public void CancellationIsObservedAtEntryAndDuringGeneration()
    {
        using var cancelled = new CancellationTokenSource(); cancelled.Cancel();
        Assert.Throws<OperationCanceledException>(() => TechnicalIndicatorCalculator.Calculate(
            PrivateCandles([100m]), PrivateIdentity(), PrivateProvenance(), cancelled.Token));
        Assert.Throws<OperationCanceledException>(() => Budget(cancelled.Token));
        using var mid = new CancellationTokenSource();
        var clock = new CancellingClock(mid);
        var budget = new SyntheticHistoricalMarketDataProvider.GenerationBudget(clock, TimeSpan.FromSeconds(10), mid.Token);
        Assert.Throws<OperationCanceledException>(() => SyntheticHistoricalMarketDataProvider.GenerateCompleteDataset(budget));
        Assert.True(mid.IsCancellationRequested);
    }

    [Fact]
    public async Task ByteCaptureRejectsOverflowAndHonorsCancellation()
    {
        using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(1));
        await Assert.ThrowsAsync<InvalidDataException>(() => ReadBoundedAsync(new MemoryStream(new byte[5]), 4, deadline));
        using var stopped = new CancellationTokenSource(); stopped.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => ReadBoundedAsync(new MemoryStream([1]), 4, stopped));
    }

    [Theory]
    [InlineData("{\"version\":1,\"version\":2}")]
    [InlineData("{\"unknown\":1}")]
    public void StrictTransportRejectsDuplicateExtraAndMissingFields(string text) =>
        Assert.Throws<InvalidDataException>(() => { using var ignored = ParseOracle(StrictUtf8.GetBytes(text)); });

    [Theory]
    [InlineData("{\"version\":")]
    [InlineData("{}{}")]
    public void StrictTransportRejectsMalformedTruncatedAndTrailingJson(string text) =>
        Assert.Throws<JsonException>(() => { using var ignored = ParseOracle(StrictUtf8.GetBytes(text)); });

    [Fact]
    public void StrictTransportRejectsMalformedUtf8() =>
        Assert.Throws<DecoderFallbackException>(() => ParseOracle([0xc3, 0x28]));

    private static JsonDocument ParseOracle(byte[] bytes)
    {
        if (bytes.Length == 0 || bytes.Length > StdoutLimit) throw new InvalidDataException("Oracle capture bounds.");
        var document = JsonDocument.Parse(StrictUtf8.GetString(bytes), new JsonDocumentOptions { MaxDepth = 20 });
        try
        {
            var root = document.RootElement;
            Fields(root, "version", "generator", "seed", "coverageStart", "coverageEnd", "parameters", "datasets", "windows", "edges");
            if (root.GetProperty("version").GetString() != "e4-test-only-v1" || root.GetProperty("datasets").GetArrayLength() != 3 ||
                root.GetProperty("windows").GetArrayLength() != 12 || root.GetProperty("edges").GetArrayLength() != 11)
                throw new InvalidDataException("Oracle identity/count.");
            foreach (var item in root.GetProperty("datasets").EnumerateArray())
            { Fields(item, "interval", "count", "canonical"); Fields(item.GetProperty("canonical"), "bytes", "length", "sha256"); }
            foreach (var item in root.GetProperty("windows").EnumerateArray())
            {
                Fields(item, "interval", "range", "count", "visibleCount", "calculationStart", "visibleStart", "canonical", "visible", "indicators", "visibleIndicators");
                Fields(item.GetProperty("canonical"), "bytes", "length", "sha256"); Fields(item.GetProperty("visible"), "bytes", "length", "sha256");
            }
            var edgeNames = new HashSet<string>(StringComparer.Ordinal);
            foreach (var edge in root.GetProperty("edges").EnumerateArray())
            { Fields(edge, "id", "closes", "indicators"); if (!edgeNames.Add(edge.GetProperty("id").GetString()!)) throw new InvalidDataException("Duplicate edge."); }
            if (!edgeNames.SetEquals(new[] { "constant", "rising", "falling", "alternating", "mixed", "large", "minimum",
                "tie-0--1", "tie-0-1", "tie-1--1", "tie-1-1" })) throw new InvalidDataException("Missing/extra edge case.");
            return document;
        }
        catch { document.Dispose(); throw; }
    }

    private static void Fields(JsonElement item, params string[] names)
    {
        if (item.ValueKind != JsonValueKind.Object) throw new InvalidDataException("Expected object.");
        var remaining = names.ToHashSet(StringComparer.Ordinal);
        foreach (var property in item.EnumerateObject())
            if (!remaining.Remove(property.Name)) throw new InvalidDataException("Duplicate/extra field.");
        if (remaining.Count != 0) throw new InvalidDataException("Missing field.");
    }

    private static async Task<byte[]> RunOracleAsync()
    {
        var executable = Environment.GetEnvironmentVariable("AIQ_E4_REFERENCE_PYTHON");
        if (string.IsNullOrWhiteSpace(executable) || !Path.IsPathFullyQualified(executable) || !File.Exists(executable))
            throw new InvalidOperationException("Separate run authority must set AIQ_E4_REFERENCE_PYTHON to the exact approved absolute interpreter; no PATH fallback.");
        var script = Path.Combine(AppContext.BaseDirectory, "PythonIntegrationFixtures", "synthetic_snapshot_reference.py");
        if (!File.Exists(script)) throw new InvalidOperationException("Approved oracle fixture is missing.");
        var start = new ProcessStartInfo(executable) { UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true, WorkingDirectory = AppContext.BaseDirectory };
        foreach (var argument in new[] { "-I", "-B", script, "--proof", "aiq-synthetic-ohlcv-v1", "114" }) start.ArgumentList.Add(argument);
        using var process = new Process { StartInfo = start };
        using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(30));
        if (!process.Start()) throw new InvalidOperationException("Oracle launch failed.");
        try
        {
            var output = ReadBoundedAsync(process.StandardOutput.BaseStream, StdoutLimit, deadline);
            var error = ReadBoundedAsync(process.StandardError.BaseStream, StderrLimit, deadline);
            await Task.WhenAll(output, error, process.WaitForExitAsync(deadline.Token));
            if (process.ExitCode != 0 || (await error).Length != 0)
                throw new InvalidDataException("Oracle failed or emitted diagnostics; raw stderr suppressed.");
            return await output;
        }
        finally
        {
            deadline.Cancel();
            if (!process.HasExited)
            {
                try { process.Kill(entireProcessTree: true); }
                catch (Exception e) when (e is InvalidOperationException or System.ComponentModel.Win32Exception or NotSupportedException)
                { throw new InvalidOperationException("Termination uncertain: stop further runs and manually inspect owned processes.", e); }
                using var grace = new CancellationTokenSource(TimeSpan.FromSeconds(5));
                try { await process.WaitForExitAsync(grace.Token); }
                catch (OperationCanceledException)
                { throw new InvalidOperationException("Final wait expired: stop further runs and manually inspect owned processes."); }
            }
            // Observed root exit is NOT proof that every descendant exited. Operator supervises
            // outer deadline/resource pressure and manually inspects any uncertain cleanup.
        }
    }

    private static async Task<byte[]> ReadBoundedAsync(Stream stream, int maximum, CancellationTokenSource deadline)
    {
        using var result = new MemoryStream(); var buffer = new byte[8192];
        try
        {
            while (true)
            {
                deadline.Token.ThrowIfCancellationRequested();
                var count = await stream.ReadAsync(buffer.AsMemory(), deadline.Token);
                if (count == 0) return result.ToArray();
                if (count > maximum - result.Length) throw new InvalidDataException("Oracle byte cap exceeded.");
                result.Write(buffer, 0, count);
            }
        }
        catch { deadline.Cancel(); throw; }
    }

    private static HistoricalMarketDataRequest Request(HistoricalCandleInterval interval, HistoricalCandleRange range) =>
        new(CanonicalMarketSymbol.BtcLikeSimulation, interval, range, TimeSpan.FromSeconds(10));
    private static SyntheticHistoricalMarketDataProvider.GenerationBudget Budget(CancellationToken token = default) =>
        new(TimeProvider.System, TimeSpan.FromSeconds(10), token);
    private static HistoricalMarketDataProvenance Provenance(HistoricalMarketDataRequest request) =>
        new("independent-synthetic", End, End, false, true) { SyntheticIdentity = new("aiq-candles-v2", "btc-like-simulation",
            "simulation", "independent-synthetic", "simulated", null, null, null, null, "illustrative-price-unit", "illustrative-activity-unit",
            "aiq-synthetic-ohlcv-v1", 114, Start, End, SyntheticHistoricalMarketDataProvider.DatasetFingerprints[request.Interval],
            SyntheticHistoricalMarketDataProvider.WindowFingerprints[(request.Interval, request.Range)]) };
    private static HistoricalCandle Move(HistoricalCandle c, DateTimeOffset time) => new(c.Symbol, c.Interval, time, c.Open, c.High, c.Low, c.Close, c.Volume);
    private static HistoricalMarketDataProvenance PrivateProvenance() => new("e4-test-only", Start, Start, false, true);
    private static TechnicalIndicatorInputIdentity PrivateIdentity() => new("aiq-candles-v2", "private-research", "e4-test-only",
        CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, Start, new string('a', 64), new string('b', 64));
    private static HistoricalCandle[] PrivateCandles(IEnumerable<decimal> values) => values.Select((v, i) =>
        new HistoricalCandle(CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, Start.AddHours(i), v, v, v, v, 0)).ToArray();
    private static TechnicalIndicatorResult PrivateCalculate(IEnumerable<decimal> values) =>
        TechnicalIndicatorCalculator.Calculate(PrivateCandles(values), PrivateIdentity(), PrivateProvenance());
    private sealed class CancellingClock(CancellationTokenSource cancellation) : TimeProvider
    {
        private int calls;
        public override long TimestampFrequency => 1000;
        public override long GetTimestamp() { if (++calls == 3) cancellation.Cancel(); return calls; }
    }
}
