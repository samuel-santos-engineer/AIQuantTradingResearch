using System.Globalization;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Synthetic;
using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class LocalSyntheticChartTests
{
    [Fact]
    public void GateRequiresExactLocalWindowsDevelopmentCommandAndRejectsAzure()
    {
        var env = new Dictionary<string, string?> { ["DOTNET_ENVIRONMENT"] = "Development" };
        string[] command = ["--local-synthetic-chart"];
        Assert.True(LocalSyntheticChart.IsAllowed(command, env, true));
        Assert.False(LocalSyntheticChart.IsAllowed(command, env, false));
        Assert.False(LocalSyntheticChart.IsAllowed([], env, true));
        Assert.False(LocalSyntheticChart.IsAllowed(["--local-synthetic-chart", "--urls=http://0.0.0.0"], env, true));
        Assert.False(LocalSyntheticChart.IsAllowed(command, new Dictionary<string, string?>(), true));
        env["DOTNET_ENVIRONMENT"] = "Production";
        Assert.False(LocalSyntheticChart.IsAllowed(command, env, true));
        env["DOTNET_ENVIRONMENT"] = "Development"; env["WEBSITE_INSTANCE_ID"] = "azure";
        Assert.False(LocalSyntheticChart.IsAllowed(command, env, true));
    }

    [Theory]
    [InlineData("1h", "1D")]
    [InlineData("1h", "7D")]
    [InlineData("1h", "30D")]
    [InlineData("1h", "90D")]
    [InlineData("4h", "1D")]
    [InlineData("4h", "7D")]
    [InlineData("4h", "30D")]
    [InlineData("4h", "90D")]
    [InlineData("1d", "1D")]
    [InlineData("1d", "7D")]
    [InlineData("1d", "30D")]
    [InlineData("1d", "90D")]
    public async Task PayloadHasOnlyVisibleOhlcvAndVerifiedWarmedCSharpSma(string intervalText, string rangeText)
    {
        Assert.True(LocalSyntheticChart.TrySelection(intervalText, rangeText, out var interval, out var range));
        var payload = await LocalSyntheticChart.CreatePayloadAsync(interval, range);
        Assert.Equal(LocalSyntheticChart.Label, payload.Label);
        Assert.False(payload.PublicRuntimeVerified); Assert.Equal(intervalText, payload.Interval); Assert.Equal(rangeText, payload.Range);
        var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation,
            interval, range, TimeSpan.FromSeconds(10));
        Assert.Equal(SyntheticPublicationPolicy.VisibleCount(request), payload.Candles.Count);
        Assert.Equal(250, payload.Warmup); Assert.Equal(payload.Candles.Count + 250, payload.CalculationCount);
        Assert.Equal(SyntheticPublicationPolicy.Timestamp(SyntheticPublicationPolicy.CoverageEnd.AddDays(-SyntheticPublicationPolicy.Days(range))), payload.Candles[0].TimeUtc);
        var response = await new SyntheticHistoricalMarketDataProvider().GetHistoricalCandlesAsync(request);
        var expected = TechnicalIndicatorCalculator.CalculatePublic(request, response.Candles!, response.Provenance!);
        for (var i = 0; i < payload.Candles.Count; i++)
        {
            Assert.Equal(expected.Rows[i + 250].Sma20, decimal.Parse(payload.Candles[i].Sma20, CultureInfo.InvariantCulture));
            Assert.Equal(expected.Rows[i + 250].Ema20, decimal.Parse(payload.Candles[i].Ema20, CultureInfo.InvariantCulture));
            Assert.Equal(expected.Rows[i + 250].BollingerLower, decimal.Parse(payload.Candles[i].BollingerLower, CultureInfo.InvariantCulture));
            Assert.Equal(expected.Rows[i + 250].BollingerMiddle, decimal.Parse(payload.Candles[i].BollingerMiddle, CultureInfo.InvariantCulture));
            Assert.Equal(expected.Rows[i + 250].BollingerUpper, decimal.Parse(payload.Candles[i].BollingerUpper, CultureInfo.InvariantCulture));
            Assert.Equal(response.Candles![i + 250].Volume, decimal.Parse(payload.Candles[i].Volume, CultureInfo.InvariantCulture));
        }
        Assert.False(LocalSyntheticChart.TrySelection("1m", "1D", out _, out _));
        Assert.False(LocalSyntheticChart.TrySelection("1h", "live", out _, out _));
    }
}
