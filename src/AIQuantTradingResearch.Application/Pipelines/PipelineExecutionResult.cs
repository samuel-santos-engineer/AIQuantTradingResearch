using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

public sealed record PipelineExecutionResult
{
    private PipelineExecutionResult(
        PipelineProvenance provenance,
        PipelineSuccessDisposition? disposition,
        ResearchPipelineStage? failingStage,
        PipelineFailureCategory? failureCategory)
    {
        ArgumentNullException.ThrowIfNull(provenance);

        Provenance = provenance;
        Disposition = disposition;
        FailingStage = failingStage;
        FailureCategory = failureCategory;
    }

    public PipelineProvenance Provenance { get; }

    public PipelineDefinitionIdentity DefinitionIdentity => Provenance.DefinitionIdentity;

    public PipelineExecutionIdentity ExecutionIdentity => Provenance.ExecutionIdentity;

    public PipelineSuccessDisposition? Disposition { get; }

    public ResearchPipelineStage? FailingStage { get; }

    public PipelineFailureCategory? FailureCategory { get; }

    public bool IsSuccess => Disposition is not null;

    public DatasetSnapshotIdentity? SnapshotIdentity => Provenance.SnapshotIdentity;

    public DatasetVersion? DatasetVersion => Provenance.DatasetVersion;

    public static PipelineExecutionResult Succeeded(
        PipelineProvenance provenance,
        PipelineSuccessDisposition disposition)
    {
        ArgumentNullException.ThrowIfNull(provenance);

        if (!Enum.IsDefined(disposition))
        {
            throw new ArgumentOutOfRangeException(nameof(disposition), disposition, "Unknown pipeline success disposition.");
        }

        if (provenance.Stages.Count != 5
            || provenance.Stages.Any(static stage => stage.Outcome == PipelineStageOutcome.Failed)
            || provenance.SnapshotIdentity is null
            || provenance.DatasetVersion is null
            || provenance.SourceStateIdentity is null)
        {
            throw new ArgumentException(
                "Pipeline success requires every fixed stage and established dataset output evidence.",
                nameof(provenance));
        }

        return new PipelineExecutionResult(provenance, disposition, null, null);
    }

    public static PipelineExecutionResult Failed(
        PipelineProvenance provenance,
        ResearchPipelineStage failingStage,
        PipelineFailureCategory failureCategory)
    {
        ArgumentNullException.ThrowIfNull(provenance);

        if (!Enum.IsDefined(failingStage))
        {
            throw new ArgumentOutOfRangeException(nameof(failingStage), failingStage, "Unknown pipeline stage.");
        }

        if (!Enum.IsDefined(failureCategory))
        {
            throw new ArgumentOutOfRangeException(nameof(failureCategory), failureCategory, "Unknown pipeline failure category.");
        }

        if (provenance.Stages[^1].Stage != failingStage
            || provenance.Stages[^1].Outcome != PipelineStageOutcome.Failed
            || provenance.Stages[^1].FailureCategory != failureCategory)
        {
            throw new ArgumentException(
                "Pipeline failure must agree with the terminal failed stage evidence.",
                nameof(provenance));
        }

        return new PipelineExecutionResult(provenance, null, failingStage, failureCategory);
    }
}

public interface IPipelineExecutionUseCase
{
    PipelineExecutionResult Execute(PipelineRequest request);
}
