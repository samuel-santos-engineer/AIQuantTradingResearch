using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ResearchStateInitializationTests
{
    private static readonly Guid Generation = Guid.Parse("12345678-1234-4234-8234-123456789abc");
    private static string Config(Guid? generation = null, string? api = null, long epoch = 1) => JsonSerializer.Serialize(new
    {
        generation = generation ?? Generation, keyEpoch = "87654321-4321-4321-8321-abcdef123456", transformEpoch = epoch,
        hmacKeyBase64 = Convert.ToBase64String(Enumerable.Range(0, 32).Select(x => (byte)x).ToArray()),
        apiKey = api ?? "offline-independent-api-key-1234567890"
    });
    private static async Task<int> Run(string directory, string config, string? import = null)
    {
        using var input = new MemoryStream(Encoding.UTF8.GetBytes(config)); using var log = new StringWriter();
        var code = await ResearchStateInitialization.RunAsync(input, log, directory, import);
        Assert.DoesNotContain("1234567890", log.ToString()); Assert.DoesNotContain("hmacKey", log.ToString());
        Assert.DoesNotContain(directory, log.ToString()); return code;
    }
    [Fact] public async Task InitializesExplicitGenerationAndTupleAndExactRetryDoesNotChangeWitness()
    {
        using var dir = new ResearchTestDirectory(); Assert.Equal(0, await Run(dir.Path, Config()));
        var witness = File.ReadAllBytes(Path.Combine(dir.Path, "watermark.json"));
        Assert.Equal(0, await Run(dir.Path, Config())); Assert.Equal(witness, File.ReadAllBytes(Path.Combine(dir.Path, "watermark.json")));
        using var store = dir.Open(); var state = store.Read(ResearchInstrument.BtcUsd);
        Assert.Equal(Generation, state.Generation); Assert.Empty(state.Candles); Assert.False(state.ResearchReady);
        Assert.Equal(1, store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) => c.TransformEpoch));
    }
    [Theory]
    [InlineData("{}")]
    [InlineData("invalid")]
    [InlineData("{\"generation\":1,\"generation\":2}")]
    public async Task RejectsInvalidConfigurationWithoutCreatingDatabase(string config)
    {
        using var dir = new ResearchTestDirectory(); Assert.Equal(1, await Run(dir.Path, config));
        Assert.False(File.Exists(Path.Combine(dir.Path, "research.sqlite")));
    }
    [Fact] public async Task RejectsOversizeMalformedUtf8CancellationAndNonSeparateSecrets()
    {
        using var dir = new ResearchTestDirectory();
        Assert.Equal(1, await Run(dir.Path, new string('x', 4097)));
        Assert.Equal(1, await Run(dir.Path, Config(api: Convert.ToBase64String(Enumerable.Range(0,32).Select(x => (byte)x).ToArray()))));
        Assert.Equal(1, await Run(dir.Path, Config(epoch: 2)));
        using var invalid = new MemoryStream(new byte[] { 0xff });
        Assert.Equal(1, await ResearchStateInitialization.RunAsync(invalid, TextWriter.Null, dir.Path));
        using var cancel = new CancellationTokenSource(); cancel.Cancel(); using var input = new MemoryStream(Encoding.UTF8.GetBytes(Config()));
        Assert.Equal(1, await ResearchStateInitialization.RunAsync(input, TextWriter.Null, dir.Path, cancellationToken: cancel.Token));
        Assert.False(File.Exists(Path.Combine(dir.Path, "research.sqlite")));
    }
    [Fact] public async Task WrongGenerationSecretAndLiveWriterFailClosedWithoutReset()
    {
        using var dir = new ResearchTestDirectory(); Assert.Equal(0, await Run(dir.Path, Config()));
        var witness = File.ReadAllBytes(Path.Combine(dir.Path, "watermark.json"));
        Assert.Equal(1, await Run(dir.Path, Config(Guid.NewGuid())));
        Assert.Equal(1, await Run(dir.Path, Config().Replace("87654321", "97654321")));
        using (var store = dir.Open()) Assert.Equal(1, await Run(dir.Path, Config()));
        Assert.Equal(witness, File.ReadAllBytes(Path.Combine(dir.Path, "watermark.json")));
    }
    private static string Import(int count, bool gap = false) => JsonSerializer.Serialize(new Dictionary<string, object>
    {
        ["source"] = "twelvedata", ["observedAtUtc"] = "2026-10-23T12:00:00Z",
        ["BTC/USD"] = Rows(count, gap), ["ETH/USD"] = Rows(count, gap)
    });
    private static object[] Rows(int count, bool gap) => Enumerable.Range(0,count).Select(i => (object)new
    {
        openTimeUtc = new DateTimeOffset(2026,10,22,0,0,0,TimeSpan.Zero).AddMinutes(i + (gap && i >= 200 ? 1 : 0)).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", System.Globalization.CultureInfo.InvariantCulture),
        open = "100", high = "102", low = "99", close = "101", volume = "5"
    }).ToArray();
    [Theory]
    [InlineData(400, false, false)] [InlineData(401, false, true)] [InlineData(1000, false, true)] [InlineData(401, true, false)]
    public async Task ImportsBothInstrumentsThroughExistingCalculatedStoreAndHonestReadiness(int count, bool gap, bool ready)
    {
        using var dir = new ResearchTestDirectory(); var file = Path.Combine(Path.GetTempPath(), "aiq-import-"+Guid.NewGuid().ToString("N")+".json");
        try
        {
            await File.WriteAllTextAsync(file, Import(count,gap), new UTF8Encoding(false));
            if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(file, UnixFileMode.UserRead | UnixFileMode.UserWrite);
            Assert.Equal(0, await Run(dir.Path, Config(), file)); var witness = File.ReadAllBytes(Path.Combine(dir.Path, "watermark.json"));
            Assert.Equal(0, await Run(dir.Path, Config(), file)); Assert.Equal(witness, File.ReadAllBytes(Path.Combine(dir.Path,"watermark.json")));
            using var store = new SqliteResearchStateStore(dir.Path, true);
            foreach(var instrument in Enum.GetValues<ResearchInstrument>())
            { var state=store.Read(instrument);Assert.Equal(count,state.Candles.Count);Assert.Equal(ready,state.ResearchReady);Assert.Equal(ResearchIntegrity.Protected,state.Integrity); }
        }
        finally { File.Delete(file); }
    }
    [Theory]
    [InlineData("twelvedata", "other")]
    [InlineData("2026-10-22T00:00:00Z", "2099-10-22T00:00:00Z")]
    [InlineData("\"open\":\"100\"", "\"open\":\"1e2\"")]
    [InlineData("2026-10-22T00:01:00Z", "2026-10-22T00:00:00Z")]
    public async Task InvalidImportNeverCreatesState(string before, string after)
    {
        using var dir=new ResearchTestDirectory();var file=Path.Combine(Path.GetTempPath(),"aiq-import-"+Guid.NewGuid().ToString("N")+".json");
        try { await File.WriteAllTextAsync(file,Import(2).Replace(before,after),new UTF8Encoding(false));
            if(!OperatingSystem.IsWindows()) File.SetUnixFileMode(file,UnixFileMode.UserRead|UnixFileMode.UserWrite);
            Assert.Equal(1,await Run(dir.Path,Config(),file));Assert.False(File.Exists(Path.Combine(dir.Path,"research.sqlite"))); }
        finally { File.Delete(file); }
    }
    [Fact] public async Task ValidShapedDuplicateAndExtraSecretFieldsAreRejected()
    {
        using var dir = new ResearchTestDirectory();
        Assert.Equal(1, await Run(dir.Path, Config().Replace("{", "{\"apiKey\":\"duplicate-secret\",")));
        Assert.Equal(1, await Run(dir.Path, Config().Replace("{", "{\"extra\":1,")));
        Assert.Equal(1, await Run(dir.Path, Config(api: "short")));
        Assert.False(File.Exists(Path.Combine(dir.Path,"research.sqlite")));
    }
    [Theory] [InlineData(1001)] [InlineData(0)]
    public async Task RejectsOutOfBoundsImportBeforeCreatingState(int count)
    {
        using var dir = new ResearchTestDirectory(); var file = Path.Combine(Path.GetTempPath(),"aiq-import-"+Guid.NewGuid().ToString("N"));
        try { await File.WriteAllTextAsync(file,Import(count),new UTF8Encoding(false));
            if(!OperatingSystem.IsWindows()) File.SetUnixFileMode(file,UnixFileMode.UserRead|UnixFileMode.UserWrite);
            Assert.Equal(1,await Run(dir.Path,Config(),file));Assert.False(File.Exists(Path.Combine(dir.Path,"research.sqlite"))); }
        finally { File.Delete(file); }
    }
    [Fact] public async Task IncompatibleReimportDoesNotReplaceCommittedObservation()
    {
        using var dir = new ResearchTestDirectory(); var file=Path.Combine(Path.GetTempPath(),"aiq-import-"+Guid.NewGuid().ToString("N"));
        try { await File.WriteAllTextAsync(file,Import(401),new UTF8Encoding(false));
            if(!OperatingSystem.IsWindows()) File.SetUnixFileMode(file,UnixFileMode.UserRead|UnixFileMode.UserWrite);
            Assert.Equal(0,await Run(dir.Path,Config(),file)); var witness=File.ReadAllBytes(Path.Combine(dir.Path,"watermark.json"));
            await File.WriteAllTextAsync(file,Import(401).Replace("101","100"),new UTF8Encoding(false));
            Assert.Equal(1,await Run(dir.Path,Config(),file));Assert.Equal(witness,File.ReadAllBytes(Path.Combine(dir.Path,"watermark.json"))); }
        finally { File.Delete(file); }
    }
    [Fact] public async Task TamperedWitnessNeverReinitializesState()
    {
        using var dir=new ResearchTestDirectory();Assert.Equal(0,await Run(dir.Path,Config()));
        File.WriteAllText(Path.Combine(dir.Path,"watermark.json"),"{}",new UTF8Encoding(false));
        Assert.Equal(1,await Run(dir.Path,Config()));Assert.Equal("{}",File.ReadAllText(Path.Combine(dir.Path,"watermark.json")));
    }
}
