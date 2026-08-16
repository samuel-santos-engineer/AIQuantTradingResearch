using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Research;

namespace AIQuantTradingResearch.Worker;

internal sealed class PersistentMarketDataExecution
{
    private readonly IObservationSource observationSource;
    private readonly IPersistHistoricalObservationsUseCase persistenceUseCase;

    public PersistentMarketDataExecution(
        IObservationSource observationSource,
        IPersistHistoricalObservationsUseCase persistenceUseCase)
    {
        ArgumentNullException.ThrowIfNull(observationSource);
        ArgumentNullException.ThrowIfNull(persistenceUseCase);

        this.observationSource = observationSource;
        this.persistenceUseCase = persistenceUseCase;
    }

    public int Execute(ResearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var sourceResult = observationSource.GetObservations(request)
            ?? throw new InvalidOperationException("The observation source returned no result.");

        if (!sourceResult.IsSuccess)
        {
            var failure = sourceResult.Failure
                ?? throw new InvalidOperationException("A failed observation-source result must contain a failure.");

            Console.Error.WriteLine($"Research failed: {failure}");
            return 1;
        }

        var observations = sourceResult.Observations
            ?? throw new InvalidOperationException("A successful observation-source result contained no observations.");

        if (observations.Count != request.RequestedObservationCount)
        {
            Console.Error.WriteLine($"Research failed: {ResearchFailure.InsufficientObservations}");
            return 1;
        }

        var persistenceResult = persistenceUseCase.Execute(
            new PersistHistoricalObservationsRequest(request.Target, observations));

        if (!persistenceResult.IsValidRequest)
        {
            var failure = persistenceResult.Failure
                ?? throw new InvalidOperationException("An invalid persistence request must contain a failure.");

            Console.Error.WriteLine($"Persistence failed: {failure}");
            return 1;
        }

        var storeResult = persistenceResult.PersistenceResult
            ?? throw new InvalidOperationException("A valid persistence request must contain a persistence result.");

        if (storeResult.HasOutcome)
        {
            var outcome = storeResult.Outcome
                ?? throw new InvalidOperationException("A completed persistence result must contain an outcome.");

            if (outcome is ObservationPersistenceOutcome.NewlyAccepted
                or ObservationPersistenceOutcome.Idempotent)
            {
                Console.WriteLine($"Target: {request.Target}");
                Console.WriteLine($"Observation count: {observations.Count}");
                Console.WriteLine($"Persistence outcome: {outcome}");
                return 0;
            }

            Console.Error.WriteLine($"Persistence conflict: {outcome}");
            return 1;
        }

        var persistenceFailure = storeResult.Failure
            ?? throw new InvalidOperationException("A failed persistence result must contain a failure.");

        Console.Error.WriteLine($"Persistence failed: {persistenceFailure}");
        return 1;
    }
}
