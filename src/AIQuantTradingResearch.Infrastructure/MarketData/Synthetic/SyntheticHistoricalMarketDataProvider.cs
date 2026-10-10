using System.Buffers.Binary;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.Synthetic;

/// <summary>
/// Independent illustrative OHLCV, defined solely by the approved counter equations.
/// Construction and qualification lookup generate no candles. Each acquisition generates
/// one complete hourly dataset and its complete aggregates; it retains no data between requests.
/// Registration, process isolation and publication activation belong to WP07.
/// </summary>
public sealed class SyntheticHistoricalMarketDataProvider : IIndependentSyntheticHistoricalMarketDataProvider
{
    public const string GeneratorVersion = "aiq-synthetic-ohlcv-v1";
    public const ulong Seed = 114;
    public const int HourlyCount = 8160;
    private const int MaximumSerializationBytes = 2 * 1024 * 1024;
    private static readonly TimeSpan MaximumExecution = TimeSpan.FromSeconds(10);
    private static readonly UTF8Encoding CanonicalEncoding = new(false, true);
    private readonly TimeProvider clock;

    // Fixed qualification values independently reproduced by the CPython reference retained
    // in SyntheticHistoricalMarketDataProviderTests.cs. Tests compare COMPLETE canonical bytes.
    // These are immutable version constants, not caller-configurable authorization or generated assets.
    public static IReadOnlyDictionary<HistoricalCandleInterval, string> DatasetFingerprints { get; } =
        new ReadOnlyDictionary<HistoricalCandleInterval, string>(new Dictionary<HistoricalCandleInterval, string>
        {
            [HistoricalCandleInterval.OneHour] = "5b187d5bea1fd202077c33713855f9c0518446ce6a8864a5249daa0e7e45dc29",
            [HistoricalCandleInterval.FourHours] = "d713767c7afeb5d3f535e8276e1cecb905b3f2d6c096aac5a62214f62ccc22ec",
            [HistoricalCandleInterval.OneDay] = "0fc878f11193073aa88f71b026825c9f7cbb4cb18d5c87fec2d66a9e44291c81",
        });

    public static IReadOnlyDictionary<(HistoricalCandleInterval Interval, HistoricalCandleRange Range), string> WindowFingerprints { get; } = QualificationMap(
        "52008a41d0581292bc72b29e6fd1273075b08fedc95b9c82bf7570784cb92cfb",
        "1da0ac6ac728048f1751f2a0c0f48b5db8fa0c1942174f51afcadb1e29157a04",
        "2c14463a88aa3cfd452cfd5d6539b78f12b1f8110cd03791c85da15e82a564c4",
        "0346dcb017ad45a4867d34d458db777d02215843f1a555ef7038bf9b80140d9e",
        "513ee57961faa5bb633f6f9bfd36128b8af366d1ba276fa79dab662889e2d8f8",
        "e16132d5648cc79f086a254b933dd08b9ea6afc4dec7c370089cc389c88c7bc2",
        "9bd4498592a669f394173ca46f4a20a2bf79e3e5fea306d72ef208f038e37a25",
        "b2cf082d689ab93e38ed879f5268aff6f1cac84769044a894b6c4cd25c99397f",
        "a5e98a46b5aaa62af444368ff269855886f5f84dd8e82d33d1087067ed2237dd",
        "735aee58440fcf4f4ba50c3000c9d54378abce696cc464c6f9518be5d1ed7e38",
        "43451b12a772287c65536556f400e4bd0676505c169a8d5096761787a6ad3f38",
        "85e6e5952eb35fc37e741eed3d0b4169fe933954cd4eb7dc5531ef5013e35cdf");

