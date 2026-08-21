using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

public enum PipelineSuccessDisposition
{
    NewlyAccepted,
    EquivalentExisting,
}

public enum PipelineStageOutcome
{
    NewlyAccepted,
    EquivalentExisting,
    Failed,
}

public enum PipelineFailureCategory
{
    InvalidInput,
    IntegrityConflict,
    DependencyUnavailable,
    InvalidEvidence,
}

public sealed record PipelineStageEvidence
{
    public PipelineStageEvidence(
        ResearchPipelineStage stage,
        PipelineStageOutcome outcome,
        IEnumerable<string>? inputIdentityReferences = null,
        IEnumerable<string>? outputIdentityReferences = null,
        PipelineFailureCategory? failureCategory = null)
    {
        if (!Enum.IsDefined(stage))
        {
            throw new ArgumentOutOfRangeException(nameof(stage), stage, "Unknown pipeline stage.");
        }

        if (!Enum.IsDefined(outcome))
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown pipeline stage outcome.");
        }

        if (outcome == PipelineStageOutcome.Failed && failureCategory is null)
        {
            throw new ArgumentException("A failed stage requires a failure category.", nameof(failureCategory));
        }

        if (outcome != PipelineStageOutcome.Failed && failureCategory is not null)
        {
            throw new ArgumentException("A completed stage cannot have a failure category.", nameof(failureCategory));
        }

        if (failureCategory is not null && !Enum.IsDefined(failureCategory.Value))
        {
            throw new ArgumentOutOfRangeException(nameof(failureCategory), failureCategory, "Unknown pipeline failure category.");
        }

        Stage = stage;
        Outcome = outcome;
        InputIdentityReferences = Snapshot(inputIdentityReferences, nameof(inputIdentityReferences));
        OutputIdentityReferences = Snapshot(outputIdentityReferences, nameof(outputIdentityReferences));
        FailureCategory = failureCategory;
    }

    public ResearchPipelineStage Stage { get; }

    public PipelineStageOutcome Outcome { get; }

    public IReadOnlyList<string> InputIdentityReferences { get; }

    public IReadOnlyList<string> OutputIdentityReferences { get; }

    public PipelineFailureCategory? FailureCategory { get; }

    private static System.Collections.ObjectModel.ReadOnlyCollection<string> Snapshot(
        IEnumerable<string>? values,
        string parameterName)
    {
        var snapshot = values?.ToArray() ?? [];

        if (snapshot.Any(string.IsNullOrWhiteSpace))
        {
            throw new ArgumentException("Identity references cannot contain null or whitespace values.", parameterName);
        }

        return Array.AsReadOnly(snapshot);
    }
}

public sealed record PipelineProvenance
{
    public PipelineProvenance(
        PipelineDefinitionIdentity definitionIdentity,
        PipelineExecutionIdentity executionIdentity,
        DatasetDefinitionIdentity datasetDefinitionIdentity,
        SourceStateIdentity? sourceStateIdentity,
        IEnumerable<PipelineStageEvidence> stages,
        DatasetSnapshotIdentity? snapshotIdentity = null,
        DatasetVersion? datasetVersion = null)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(executionIdentity);
        ArgumentNullException.ThrowIfNull(datasetDefinitionIdentity);
        ArgumentNullException.ThrowIfNull(stages);

        var stageSnapshot = stages.ToArray();

        if (stageSnapshot.Any(static stage => stage is null))
        {
            throw new ArgumentException("Pipeline stages cannot contain null values.", nameof(stages));
        }

        ValidateOrderedStages(stageSnapshot);
        ValidateEstablishedIdentities(stageSnapshot, sourceStateIdentity, snapshotIdentity);

        if ((snapshotIdentity is null) != (datasetVersion is null))
        {
            throw new ArgumentException("Snapshot identity and dataset version must be established together.");
        }

        if (datasetVersion is not null && datasetVersion.SnapshotIdentity != snapshotIdentity)
        {
            throw new ArgumentException("Dataset version must represent the same snapshot identity.", nameof(datasetVersion));
        }

        DefinitionIdentity = definitionIdentity;
        ExecutionIdentity = executionIdentity;
        DatasetDefinitionIdentity = datasetDefinitionIdentity;
        SourceStateIdentity = sourceStateIdentity;
        Stages = Array.AsReadOnly(stageSnapshot);
        SnapshotIdentity = snapshotIdentity;
        DatasetVersion = datasetVersion;
    }

    public PipelineDefinitionIdentity DefinitionIdentity { get; }

    public PipelineExecutionIdentity ExecutionIdentity { get; }

    public DatasetDefinitionIdentity DatasetDefinitionIdentity { get; }

    public SourceStateIdentity? SourceStateIdentity { get; }

    public IReadOnlyList<PipelineStageEvidence> Stages { get; }

    public DatasetSnapshotIdentity? SnapshotIdentity { get; }

    public DatasetVersion? DatasetVersion { get; }

    internal static void ValidateOrderedStages(IReadOnlyList<PipelineStageEvidence> stages)
    {
        if (stages.Count == 0)
        {
            throw new ArgumentException("Pipeline evidence requires at least one stage.", nameof(stages));
        }

        var failed = false;

        for (var index = 0; index < stages.Count; index++)
        {
            if ((int)stages[index].Stage != index + 1)
            {
                throw new ArgumentException("Pipeline stages must follow the fixed semantic order.", nameof(stages));
            }

            if (failed)
            {
                throw new ArgumentException("No stage evidence may follow a failed stage.", nameof(stages));
            }

            failed = stages[index].Outcome == PipelineStageOutcome.Failed;
        }
    }

    private static void ValidateEstablishedIdentities(
        IReadOnlyList<PipelineStageEvidence> stages,
        SourceStateIdentity? sourceStateIdentity,
        DatasetSnapshotIdentity? snapshotIdentity)
    {
        PipelineStageEvidence? materialization = stages.FirstOrDefault(
            static stage => stage.Stage == ResearchPipelineStage.DatasetMaterialization);
        bool sourceStateEstablished = materialization is not null
            && materialization.Outcome != PipelineStageOutcome.Failed;

        if (sourceStateEstablished != (sourceStateIdentity is not null))
        {
            throw new ArgumentException(
                "Source-state identity must be present exactly when dataset materialization established it.",
                nameof(sourceStateIdentity));
        }

        PipelineStageEvidence? persistence = stages.FirstOrDefault(
            static stage => stage.Stage == ResearchPipelineStage.SnapshotPersistence);
        bool snapshotEstablished = persistence is not null
            && persistence.Outcome != PipelineStageOutcome.Failed;

        if (snapshotEstablished != (snapshotIdentity is not null))
        {
            throw new ArgumentException(
                "Snapshot identity must be present exactly when snapshot persistence established it.",
                nameof(snapshotIdentity));
        }
    }
}
