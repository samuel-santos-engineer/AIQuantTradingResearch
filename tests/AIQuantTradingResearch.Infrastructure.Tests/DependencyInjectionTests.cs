using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructureWithConfigurationRegistersSingleProviderGraphWithoutNetworkCall()
    {
        var services = new ServiceCollection();
        services.AddInfrastructure(new TwelveDataConfiguration("wp13-placeholder-key"));

        var sourceDescriptors = services
            .Where(static descriptor => descriptor.ServiceType == typeof(IObservationSource))
            .ToArray();
        var httpDescriptor = Assert.Single(
            services,
            static descriptor => descriptor.ServiceType == typeof(HttpClient));
        using var productionProvider = services.BuildServiceProvider(
            CreateValidationOptions());

        var firstSource = productionProvider.GetRequiredService<IObservationSource>();
        var secondSource = productionProvider.GetRequiredService<IObservationSource>();
        var client = productionProvider.GetRequiredService<TwelveDataClient>();
        var httpClient = productionProvider.GetRequiredService<HttpClient>();

        Assert.Single(sourceDescriptors);
        Assert.Equal(ServiceLifetime.Singleton, sourceDescriptors[0].Lifetime);
        Assert.Equal(typeof(TwelveDataObservationSource), sourceDescriptors[0].ImplementationType);
        Assert.IsType<TwelveDataObservationSource>(firstSource);
        Assert.Same(firstSource, secondSource);
        Assert.NotNull(client);
        Assert.Equal(new Uri("https://api.twelvedata.com/"), httpClient.BaseAddress);
        Assert.DoesNotContain(
            services,
            static descriptor =>
                descriptor.ImplementationType?.Name == "DeterministicObservationSource");

        using var handler = new TwelveDataTestHttpMessageHandler(
            static (_, _) => throw new InvalidOperationException("Provider HTTP must not be called."));
        services.Remove(httpDescriptor);
        services.AddSingleton(
            new HttpClient(handler)
            {
                BaseAddress = new Uri("https://api.twelvedata.com/"),
            });
        using var instrumentedProvider = services.BuildServiceProvider(
            CreateValidationOptions());

        _ = instrumentedProvider.GetRequiredService<IObservationSource>();
        Assert.Equal(0, handler.CallCount);
    }

    [Fact]
    public void AddInfrastructureWithoutConfigurationThrowsMissingConfigurationFailure()
    {
        var services = new ServiceCollection();

        var exception = Assert.Throws<InvalidOperationException>(services.AddInfrastructure);

        Assert.Contains("TwelveData:ApiKey", exception.Message, StringComparison.Ordinal);
        Assert.Empty(services);
    }

    private static ServiceProviderOptions CreateValidationOptions() =>
        new()
        {
            ValidateOnBuild = true,
            ValidateScopes = true,
        };
}
