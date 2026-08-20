namespace AIQuantTradingResearch.Application.Datasets;

internal sealed class DatasetMaterializationIntegrationUseCase : IDatasetMaterializationIntegrationUseCase
{
    private readonly IMaterializeDatasetUseCase materializeDataset;
    private readonly IDatasetSnapshotStore snapshotStore;
    private readonly IDatasetCatalog datasetCatalog;

    public DatasetMaterializationIntegrationUseCase(
        IMaterializeDatasetUseCase materializeDataset,
        IDatasetSnapshotStore snapshotStore,
        IDatasetCatalog datasetCatalog)
    {
        ArgumentNullException.ThrowIfNull(materializeDataset);
        ArgumentNullException.ThrowIfNull(snapshotStore);
        ArgumentNullException.ThrowIfNull(datasetCatalog);

        this.materializeDataset = materializeDataset;
        this.snapshotStore = snapshotStore;
        this.datasetCatalog = datasetCatalog;
    }

    public DatasetMaterializationIntegrationResult Execute(DatasetDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        var materialization = materializeDataset.Execute(definition)
            ?? throw new InvalidOperationException("The dataset materialization use case returned no result.");

        if (!materialization.IsSuccess)
        {
            return DatasetMaterializationIntegrationResult.Failed(materialization.Failure!.Value);
        }

        var snapshot = materialization.Snapshot!;
        var persistence = snapshotStore.Store(snapshot)
            ?? throw new InvalidOperationException("The dataset snapshot store returned no result.");

        if (!persistence.HasOutcome)
        {
            return DatasetMaterializationIntegrationResult.Failed(MapFailure(persistence.Failure!.Value));
        }

        if (persistence.Outcome == DatasetSnapshotStoreOutcome.IntegrityConflict)
        {
            return DatasetMaterializationIntegrationResult.Failed(
                DatasetMaterializationFailure.IntegrityConflict);
        }

        var registration = datasetCatalog.Register(new DatasetCatalogEntry(snapshot))
            ?? throw new InvalidOperationException("The dataset catalog returned no registration result.");

        if (!registration.HasOutcome)
        {
            return DatasetMaterializationIntegrationResult.Failed(MapFailure(registration.Failure!.Value));
        }

        if (registration.Outcome == DatasetCatalogRegistrationOutcome.IntegrityConflict)
        {
            return DatasetMaterializationIntegrationResult.Failed(
                DatasetMaterializationFailure.IntegrityConflict);
        }

        return DatasetMaterializationIntegrationResult.Completed(
            snapshot,
            persistence.Outcome == DatasetSnapshotStoreOutcome.NewlyAccepted
                ? DatasetMaterializationIntegrationOutcome.NewlyAccepted
                : DatasetMaterializationIntegrationOutcome.EquivalentExisting);
    }

    private static DatasetMaterializationFailure MapFailure(DatasetStoreFailure failure) => failure switch
    {
        DatasetStoreFailure.Unavailable => DatasetMaterializationFailure.SnapshotStoreUnavailable,
        DatasetStoreFailure.InvalidData => DatasetMaterializationFailure.IntegrityConflict,
        _ => throw new InvalidOperationException("The dataset store returned an unknown failure."),
    };
}
