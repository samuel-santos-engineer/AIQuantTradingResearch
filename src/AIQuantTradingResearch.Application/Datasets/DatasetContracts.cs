namespace AIQuantTradingResearch.Application.Datasets;

public enum DatasetMaterializationFailure
{
    InvalidDefinition,
    SourceHistoryUnavailable,
    IntegrityConflict,
    SnapshotStoreUnavailable,
}

public sealed record DatasetMaterializationResult
{
    private DatasetMaterializationResult(
        DatasetSnapshotCandidate? snapshot,
        DatasetMaterializationFailure? failure)
    {
        Snapshot = snapshot;
        Failure = failure;
    }

    public bool IsSuccess => Snapshot is not null;

    public DatasetSnapshotCandidate? Snapshot { get; }

    public DatasetMaterializationFailure? Failure { get; }

    public static DatasetMaterializationResult Materialized(DatasetSnapshotCandidate snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new DatasetMaterializationResult(snapshot, null);
    }

    public static DatasetMaterializationResult Failed(DatasetMaterializationFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown dataset materialization failure.");
        }

        return new DatasetMaterializationResult(null, failure);
    }
}

public enum DatasetSnapshotStoreOutcome
{
    NewlyAccepted,
    EquivalentExisting,
    IntegrityConflict,
}

public enum DatasetStoreFailure
{
    Unavailable,
    InvalidData,
}

public sealed record DatasetSnapshotStoreResult
{
    private DatasetSnapshotStoreResult(DatasetSnapshotStoreOutcome? outcome, DatasetStoreFailure? failure)
    {
        Outcome = outcome;
        Failure = failure;
    }

    public bool HasOutcome => Outcome is not null;

    public DatasetSnapshotStoreOutcome? Outcome { get; }

    public DatasetStoreFailure? Failure { get; }

    public static DatasetSnapshotStoreResult Completed(DatasetSnapshotStoreOutcome outcome)
    {
        if (!Enum.IsDefined(outcome))
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown snapshot-store outcome.");
        }

        return new DatasetSnapshotStoreResult(outcome, null);
    }

    public static DatasetSnapshotStoreResult Failed(DatasetStoreFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown dataset-store failure.");
        }

        return new DatasetSnapshotStoreResult(null, failure);
    }
}

public sealed record DatasetSnapshotRetrievalResult
{
    private DatasetSnapshotRetrievalResult(DatasetSnapshotCandidate? snapshot, DatasetStoreFailure? failure)
    {
        Snapshot = snapshot;
        Failure = failure;
    }

    public bool IsFound => Snapshot is not null;

    public bool IsNotFound => Snapshot is null && Failure is null;

    public DatasetSnapshotCandidate? Snapshot { get; }

    public DatasetStoreFailure? Failure { get; }

    public static DatasetSnapshotRetrievalResult Found(DatasetSnapshotCandidate snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);
        return new DatasetSnapshotRetrievalResult(snapshot, null);
    }

    public static DatasetSnapshotRetrievalResult NotFound() => new(null, null);

    public static DatasetSnapshotRetrievalResult Failed(DatasetStoreFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown dataset-store failure.");
        }

        return new DatasetSnapshotRetrievalResult(null, failure);
    }
}

public interface IDatasetSnapshotStore
{
    DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot);

    DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity);
}

public enum DatasetCatalogRegistrationOutcome
{
    NewlyRegistered,
    EquivalentExisting,
    IntegrityConflict,
}

public sealed record DatasetCatalogRegistrationResult
{
    private DatasetCatalogRegistrationResult(
        DatasetCatalogRegistrationOutcome? outcome,
        DatasetStoreFailure? failure)
    {
        Outcome = outcome;
        Failure = failure;
    }

    public bool HasOutcome => Outcome is not null;

    public DatasetCatalogRegistrationOutcome? Outcome { get; }

    public DatasetStoreFailure? Failure { get; }

    public static DatasetCatalogRegistrationResult Completed(DatasetCatalogRegistrationOutcome outcome)
    {
        if (!Enum.IsDefined(outcome))
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown catalog-registration outcome.");
        }

        return new DatasetCatalogRegistrationResult(outcome, null);
    }

    public static DatasetCatalogRegistrationResult Failed(DatasetStoreFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown dataset-store failure.");
        }

        return new DatasetCatalogRegistrationResult(null, failure);
    }
}

public sealed record DatasetCatalogLookupResult
{
    private DatasetCatalogLookupResult(DatasetCatalogEntry? entry, DatasetStoreFailure? failure)
    {
        Entry = entry;
        Failure = failure;
    }

    public bool IsFound => Entry is not null;

    public bool IsNotFound => Entry is null && Failure is null;

    public DatasetCatalogEntry? Entry { get; }

    public DatasetStoreFailure? Failure { get; }

    public static DatasetCatalogLookupResult Found(DatasetCatalogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);
        return new DatasetCatalogLookupResult(entry, null);
    }

    public static DatasetCatalogLookupResult NotFound() => new(null, null);

    public static DatasetCatalogLookupResult Failed(DatasetStoreFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown dataset-store failure.");
        }

        return new DatasetCatalogLookupResult(null, failure);
    }
}

public interface IDatasetCatalog
{
    DatasetCatalogRegistrationResult Register(DatasetCatalogEntry entry);

    DatasetCatalogLookupResult Find(DatasetSnapshotIdentity snapshotIdentity);
}
