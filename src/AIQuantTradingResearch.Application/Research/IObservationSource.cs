namespace AIQuantTradingResearch.Application.Research;

public interface IObservationSource
{
    Task<ObservationSourceResult> GetObservationsAsync(
        ResearchRequest request,
        CancellationToken cancellationToken = default);

    ReplayObservationResult Replay(
        ReplayRequest request,
        CancellationToken cancellationToken = default) =>
        ReplayObservationResult.Failed(ObservationSourceFailure.UnsupportedTarget);
}
