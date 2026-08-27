using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Infrastructure.Research;
using AIQuantTradingResearch.Infrastructure.Visualization;
using AIQuantTradingResearch.Application.Visualization;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        throw new InvalidOperationException(
            $"Missing mandatory configuration: {TwelveDataConfiguration.SectionName}:{TwelveDataConfiguration.ApiKeyName}.");
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        TwelveDataConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton(
            static _ => new HttpClient
            {
                BaseAddress = new Uri("https://api.twelvedata.com/"),
            });
        services.AddSingleton(
            serviceProvider => new TwelveDataClient(
                serviceProvider.GetRequiredService<HttpClient>(),
                configuration.ApiKey));
        services.AddSingleton<IObservationSource, TwelveDataObservationSource>();
        services.AddSingleton<SimulatedLiveReplayConfiguration>();
        services.AddSingleton<SimulatedLiveObservationSource>();
        services.AddSingleton<ISimulatedLiveReplaySource>(
            serviceProvider => serviceProvider.GetRequiredService<SimulatedLiveObservationSource>());
        services.AddTransient<IncrementalPipelineExecutionUseCase>(
            serviceProvider => new IncrementalPipelineExecutionUseCase(
                serviceProvider.GetRequiredService<SimulatedLiveObservationSource>(),
                serviceProvider.GetRequiredService<IPipelineRequestFactory>(),
                serviceProvider.GetRequiredService<IPipelineExecutionUseCase>()));
        services.AddSingleton<IVisualizationReadModelStore, AtomicVisualizationReadModelStore>();
        services.AddSingleton<VisualizationReadModelUseCase>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        TwelveDataConfiguration configuration,
        SqliteStorageConfiguration storageConfiguration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(storageConfiguration);

        services.AddInfrastructure(configuration);
        services.AddSingleton(storageConfiguration);
        services.AddSingleton<ISqliteConnectionFactory, SqliteConnectionFactory>();
        services.AddTransient<IHistoricalObservationStore, SqliteHistoricalObservationStore>();
        services.AddTransient<IDatasetSnapshotStore, SqliteDatasetSnapshotStore>();
        services.AddTransient<IDatasetCatalog, SqliteDatasetCatalog>();
        services.AddTransient<SqliteExperimentResultStore>();
        services.AddTransient<IDurableExperimentEvidenceStore>(
            static serviceProvider => serviceProvider.GetRequiredService<SqliteExperimentResultStore>());
        services.AddTransient<IDurableExperimentEvidenceDiscoveryStore>(
            static serviceProvider => serviceProvider.GetRequiredService<SqliteExperimentResultStore>());

        return services;
    }
}
