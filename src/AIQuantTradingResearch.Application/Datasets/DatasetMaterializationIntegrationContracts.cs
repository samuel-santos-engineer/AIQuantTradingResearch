namespace AIQuantTradingResearch.Application.Datasets;

public enum DatasetMaterializationIntegrationOutcome
{
    NewlyAccepted,
    EquivalentExisting,
}

public sealed record DatasetMaterializationIntegrationResult
{
    private DatasetMaterializationIntegrationResult(
        DatasetSnapshotCandidate? snapshot,
        DatasetMaterializationIntegrationOutcome? outcome,
        DatasetMaterializationFailure? failure)
    {
        Snapshot = snapshot;
        Outcome = outcome;
        Failure = failure;
    }

    public bool IsSuccess => Snapshot is not null;

    public DatasetSnapshotCandidate? Snapshot { get; }

    public DatasetMaterializationIntegrationOutcome? Outcome { get; }

    public DatasetMaterializationFailure? Failure { get; }

    public static DatasetMaterializationIntegrationResult Completed(
        DatasetSnapshotCandidate snapshot,
        DatasetMaterializationIntegrationOutcome outcome)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        if (!Enum.IsDefined(outcome))
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown dataset integration outcome.");
        }

        return new DatasetMaterializationIntegrationResult(snapshot, outcome, null);
    }

    public static DatasetMaterializationIntegrationResult Failed(DatasetMaterializationFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown dataset materialization failure.");
        }

        return new DatasetMaterializationIntegrationResult(null, null, failure);
    }
}

public interface IDatasetMaterializationIntegrationUseCase
{
    DatasetMaterializationIntegrationResult Execute(DatasetDefinition definition);
}
