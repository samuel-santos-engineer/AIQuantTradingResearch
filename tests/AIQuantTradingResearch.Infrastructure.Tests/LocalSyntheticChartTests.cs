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

    [Fact]
    public async Task PayloadHasOnly24VisibleCandlesAndVerifiedWarmedCSharpSma()
    {
        var payload = await LocalSyntheticChart.CreatePayloadAsync();
        Assert.Equal(LocalSyntheticChart.Label, payload.Label);
        Assert.False(payload.PublicRuntimeVerified); Assert.Equal("1h", payload.Interval); Assert.Equal("1D", payload.Range);
        Assert.Equal(24, payload.Candles.Count);
        Assert.Equal("2026-09-30T00:00:00Z", payload.Candles[0].TimeUtc);
        Assert.Equal("2026-09-30T23:00:00Z", payload.Candles[^1].TimeUtc);
        var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation,
            HistoricalCandleInterval.OneHour, HistoricalCandleRange.OneDay, TimeSpan.FromSeconds(10));
        var response = await new SyntheticHistoricalMarketDataProvider().GetHistoricalCandlesAsync(request);
        var expected = TechnicalIndicatorCalculator.CalculatePublic(request, response.Candles!, response.Provenance!);
        for (var i = 0; i < 24; i++)
            Assert.Equal(expected.Rows[i + 250].Sma20, decimal.Parse(payload.Candles[i].Sma20, CultureInfo.InvariantCulture));
        var page = LocalSyntheticChart.Render(payload);
        Assert.Equal(24, page.Split("class='candle ").Length - 1);
        Assert.Contains("class=\"sma\"", page); Assert.Contains("UTC", page); Assert.Contains(LocalSyntheticChart.Label, page);
        Assert.DoesNotContain("<script", page); Assert.DoesNotContain("cdn.", page);
    }
}
