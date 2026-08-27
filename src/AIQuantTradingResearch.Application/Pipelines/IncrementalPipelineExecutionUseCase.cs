using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Research;

namespace AIQuantTradingResearch.Application.Pipelines;

public sealed record IncrementalPipelineRequest(
    DatasetDefinition DatasetDefinition,
    ReplayRequest ReplayRequest);

public sealed record IncrementalPipelineExecutionResult(
    ReplayObservationResult Replay,
    PipelineExecutionResult? Pipeline)
{
    public bool IsSuccess => Replay.IsSuccess && Pipeline?.IsSuccess == true;
}

public sealed class IncrementalPipelineExecutionUseCase
{
    private readonly IObservationSource observationSource;
    private readonly IPipelineRequestFactory requestFactory;
    private readonly IPipelineExecutionUseCase pipeline;

    public IncrementalPipelineExecutionUseCase(
        IObservationSource observationSource,
        IPipelineRequestFactory requestFactory,
        IPipelineExecutionUseCase pipeline)
    {
        ArgumentNullException.ThrowIfNull(observationSource);
        ArgumentNullException.ThrowIfNull(requestFactory);
        ArgumentNullException.ThrowIfNull(pipeline);
        this.observationSource = observationSource;
        this.requestFactory = requestFactory;
        this.pipeline = pipeline;
    }

    public IncrementalPipelineExecutionResult Execute(
        IncrementalPipelineRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        ReplayObservationResult replay = observationSource.Replay(request.ReplayRequest, cancellationToken);
        if (!replay.IsSuccess || replay.Observations is null || replay.Observations.Count == 0)
        {
            return new IncrementalPipelineExecutionResult(replay, null);
        }

        cancellationToken.ThrowIfCancellationRequested();
        PipelineExecutionResult pipelineResult = pipeline.Execute(
            requestFactory.Create(request.DatasetDefinition),
            replay.Observations);
        return new IncrementalPipelineExecutionResult(replay, pipelineResult);
    }
}
