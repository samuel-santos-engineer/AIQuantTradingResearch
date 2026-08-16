namespace AIQuantTradingResearch.Application.Persistence;

public interface IPersistHistoricalObservationsUseCase
{
    PersistHistoricalObservationsResult Execute(PersistHistoricalObservationsRequest request);
}
