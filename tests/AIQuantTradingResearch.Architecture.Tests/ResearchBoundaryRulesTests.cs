using System.Reflection;
using AIQuantTradingResearch.Application.Research;
using Xunit;

namespace AIQuantTradingResearch.Architecture.Tests;

public sealed class ResearchBoundaryRulesTests
{
    private const string Application = "AIQuantTradingResearch.Application";
    private const string Infrastructure = "AIQuantTradingResearch.Infrastructure";

    [Fact]
    public void ObservationSourceAbstractionShouldBeOwnedByApplication()
    {
        Assert.Equal(Application, typeof(IObservationSource).Assembly.GetName().Name);
    }

    [Fact]
    public void ResearchBoundaryImplementationsShouldRemainNonPublic()
    {
        AssertImplementationsAreNonPublic<IResearchUseCase>(Assembly.Load(Application));
        AssertImplementationsAreNonPublic<IObservationSource>(Assembly.Load(Infrastructure));
    }

    private static void AssertImplementationsAreNonPublic<TAbstraction>(Assembly assembly)
    {
        var implementations = assembly
            .GetTypes()
            .Where(type =>
                typeof(TAbstraction).IsAssignableFrom(type)
                && type is { IsInterface: false, IsAbstract: false })
            .ToArray();

        Assert.NotEmpty(implementations);

        foreach (var implementation in implementations)
        {
            Assert.False(
                implementation.IsVisible,
                $"Implementation '{implementation.FullName}' of '{typeof(TAbstraction).FullName}' must remain non-public.");
        }
    }
}
