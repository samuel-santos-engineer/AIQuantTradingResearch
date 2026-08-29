using System.Text.Json;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Infrastructure.Visualization;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class VisualizationReadModelFilePublisherTests
{
    [Fact]
    public void StartupCleansOnlyCanonicalAndOwnedTemporaryFiles()
    {
        using var fixture = new TemporaryHandoff();
        File.WriteAllText(fixture.Path, "prior");
        string owned = System.IO.Path.Combine(fixture.Directory, ".visualization-read-model.json.old.tmp");
        string unrelated = System.IO.Path.Combine(fixture.Directory, ".visualization-read-model.json.keep.txt");
        File.WriteAllText(owned, "old"); File.WriteAllText(unrelated, "keep");
        fixture.Publisher.StartSession();
        Assert.False(File.Exists(fixture.Path)); Assert.False(File.Exists(owned)); Assert.True(File.Exists(unrelated));
    }

    [Fact]
    public void PublisherWritesCompleteVersionedHistoricalEnvelope()
    {
        using var fixture = new TemporaryHandoff(); fixture.Publisher.StartSession();
        fixture.Publisher.Publish(Model(VisualizationRevision.Historical(new HistoricalPresentationRevision(1), new string('a', 64)), VisualizationSourceMode.Historical, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations));
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixture.Path));
        Assert.Equal(VisualizationReadModelFilePublisher.ContractVersion, document.RootElement.GetProperty("contractVersion").GetString());
        Assert.Equal("HistoricalPresentation", document.RootElement.GetProperty("revision").GetProperty("kind").GetString());
        Assert.Equal(0, document.RootElement.GetProperty("sourceAuthority").GetInt32());
        Assert.Equal("Ready", document.RootElement.GetProperty("state").GetString());
        var health = document.RootElement.GetProperty("systemHealth");
        Assert.Equal("ready", health.GetProperty("state").GetString());
        Assert.Equal("historical", health.GetProperty("provenance").GetString());
        Assert.Equal(JsonValueKind.Null, health.GetProperty("reason").ValueKind);
    }

    [Fact]
    public void PublishingDecoratorPreservesAtomicStoreAndPublishesReplayEnvelope()
    {
        using var fixture = new TemporaryHandoff(); fixture.Publisher.StartSession();
        var memory = new AtomicVisualizationReadModelStore(); var store = new VisualizationReadModelFilePublishingStore(memory, fixture.Publisher);
        VisualizationReadModel model = Model(VisualizationRevision.Replay(3, new string('b', 64)), VisualizationSourceMode.Replay, DatasetSourceAuthority.Release19SimulatedLiveReplay);
        Assert.True(store.Publish(model)); Assert.Same(model, memory.Current);
        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(fixture.Path));
        Assert.Equal("ReplayLogicalTick", document.RootElement.GetProperty("revision").GetProperty("kind").GetString());
        Assert.Equal(1, document.RootElement.GetProperty("sourceAuthority").GetInt32());
        Assert.Equal("simulated", document.RootElement.GetProperty("systemHealth").GetProperty("provenance").GetString());
    }

    [Fact]
    public async Task ReadersObserveOnlyCompleteOldOrNewJson()
    {
        using var fixture = new TemporaryHandoff(); fixture.Publisher.StartSession();
        fixture.Publisher.Publish(Model(VisualizationRevision.Historical(new HistoricalPresentationRevision(1), new string('c', 64)), VisualizationSourceMode.Historical, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations));
        var reads = Enumerable.Range(0, 32).Select(_ => Task.Run(() => ReadContractVersion(fixture.Path))).ToArray();
        fixture.Publisher.Publish(Model(VisualizationRevision.Historical(new HistoricalPresentationRevision(2), new string('d', 64)), VisualizationSourceMode.Historical, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations));
        Assert.All(await Task.WhenAll(reads), version => Assert.Equal(VisualizationReadModelFilePublisher.ContractVersion, version));
    }

    private static VisualizationReadModel Model(VisualizationRevision revision, VisualizationSourceMode mode, DatasetSourceAuthority authority) => VisualizationReadModel.Create(
        revision, mode, authority, "BTC", VisualizationPresentationState.Ready,
        [new VisualizationObservation(DateTimeOffset.UnixEpoch, 10m), new VisualizationObservation(DateTimeOffset.UnixEpoch.AddMinutes(1), 12.5m)],
        feature: new VisualizationFeature(VisualizationFeature.SimpleReturnLag1V1, .25m, 2, 2));

    private static string? ReadContractVersion(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
        using var reader = new StreamReader(stream);
        using JsonDocument document = JsonDocument.Parse(reader.ReadToEnd());
        return document.RootElement.GetProperty("contractVersion").GetString();
    }

    private sealed class TemporaryHandoff : IDisposable
    {
        public TemporaryHandoff()
        { Directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"aiq-wp05-{Guid.NewGuid():N}"); System.IO.Directory.CreateDirectory(Directory); Path = System.IO.Path.Combine(Directory, "visualization-read-model.json"); Publisher = new VisualizationReadModelFilePublisher(Path); }
        public string Directory { get; } public string Path { get; } public VisualizationReadModelFilePublisher Publisher { get; }
        public void Dispose() { if (System.IO.Directory.Exists(Directory)) System.IO.Directory.Delete(Directory, true); }
    }
}
