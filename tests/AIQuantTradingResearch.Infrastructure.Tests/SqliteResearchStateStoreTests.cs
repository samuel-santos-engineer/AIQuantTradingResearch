using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SqliteResearchStateStoreTests(Xunit.Abstractions.ITestOutputHelper output)
{
    [Fact] public void NativeSqliteVersionIsRecordedWithoutNetwork()
    {
        using var connection = new Microsoft.Data.Sqlite.SqliteConnection("Data Source=:memory:");connection.Open();
        output.WriteLine("Native SQLite=" + connection.ServerVersion + "; runtime=" + Environment.Version);
        Assert.NotEmpty(connection.ServerVersion);
    }
    [Fact] public void EmptyDatabaseHasStableGenerationAndNoResearchOrSeed()
    {
        using var dir = new ResearchTestDirectory(); Guid generation;
        using (var store = dir.Open())
        {
            var state = store.Read(ResearchInstrument.BtcUsd);generation = state.Generation;
            Assert.NotEqual(Guid.Empty, generation);Assert.Equal(0, state.Version);Assert.Empty(state.Candles);
            Assert.Null(state.LastCommitAt);Assert.False(state.ResearchReady);Assert.Equal("Unavailable", state.ResearchStatus);
        }
        using var reopened = dir.Open();Assert.Equal(generation, reopened.Read(ResearchInstrument.EthUsd).Generation);
    }
    [Fact] public void InstrumentsAreSeparateAndSnapshotsAreImmutable()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();
        var input = new[] { ResearchTestDirectory.Candle(0) };var batch = new ResearchPollBatch(input);
        input[0] = ResearchTestDirectory.Candle(1);
        var btc = ResearchTestDirectory.Commit(store, batch);
        Assert.Single(btc.Candles);Assert.Equal(ResearchTestDirectory.Start, btc.Candles[0].Candle.OpenTimeUtc);
        Assert.Empty(store.Read(ResearchInstrument.EthUsd).Candles);
        Assert.Throws<NotSupportedException>(() => ((IList<ResearchStoredCandle>)btc.Candles).Clear());
        ResearchTestDirectory.Commit(store, new ResearchPollBatch([ResearchTestDirectory.Candle(1)]));
        Assert.Single(btc.Candles);
    }
    [Fact] public void IdenticalDuplicatesDoNotAdvanceVersionAndCorrectionDoes()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();var first = ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0), ResearchTestDirectory.Candle(1)]));
        Assert.Equal(first.Version, ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)])).Version);
        var corrected = ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0, close: 101)]));
        Assert.Equal(first.Version + 1, corrected.Version);Assert.Equal(101, corrected.Candles[0].Candle.Close);
        Assert.NotEqual(first.Candles[0].Segment, corrected.Candles[0].Segment);Assert.Equal(1, corrected.Candles[0].SegmentIndex);
        Assert.Equal(corrected.Candles[0].Segment, corrected.Candles[1].Segment);
    }
    [Fact] public void GapsRemainActualGapsAndStartNewSegment()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();
        var state = ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0), ResearchTestDirectory.Candle(2)]));
        Assert.Equal(2, state.Candles.Count);Assert.NotEqual(state.Candles[0].Segment, state.Candles[1].Segment);Assert.Equal(1, state.Candles[1].SegmentIndex);
    }
    [Fact] public void RetainsOnlyLatestThousandWithoutResettingExistingSegmentIndex()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();
        ResearchTestDirectory.Commit(store, new(Enumerable.Range(0, 1000).Select(i => ResearchTestDirectory.Candle(i))));
        var state = ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(1000)]));
        Assert.Equal(1000, state.Candles.Count);Assert.Equal(ResearchTestDirectory.Start.AddMinutes(1), state.Candles[0].Candle.OpenTimeUtc);
        Assert.Equal(1001, state.Candles[^1].SegmentIndex);
        Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)])));
    }
    [Fact] public void StaleExpectedVersionCannotOverwriteCommittedData()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();var old = store.Read(ResearchInstrument.BtcUsd);
        ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)]));
        Assert.Throws<ResearchUnavailableException>(() => store.Commit(ResearchTestDirectory.Request(), new([ResearchTestDirectory.Candle(1)]), old.Generation, old.Version));
        Assert.Single(store.Read(ResearchInstrument.BtcUsd).Candles);
    }
    [Fact] public void EmptySuccessfulPollDoesNotInventCommit()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();var state = ResearchTestDirectory.Commit(store, new([]));
        Assert.Equal(0, state.Version);Assert.Null(state.LastCommitAt);Assert.NotNull(state.LastSuccessfulProviderPollAt);Assert.Empty(state.Candles);
    }
    [Fact] public void LocalWriterCannotOverlapAndOwnershipCanBeReacquiredAfterDispose()
    {
        using var dir = new ResearchTestDirectory();using (var first = dir.Open()) Assert.Throws<ResearchUnavailableException>(() => dir.Open());
        using var second = dir.Open();Assert.Empty(second.Read(ResearchInstrument.BtcUsd).Candles);
    }
    [Fact] public void CancellationDoesNotCommit()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();var prior = store.Read(ResearchInstrument.BtcUsd);
        Assert.Throws<OperationCanceledException>(() => store.Commit(ResearchTestDirectory.Request(), new([ResearchTestDirectory.Candle(0)]), prior.Generation, 0, new CancellationToken(true)));
        Assert.Equal(0, store.Read(ResearchInstrument.BtcUsd).Version);
    }
    [Fact] public async Task ConcurrentReadersObserveOnlyCompleteCommittedSnapshots()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();
        var writer = Task.Run(() => { for (var i = 0; i < 12; i++) ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(i)])); });
        for (var i = 0; i < 12; i++) { var view = store.Read(ResearchInstrument.BtcUsd);Assert.Equal(view.Version, view.Candles.Count); }
        await writer;Assert.Equal(12, store.Read(ResearchInstrument.BtcUsd).Version);
    }
}

internal sealed class ResearchTestDirectory : IDisposable
{
    internal static readonly DateTimeOffset Start = new(2026, 10, 1, 0, 0, 0, TimeSpan.Zero);
    internal string Path { get; } = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "aiq-wp03-test-" + Guid.NewGuid().ToString("N"));
    internal SqliteResearchStateStore Open(Action<string>? fault = null) => new(Path, fault);
    internal static CompletedResearchCandle Candle(int minute, ResearchInstrument instrument = ResearchInstrument.BtcUsd, decimal close = 100) =>
        new(instrument, Start.AddMinutes(minute), 100, Math.Max(101, close), 99, close, 1);
    internal static ResearchPollRequest Request(ResearchInstrument instrument = ResearchInstrument.BtcUsd) => new(instrument, Start.AddDays(2), TimeSpan.Zero);
    internal static ResearchCommittedState Commit(SqliteResearchStateStore store, ResearchPollBatch batch, ResearchInstrument instrument = ResearchInstrument.BtcUsd)
    {
        var prior = store.Read(instrument);return store.Commit(Request(instrument), batch, prior.Generation, prior.Version);
    }
    public void Dispose() { if (Directory.Exists(Path)) Directory.Delete(Path, true); }
}
