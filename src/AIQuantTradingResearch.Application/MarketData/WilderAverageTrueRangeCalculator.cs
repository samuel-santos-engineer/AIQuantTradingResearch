using System.Numerics;

namespace AIQuantTradingResearch.Application.MarketData;

/// <summary>Bounded ATR14, fixed 1e-12 ties-to-even. No provider, cache or runtime work.</summary>
public static class WilderAverageTrueRangeCalculator
{
    public const string Version = "aiq-atr-wilder-v1";
    public static IReadOnlyList<decimal?> CalculatePublic(HistoricalMarketDataRequest request,
        IReadOnlyList<HistoricalCandle> candles, HistoricalMarketDataProvenance provenance,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(candles);
        ArgumentNullException.ThrowIfNull(provenance);
        cancellationToken.ThrowIfCancellationRequested();
        SyntheticPublicationPolicy.ValidateWindow(request, candles, provenance);
        return Calculate(candles, cancellationToken);
    }

    internal static IReadOnlyList<decimal?> Calculate(IReadOnlyList<HistoricalCandle> candles,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(candles);
        cancellationToken.ThrowIfCancellationRequested();
        if (candles.Count is < 1 or > TechnicalIndicatorCalculator.MaximumInputs)
            throw new ArgumentException("Invalid ATR input bounds.");
        var input = candles.ToArray();
        var first = input[0] ?? throw new ArgumentException("Missing candle.");
        var hours = SyntheticPublicationPolicy.Hours(first.Interval);
        if (!Enum.IsDefined(first.Symbol)) throw new ArgumentException("Invalid instrument.");
        var result = new decimal?[input.Length];
        BigInteger sum = 0, previousClose = 0, atr = 0;
        for (var i = 0; i < input.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var c = input[i];
            if (c is null || c.Symbol != first.Symbol || c.Interval != first.Interval ||
                c.OpenTimeUtc.Offset != TimeSpan.Zero || c.OpenTimeUtc != first.OpenTimeUtc.AddHours(i * hours) ||
                c.OpenTimeUtc.Ticks % TimeSpan.FromHours(hours).Ticks != 0 || c.Low <= 0 || c.High > 1_000_000_000m ||
                c.Low > Math.Min(c.Open, c.Close) || c.High < Math.Max(c.Open, c.Close) || c.Volume < 0)
                throw new ArgumentException("Malformed ATR OHLCV or grid.");
            static BigInteger Price(decimal x)
            {
                var scaled = x * 1_000_000_000_000m;
                if (decimal.Truncate(scaled) != scaled) throw new ArgumentException("Excess price precision.");
                return new BigInteger(scaled);
            }
            _ = Price(c.Open);
            var high = Price(c.High); var low = Price(c.Low); var close = Price(c.Close);
            // TR0 has no previous close. Include high-low in the 14-sample seed.
            var tr = high - low;
            if (i > 0) tr = BigInteger.Max(tr, BigInteger.Max(BigInteger.Abs(high - previousClose), BigInteger.Abs(low - previousClose)));
            previousClose = close;
            if (i < 14) sum += tr;
            if (i == 13) atr = TechnicalIndicatorCalculator.Round(sum, 14);
            else if (i >= 14) atr = TechnicalIndicatorCalculator.Round(13 * atr + tr, 14);
            if (i >= 13) result[i] = (decimal)atr / 1_000_000_000_000m;
        }
        return Array.AsReadOnly(result);
    }
}
