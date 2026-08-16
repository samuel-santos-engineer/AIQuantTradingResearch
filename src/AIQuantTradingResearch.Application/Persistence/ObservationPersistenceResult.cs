namespace AIQuantTradingResearch.Application.Persistence;

public sealed record ObservationPersistenceResult
{
    private ObservationPersistenceResult(
        ObservationPersistenceOutcome? outcome,
        PersistenceFailure? failure)
    {
        Outcome = outcome;
        Failure = failure;
    }

    public bool HasOutcome => Outcome is not null;

    public ObservationPersistenceOutcome? Outcome { get; }

    public PersistenceFailure? Failure { get; }

    public static ObservationPersistenceResult Completed(ObservationPersistenceOutcome outcome)
    {
        if (!Enum.IsDefined(outcome))
        {
            throw new ArgumentOutOfRangeException(nameof(outcome), outcome, "Unknown persistence outcome.");
        }

        return new ObservationPersistenceResult(outcome, null);
    }

    public static ObservationPersistenceResult Failed(PersistenceFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown persistence failure.");
        }

        return new ObservationPersistenceResult(null, failure);
    }
}
