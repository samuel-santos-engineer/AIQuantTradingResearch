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
}
