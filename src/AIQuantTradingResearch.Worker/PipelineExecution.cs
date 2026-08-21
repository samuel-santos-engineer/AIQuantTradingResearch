using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Pipelines;

namespace AIQuantTradingResearch.Worker;

internal sealed class PipelineExecution
{
    private readonly IPipelineExecutionUseCase pipelineExecutionUseCase;
    private readonly IPipelineRequestFactory pipelineRequestFactory;

    public PipelineExecution(
        IPipelineExecutionUseCase pipelineExecutionUseCase,
        IPipelineRequestFactory pipelineRequestFactory)
    {
        ArgumentNullException.ThrowIfNull(pipelineExecutionUseCase);
        ArgumentNullException.ThrowIfNull(pipelineRequestFactory);

        this.pipelineExecutionUseCase = pipelineExecutionUseCase;
        this.pipelineRequestFactory = pipelineRequestFactory;
    }

    public int Execute(PipelineExecutionConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        PipelineRequest request = pipelineRequestFactory.Create(configuration.DatasetDefinition);
        PipelineExecutionResult result = pipelineExecutionUseCase.Execute(request)
            ?? throw new InvalidOperationException("The pipeline execution use case returned no result.");
        PipelineExecutionEvidence evidence = PipelineExecutionEvidence.From(result);

        Present(evidence);
        return evidence.IsSuccess ? 0 : 1;
    }

    private static void Present(PipelineExecutionEvidence evidence)
    {
        TextWriter output = evidence.IsSuccess ? Console.Out : Console.Error;

        output.WriteLine($"Pipeline definition identity: {evidence.DefinitionIdentity.Fingerprint}");
        output.WriteLine($"Pipeline execution identity: {evidence.ExecutionIdentity.Fingerprint}");
        output.WriteLine($"Dataset definition identity: {evidence.DatasetDefinitionIdentity.Fingerprint}");

        if (evidence.SourceStateIdentity is not null)
        {
            output.WriteLine($"Source state identity: {evidence.SourceStateIdentity.Fingerprint}");
        }

        if (evidence.SnapshotIdentity is not null && evidence.DatasetVersion is not null)
        {
            output.WriteLine($"Snapshot identity: {evidence.SnapshotIdentity.Fingerprint}");
            output.WriteLine($"Dataset version identity: {evidence.DatasetVersion.SnapshotIdentity.Fingerprint}");
        }

        foreach (PipelineStageEvidence stage in evidence.Stages)
        {
            output.WriteLine(
                $"Stage {(int)stage.Stage}: {stage.Stage} = {stage.Outcome}"
                + (stage.FailureCategory is null ? string.Empty : $" ({stage.FailureCategory})"));
        }

        if (evidence.IsSuccess)
        {
            output.WriteLine($"Pipeline outcome: {evidence.Disposition}");
            return;
        }

        output.WriteLine($"Pipeline failure stage: {evidence.FailingStage}");
        output.WriteLine($"Pipeline failure category: {evidence.FailureCategory}");
    }
}
