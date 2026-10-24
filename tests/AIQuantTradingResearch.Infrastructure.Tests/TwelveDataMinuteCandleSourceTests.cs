using System.Net;
using System.Text;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class TwelveDataMinuteCandleSourceTests
{
    private const string Key = "offline-only-private-canary";
    private const string Valid = """
        {"status":"ok","meta":{"symbol":"BTC/USD","interval":"1min","exchange_timezone":"UTC"},"values":[
        {"datetime":"2026-10-01 00:00:00","open":"100","high":"101","low":"99","close":"100","volume":"1"}]}
        """;
    [Fact] public async Task OfflineMinuteAdapterAndIngestionCommitBothInstrumentsToRealSqlite()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();
        using var handler = new Handler(Valid) { MatchInstrument = true };using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        using var ingestion = new ResearchIngestionService(source, store, store, 2);
        var btc = await ingestion.PollNextAsync(ResearchTestDirectory.Start.AddDays(2), TimeSpan.Zero);
        var eth = await ingestion.PollNextAsync(ResearchTestDirectory.Start.AddDays(2), TimeSpan.Zero);
        Assert.Equal(ResearchInstrument.BtcUsd, btc.Instrument);Assert.Equal(ResearchInstrument.EthUsd, eth.Instrument);
        Assert.Single(store.Read(ResearchInstrument.BtcUsd).Candles);Assert.Single(store.Read(ResearchInstrument.EthUsd).Candles);
        Assert.Equal(btc.Generation, eth.Generation);Assert.Equal(2, handler.Calls);Assert.False(btc.ResearchReady);Assert.False(eth.ResearchReady);
    }
    [Theory] [InlineData(ResearchInstrument.BtcUsd)] [InlineData(ResearchInstrument.EthUsd)]
    public async Task ReadsActualMinuteOhlcvUsingOnlyInjectedOfflineHandler(ResearchInstrument instrument)
    {
        using var handler = new Handler(Valid.Replace("BTC/USD", instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD"));
        using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        var batch = await source.ReadCompletedAsync(ResearchTestDirectory.Request(instrument));
        Assert.Single(batch.Candles);Assert.Equal(instrument, batch.Candles[0].Instrument);Assert.Equal(1, batch.Candles[0].Volume);
        Assert.Equal(1, handler.Calls);Assert.Contains("interval=1min", handler.Uri);Assert.DoesNotContain(Key, handler.Uri);
        Assert.Equal("apikey " + Key, handler.Authorization);Assert.Equal("twelvedata", batch.Candles[0].Provider);
    }
    [Theory] [InlineData("symbol")] [InlineData("interval")] [InlineData("timezone")] [InlineData("volume")] [InlineData("numeric")]
    [InlineData("precision")] [InlineData("duplicate-key")] [InlineData("future")] [InlineData("status")] [InlineData("truncated")]
    public async Task MalformedOrUnverifiedResponsesFailGenerically(string mutation)
    {
        var json = mutation switch
        {
            "symbol" => Valid.Replace("BTC/USD", "SOL/USD"), "interval" => Valid.Replace("1min", "1day"),
            "timezone" => Valid.Replace("UTC", "America/New_York"), "volume" => Valid.Replace("\"volume\":\"1\"", "\"volume\":null"),
            "numeric" => Valid.Replace("\"open\":\"100\"", "\"open\":100"),
            "precision" => Valid.Replace("\"close\":\"100\"", "\"close\":\"100.00000000000000000000000000000000000001\""),
            "duplicate-key" => Valid.Replace("\"status\":\"ok\"", "\"status\":\"ok\",\"status\":\"ok\""),
            "future" => Valid.Replace("2026-10-01", "2026-10-09"), "status" => Valid.Replace("\"ok\"", "\"error\""), _ => "{" + Key
        };
        using var handler = new Handler(json);using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        var error = await Assert.ThrowsAsync<ResearchUnavailableException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal("Research unavailable.", error.Message);Assert.Null(error.InnerException);Assert.Equal(1, handler.Calls);
    }
    [Theory] [InlineData(301)] [InlineData(401)] [InlineData(429)] [InlineData(500)]
    public async Task HttpFailuresDoNotRetryOrReturnFakeCandles(int status)
    {
        using var handler = new Handler(Valid) { Status = (HttpStatusCode)status };using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request()));Assert.Equal(1, handler.Calls);
    }
    [Fact] public async Task FormingMinuteAndGraceAreExcludedWithoutFabricatingCandles()
    {
        using var handler = new Handler(Valid);using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        var request = new ResearchPollRequest(ResearchInstrument.BtcUsd, ResearchTestDirectory.Start, TimeSpan.Zero);
        Assert.Empty((await source.ReadCompletedAsync(request)).Candles);
        Assert.Empty((await source.ReadCompletedAsync(request with { ObservedAtUtc = request.ObservedAtUtc.AddMinutes(1), CompletionGrace = TimeSpan.FromSeconds(1) })).Candles);
        Assert.Single((await source.ReadCompletedAsync(request with { ObservedAtUtc = request.ObservedAtUtc.AddMinutes(1) })).Candles);
    }
    [Fact] public async Task UnknownVolumeCapabilityAndCancellationMakeNoRequest()
    {
        using var handler = new Handler(Valid);using var unknown = new TwelveDataMinuteCandleSource(handler, Key, false);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => unknown.ReadCompletedAsync(ResearchTestDirectory.Request()));
        using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request(), new CancellationToken(true)));
        Assert.Equal(0, handler.Calls);
    }
    [Fact] public async Task EnforcesActualBytesWithoutContentLengthAndRejectsInvalidUtf8()
    {
        using var handler = new Handler("") { Bytes = new byte[TwelveDataMinuteCandleSource.MaximumResponseBytes + 1], NoLength = true };
        using var source = new TwelveDataMinuteCandleSource(handler, Key, true);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        handler.Bytes = [0xC3, 0x28];await Assert.ThrowsAsync<ResearchUnavailableException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request()));
    }
    [Fact] public async Task EnforcesDeadlineOnOfflineHandler()
    {
        using var handler = new Handler(Valid) { Delay = true };using var source = new TwelveDataMinuteCandleSource(handler, Key, true, TimeSpan.FromMilliseconds(30));
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request()));Assert.Equal(1, handler.Calls);
    }
    private sealed class Handler(string json) : HttpMessageHandler
    {
        internal int Calls;internal string Uri = "";internal string Authorization = "";internal HttpStatusCode Status = HttpStatusCode.OK;
        internal byte[]? Bytes;internal bool NoLength;internal bool Delay;internal bool MatchInstrument;
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Calls++;Uri = request.RequestUri!.ToString();Authorization = request.Headers.Authorization!.ToString();
            if (Delay) await Task.Delay(Timeout.Infinite, cancellationToken);
            var body = MatchInstrument && Uri.Contains("ETH", StringComparison.Ordinal) ? json.Replace("BTC/USD", "ETH/USD") : json;
            var content = new ByteArrayContent(Bytes ?? Encoding.UTF8.GetBytes(body));content.Headers.ContentType = new("application/json");
            if (NoLength) return new HttpResponseMessage(Status) { Content = new StreamContent(new NoLengthStream(Bytes!)) { Headers = { ContentType = new("application/json") } }, RequestMessage = request };
            return new HttpResponseMessage(Status) { Content = content, RequestMessage = request };
        }
    }
    private sealed class NoLengthStream(byte[] bytes) : MemoryStream(bytes)
    {
        public override bool CanSeek => false;
    }
}
