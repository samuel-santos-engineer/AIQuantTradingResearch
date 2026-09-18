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

    public Task<ObservationSourceResult> GetObservationsAsync(
        ResearchRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!string.Equals(request.Target, SupportedTarget, StringComparison.Ordinal))
        {
            return Task.FromResult(ObservationSourceResult.Failed(ObservationSourceFailure.UnsupportedTarget));
        }

        if (request.RequestedObservationCount <= 0
            || request.RequestedObservationCount > AvailableObservations.Count)
        {
            return Task.FromResult(ObservationSourceResult.Failed(ObservationSourceFailure.InsufficientObservations));
        }

        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(ObservationSourceResult.ObservationsAvailable(
            AvailableObservations.Take(request.RequestedObservationCount)));
    }
}
