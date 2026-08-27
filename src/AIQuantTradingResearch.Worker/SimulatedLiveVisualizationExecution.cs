using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Infrastructure.Research;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Visualization;

namespace AIQuantTradingResearch.Worker;

internal sealed class SimulatedLiveVisualizationExecution
{
    private readonly ISimulatedLiveReplaySource observationSource;
    private readonly IPipelineRequestFactory requestFactory;
    private readonly IPipelineExecutionUseCase pipeline;
    private readonly VisualizationReadModelUseCase presentation;
    private readonly IWorkerLifecycleLivenessGate livenessGate;

    public SimulatedLiveVisualizationExecution(
        ISimulatedLiveReplaySource observationSource,
        IPipelineRequestFactory requestFactory,
        IPipelineExecutionUseCase pipeline,
        VisualizationReadModelUseCase presentation,
        IWorkerLifecycleLivenessGate? livenessGate = null)
    {
        ArgumentNullException.ThrowIfNull(observationSource);
        ArgumentNullException.ThrowIfNull(requestFactory);
        ArgumentNullException.ThrowIfNull(pipeline);
        ArgumentNullException.ThrowIfNull(presentation);
        this.observationSource = observationSource;
        this.requestFactory = requestFactory;
        this.pipeline = pipeline;
        this.presentation = presentation;
        this.livenessGate = livenessGate ?? new NoOpWorkerLifecycleLivenessGate();
    }

    public int Execute(
        SimulatedLiveVisualizationConfiguration configuration,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        if (configuration.Mode != WorkerExecutionMode.Replay || configuration.Replay is null)
        {
            throw new ArgumentException("Replay execution requires Replay mode and replay settings.", nameof(configuration));
        }

        var replay = configuration.Replay;
        var start = replay.StartingTick;
        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ReplayObservationResult result = observationSource.Replay(
                new ReplayRequest(replay.ReplayIdentity, replay.Target, start, replay.RequestedObservationCount),
                cancellationToken);

            if (!result.IsSuccess)
            {
                return 1;
            }

            if (result.Observations is { Count: > 0 })
            {
                foreach (var observation in result.Observations)
                {
                    if (observation.Instant < configuration.DatasetDefinition.From
                        || observation.Instant >= configuration.DatasetDefinition.To)
                    {
                        return 1;
                    }
                }
            }

            if (result.IsEndOfReplay && result.Observations is { Count: 0 })
            {
                return 0;
            }

            PipelineExecutionResult pipelineResult = pipeline.Execute(
                requestFactory.Create(configuration.DatasetDefinition),
                result.Observations!);
            if (!pipelineResult.IsSuccess)
            {
                presentation.PublishReplay(result.FirstTick, replay.Target, DatasetSourceAuthority.Release19SimulatedLiveReplay,
                    VisualizationPresentationState.Failed, result.Observations!.Select(VisualizationObservation.From),
                    category: pipelineResult.FailureCategory?.ToString(), safeMessage: "The pipeline did not produce a complete presentation result.");
                return 1;
            }

            var rows = result.Observations!.Select(VisualizationObservation.From);
            var state = rows.Count() < 2 ? VisualizationPresentationState.WarmUp : VisualizationPresentationState.Ready;
            presentation.PublishReplay(result.FirstTick, replay.Target, DatasetSourceAuthority.Release19SimulatedLiveReplay,
                state, rows, pipelineResult.SnapshotIdentity, pipelineResult.DatasetVersion,
                state == VisualizationPresentationState.WarmUp ? VisualizationFeature.WarmUp(rows.Count()) : null,
                PipelineExecutionEvidence.From(pipelineResult));

            livenessGate.AwaitReleaseAsync(cancellationToken).GetAwaiter().GetResult();

            if (result.IsEndOfReplay)
            {
                return 0;
            }

            start = result.NextTick;
        }
    }
}
