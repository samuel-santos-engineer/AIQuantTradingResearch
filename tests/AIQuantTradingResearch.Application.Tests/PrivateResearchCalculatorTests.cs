using System.Numerics;
using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class PrivateResearchCalculatorTests
{
    internal static readonly DateTimeOffset Start = new(2026, 10, 1, 0, 0, 0, TimeSpan.Zero);
    [Theory] [InlineData("flat")] [InlineData("rising")] [InlineData("falling")] [InlineData("mixed")] [InlineData("large")] [InlineData("ratios")] [InlineData("ohlc")]
    public void EntireSeriesMatchesIndependentBatchOracle(string family)
    {
        var rows = Rows(family, 510);var oracle = PrivateResearchReference.Evaluate(rows.Select(x => x.Candle).ToArray());
        var actual = PrivateResearchCalculator.Project(Candidate(rows));
        for (var i = 0; i < rows.Length; i++) Equal(oracle[i], actual.Candles[i].Calculation!);
    }
    [Fact] public void NullAlignmentAndFlat401ReadinessUseExactSeeds()
    {
        var rows = PrivateResearchCalculator.Project(Candidate(Rows("flat", 450))).Candles;
        Assert.Null(rows[12].Calculation!.Indicators.Atr14);Assert.Equal("0.000000000000", rows[13].Calculation!.Indicators.Atr14);
        Assert.Null(rows[13].Calculation!.Indicators.Rsi14);Assert.Equal("50.000000000000", rows[14].Calculation!.Indicators.Rsi14);
        Assert.Null(rows[18].Calculation!.Indicators.Sma20);Assert.Equal("100.000000000000", rows[19].Calculation!.Indicators.Sma20);
        Assert.Equal("100.000000000000", rows[19].Calculation!.Indicators.Ema20);Assert.Equal("100.000000000000", rows[19].Calculation!.Indicators.BollingerLower);
        Assert.Null(rows[24].Calculation!.Indicators.Macd);Assert.Equal("0.000000000000", rows[25].Calculation!.Indicators.Macd);
        Assert.Null(rows[32].Calculation!.Indicators.MacdSignal);Assert.Equal("0.000000000000", rows[33].Calculation!.Indicators.MacdSignal);
        Assert.False(rows[399].Calculation!.Classification.Valid);Assert.Null(rows[399].Calculation!.Classification.TrendScore);
        Assert.Equal(new ResearchClassification(true, 0, "Neutral", "Undetermined"), rows[400].Calculation!.Classification);
        Assert.Single(rows, x => x.Calculation!.TrendTransition is not null);
    }
    [Fact] public void AnalyticalRampsAndVolatilePrecedenceAgreeWithDerivedValues()
    {
        var up = PrivateResearchCalculator.Project(Candidate(Rows("rising", 401))).Candles;
        Assert.Equal("10.500000000000", up[19].Calculation!.Indicators.Ema20);Assert.Equal("100.000000000000", up[14].Calculation!.Indicators.Rsi14);
        Assert.Equal("7.000000000000", up[25].Calculation!.Indicators.Macd);Assert.Equal("7.000000000000", up[33].Calculation!.Indicators.MacdSignal);
        Assert.Equal("0.928571428571", up[13].Calculation!.Indicators.Atr14);
        var down = PrivateResearchCalculator.Project(Candidate(Rows("falling", 401))).Candles;
        Assert.Equal("0.000000000000", down[14].Calculation!.Indicators.Rsi14);
        Assert.Equal(new ResearchClassification(true, -2, "Bearish", "Volatile"), down[400].Calculation!.Classification);
    }
    [Theory] [InlineData(1, 2, 0)] [InlineData(3, 2, 2)] [InlineData(-1, 2, 0)] [InlineData(-3, 2, -2)] [InlineData(5, 2, 2)] [InlineData(-5, 2, -2)]
    public void SignedMidpointRoundingIsSymmetricAndEven(int numerator, int denominator, int expected) => Assert.Equal(new BigInteger(expected), PrivateResearchCalculator.Round(numerator, denominator));
    [Theory] [InlineData(2, 0)] [InlineData(4, 2)]
    public void BollingerTrueMidpointTiesAndNegativeBands(int secondUnits, int sdUnits)
    {
        var segment = Guid.NewGuid();var rows = Enumerable.Range(0, 20).Select(i =>
        {
            var price = (i < 10 ? 1 : secondUnits) / 1_000_000_000_000m;
            return new ResearchStoredCandle(new(ResearchInstrument.BtcUsd, Start.AddMinutes(i), price, price, price, price, 1), segment, i + 1);
        }).ToArray();
        var actual = PrivateResearchCalculator.Project(Candidate(rows)).Candles[^1].Calculation!.Indicators;
        Assert.Equal(((2 - 2 * sdUnits) / 1_000_000_000_000m).ToString("F12", System.Globalization.CultureInfo.InvariantCulture), actual.BollingerLower);
        Assert.Equal(((2 + 2 * sdUnits) / 1_000_000_000_000m).ToString("F12", System.Globalization.CultureInfo.InvariantCulture), actual.BollingerUpper);
        Equal(PrivateResearchReference.Evaluate(rows.Select(x => x.Candle).ToArray())[^1], PrivateResearchCalculator.Project(Candidate(rows)).Candles[^1].Calculation!);
        if (secondUnits == 4) Assert.StartsWith("-", actual.BollingerLower);
    }
    [Theory] [InlineData(3, 0)] [InlineData(4, 1)]
    public void BollingerSquareRootImmediatelyBelowAndAboveMidpoint(int lastUnits, int sdUnits)
    {
        var segment = Guid.NewGuid();var rows = Enumerable.Range(0, 20).Select(i =>
        {
            var p = (i == 19 ? lastUnits : 1) / 1_000_000_000_000m;
            return new ResearchStoredCandle(new(ResearchInstrument.BtcUsd, Start.AddMinutes(i), p, p, p, p, 1), segment, i + 1);
        }).ToArray();
        var result = PrivateResearchCalculator.Project(Candidate(rows)).Candles[^1].Calculation!;
        var middleUnits = 1; // Both means round to one integer unit.
        Assert.Equal(((middleUnits + 2 * sdUnits) / 1_000_000_000_000m).ToString("F12", System.Globalization.CultureInfo.InvariantCulture), result.Indicators.BollingerUpper);
        Equal(PrivateResearchReference.Evaluate(rows.Select(x => x.Candle).ToArray())[^1], result);
    }
    [Fact] public void IndependentTrendTransitionsExcludeRegimeOnlyChanges()
    {
        var rows = Rows("mixed", 700);var oracle = PrivateResearchReference.Evaluate(rows.Select(x => x.Candle).ToArray());
        var actual = PrivateResearchCalculator.Project(Candidate(rows));
        var regimeOnly = Enumerable.Range(401, rows.Length - 401).Where(i => oracle[i].Trend == oracle[i - 1].Trend && oracle[i].Regime != oracle[i - 1].Regime).ToArray();
        Assert.NotEmpty(regimeOnly);
        foreach (var i in regimeOnly) Assert.Null(actual.Candles[i].Calculation!.TrendTransition);
        string? previous = null;
        for (var i = 0; i < rows.Length; i++)
        {
            var expected = oracle[i].Trend is not null && oracle[i].Trend != previous;
            Assert.Equal(expected, actual.Candles[i].Calculation!.TrendTransition is not null);
            if (expected) Assert.Equal(previous, actual.Candles[i].Calculation!.TrendTransition!.PreviousTrend);
            if (oracle[i].Trend is not null) previous = oracle[i].Trend;
        }
    }
    [Fact] public void CheckpointPruningAndResumeMatchUninterruptedReferenceExactly()
    {
        var full = Rows("mixed", 1400);var first = PrivateResearchCalculator.Project(Candidate(full.Take(1000).ToArray()));
        var resumed = PrivateResearchCalculator.Project(Candidate(first.Candles.Concat(full.Skip(1000)).ToArray(), 2, first.Generation), first.Checkpoint);
        var reference = PrivateResearchReference.Evaluate(full.Select(x => x.Candle).ToArray());
        Assert.Equal(1000, resumed.Candles.Count);
        for (var i = 0; i < 1000; i++) Equal(reference[i + 400], resumed.Candles[i].Calculation!);
        var tail = new ResearchStoredCandle(new(ResearchInstrument.BtcUsd, Start.AddMinutes(1400), 100, 100, 100, 100, 1), full[^1].Segment, 1401);
        var again = PrivateResearchCalculator.Project(Candidate(resumed.Candles.Append(tail).ToArray(), 3, first.Generation), resumed.Checkpoint);
        var finalReference = PrivateResearchReference.Evaluate(full.Select(x => x.Candle).Append(tail.Candle).ToArray());
        Equal(finalReference[^1], again.Candles[^1].Calculation!);
    }
    [Fact] public void GapStartsNewSeedsAndNoCrossGapTrueRange()
    {
        var first = Rows("rising", 100);var second = Rows("flat", 401).Select(x => x with { Candle = new(ResearchInstrument.BtcUsd, x.Candle.OpenTimeUtc.AddMinutes(101), 100, 100, 100, 100, 1) }).ToArray();
        var rows = PrivateResearchCalculator.Project(Candidate(first.Concat(second).ToArray())).Candles;
        Assert.Null(rows[100].Calculation!.Indicators.Atr14);Assert.Equal("0.000000000000", rows[113].Calculation!.Indicators.Atr14);
        Assert.NotNull(rows[100].Calculation!.SegmentStart);Assert.False(rows[499].Calculation!.Classification.Valid);Assert.True(rows[500].Calculation!.Classification.Valid);
    }
    [Fact] public void InsufficientPrunedOriginNeverReseedsToFalseReady()
    {
        var rows = Rows("flat", 1200).Skip(200).ToArray();var projection = PrivateResearchCalculator.Project(Candidate(rows));
        Assert.All(projection.Candles, x => Assert.Null(x.Calculation));
    }
    [Theory] [InlineData("generation")] [InlineData("instrument")] [InlineData("version")] [InlineData("state")] [InlineData("missing-state")]
    public void StaleMixedOrMalformedCheckpointIsRejected(string mutation)
    {
        var rows = Rows("flat", 420);var first = PrivateResearchCalculator.Project(Candidate(rows));
        var cp = mutation switch { "generation" => first.Checkpoint with { Generation = Guid.NewGuid() }, "instrument" => first.Checkpoint with { Instrument = ResearchInstrument.EthUsd }, "version" => first.Checkpoint with { Version = 0 }, "missing-state" => first.Checkpoint with { State = "{}" }, _ => first.Checkpoint with { State = "{" } };
        Assert.Throws<ResearchUnavailableException>(() => PrivateResearchCalculator.Project(Candidate(rows, 2, first.Generation), cp));
    }
    [Fact] public void InvalidOrderSegmentAndCancellationFailClosed()
    {
        var rows = Rows("flat", 20);Assert.Throws<ResearchUnavailableException>(() => PrivateResearchCalculator.Project(Candidate(rows.Reverse().ToArray())));
        rows[1] = rows[1] with { SegmentIndex = 9 };Assert.Throws<ResearchUnavailableException>(() => PrivateResearchCalculator.Project(Candidate(rows)));
        Assert.Throws<OperationCanceledException>(() => PrivateResearchCalculator.Project(Candidate(Rows("flat", 20)), cancellationToken: new CancellationToken(true)));
    }
    [Fact] public void AppendedCurrentObservationCannotChangePriorResults()
    {
        var rows = Rows("mixed", 401);var first = PrivateResearchCalculator.Project(Candidate(rows.Take(400).ToArray()));var all = PrivateResearchCalculator.Project(Candidate(rows));
        for (var i = 0; i < 400; i++) Assert.Equal(first.Candles[i].Calculation, all.Candles[i].Calculation);
    }
    internal static ResearchStoredCandle[] Rows(string family, int count)
    {
        var segment = Guid.NewGuid();return Enumerable.Range(0, count).Select(i =>
        {
            var price = family switch { "flat" => 100m, "rising" => i + 1m, "falling" => 1000m - i, "large" => 1_000_000_000m - i % 7 / 1_000_000_000_000m, "ratios" => i % 27 == 0 ? 0.000000000001m : 999999999m, _ => 1000m + (i * 37 % 127) - (i % 11) / 3m };
            price = decimal.Round(price, 12, MidpointRounding.ToEven);
            return new ResearchStoredCandle(new(ResearchInstrument.BtcUsd, Start.AddMinutes(i), price, family == "ohlc" ? price + 3 : price, family == "ohlc" ? price - 2 : price, price, 1), segment, i + 1);
        }).ToArray();
    }
    internal static ResearchCommittedState Candidate(ResearchStoredCandle[] rows, long version = 1, Guid? generation = null) =>
        new(generation ?? Guid.NewGuid(), ResearchInstrument.BtcUsd, version, rows, Start.AddDays(3), Start.AddDays(3), ResearchIntegrity.Protected);
    private static void Equal(PrivateResearchReference.Row expected, ResearchCalculation actual)
    {
        var x = actual.Indicators;Assert.Equal(expected.Values, new[] { x.Sma20, x.Ema20, x.BollingerLower, x.BollingerMiddle, x.BollingerUpper, x.Rsi14, x.Macd, x.MacdSignal, x.MacdHistogram, x.Atr14, x.NormalizedAtr14, x.BollingerWidth, x.EmaSlope5 });
        Assert.Equal(expected.Score, actual.Classification.TrendScore);Assert.Equal(expected.Trend, actual.Classification.Trend);Assert.Equal(expected.Regime, actual.Classification.Regime);
    }
}
