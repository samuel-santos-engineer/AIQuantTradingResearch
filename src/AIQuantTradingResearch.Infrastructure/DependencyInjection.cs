using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure.Research;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IObservationSource, DeterministicObservationSource>();

        return services;
    }
}