    public static IReadOnlyDictionary<(HistoricalCandleInterval Interval, HistoricalCandleRange Range), string> VisibleRowFingerprints { get; } = QualificationMap(
        "36ac833f75709662a319833cc4b76eec2644912e9e81213145d52b3b923afbc6",
        "91aaf2d564ad150d321d8e94587661f1c1b9df5fd2d9cfc2e756d7188febe71f",
        "36b544790c2b90161cfd729a654c9314e2b477eade8cb0bb109162ce9bb934ac",
        "b71c04ace336fccd77f8a317372a0c13626b371aa32700cfe0ecf4918e75eb12",
        "c77eabc3699e29bb29a0581f8baa84f3c14761fb7173bcb67cd48b19283d21d1",
        "f59bff85e6c3fc0401138f901e2df85e05f7068b2e1a743b19ecdb745189cb03",
        "75d8030c3714d7e18fedfe9bb2a622321e82ab377ddc7bacab14e3f8a63b0814",
        "40795cf048985613d3379503114a29178b1b8ee49da5fd3dc47e5eee91367d38",
        "e32e09e717b1aff555e8cc620d1ff40cc64c232c866f1391b89bb5c0cc732873",
        "2339827d95c7469e890c842fcf58c5d862c8c66199df0770e294eeddff9d4373",
        "4d9e7cb57dc6834ad7ef35265191d7c465c1bfad51fafcaef2871ee831124d14",
        "27bbc5f787b71b064986a457da0ccae673cf1b41641f87df162b5bdbf2be61b8");

    public SyntheticHistoricalMarketDataProvider(TimeProvider? clock = null) => this.clock = clock ?? TimeProvider.System;

    public static SyntheticPublicationPolicy CreatePublicationPolicy() => new(DatasetFingerprints, WindowFingerprints);

