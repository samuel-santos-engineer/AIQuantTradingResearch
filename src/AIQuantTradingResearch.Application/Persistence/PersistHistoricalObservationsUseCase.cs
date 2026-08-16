using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Persistence;

internal sealed class PersistHistoricalObservationsUseCase : IPersistHistoricalObservationsUseCase
{
    private readonly IHistoricalObservationStore store;

    public PersistHistoricalObservationsUseCase(IHistoricalObservationStore store)
    {
        ArgumentNullException.ThrowIfNull(store);
        this.store = store;
    }

    public PersistHistoricalObservationsResult Execute(
        PersistHistoricalObservationsRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!IsValid(request))
        {
            return PersistHistoricalObservationsResult.InvalidRequest();
        }

        var persistenceResult = store.Persist(request.Target, request.Observations)
            ?? throw new InvalidOperationException("The historical observation store returned no result.");

        return PersistHistoricalObservationsResult.Completed(persistenceResult);
    }

    private static bool IsValid(PersistHistoricalObservationsRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Target)
            || request.Observations is null
            || request.Observations.Count == 0
            || request.Observations.Any(static observation => observation is null))
        {
            return false;
        }

        for (var index = 1; index < request.Observations.Count; index++)
        {
            if (request.Observations[index].Instant <= request.Observations[index - 1].Instant)
            {
                return false;
            }
        }

        return true;
    }
}
