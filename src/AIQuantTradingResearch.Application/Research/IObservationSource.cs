namespace AIQuantTradingResearch.Application.Research;

public interface IObservationSource
{
    ObservationSourceResult GetObservations(ResearchRequest request);

    ReplayObservationResult Replay(
        ReplayRequest request,
        CancellationToken cancellationToken = default) =>
        ReplayObservationResult.Failed(ObservationSourceFailure.UnsupportedTarget);
}
