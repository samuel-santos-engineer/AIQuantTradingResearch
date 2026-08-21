using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Features;

public sealed record FeatureGenerationRequest
{
    public FeatureGenerationRequest(
        FeatureDefinition definition,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion snapshotVersion)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(snapshotIdentity);
        ArgumentNullException.ThrowIfNull(snapshotVersion);

        if (snapshotVersion.SnapshotIdentity != snapshotIdentity)
        {
            throw new ArgumentException(
                "The requested dataset version must represent the requested snapshot identity.",
                nameof(snapshotVersion));
        }

        Definition = definition;
        SnapshotIdentity = snapshotIdentity;
        SnapshotVersion = snapshotVersion;
    }

    public FeatureDefinition Definition { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion SnapshotVersion { get; }
}

public enum FeatureGenerationFailure
{
    InvalidRequest,
    UnsupportedDefinition,
    SnapshotNotFound,
    DependencyUnavailable,
    InvalidSnapshotEvidence,
    InvalidNumericInput,
    IntegrityConflict,
}

public sealed record FeatureGenerationResult
{
    private FeatureGenerationResult(FeatureSet? featureSet, FeatureGenerationFailure? failure)
    {
        FeatureSet = featureSet;
        Failure = failure;
    }

    public bool IsSuccess => FeatureSet is not null;

    public FeatureSet? FeatureSet { get; }

    public FeatureGenerationFailure? Failure { get; }

    public static FeatureGenerationResult Generated(FeatureSet featureSet)
    {
        ArgumentNullException.ThrowIfNull(featureSet);
        return new FeatureGenerationResult(featureSet, null);
    }

    public static FeatureGenerationResult Failed(FeatureGenerationFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown feature-generation failure.");
        }

        return new FeatureGenerationResult(null, failure);
    }
}

public interface IFeatureGenerationUseCase
{
    FeatureGenerationResult Execute(FeatureGenerationRequest request);
}

public interface IFeatureComputer
{
    FeatureSet Compute(FeatureGenerationRequest request, DatasetSnapshotCandidate snapshot);
}
