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
    [Theory][InlineData("missing")][InlineData("null")][InlineData("present")]
    public async Task ExplicitOhlcOnlyKeepsPricesAndNeverInventsVolume(string kind)
    {
        var json=System.Text.Json.Nodes.JsonNode.Parse(Valid)!;
        if(kind=="missing") json["values"]![0]!.AsObject().Remove("volume");
        if(kind=="null") json["values"]![0]!["volume"]=null;
        using var handler=new Handler(json.ToJsonString());
        using var source=new TwelveDataMinuteCandleSource(handler,Key,false,ohlcOnly:true);
        var batch=await source.ReadCompletedAsync(ResearchTestDirectory.Request());
        var candle=Assert.Single(batch.Candles);
        Assert.Null(candle.Volume);Assert.Null(candle.VolumeUnit);Assert.Equal(100,candle.Close);
        Assert.Equal(1,handler.Calls);
    }
    [Theory][InlineData("price")][InlineData("time")][InlineData("symbol")]
    public async Task OhlcOnlyStillRejectsMalformedPricesTimeAndSymbol(string mutation)
    {
        var invalid=mutation switch
        {
            "price"=>Valid.Replace("\"open\":\"100\"","\"open\":\"bad\""),
            "time"=>Valid.Replace("\"datetime\"","\"missingTime\""),
            _=>Valid.Replace("BTC/USD","ETH/USD")
        };
        using var handler=new Handler(invalid);
        using var source=new TwelveDataMinuteCandleSource(handler,Key,false,ohlcOnly:true);
        await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
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
    [Theory][InlineData(ResearchInstrument.BtcUsd)][InlineData(ResearchInstrument.EthUsd)]
    public async Task DocumentedOptionalExchangeTimezoneUsesExplicitUtcRequest(ResearchInstrument instrument)
    {
        var json=System.Text.Json.Nodes.JsonNode.Parse(Valid)!;
        json["meta"]!["symbol"]=instrument==ResearchInstrument.BtcUsd?"BTC/USD":"ETH/USD";
        json["meta"]!.AsObject().Remove("exchange_timezone");
        using var handler=new Handler(json.ToJsonString());using var source=new TwelveDataMinuteCandleSource(handler,Key,true);
        var batch=await source.ReadCompletedAsync(ResearchTestDirectory.Request(instrument));
        Assert.Single(batch.Candles);Assert.Equal(TimeSpan.Zero,batch.Candles[0].OpenTimeUtc.Offset);
        Assert.Equal(instrument,batch.Candles[0].Instrument);Assert.Contains("timezone=UTC",handler.Uri);
        Assert.Equal(1,handler.Calls);Assert.DoesNotContain(Key,handler.Uri);
    }
    [Theory][InlineData("symbol")][InlineData("interval")][InlineData("datetime")]
    [InlineData("ohlc")][InlineData("volume")][InlineData("status-error")][InlineData("status-missing")]
    public async Task MissingExchangeTimezoneDoesNotBypassCoreValidation(string mutation)
    {
        var json=System.Text.Json.Nodes.JsonNode.Parse(Valid)!;
        json["meta"]!.AsObject().Remove("exchange_timezone");
        if(mutation=="symbol") json["meta"]!.AsObject().Remove("symbol");
        if(mutation=="interval") json["meta"]!["interval"]="1day";
        if(mutation=="datetime") json["values"]![0]!["datetime"]="2026-10-01 00:00:00+02:00";
        if(mutation=="ohlc") json["values"]![0]!["low"]="102";
        if(mutation=="volume") json["values"]![0]!.AsObject().Remove("volume");
        if(mutation=="status-error") json["status"]="error";
        if(mutation=="status-missing") json.AsObject().Remove("status");
        using var handler=new Handler(json.ToJsonString());using var source=new TwelveDataMinuteCandleSource(handler,Key,true);
        var error=await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal(1,handler.Calls);Assert.DoesNotContain(Key,error.ToString());
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
    [Fact] public async Task StreamedOversizeHasFixedCodeAndObservedHttpFacts()
    {
        using var handler = new Handler("") { Bytes = new byte[TwelveDataMinuteCandleSource.MaximumResponseBytes + 1], NoLength = true };
        var trace = new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var source = new TwelveDataMinuteCandleSource(handler, Key, true, diagnostic:trace.Observe);
        var error=await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal("PAYLOAD_SIZE_LIMIT",trace.Code);Assert.Equal("http_response",trace.Stage);
        Assert.Equal(200,trace.HttpStatus);Assert.Equal(1,trace.RequestsStarted);Assert.Equal(1,trace.ResponsesReceived);
        Assert.DoesNotContain(Key,trace.FailureJson());Assert.DoesNotContain(Key,error.ToString());Assert.Equal(1,handler.Calls);
    }
    [Fact] public async Task EnforcesDeadlineOnOfflineHandler()
    {
        using var handler = new Handler(Valid) { Delay = true };using var source = new TwelveDataMinuteCandleSource(handler, Key, true, TimeSpan.FromMilliseconds(30));
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => source.ReadCompletedAsync(ResearchTestDirectory.Request()));Assert.Equal(1, handler.Calls);
    }
    [Theory] [InlineData("duplicate")] [InlineData("descending")] [InlineData("unaligned")] [InlineData("ohlc")]
    public async Task InvalidRowSequencesNeverBecomeObservations(string mutation)
    {
        var root=System.Text.Json.Nodes.JsonNode.Parse(Valid)!;
        var rows=root["values"]!.AsArray();
        if(mutation=="duplicate") rows.Add(rows[0]!.DeepClone());
        if(mutation=="descending") { rows.Add(rows[0]!.DeepClone());rows[0]!["datetime"]="2026-10-01 00:01:00"; }
        if(mutation=="unaligned") rows[0]!["datetime"]="2026-10-01 00:00:01";
        if(mutation=="ohlc") rows[0]!["low"]="102";
        using var handler=new Handler(root.ToJsonString());using var source=new TwelveDataMinuteCandleSource(handler,Key,true);
        await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal(1,handler.Calls);
    }

    [Fact] public async Task DiagnosticCountsOnlySendBoundaryAndRetainsCallerCancellation()
    {
        using var cancelled=new CancellationTokenSource();
        using var handler=new CancelAtSendHandler(cancelled);
        var trace=new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var source=new TwelveDataMinuteCandleSource(handler,Key,true,diagnostic:trace.Observe);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request(),cancelled.Token));
        Assert.Equal(1,handler.Calls);Assert.Equal(1,trace.RequestsStarted);Assert.Equal(0,trace.ResponsesReceived);
        Assert.Null(trace.HttpStatus);Assert.Equal("request_start",trace.Stage);Assert.Equal("CANCELLED",trace.Code);
        Assert.DoesNotContain(Key,trace.FailureJson());
    }
    [Fact] public async Task DiagnosticUnknownVolumeAndPrecancelAreBeforeSend()
    {
        using var handler=new Handler(Valid);var trace=new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var source=new TwelveDataMinuteCandleSource(handler,Key,false,diagnostic:trace.Observe);
        await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal(0,handler.Calls);Assert.Equal(0,trace.RequestsStarted);Assert.Equal("VOLUME_UNCONFIRMED",trace.Code);
        var other=new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var known=new TwelveDataMinuteCandleSource(handler,Key,true,diagnostic:other.Observe);
        await Assert.ThrowsAnyAsync<OperationCanceledException>(()=>known.ReadCompletedAsync(ResearchTestDirectory.Request(),new CancellationToken(true)));
        Assert.Equal(0,other.RequestsStarted);Assert.Equal(0,other.ResponsesReceived);Assert.Equal("CANCELLED",other.Code);
    }
    [Fact] public async Task DiagnosticDeadlineHasStartedSendButNoInventedResponse()
    {
        using var handler=new Handler(Valid){Delay=true};var trace=new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var source=new TwelveDataMinuteCandleSource(handler,Key,true,TimeSpan.FromMilliseconds(30),trace.Observe);
        await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal(1,handler.Calls);Assert.Equal(1,trace.RequestsStarted);Assert.Equal(0,trace.ResponsesReceived);
        Assert.Equal("TIMEOUT",trace.Code);Assert.Null(trace.HttpStatus);
    }
    [Fact] public async Task DiagnosticBodyReadFailureRetainsActuallyReceivedStatus()
    {
        using var handler=new BodyFailureHandler();var trace=new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var source=new TwelveDataMinuteCandleSource(handler,Key,true,diagnostic:trace.Observe);
        await Assert.ThrowsAsync<ResearchUnavailableException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request()));
        Assert.Equal(1,trace.RequestsStarted);Assert.Equal(1,trace.ResponsesReceived);Assert.Equal(200,trace.HttpStatus);
        Assert.Equal("http_response",trace.Stage);Assert.Equal("TRANSPORT_ERROR",trace.Code);Assert.DoesNotContain(Key,trace.FailureJson());
    }
    [Fact] public async Task InvalidPollRetainsExistingExceptionContractAndZeroSendCount()
    {
        using var handler=new Handler(Valid);var trace=new AIQuantTradingResearch.Worker.ResearchLocalHistory.AcquisitionTrace();
        using var source=new TwelveDataMinuteCandleSource(handler,Key,true,diagnostic:trace.Observe);
        await Assert.ThrowsAsync<ArgumentException>(()=>source.ReadCompletedAsync(ResearchTestDirectory.Request() with { CompletionGrace=TimeSpan.FromSeconds(-1) }));
        Assert.Equal(0,handler.Calls);Assert.Equal(0,trace.RequestsStarted);Assert.Equal("preflight",trace.Stage);Assert.Equal("INVALID_PRIVATE_INPUT",trace.Code);
    }

    private sealed class CancelAtSendHandler(CancellationTokenSource cancellation):HttpMessageHandler
    {
        public int Calls;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)
        { Calls++;cancellation.Cancel();return Task.FromCanceled<HttpResponseMessage>(cancellation.Token); }
    }
    private sealed class BodyFailureHandler:HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken token)=>
            Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK){RequestMessage=request,Content=new StreamContent(new BodyFailureStream()){Headers={ContentType=new("application/json")}}});
    }
    private sealed class BodyFailureStream:Stream
    {
        public override bool CanRead=>true;public override bool CanSeek=>false;public override bool CanWrite=>false;
        public override long Length=>throw new NotSupportedException();public override long Position{get=>throw new NotSupportedException();set=>throw new NotSupportedException();}
        public override void Flush()=>throw new NotSupportedException();public override long Seek(long offset,SeekOrigin origin)=>throw new NotSupportedException();
        public override void SetLength(long value)=>throw new NotSupportedException();public override void Write(byte[] buffer,int offset,int count)=>throw new NotSupportedException();
        public override int Read(byte[] buffer,int offset,int count)=>throw new IOException(Key);
        public override ValueTask<int> ReadAsync(Memory<byte> buffer,CancellationToken token=default)=>ValueTask.FromException<int>(new IOException(Key));
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
