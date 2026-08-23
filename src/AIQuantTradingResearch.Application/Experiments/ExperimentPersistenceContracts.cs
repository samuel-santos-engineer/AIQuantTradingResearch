using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

public enum DurableExperimentAcceptanceDisposition
{
    NewlyAccepted,
    EquivalentExisting,
}

public enum DurableExperimentEvidenceFailure
{
    InvalidRequest,
    NotFound,
    DependencyUnavailable,
    InvalidEvidence,
    IntegrityConflict,
}

public sealed record DurableExperimentProvenance
{
    public DurableExperimentProvenance(
        ExperimentDefinitionIdentity definitionIdentity,
        FeatureSetIdentity featureSetIdentity,
        FeatureDefinitionIdentity featureDefinitionIdentity,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion snapshotVersion,
        DatasetDefinitionIdentity datasetDefinitionIdentity,
        ResearchDatasetIdentity researchDatasetIdentity,
        SourceStateIdentity sourceStateIdentity,
        DatasetSourceAuthority sourceAuthority,
        int datasetObservationCount)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(featureSetIdentity);
        ArgumentNullException.ThrowIfNull(featureDefinitionIdentity);
        ArgumentNullException.ThrowIfNull(snapshotIdentity);
        ArgumentNullException.ThrowIfNull(snapshotVersion);
        ArgumentNullException.ThrowIfNull(datasetDefinitionIdentity);
        ArgumentNullException.ThrowIfNull(researchDatasetIdentity);
        ArgumentNullException.ThrowIfNull(sourceStateIdentity);

        if (snapshotVersion.SnapshotIdentity != snapshotIdentity)
        {
            throw new ArgumentException(
                "Dataset version must represent the durable evidence snapshot identity.",
                nameof(snapshotVersion));
        }

        if (!Enum.IsDefined(sourceAuthority))
        {
            throw new ArgumentOutOfRangeException(
                nameof(sourceAuthority),
                sourceAuthority,
                "Unknown dataset source authority.");
        }

        if (datasetObservationCount < 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(datasetObservationCount),
                datasetObservationCount,
                "Dataset observation count cannot be negative.");
        }

        DefinitionIdentity = definitionIdentity;
        FeatureSetIdentity = featureSetIdentity;
        FeatureDefinitionIdentity = featureDefinitionIdentity;
        SnapshotIdentity = snapshotIdentity;
        SnapshotVersion = snapshotVersion;
        DatasetDefinitionIdentity = datasetDefinitionIdentity;
        ResearchDatasetIdentity = researchDatasetIdentity;
        SourceStateIdentity = sourceStateIdentity;
        SourceAuthority = sourceAuthority;
        DatasetObservationCount = datasetObservationCount;
    }

    public ExperimentDefinitionIdentity DefinitionIdentity { get; }

    public FeatureSetIdentity FeatureSetIdentity { get; }

    public FeatureDefinitionIdentity FeatureDefinitionIdentity { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion SnapshotVersion { get; }

    public DatasetDefinitionIdentity DatasetDefinitionIdentity { get; }

    public ResearchDatasetIdentity ResearchDatasetIdentity { get; }

    public SourceStateIdentity SourceStateIdentity { get; }

    public DatasetSourceAuthority SourceAuthority { get; }

    public int DatasetObservationCount { get; }
}

public sealed record DurableExperimentLineage
{
    public DurableExperimentLineage(
        ExperimentDefinitionIdentity definitionIdentity,
        FeatureDefinitionIdentity featureDefinitionIdentity,
        DatasetDefinitionIdentity datasetDefinitionIdentity,
        SourceStateIdentity sourceStateIdentity)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(featureDefinitionIdentity);
        ArgumentNullException.ThrowIfNull(datasetDefinitionIdentity);
        ArgumentNullException.ThrowIfNull(sourceStateIdentity);

        DefinitionIdentity = definitionIdentity;
        FeatureDefinitionIdentity = featureDefinitionIdentity;
        DatasetDefinitionIdentity = datasetDefinitionIdentity;
        SourceStateIdentity = sourceStateIdentity;
    }

    public ExperimentDefinitionIdentity DefinitionIdentity { get; }

    public FeatureDefinitionIdentity FeatureDefinitionIdentity { get; }

    public DatasetDefinitionIdentity DatasetDefinitionIdentity { get; }

    public SourceStateIdentity SourceStateIdentity { get; }
}

public sealed record DurableExperimentEvidence
{
    public DurableExperimentEvidence(
        ExperimentDefinition definition,
        ExperimentDefinitionIdentity definitionIdentity,
        ExperimentResultIdentity identity,
        ExperimentSummaryEvidence summary,
        DurableExperimentProvenance provenance,
        DurableExperimentLineage lineage)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(identity);
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(provenance);
        ArgumentNullException.ThrowIfNull(lineage);

        var expectedDefinitionIdentity = ExperimentIdentityComputer.ComputeDefinitionIdentity(definition);
        var expectedResultIdentity = ExperimentIdentityComputer.ComputeResultIdentity(
            definitionIdentity,
            provenance.FeatureSetIdentity,
            summary);

