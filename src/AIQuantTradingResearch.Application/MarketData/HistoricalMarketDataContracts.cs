using System.Collections.ObjectModel;

namespace AIQuantTradingResearch.Application.MarketData;

public enum CanonicalMarketSymbol
{
    BtcUsd,
    EthUsd,
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

        var snapshot = candles.ToArray();
        if (snapshot.Any(static candle => candle is null) ||
            snapshot.Any(candle => candle.Symbol != request.Symbol || candle.Interval != request.Interval) ||
            snapshot.Zip(snapshot.Skip(1), static (left, right) => left.OpenTimeUtc >= right.OpenTimeUtc).Any(static duplicateOrOutOfOrder => duplicateOrOutOfOrder))
        {
            throw new ArgumentException(
                "Successful candles must be complete, request-aligned, unique, and strictly ascending.",
                nameof(candles));
        }

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

public interface IHistoricalMarketDataProvider
{
    Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(
        HistoricalMarketDataRequest request,
        CancellationToken cancellationToken = default);
}
