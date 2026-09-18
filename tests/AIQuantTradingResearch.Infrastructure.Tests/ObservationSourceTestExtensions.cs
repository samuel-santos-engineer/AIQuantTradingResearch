using AIQuantTradingResearch.Application.Research;

namespace AIQuantTradingResearch.Infrastructure.Tests;

internal static class ObservationSourceTestExtensions
{
    public static ObservationSourceResult GetObservations(
        this IObservationSource source,
        ResearchRequest request) => source.GetObservationsAsync(request).GetAwaiter().GetResult();
}
