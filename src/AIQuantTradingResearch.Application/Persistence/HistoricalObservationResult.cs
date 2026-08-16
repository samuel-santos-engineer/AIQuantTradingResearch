using System.Collections.ObjectModel;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Persistence;

public sealed record HistoricalObservationResult
{
    private HistoricalObservationResult(
        ReadOnlyCollection<PriceObservation>? observations,
        PersistenceFailure? failure)
    {
        Observations = observations;
        Failure = failure;
    }

    public bool IsSuccess => Observations is not null;

    public IReadOnlyList<PriceObservation>? Observations { get; }

    public PersistenceFailure? Failure { get; }

    public static HistoricalObservationResult Retrieved(
        IEnumerable<PriceObservation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var snapshot = observations.ToArray();

        if (snapshot.Any(static observation => observation is null))
        {
            throw new ArgumentException(
                "Retrieved observations cannot contain a null observation.",
                nameof(observations));
        }

        for (var index = 1; index < snapshot.Length; index++)
        {
            if (snapshot[index].Instant <= snapshot[index - 1].Instant)
            {
                throw new ArgumentException(
                    "Retrieved observation instants must be unique and strictly increasing.",
                    nameof(observations));
            }
        }

        return new HistoricalObservationResult(Array.AsReadOnly(snapshot), null);
    }

    public static HistoricalObservationResult Failed(PersistenceFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown persistence failure.");
        }

        return new HistoricalObservationResult(null, failure);
    }
}
