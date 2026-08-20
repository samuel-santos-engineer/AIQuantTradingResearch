using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteDatasetCatalog : IDatasetCatalog
{
    private readonly SqliteDatasetSnapshotStore snapshotStore;

    public SqliteDatasetCatalog(ISqliteConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        snapshotStore = new SqliteDatasetSnapshotStore(connectionFactory);
    }

    public DatasetCatalogRegistrationResult Register(DatasetCatalogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var result = snapshotStore.Store(ToSnapshotCandidate(entry));

        if (!result.HasOutcome)
        {
            return DatasetCatalogRegistrationResult.Failed(result.Failure!.Value);
        }

        return DatasetCatalogRegistrationResult.Completed(result.Outcome!.Value switch
        {
            DatasetSnapshotStoreOutcome.NewlyAccepted => DatasetCatalogRegistrationOutcome.NewlyRegistered,
            DatasetSnapshotStoreOutcome.EquivalentExisting => DatasetCatalogRegistrationOutcome.EquivalentExisting,
            DatasetSnapshotStoreOutcome.IntegrityConflict => DatasetCatalogRegistrationOutcome.IntegrityConflict,
            _ => throw new InvalidOperationException("The snapshot-store outcome is incompatible with catalog registration."),
        });
    }

    public DatasetCatalogLookupResult Find(DatasetSnapshotIdentity snapshotIdentity)
    {
        ArgumentNullException.ThrowIfNull(snapshotIdentity);

        var result = snapshotStore.Retrieve(snapshotIdentity);

        if (result.IsFound)
        {
            return DatasetCatalogLookupResult.Found(new DatasetCatalogEntry(result.Snapshot!));
        }

        return result.IsNotFound
            ? DatasetCatalogLookupResult.NotFound()
            : DatasetCatalogLookupResult.Failed(result.Failure!.Value);
    }

    private static DatasetSnapshotCandidate ToSnapshotCandidate(DatasetCatalogEntry entry) =>
        new(
            entry.Definition,
            entry.DefinitionIdentity,
            entry.ResearchDatasetIdentity,
            entry.SourceStateIdentity,
            entry.SnapshotIdentity,
            entry.Version,
            entry.Lineage.SourceObservations,
            entry.Coverage,
            entry.Provenance,
            entry.Lineage);
}
