using System.Collections.ObjectModel;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Infrastructure.Research;

internal sealed class DeterministicObservationSource : IObservationSource
{
    private const string SupportedTarget = "SAMPLE-USD";

    private static readonly ReadOnlyCollection<PriceObservation> AvailableObservations =
        Array.AsReadOnly(
        [
            new PriceObservation(
                new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero),
                100.00m),
            new PriceObservation(
                new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
                110.00m),
            new PriceObservation(
                new DateTimeOffset(2024, 1, 3, 0, 0, 0, TimeSpan.Zero),
                120.00m),
        ]);

    public DeterministicObservationSource()
    {
    }

    public ObservationSourceResult GetObservations(ResearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!string.Equals(request.Target, SupportedTarget, StringComparison.Ordinal))
        {
            return ObservationSourceResult.Failed(ObservationSourceFailure.UnsupportedTarget);
        }

        if (request.RequestedObservationCount <= 0
            || request.RequestedObservationCount > AvailableObservations.Count)
        {
            return ObservationSourceResult.Failed(ObservationSourceFailure.InsufficientObservations);
        }

        return ObservationSourceResult.ObservationsAvailable(
            AvailableObservations.Take(request.RequestedObservationCount));
    }
}
