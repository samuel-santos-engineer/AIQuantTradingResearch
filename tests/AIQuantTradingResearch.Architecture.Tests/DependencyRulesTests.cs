using System.Reflection;
using Xunit;

namespace AIQuantTradingResearch.Architecture.Tests;

public sealed class DependencyRulesTests
{
    private const string Domain = "AIQuantTradingResearch.Domain";
    private const string Application = "AIQuantTradingResearch.Application";
    private const string Infrastructure = "AIQuantTradingResearch.Infrastructure";
    private const string Worker = "AIQuantTradingResearch.Worker";

    private static readonly IReadOnlyDictionary<string, Assembly> ProductionAssemblies =
        new Dictionary<string, Assembly>
        {
            [Domain] = Assembly.Load(Domain),
            [Application] = Assembly.Load(Application),
            [Infrastructure] = Assembly.Load(Infrastructure),
            [Worker] = Assembly.Load(Worker),
        };

    [Fact]
    public void DomainShouldNotDependOnApplication()
    {
        AssertDoesNotReference(Domain, Application);
    }

    [Fact]
    public void DomainShouldNotDependOnInfrastructure()
    {
        AssertDoesNotReference(Domain, Infrastructure);
    }

    [Fact]
    public void DomainShouldNotDependOnWorker()
    {
        AssertDoesNotReference(Domain, Worker);
    }

    [Fact]
    public void ApplicationShouldNotDependOnInfrastructure()
    {
        AssertDoesNotReference(Application, Infrastructure);
    }

    [Fact]
    public void ApplicationShouldNotDependOnWorker()
    {
        AssertDoesNotReference(Application, Worker);
    }

    [Fact]
    public void InfrastructureShouldNotDependOnWorker()
    {
        AssertDoesNotReference(Infrastructure, Worker);
    }

    [Fact]
    public void ProductionProjectGraphShouldBeAcyclic()
    {
        var visited = new HashSet<string>(StringComparer.Ordinal);
        var activePath = new HashSet<string>(StringComparer.Ordinal);

        foreach (var assemblyName in ProductionAssemblies.Keys)
        {
            Assert.False(
                ContainsCycle(assemblyName, visited, activePath),
                $"The production assembly dependency graph contains a cycle involving '{assemblyName}'.");
        }
    }

    private static void AssertDoesNotReference(string source, string forbiddenTarget)
    {
        var references = GetProductionReferences(source);

        Assert.DoesNotContain(forbiddenTarget, references);
    }

    private static bool ContainsCycle(
        string assemblyName,
        ISet<string> visited,
        ISet<string> activePath)
    {
        if (activePath.Contains(assemblyName))
        {
            return true;
        }

        if (!visited.Add(assemblyName))
        {
            return false;
        }

        activePath.Add(assemblyName);

        foreach (var dependency in GetProductionReferences(assemblyName))
        {
            if (ContainsCycle(dependency, visited, activePath))
            {
                return true;
            }
        }

        activePath.Remove(assemblyName);
        return false;
    }

    private static HashSet<string> GetProductionReferences(string assemblyName)
    {
        return ProductionAssemblies[assemblyName]
            .GetReferencedAssemblies()
            .Select(reference => reference.Name)
            .Where(reference => reference is not null && ProductionAssemblies.ContainsKey(reference))
            .Select(reference => reference!)
            .ToHashSet(StringComparer.Ordinal);
    }
}
