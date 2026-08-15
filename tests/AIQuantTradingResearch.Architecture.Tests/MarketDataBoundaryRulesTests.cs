using System.Reflection;
using AIQuantTradingResearch.Application.Research;
using Xunit;

namespace AIQuantTradingResearch.Architecture.Tests;

public sealed class MarketDataBoundaryRulesTests
{
    private const string Domain = "AIQuantTradingResearch.Domain";
    private const string Application = "AIQuantTradingResearch.Application";
    private const string Infrastructure = "AIQuantTradingResearch.Infrastructure";
    private const string Worker = "AIQuantTradingResearch.Worker";
    private const string HttpTransportAssembly = "System.Net.Http";
    private const string ProviderName = "TwelveData";
    private const string ProviderNamespace =
        "AIQuantTradingResearch.Infrastructure.MarketData.TwelveData";

    private static readonly IReadOnlyDictionary<string, Assembly> ProductionAssemblies =
        new Dictionary<string, Assembly>
        {
            [Domain] = Assembly.Load(Domain),
            [Application] = Assembly.Load(Application),
            [Infrastructure] = Assembly.Load(Infrastructure),
            [Worker] = Assembly.Load(Worker),
        };

    [Fact]
    public void ProviderIndependentAssembliesShouldNotDefineTwelveDataTypes()
    {
        foreach (var assemblyName in new[] { Domain, Application })
        {
            var providerTypes = ProductionAssemblies[assemblyName]
                .GetTypes()
                .Where(IsTwelveDataType)
                .Select(type => type.FullName)
                .ToArray();

            Assert.True(
                providerTypes.Length == 0,
                $"Assembly '{assemblyName}' defines Twelve Data-specific types: {string.Join(", ", providerTypes)}.");
        }
    }

    [Fact]
    public void ProviderIndependentAssembliesShouldNotReferenceHttpTransport()
    {
        foreach (var assemblyName in new[] { Domain, Application })
        {
            var referencesHttpTransport = ProductionAssemblies[assemblyName]
                .GetReferencedAssemblies()
                .Any(reference => string.Equals(
                    reference.Name,
                    HttpTransportAssembly,
                    StringComparison.Ordinal));

            Assert.False(
                referencesHttpTransport,
                $"Assembly '{assemblyName}' must not reference provider HTTP transport assembly '{HttpTransportAssembly}'.");
        }
    }

    [Fact]
    public void ProviderIndependentAcquisitionContractsShouldBeOwnedByApplication()
    {
        var contractTypes = new[]
        {
            typeof(IObservationSource),
            typeof(ObservationSourceResult),
            typeof(ObservationSourceFailure),
            typeof(ResearchFailure),
        };

        foreach (var contractType in contractTypes)
        {
            Assert.Equal(Application, contractType.Assembly.GetName().Name);
        }
    }

    [Fact]
    public void TwelveDataTypesShouldBeConfinedToInfrastructureWithAuthoritativeVisibility()
    {
        var providerTypes = ProductionAssemblies.Values
            .SelectMany(assembly => assembly.GetTypes())
            .Where(IsTwelveDataType)
            .ToArray();

        Assert.NotEmpty(providerTypes);

        foreach (var providerType in providerTypes)
        {
            Assert.Equal(Infrastructure, providerType.Assembly.GetName().Name);
        }

        var configurationType = providerTypes.Single(type =>
            string.Equals(type.FullName, $"{ProviderNamespace}.TwelveDataConfiguration", StringComparison.Ordinal));

        Assert.True(configurationType.IsVisible);

        foreach (var implementationType in providerTypes.Where(type => type != configurationType))
        {
            Assert.False(
                implementationType.IsVisible,
                $"Provider implementation/transport type '{implementationType.FullName}' must remain non-public.");
        }
    }

    private static bool IsTwelveDataType(Type type) =>
        type.Name.Contains(ProviderName, StringComparison.Ordinal)
        || (type.Namespace?.Contains(ProviderName, StringComparison.Ordinal) ?? false);
}
