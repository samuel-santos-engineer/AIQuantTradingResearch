using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
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

        return services;
    }
}
