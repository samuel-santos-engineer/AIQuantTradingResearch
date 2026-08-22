using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

public sealed record ExperimentGenerationRequest
{
    public ExperimentGenerationRequest(
        ExperimentDefinition definition,
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

    public ExperimentDefinition Definition { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion SnapshotVersion { get; }
}

public enum ExperimentGenerationFailure
{
    InvalidRequest,
    UnsupportedDefinition,
    FeatureSetNotFound,
    DependencyUnavailable,
    InvalidFeatureEvidence,
    InvalidNumericEvidence,
    IntegrityConflict,
}

public sealed record ExperimentGenerationResult
{
    private ExperimentGenerationResult(
        ExperimentResult? experiment,
        ExperimentGenerationFailure? failure)
    {
        Experiment = experiment;
        Failure = failure;
    }

    public bool IsSuccess => Experiment is not null;

    public ExperimentResult? Experiment { get; }

    public ExperimentGenerationFailure? Failure { get; }

    public static ExperimentGenerationResult Generated(ExperimentResult experiment)
    {
        ArgumentNullException.ThrowIfNull(experiment);
        return new ExperimentGenerationResult(experiment, null);
    }

    public static ExperimentGenerationResult Failed(ExperimentGenerationFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(
                nameof(failure),
                failure,
                "Unknown experiment-generation failure.");
        }

        return new ExperimentGenerationResult(null, failure);
    }
}

public interface IExperimentGenerationUseCase
{
    ExperimentGenerationResult Execute(ExperimentGenerationRequest request);
}

public interface IExperimentSummaryComputer
{
    ExperimentSummaryEvidence Compute(FeatureSet featureSet);
}

public interface IExperimentGenerationValidator
{
    ExperimentGenerationFailure? ValidateRequest(ExperimentGenerationRequest request);

    ExperimentGenerationFailure? ValidateFeatureSet(FeatureSet featureSet);
}
