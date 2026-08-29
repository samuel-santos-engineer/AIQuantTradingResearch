using AIQuantTradingResearch.Application.Datasets;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SqliteDatasetTests
{
    [Fact]
    public void StoreAndRetrieveEmitBoundedPersistenceActivitiesAndMetrics()
    {
        using var database = new DatasetDatabase();
        var store = new SqliteDatasetSnapshotStore(database.Factory);
        var snapshot = Snapshot('a');
        var activities = new List<Activity>();
        using var parent = new Activity(nameof(StoreAndRetrieveEmitBoundedPersistenceActivitiesAndMetrics)).Start();
        using var listener = new ActivityListener { ShouldListenTo = source => source.Name == "AIQuantTradingResearch.Infrastructure", Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData, ActivityStopped = activity => activities.Add(activity) };
        ActivitySource.AddActivityListener(listener);
        var instruments = new List<(string Name, string? Unit)>();
        using var meter = new MeterListener();
        meter.InstrumentPublished = (instrument, observer) => { if (instrument.Meter.Name == "AIQuantTradingResearch.Infrastructure") { instruments.Add((instrument.Name, instrument.Unit)); observer.EnableMeasurementEvents(instrument); } };
        meter.Start();

        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, store.Store(snapshot).Outcome);
        Assert.True(store.Retrieve(snapshot.SnapshotIdentity).IsFound);

        Assert.Equal(2, activities.Count(activity => activity.OperationName == "persistence.operation" && activity.TraceId == parent.TraceId));
        Assert.Contains(("persistence.operations", "{operation}"), instruments);
        Assert.Contains(("persistence.duration", "ms"), instruments);
        Assert.Contains(("persistence.failures", "{operation}"), instruments);
    }
    [Fact]
    public void MapperRoundTripPreservesDatasetEvidenceIncludingOffsetsAndDecimals()
    {
        var snapshot = Snapshot('a', price: 12.34567890123456789012345678m);

        var record = SqliteDatasetMapper.ToSnapshotRecord(new DatasetCatalogEntry(snapshot));
        var observations = SqliteDatasetMapper.ToObservationRecords(snapshot);
        var reconstructed = SqliteDatasetMapper.ToSnapshotCandidate(record, observations);

        AssertSnapshotEquivalent(snapshot, reconstructed);
    }

    [Fact]
    public void StorePersistsRetrievesAndKeepsEquivalentEvidenceAcrossInstances()
    {
        using var database = new DatasetDatabase();
        var snapshot = Snapshot('a');
        var first = new SqliteDatasetSnapshotStore(database.Factory);

        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, first.Store(snapshot).Outcome);
        var retrieved = first.Retrieve(snapshot.SnapshotIdentity);
        Assert.True(retrieved.IsFound);
        AssertSnapshotEquivalent(snapshot, retrieved.Snapshot!);

        var second = new SqliteDatasetSnapshotStore(database.Factory);
        Assert.Equal(DatasetSnapshotStoreOutcome.EquivalentExisting, second.Store(snapshot).Outcome);
        AssertSnapshotEquivalent(snapshot, second.Retrieve(snapshot.SnapshotIdentity).Snapshot!);
    }

    [Fact]
    public void StoreSupportsEmptyAndMultipleVersionsButRejectsConflictingSameIdentityWithoutOverwrite()
    {
        using var database = new DatasetDatabase();
        var store = new SqliteDatasetSnapshotStore(database.Factory);
        var nonEmpty = Snapshot('a');
        var empty = Snapshot('b', empty: true);

        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, store.Store(nonEmpty).Outcome);
        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, store.Store(empty).Outcome);
        Assert.Empty(store.Retrieve(empty.SnapshotIdentity).Snapshot!.Observations);
        Assert.Equal(2, Count(database.Factory, "SELECT COUNT(*) FROM dataset_snapshots;"));

        var conflict = Snapshot('a', price: 99m);
        Assert.Equal(DatasetSnapshotStoreOutcome.IntegrityConflict, store.Store(conflict).Outcome);
        AssertSnapshotEquivalent(nonEmpty, store.Retrieve(nonEmpty.SnapshotIdentity).Snapshot!);
    }

    [Fact]
    public void CatalogRegistersEquivalentEvidenceAndProvidesExactHitOrMiss()
    {
        using var database = new DatasetDatabase();
        var catalog = new SqliteDatasetCatalog(database.Factory);
        var snapshot = Snapshot('c');
        var entry = new DatasetCatalogEntry(snapshot);

        Assert.Equal(DatasetCatalogRegistrationOutcome.NewlyRegistered, catalog.Register(entry).Outcome);
        Assert.Equal(DatasetCatalogRegistrationOutcome.EquivalentExisting, catalog.Register(entry).Outcome);
        var found = catalog.Find(snapshot.SnapshotIdentity).Entry!;
        Assert.Equal(entry.SnapshotIdentity, found.SnapshotIdentity);
        Assert.Equal(entry.Version, found.Version);
        Assert.Equal(entry.Definition, found.Definition);
        Assert.Equal(entry.Coverage, found.Coverage);
        Assert.Equal(entry.Provenance, found.Provenance);
        Assert.Equal(entry.Lineage.SourceObservations, found.Lineage.SourceObservations);
        Assert.True(catalog.Find(new DatasetSnapshotIdentity(Fingerprint('d'))).IsNotFound);
    }

    [Fact]
    public void StoreRollsBackDescriptorWhenMembershipWriteFailsAndMapsInvalidEvidence()
    {
        using var database = new DatasetDatabase();
        var store = new SqliteDatasetSnapshotStore(database.Factory);
        var snapshot = Snapshot('e');
        using (var connection = database.Factory.OpenConnection())
        {
            Execute(connection, "CREATE TRIGGER reject_dataset_membership BEFORE INSERT ON dataset_snapshot_observations BEGIN SELECT RAISE(ABORT, 'test failure'); END;");
        }

        Assert.Equal(DatasetStoreFailure.InvalidData, store.Store(snapshot).Failure);
        Assert.True(store.Retrieve(snapshot.SnapshotIdentity).IsNotFound);
        Assert.Equal(0L, Count(database.Factory, "SELECT COUNT(*) FROM dataset_snapshots;"));
    }

    [Fact]
    public void RetrieveMapsMalformedPersistedDatasetEvidenceToInvalidDataWithoutRepair()
    {
        using var database = new DatasetDatabase();
        var store = new SqliteDatasetSnapshotStore(database.Factory);
        var snapshot = Snapshot('f');
        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, store.Store(snapshot).Outcome);
        using (var connection = database.Factory.OpenConnection())
        {
            Execute(connection, "PRAGMA ignore_check_constraints = ON;");
            Execute(connection, "UPDATE dataset_snapshot_observations SET price_text = 'malformed';");
        }

        var result = store.Retrieve(snapshot.SnapshotIdentity);

        Assert.Equal(DatasetStoreFailure.InvalidData, result.Failure);
        Assert.Equal(1L, Count(database.Factory, "SELECT COUNT(*) FROM dataset_snapshot_observations;"));
    }

    [Fact]
    public void VersionOneDatabaseUpgradesToVersionTwoWithoutHistoricalDataLoss()
    {
        using var database = new DatasetDatabase(createDirectory: true);
        using (var connection = new SqliteConnection($"Data Source={database.Path}"))
        {
            connection.Open();
            Execute(connection, SqliteHistoricalObservationSchema.CreateTableStatement);
            Execute(connection, "INSERT INTO historical_observations VALUES ('AAPL', 638396748000000000, -180, '123.45');");
            Execute(connection, "PRAGMA user_version = 1;");
        }

        using var upgraded = database.Factory.OpenConnection();
        Assert.Equal(4L, Scalar<long>(upgraded, "PRAGMA user_version;"));
        Assert.Equal(1L, Scalar<long>(upgraded, "SELECT COUNT(*) FROM historical_observations;"));
        Assert.Equal(1L, Scalar<long>(upgraded, "SELECT COUNT(*) FROM sqlite_schema WHERE name = 'dataset_snapshots';"));
        Assert.Equal(1L, Scalar<long>(upgraded, "SELECT COUNT(*) FROM sqlite_schema WHERE name = 'experiment_results';"));
    }

    [Fact]
    public void DatasetRegistrationsResolveWithoutCreatingDatabaseOrProviderCall()
    {
        using var database = new DatasetDatabase(createDirectory: false);
        var services = new ServiceCollection();
        services.AddInfrastructure(new TwelveDataConfiguration("offline-placeholder"), database.Configuration);

        _ = Assert.Single(services, static x => x.ServiceType == typeof(IDatasetSnapshotStore));
        _ = Assert.Single(services, static x => x.ServiceType == typeof(IDatasetCatalog));
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        Assert.IsType<SqliteDatasetSnapshotStore>(provider.GetRequiredService<IDatasetSnapshotStore>());
        Assert.IsType<SqliteDatasetCatalog>(provider.GetRequiredService<IDatasetCatalog>());
        _ = provider.GetRequiredService<IHistoricalObservationStore>();
        _ = provider.GetRequiredService<IObservationSource>();
        Assert.False(File.Exists(database.Path));
    }

    private static DatasetSnapshotCandidate Snapshot(char identityCharacter, bool empty = false, decimal price = 12.34567890123456789012345678m)
    {
        var from = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));
        var to = from.AddDays(3);
        var definition = new DatasetDefinition(" AAPL ", from, to);
        var definitionIdentity = new DatasetDefinitionIdentity(Fingerprint('1'));
        var researchIdentity = new ResearchDatasetIdentity(Fingerprint('2'));
        var sourceIdentity = new SourceStateIdentity(Fingerprint('3'));
        var snapshotIdentity = new DatasetSnapshotIdentity(Fingerprint(identityCharacter));
        var observations = empty ? Array.Empty<PriceObservation>() : [new PriceObservation(from.AddDays(1), price)];
        var coverage = new DatasetCoverage(from, to, observations.Length, observations.FirstOrDefault()?.Instant, observations.LastOrDefault()?.Instant);
        var version = new DatasetVersion(snapshotIdentity);
        var provenance = new DatasetProvenance(definition, definitionIdentity, researchIdentity, sourceIdentity, snapshotIdentity, version, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, observations.Length);
        var lineage = new DatasetLineage(definitionIdentity, sourceIdentity, observations);
        return new DatasetSnapshotCandidate(definition, definitionIdentity, researchIdentity, sourceIdentity, snapshotIdentity, version, observations, coverage, provenance, lineage);
    }

    private static string Fingerprint(char character) => new(character, 64);

    private static void AssertSnapshotEquivalent(DatasetSnapshotCandidate expected, DatasetSnapshotCandidate actual)
    {
        Assert.Equal(expected.Definition, actual.Definition);
        Assert.Equal(expected.DefinitionIdentity, actual.DefinitionIdentity);
        Assert.Equal(expected.ResearchDatasetIdentity, actual.ResearchDatasetIdentity);
        Assert.Equal(expected.SourceStateIdentity, actual.SourceStateIdentity);
        Assert.Equal(expected.SnapshotIdentity, actual.SnapshotIdentity);
        Assert.Equal(expected.Version, actual.Version);
        Assert.Equal(expected.Coverage, actual.Coverage);
        Assert.Equal(expected.Provenance, actual.Provenance);
        Assert.Equal(expected.Observations, actual.Observations);
        Assert.Equal(expected.Lineage.SourceObservations, actual.Lineage.SourceObservations);
    }

    private static long Count(SqliteConnectionFactory factory, string sql)
    {
        using var connection = factory.OpenConnection();
        return Scalar<long>(connection, sql);
    }

    private static void Execute(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }

    private static T Scalar<T>(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return (T)command.ExecuteScalar()!;
    }

    private sealed class DatasetDatabase : IDisposable
    {
        private readonly string directory;

        public DatasetDatabase(bool createDirectory = true)
        {
            directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"aiq-wp14-dataset-{Guid.NewGuid():N}");
            if (createDirectory) Directory.CreateDirectory(directory);
            Path = System.IO.Path.Combine(directory, "dataset.sqlite");
            Configuration = new SqliteStorageConfiguration(Path);
            Factory = new SqliteConnectionFactory(Configuration);
        }

        public string Path { get; }
        public SqliteStorageConfiguration Configuration { get; }
        public SqliteConnectionFactory Factory { get; }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
        }
    }
}