    public Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken = default)
    {
        // This is bounded synchronous CPU work in the future isolated Worker, with no background task.
        if (request is null || request.Symbol != CanonicalMarketSymbol.BtcLikeSimulation)
            return Task.FromResult(HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.InvalidRequest));
        try
        {
            var budget = new GenerationBudget(clock, request.Deadline < MaximumExecution ? request.Deadline : MaximumExecution, cancellationToken);
            var dataset = GenerateCompleteDataset(budget);
            foreach (var interval in Enum.GetValues<HistoricalCandleInterval>())
            {
                var fingerprint = Hash(SerializeDataset(interval, dataset.For(interval), budget));
                if (fingerprint != DatasetFingerprints[interval]) throw new InvalidDataException("Synthetic dataset qualification failed.");
            }
            var calculation = SelectCalculationWindow(request, dataset.For(request.Interval));
            var windowFingerprint = Hash(SerializeWindow(request, calculation, budget));
            if (windowFingerprint != WindowFingerprints[(request.Interval, request.Range)] ||
                Hash(SerializeVisibleRows(request, calculation, budget)) != VisibleRowFingerprints[(request.Interval, request.Range)])
                throw new InvalidDataException("Synthetic window qualification failed.");

            budget.Check();
            var generatedAt = clock.GetUtcNow().ToUniversalTime();
            var identity = new SyntheticMarketDataIdentity(SyntheticPublicationPolicy.CanonicalVersion,
                SyntheticPublicationPolicy.Instrument, "simulation", SyntheticPublicationPolicy.Source, "simulated",
                null, null, null, null, "illustrative-price-unit", "illustrative-activity-unit", GeneratorVersion, Seed,
                SyntheticPublicationPolicy.CoverageStart, SyntheticPublicationPolicy.CoverageEnd,
                DatasetFingerprints[request.Interval], windowFingerprint);
            var provenance = new HistoricalMarketDataProvenance(SyntheticPublicationPolicy.Source, generatedAt, generatedAt, false, true)
            { SyntheticIdentity = identity };
            CreatePublicationPolicy().Validate(request, calculation, provenance);
            var response = HistoricalMarketDataResponse.Available(request, calculation, provenance);
            budget.Check();
            return Task.FromResult(response);
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        { return Task.FromResult(HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Cancelled)); }
        catch (TimeoutException) { return Task.FromResult(HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Timeout)); }
        catch (ArgumentException) { return Task.FromResult(HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.InvalidRequest)); }
        catch (InvalidDataException) { return Task.FromResult(HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse)); }
        catch (OverflowException) { return Task.FromResult(HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse)); }
    }

    internal sealed class CompleteDataset(ReadOnlyCollection<HistoricalCandle> hourly, ReadOnlyCollection<HistoricalCandle> fourHourly, ReadOnlyCollection<HistoricalCandle> daily)
    {
        internal IReadOnlyList<HistoricalCandle> For(HistoricalCandleInterval interval) => interval switch
        { HistoricalCandleInterval.OneHour => hourly, HistoricalCandleInterval.FourHours => fourHourly, HistoricalCandleInterval.OneDay => daily, _ => throw new ArgumentOutOfRangeException(nameof(interval)) };
    }

    internal sealed class GenerationBudget
    {
        private readonly TimeProvider clock;
        private readonly long started;
        private readonly TimeSpan deadline;
        private readonly CancellationToken cancellationToken;
        internal GenerationBudget(TimeProvider clock, TimeSpan deadline, CancellationToken cancellationToken)
        {
            this.clock = clock; this.deadline = deadline; this.cancellationToken = cancellationToken;
            cancellationToken.ThrowIfCancellationRequested(); started = clock.GetTimestamp();
        }
        internal void Check()
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (clock.GetElapsedTime(started, clock.GetTimestamp()) >= deadline) throw new TimeoutException("Synthetic operation deadline exceeded.");
            cancellationToken.ThrowIfCancellationRequested();
        }
    }

    internal static CompleteDataset GenerateCompleteDataset(GenerationBudget budget)
    {
        var boundaries = new long[HourlyCount + 1];
        for (var i = 0; i <= HourlyCount; i++)
        { if (i % 64 == 0) budget.Check(); boundaries[i] = PriceTicks(i); }
        var hourly = new HistoricalCandle[HourlyCount];
        for (var i = 0; i < hourly.Length; i++)
        {
            if (i % 64 == 0) budget.Check();
            var open = boundaries[i]; var close = boundaries[i + 1];
            var high = checked(Math.Max(open, close) + 1 + (long)Counter(i, 1, 50000));
            var low = checked(Math.Min(open, close) - 1 - (long)Counter(i, 2, 50000));
            var volume = checked(100000UL + Counter(i, 3, 1000000));
            hourly[i] = new HistoricalCandle(CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour,
                SyntheticPublicationPolicy.CoverageStart.AddHours(i), open / 10000m, high / 10000m, low / 10000m, close / 10000m, volume / 1000m);
        }
        var fourHourly = Aggregate(hourly, HistoricalCandleInterval.FourHours, budget);
        var daily = Aggregate(hourly, HistoricalCandleInterval.OneDay, budget);
        budget.Check();
        return new(Array.AsReadOnly(hourly), fourHourly, daily);
    }

    internal static long PriceTicks(int index)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index); ArgumentOutOfRangeException.ThrowIfGreaterThan(index, HourlyCount);
        var remainder = index % 480;
        var triangle = remainder <= 240 ? remainder - 120 : 360 - remainder;
        return checked(500000000L + 2500L * triangle + (long)Counter(index, 0, 200001) - 100000L);
    }

    private static ulong Counter(int index, byte lane, ulong modulus)
    {
        ReadOnlySpan<byte> version = "aiq-synthetic-ohlcv-v1"u8;
        Span<byte> input = stackalloc byte[version.Length + 18];
        version.CopyTo(input); input[version.Length] = 0;
        BinaryPrimitives.WriteUInt64BigEndian(input.Slice(version.Length + 1, 8), Seed);
        BinaryPrimitives.WriteUInt64BigEndian(input.Slice(version.Length + 9, 8), checked((ulong)index));
        input[^1] = lane;
        Span<byte> digest = stackalloc byte[32]; SHA256.HashData(input, digest);
        return checked((ulong)BinaryPrimitives.ReadUInt32BigEndian(digest) * modulus) / 4294967296UL;
    }

    internal static ReadOnlyCollection<HistoricalCandle> Aggregate(IReadOnlyList<HistoricalCandle> hourly, HistoricalCandleInterval interval, GenerationBudget budget)
    {
        if (interval is not (HistoricalCandleInterval.FourHours or HistoricalCandleInterval.OneDay)) throw new ArgumentException("Only complete 4h/daily aggregation is supported.");
        ValidateRows(hourly, HistoricalCandleInterval.OneHour, SyntheticPublicationPolicy.CoverageStart, HourlyCount, budget);
        var width = SyntheticPublicationPolicy.Hours(interval);
        var result = new HistoricalCandle[hourly.Count / width];
        for (var bucket = 0; bucket < result.Length; bucket++)
        {
            if (bucket % 64 == 0) budget.Check();
            var index = checked(bucket * width); var first = hourly[index];
            var high = first.High; var low = first.Low; var volume = 0m;
            for (var j = 0; j < width; j++)
            { var row = hourly[index + j]; high = Math.Max(high, row.High); low = Math.Min(low, row.Low); volume = checked(volume + row.Volume); }
            result[bucket] = new(first.Symbol, interval, first.OpenTimeUtc, first.Open, high, low, hourly[index + width - 1].Close, volume);
        }
        return Array.AsReadOnly(result);
    }

    internal static ReadOnlyCollection<HistoricalCandle> SelectCalculationWindow(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> dataset)
    {
        RequireSyntheticRequest(request);
        if (dataset.Count != HourlyCount / SyntheticPublicationPolicy.Hours(request.Interval)) throw new InvalidDataException("Incomplete interval dataset.");
        var count = checked(SyntheticPublicationPolicy.VisibleCount(request) + SyntheticPublicationPolicy.Warmup);
        if (count > 2410 || count > dataset.Count) throw new InvalidDataException("Calculation window exceeds coverage.");
        var selected = new HistoricalCandle[count];
        for (var i = 0; i < count; i++) selected[i] = dataset[dataset.Count - count + i];
        return Array.AsReadOnly(selected);
    }

    internal static byte[] SerializeDataset(HistoricalCandleInterval interval, IReadOnlyList<HistoricalCandle> rows, GenerationBudget budget)
    {
        ValidateRows(rows, interval, SyntheticPublicationPolicy.CoverageStart, HourlyCount / SyntheticPublicationPolicy.Hours(interval), budget);
        var text = Header(interval, false); text.Append("columns=openTimeUtc,open,high,low,close,volume\n");
        AppendRows(text, rows, 0, budget); return Encode(text, budget);
    }

    internal static byte[] SerializeWindow(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> calculation, GenerationBudget budget)
    {
        ValidateCalculation(request, calculation, budget);
        var text = Header(request.Interval, true);
        text.Append("range=").Append(SyntheticPublicationPolicy.RangeLiteral(request.Range)).Append('\n')
            .Append("visibleStart=").Append(SyntheticPublicationPolicy.Timestamp(SyntheticPublicationPolicy.CoverageEnd.AddDays(-SyntheticPublicationPolicy.Days(request.Range)))).Append('\n')
            .Append("calculationStart=").Append(SyntheticPublicationPolicy.Timestamp(SyntheticPublicationPolicy.CalculationStart(request))).Append("\nwarmup=250\n")
            .Append("columns=openTimeUtc,open,high,low,close,volume\n");
        AppendRows(text, calculation, 0, budget); return Encode(text, budget);
    }

    internal static byte[] SerializeVisibleRows(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> calculation, GenerationBudget budget)
    {
        ValidateCalculation(request, calculation, budget);
        var text = new StringBuilder(); AppendRows(text, calculation, SyntheticPublicationPolicy.Warmup, budget); return Encode(text, budget);
    }

    private static void RequireSyntheticRequest(HistoricalMarketDataRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (request.Symbol != CanonicalMarketSymbol.BtcLikeSimulation || !Enum.IsDefined(request.Interval) || !Enum.IsDefined(request.Range) ||
            request.Deadline < HistoricalMarketDataRequest.MinimumDeadline || request.Deadline > HistoricalMarketDataRequest.MaximumDeadline)
            throw new ArgumentException("Unsupported synthetic request.");
    }

    private static void ValidateCalculation(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> rows, GenerationBudget budget)
    {
        RequireSyntheticRequest(request);
        ValidateRows(rows, request.Interval, SyntheticPublicationPolicy.CalculationStart(request),
            checked(SyntheticPublicationPolicy.VisibleCount(request) + SyntheticPublicationPolicy.Warmup), budget);
    }

    private static void ValidateRows(IReadOnlyList<HistoricalCandle> rows, HistoricalCandleInterval interval, DateTimeOffset start, int count, GenerationBudget budget)
    {
        ArgumentNullException.ThrowIfNull(rows);
        if (rows.Count != count || count > HourlyCount) throw new InvalidDataException("Incomplete or oversized candle grid.");
        var hours = SyntheticPublicationPolicy.Hours(interval);
        for (var i = 0; i < count; i++)
        {
            if (i % 64 == 0) budget.Check();
            var row = rows[i];
            if (row is null || row.Symbol != CanonicalMarketSymbol.BtcLikeSimulation || row.Interval != interval || row.OpenTimeUtc.Offset != TimeSpan.Zero ||
                row.OpenTimeUtc != start.AddHours(checked(i * hours)) || row.Low <= 0m || row.High > 1000000000m ||
                row.Low > Math.Min(row.Open, row.Close) || row.High < Math.Max(row.Open, row.Close) || row.Volume < 0m ||
                decimal.Round(row.Open, 4) != row.Open || decimal.Round(row.High, 4) != row.High || decimal.Round(row.Low, 4) != row.Low ||
                decimal.Round(row.Close, 4) != row.Close || decimal.Round(row.Volume, 3) != row.Volume || (i > 0 && rows[i - 1].Close != row.Open))
                throw new InvalidDataException("Malformed synthetic candle grid, precision or continuity.");
        }
    }

    private static StringBuilder Header(HistoricalCandleInterval interval, bool window) => new StringBuilder()
        .Append(window ? "aiq-synthetic-window-v1\n" : "aiq-synthetic-dataset-v1\n")
        .Append("canonical=aiq-candles-v2\nsource=independent-synthetic\nstatus=simulated\ninstrument=btc-like-simulation\nmarketType=simulation\nvenue=none\nbase=none\nquote=none\nsettlement=none\npriceUnit=illustrative-price-unit\nvolumeUnit=illustrative-activity-unit\ngenerator=aiq-synthetic-ohlcv-v1\nseed=114\nstart=2025-10-26T00:00:00Z\nendExclusive=2026-10-01T00:00:00Z\ninterval=")
        .Append(SyntheticPublicationPolicy.IntervalLiteral(interval)).Append("\npriceScale=4\nvolumeScale=3\n");

    private static void AppendRows(StringBuilder text, IReadOnlyList<HistoricalCandle> rows, int skip, GenerationBudget budget)
    {
        for (var i = skip; i < rows.Count; i++)
        {
            if (i % 64 == 0) budget.Check();
            var row = rows[i]; text.Append(SyntheticPublicationPolicy.Timestamp(row.OpenTimeUtc)).Append(',')
                .Append(row.Open.ToString("F4", CultureInfo.InvariantCulture)).Append(',')
                .Append(row.High.ToString("F4", CultureInfo.InvariantCulture)).Append(',')
                .Append(row.Low.ToString("F4", CultureInfo.InvariantCulture)).Append(',')
                .Append(row.Close.ToString("F4", CultureInfo.InvariantCulture)).Append(',')
                .Append(row.Volume.ToString("F3", CultureInfo.InvariantCulture)).Append('\n');
            if (text.Length > MaximumSerializationBytes) throw new InvalidDataException("Synthetic serialization budget exceeded.");
        }
    }

    private static byte[] Encode(StringBuilder text, GenerationBudget budget)
    {
        budget.Check(); if (CanonicalEncoding.GetByteCount(text.ToString()) > MaximumSerializationBytes) throw new InvalidDataException("Synthetic byte budget exceeded.");
        return CanonicalEncoding.GetBytes(text.ToString());
    }
    private static string Hash(byte[] bytes) => Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();

    private static ReadOnlyDictionary<(HistoricalCandleInterval, HistoricalCandleRange), string> QualificationMap(params string[] values)
    {
        if (values.Length != 12 || values.Any(value => !SyntheticPublicationPolicy.IsFingerprint(value))) throw new InvalidDataException("Invalid version qualification constants.");
        var map = new Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string>(); var index = 0;
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>()) foreach (var range in Enum.GetValues<HistoricalCandleRange>()) map.Add((interval, range), values[index++]);
        return new(map);
    }
}
