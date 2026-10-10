using System.Collections.ObjectModel;

namespace AIQuantTradingResearch.Application.MarketData;

public enum CanonicalMarketSymbol
{
    BtcUsd,
    EthUsd,
    BtcLikeSimulation,
}

public enum HistoricalCandleInterval
{
    OneHour,
    FourHours,
    OneDay,
}

public enum HistoricalCandleRange
{
    OneDay,
    SevenDays,
    ThirtyDays,
    NinetyDays,
}

public enum HistoricalMarketDataFailure
{
    InvalidRequest,
    Unavailable,
    RateLimited,
    AuthenticationOrConfiguration,
    MalformedResponse,
    Timeout,
    Cancelled,
}

public sealed record HistoricalMarketDataRequest
{
    public static readonly TimeSpan MinimumDeadline = TimeSpan.FromSeconds(1);
    public static readonly TimeSpan MaximumDeadline = TimeSpan.FromSeconds(30);

    public HistoricalMarketDataRequest(
        CanonicalMarketSymbol symbol,
        HistoricalCandleInterval interval,
        HistoricalCandleRange range,
        TimeSpan deadline)
    {
        if (!Enum.IsDefined(symbol))
        {
            throw new ArgumentOutOfRangeException(nameof(symbol), symbol, "Unsupported canonical market symbol.");
        }

        if (!Enum.IsDefined(interval))
        {
            throw new ArgumentOutOfRangeException(nameof(interval), interval, "Unsupported historical candle interval.");
        }

        if (!Enum.IsDefined(range))
        {
            throw new ArgumentOutOfRangeException(nameof(range), range, "Unsupported historical candle range.");
        }

        if (deadline < MinimumDeadline || deadline > MaximumDeadline)
        {
            throw new ArgumentOutOfRangeException(
                nameof(deadline),
                deadline,
                "Historical market-data deadline must be between one and thirty seconds.");
        }

        Symbol = symbol;
        Interval = interval;
        Range = range;
        Deadline = deadline;
    }

    public CanonicalMarketSymbol Symbol { get; }

    public HistoricalCandleInterval Interval { get; }

    public HistoricalCandleRange Range { get; }

    public TimeSpan Deadline { get; }
}

public sealed record HistoricalCandle
{
    public HistoricalCandle(
        CanonicalMarketSymbol symbol,
        HistoricalCandleInterval interval,
        DateTimeOffset openTimeUtc,
        decimal open,
        decimal high,
        decimal low,
        decimal close,
        decimal volume)
    {
        if (!Enum.IsDefined(symbol) || !Enum.IsDefined(interval))
        {
            throw new ArgumentOutOfRangeException(nameof(symbol), symbol, "Candle symbol and interval must be canonical values.");
        }

        if (openTimeUtc.Offset != TimeSpan.Zero)
        {
            throw new ArgumentException("Candle open time must be expressed in UTC.", nameof(openTimeUtc));
        }

        if (open <= 0m || high <= 0m || low <= 0m || close <= 0m || volume < 0m ||
            low > Math.Min(open, close) || high < Math.Max(open, close) || high < low)
        {
            throw new ArgumentException("Candle OHLCV values are incomplete or internally inconsistent.");
        }

        Symbol = symbol;
        Interval = interval;
        OpenTimeUtc = openTimeUtc;
        Open = open;
        High = high;
        Low = low;
        Close = close;
        Volume = volume;
    }

    public CanonicalMarketSymbol Symbol { get; }

    public HistoricalCandleInterval Interval { get; }

    public DateTimeOffset OpenTimeUtc { get; }

    public decimal Open { get; }

    public decimal High { get; }

    public decimal Low { get; }

    public decimal Close { get; }

    public decimal Volume { get; }
}

public sealed record HistoricalMarketDataProvenance
{
    public HistoricalMarketDataProvenance(
        string provider,
        DateTimeOffset acquiredAtUtc,
        DateTimeOffset lastValidatedAtUtc,
        bool isStale,
        bool containsOnlyClosedCandles)
    {
        if (string.IsNullOrWhiteSpace(provider))
        {
            throw new ArgumentException("Provider identity is required.", nameof(provider));
        }

        if (acquiredAtUtc.Offset != TimeSpan.Zero || lastValidatedAtUtc.Offset != TimeSpan.Zero)
        {
            throw new ArgumentException("Provenance timestamps must be expressed in UTC.");
        }

        if (lastValidatedAtUtc < acquiredAtUtc)
        {
            throw new ArgumentException("Last validation cannot precede acquisition.", nameof(lastValidatedAtUtc));
        }

        Provider = provider;
        AcquiredAtUtc = acquiredAtUtc;
        LastValidatedAtUtc = lastValidatedAtUtc;
        IsStale = isStale;
        ContainsOnlyClosedCandles = containsOnlyClosedCandles;
    }

