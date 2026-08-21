using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Features;

internal sealed class FeatureGenerationUseCase : IFeatureGenerationUseCase
{
    private readonly IDatasetSnapshotStore snapshotStore;
    private readonly IFeatureGenerationValidator validator;
    private readonly IFeatureComputer computer;

    public FeatureGenerationUseCase(IDatasetSnapshotStore snapshotStore, IFeatureGenerationValidator validator, IFeatureComputer computer)
    {
        ArgumentNullException.ThrowIfNull(snapshotStore);
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(computer);
        this.snapshotStore = snapshotStore;
        this.validator = validator;
        this.computer = computer;
    }

    public FeatureGenerationResult Execute(FeatureGenerationRequest request)
    {
        var requestFailure = validator.ValidateRequest(request);
        if (requestFailure is not null)
        {
            return FeatureGenerationResult.Failed(requestFailure.Value);
        }

        var retrieval = snapshotStore.Retrieve(request.SnapshotIdentity)
            ?? throw new InvalidOperationException("The dataset snapshot store returned no result.");
        if (!retrieval.IsFound)
        {
            if (retrieval.IsNotFound)
            {
                return FeatureGenerationResult.Failed(FeatureGenerationFailure.SnapshotNotFound);
            }

            return retrieval.Failure switch
            {
                DatasetStoreFailure.Unavailable => FeatureGenerationResult.Failed(FeatureGenerationFailure.DependencyUnavailable),
                DatasetStoreFailure.InvalidData => FeatureGenerationResult.Failed(FeatureGenerationFailure.IntegrityConflict),
                _ => throw new InvalidOperationException("The dataset snapshot store returned an unknown failure."),
            };
        }

        var snapshot = retrieval.Snapshot
            ?? throw new InvalidOperationException("A found dataset snapshot result contained no snapshot.");
        var snapshotFailure = validator.ValidateSnapshot(request, snapshot);
        return snapshotFailure is null
            ? FeatureGenerationResult.Generated(computer.Compute(request, snapshot))
            : FeatureGenerationResult.Failed(snapshotFailure.Value);
    }
}
