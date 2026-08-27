using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

public sealed record PipelineExecutionEvidence
{
    private PipelineExecutionEvidence(PipelineExecutionResult result)
    {
        Result = result;
        SemanticModelVersion = PipelineDefinition.SemanticModelVersion;
        Topology = PipelineDefinition.Topology;
    }

    public PipelineExecutionResult Result { get; }

    public string SemanticModelVersion { get; }

    public IReadOnlyList<ResearchPipelineStage> Topology { get; }

    public PipelineDefinitionIdentity DefinitionIdentity => Result.DefinitionIdentity;

    public PipelineExecutionIdentity ExecutionIdentity => Result.ExecutionIdentity;

    public DatasetDefinitionIdentity DatasetDefinitionIdentity => Result.Provenance.DatasetDefinitionIdentity;

    public SourceStateIdentity? SourceStateIdentity => Result.Provenance.SourceStateIdentity;

    public DatasetSnapshotIdentity? SnapshotIdentity => Result.SnapshotIdentity;

    public DatasetVersion? DatasetVersion => Result.DatasetVersion;

    public IReadOnlyList<PipelineStageEvidence> Stages => Result.Provenance.Stages;

    public bool IsSuccess => Result.IsSuccess;

    public PipelineSuccessDisposition? Disposition => Result.Disposition;

    public ResearchPipelineStage? FailingStage => Result.FailingStage;

    public PipelineFailureCategory? FailureCategory => Result.FailureCategory;

    public PresentationIdempotencyStatus PresentationIdempotencyStatus => Result.PresentationIdempotencyStatus;

    public PresentationDataQualityStatus PresentationDataQualityStatus => Result.PresentationDataQualityStatus;

    public static PipelineExecutionEvidence From(PipelineExecutionResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        return new PipelineExecutionEvidence(result);
    }
}
