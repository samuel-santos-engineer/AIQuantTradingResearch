using AIQuantTradingResearch.Application.Datasets;
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
        services.AddTransient<IMaterializeDatasetUseCase, MaterializeDatasetUseCase>();
        services.AddTransient<IDatasetMaterializationIntegrationUseCase, DatasetMaterializationIntegrationUseCase>();

        return services;
    }
}
