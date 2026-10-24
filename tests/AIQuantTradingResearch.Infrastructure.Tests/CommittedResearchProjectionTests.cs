using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class CommittedResearchProjectionTests
{
    [Fact] public void ActualStorePersistsCalculatedRowsAndCheckpointAtSameVersionAndRestart()
    {
        using var dir = new ResearchTestDirectory();ResearchCommittedState saved;
        using (var store = new SqliteResearchStateStore(dir.Path, true))
        {
            saved = ResearchTestDirectory.Commit(store, new(Flat(0, 450)));
            Assert.True(saved.ResearchReady);Assert.Equal("Ready", saved.ResearchStatus);Assert.Equal(saved.Version, saved.Checkpoint!.Version);
            Assert.Equal("100.000000000000", saved.Candles[^1].Calculation!.Indicators.Sma20);
        }
        using var restart = new SqliteResearchStateStore(dir.Path, true);var read = restart.Read(ResearchInstrument.BtcUsd);
        Assert.Equal(saved.Generation, read.Generation);Assert.Equal(saved.Version, read.Version);Assert.Equal(saved.Candles, read.Candles);Assert.Equal(saved.Checkpoint, read.Checkpoint);
        var next = ResearchTestDirectory.Commit(restart, new(Flat(450, 1)));Assert.True(next.ResearchReady);Assert.Equal(saved.Version + 1, next.Checkpoint!.Version);
    }
    [Theory] [InlineData("projection")] [InlineData("checkpoint")] [InlineData("before-commit")]
    public void FailedTransactionNeverAdvancesRawOrCalculatedState(string point)
    {
        using var dir = new ResearchTestDirectory();var fail = false;
        using var store = new SqliteResearchStateStore(dir.Path, true, p => { if (fail && p == point) throw new IOException(); });
        var prior = ResearchTestDirectory.Commit(store, new(Flat(0, 401)));fail = true;
        Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(store, new(Flat(401, 1))));
        var after = store.Read(ResearchInstrument.BtcUsd);Assert.Equal(prior.Version, after.Version);Assert.Equal(prior.Candles, after.Candles);Assert.Equal(prior.Checkpoint, after.Checkpoint);
    }
    [Fact] public void PruningKeepsRecursiveStateAndEventWhenPredecessorWasPruned()
    {
        using var dir = new ResearchTestDirectory();
        using (var store = new SqliteResearchStateStore(dir.Path, true))
        {
            ResearchTestDirectory.Commit(store, new(Flat(0, 1000)));
            var retained = ResearchTestDirectory.Commit(store, new(Flat(1000, 400)));
            Assert.Equal(1000, retained.Candles.Count);Assert.Equal(401, retained.Candles[0].SegmentIndex);
            Assert.NotNull(retained.Candles[0].Calculation!.TrendTransition);Assert.True(retained.ResearchReady);
            Assert.Equal("100.000000000000", retained.Candles[^1].Calculation!.Indicators.Ema20);Assert.Equal("50.000000000000", retained.Candles[^1].Calculation!.Indicators.Rsi14);
        }
        using var restart = new SqliteResearchStateStore(dir.Path, true);
        var next = ResearchTestDirectory.Commit(restart, new(Flat(1400, 1)));
        Assert.Equal(1401, next.Candles[^1].SegmentIndex);Assert.True(next.ResearchReady);
        Assert.All(next.Candles, x => Assert.Null(x.Calculation!.TrendTransition));
    }
    [Fact] public void CorrectionRestartsAffectedSegmentAndNeverRetainsStaleResearch()
    {
        using var dir = new ResearchTestDirectory();using var store = new SqliteResearchStateStore(dir.Path, true);
        var original = ResearchTestDirectory.Commit(store, new(Flat(0, 450)));
        var correction = new CompletedResearchCandle(ResearchInstrument.BtcUsd, ResearchTestDirectory.Start.AddMinutes(400), 101, 101, 101, 101, 1);
        var updated = ResearchTestDirectory.Commit(store, new([correction]));
        Assert.Equal(original.Version + 1, updated.Version);Assert.False(updated.ResearchReady);Assert.Equal("Recalibrating", updated.ResearchStatus);
        Assert.NotEqual(original.Candles[400].Segment, updated.Candles[400].Segment);Assert.Null(updated.Candles[400].Calculation!.Indicators.Ema20);
        Assert.All(updated.Candles.Skip(400), x => Assert.False(x.Calculation!.Classification.Valid));
        Assert.Equal(original.Candles[399].Calculation, updated.Candles[399].Calculation);
        Assert.DoesNotContain(updated.Candles.Skip(400), x => x.Calculation!.TrendTransition is not null);
    }
    [Fact] public void StaleGenerationOrVersionAndDuplicatePollCannotAdvanceProjection()
    {
        using var dir = new ResearchTestDirectory();using var store = new SqliteResearchStateStore(dir.Path, true);
        var before = store.Read(ResearchInstrument.BtcUsd);var committed = ResearchTestDirectory.Commit(store, new(Flat(0, 420)));
        Assert.Throws<ResearchUnavailableException>(() => store.Commit(ResearchTestDirectory.Request(), new(Flat(420, 1)), before.Generation, before.Version));
        Assert.Throws<ResearchUnavailableException>(() => store.Commit(ResearchTestDirectory.Request(), new(Flat(420, 1)), Guid.NewGuid(), committed.Version));
        var repeated = ResearchTestDirectory.Commit(store, new(Flat(0, 420)));Assert.Equal(committed.Version, repeated.Version);Assert.Equal(committed.Checkpoint, repeated.Checkpoint);
    }
    [Fact] public void ResearchIsInstrumentIsolatedAndRawModeCannotOverwriteCalculatedStore()
    {
        using var dir = new ResearchTestDirectory();
        using (var store = new SqliteResearchStateStore(dir.Path, true))
        {
            ResearchTestDirectory.Commit(store, new(Flat(0, 401)));
            var eth = ResearchTestDirectory.Commit(store, new(Flat(0, 25, ResearchInstrument.EthUsd)), ResearchInstrument.EthUsd);
            Assert.False(eth.ResearchReady);Assert.True(store.Read(ResearchInstrument.BtcUsd).ResearchReady);
        }
        using var raw = new SqliteResearchStateStore(dir.Path);
        Assert.Throws<ResearchUnavailableException>(() => ResearchTestDirectory.Commit(raw, new(Flat(401, 1))));
    }
    [Fact] public void ExistingPrunedRawHistoryIsUnavailableWithoutSufficientSeedEvidence()
    {
        using var dir = new ResearchTestDirectory();
        using (var raw = new SqliteResearchStateStore(dir.Path))
        {
            ResearchTestDirectory.Commit(raw, new(Flat(0, 1000)));ResearchTestDirectory.Commit(raw, new(Flat(1000, 1)));
        }
        using var calculated = new SqliteResearchStateStore(dir.Path, true);
        var state = ResearchTestDirectory.Commit(calculated, new(Flat(1001, 1)));Assert.False(state.ResearchReady);Assert.All(state.Candles, x => Assert.Null(x.Calculation));
        var reset = ResearchTestDirectory.Commit(calculated, new(Flat(1003, 1)));Assert.NotNull(reset.Candles[^1].Calculation!.SegmentStart);Assert.False(reset.ResearchReady);
    }
    [Fact] public void NonflatCheckpointRoundTripMatchesUninterruptedProjection()
    {
        using var dir = new ResearchTestDirectory();var all = Enumerable.Range(0, 1401).Select(i =>
        {
            var p = 1000m + (i * 31 % 53);return new CompletedResearchCandle(ResearchInstrument.BtcUsd, ResearchTestDirectory.Start.AddMinutes(i), p, p + 2, p - 3, p, 1);
        }).ToArray();
        ResearchCommittedState prior;
        using (var store = new SqliteResearchStateStore(dir.Path, true))
        {
            ResearchTestDirectory.Commit(store, new(all.Take(1000)));prior = ResearchTestDirectory.Commit(store, new(all.Skip(1000).Take(400)));
        }
        using var restarted = new SqliteResearchStateStore(dir.Path, true);
        var resumed = ResearchTestDirectory.Commit(restarted, new([all[^1]]));
        var segment = resumed.Candles[^1].Segment;
        var origin = all.Select((c, i) => new ResearchStoredCandle(c, segment, i + 1)).ToArray();
        var expected = PrivateResearchCalculator.Project(new ResearchCommittedState(prior.Generation, ResearchInstrument.BtcUsd, resumed.Version, origin, ResearchTestDirectory.Start.AddDays(2), ResearchTestDirectory.Start.AddDays(2), ResearchIntegrity.Protected));
        Assert.Equal(expected.Candles.Select(x => x.Calculation), resumed.Candles.Select(x => x.Calculation));
    }
    [Fact] public void SqliteSchemaDdlIsUnchangedByCalculatedProjection()
    {
        using var dir = new ResearchTestDirectory();using (var raw = dir.Open()) { }
        var before = Ddl(dir.Path);
        using (var calculated = new SqliteResearchStateStore(dir.Path, true)) ResearchTestDirectory.Commit(calculated, new(Flat(0, 401)));
        Assert.Equal(before, Ddl(dir.Path));
    }
    private static string[] Ddl(string directory)
    {
        using var c = new SqliteConnection("Data Source=" + System.IO.Path.Combine(directory, "research.sqlite") + ";Pooling=False");c.Open();
        using var cmd = c.CreateCommand();cmd.CommandText = "SELECT sql FROM sqlite_schema WHERE type='table' ORDER BY name";using var r = cmd.ExecuteReader();
        var rows = new List<string>();while (r.Read()) rows.Add(r.GetString(0));return rows.ToArray();
    }
    private static CompletedResearchCandle[] Flat(int start, int count, ResearchInstrument instrument = ResearchInstrument.BtcUsd) => Enumerable.Range(start, count)
        .Select(i => new CompletedResearchCandle(instrument, ResearchTestDirectory.Start.AddMinutes(i), 100, 100, 100, 100, 1)).ToArray();
}