        if (definition != ExperimentDefinition.SimpleReturnDescriptiveSummaryV1
            || definitionIdentity != expectedDefinitionIdentity
            || identity != expectedResultIdentity
            || provenance.DefinitionIdentity != definitionIdentity
            || lineage.DefinitionIdentity != definitionIdentity
            || provenance.FeatureDefinitionIdentity != lineage.FeatureDefinitionIdentity
            || provenance.DatasetDefinitionIdentity != lineage.DatasetDefinitionIdentity
            || provenance.SourceStateIdentity != lineage.SourceStateIdentity)
        {
            throw new ArgumentException(
                "Durable experiment evidence must retain coherent identity, summary, provenance, and lineage facts.");
        }

        Definition = definition;
        DefinitionIdentity = definitionIdentity;
        Identity = identity;
        Summary = summary;
        Provenance = provenance;
        Lineage = lineage;
    }

    public ExperimentDefinition Definition { get; }

    public ExperimentDefinitionIdentity DefinitionIdentity { get; }

    public ExperimentResultIdentity Identity { get; }

    public ExperimentSummaryEvidence Summary { get; }

    public DurableExperimentProvenance Provenance { get; }

    public DurableExperimentLineage Lineage { get; }
}

public sealed record DurableExperimentAcceptanceRequest
{
    public DurableExperimentAcceptanceRequest(DurableExperimentEvidence evidence)
    {
        ArgumentNullException.ThrowIfNull(evidence);
        Evidence = evidence;
    }

    public DurableExperimentEvidence Evidence { get; }
}

public sealed record DurableExperimentRetrievalRequest
{
    public DurableExperimentRetrievalRequest(ExperimentResultIdentity identity)
    {
        ArgumentNullException.ThrowIfNull(identity);
        Identity = identity;
    }

    public ExperimentResultIdentity Identity { get; }
}

public sealed record DurableExperimentAcceptanceResult
{
    private DurableExperimentAcceptanceResult(
        DurableExperimentAcceptanceDisposition? disposition,
        DurableExperimentEvidenceFailure? failure)
    {
        Disposition = disposition;
        Failure = failure;
    }

    public bool IsSuccess => Disposition is not null;

    public DurableExperimentAcceptanceDisposition? Disposition { get; }

    public DurableExperimentEvidenceFailure? Failure { get; }

    public static DurableExperimentAcceptanceResult Accepted(
        DurableExperimentAcceptanceDisposition disposition)
    {
        if (!Enum.IsDefined(disposition))
        {
            throw new ArgumentOutOfRangeException(
                nameof(disposition),
                disposition,
                "Unknown durable-experiment acceptance disposition.");
        }

        return new DurableExperimentAcceptanceResult(disposition, null);
    }

    public static DurableExperimentAcceptanceResult Failed(DurableExperimentEvidenceFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(
                nameof(failure),
                failure,
                "Unknown durable-experiment evidence failure.");
        }

        return new DurableExperimentAcceptanceResult(null, failure);
    }
}

public sealed record DurableExperimentRetrievalResult
{
    private DurableExperimentRetrievalResult(
        DurableExperimentEvidence? evidence,
        DurableExperimentEvidenceFailure? failure)
    {
        Evidence = evidence;
        Failure = failure;
    }

    public bool IsSuccess => Evidence is not null;

    public DurableExperimentEvidence? Evidence { get; }

    public DurableExperimentEvidenceFailure? Failure { get; }

    public static DurableExperimentRetrievalResult Found(DurableExperimentEvidence evidence)
    {
        ArgumentNullException.ThrowIfNull(evidence);
        return new DurableExperimentRetrievalResult(evidence, null);
    }

    public static DurableExperimentRetrievalResult Failed(DurableExperimentEvidenceFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(
                nameof(failure),
                failure,
                "Unknown durable-experiment evidence failure.");
        }

        return new DurableExperimentRetrievalResult(null, failure);
    }
}

public interface IDurableExperimentEvidenceStore
{
    DurableExperimentAcceptanceResult Accept(DurableExperimentAcceptanceRequest request);

    DurableExperimentRetrievalResult Retrieve(DurableExperimentRetrievalRequest request);
}

public sealed record DurableExperimentUseCaseResult
{
    private DurableExperimentUseCaseResult(
        DurableExperimentEvidence? evidence,
        DurableExperimentAcceptanceDisposition? disposition,
        DurableExperimentEvidenceFailure? failure)
    {
        Evidence = evidence;
        Disposition = disposition;
        Failure = failure;
    }

    public bool IsSuccess => Evidence is not null;

    public DurableExperimentEvidence? Evidence { get; }

    public DurableExperimentAcceptanceDisposition? Disposition { get; }

    public DurableExperimentEvidenceFailure? Failure { get; }

    public static DurableExperimentUseCaseResult Accepted(
        DurableExperimentEvidence evidence,
        DurableExperimentAcceptanceDisposition disposition)
    {
        ArgumentNullException.ThrowIfNull(evidence);

        if (!Enum.IsDefined(disposition))
        {
            throw new ArgumentOutOfRangeException(
                nameof(disposition),
                disposition,
                "Unknown durable-experiment acceptance disposition.");
        }

        return new DurableExperimentUseCaseResult(evidence, disposition, null);
    }

    public static DurableExperimentUseCaseResult Failed(DurableExperimentEvidenceFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(
                nameof(failure),
                failure,
                "Unknown durable-experiment evidence failure.");
        }

        return new DurableExperimentUseCaseResult(null, null, failure);
    }
}

public interface IDurableExperimentUseCase
{
    DurableExperimentUseCaseResult Execute(ExperimentGenerationRequest request);
}
