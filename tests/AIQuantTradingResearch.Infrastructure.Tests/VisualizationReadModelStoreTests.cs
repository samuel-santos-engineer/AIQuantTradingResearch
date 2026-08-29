using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Infrastructure.Visualization;
using System.Reflection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class VisualizationReadModelStoreTests
{
    [Fact]
    public void WindowIsBoundedOrderedAndReplacesDuplicateSourceTime()
    {
        var start = DateTimeOffset.UnixEpoch;
        var rows = Enumerable.Range(0, 65).Select(i => new VisualizationObservation(start.AddMinutes(i), i + 1m)).ToList();
        rows.Add(new VisualizationObservation(start.AddMinutes(64), 999m));
        var model = VisualizationReadModel.Create(VisualizationRevision.Replay(1, "state"), VisualizationSourceMode.Replay,
            DatasetSourceAuthority.Release19SimulatedLiveReplay, "BTC", VisualizationPresentationState.Ready, rows);
        Assert.Equal(64, model.Window.Count); Assert.Equal(start.AddMinutes(1), model.Window[0].SourceTime);
        Assert.Equal(999m, model.Latest!.Price); Assert.Throws<NotSupportedException>(() => ((IList<VisualizationObservation>)model.Window).Add(model.Latest));
    }

    [Fact]
    public void StoreRejectsOlderAndEqualConflictingButAcceptsIdempotence()
    {
        var store = new AtomicVisualizationReadModelStore();
        var newer = Model(2, "same"); Assert.True(store.Publish(newer)); Assert.False(store.Publish(Model(1, "old")));
        Assert.True(store.Publish(Model(2, "same"))); Assert.Throws<InvalidOperationException>(() => store.Publish(Model(2, "other")));
    }

    [Fact]
    public void HistoricalRevisionsStartAtOneIncrementAndStaleRetainsRevision()
    {
        var useCase = new VisualizationReadModelUseCase(new AtomicVisualizationReadModelStore());
        var first = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Empty);
        var stale = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Stale, category: "NoNewResult");
        var failed = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Failed, category: "DependencyUnavailable");
        Assert.Equal((ulong)1, first.Revision.Value); Assert.Equal(first.Revision.Value, stale.Revision.Value); Assert.Equal((ulong)2, failed.Revision.Value);
    }

    [Fact]
    public void HistoricalStatesTransitionAndRecoveryRetainSafeLastGoodPayload()
    {
        var useCase = new VisualizationReadModelUseCase(new AtomicVisualizationReadModelStore());
        var warm = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.WarmUp,
            [new VisualizationObservation(DateTimeOffset.UnixEpoch, 1m)], feature: VisualizationFeature.WarmUp(1));
        var ready = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Ready,
            [new VisualizationObservation(DateTimeOffset.UnixEpoch.AddMinutes(1), 2m)], feature: new VisualizationFeature(VisualizationFeature.SimpleReturnLag1V1, 1m, 2, 2));
        var failed = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Failed, category: "DependencyUnavailable");
        var recovered = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Ready,
            feature: new VisualizationFeature(VisualizationFeature.SimpleReturnLag1V1, 1m, 2, 2));
        Assert.Equal(VisualizationPresentationState.WarmUp, warm.State); Assert.Equal(2, warm.Feature!.RequiredObservationCount);
        Assert.Equal(VisualizationPresentationState.Ready, ready.State); Assert.Equal(VisualizationPresentationState.Failed, failed.State);
        Assert.Equal(ready.Window, failed.Window); Assert.NotNull(failed.Failure); Assert.Equal(VisualizationPresentationState.Ready, recovered.State);
        Assert.Null(recovered.Failure); Assert.Equal((ulong)4, recovered.Revision.Value);
    }

    [Fact]
    public void PublicationComposesBoundedSystemHealthWithoutChangingVisualizationState()
    {
        var useCase = new VisualizationReadModelUseCase(new AtomicVisualizationReadModelStore());
        var ready = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Ready);
        var warmup = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.WarmUp);
        var empty = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Empty);
        var stale = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Stale, category: "NoNewResult");
        var failed = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Failed, category: "DependencyUnavailable");
        var replay = useCase.PublishReplay(1, "BTC", DatasetSourceAuthority.Release19SimulatedLiveReplay, VisualizationPresentationState.Ready);

        Assert.Equal((VisualizationPresentationState.Ready, SystemHealthState.Ready, "historical", (string?)null), (ready.State, ready.SystemHealth!.State, ready.SystemHealth.Provenance, ready.SystemHealth.Reason));
        Assert.Equal((VisualizationPresentationState.WarmUp, SystemHealthState.WarmUp, "historical", (string?)null), (warmup.State, warmup.SystemHealth!.State, warmup.SystemHealth.Provenance, warmup.SystemHealth.Reason));
        Assert.Equal((VisualizationPresentationState.Empty, SystemHealthState.Empty, "historical", (string?)null), (empty.State, empty.SystemHealth!.State, empty.SystemHealth.Provenance, empty.SystemHealth.Reason));
        Assert.Equal((VisualizationPresentationState.Stale, SystemHealthState.Stale, "historical", "structural-staleness"), (stale.State, stale.SystemHealth!.State, stale.SystemHealth.Provenance, stale.SystemHealth.Reason));
        Assert.Equal((VisualizationPresentationState.Failed, SystemHealthState.Failed, "historical", "pipeline-failed"), (failed.State, failed.SystemHealth!.State, failed.SystemHealth.Provenance, failed.SystemHealth.Reason));
        Assert.Equal((VisualizationPresentationState.Ready, SystemHealthState.Ready, "simulated", (string?)null), (replay.State, replay.SystemHealth!.State, replay.SystemHealth.Provenance, replay.SystemHealth.Reason));
    }

    [Fact]
    public void ProducerIgnoresOlderRowsAndAccumulatesAcceptedRows()
    {
        var useCase = new VisualizationReadModelUseCase(new AtomicVisualizationReadModelStore()); var start = DateTimeOffset.UnixEpoch;
        useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.WarmUp,
            [new VisualizationObservation(start.AddMinutes(2), 2m)]);
        var current = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Ready,
            [new VisualizationObservation(start.AddMinutes(1), 1m), new VisualizationObservation(start.AddMinutes(3), 3m)]);
        Assert.Equal([start.AddMinutes(2), start.AddMinutes(3)], current.Window.Select(row => row.SourceTime));
    }

    [Fact]
    public void HistoricalOverflowDoesNotWrapAndNewSessionResets()
    {
        var store = new AtomicVisualizationReadModelStore(); var useCase = new VisualizationReadModelUseCase(store);
        typeof(VisualizationReadModelUseCase).GetField("nextHistoricalRevision", BindingFlags.Instance | BindingFlags.NonPublic)!.SetValue(useCase, ulong.MaxValue - 1);
        var maximum = useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Empty);
        Assert.Equal(ulong.MaxValue, maximum.Revision.Value);
        Assert.Throws<OverflowException>(() => useCase.PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Empty));
        Assert.Equal(ulong.MaxValue, store.Current!.Revision.Value);
        var restarted = new VisualizationReadModelUseCase(new AtomicVisualizationReadModelStore()).PublishHistorical("BTC", DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, VisualizationPresentationState.Empty);
        Assert.Equal((ulong)1, restarted.Revision.Value);
    }

    [Fact]
    public async Task ConcurrentReadersObserveOnlyCompleteOldOrNewEnvelope()
    {
        var store = new AtomicVisualizationReadModelStore(); var oldModel = Model(1, "old"); var newModel = Model(2, "new");
        Assert.True(store.Publish(oldModel)); var beforeRead = new CountdownEvent(16); var publish = new ManualResetEventSlim();
        var readers = Enumerable.Range(0, 16).Select(_ => Task.Run(() => { var value = store.Current; beforeRead.Signal(); publish.Wait(); return value; })).ToList();
        beforeRead.Wait(); Assert.True(store.Publish(newModel)); publish.Set();
        readers.AddRange(Enumerable.Range(0, 16).Select(_ => Task.Run(() => store.Current)));
        var observed = await Task.WhenAll(readers);
        Assert.All(observed, item => Assert.True(ReferenceEquals(item, oldModel) || ReferenceEquals(item, newModel)));
        Assert.All(observed, item => Assert.Equal(item!.Revision.Value == 1 ? "old" : "new", item.Revision.Identity));
    }

    private static VisualizationReadModel Model(int revision, string identity) => VisualizationReadModel.Create(
        VisualizationRevision.Replay(revision, identity), VisualizationSourceMode.Replay, DatasetSourceAuthority.Release19SimulatedLiveReplay,
        "BTC", VisualizationPresentationState.Empty);
}
