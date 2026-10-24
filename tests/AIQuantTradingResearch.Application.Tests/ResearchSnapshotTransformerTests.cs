using System.Numerics;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class ResearchSnapshotTransformerTests
{
    [Theory]
    [InlineData(2026, 10, 23, "916024341133", "91.602434113300", "-6.412170387931")]
    [InlineData(2026, 10, 24, "913126768802", "91.312676880200", "-6.391887381614")]
    public void IndependentPythonHmacAndSignedPriceVectors(int year, int month, int day, string factor, string price, string negative)
    {
        var config = new ResearchTransformConfiguration(Guid.NewGuid(), Enumerable.Range(0, 32).Select(i => (byte)i).ToArray(), Guid.Parse("11111111-1111-4111-8111-111111111111"), 1);
        var actual = ResearchSnapshotTransformer.DailyFactor(config, new DateOnly(year, month, day));
        Assert.Equal(BigInteger.Parse(factor, System.Globalization.CultureInfo.InvariantCulture), actual);
        Assert.Equal(price, ResearchSnapshotTransformer.TransformPrice("100.000000000000", actual));
        Assert.Equal(negative, ResearchSnapshotTransformer.TransformPrice("-7.000000000000", actual));
        Assert.Equal("0.000000000001", ResearchSnapshotTransformer.TransformPrice("0.000000000001", actual));
    }
    [Theory] [InlineData(1, 2, 0)] [InlineData(3, 2, 2)] [InlineData(-1, 2, 0)] [InlineData(-3, 2, -2)]
    public void SignedRoundingUsesEvenTies(int n, int d, int expected) => Assert.Equal(new BigInteger(expected), ResearchSnapshotTransformer.RoundEven(n, d));
    [Theory] [InlineData("-0.000000000000")] [InlineData("1.0")] [InlineData("+1.000000000000")] [InlineData("1e0")]
    public void NonCanonicalPrivateDecimalsFailClosed(string value) => Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotTransformer.Units(value));
    [Theory]
    [InlineData("{\"schemaVersion\":\"1.0\",\"schemaVersion\":\"1.0\"}")]
    [InlineData("{\"x\":\"\\ud800\"}")]
    [InlineData("{}{}")]
    [InlineData("{\"x\":1e999999999999999}")]
    [InlineData("{\"x\":NaN}")]
    public void StrictRequestParsingRejectsMalformedJson(string json) => Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotJson.Request(Encoding.UTF8.GetBytes(json)));
    [Fact] public void RequestTypesAndExactIdentityAreValidated()
    {
        const string valid = "{\"schemaVersion\":\"1.1\",\"instrument\":\"BTC/USD\",\"requestEpoch\":\"1\",\"knownIdentity\":null}";
        Assert.Equal("1.1", ResearchSnapshotJson.Request(Encoding.UTF8.GetBytes(valid)).SchemaVersion);
        Assert.Equal("1", ResearchSnapshotJson.Request(Encoding.UTF8.GetBytes(valid.PadRight(4096))).RequestEpoch);
        foreach (var mutation in new[] { valid.Replace("\"1\"", "1"), valid.Replace("BTC/USD", "DOGE/USD"), valid.Replace("null", "{}"), valid.Replace("1.1", "2.0"), valid.Replace("null}", "null,\"extra\":0}") })
            Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotJson.Request(Encoding.UTF8.GetBytes(mutation)));
        Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotJson.Request(new byte[4097]));
        Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotJson.Request([0xff]));
    }
    [Fact] public void FullUnchangedDayAndEpochIdentityAreExactAndEmptyIsTruthful()
    {
        var g = Guid.NewGuid(); var cfg = new ResearchTransformConfiguration(g, new byte[32], Guid.NewGuid(), 1);
        var state = new ResearchCommittedState(g, ResearchInstrument.BtcUsd, 0, [], null, null, ResearchIntegrity.Protected);
        var now = new DateTimeOffset(2026, 10, 23, 12, 0, 0, TimeSpan.Zero);
        var request = new ResearchSnapshotRequest("1.0", "BTC/USD", "1", null);
        using var full = JsonDocument.Parse(ResearchSnapshotTransformer.Project(state, cfg, request, now));
        var id = ResearchSnapshotJson.Identity(full.RootElement.GetProperty("identity"));
        Assert.Equal("Unavailable", full.RootElement.GetProperty("status").GetProperty("research").GetString());
        using var unchanged = JsonDocument.Parse(ResearchSnapshotTransformer.Project(state, cfg, request with { KnownIdentity = id }, now));
        Assert.Equal("unchanged", unchanged.RootElement.GetProperty("type").GetString());
        foreach (var name in ResearchSnapshotJson.Collections) Assert.False(unchanged.RootElement.TryGetProperty(name, out _));
        using var nextDay = JsonDocument.Parse(ResearchSnapshotTransformer.Project(state, cfg, request with { KnownIdentity = id }, now.AddDays(1)));
        Assert.Equal("full", nextDay.RootElement.GetProperty("type").GetString());
        using var rotated = JsonDocument.Parse(ResearchSnapshotTransformer.Project(state, new(g, new byte[32], Guid.NewGuid(), 2), request with { KnownIdentity = id }, now));
        Assert.Equal("full", rotated.RootElement.GetProperty("type").GetString());
        Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotTransformer.Project(state, new(Guid.NewGuid(), new byte[32], Guid.NewGuid(), 1), request, now));
    }
    [Fact] public void PriceIndicatorsAreTransformedAfterCalculationWhileRatiosVolumeAndRsiAreUnchanged()
    {
        var g = Guid.NewGuid(); var segment = Guid.NewGuid(); var now = new DateTimeOffset(2026, 10, 23, 12, 1, 15, TimeSpan.Zero);
        var candle = new CompletedResearchCandle(ResearchInstrument.BtcUsd, now.AddSeconds(-75), 100, 101, 99, 100, 3);
        var indicators = new ResearchIndicators("100.000000000000", "100.000000000000", "-7.000000000000", "100.000000000000", "101.000000000000",
            "55.000000000000", "-7.000000000000", "-7.000000000000", "-7.000000000000", "2.000000000000", "0.020000000000", "0.000000000000", "-0.010000000000");
        var row = new ResearchStoredCandle(candle, segment, 401, new(indicators, new(true, 0, "Neutral", "Undetermined"), null, null));
        var state = new ResearchCommittedState(g, ResearchInstrument.BtcUsd, 1, [row], now, now, ResearchIntegrity.Protected);
        var config = new ResearchTransformConfiguration(g, Enumerable.Range(0, 32).Select(i => (byte)i).ToArray(), Guid.Parse("11111111-1111-4111-8111-111111111111"), 1);
        using var doc = JsonDocument.Parse(ResearchSnapshotTransformer.Project(state, config, new("1.0", "BTC/USD", "1", null), now));
        var a = doc.RootElement.GetProperty("analytics")[0];
        foreach (var name in new[] { "bollingerLower", "macd", "macdSignal", "macdHistogram" }) Assert.Equal("-6.412170387931", a.GetProperty(name).GetString());
        Assert.Equal("1.832048682266", a.GetProperty("atr14").GetString()); Assert.Equal("55.000000000000", a.GetProperty("rsi14").GetString());
        Assert.Equal("-0.010000000000", a.GetProperty("emaSlope5").GetString()); Assert.Equal("0.020000000000", a.GetProperty("normalizedAtr14").GetString());
        Assert.Equal("3.000000000000", doc.RootElement.GetProperty("candles")[0].GetProperty("volume").GetString());
    }
    [Fact] public void ClientKnownFutureIdentityDoesNotSilentlyReceiveRolledBackData()
    {
        var g = Guid.NewGuid(); var now = new DateTimeOffset(2026, 10, 23, 12, 1, 15, TimeSpan.Zero);
        var state = new ResearchCommittedState(g, ResearchInstrument.BtcUsd, 0, [], null, null, ResearchIntegrity.Protected);
        var config = new ResearchTransformConfiguration(g, new byte[32], Guid.NewGuid(), 1);
        foreach (var identity in new[] { new ResearchSnapshotIdentity(g.ToString("D"), "1", "2026-10-23", "1"),
            new(g.ToString("D"), "0", "2026-10-24", "1"), new(g.ToString("D"), "0", "2026-10-23", "2") })
            Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotTransformer.Project(state, config, new("1.0", "BTC/USD", "1", identity), now));
    }
    [Fact] public void OutputBoundIsEnforcedDuringSerialization() => Assert.Throws<ResearchSnapshotException>(() => ResearchSnapshotJson.Serialize(new string('x', 4194305)));
}
