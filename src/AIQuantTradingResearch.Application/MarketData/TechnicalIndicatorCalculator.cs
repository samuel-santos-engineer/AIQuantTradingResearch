using System.Numerics;

namespace AIQuantTradingResearch.Application.MarketData;

/// <summary>Source-independent exact arithmetic. No provider access, caching or background work.</summary>
public static class TechnicalIndicatorCalculator
{
    internal static readonly BigInteger Scale = BigInteger.Pow(10, 12);
    public const int MaximumInputs = 2410;

    public static TechnicalIndicatorResult CalculatePublic(HistoricalMarketDataRequest request,
        IReadOnlyList<HistoricalCandle> candles, HistoricalMarketDataProvenance provenance,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(candles);
        ArgumentNullException.ThrowIfNull(provenance);
        cancellationToken.ThrowIfCancellationRequested();
        SyntheticPublicationPolicy.ValidateWindow(request, candles, provenance);
        var source = provenance.SyntheticIdentity!;
        return Calculate(candles, new(source.CanonicalVersion, "public-synthetic", source.Source,
            request.Symbol, request.Interval, SyntheticPublicationPolicy.CalculationStart(request),
            source.DatasetFingerprint, source.WindowFingerprint), provenance, cancellationToken);
    }

    public static TechnicalIndicatorResult Calculate(IReadOnlyList<HistoricalCandle> candles,
        TechnicalIndicatorInputIdentity identity, HistoricalMarketDataProvenance provenance,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(candles);
        ArgumentNullException.ThrowIfNull(identity);
        ArgumentNullException.ThrowIfNull(provenance);
        cancellationToken.ThrowIfCancellationRequested();
        if (candles.Count is < 1 or > MaximumInputs || identity.CanonicalVersion != "aiq-candles-v2" ||
            identity.Context is not ("public-synthetic" or "private-research") ||
            string.IsNullOrWhiteSpace(identity.Source) || identity.Source != provenance.Provider ||
            identity.CalculationStartUtc.Offset != TimeSpan.Zero || provenance.IsStale || !provenance.ContainsOnlyClosedCandles ||
            !SyntheticPublicationPolicy.IsFingerprint(identity.DatasetFingerprint) || !SyntheticPublicationPolicy.IsFingerprint(identity.WindowFingerprint))
            throw new ArgumentException("Invalid calculation identity or input bounds.");
        // Keep a bounded snapshot so caller-owned lists cannot change between validation and calculation.
        candles = Array.AsReadOnly(candles.ToArray());
        var hours = SyntheticPublicationPolicy.Hours(identity.Interval);
        if (!Enum.IsDefined(identity.Symbol)) throw new ArgumentException("Invalid instrument.");
        if (identity.Context == "public-synthetic")
        {
            var source = provenance.SyntheticIdentity;
            if (source is null || source.CanonicalVersion != identity.CanonicalVersion || source.Source != identity.Source ||
                source.DatasetFingerprint != identity.DatasetFingerprint || source.WindowFingerprint != identity.WindowFingerprint ||
                identity.Symbol != CanonicalMarketSymbol.BtcLikeSimulation || source.CalculationVersion is not null)
                throw new ArgumentException("Calculation provenance mismatch.");
            // Require an exact approved range including all 250 warmup rows, even at this shared entry point.
            var range = Enum.GetValues<HistoricalCandleRange>().SingleOrDefault(r =>
                24 * SyntheticPublicationPolicy.Days(r) / hours + 250 == candles.Count);
            var request = new HistoricalMarketDataRequest(identity.Symbol, identity.Interval, range, TimeSpan.FromSeconds(10));
            if (identity.CalculationStartUtc != SyntheticPublicationPolicy.CalculationStart(request))
                throw new ArgumentException("Calculation bounds mismatch.");
            SyntheticPublicationPolicy.ValidateWindow(request, candles, provenance);
        }
        else if (identity.Symbol == CanonicalMarketSymbol.BtcLikeSimulation || provenance.SyntheticIdentity is not null)
            throw new ArgumentException("Private and synthetic identities cannot be mixed.");

        var values = new BigInteger[candles.Count];
        for (var i = 0; i < candles.Count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var candle = candles[i];
            if (candle is null || candle.Symbol != identity.Symbol || candle.Interval != identity.Interval ||
                candle.OpenTimeUtc.Offset != TimeSpan.Zero || candle.OpenTimeUtc != identity.CalculationStartUtc.AddHours(i * hours) ||
                candle.OpenTimeUtc.Ticks % TimeSpan.FromHours(hours).Ticks != 0 || candle.Close is <= 0m or > 1_000_000_000m)
                throw new ArgumentException("Malformed canonical calculation grid.");
            var scaled = candle.Close * 1_000_000_000_000m;
            if (decimal.Truncate(scaled) != scaled) throw new ArgumentException("Excess close precision.");
            values[i] = new BigInteger(scaled);
        }
        var ema12 = Ema(values, 12, cancellationToken);
        var ema26 = Ema(values, 26, cancellationToken);
        var ema20 = Ema(values, 20, cancellationToken);
        var macd = values.Select((_, i) => ema26[i] is { } slow ? ema12[i]!.Value - slow : (BigInteger?)null).ToArray();
        var signalValues = Ema(macd.Skip(25).Select(x => x!.Value).ToArray(), 9, cancellationToken);
        var rows = new TechnicalIndicatorRow[values.Length];
        BigInteger gain = 0, loss = 0;
        for (var i = 0; i < values.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            BigInteger? sma = null, sigma = null, rsi = null;
            if (i >= 19)
            {
                BigInteger sum = 0, squares = 0;
                for (var j = i - 19; j <= i; j++) { sum += values[j]; squares += values[j] * values[j]; }
                sma = Round(sum, 20);
                sigma = PopulationSigma(20 * squares - sum * sum);
            }
            if (i is >= 1 and <= 14)
            {
                var delta = values[i] - values[i - 1];
                gain += BigInteger.Max(delta, 0); loss += BigInteger.Max(-delta, 0);
                if (i == 14) { gain = Round(gain, 14); loss = Round(loss, 14); }
            }
            else if (i > 14)
            {
                var delta = values[i] - values[i - 1];
                gain = Round(13 * gain + BigInteger.Max(delta, 0), 14);
                loss = Round(13 * loss + BigInteger.Max(-delta, 0), 14);
            }
            if (i >= 14) rsi = gain + loss == 0 ? 50 * Scale : Round(100 * Scale * gain, gain + loss);
            BigInteger? signal = i >= 25 ? signalValues[i - 25] : null;
            rows[i] = new(candles[i].OpenTimeUtc, Decimal(sma), Decimal(ema20[i]), Decimal(rsi), Decimal(macd[i]),
                Decimal(signal), Decimal(signal is null ? null : macd[i] - signal),
                Decimal(sma - 2 * sigma), Decimal(sma), Decimal(sma + 2 * sigma));
        }
        return new(identity, rows);
    }

