using System.Text.Json.Nodes;
using AIQuantTradingResearch.Application.MarketData;
using Microsoft.Data.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ResearchStateRecoveryTests
{
    [Theory] [InlineData("before-commit", 1)] [InlineData("checkpoint", 5)]
    public void InjectedTransactionalFaultRollsBackAllState(string point, int failureCommit)
    {
        using var dir = new ResearchTestDirectory();var enabled = false;
        using (var store = dir.Open(p => { if (enabled && p == point) throw new IOException("injected private canary"); }))
        {
            for (var i = 0; i < failureCommit - 1; i++) ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(i)]));
            enabled = true;
            var error = Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(failureCommit)])));
            Assert.Equal("Research unavailable.", error.Message);Assert.Equal(failureCommit - 1, store.Read(ResearchInstrument.BtcUsd).Version);
        }
        using var restart = dir.Open();Assert.Equal(failureCommit - 1, restart.Read(ResearchInstrument.BtcUsd).Version);
    }
    [Theory] [InlineData("watermark")] [InlineData("watermark-replace")]
    public void CommittedDatabaseAheadOfWitnessReportsDegradedAndValidatesOnRestart(string point)
    {
        using var dir = new ResearchTestDirectory();var enabled = false;
        using (var store = dir.Open(p => { if (enabled && p == point) throw new IOException(); }))
        {
            enabled = true;var committed = ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)]));
            Assert.Equal(1, committed.Version);Assert.Equal(ResearchIntegrity.Degraded, committed.Integrity);
            Assert.Single(store.Read(ResearchInstrument.BtcUsd).Candles);
        }
        using var restart = dir.Open();Assert.Equal(ResearchIntegrity.Protected, restart.Read(ResearchInstrument.BtcUsd).Integrity);
        Assert.Equal(1, restart.Read(ResearchInstrument.BtcUsd).Version);Assert.False(File.Exists(System.IO.Path.Combine(dir.Path, "watermark.json.pending")));
    }
    [Theory] [InlineData("ahead")] [InlineData("generation")] [InlineData("hash")] [InlineData("missing")]
    public void ContradictoryOrMissingWitnessFailsClosed(string mutation)
    {
        using var dir = new ResearchTestDirectory();using (var store = dir.Open()) ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)]));
        var path = System.IO.Path.Combine(dir.Path, "watermark.json");var node = JsonNode.Parse(File.ReadAllText(path))!;
        if (mutation == "ahead") node["Revision"] = 50;
        if (mutation == "generation") node["Generation"] = Guid.NewGuid().ToString();
        if (mutation == "hash") node["Hash"] = new string('0', 64);
        if (mutation == "missing") File.Delete(path);else File.WriteAllText(path, node.ToJsonString());
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Theory] [InlineData("checkpoint")] [InlineData("payload")] [InlineData("chain")]
    public void CorruptCheckpointOrCommittedStateCannotBePublished(string mutation)
    {
        using var dir = new ResearchTestDirectory();using (var store = dir.Open()) ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)]));
        using (var connection = new SqliteConnection("Data Source=" + System.IO.Path.Combine(dir.Path, "research.sqlite") + ";Pooling=False"))
        {
            connection.Open();using var cmd = connection.CreateCommand();
            cmd.CommandText = mutation switch { "checkpoint" => "UPDATE checkpoints SET payload='{}' WHERE instrument=0", "payload" => "UPDATE instruments SET payload='[]' WHERE instrument=0", _ => "UPDATE commits SET previous_hash='bad' WHERE revision=1" };cmd.ExecuteNonQuery();
        }
        Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Fact] public void CorruptDatabaseAndDatabaseRollbackWithNewerWitnessFailClosed()
    {
        using var dir = new ResearchTestDirectory();byte[] old;
        using (var initial = dir.Open()) ResearchTestDirectory.Commit(initial, new([ResearchTestDirectory.Candle(0)]));
        var path = System.IO.Path.Combine(dir.Path, "research.sqlite");old = File.ReadAllBytes(path);
        using (var later = dir.Open()) ResearchTestDirectory.Commit(later, new([ResearchTestDirectory.Candle(1)]));
        File.WriteAllBytes(path, old);Assert.Throws<ResearchUnavailableException>(() => dir.Open());
        File.WriteAllBytes(path, [1, 2, 3]);Assert.Throws<ResearchUnavailableException>(() => dir.Open());
    }
    [Fact] public void EvidenceBudgetCannotPruneAnUnresolvedWitness()
    {
        using var dir = new ResearchTestDirectory();var enabled = false;
        using var store = dir.Open(p => { if (enabled && p == "watermark") throw new IOException(); });
        enabled = true;
        for (var i = 0; i < 127; i++) ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(i)]));
        Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(127)])));
        Assert.Equal(127, store.Read(ResearchInstrument.BtcUsd).Version);
    }
    [Fact] public void CorruptionAfterOpeningDisablesFurtherReadsAndWrites()
    {
        using var dir = new ResearchTestDirectory();using var store = dir.Open();
        ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(0)]));
        File.WriteAllText(System.IO.Path.Combine(dir.Path, "watermark.json"), "{}");
        Assert.Throws<ResearchUnavailableException>(() => store.Read(ResearchInstrument.BtcUsd));
        Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(1)])));
    }
    [Fact] public void CheckpointCadenceAndPruningSurviveRestartWithoutClaimingIndicatorState()
    {
        using var dir = new ResearchTestDirectory();Guid generation;
        using (var store = dir.Open())
        {
            for (var i = 0; i < 7; i++) ResearchTestDirectory.Commit(store, new([ResearchTestDirectory.Candle(i)]));
            generation = store.Read(ResearchInstrument.BtcUsd).Generation;
        }
        using var reopened = dir.Open();var state = reopened.Read(ResearchInstrument.BtcUsd);
        Assert.Equal(generation, state.Generation);Assert.Equal(7, state.Version);Assert.False(state.ResearchReady);
    }
}
