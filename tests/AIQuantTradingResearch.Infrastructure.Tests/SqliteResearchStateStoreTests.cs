using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SqliteResearchStateStoreTests(Xunit.Abstractions.ITestOutputHelper output)
{
    private static ResearchTransformConfiguration Configuration(SqliteResearchStateStore store, long epoch = 1, Guid? keyEpoch = null, byte fill = 7) =>
        new(store.Read(ResearchInstrument.BtcUsd).Generation, Enumerable.Repeat(fill, 32).ToArray(), keyEpoch ?? Guid.NewGuid(), epoch);

    [Fact] public void TransformMissingFailsClosedWithoutBreakingLegacyReader()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open();
        Assert.Throws<ResearchUnavailableException>(() => store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, _) => true));
        Assert.Empty(store.Read(ResearchInstrument.BtcUsd).Candles);
    }
    [Fact] public void TransformRestartReusesExactTupleForBothInstrumentsWithoutChangingCandleVersion()
    {
        using var dir = new ResearchTestDirectory(); ResearchTransformConfiguration config; Guid generation;
        using (var store = dir.Open())
        {
            var before = ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)])); generation = before.Generation;
            config = Configuration(store); store.ActivateTransform(config, 0);
            Assert.Equal(before.Version, store.Read(ResearchInstrument.BtcUsd).Version);
            Assert.Equal(before.LastCommitAt, store.Read(ResearchInstrument.BtcUsd).LastCommitAt);
        }
        using var reopened = dir.Open();
        foreach (var instrument in Enum.GetValues<ResearchInstrument>())
            reopened.UseTransformSnapshot(instrument, (state, active) =>
            {
                Assert.Equal(generation, state.Generation); Assert.Equal(generation, active.Generation);
                Assert.Equal(config.KeyEpoch, active.KeyEpoch); Assert.Equal(1, active.TransformEpoch);
                Assert.Equal(config.CopyKey(), active.CopyKey()); return true;
            });
    }
    [Fact] public void TransformDefensivelyCopiesKeyAndDoesNotSerializePrivateMetadata()
    {
        var bytes = new byte[32]; var config = new ResearchTransformConfiguration(Guid.NewGuid(), bytes, Guid.NewGuid(), 1);
        bytes[0] = 5; var copy = config.CopyKey(); copy[0] = 6;
        Assert.Equal(0, config.CopyKey()[0]); Assert.Equal("{}", System.Text.Json.JsonSerializer.Serialize(config));
        Assert.Equal("Private research transform configuration.", config.ToString());
    }
    [Theory] [InlineData(0)] [InlineData(31)] [InlineData(33)]
    public void TransformRejectsInvalidKeyLength(int size) =>
        Assert.Throws<ArgumentException>(() => new ResearchTransformConfiguration(Guid.NewGuid(), new byte[size], Guid.NewGuid(), 1));
    [Fact] public void TransformRejectsEmptyIdentitiesAndNonpositiveEpoch()
    {
        Assert.Throws<ArgumentException>(() => new ResearchTransformConfiguration(Guid.Empty, new byte[32], Guid.NewGuid(), 1));
        Assert.Throws<ArgumentException>(() => new ResearchTransformConfiguration(Guid.NewGuid(), new byte[32], Guid.Empty, 1));
        Assert.Throws<ArgumentException>(() => new ResearchTransformConfiguration(Guid.NewGuid(), new byte[32], Guid.NewGuid(), 0));
    }
    [Fact] public void TransformRotationIsMonotonicAndExactReinstallIsNoOp()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open();
        var first = Configuration(store); store.ActivateTransform(first, 0);
        var witness = File.ReadAllBytes(System.IO.Path.Combine(dir.Path, "watermark.json"));
        store.ActivateTransform(first, 1);
        Assert.Equal(witness, File.ReadAllBytes(System.IO.Path.Combine(dir.Path, "watermark.json")));
        Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(Configuration(store, 2, first.KeyEpoch, 8), 1));
        Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(Configuration(store, 3), 1));
        Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(Configuration(store, 2), 0));
        Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(new(Guid.NewGuid(), new byte[32], Guid.NewGuid(), 2), 1));
        store.ActivateTransform(Configuration(store, 2), 1);
        Assert.Equal(2, store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) => c.TransformEpoch));
        Assert.Equal(0, store.Read(ResearchInstrument.BtcUsd).Version);
    }
    [Fact] public void TransformCancellationDoesNotProvision()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open();
        Assert.Throws<OperationCanceledException>(() => store.ActivateTransform(Configuration(store), 0, new CancellationToken(true)));
        Assert.Throws<ResearchUnavailableException>(() => store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, _) => true));
    }
    [Fact] public void TransformPrecommitFailureRollsBackAndCanRetry()
    {
        using var dir = new ResearchTestDirectory(); var fail = true;
        using var store = dir.Open(point => { if (fail && point == "transform-before-commit") throw new IOException("Injected."); });
        var config = Configuration(store);
        Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(config, 0)); fail = false;
        Assert.Throws<ResearchUnavailableException>(() => store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, _) => true));
        store.ActivateTransform(config, 0);
        Assert.Equal(1, store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) => c.TransformEpoch));
    }
    [Theory] [InlineData("transform-after-commit")] [InlineData("watermark")] [InlineData("watermark-replace")]
    public void TransformUnwitnessedCommitBlocksPublicationAndRestart(string faultPoint)
    {
        using var dir = new ResearchTestDirectory(); var armed = false;
        using (var store = dir.Open(point => { if (armed && point == faultPoint) throw new IOException("Injected."); }))
        {
            var config = Configuration(store); armed = true;
            Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(config, 0));
            Assert.Throws<ResearchUnavailableException>(() => store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, _) => true));
        }
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Theory] [InlineData("key_base64", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=")]
    [InlineData("key_epoch", "22222222-2222-4222-8222-222222222222")]
    [InlineData("transform_epoch", "2")]
    [InlineData("generation", "22222222-2222-4222-8222-222222222222")]
    public void TransformTamperIsBoundIntoExistingRecoveryHash(string column, string value)
    {
        using var dir = new ResearchTestDirectory();
        using (var store = dir.Open()) store.ActivateTransform(Configuration(store), 0);
        using (var c = new Microsoft.Data.Sqlite.SqliteConnection("Pooling=False;Data Source=" + System.IO.Path.Combine(dir.Path, "research.sqlite")))
        {
            c.Open(); using var command = c.CreateCommand();
            command.CommandText = "UPDATE transform_configuration SET " + column + "=$v";
            command.Parameters.AddWithValue("$v", value); command.ExecuteNonQuery();
        }
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Fact] public void TransformRestoredOldWitnessCannotValidateNewRotation()
    {
        using var dir = new ResearchTestDirectory(); byte[] oldWitness;
        using (var store = dir.Open())
        {
            store.ActivateTransform(Configuration(store), 0);
            oldWitness = File.ReadAllBytes(System.IO.Path.Combine(dir.Path, "watermark.json"));
            store.ActivateTransform(Configuration(store, 2), 1);
        }
        File.WriteAllBytes(System.IO.Path.Combine(dir.Path, "watermark.json"), oldWitness);
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Fact] public void TransformPublicationRejectsReentrantMutationAndReleasesGuardAfterCallbackFailure()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); store.ActivateTransform(Configuration(store), 0);
        store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, _) =>
        {
            Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(Configuration(store, 2), 1));
            Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(store, new([])));
            Assert.Throws<ResearchUnavailableException>(() => store.Dispose());
            return true;
        });
        Assert.Throws<InvalidOperationException>(() => store.UseTransformSnapshot<bool>(ResearchInstrument.BtcUsd, (_, _) => throw new InvalidOperationException()));
        store.ActivateTransform(Configuration(store, 2), 1);
        Assert.Equal(2, store.UseTransformSnapshot(ResearchInstrument.EthUsd, (_, c) => c.TransformEpoch));
    }
    [Fact] public async Task TransformRotationWaitsUntilStagedPublicationCompletes()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); store.ActivateTransform(Configuration(store), 0);
        var next = Configuration(store, 2); using var entered = new ManualResetEventSlim(); using var release = new ManualResetEventSlim();
        var publication = Task.Run(() => store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) =>
        {
            entered.Set(); Assert.True(release.Wait(TimeSpan.FromSeconds(5))); Assert.Equal(1, c.TransformEpoch); return true;
        }));
        Assert.True(entered.Wait(TimeSpan.FromSeconds(5)));
        using var rotating = new ManualResetEventSlim();
        var rotation = Task.Run(() => { rotating.Set(); store.ActivateTransform(next, 1); });
        Assert.True(rotating.Wait(TimeSpan.FromSeconds(5)));
        try { await Task.Delay(100); Assert.False(rotation.IsCompleted); }
        finally { release.Set(); }
        await publication; await rotation;
        Assert.Equal(2, store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) => c.TransformEpoch));
    }

    [Theory] [InlineData("transform-after-commit")] [InlineData("watermark")] [InlineData("watermark-replace")]
    public void TransformFailedRotationNeverFallsBackToPreviouslyWitnessedEpoch(string faultPoint)
    {
        using var dir = new ResearchTestDirectory(); var armed = false;
        using (var store = dir.Open(point => { if (armed && point == faultPoint) throw new IOException("Injected."); }))
        {
            store.ActivateTransform(Configuration(store), 0); var next = Configuration(store, 2); armed = true;
            Assert.Throws<ResearchUnavailableException>(() => store.ActivateTransform(next, 1));
            Assert.Throws<ResearchUnavailableException>(() => store.UseTransformSnapshot(ResearchInstrument.EthUsd, (_, _) => true));
        }
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Fact] public void TransformBindingSurvivesCommitChainPruningAndMissingWitnessFailsClosed()
    {
        using var dir = new ResearchTestDirectory();
        using (var store = dir.Open())
        {
            store.ActivateTransform(Configuration(store), 0);
            for (var i = 0; i < 135; i++) ResearchTestDirectory.Commit(store, new([]));
        }
        using (var reopened = dir.Open()) Assert.Equal(1, reopened.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) => c.TransformEpoch));
        File.Delete(System.IO.Path.Combine(dir.Path, "watermark.json"));
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Fact] public void TransformProvisioningPreservesCalculatedCheckpointRowsAndLineage()
    {
        using var dir = new ResearchTestDirectory(); ResearchCommittedState before;
        using (var store = new SqliteResearchStateStore(dir.Path, true))
        {
            before = ResearchTestDirectory.Commit(store, new(Enumerable.Range(0, 401).Select(i => ResearchTestDirectory.Candle(i))));
            Assert.True(before.ResearchReady); store.ActivateTransform(Configuration(store), 0);
        }
        using var reopened = new SqliteResearchStateStore(dir.Path, true);
        reopened.UseTransformSnapshot(ResearchInstrument.BtcUsd, (state, _) =>
        {
            Assert.Equal(before.Generation, state.Generation); Assert.Equal(before.Version, state.Version);
            Assert.Equal(before.Checkpoint, state.Checkpoint); Assert.Equal(before.Candles.ToArray(), state.Candles.ToArray());
            Assert.Equal(before.LastCommitAt, state.LastCommitAt); Assert.True(state.ResearchReady); return true;
        });
    }

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
