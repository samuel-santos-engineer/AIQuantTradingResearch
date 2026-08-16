using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Research;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IResearchUseCase, ResearchUseCase>();
        services.AddTransient<IPersistHistoricalObservationsUseCase, PersistHistoricalObservationsUseCase>();

        return services;
    }
}