    private static BigInteger?[] Ema(BigInteger[] values, int period, CancellationToken cancellationToken)
    {
        var result = new BigInteger?[values.Length];
        BigInteger sum = 0;
        for (var i = 0; i < values.Length; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (i < period) sum += values[i];
            if (i == period - 1) result[i] = Round(sum, period);
            else if (i >= period) result[i] = Round(2 * values[i] + (period - 1) * result[i - 1]!.Value, period + 1);
        }
        return result;
    }

    internal static BigInteger Round(BigInteger numerator, BigInteger denominator)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(denominator);
        var quotient = BigInteger.DivRem(BigInteger.Abs(numerator), denominator, out var remainder);
        if (2 * remainder > denominator || (2 * remainder == denominator && !quotient.IsEven)) quotient++;
        return numerator.Sign < 0 ? -quotient : quotient;
    }

    internal static BigInteger FloorSquareRoot(BigInteger value)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(value);
        BigInteger low = 0, high = value + 1;
        while (high - low > 1)
        {
            var middle = (high + low) / 2;
            if (middle * middle <= value) low = middle; else high = middle;
        }
        return low;
    }

    internal static BigInteger PopulationSigma(BigInteger varianceNumerator)
    {
        var k = FloorSquareRoot(varianceNumerator) / 20;
        var midpoint = 400 * (2 * k + 1) * (2 * k + 1);
        var comparison = (4 * varianceNumerator).CompareTo(midpoint);
        return comparison > 0 || (comparison == 0 && !k.IsEven) ? k + 1 : k;
    }

    private static decimal? Decimal(BigInteger? scaled) => scaled is null ? null :
        checked((decimal)(scaled.Value / Scale) + (decimal)(scaled.Value % Scale) / 1_000_000_000_000m);
}
