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
    private sealed class DiagnosticHandler(string failure = "success", int failAt = 1, string? volumeJson = null) : HttpMessageHandler
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
            if(volumeJson is not null && (Calls==failAt || failAt==0))
            {
                if(volumeJson=="missing") rows[0]!.AsObject().Remove("volume");
                else if(volumeJson=="raw-nan") return Task.FromResult(Response(request,System.Net.HttpStatusCode.OK,
                    body.ToJsonString().Replace("\"volume\":\"1\"","\"volume\":NaN",StringComparison.Ordinal)));
                else rows[0]!["volume"]=System.Text.Json.Nodes.JsonNode.Parse(volumeJson);
            }
            if(kind=="json") return Task.FromResult(Response(request,System.Net.HttpStatusCode.OK,"{"+DiagnosticCanary));
            if(kind=="shape") return Task.FromResult(Response(request,System.Net.HttpStatusCode.OK,"[]"));
            if(kind=="provider-error") { body["status"]="error"; body["message"]=DiagnosticCanary; }
            if(kind=="status") body["status"]=42;
            if(kind=="missing-meta") body.AsObject().Remove("meta");
            if(kind=="invalid-meta") body["meta"]=42;
            if(kind=="missing-status") body.AsObject().Remove("status");
            if(kind=="null-meta") body["meta"]=null;
            if(kind=="missing-symbol") body["meta"]!.AsObject().Remove("symbol");
            if(kind=="invalid-symbol") body["meta"]!["symbol"]=42;
            if(kind=="null-symbol") body["meta"]!["symbol"]=null;
            if(kind=="missing-interval") body["meta"]!.AsObject().Remove("interval");
            if(kind=="invalid-interval") body["meta"]!["interval"]=42;
            if(kind=="null-interval") body["meta"]!["interval"]=null;
            if(kind=="missing-timezone") body["meta"]!.AsObject().Remove("exchange_timezone");
            if(kind=="invalid-timezone") body["meta"]!["exchange_timezone"]=42;
            if(kind=="null-timezone") body["meta"]!["exchange_timezone"]=null;
            if(kind=="interval") body["meta"]!["interval"]="1day";
            if(kind=="missing-values") body.AsObject().Remove("values");
            if(kind=="invalid-values") body["values"]=42;
            if(kind=="invalid-row") rows[0]=42;
            if(kind=="timestamp") rows[0]!["datetime"]=DiagnosticCanary;
            if(kind=="descending") { rows.Add(rows[0]!.DeepClone()); rows[0]!["datetime"]=DateTimeOffset.UtcNow.AddMinutes(-9).ToString("yyyy-MM-dd HH:mm:00",System.Globalization.CultureInfo.InvariantCulture); }
            if(kind=="forming") rows[0]!["datetime"]=DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:00",System.Globalization.CultureInfo.InvariantCulture);
            if(kind=="invalid-volume") rows[0]!["volume"]=DiagnosticCanary;
            if(kind=="oversize") return Task.FromResult(Response(request,System.Net.HttpStatusCode.OK,new string(' ',1_048_577)));
            if(kind=="invalid-utf8")
            {
                var malformed=Response(request,System.Net.HttpStatusCode.OK,"");
                malformed.Content=new ByteArrayContent([0xC3,0x28]); malformed.Content.Headers.ContentType=new("application/json");
                return Task.FromResult(malformed);
            }
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
    [InlineData("json","response_parse","INVALID_JSON_ENCODING")]
    [InlineData("metadata","response_parse","PROVIDER_SYMBOL_MISMATCH")]
    [InlineData("timezone","response_parse","PROVIDER_TIMEZONE_MISMATCH")]
    [InlineData("duplicate","observation_validation","INVALID_OBSERVATION")]
    [InlineData("unaligned","observation_validation","INVALID_OBSERVATION")]
    [InlineData("ohlc","observation_validation","INVALID_OBSERVATION")]
    [InlineData("numeric","observation_validation","INVALID_OHLC_ENCODING")]
    [InlineData("volume","observation_validation","INVALID_VOLUME")]
    [InlineData("empty","observation_validation","ZERO_ACCEPTED_CANDLES")]
    [InlineData("shape","response_parse","INVALID_PAYLOAD_SHAPE")]
    [InlineData("provider-error","response_parse","PROVIDER_ERROR_ENVELOPE")]
    [InlineData("status","response_parse","INVALID_PROVIDER_STATUS")]
    [InlineData("missing-meta","response_parse","INVALID_METADATA_OBJECT")]
    [InlineData("invalid-meta","response_parse","INVALID_METADATA_OBJECT")]
    [InlineData("missing-status","response_parse","INVALID_PROVIDER_STATUS")]
    [InlineData("null-meta","response_parse","INVALID_METADATA_OBJECT")]
    [InlineData("missing-symbol","response_parse","INVALID_METADATA_SYMBOL_FIELD")]
    [InlineData("invalid-symbol","response_parse","INVALID_METADATA_SYMBOL_FIELD")]
    [InlineData("null-symbol","response_parse","INVALID_METADATA_SYMBOL_FIELD")]
    [InlineData("missing-interval","response_parse","INVALID_METADATA_INTERVAL_FIELD")]
    [InlineData("invalid-interval","response_parse","INVALID_METADATA_INTERVAL_FIELD")]
    [InlineData("null-interval","response_parse","INVALID_METADATA_INTERVAL_FIELD")]
    [InlineData("invalid-timezone","response_parse","INVALID_METADATA_TIMEZONE_FIELD")]
    [InlineData("null-timezone","response_parse","INVALID_METADATA_TIMEZONE_FIELD")]
    [InlineData("interval","response_parse","PROVIDER_INTERVAL_MISMATCH")]
    [InlineData("missing-values","response_parse","INVALID_CANDLE_COLLECTION")]
    [InlineData("invalid-values","response_parse","INVALID_CANDLE_COLLECTION")]
    [InlineData("invalid-row","observation_validation","INVALID_CANDLE_COLLECTION")]
    [InlineData("timestamp","observation_validation","INVALID_CANDLE_TIMESTAMP")]
    [InlineData("descending","observation_validation","INVALID_OBSERVATION")]
    [InlineData("forming","observation_validation","ZERO_ACCEPTED_CANDLES")]
    [InlineData("invalid-volume","observation_validation","INVALID_VOLUME")]
    [InlineData("oversize","http_response","PAYLOAD_SIZE_LIMIT")]
    [InlineData("invalid-utf8","response_parse","INVALID_JSON_ENCODING")]
    [InlineData("headers","http_response","INVALID_RESPONSE_HEADERS")]
    public async Task InvalidFakeResponseIsClassifiedWithoutPayloadLeak(string kind,string stage,string failure)
    {
        var path=Output();using var handler=new DiagnosticHandler(kind);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(1,handler.Calls);
        DiagnosticEquals(result.Log,stage,failure,"BTC/USD",200,1,1,false);Assert.False(Directory.Exists(path));
        var root = new DirectoryInfo(AppContext.BaseDirectory);
        while (root is not null && !File.Exists(Path.Combine(root.FullName,"eng","docker","release-1.15","wp39_local_history.py"))) root=root.Parent;
        Assert.NotNull(root);
        var start = new System.Diagnostics.ProcessStartInfo("python") { RedirectStandardInput=true, RedirectStandardOutput=true, RedirectStandardError=true, UseShellExecute=false };
        start.ArgumentList.Add("-B"); start.ArgumentList.Add("-c");
        start.ArgumentList.Add("import importlib.util,json,sys; s=importlib.util.spec_from_file_location('w',sys.argv[1]); w=importlib.util.module_from_spec(s); s.loader.exec_module(w); raw=sys.stdin.buffer.read(2049); assert w.child_failure(raw).diagnostic==json.loads(raw)");
        start.ArgumentList.Add(Path.Combine(root!.FullName,"eng","docker","release-1.15","wp39_local_history.py"));
        using var child=System.Diagnostics.Process.Start(start)!;
        await child.StandardInput.WriteAsync(result.Log); child.StandardInput.Close();
        using var deadline=new CancellationTokenSource(TimeSpan.FromSeconds(10));
        try { await child.WaitForExitAsync(deadline.Token); }
        catch { if(!child.HasExited) child.Kill(); throw; }
        Assert.Equal(0,child.ExitCode);Assert.Equal("",await child.StandardOutput.ReadToEndAsync());
        Assert.Equal("",await child.StandardError.ReadToEndAsync());
    }
    [Theory]
    [InlineData("missing-meta","INVALID_METADATA_OBJECT")]
    [InlineData("missing-symbol","INVALID_METADATA_SYMBOL_FIELD")]
    [InlineData("missing-interval","INVALID_METADATA_INTERVAL_FIELD")]
    [InlineData("invalid-timezone","INVALID_METADATA_TIMEZONE_FIELD")]
    public async Task SecondSymbolMetadataFailuresArePreciseAndPublishNothing(string kind,string code)
    {
        var path=Output();using var handler=new DiagnosticHandler(kind,2);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(2,handler.Calls);
        DiagnosticEquals(result.Log,"response_parse",code,"ETH/USD",200,2,2,false);
        Assert.False(Directory.Exists(path));
    }
    [Fact] public async Task SecondSymbolParserFailureRetainsBothResponsesAndPublishesNothing()
    {
        var path=Output();using var handler=new DiagnosticHandler("provider-error",2);var result=await DiagnosticRun(handler,path);
        Assert.Equal(1,result.Code);Assert.Equal(2,handler.Calls);
        DiagnosticEquals(result.Log,"response_parse","PROVIDER_ERROR_ENVELOPE","ETH/USD",200,2,2,false);
        Assert.False(Directory.Exists(path));
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
    [Theory][InlineData(1)][InlineData(2)]
    public async Task OptionalExchangeTimezoneAllowsSyntheticSuccessWithoutInventingProvenance(int missingAt)
    {
        var path=Output();
        try
        {
            using var handler=new DiagnosticHandler("missing-timezone",missingAt);var result=await DiagnosticRun(handler,path);
            Assert.Equal(0,result.Code);Assert.Equal(2,handler.Calls);
            Assert.Equal("SyntheticFixture",ResearchLocalHistory.VerifyBundle(path,true));
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(path));
            foreach(var file in Directory.GetFiles(path))Assert.DoesNotContain(DiagnosticCanary,File.ReadAllText(file));
        }
        finally{if(Directory.Exists(path))Directory.Delete(path,true);}
    }
    [Theory]
    [InlineData("missing","INVALID_VOLUME")]
    [InlineData("null","INVALID_VOLUME")]
    [InlineData("1","INVALID_VOLUME")]
    [InlineData("1.25","INVALID_VOLUME")]
    [InlineData("-1","INVALID_VOLUME")]
    [InlineData("\"11111111111111111111111111111111111111111\"","INVALID_VOLUME")]
    [InlineData("true","INVALID_VOLUME")]
    [InlineData("[]","INVALID_VOLUME")]
    [InlineData("{}","INVALID_VOLUME")]
    [InlineData("\"\"","INVALID_VOLUME")]
    [InlineData("\" \"","INVALID_VOLUME")]
    [InlineData("\"bad-FAKE-WP39-SECRET-CANARY-DO-NOT-DISCLOSE\"","INVALID_VOLUME")]
    [InlineData("\"-1\"","INVALID_VOLUME")]
    [InlineData("\"1e2\"","INVALID_VOLUME")]
    [InlineData("\"+1\"","INVALID_VOLUME")]
    [InlineData("\"NaN\"","INVALID_VOLUME")]
    [InlineData("\"Infinity\"","INVALID_VOLUME")]
    [InlineData("\"0.1234567890123\"","INVALID_VOLUME")]
    [InlineData("\"79228162514264337593543950336\"","INVALID_VOLUME")]
    [InlineData("\"1000000000001\"","INVALID_OBSERVATION")]
    [InlineData("raw-nan","INVALID_JSON_ENCODING")]
    public async Task SyntheticVolumeRepresentationMatrixRejectsWithoutPartialPublication(string volumeJson,string code)
    {
        foreach(var failAt in new[]{1,2})
        {
            var path=Output();using var handler=new DiagnosticHandler(failAt:failAt,volumeJson:volumeJson);
            var result=await DiagnosticRun(handler,path);
            Assert.Equal(1,result.Code);Assert.Equal(failAt,handler.Calls);
            DiagnosticEquals(result.Log,code=="INVALID_JSON_ENCODING"?"response_parse":"observation_validation",
                code,failAt==1?"BTC/USD":"ETH/USD",200,failAt,failAt,false);
            Assert.False(Directory.Exists(path));
        }
    }
    [Theory][InlineData("\"0\"")][InlineData("\"0.123456789012\"")][InlineData("\"1000000000000\"")]
    public async Task SyntheticVolumeBoundariesRemainExactAndNeverAuthentic(string volumeJson)
    {
        var path=Output();
        try
        {
            using var handler=new DiagnosticHandler(failAt:0,volumeJson:volumeJson);var result=await DiagnosticRun(handler,path);
            Assert.Equal(0,result.Code);Assert.Equal(2,handler.Calls);
            using var observations=JsonDocument.Parse(File.ReadAllText(Path.Combine(path,"observations.json")));
            var expected=decimal.Parse(JsonSerializer.Deserialize<string>(volumeJson)!,System.Globalization.CultureInfo.InvariantCulture);
            foreach(var symbol in new[]{"BTC/USD","ETH/USD"})
                Assert.Equal(expected,decimal.Parse(observations.RootElement.GetProperty(symbol)[0].GetProperty("volume").GetString()!,System.Globalization.CultureInfo.InvariantCulture));
            Assert.Equal("SyntheticFixture",ResearchLocalHistory.VerifyBundle(path,true));
            Assert.Throws<ResearchUnavailableException>(()=>ResearchLocalHistory.VerifyBundle(path));
            foreach(var file in Directory.GetFiles(path))Assert.DoesNotContain(DiagnosticCanary,File.ReadAllText(file));
        }
        finally{if(Directory.Exists(path))Directory.Delete(path,true);}
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
