using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Persistence;

public interface IHistoricalObservationStore
{
    ObservationPersistenceResult Persist(
        string target,
        IReadOnlyList<PriceObservation> observations);

    HistoricalObservationResult Retrieve(string target);
}
