namespace AIQuantTradingResearch.Application.Persistence;

public sealed record PersistHistoricalObservationsResult
{
    private PersistHistoricalObservationsResult(
        ObservationPersistenceResult? persistenceResult,
        PersistHistoricalObservationsFailure? failure)
    {
        PersistenceResult = persistenceResult;
        Failure = failure;
    }

    public bool IsValidRequest => PersistenceResult is not null;

    public ObservationPersistenceResult? PersistenceResult { get; }

    public PersistHistoricalObservationsFailure? Failure { get; }

    internal static PersistHistoricalObservationsResult Completed(
        ObservationPersistenceResult persistenceResult)
    {
        ArgumentNullException.ThrowIfNull(persistenceResult);
        return new PersistHistoricalObservationsResult(persistenceResult, null);
    }

    internal static PersistHistoricalObservationsResult InvalidRequest() =>
        new(null, PersistHistoricalObservationsFailure.InvalidRequest);
}
