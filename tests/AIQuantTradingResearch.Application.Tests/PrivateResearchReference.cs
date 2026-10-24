using System.Numerics;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Application.Tests;

// Independently authored batch oracle: full series arrays, not production's streaming/checkpoint engine.
internal static class PrivateResearchReference
{
    private static readonly BigInteger Unit = BigInteger.Pow(10, 12);
    internal sealed record Row(string?[] Values, int? Score, string? Trend, string? Regime);
    private static BigInteger Round(BigInteger a, BigInteger b)
    {
        var sign = a.Sign;a = BigInteger.Abs(a);var quotient = a / b;var remainder = a % b;
        if (remainder > b - remainder || (remainder == b - remainder && quotient % 2 != 0)) quotient++;
        return sign * quotient;
    }
    private static string D(BigInteger n)
    {
        var digits = BigInteger.Abs(n).ToString(System.Globalization.CultureInfo.InvariantCulture).PadLeft(13, '0');
        return (n.Sign < 0 ? "-" : "") + digits[..^12] + "." + digits[^12..];
    }
    private static BigInteger U(decimal n) => new(n * 1_000_000_000_000m);
    private static BigInteger?[] Ema(BigInteger[] input, int period)
    {
        var output = new BigInteger?[input.Length];
        if (input.Length < period) return output;
        output[period - 1] = Round(input.Take(period).Aggregate(BigInteger.Zero, (a, b) => a + b), period);
        for (var i = period; i < input.Length; i++) output[i] = Round(2 * input[i] + (period - 1) * output[i - 1]!.Value, period + 1);
        return output;
    }
    private static BigInteger Root(BigInteger n)
    {
        if (n == 0) return 0;var x = n;
        while (true) { var y = (x + n / x) / 2;if (y >= x) return x;x = y; }
    }
    private static BigInteger Percentile(IEnumerable<BigInteger> input, int q)
    {
        var ordered = input.Order().ToArray();var rank = (ordered.Length - 1) * q;var index = rank / 100;
        return Round(ordered[index] * 100 + (ordered[index + 1] - ordered[index]) * (rank % 100), 100);
    }
    internal static Row[] Evaluate(IReadOnlyList<CompletedResearchCandle> bars)
    {
        var n = bars.Count;var close = bars.Select(x => U(x.Close)).ToArray();var ema12 = Ema(close, 12);var ema20 = Ema(close, 20);var ema26 = Ema(close, 26);
        var gains = new BigInteger[n];var losses = new BigInteger[n];var tr = new BigInteger[n];
        for (var i = 0; i < n; i++)
        {
            if (i > 0) { gains[i] = BigInteger.Max(0, close[i] - close[i - 1]);losses[i] = BigInteger.Max(0, close[i - 1] - close[i]); }
            tr[i] = U(bars[i].High - bars[i].Low);
            if (i > 0) tr[i] = BigInteger.Max(tr[i], BigInteger.Max(BigInteger.Abs(U(bars[i].High) - close[i - 1]), BigInteger.Abs(U(bars[i].Low) - close[i - 1])));
        }
        BigInteger? gain = null, loss = null, atr = null, signal = null;var macdSeed = new List<BigInteger>();
        var features = new (BigInteger Atr, BigInteger Width, BigInteger Slope)?[n];var result = new Row[n];
        for (var i = 0; i < n; i++)
        {
            BigInteger? sma = null, lower = null, upper = null, rsi = null, macd = null, histogram = null, norm = null, width = null, slope = null;
            if (i >= 19)
            {
                var window = close.Skip(i - 19).Take(20).ToArray();var sum = window.Aggregate(BigInteger.Zero, (a, b) => a + b);
                sma = Round(sum, 20);var a = 20 * window.Aggregate(BigInteger.Zero, (acc, x) => acc + x * x) - sum * sum;
                var sd = Root(a) / 20;var edge = 400 * BigInteger.Pow(2 * sd + 1, 2);
                if (4 * a > edge || (4 * a == edge && !sd.IsEven)) sd++;
                lower = sma - 2 * sd;upper = sma + 2 * sd;width = Round((upper.Value - lower.Value) * Unit, sma.Value);
            }
            if (i == 13) atr = Round(tr.Take(14).Aggregate(BigInteger.Zero, (a, b) => a + b), 14);
            else if (i > 13) atr = Round(13 * atr!.Value + tr[i], 14);
            if (atr is not null) norm = Round(atr.Value * Unit, close[i]);
            if (i == 14)
            {
                gain = Round(gains.Skip(1).Take(14).Aggregate(BigInteger.Zero, (a, b) => a + b), 14);
                loss = Round(losses.Skip(1).Take(14).Aggregate(BigInteger.Zero, (a, b) => a + b), 14);
            }
            else if (i > 14) { gain = Round(13 * gain!.Value + gains[i], 14);loss = Round(13 * loss!.Value + losses[i], 14); }
            if (gain is not null) rsi = gain + loss == 0 ? 50 * Unit : Round(100 * Unit * gain.Value, gain.Value + loss!.Value);
            if (i >= 25)
            {
                macd = ema12[i] - ema26[i];
                if (i <= 33) macdSeed.Add(macd!.Value);
                if (i == 33) signal = Round(macdSeed.Aggregate(BigInteger.Zero, (a, b) => a + b), 9);
                else if (i > 33) signal = Round(2 * macd!.Value + 8 * signal!.Value, 10);
                if (signal is not null) histogram = macd - signal;
            }
            if (i >= 24) slope = Round((ema20[i]!.Value - ema20[i - 5]!.Value) * Unit, 5 * ema20[i - 5]!.Value);
            int? score = null;string? trend = null, regime = null;
            if (i >= 400)
            {
                var prior = features.Skip(i - 300).Take(300).Select(x => x!.Value).ToArray();
                score = close[i].CompareTo(ema20[i]!.Value) + histogram!.Value.Sign + (rsi > 55 * Unit ? 1 : rsi < 45 * Unit ? -1 : 0);
                trend = score >= 2 ? "Bullish" : score <= -2 ? "Bearish" : "Neutral";
                regime = norm > Percentile(prior.Select(x => x.Atr), 80) && width > Percentile(prior.Select(x => x.Width), 80) ? "Volatile" :
                    Math.Abs(score.Value) >= 2 && BigInteger.Abs(slope!.Value) > Percentile(prior.Select(x => BigInteger.Abs(x.Slope)), 70) && Math.Sign(score.Value) == slope.Value.Sign ? "Trending" :
                    Math.Abs(score.Value) <= 1 && BigInteger.Abs(slope!.Value) < Percentile(prior.Select(x => BigInteger.Abs(x.Slope)), 40) ? "Ranging" : "Undetermined";
            }
            if (i >= 100) features[i] = (norm!.Value, width!.Value, slope!.Value);
            BigInteger?[] all = [sma, ema20[i], lower, sma, upper, rsi, macd, signal, histogram, atr, norm, width, slope];
            result[i] = new Row(all.Select(x => x is null ? null : D(x.Value)).ToArray(), score, trend, regime);
        }
        return result;
    }
}
