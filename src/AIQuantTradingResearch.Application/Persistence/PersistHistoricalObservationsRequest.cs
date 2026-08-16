using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Persistence;

public sealed record PersistHistoricalObservationsRequest(
    string Target,
    IReadOnlyList<PriceObservation> Observations);
