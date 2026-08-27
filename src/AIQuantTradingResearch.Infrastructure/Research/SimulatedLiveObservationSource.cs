using System.Collections.ObjectModel;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Infrastructure.Research;

public interface ISimulatedLiveReplaySource
{
    ReplayObservationResult Replay(ReplayRequest request, CancellationToken cancellationToken = default);
}

internal sealed class SimulatedLiveObservationSource : IObservationSource, ISimulatedLiveReplaySource
{
    private static readonly ReadOnlyCollection<PriceObservation> Fixture = Array.AsReadOnly(new PriceObservation[]
    {
        new(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 100.00m),
        new(new DateTimeOffset(2024, 1, 1, 0, 1, 0, TimeSpan.Zero), 101.25m),
        new(new DateTimeOffset(2024, 1, 1, 0, 2, 0, TimeSpan.Zero), 100.75m),
    });

    private readonly SimulatedLiveReplayConfiguration configuration;

    public SimulatedLiveObservationSource(SimulatedLiveReplayConfiguration? configuration = null)
    {
        this.configuration = configuration ?? new();
        this.configuration.Validate();
    }

    public ObservationSourceResult GetObservations(ResearchRequest request) =>
        ObservationSourceResult.Failed(ObservationSourceFailure.UnsupportedTarget);

    public ReplayObservationResult Replay(ReplayRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        cancellationToken.ThrowIfCancellationRequested();

        if (!string.Equals(request.ReplayIdentity, configuration.ReplayIdentity, StringComparison.Ordinal)
            || !string.Equals(request.Target, configuration.Target, StringComparison.Ordinal))
        {
            return ReplayObservationResult.Failed(ObservationSourceFailure.UnsupportedTarget);
        }

        if (request.StartingTick < 0 || request.StartingTick > Fixture.Count || request.RequestedObservationCount <= 0)
        {
            return ReplayObservationResult.Failed(ObservationSourceFailure.InsufficientObservations);
        }

        if (request.StartingTick == Fixture.Count)
        {
            return ReplayObservationResult.Available([], Fixture.Count, Fixture.Count, true);
        }

        var count = Math.Min(request.RequestedObservationCount, Fixture.Count - request.StartingTick);
        var values = new List<PriceObservation>(count);
        for (var index = request.StartingTick; index < request.StartingTick + count; index++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            values.Add(Fixture[index]);
        }

        var nextTick = request.StartingTick + count;
        return ReplayObservationResult.Available(values, request.StartingTick, nextTick, nextTick == Fixture.Count);
    }
}
