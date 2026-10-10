using System.Collections.ObjectModel;

namespace AIQuantTradingResearch.Application.MarketData;

/// <summary>Fixed semantics; changing any parameter requires a new calculation version.</summary>
public sealed record TechnicalIndicatorParameters
{
    public string Version { get; } = "aiq-ta-v1";
    public int Scale { get; } = 12;
    public int SmaPeriod { get; } = 20;
    public int EmaPeriod { get; } = 20;
    public int RsiPeriod { get; } = 14;
    public int MacdFastPeriod { get; } = 12;
    public int MacdSlowPeriod { get; } = 26;
    public int MacdSignalPeriod { get; } = 9;
    public int BollingerPeriod { get; } = 20;
    public int BollingerMultiplier { get; } = 2;
    public static TechnicalIndicatorParameters Fixed { get; } = new();
}

/// <summary>Server-owned input identity. Public qualification remains a separate publication gate.</summary>
public sealed record TechnicalIndicatorInputIdentity(
    string CanonicalVersion, string Context, string Source, CanonicalMarketSymbol Symbol,
    HistoricalCandleInterval Interval, DateTimeOffset CalculationStartUtc,
    string DatasetFingerprint, string WindowFingerprint);

public sealed record TechnicalIndicatorRow(DateTimeOffset OpenTimeUtc,
    decimal? Sma20, decimal? Ema20, decimal? Rsi14, decimal? Macd,
    decimal? MacdSignal, decimal? MacdHistogram,
    decimal? BollingerLower, decimal? BollingerMiddle, decimal? BollingerUpper);

public sealed class TechnicalIndicatorResult
{
    internal TechnicalIndicatorResult(TechnicalIndicatorInputIdentity identity, IEnumerable<TechnicalIndicatorRow> rows)
    {
        Identity = identity;
        Rows = new ReadOnlyCollection<TechnicalIndicatorRow>(rows.ToArray());
    }
    public TechnicalIndicatorInputIdentity Identity { get; }
    public TechnicalIndicatorParameters Parameters { get; } = TechnicalIndicatorParameters.Fixed;
    public IReadOnlyList<TechnicalIndicatorRow> Rows { get; }
}