    public string Provider { get; }

    public DateTimeOffset AcquiredAtUtc { get; }

    public DateTimeOffset LastValidatedAtUtc { get; }

    public bool IsStale { get; }

    public bool ContainsOnlyClosedCandles { get; }

    public SyntheticMarketDataIdentity? SyntheticIdentity { get; init; }
}

public sealed record HistoricalMarketDataResponse
{
    private HistoricalMarketDataResponse(
        ReadOnlyCollection<HistoricalCandle>? candles,
        HistoricalMarketDataProvenance? provenance,
        HistoricalMarketDataFailure? failure)
    {
        Candles = candles;
        Provenance = provenance;
        Failure = failure;
    }

    public bool IsSuccess => Candles is not null;

    public IReadOnlyList<HistoricalCandle>? Candles { get; }

    public HistoricalMarketDataProvenance? Provenance { get; }

    public HistoricalMarketDataFailure? Failure { get; }

    public static HistoricalMarketDataResponse Available(
        HistoricalMarketDataRequest request,
        IEnumerable<HistoricalCandle> candles,
        HistoricalMarketDataProvenance provenance)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(candles);
        ArgumentNullException.ThrowIfNull(provenance);

        var snapshot = candles.Take(request.Symbol == CanonicalMarketSymbol.BtcLikeSimulation ? 2411 : int.MaxValue).ToArray();
        if (snapshot.Any(static candle => candle is null) ||
            snapshot.Any(candle => candle.Symbol != request.Symbol || candle.Interval != request.Interval) ||
            snapshot.Zip(snapshot.Skip(1), static (left, right) => left.OpenTimeUtc >= right.OpenTimeUtc).Any(static duplicateOrOutOfOrder => duplicateOrOutOfOrder))
        {
            throw new ArgumentException(
                "Successful candles must be complete, request-aligned, unique, and strictly ascending.",
                nameof(candles));
        }

        if (request.Symbol == CanonicalMarketSymbol.BtcLikeSimulation)
            SyntheticPublicationPolicy.ValidateWindow(request, snapshot, provenance);

        return new HistoricalMarketDataResponse(Array.AsReadOnly(snapshot), provenance, null);
    }

    public static HistoricalMarketDataResponse Failed(HistoricalMarketDataFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown historical market-data failure.");
        }

        return new HistoricalMarketDataResponse(null, null, failure);
    }
}

/// <summary>Canonical simulation metadata. Never inferred from a provider display name.</summary>
public sealed record SyntheticMarketDataIdentity(
    string CanonicalVersion, string Instrument, string MarketType, string Source, string Status,
    string? Venue, string? Base, string? Quote, string? Settlement, string PriceUnit, string VolumeUnit,
    string GeneratorVersion, ulong Seed, DateTimeOffset CoverageStartUtc, DateTimeOffset CoverageEndExclusiveUtc,
    string DatasetFingerprint, string WindowFingerprint, string? CalculationVersion = null);

/// <summary>Server-owned publication policy; no caller may supply or change this policy.</summary>
public sealed class SyntheticPublicationPolicy
{
    public const string CanonicalVersion = "aiq-candles-v2";
    public const string QueryVersion = "aiq-historical-query-v2";
    public const string CacheVersion = "aiq-historical-market-data-cache-v2";
    public const string Instrument = "btc-like-simulation";
    public const string Source = "independent-synthetic";
    public const string GeneratorVersion = "aiq-synthetic-ohlcv-v1";
    public const int Warmup = 250;
    public static readonly DateTimeOffset CoverageStart = new(2025, 10, 26, 0, 0, 0, TimeSpan.Zero);
    public static readonly DateTimeOffset CoverageEnd = new(2026, 10, 1, 0, 0, 0, TimeSpan.Zero);
    private readonly ReadOnlyDictionary<HistoricalCandleInterval, string> fingerprints;
    private readonly ReadOnlyDictionary<(HistoricalCandleInterval, HistoricalCandleRange), string> windowFingerprints;

