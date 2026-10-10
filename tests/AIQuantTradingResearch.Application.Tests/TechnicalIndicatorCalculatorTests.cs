using System.Globalization;
using System.Numerics;
using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class TechnicalIndicatorCalculatorTests
{
    private static readonly DateTimeOffset Start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static HistoricalMarketDataProvenance Provenance(string provider = "independent-test") => new(provider, Start, Start, false, true);
    private static TechnicalIndicatorInputIdentity Identity(string source = "independent-test") => new("aiq-candles-v2", "private-research", source,
        CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, Start, new string('a', 64), new string('b', 64));
    private static HistoricalCandle[] Candles(IEnumerable<decimal> values) => values.Select((x, i) => new HistoricalCandle(
        CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, Start.AddHours(i), x, x, x, x, 0m)).ToArray();
    private static TechnicalIndicatorResult Calculate(IEnumerable<decimal> values) => TechnicalIndicatorCalculator.Calculate(Candles(values), Identity(), Provenance());

    [Theory]
    [InlineData(0, "100", "100", 50, 0)]
    [InlineData(1, "10.5", "11.5", 100, 7)]
    [InlineData(-1, "30.5", "29.5", 0, -7)]
    public void IndependentlyDerivedConstantRisingFalling(int direction, string sma, string ema, int rsi, int macd)
    {
        var values = Enumerable.Range(1, 40).Select(i => direction == 0 ? 100m : direction == 1 ? i : 41m - i);
        var rows = Calculate(values).Rows;
        Assert.Equal(decimal.Parse(sma, CultureInfo.InvariantCulture), rows[19].Sma20); Assert.Equal(decimal.Parse(ema, CultureInfo.InvariantCulture), rows[20].Ema20);
        Assert.Equal(rsi, rows[14].Rsi14); Assert.Equal(macd, rows[25].Macd);
        Assert.Equal(macd, rows[33].MacdSignal); Assert.Equal(0m, rows[33].MacdHistogram);
        if (direction == 0) Assert.All(rows.Skip(19), row => { Assert.Equal(100m, row.BollingerLower); Assert.Equal(100m, row.BollingerUpper); });
        if (direction == 1)
        {
            Assert.Equal(-1.032562594670m, rows[19].BollingerLower);
            Assert.Equal(10.500000000000m, rows[19].BollingerMiddle);
            Assert.Equal(22.032562594670m, rows[19].BollingerUpper);
        }
    }

    [Fact]
    public void FirstValidSamplesAndNullsAreExact()
    {
        var rows = Calculate(Enumerable.Repeat(100m, 40)).Rows;
        for (var i = 0; i < rows.Count; i++)
        {
            Assert.Equal(i < 19, rows[i].Sma20 is null); Assert.Equal(i < 19, rows[i].Ema20 is null);
            Assert.Equal(i < 14, rows[i].Rsi14 is null); Assert.Equal(i < 25, rows[i].Macd is null);
            Assert.Equal(i < 33, rows[i].MacdSignal is null); Assert.Equal(i < 33, rows[i].MacdHistogram is null);
            Assert.Equal(i < 19, rows[i].BollingerLower is null);
        }
        Assert.Null(Calculate([1m]).Rows[0].Sma20);
    }

    [Fact]
    public void AlternatingWilderRecurrenceMatchesIndependentRationalDerivation()
    {
        var rows = Calculate(Enumerable.Range(0, 40).Select(i => 100m + i % 2)).Rows;
        Assert.Equal(50m, rows[14].Rsi14); Assert.Equal(53.571428571400m, rows[15].Rsi14);
    }

    // Authored before executing the production calculator. CPython Decimal, precision 90,
    // ROUND_HALF_EVEN quantization to 1e-12 at every seed/recurrence; population variance
    // computed from Decimal sums and Decimal.sqrt (not the production integer-root algorithm).
    // Mixed inputs: 100 + ((i*17)%23) - 11 + (i%3)/4, i=0..39. No generator/provider data.
    [Theory]
    [InlineData(20, "100.087500000000", "100.087500000000", "49.233653672772", null, null, null, "86.308643915404", "113.866356084596")]
    [InlineData(26, "99.737500000000", "99.932045178859", "52.316386083525", "-0.671270135261", null, null, "86.922981866258", "112.552018133742")]
    [InlineData(34, "100.050000000000", "101.058036771685", "49.993903036163", "0.321207369221", "0.011836810754", "0.309370558467", "86.965085021292", "113.134914978708")]
    [InlineData(40, "100.850000000000", "100.765853314671", "53.832965455167", "0.065899915299", "-0.024603454309", "0.090503369608", "87.642615701812", "114.057384298188")]
    public void MixedSeriesMatchesIndependentDecimalOracle(int sample, string sma, string ema, string rsi, string? macd,
        string? signal, string? histogram, string lower, string upper)
    {
        var row = Calculate(Enumerable.Range(0, 40).Select(i => 100m + i * 17 % 23 - 11 + (i % 3) / 4m)).Rows[sample - 1];
        static decimal? Parse(string? value) => value is null ? null : decimal.Parse(value, CultureInfo.InvariantCulture);
        Assert.Equal(Parse(sma), row.Sma20); Assert.Equal(Parse(ema), row.Ema20); Assert.Equal(Parse(rsi), row.Rsi14);
        Assert.Equal(Parse(macd), row.Macd); Assert.Equal(Parse(signal), row.MacdSignal); Assert.Equal(Parse(histogram), row.MacdHistogram);
        Assert.Equal(Parse(lower), row.BollingerLower); Assert.Equal(Parse(sma), row.BollingerMiddle); Assert.Equal(Parse(upper), row.BollingerUpper);
    }

    [Theory]
    [InlineData(1, 2, 0)] [InlineData(3, 2, 2)] [InlineData(-1, 2, 0)] [InlineData(-3, 2, -2)]
    [InlineData(5, 2, 2)] [InlineData(-5, 2, -2)] [InlineData(7, 2, 4)] [InlineData(-7, 2, -4)]
    [InlineData(4, 3, 1)] [InlineData(-5, 3, -2)]
    public void SignedExactRationalRounding(int numerator, int denominator, int expected) =>
        Assert.Equal(new BigInteger(expected), TechnicalIndicatorCalculator.Round(numerator, denominator));

    [Theory]
    [InlineData(99, 0)] [InlineData(100, 0)] [InlineData(101, 1)]
    [InlineData(899, 1)] [InlineData(900, 2)] [InlineData(901, 2)]
    [InlineData(2499, 2)] [InlineData(2500, 2)] [InlineData(2501, 3)]
    public void SquareRootMidpointsUseExactComparison(int a, int expected) =>
        Assert.Equal(new BigInteger(expected), TechnicalIndicatorCalculator.PopulationSigma(a));

    [Fact]
    public void ArbitraryPrecisionIntermediatesAndMaximumInputAreSafe()
    {
        var root = BigInteger.Pow(10, 50);
        Assert.Equal(root, TechnicalIndicatorCalculator.FloorSquareRoot(root * root));
        Assert.Equal(root - 1, TechnicalIndicatorCalculator.FloorSquareRoot(root * root - 1));
        var rows = Calculate(Enumerable.Repeat(1_000_000_000m, 2410)).Rows;
        Assert.Equal(2410, rows.Count); Assert.Equal(1_000_000_000m, rows[^1].BollingerUpper);
        Assert.Equal(0m, rows[^1].MacdHistogram);
        Assert.Throws<ArgumentOutOfRangeException>(() => TechnicalIndicatorCalculator.FloorSquareRoot(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => TechnicalIndicatorCalculator.Round(1, 0));
    }

    [Fact]
    public void NoLookaheadAndAdapterIndependentDeterminism()
    {
        var values = Enumerable.Range(0, 60).Select(i => 100m + i % 7).ToArray();
        var full = Calculate(values); var prefix = Calculate(values.Take(40));
        Assert.Equal(prefix.Rows, full.Rows.Take(40));
        var alternate = TechnicalIndicatorCalculator.Calculate(Candles(values), Identity("alternate-test"), Provenance("alternate-test"));
        Assert.Equal(full.Rows, alternate.Rows); Assert.Equal("alternate-test", alternate.Identity.Source);
        Assert.Equal(TechnicalIndicatorParameters.Fixed, full.Parameters);
        Assert.Equal("aiq-ta-v1", full.Parameters.Version); Assert.Equal(12, full.Parameters.Scale);
        Assert.Throws<NotSupportedException>(() => ((IList<TechnicalIndicatorRow>)full.Rows)[0] = full.Rows[0]);
    }

    [Fact]
    public void InvalidCountsPrecisionBoundsAndCancellationFailClosed()
    {
        Assert.Throws<ArgumentException>(() => Calculate([]));
        Assert.Throws<ArgumentException>(() => Calculate(Enumerable.Repeat(1m, 2411)));
        Assert.Throws<ArgumentException>(() => Calculate([1.0000000000001m]));
        Assert.Throws<ArgumentException>(() => Calculate([1_000_000_001m]));
        Assert.Throws<ArgumentException>(() => Calculate([decimal.MaxValue]));
        using var cancellation = new CancellationTokenSource(); cancellation.Cancel();
        Assert.Throws<OperationCanceledException>(() => TechnicalIndicatorCalculator.Calculate(Candles([1m]), Identity(), Provenance(), cancellation.Token));
    }

    [Fact]
    public void GapsDuplicatesOrderMisalignmentAndInstrumentMismatchAreRejected()
    {
        var rows = Candles([1m, 2m, 3m]);
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate([rows[0], rows[2]], Identity(), Provenance()));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate([rows[0], rows[0]], Identity(), Provenance()));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(rows.Reverse().ToArray(), Identity(), Provenance()));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(rows, Identity() with { Symbol = CanonicalMarketSymbol.EthUsd }, Provenance()));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(rows, Identity() with { CalculationStartUtc = Start.AddMinutes(1) }, Provenance()));
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate([null!], Identity(), Provenance()));
    }

    [Theory]
    [InlineData("source")] [InlineData("version")] [InlineData("context")] [InlineData("fingerprint")]
    [InlineData("stale")] [InlineData("open")]
    public void ProvenanceAndIdentityMismatchIsRejected(string invalid)
    {
        var identity = Identity(); var provenance = Provenance();
        identity = invalid switch { "source" => identity with { Source = "Vike" }, "version" => identity with { CanonicalVersion = "v1" },
            "context" => identity with { Context = "unknown" }, "fingerprint" => identity with { WindowFingerprint = "bad" }, _ => identity };
        if (invalid == "stale") provenance = new("independent-test", Start, Start, true, true);
        if (invalid == "open") provenance = new("independent-test", Start, Start, false, false);
        Assert.Throws<ArgumentException>(() => TechnicalIndicatorCalculator.Calculate(Candles([100m]), identity, provenance));
    }
}
