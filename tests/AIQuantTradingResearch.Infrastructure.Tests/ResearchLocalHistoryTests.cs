using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ResearchLocalHistoryTests
{
    private sealed class Source(bool failSecond = false) : IResearchCandleSource
    {
        public int Calls;
        public Task<ResearchPollBatch> ReadCompletedAsync(ResearchPollRequest request, CancellationToken cancellationToken = default)
        {
            Calls++;
            if (failSecond && Calls == 2) throw new ResearchUnavailableException();
            return Task.FromResult(new ResearchPollBatch(Enumerable.Range(0, 3).Select(i =>
                new CompletedResearchCandle(request.Instrument, request.ObservedAtUtc.AddTicks(-(request.ObservedAtUtc.Ticks % TimeSpan.TicksPerMinute)).AddMinutes(-10 + (i==2?3:i)),100,102,99,101,1))));
        }
    }
    private static string Output() => Path.Combine(Path.GetTempPath(), "aiq-wp39-test-" + Guid.NewGuid().ToString("N"));
    [Fact] public async Task BothSymbolsBoundedGapAwareAndFixtureCannotBecomeAuthentic()
    {
        var path=Output();var source=new Source();
        try
        {
            await ResearchLocalHistory.AcquireAsync(source,path,DateTimeOffset.UtcNow,true);
            Assert.Equal(2,source.Calls);
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(path));
            Assert.Equal("SyntheticFixture",ResearchLocalHistory.VerifyBundle(path,true));
            using var doc=JsonDocument.Parse(File.ReadAllBytes(Path.Combine(path,"provenance.json")));
            foreach(var symbol in new[]{"BTC/USD","ETH/USD"})
            { Assert.Equal(3,doc.RootElement.GetProperty("symbols").GetProperty(symbol).GetProperty("rows").GetInt32());
              Assert.Equal(1,doc.RootElement.GetProperty("symbols").GetProperty(symbol).GetProperty("gaps").GetInt32()); }
        }
        finally {if(Directory.Exists(path))Directory.Delete(path,true);}
    }
    [Fact] public async Task PartialFailurePublishesNothingAndDoesNotRetry()
    {
        var path=Output();var source=new Source(true);
        await Assert.ThrowsAsync<ResearchUnavailableException>(()=>ResearchLocalHistory.AcquireAsync(source,path,DateTimeOffset.UtcNow,true));
        Assert.Equal(2,source.Calls);Assert.False(Directory.Exists(path));
    }
    [Fact] public async Task TamperingRejectedAndDestinationNeverOverwritten()
    {
        var path=Output();var source=new Source();
        try
        {
            await ResearchLocalHistory.AcquireAsync(source,path,DateTimeOffset.UtcNow,true);
            var original=File.ReadAllBytes(Path.Combine(path,"observations.json"));
            await Assert.ThrowsAsync<ResearchUnavailableException>(()=>ResearchLocalHistory.AcquireAsync(source,path,DateTimeOffset.UtcNow,true));
            Assert.Equal(2,source.Calls);Assert.Equal(original,File.ReadAllBytes(Path.Combine(path,"observations.json")));
            File.AppendAllText(Path.Combine(path,"observations.json")," ");
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(path,true));
        }
        finally {Directory.Delete(path,true);}
    }
    [Fact] public async Task FixtureBundleUsesExistingInitializerAndExactRetryWithoutAuthenticClaim()
    {
        var path=Output();
        try
        {
            await ResearchLocalHistory.AcquireAsync(new Source(),Path.Combine(path,"bundle"),DateTimeOffset.UtcNow,true);
            ResearchLocalHistory.Prepare(Path.Combine(path,"runtime"));
            var config=File.ReadAllBytes(Path.Combine(path,"runtime","initialize.json"));
            var imported=Path.Combine(path,"bundle","observations.json"); var state=Path.Combine(path,"private");
            for(var attempt=0;attempt<2;attempt++)
            {
                using var input=new MemoryStream(config);using var log=new StringWriter();
                Assert.Equal(0,await ResearchStateInitialization.RunAsync(input,log,state,imported));
            }
            using var store=new AIQuantTradingResearch.Infrastructure.Persistence.Sqlite.SqliteResearchStateStore(state,true);
            foreach(var instrument in Enum.GetValues<ResearchInstrument>())
            { Assert.Equal(3,store.Read(instrument).Candles.Count);Assert.False(store.Read(instrument).ResearchReady); }
            Assert.Equal("SyntheticFixture",ResearchLocalHistory.VerifyBundle(Path.Combine(path,"bundle"),true));
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(Path.Combine(path,"bundle")));
        }
        finally {if(Directory.Exists(path))Directory.Delete(path,true);}
    }

    [Fact] public async Task ProvenanceSummaryTamperingIsRejected()
    {
        var path=Output();
        try
        {
            await ResearchLocalHistory.AcquireAsync(new Source(),path,DateTimeOffset.UtcNow,true);
            var file=Path.Combine(path,"provenance.json");
            var evidence=System.Text.Json.Nodes.JsonNode.Parse(File.ReadAllText(file))!;
            evidence["symbols"]!["BTC/USD"]!["rows"]=99;
            File.WriteAllText(file,evidence.ToJsonString());
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(path,true));
        }
        finally {Directory.Delete(path,true);}
    }

    [Fact] public async Task CancellationDoesNotReadSourceOrPublishBundle()
    {
        var path=Output();var source=new Source();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(()=>ResearchLocalHistory.AcquireAsync(source,path,DateTimeOffset.UtcNow,true,new CancellationToken(true)));
        Assert.Equal(0,source.Calls);Assert.False(Directory.Exists(path));
    }

    [Fact] public async Task NoApprovalNeverReadsSecretOrCallsNetwork()
    {
        using var input=new MemoryStream(Encoding.UTF8.GetBytes("not-a-secret"));using var log=new StringWriter();
        Assert.Equal(1,await ResearchLocalHistory.RunAsync(input,log,Output()));Assert.Equal(0,input.Position);
        Assert.DoesNotContain("not-a-secret",log.ToString());
    }
    private const string DiagnosticCanary = "FAKE-WP39-SECRET-CANARY-DO-NOT-DISCLOSE";
    private sealed class DiagnosticHandler(string failure = "success", int failAt = 1) : HttpMessageHandler
    {
        public int Calls;
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            Calls++;
            var kind = Calls == failAt ? failure : "success";
            if (kind == "transport") throw new HttpRequestException(DiagnosticCanary);
            if (kind == "timeout") throw new OperationCanceledException(DiagnosticCanary);
            var symbol = request.RequestUri!.Query.Contains("ETH",StringComparison.Ordinal) ? "ETH/USD" : "BTC/USD";
            var time = DateTimeOffset.UtcNow.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:00",System.Globalization.CultureInfo.InvariantCulture);
            var body = System.Text.Json.Nodes.JsonNode.Parse(JsonSerializer.Serialize(new {
                status="ok",meta=new{symbol,interval="1min",exchange_timezone="UTC"},
                values=new[]{new{datetime=time,open="100",high="102",low="99",close="101",volume="1"}},
                ignored=DiagnosticCanary }))!;
            var rows=body["values"]!.AsArray();
            if(kind=="json") return Task.FromResult(Response(request,System.Net.HttpStatusCode.OK,"{"+DiagnosticCanary));
            if(kind=="metadata") body["meta"]!["symbol"]="OTHER";
            if(kind=="timezone") body["meta"]!["exchange_timezone"]="Not-UTC";
            if(kind=="duplicate") rows.Add(rows[0]!.DeepClone());
            if(kind=="unaligned") rows[0]!["datetime"]=time[..^2]+"01";
            if(kind=="ohlc") rows[0]!["low"]="103";
            if(kind=="numeric") rows[0]!["close"]="not-a-decimal-"+DiagnosticCanary;
            if(kind=="volume") rows[0]!.AsObject().Remove("volume");
            if(kind=="empty") rows.Clear();
            var status=int.TryParse(kind,out var number)?(System.Net.HttpStatusCode)number:System.Net.HttpStatusCode.OK;
            var response=Response(request,status,body.ToJsonString());
            if(kind=="headers")response.Content.Headers.ContentType=new("text/plain");
            return Task.FromResult(response);
        }
        private static HttpResponseMessage Response(HttpRequestMessage request,System.Net.HttpStatusCode status,string body) => new(status)
        {RequestMessage=request,Content=new StringContent(body,Encoding.UTF8,"application/json")};
    }
    private static async Task<(int Code,string Log)> DiagnosticRun(DiagnosticHandler handler,string path,string? config=null,bool approved=true)
    {
        var prior=Environment.GetEnvironmentVariable("AIQ_WP39_PROVIDER_APPROVED");
        try
        {
            Environment.SetEnvironmentVariable("AIQ_WP39_PROVIDER_APPROVED",approved?"1":null);
            using var input=new MemoryStream(Encoding.UTF8.GetBytes(config??JsonSerializer.Serialize(new{providerKey=DiagnosticCanary,baseAssetVolumeConfirmed=true})));
            using var log=new StringWriter();
            var code=await ResearchLocalHistory.RunAsync(input,log,path,()=>handler);
            Assert.DoesNotContain(DiagnosticCanary,log.ToString());Assert.DoesNotContain(path,log.ToString());
            Assert.DoesNotContain("Authorization",log.ToString());Assert.DoesNotContain("api.twelvedata",log.ToString());
            return(code,log.ToString());
        }
        finally{Environment.SetEnvironmentVariable("AIQ_WP39_PROVIDER_APPROVED",prior);}
    }
    private static void DiagnosticEquals(string log,string stage,string code,string? symbol,int? status,int started,int received,bool unknown)
    {
        Assert.True(Encoding.UTF8.GetByteCount(log)<2048);
        using var doc=JsonDocument.Parse(log);var d=doc.RootElement;
        Assert.Equal(10,d.EnumerateObject().Count());Assert.Equal(1,d.GetProperty("version").GetInt32());
        Assert.Equal("WP39_ACQUIRE_FAILED",d.GetProperty("event").GetString());
        Assert.Equal(stage,d.GetProperty("failedStage").GetString());Assert.Equal(code,d.GetProperty("failureCode").GetString());
        Assert.Equal(symbol,d.GetProperty("instrument").GetString());
        if(status is null)Assert.Equal(JsonValueKind.Null,d.GetProperty("httpStatus").ValueKind);
        else Assert.Equal(status,d.GetProperty("httpStatus").GetInt32());
        Assert.Equal(started,d.GetProperty("requestsStarted").GetInt32());Assert.Equal(received,d.GetProperty("responsesReceived").GetInt32());
        Assert.Equal(unknown,d.GetProperty("outcomeUnknown").GetBoolean());Assert.False(d.GetProperty("bundlePublished").GetBoolean());
    }
    [Theory]
    [InlineData(401,"HTTP_UNAUTHORIZED")][InlineData(403,"HTTP_FORBIDDEN")]
    [InlineData(429,"HTTP_RATE_LIMITED")][InlineData(500,"HTTP_SERVER_ERROR")][InlineData(301,"HTTP_OTHER")]
    public async Task FakeHttpFactsAreExactWithoutAccountInference(int status,string failure)
    {
        var path=Output();using var handler=new DiagnosticHandler(status.ToString(System.Globalization.CultureInfo.InvariantCulture));
        var result=await DiagnosticRun(handler,path);Assert.Equal(1,result.Code);Assert.Equal(1,handler.Calls);
        DiagnosticEquals(result.Log,"http_response",failure,"BTC/USD",status,1,1,false);Assert.False(Directory.Exists(path));
    }
    [Theory][InlineData("transport","TRANSPORT_ERROR")][InlineData("timeout","TIMEOUT")]
    public async Task SendWithoutResponseHasUnknownOutcome(string kind,string failure)
    {
        var path=Output();using var handler=new DiagnosticHandler(kind);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(1,handler.Calls);
        DiagnosticEquals(result.Log,"request_start",failure,"BTC/USD",null,1,0,true);Assert.False(Directory.Exists(path));
    }
    [Theory]
    [InlineData("json","response_parse","INVALID_PROVIDER_PAYLOAD")]
    [InlineData("metadata","response_parse","INVALID_PROVIDER_METADATA")]
    [InlineData("timezone","response_parse","INVALID_PROVIDER_METADATA")]
    [InlineData("duplicate","observation_validation","INVALID_OBSERVATION")]
    [InlineData("unaligned","observation_validation","INVALID_OBSERVATION")]
    [InlineData("ohlc","observation_validation","INVALID_OBSERVATION")]
    [InlineData("numeric","observation_validation","INVALID_OBSERVATION")]
    [InlineData("volume","observation_validation","INVALID_VOLUME")]
    [InlineData("empty","observation_validation","ZERO_ACCEPTED_CANDLES")]
    [InlineData("headers","http_response","INVALID_RESPONSE_HEADERS")]
    public async Task InvalidFakeResponseIsClassifiedWithoutPayloadLeak(string kind,string stage,string failure)
    {
        var path=Output();using var handler=new DiagnosticHandler(kind);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(1,handler.Calls);
        DiagnosticEquals(result.Log,stage,failure,"BTC/USD",200,1,1,false);Assert.False(Directory.Exists(path));
    }
    [Fact] public async Task SecondSymbolHttpFailureRetainsBothCountersAndPublishesNothing()
    {
        var path=Output();using var handler=new DiagnosticHandler("429",2);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(2,handler.Calls);
        DiagnosticEquals(result.Log,"http_response","HTTP_RATE_LIMITED","ETH/USD",429,2,2,false);Assert.False(Directory.Exists(path));
    }
    [Fact] public async Task SecondSymbolTransportFailureDoesNotReuseFirstStatus()
    {
        var path=Output();using var handler=new DiagnosticHandler("transport",2);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(2,handler.Calls);
        DiagnosticEquals(result.Log,"request_start","TRANSPORT_ERROR","ETH/USD",null,2,1,true);Assert.False(Directory.Exists(path));
    }
    [Theory][InlineData("{")][InlineData("{}")][InlineData("null")]
    public async Task MalformedPrivateInputHasProvenZeroRequests(string config)
    {
        var path=Output();using var handler=new DiagnosticHandler();var result=await DiagnosticRun(handler,path,config);
        Assert.Equal(1,result.Code);Assert.Equal(0,handler.Calls);
        DiagnosticEquals(result.Log,"private_input","INVALID_PRIVATE_INPUT",null,null,0,0,false);Assert.False(Directory.Exists(path));
    }
    [Fact] public async Task OversizedPrivateInputIsBoundedBeforeHandlerOrState()
    {
        var path=Output();using var handler=new DiagnosticHandler();
        var result=await DiagnosticRun(handler,path,new string('x',4097)+DiagnosticCanary);
        Assert.Equal(1,result.Code);Assert.Equal(0,handler.Calls);
        DiagnosticEquals(result.Log,"private_input","INVALID_PRIVATE_INPUT",null,null,0,0,false);Assert.False(Directory.Exists(path));
    }
    [Fact] public async Task UnconfirmedVolumeHasZeroRequests()
    {
        var path=Output();using var handler=new DiagnosticHandler();
        var result=await DiagnosticRun(handler,path,JsonSerializer.Serialize(new{providerKey=DiagnosticCanary,baseAssetVolumeConfirmed=false}));
        Assert.Equal(1,result.Code);Assert.Equal(0,handler.Calls);
        DiagnosticEquals(result.Log,"private_input","VOLUME_UNCONFIRMED",null,null,0,0,false);
    }
    [Fact] public async Task MissingApprovalDoesNotReadPrivateInputOrCreateHandler()
    {
        var path=Output();using var handler=new DiagnosticHandler();var result=await DiagnosticRun(handler,path,approved:false);
        Assert.Equal(1,result.Code);Assert.Equal(0,handler.Calls);
        DiagnosticEquals(result.Log,"preflight","MISSING_APPROVAL",null,null,0,0,false);
    }
    [Fact] public async Task ExistingDestinationFailsBeforeOutboundBoundaryAndPreservesSentinel()
    {
        var path=Output();Directory.CreateDirectory(path);File.WriteAllText(Path.Combine(path,"sentinel"),"preserve");
        try
        {
            using var handler=new DiagnosticHandler();var result=await DiagnosticRun(handler,path);
            Assert.Equal(1,result.Code);Assert.Equal(0,handler.Calls);
            DiagnosticEquals(result.Log,"preflight","DESTINATION_EXISTS",null,null,0,0,false);
            Assert.Equal("preserve",File.ReadAllText(Path.Combine(path,"sentinel")));
        }
        finally{Directory.Delete(path,true);}
    }
    [Fact] public async Task PublicationFailureDoesNotCreateSuccessArtifacts()
    {
        var root=Output();Directory.CreateDirectory(root);var file=Path.Combine(root,"parent-file");File.WriteAllText(file,"preserve");
        try
        {
            using var handler=new DiagnosticHandler();var result=await DiagnosticRun(handler,Path.Combine(file,"bundle"));
            Assert.Equal(1,result.Code);Assert.Equal(2,handler.Calls);
            DiagnosticEquals(result.Log,"bundle_publication","BUNDLE_PUBLICATION_FAILED","ETH/USD",200,2,2,false);
            Assert.Equal(new[]{file},Directory.GetFileSystemEntries(root));Assert.Equal("preserve",File.ReadAllText(file));
        }
        finally{Directory.Delete(root,true);}
    }
    [Fact] public async Task SuccessfulFakeRunHasOnlySyntheticProvenanceAndExactlyTwoRequests()
    {
        var path=Output();
        try
        {
            using var handler=new DiagnosticHandler();var result=await DiagnosticRun(handler,path);
            Assert.Equal(0,result.Code);Assert.Equal(2,handler.Calls);Assert.DoesNotContain("WP39_ACQUIRE_FAILED",result.Log);
            Assert.Equal("SyntheticFixture",ResearchLocalHistory.VerifyBundle(path,true));
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(path));
            foreach(var file in Directory.GetFiles(path))Assert.DoesNotContain(DiagnosticCanary,File.ReadAllText(file));
            Assert.False(File.Exists(Path.Combine(path,"research.sqlite")));Assert.False(File.Exists(Path.Combine(path,"qualification.json")));
        }
        finally{if(Directory.Exists(path))Directory.Delete(path,true);}
    }

}
