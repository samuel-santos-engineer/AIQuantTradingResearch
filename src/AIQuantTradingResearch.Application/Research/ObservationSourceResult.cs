using System.Collections.ObjectModel;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Research;

public sealed record ObservationSourceResult
{
    private ObservationSourceResult(
        ReadOnlyCollection<PriceObservation>? observations,
        ObservationSourceFailure? failure)
    {
        Observations = observations;
        Failure = failure;
    }

    public bool IsSuccess => Observations is not null;

    public IReadOnlyList<PriceObservation>? Observations { get; }

    public ObservationSourceFailure? Failure { get; }

    public static ObservationSourceResult ObservationsAvailable(
        IEnumerable<PriceObservation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var snapshot = observations.ToArray();

        if (snapshot.Length == 0 || snapshot.Any(static observation => observation is null))
        {
            throw new ArgumentException(
                "Available observations must contain at least one non-null observation.",
                nameof(observations));
        }

        return new ObservationSourceResult(Array.AsReadOnly(snapshot), null);
    }

    public static ObservationSourceResult Failed(ObservationSourceFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown observation-source failure.");
        }

        return new ObservationSourceResult(null, failure);
    }
}
