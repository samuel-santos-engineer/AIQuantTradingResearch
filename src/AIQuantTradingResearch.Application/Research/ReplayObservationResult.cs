using System.Collections.ObjectModel;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Research;

public sealed record ReplayObservationResult
{
    private ReplayObservationResult(
        ReadOnlyCollection<PriceObservation>? observations,
        int firstTick,
        int nextTick,
        bool isEndOfReplay,
        ObservationSourceFailure? failure)
    {
        Observations = observations;
        FirstTick = firstTick;
        NextTick = nextTick;
        IsEndOfReplay = isEndOfReplay;
        Failure = failure;
    }

    public IReadOnlyList<PriceObservation>? Observations { get; }
    public int FirstTick { get; }
    public int NextTick { get; }
    public bool IsEndOfReplay { get; }
    public ObservationSourceFailure? Failure { get; }
    public bool IsSuccess => Observations is not null;

    public static ReplayObservationResult Available(
        IEnumerable<PriceObservation> observations,
        int firstTick,
        int nextTick,
        bool isEndOfReplay)
    {
        ArgumentNullException.ThrowIfNull(observations);
        var snapshot = observations.ToArray();
        if (snapshot.Any(static observation => observation is null)
            || (snapshot.Length == 0 && !isEndOfReplay))
        {
            throw new ArgumentException("Replay observations must be non-empty.", nameof(observations));
        }

        return new(Array.AsReadOnly(snapshot), firstTick, nextTick, isEndOfReplay, null);
    }

    public static ReplayObservationResult Failed(ObservationSourceFailure failure) =>
        new(null, 0, 0, false, failure);
}
