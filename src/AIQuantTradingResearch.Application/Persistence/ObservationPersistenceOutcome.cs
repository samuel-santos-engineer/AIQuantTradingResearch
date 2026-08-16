namespace AIQuantTradingResearch.Application.Persistence;

public enum ObservationPersistenceOutcome
{
    NewlyAccepted,
    Idempotent,
    Conflict,
}
