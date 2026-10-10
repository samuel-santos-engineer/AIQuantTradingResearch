using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Synthetic;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SyntheticHistoricalMarketDataProviderTests
{
    private static readonly Lazy<Task<JsonElement>> Reference = new(RunReferenceAsync);
    private static readonly Lazy<SyntheticHistoricalMarketDataProvider.CompleteDataset> Dataset = new(() =>
        SyntheticHistoricalMarketDataProvider.GenerateCompleteDataset(Budget()));

    private static SyntheticHistoricalMarketDataProvider.GenerationBudget Budget() =>
        new(TimeProvider.System, TimeSpan.FromSeconds(10), CancellationToken.None);

    private static HistoricalMarketDataRequest Request(HistoricalCandleInterval interval = HistoricalCandleInterval.OneHour,
        HistoricalCandleRange range = HistoricalCandleRange.ThirtyDays, CanonicalMarketSymbol symbol = CanonicalMarketSymbol.BtcLikeSimulation,
        int deadline = 10) => new(symbol, interval, range, TimeSpan.FromSeconds(deadline));

    [Theory]
    [InlineData(HistoricalCandleInterval.OneHour, "1h", 8160, 596926, "5b187d5bea1fd202077c33713855f9c0518446ce6a8864a5249daa0e7e45dc29")]
    [InlineData(HistoricalCandleInterval.FourHours, "4h", 2040, 151407, "d713767c7afeb5d3f535e8276e1cecb905b3f2d6c096aac5a62214f62ccc22ec")]
    [InlineData(HistoricalCandleInterval.OneDay, "1d", 340, 25957, "0fc878f11193073aa88f71b026825c9f7cbb4cb18d5c87fec2d66a9e44291c81")]
    public async Task CompleteCanonicalBytesMatchIndependentCPythonAndFrozenCandidates(
        HistoricalCandleInterval interval, string literal, int count, int byteCount, string fingerprint)
    {
        var reference = (await Reference.Value).GetProperty("datasets").GetProperty(literal);
        var rows = Dataset.Value.For(interval);
        var bytes = SyntheticHistoricalMarketDataProvider.SerializeDataset(interval, rows, Budget());
        Assert.Equal(count, rows.Count); Assert.Equal(count, reference.GetProperty("count").GetInt32());
        Assert.Equal(byteCount, bytes.Length); Assert.Equal(byteCount, reference.GetProperty("byteCount").GetInt32());
        Assert.Equal(Convert.FromBase64String(reference.GetProperty("bytes").GetString()!), bytes);
        Assert.Equal(fingerprint, Hash(bytes)); Assert.Equal(fingerprint, reference.GetProperty("fingerprint").GetString());
        Assert.Equal(fingerprint, SyntheticHistoricalMarketDataProvider.DatasetFingerprints[interval]);
        Assert.Equal(fingerprint, SyntheticPublicationPolicy.DatasetFingerprint(interval, rows));
        Assert.Equal((byte)'\n', bytes[^1]); Assert.DoesNotContain((byte)'\r', bytes);
        Assert.False(bytes.AsSpan().StartsWith(new byte[] { 0xef, 0xbb, 0xbf }));
    }

    // Independent CPython results recorded before authoring the C# generator, not copied from its outputs.
    public static TheoryData<HistoricalCandleInterval, HistoricalCandleRange, int, int, int, string, string> Windows => new()
    {
        { HistoricalCandleInterval.OneHour, HistoricalCandleRange.OneDay, 274, 24, 20578, "52008a41d0581292bc72b29e6fd1273075b08fedc95b9c82bf7570784cb92cfb", "36ac833f75709662a319833cc4b76eec2644912e9e81213145d52b3b923afbc6" },
        { HistoricalCandleInterval.OneHour, HistoricalCandleRange.SevenDays, 418, 168, 31107, "1da0ac6ac728048f1751f2a0c0f48b5db8fa0c1942174f51afcadb1e29157a04", "91aaf2d564ad150d321d8e94587661f1c1b9df5fd2d9cfc2e756d7188febe71f" },
        { HistoricalCandleInterval.OneHour, HistoricalCandleRange.ThirtyDays, 970, 720, 71459, "2c14463a88aa3cfd452cfd5d6539b78f12b1f8110cd03791c85da15e82a564c4", "36b544790c2b90161cfd729a654c9314e2b477eade8cb0bb109162ce9bb934ac" },
        { HistoricalCandleInterval.OneHour, HistoricalCandleRange.NinetyDays, 2410, 2160, 176720, "0346dcb017ad45a4867d34d458db777d02215843f1a555ef7038bf9b80140d9e", "b71c04ace336fccd77f8a317372a0c13626b371aa32700cfe0ecf4918e75eb12" },
        { HistoricalCandleInterval.FourHours, HistoricalCandleRange.OneDay, 256, 6, 19493, "513ee57961faa5bb633f6f9bfd36128b8af366d1ba276fa79dab662889e2d8f8", "c77eabc3699e29bb29a0581f8baa84f3c14761fb7173bcb67cd48b19283d21d1" },
        { HistoricalCandleInterval.FourHours, HistoricalCandleRange.SevenDays, 292, 42, 22157, "e16132d5648cc79f086a254b933dd08b9ea6afc4dec7c370089cc389c88c7bc2", "f59bff85e6c3fc0401138f901e2df85e05f7068b2e1a743b19ecdb745189cb03" },
        { HistoricalCandleInterval.FourHours, HistoricalCandleRange.ThirtyDays, 430, 180, 32369, "9bd4498592a669f394173ca46f4a20a2bf79e3e5fea306d72ef208f038e37a25", "75d8030c3714d7e18fedfe9bb2a622321e82ab377ddc7bacab14e3f8a63b0814" },
        { HistoricalCandleInterval.FourHours, HistoricalCandleRange.NinetyDays, 790, 540, 59006, "b2cf082d689ab93e38ed879f5268aff6f1cac84769044a894b6c4cd25c99397f", "40795cf048985613d3379503114a29178b1b8ee49da5fd3dc47e5eee91367d38" },
        { HistoricalCandleInterval.OneDay, HistoricalCandleRange.OneDay, 251, 1, 19373, "a5e98a46b5aaa62af444368ff269855886f5f84dd8e82d33d1087067ed2237dd", "e32e09e717b1aff555e8cc620d1ff40cc64c232c866f1391b89bb5c0cc732873" },
        { HistoricalCandleInterval.OneDay, HistoricalCandleRange.SevenDays, 257, 7, 19823, "735aee58440fcf4f4ba50c3000c9d54378abce696cc464c6f9518be5d1ed7e38", "2339827d95c7469e890c842fcf58c5d862c8c66199df0770e294eeddff9d4373" },
        { HistoricalCandleInterval.OneDay, HistoricalCandleRange.ThirtyDays, 280, 30, 21549, "43451b12a772287c65536556f400e4bd0676505c169a8d5096761787a6ad3f38", "4d9e7cb57dc6834ad7ef35265191d7c465c1bfad51fafcaef2871ee831124d14" },
        { HistoricalCandleInterval.OneDay, HistoricalCandleRange.NinetyDays, 340, 90, 26049, "85e6e5952eb35fc37e741eed3d0b4169fe933954cd4eb7dc5531ef5013e35cdf", "27bbc5f787b71b064986a457da0ccae673cf1b41641f87df162b5bdbf2be61b8" },
    };

    [Theory]
    [MemberData(nameof(Windows))]
    public async Task AllTwelveCalculationAndVisibleWindowsMatchCompleteIndependentBytes(
        HistoricalCandleInterval interval, HistoricalCandleRange range, int calculationCount, int visibleCount,
        int windowByteCount, string windowFingerprint, string visibleFingerprint)
    {
        var reference = (await Reference.Value).GetProperty("windows").GetProperty(SyntheticPublicationPolicy.IntervalLiteral(interval) + "/" + SyntheticPublicationPolicy.RangeLiteral(range));
        var request = Request(interval, range);
        var result = await new SyntheticHistoricalMarketDataProvider(new FixedClock()).GetHistoricalCandlesAsync(request);
        Assert.True(result.IsSuccess, result.Failure?.ToString());
        var rows = result.Candles!;
        Assert.Equal(calculationCount, rows.Count); Assert.Equal(visibleCount + 250, rows.Count);
        Assert.Equal(reference.GetProperty("count").GetInt32(), rows.Count); Assert.Equal(reference.GetProperty("visibleCount").GetInt32(), visibleCount);
        Assert.Equal(reference.GetProperty("calculationStart").GetString(), SyntheticPublicationPolicy.Timestamp(rows[0].OpenTimeUtc));
        Assert.Equal(reference.GetProperty("visibleStart").GetString(), SyntheticPublicationPolicy.Timestamp(rows[250].OpenTimeUtc));
        Assert.Equal(SyntheticPublicationPolicy.CoverageEnd, rows[^1].OpenTimeUtc.AddHours(SyntheticPublicationPolicy.Hours(interval)));
        var windowBytes = SyntheticHistoricalMarketDataProvider.SerializeWindow(request, rows, Budget());
        var visibleBytes = SyntheticHistoricalMarketDataProvider.SerializeVisibleRows(request, rows, Budget());
        Assert.Equal(windowByteCount, windowBytes.Length);
        Assert.Equal(Convert.FromBase64String(reference.GetProperty("bytes").GetString()!), windowBytes);
        Assert.Equal(Convert.FromBase64String(reference.GetProperty("visible").GetProperty("bytes").GetString()!), visibleBytes);
        Assert.Equal(windowFingerprint, Hash(windowBytes)); Assert.Equal(windowFingerprint, reference.GetProperty("fingerprint").GetString());
        Assert.Equal(visibleFingerprint, Hash(visibleBytes)); Assert.Equal(visibleCount, Encoding.UTF8.GetString(visibleBytes).Count(c => c == '\n'));
        Assert.Equal(windowFingerprint, SyntheticHistoricalMarketDataProvider.WindowFingerprints[(interval, range)]);
        Assert.Equal(visibleFingerprint, SyntheticHistoricalMarketDataProvider.VisibleRowFingerprints[(interval, range)]);
        SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy().Validate(request, rows, result.Provenance!);
        AssertProvenance(result.Provenance!);
        if (interval == HistoricalCandleInterval.OneDay && range == HistoricalCandleRange.NinetyDays)
            Assert.Equal(new DateTimeOffset(2025, 10, 26, 0, 0, 0, TimeSpan.Zero), rows[0].OpenTimeUtc);
    }

    [Fact]
    public async Task ApprovedRowVectorsMatchIndependentReferenceAndFrozenHashes()
    {
        var reference = (await Reference.Value).GetProperty("rowVectors");
        var hourly = Dataset.Value.For(HistoricalCandleInterval.OneHour); var four = Dataset.Value.For(HistoricalCandleInterval.FourHours);
        var actual = new[] { Row(hourly[0]), Row(four[0]), Row(hourly[^1]) };
        var expected = new[]
        {
            "2025-10-26T00:00:00Z,49969.1046,49973.6524,49961.7725,49962.8831,366.310\n",
            "2025-10-26T00:00:00Z,49969.1046,49978.4032,49959.6323,49963.6863,1769.191\n",
            "2026-09-30T23:00:00Z,49977.1349,49979.5310,49968.9920,49973.5849,115.148\n",
        };
        var hashes = new[] { "987c0eb293692c988474f3e29c2e2324aef42ffcbbc8d448dc7f7a48a3d5f744", "75a3ee54c5c4f12ac3c1a39d732899cc977747cfa146605f7f2a4357812da79a", "6b18c0a41339ec6e754d70475b9b6d52c1e18c567b6f8cfdeb3323caa5e9b708" };
        for (var i = 0; i < actual.Length; i++) { Assert.Equal(expected[i], actual[i]); Assert.Equal(expected[i], reference[i].GetString()); Assert.Equal(hashes[i], Hash(Encoding.ASCII.GetBytes(actual[i]))); }
    }

    [Fact]
    public void EveryCompleteGridPreservesInvariantsContinuityAndIndependentAggregation()
    {
        var hourly = Dataset.Value.For(HistoricalCandleInterval.OneHour);
        Assert.Equal(10540, Enum.GetValues<HistoricalCandleInterval>().Sum(i => Dataset.Value.For(i).Count));
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>())
        {
            var rows = Dataset.Value.For(interval); var width = SyntheticPublicationPolicy.Hours(interval);
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i]; Assert.Equal(TimeSpan.Zero, row.OpenTimeUtc.Offset);
                Assert.Equal(SyntheticPublicationPolicy.CoverageStart.AddHours(i * width), row.OpenTimeUtc);
                Assert.True(row.Low > 0 && row.Low <= Math.Min(row.Open, row.Close) && row.High >= Math.Max(row.Open, row.Close) && row.Volume >= 0);
                Assert.Equal(row.Open, decimal.Round(row.Open, 4)); Assert.Equal(row.Volume, decimal.Round(row.Volume, 3));
                if (i > 0) Assert.Equal(rows[i - 1].Close, row.Open);
                var group = hourly.Skip(i * width).Take(width).ToArray();
                Assert.Equal(group[0].Open, row.Open); Assert.Equal(group[^1].Close, row.Close);
                Assert.Equal(group.Max(c => c.High), row.High); Assert.Equal(group.Min(c => c.Low), row.Low); Assert.Equal(group.Sum(c => c.Volume), row.Volume);
            }
        }
        Assert.Equal(hourly[^1].Close * 10000m, SyntheticHistoricalMarketDataProvider.PriceTicks(8160));
    }

    [Theory]
    [InlineData(HistoricalCandleInterval.OneHour)]
    [InlineData(HistoricalCandleInterval.FourHours)]
    [InlineData(HistoricalCandleInterval.OneDay)]
    public async Task LocaleClockRequestOrderAndOverlapCannotChangeCandlesOrFingerprint(HistoricalCandleInterval interval)
    {
        var originalCulture = CultureInfo.CurrentCulture; var originalUi = CultureInfo.CurrentUICulture;
        try
        {
            var byRange = new Dictionary<HistoricalCandleRange, HistoricalMarketDataResponse>();
            var first = new SyntheticHistoricalMarketDataProvider(new FixedClock());
            foreach (var range in Enum.GetValues<HistoricalCandleRange>()) byRange.Add(range, await first.GetHistoricalCandlesAsync(Request(interval, range)));
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR"); CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("tr-TR");
            var second = new SyntheticHistoricalMarketDataProvider(new FixedClock { Now = DateTimeOffset.UnixEpoch });
            foreach (var range in Enum.GetValues<HistoricalCandleRange>().Reverse())
            {
                var later = await second.GetHistoricalCandlesAsync(Request(interval, range));
                Assert.True(later.IsSuccess, later.Failure?.ToString()); Assert.Equal(byRange[range].Candles!, later.Candles!);
                Assert.Equal(byRange[range].Provenance!.SyntheticIdentity, later.Provenance!.SyntheticIdentity);
                Assert.NotEqual(byRange[range].Provenance!.AcquiredAtUtc, later.Provenance.AcquiredAtUtc);
                var longest = byRange[HistoricalCandleRange.NinetyDays].Candles!.ToDictionary(c => c.OpenTimeUtc);
                Assert.All(later.Candles!, candle => Assert.Equal(longest[candle.OpenTimeUtc], candle));
            }
        }
        finally { CultureInfo.CurrentCulture = originalCulture; CultureInfo.CurrentUICulture = originalUi; }
    }

    [Fact]
    public void ConstructionAndQualificationLookupHaveNoGenerationOrExternalProviderDependencies()
    {
        _ = new SyntheticHistoricalMarketDataProvider(new ThrowingClock());
        _ = SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy();
        Assert.Equal(3, SyntheticHistoricalMarketDataProvider.DatasetFingerprints.Count);
        Assert.Equal(12, SyntheticHistoricalMarketDataProvider.WindowFingerprints.Count); Assert.Equal(12, SyntheticHistoricalMarketDataProvider.VisibleRowFingerprints.Count);
        Assert.All(typeof(SyntheticHistoricalMarketDataProvider).GetConstructors().SelectMany(c => c.GetParameters()), parameter => Assert.Equal(typeof(TimeProvider), parameter.ParameterType));
        Assert.True(typeof(IIndependentSyntheticHistoricalMarketDataProvider).IsAssignableFrom(typeof(SyntheticHistoricalMarketDataProvider)));
        Assert.Throws<NotSupportedException>(() => ((IDictionary<HistoricalCandleInterval, string>)SyntheticHistoricalMarketDataProvider.DatasetFingerprints).Add((HistoricalCandleInterval)999, "bad"));
        Assert.True(typeof(SyntheticHistoricalMarketDataProvider).GetField(nameof(SyntheticHistoricalMarketDataProvider.Seed))!.IsLiteral);
        Assert.Equal(114UL, SyntheticHistoricalMarketDataProvider.Seed); Assert.Equal("aiq-synthetic-ohlcv-v1", SyntheticHistoricalMarketDataProvider.GeneratorVersion);
    }

    [Fact]
    public async Task AlteredSeedAndVersionAreDetectedWithoutMakingProductionConstantsConfigurable()
    {
        var reference = await Reference.Value;
        Assert.Equal("6ca2206a41985e19b42f31aabb3dc3a37f25768021eca0b36015e09cc320cd5a", reference.GetProperty("hourlyRowsFingerprint").GetString());
        Assert.Equal("a09a0a426feec51871cab213a859fd17dc430ce079d9c95e6edcc2845fe2c186", reference.GetProperty("alteredSeedRowsFingerprint").GetString());
        Assert.Equal("72c877712ed75a8c08958fa19de6457d7985f94e1fafb3094a2d787c3e20c6a7", reference.GetProperty("alteredVersionRowsFingerprint").GetString());
        var productionRows = Encoding.ASCII.GetBytes(string.Concat(Dataset.Value.For(HistoricalCandleInterval.OneHour).Select(Row)));
        Assert.Equal(reference.GetProperty("hourlyRowsFingerprint").GetString(), Hash(productionRows));
        Assert.NotEqual(reference.GetProperty("alteredSeedRowsFingerprint").GetString(), Hash(productionRows)); Assert.NotEqual(reference.GetProperty("alteredVersionRowsFingerprint").GetString(), Hash(productionRows));
    }

    [Theory]
    [InlineData(CanonicalMarketSymbol.BtcUsd)]
    [InlineData(CanonicalMarketSymbol.EthUsd)]
    public async Task ObservedRequestsFailBeforeGeneration(CanonicalMarketSymbol symbol)
    {
        var result = await new SyntheticHistoricalMarketDataProvider(new ThrowingClock()).GetHistoricalCandlesAsync(Request(symbol: symbol));
        Assert.Equal(HistoricalMarketDataFailure.InvalidRequest, result.Failure); Assert.Null(result.Candles); Assert.Null(result.Provenance);
    }

    [Fact]
    public async Task NullAndMalformedTypedRequestsAreRejected()
    {
        var result = await new SyntheticHistoricalMarketDataProvider(new ThrowingClock()).GetHistoricalCandlesAsync(null!);
        Assert.Equal(HistoricalMarketDataFailure.InvalidRequest, result.Failure);
        Assert.Throws<ArgumentOutOfRangeException>(() => Request((HistoricalCandleInterval)999));
        Assert.Throws<ArgumentOutOfRangeException>(() => Request(range: (HistoricalCandleRange)999));
        Assert.Throws<ArgumentOutOfRangeException>(() => Request(deadline: 0));
        Assert.Throws<ArgumentOutOfRangeException>(() => SyntheticHistoricalMarketDataProvider.PriceTicks(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => SyntheticHistoricalMarketDataProvider.PriceTicks(8161));
    }

    [Fact]
    public async Task PreCancelledAndMidGenerationCancellationNeverReturnPartialData()
    {
        using var cancelled = new CancellationTokenSource(); cancelled.Cancel();
        var first = await new SyntheticHistoricalMarketDataProvider(new ThrowingClock()).GetHistoricalCandlesAsync(Request(), cancelled.Token);
        Assert.Equal(HistoricalMarketDataFailure.Cancelled, first.Failure); Assert.Null(first.Candles); Assert.Null(first.Provenance);
        using var during = new CancellationTokenSource(); var clock = new CountingClock { Cancel = during, CancelAt = 20 };
        var provider = new SyntheticHistoricalMarketDataProvider(clock);
        var second = await provider.GetHistoricalCandlesAsync(Request(), during.Token);
        Assert.Equal(20, clock.Calls); Assert.Equal(HistoricalMarketDataFailure.Cancelled, second.Failure); Assert.Null(second.Candles); Assert.Null(second.Provenance);
        var retry = await provider.GetHistoricalCandlesAsync(Request());
        Assert.True(retry.IsSuccess, retry.Failure?.ToString()); Assert.Equal(970, retry.Candles!.Count);
    }

    [Theory]
    [InlineData(1, 1001)]
    [InlineData(30, 5001)]
    public async Task MonotonicDeadlineAndTenSecondCapBoundExecution(int requestSeconds, long tickStep)
    {
        var clock = new CountingClock { TickStep = tickStep };
        var result = await new SyntheticHistoricalMarketDataProvider(clock).GetHistoricalCandlesAsync(Request(deadline: requestSeconds));
        Assert.Equal(HistoricalMarketDataFailure.Timeout, result.Failure); Assert.Null(result.Candles); Assert.Null(result.Provenance);
        Assert.InRange(clock.Calls, 2, 3);
    }

    [Fact]
    public void MalformedCompleteGridsPartialGroupsAndOverflowFailClosed()
    {
        var original = Dataset.Value.For(HistoricalCandleInterval.OneHour); var first = original[0];
        Assert.Throws<InvalidDataException>(() => SyntheticHistoricalMarketDataProvider.Aggregate(original.Take(8159).ToArray(), HistoricalCandleInterval.FourHours, Budget()));
        Assert.Throws<ArgumentException>(() => SyntheticHistoricalMarketDataProvider.Aggregate(original, HistoricalCandleInterval.OneHour, Budget()));
        var replacements = new[]
        {
            new HistoricalCandle(first.Symbol, first.Interval, first.OpenTimeUtc.AddHours(1), first.Open, first.High, first.Low, first.Close, first.Volume),
            new HistoricalCandle(CanonicalMarketSymbol.BtcUsd, first.Interval, first.OpenTimeUtc, first.Open, first.High, first.Low, first.Close, first.Volume),
            new HistoricalCandle(first.Symbol, first.Interval, first.OpenTimeUtc, first.Open + 0.00001m, first.High, first.Low, first.Close, first.Volume),
            new HistoricalCandle(first.Symbol, first.Interval, first.OpenTimeUtc, first.Open, first.High, first.Low, first.Close + 0.0001m, first.Volume),
            new HistoricalCandle(first.Symbol, first.Interval, first.OpenTimeUtc, first.Open, first.High, first.Low, first.Close, first.Volume + 0.0001m),
        };
        foreach (var replacement in replacements)
        {
            var rows = original.ToArray(); rows[0] = replacement;
            Assert.Throws<InvalidDataException>(() => SyntheticHistoricalMarketDataProvider.SerializeDataset(HistoricalCandleInterval.OneHour, rows, Budget()));
            Assert.Throws<InvalidDataException>(() => SyntheticHistoricalMarketDataProvider.Aggregate(rows, HistoricalCandleInterval.OneDay, Budget()));
        }
        var duplicates = original.ToArray(); duplicates[1] = duplicates[0];
        Assert.Throws<InvalidDataException>(() => SyntheticHistoricalMarketDataProvider.Aggregate(duplicates, HistoricalCandleInterval.FourHours, Budget()));
        var overflowing = original.ToArray(); overflowing[0] = new(first.Symbol, first.Interval, first.OpenTimeUtc, first.Open, first.High, first.Low, first.Close, decimal.MaxValue);
        Assert.Throws<OverflowException>(() => SyntheticHistoricalMarketDataProvider.Aggregate(overflowing, HistoricalCandleInterval.FourHours, Budget()));
        Assert.Throws<InvalidDataException>(() => SyntheticHistoricalMarketDataProvider.SerializeDataset(HistoricalCandleInterval.OneHour, original.Append(first).ToArray(), Budget()));
    }

    [Fact]
    public async Task WindowAndProvenanceContaminationCannotBePublished()
    {
        var request = Request(); var response = await new SyntheticHistoricalMarketDataProvider().GetHistoricalCandlesAsync(request);
        Assert.True(response.IsSuccess, response.Failure?.ToString());
        Assert.Throws<InvalidDataException>(() => SyntheticHistoricalMarketDataProvider.SerializeWindow(request, response.Candles!.Skip(1).ToArray(), Budget()));
        var identity = response.Provenance!.SyntheticIdentity!;
        foreach (var contaminated in new[] { identity with { Source = "Vike" }, identity with { Seed = 115 }, identity with { PriceUnit = "USD" }, identity with { WindowFingerprint = new string('a', 64) } })
            Assert.Throws<ArgumentException>(() => HistoricalMarketDataResponse.Available(request, response.Candles!, response.Provenance with { SyntheticIdentity = contaminated }));
    }

    [Fact]
    public void GenerationAggregationAndSerializationRespectCancellation()
    {
        // 128 complete-hourly-grid validation checks precede the aggregate/serialization loops.
        // Check 131 cancels after each loop has done work, rather than only during input validation.
        using var cancellation = new CancellationTokenSource(); var clock = new CountingClock { Cancel = cancellation, CancelAt = 131 };
        var budget = new SyntheticHistoricalMarketDataProvider.GenerationBudget(clock, TimeSpan.FromSeconds(10), cancellation.Token);
        Assert.Throws<OperationCanceledException>(() => SyntheticHistoricalMarketDataProvider.Aggregate(Dataset.Value.For(HistoricalCandleInterval.OneHour), HistoricalCandleInterval.OneDay, budget));
        Assert.Equal(131, clock.Calls);
        using var serializeCancellation = new CancellationTokenSource(); var serializeClock = new CountingClock { Cancel = serializeCancellation, CancelAt = 131 };
        var serializeBudget = new SyntheticHistoricalMarketDataProvider.GenerationBudget(serializeClock, TimeSpan.FromSeconds(10), serializeCancellation.Token);
        Assert.Throws<OperationCanceledException>(() => SyntheticHistoricalMarketDataProvider.SerializeDataset(HistoricalCandleInterval.OneHour, Dataset.Value.For(HistoricalCandleInterval.OneHour), serializeBudget));
        Assert.Equal(131, serializeClock.Calls);
    }

    private static void AssertProvenance(HistoricalMarketDataProvenance provenance)
    {
        Assert.Equal("independent-synthetic", provenance.Provider); Assert.False(provenance.IsStale); Assert.True(provenance.ContainsOnlyClosedCandles);
        Assert.Equal(TimeSpan.Zero, provenance.AcquiredAtUtc.Offset); Assert.Equal(provenance.AcquiredAtUtc, provenance.LastValidatedAtUtc);
        var identity = provenance.SyntheticIdentity!;
        Assert.Equal("aiq-candles-v2", identity.CanonicalVersion); Assert.Equal("btc-like-simulation", identity.Instrument);
        Assert.Equal("simulation", identity.MarketType); Assert.Equal("independent-synthetic", identity.Source); Assert.Equal("simulated", identity.Status);
        Assert.Null(identity.Venue); Assert.Null(identity.Base); Assert.Null(identity.Quote); Assert.Null(identity.Settlement); Assert.Null(identity.CalculationVersion);
        Assert.Equal("illustrative-price-unit", identity.PriceUnit); Assert.Equal("illustrative-activity-unit", identity.VolumeUnit);
        Assert.Equal("aiq-synthetic-ohlcv-v1", identity.GeneratorVersion); Assert.Equal(114UL, identity.Seed);
        Assert.Equal(SyntheticPublicationPolicy.CoverageStart, identity.CoverageStartUtc); Assert.Equal(SyntheticPublicationPolicy.CoverageEnd, identity.CoverageEndExclusiveUtc);
    }

    private static string Row(HistoricalCandle candle) => string.Create(CultureInfo.InvariantCulture,
        $"{candle.OpenTimeUtc:yyyy-MM-dd'T'HH:mm:ss'Z'},{candle.Open:F4},{candle.High:F4},{candle.Low:F4},{candle.Close:F4},{candle.Volume:F3}\n");
    private static string Hash(byte[] bytes) => Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
    private sealed class FixedClock : TimeProvider { public DateTimeOffset Now { get; init; } = new(2026, 10, 10, 0, 0, 0, TimeSpan.Zero); public override DateTimeOffset GetUtcNow() => Now; }
    private sealed class ThrowingClock : TimeProvider { public override long GetTimestamp() => throw new InvalidOperationException("Generation must not occur here."); }
    private sealed class CountingClock : TimeProvider
    {
        public int Calls { get; private set; }
        public long TickStep { get; init; } = 1;
        public CancellationTokenSource? Cancel { get; init; }
        public int CancelAt { get; init; }
        public override long TimestampFrequency => 1000;
        public override long GetTimestamp() { Calls++; if (Calls == CancelAt) Cancel!.Cancel(); return checked(Calls * TickStep); }
    }

    private static async Task<JsonElement> RunReferenceAsync()
    {
        var start = new ProcessStartInfo(Environment.GetEnvironmentVariable("AIQ_WP02_REFERENCE_PYTHON") ?? "python")
        { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true, StandardOutputEncoding = Encoding.UTF8, StandardErrorEncoding = Encoding.UTF8 };
        foreach (var argument in new[] { "-I", "-B", "-c", ReferenceScript }) start.ArgumentList.Add(argument);
        using var process = new Process { StartInfo = start }; using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(20));
        Assert.True(process.Start());
        try
        {
            var output = ReadBoundedAsync(process.StandardOutput, 4 * 1024 * 1024, deadline.Token);
            var error = ReadBoundedAsync(process.StandardError, 16 * 1024, deadline.Token);
            await Task.WhenAll(output, error, process.WaitForExitAsync(deadline.Token));
            Assert.True(process.ExitCode == 0, await error);
            return JsonSerializer.Deserialize<JsonElement>(await output);
        }
        finally
        {
            if (!process.HasExited) { process.Kill(entireProcessTree: true); await process.WaitForExitAsync(); }
        }
    }

    private static async Task<string> ReadBoundedAsync(StreamReader reader, int maximum, CancellationToken cancellationToken)
    {
        var buffer = new char[4096]; var result = new StringBuilder();
        while (true)
        {
            var count = await reader.ReadAsync(buffer.AsMemory(), cancellationToken);
            if (count == 0) return result.ToString();
            Assert.True(result.Length + count <= maximum, "Independent reference output exceeded its bound.");
            result.Append(buffer, 0, count);
        }
    }

    // Reference authored directly from the frozen WP01 equations before the C# implementation.
    // CPython standard library only; it receives no production output and imports no repository code.
    // Kept in this authorized test file so no Python source, fixtures or generated datasets are added.
    private const string ReferenceScript = """
import base64, hashlib, json, struct
from datetime import datetime, timedelta, timezone

ORIGIN = datetime(2025, 10, 26, tzinfo=timezone.utc)
VERSION = 'aiq-synthetic-ohlcv-v1'

def stamp(index):
    return (ORIGIN + timedelta(hours=index)).strftime('%Y-%m-%dT%H:%M:%SZ')

def build(seed=114, version=VERSION):
    def lane(index, channel, modulus):
        counter = version.encode('ascii') + b'\0' + struct.pack('>QQB', seed, index, channel)
        word = int.from_bytes(hashlib.sha256(counter).digest()[:4], 'big')
        return word * modulus // (1 << 32)
    def boundary(index):
        remainder = index % 480
        triangle = remainder - 120 if remainder <= 240 else 360 - remainder
        return 500000000 + 2500 * triangle + lane(index, 0, 200001) - 100000
    prices = [boundary(i) for i in range(8161)]
    return [(i, prices[i], max(prices[i], prices[i+1]) + 1 + lane(i, 1, 50000),
             min(prices[i], prices[i+1]) - 1 - lane(i, 2, 50000), prices[i+1],
             100000 + lane(i, 3, 1000000)) for i in range(8160)]

def rollup(hourly, width):
    assert len(hourly) % width == 0
    return [(group[0][0], group[0][1], max(r[2] for r in group), min(r[3] for r in group),
             group[-1][4], sum(r[5] for r in group))
            for group in (hourly[j:j+width] for j in range(0, len(hourly), width))]

def fixed(value, scale):
    unit = 10 ** scale
    return str(value // unit) + '.' + str(value % unit).zfill(scale)

def row_bytes(rows):
    return ''.join(stamp(row[0]) + ',' + ','.join(fixed(n, 4) for n in row[1:5]) + ',' +
                   fixed(row[5], 3) + '\n' for row in rows).encode('ascii')

def header(interval, window=False):
    lines = ['aiq-synthetic-window-v1' if window else 'aiq-synthetic-dataset-v1',
        'canonical=aiq-candles-v2', 'source=independent-synthetic', 'status=simulated',
        'instrument=btc-like-simulation', 'marketType=simulation', 'venue=none', 'base=none',
        'quote=none', 'settlement=none', 'priceUnit=illustrative-price-unit',
        'volumeUnit=illustrative-activity-unit', 'generator=aiq-synthetic-ohlcv-v1',
        'seed=114', 'start=2025-10-26T00:00:00Z', 'endExclusive=2026-10-01T00:00:00Z',
        'interval=' + interval, 'priceScale=4', 'volumeScale=3']
    return ('\n'.join(lines) + '\n').encode('ascii')

COLUMNS = b'columns=openTimeUtc,open,high,low,close,volume\n'
def evidence(data):
    return dict(byteCount=len(data), fingerprint=hashlib.sha256(data).hexdigest(),
                bytes=base64.b64encode(data).decode('ascii'))

hourly = build()
datasets = {}
windows = {}
for interval, width in [('1h',1), ('4h',4), ('1d',24)]:
    rows = hourly if width == 1 else rollup(hourly, width)
    datasets[interval] = dict(count=len(rows), **evidence(header(interval) + COLUMNS + row_bytes(rows)))
    for days in (1,7,30,90):
        visible_count = 24 * days // width
        visible_index = len(rows) - visible_count
        calculation_index = visible_index - 250
        calculation = rows[calculation_index:]
        extra = ('range=' + str(days) + 'D\nvisibleStart=' + stamp(visible_index * width) +
                 '\ncalculationStart=' + stamp(calculation_index * width) + '\nwarmup=250\n').encode('ascii')
        windows[interval + '/' + str(days) + 'D'] = dict(
            count=len(calculation), visibleCount=visible_count,
            calculationStart=stamp(calculation_index * width), visibleStart=stamp(visible_index * width),
            visible=evidence(row_bytes(rows[visible_index:])),
            **evidence(header(interval, True) + extra + COLUMNS + row_bytes(calculation)))

print(json.dumps(dict(datasets=datasets, windows=windows,
    rowVectors=[row_bytes([hourly[0]]).decode('ascii'),
                row_bytes([rollup(hourly,4)[0]]).decode('ascii'),
                row_bytes([hourly[-1]]).decode('ascii')],
    alteredSeedRowsFingerprint=hashlib.sha256(row_bytes(build(seed=115))).hexdigest(),
    alteredVersionRowsFingerprint=hashlib.sha256(row_bytes(build(version='aiq-synthetic-ohlcv-v2'))).hexdigest(),
    hourlyRowsFingerprint=hashlib.sha256(row_bytes(hourly)).hexdigest()), separators=(',',':')))
""";
}
