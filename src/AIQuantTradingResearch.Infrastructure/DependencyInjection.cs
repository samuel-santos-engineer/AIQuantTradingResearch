using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
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
}
