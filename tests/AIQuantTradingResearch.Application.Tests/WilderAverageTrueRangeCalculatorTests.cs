using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class WilderAverageTrueRangeCalculatorTests
{
    private static readonly DateTimeOffset Start = new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static HistoricalCandle[] Candles(int count, Func<int, (decimal Close, decimal High, decimal Low)> values) =>
        Enumerable.Range(0, count).Select(i => { var (c, h, l) = values(i); return new HistoricalCandle(
            CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour, Start.AddHours(i), c, h, l, c, 0); }).ToArray();

    [Theory]
    [InlineData(0)]
    [InlineData(2)]
    public void ConstantAndZeroVolatilityHaveExactWarmup(decimal spread)
    {
        var rows = WilderAverageTrueRangeCalculator.Calculate(Candles(40, _ => (100, 100 + spread / 2, 100 - spread / 2)));
        Assert.All(rows.Take(13), x => Assert.Null(x));
        Assert.All(rows.Skip(13), x => Assert.Equal(spread, x));
        Assert.Null(WilderAverageTrueRangeCalculator.Calculate(Candles(1, _ => (100, 100, 100)))[0]);
    }

    // Independently derived with CPython Decimal precision90, ROUND_HALF_EVEN 1e-12.
    // TR=[2,11,...]; seed145/14; then (13*roundedSeed+11)/14.
    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    public void PreviousCloseGapsAndWilderSeedMatchIndependentVectors(int direction)
    {
        var rows = WilderAverageTrueRangeCalculator.Calculate(Candles(30, i => (500 + direction * 10 * i, 501 + direction * 10 * i, 499 + direction * 10 * i)));
        Assert.Equal(10.357142857143m, rows[13]);
        Assert.Equal(10.403061224490m, rows[14]);
    }

    [Fact]
    public void ChangingHighLowUsesTrueRangeNotCloseOnly()
    {
        var rows = WilderAverageTrueRangeCalculator.Calculate(Candles(16, i => (100, 101 + i, 99 - i)));
        Assert.Equal(15m, rows[13]);
        Assert.Equal(16.071428571429m, rows[14]);
        Assert.Equal(17.209183673470m, rows[15]);
    }

    [Theory]
    [InlineData(7, 0)]
    [InlineData(21, 2)]
    public void SeedMidpointTiesRoundToEven(int units, int expected)
    {
        var rows = WilderAverageTrueRangeCalculator.Calculate(Candles(14, i => (100, 100 + (i == 0 ? units * 0.000000000001m : 0), 100)));
        Assert.Equal(expected * 0.000000000001m, rows[13]);
    }

    [Fact]
    public void RecurrenceMidpointTiesRoundToEven()
    {
        var rows = WilderAverageTrueRangeCalculator.Calculate(Candles(16, i => (100, 100 + (i < 14 ? 2 : i == 14 ? 9 : 23) * 0.000000000001m, 100)));
        Assert.Equal(0.000000000002m, rows[13]);
        Assert.Equal(0.000000000002m, rows[14]);
        Assert.Equal(0.000000000004m, rows[15]);
    }

    [Fact]
    public void BoundedLargeIntermediatesAreFiniteAndNoLookahead()
    {
        var input = Candles(2410, i => i % 2 == 0 ? (1_000_000_000m, 1_000_000_000m, 1m) : (1m, 1_000_000_000m, 1m));
        var rows = WilderAverageTrueRangeCalculator.Calculate(input);
        Assert.Equal(2410, rows.Count);
        Assert.All(rows.Skip(13), x => Assert.Equal(999_999_999m, x));
        Assert.Equal(rows.Take(20), WilderAverageTrueRangeCalculator.Calculate(input.Take(20).ToArray()));
    }

    [Fact]
    public void RejectsMissingBoundsBadOhlcvPrecisionAndGrid()
    {
        Assert.Throws<ArgumentNullException>(() => WilderAverageTrueRangeCalculator.Calculate(null!));
        Assert.Throws<ArgumentException>(() => WilderAverageTrueRangeCalculator.Calculate([]));
        Assert.Throws<ArgumentException>(() => WilderAverageTrueRangeCalculator.Calculate(Candles(2411, _ => (100, 101, 99))));
        var original = Candles(20, _ => (100, 101, 99));
        foreach (var bad in new HistoricalCandle[] { null!,
            new(CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour, Start, 100, 1_000_000_001m, 99, 100, 0),
            new(CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour, Start, 100, 101.0000000000001m, 99, 100, 0),
            new(CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour, Start.AddMinutes(1), 100, 101, 99, 100, 0) })
        {
            var input = original.ToArray(); input[0] = bad;
            Assert.ThrowsAny<ArgumentException>(() => WilderAverageTrueRangeCalculator.Calculate(input));
        }
        foreach (var stamp in new[] { Start, Start.AddHours(3), Start.AddHours(-1) })
        {
            var input = original.ToArray();
            input[1] = new(CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour, stamp, 100, 101, 99, 100, 0);
            Assert.Throws<ArgumentException>(() => WilderAverageTrueRangeCalculator.Calculate(input));
        }
        var mixed = original.ToArray();
        mixed[1] = new(CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, Start.AddHours(1), 100, 101, 99, 100, 0);
        Assert.Throws<ArgumentException>(() => WilderAverageTrueRangeCalculator.Calculate(mixed));
        // Canonical candle construction itself rejects malformed OHLCV.
        Assert.Throws<ArgumentException>(() => new HistoricalCandle(CanonicalMarketSymbol.BtcLikeSimulation,
            HistoricalCandleInterval.OneHour, Start, 100, 99, 98, 100, 0));

    }

    [Fact]
    public void CancellationAndPublicProvenanceFailClosed()
    {
        using var cancel = new CancellationTokenSource(); cancel.Cancel();
        var input = Candles(20, _ => (100, 101, 99));
        Assert.Throws<OperationCanceledException>(() => WilderAverageTrueRangeCalculator.Calculate(input, cancel.Token));
        var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation, HistoricalCandleInterval.OneHour,
            HistoricalCandleRange.OneDay, TimeSpan.FromSeconds(10));
        var privateProvenance = new HistoricalMarketDataProvenance("private-provider", Start, Start, false, true);
        Assert.ThrowsAny<ArgumentException>(() => WilderAverageTrueRangeCalculator.CalculatePublic(request, input, privateProvenance));
    }
}