    public SyntheticPublicationPolicy(IReadOnlyDictionary<HistoricalCandleInterval, string> datasetFingerprints,
        IReadOnlyDictionary<(HistoricalCandleInterval, HistoricalCandleRange), string> qualifiedWindowFingerprints)
    {
        ArgumentNullException.ThrowIfNull(datasetFingerprints);
        if (datasetFingerprints.Count != 3 || Enum.GetValues<HistoricalCandleInterval>().Any(i =>
            !datasetFingerprints.TryGetValue(i, out var fingerprint) || !IsFingerprint(fingerprint)))
            throw new ArgumentException("A server-qualified dataset fingerprint is required for every interval.");
        fingerprints = new ReadOnlyDictionary<HistoricalCandleInterval, string>(new Dictionary<HistoricalCandleInterval, string>(datasetFingerprints));
        ArgumentNullException.ThrowIfNull(qualifiedWindowFingerprints);
        if (qualifiedWindowFingerprints.Count != 12 || Enum.GetValues<HistoricalCandleInterval>().Any(i => Enum.GetValues<HistoricalCandleRange>().Any(r =>
            !qualifiedWindowFingerprints.TryGetValue((i, r), out var fingerprint) || !IsFingerprint(fingerprint))))
            throw new ArgumentException("A server-qualified fingerprint is required for every calculation window.");
        windowFingerprints = new(new Dictionary<(HistoricalCandleInterval, HistoricalCandleRange), string>(qualifiedWindowFingerprints));
    }

    public string DatasetFingerprint(HistoricalCandleInterval interval) => fingerprints[interval];
    public string QualifiedWindowFingerprint(HistoricalMarketDataRequest request) => windowFingerprints[(request.Interval, request.Range)];
    public static bool IsFingerprint(string? value) => value is { Length: 64 } && value.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'f');
    public static int Hours(HistoricalCandleInterval interval) => interval switch
    { HistoricalCandleInterval.OneHour => 1, HistoricalCandleInterval.FourHours => 4, HistoricalCandleInterval.OneDay => 24, _ => throw new ArgumentOutOfRangeException(nameof(interval)) };
    public static int Days(HistoricalCandleRange range) => range switch
    { HistoricalCandleRange.OneDay => 1, HistoricalCandleRange.SevenDays => 7, HistoricalCandleRange.ThirtyDays => 30, HistoricalCandleRange.NinetyDays => 90, _ => throw new ArgumentOutOfRangeException(nameof(range)) };
    public static int VisibleCount(HistoricalMarketDataRequest request) => 24 * Days(request.Range) / Hours(request.Interval);
    public static DateTimeOffset CalculationStart(HistoricalMarketDataRequest request) => CoverageEnd.AddHours(-(VisibleCount(request) + Warmup) * Hours(request.Interval));
    public static string IntervalLiteral(HistoricalCandleInterval interval) => interval switch
    { HistoricalCandleInterval.OneHour => "1h", HistoricalCandleInterval.FourHours => "4h", HistoricalCandleInterval.OneDay => "1d", _ => throw new ArgumentOutOfRangeException(nameof(interval)) };
    public static string RangeLiteral(HistoricalCandleRange range) => Days(range) + "D";
    public static string Timestamp(DateTimeOffset time) => time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", System.Globalization.CultureInfo.InvariantCulture);

    public void Validate(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> candles, HistoricalMarketDataProvenance provenance)
    {
        ValidateWindow(request, candles, provenance);
        if (provenance.SyntheticIdentity!.DatasetFingerprint != DatasetFingerprint(request.Interval) ||
            provenance.SyntheticIdentity.WindowFingerprint != QualifiedWindowFingerprint(request))
            throw new ArgumentException("Unqualified dataset fingerprint.");
    }

    public static void ValidateWindow(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> candles, HistoricalMarketDataProvenance provenance)
    {
        var identity = provenance.SyntheticIdentity;
        if (request.Symbol != CanonicalMarketSymbol.BtcLikeSimulation || identity is null ||
            provenance.Provider != Source || provenance.IsStale || !provenance.ContainsOnlyClosedCandles ||
            identity.CanonicalVersion != CanonicalVersion || identity.Instrument != Instrument ||
            identity.MarketType != "simulation" || identity.Source != Source || identity.Status != "simulated" ||
            identity.Venue is not null || identity.Base is not null || identity.Quote is not null || identity.Settlement is not null ||
            identity.PriceUnit != "illustrative-price-unit" || identity.VolumeUnit != "illustrative-activity-unit" ||
            identity.GeneratorVersion != GeneratorVersion || identity.Seed != 114 || identity.CalculationVersion is not null ||
            identity.CoverageStartUtc != CoverageStart || identity.CoverageStartUtc.Offset != TimeSpan.Zero ||
            identity.CoverageEndExclusiveUtc != CoverageEnd || identity.CoverageEndExclusiveUtc.Offset != TimeSpan.Zero ||
            !IsFingerprint(identity.DatasetFingerprint) || !IsFingerprint(identity.WindowFingerprint) ||
            candles.Count != VisibleCount(request) + Warmup)
            throw new ArgumentException("Data is not approved for synthetic publication.");
        var start = CalculationStart(request);
        for (var i = 0; i < candles.Count; i++)
        {
            var candle = candles[i];
            if (candle is null || candle.Symbol != request.Symbol || candle.Interval != request.Interval ||
                candle.OpenTimeUtc.Offset != TimeSpan.Zero || candle.OpenTimeUtc != start.AddHours(i * Hours(request.Interval)) ||
                candle.Open > 1_000_000_000m || candle.High > 1_000_000_000m || candle.Close > 1_000_000_000m ||
                decimal.Round(candle.Open, 4) != candle.Open || decimal.Round(candle.High, 4) != candle.High ||
                decimal.Round(candle.Low, 4) != candle.Low || decimal.Round(candle.Close, 4) != candle.Close ||
                decimal.Round(candle.Volume, 3) != candle.Volume)
                throw new ArgumentException("Invalid synthetic candle grid or precision.");
        }
        if (WindowFingerprint(request, candles) != identity.WindowFingerprint)
            throw new ArgumentException("Synthetic window fingerprint mismatch.");
    }

    // Serialization only: no synthetic generation or aggregation occurs here.
    public static string WindowFingerprint(HistoricalMarketDataRequest request, IReadOnlyList<HistoricalCandle> candles)
    {
        var text = Header(request.Interval, true);
        text.Append("range=").Append(RangeLiteral(request.Range)).Append('\n')
            .Append("visibleStart=").Append(Timestamp(CoverageEnd.AddDays(-Days(request.Range)))).Append('\n')
            .Append("calculationStart=").Append(Timestamp(CalculationStart(request))).Append("\nwarmup=250\n");
        AppendRows(text, candles);
        return Hash(text);
    }

    public static string DatasetFingerprint(HistoricalCandleInterval interval, IReadOnlyList<HistoricalCandle> candles)
    {
        if (candles.Count != 8160 / Hours(interval)) throw new ArgumentException("Incomplete dataset.");
        for (var i = 0; i < candles.Count; i++)
            if (candles[i].Symbol != CanonicalMarketSymbol.BtcLikeSimulation || candles[i].Interval != interval ||
                candles[i].OpenTimeUtc != CoverageStart.AddHours(i * Hours(interval)) || candles[i].OpenTimeUtc.Offset != TimeSpan.Zero ||
                new[] { candles[i].Open, candles[i].High, candles[i].Low, candles[i].Close }.Any(p => p > 1_000_000_000m || decimal.Round(p, 4) != p) ||
                decimal.Round(candles[i].Volume, 3) != candles[i].Volume)
                throw new ArgumentException("Invalid dataset grid or precision.");
        var text = Header(interval, false); AppendRows(text, candles); return Hash(text);
    }

    private static System.Text.StringBuilder Header(HistoricalCandleInterval interval, bool window) => new System.Text.StringBuilder()
        .Append(window ? "aiq-synthetic-window-v1\n" : "aiq-synthetic-dataset-v1\n")
        .Append("canonical=aiq-candles-v2\nsource=independent-synthetic\nstatus=simulated\ninstrument=btc-like-simulation\nmarketType=simulation\nvenue=none\nbase=none\nquote=none\nsettlement=none\npriceUnit=illustrative-price-unit\nvolumeUnit=illustrative-activity-unit\ngenerator=aiq-synthetic-ohlcv-v1\nseed=114\nstart=2025-10-26T00:00:00Z\nendExclusive=2026-10-01T00:00:00Z\ninterval=")
        .Append(IntervalLiteral(interval)).Append("\npriceScale=4\nvolumeScale=3\n");
    private static void AppendRows(System.Text.StringBuilder text, IReadOnlyList<HistoricalCandle> candles)
    {
        text.Append("columns=openTimeUtc,open,high,low,close,volume\n");
        foreach (var candle in candles)
            text.Append(Timestamp(candle.OpenTimeUtc)).Append(',')
                .Append(candle.Open.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)).Append(',')
                .Append(candle.High.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)).Append(',')
                .Append(candle.Low.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)).Append(',')
                .Append(candle.Close.ToString("F4", System.Globalization.CultureInfo.InvariantCulture)).Append(',')
                .Append(candle.Volume.ToString("F3", System.Globalization.CultureInfo.InvariantCulture)).Append('\n');
    }
    private static string Hash(System.Text.StringBuilder text) => Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(text.ToString()))).ToLowerInvariant();
}

public interface IHistoricalMarketDataProvider
{
    Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(
        HistoricalMarketDataRequest request,
        CancellationToken cancellationToken = default);
}

/// <summary>Explicit independent-simulation capability for public composition. Observed adapters do not implement it.</summary>
public interface IIndependentSyntheticHistoricalMarketDataProvider : IHistoricalMarketDataProvider
{
}
