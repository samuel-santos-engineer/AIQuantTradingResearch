using System.Reflection;
using Xunit;

namespace AIQuantTradingResearch.Architecture.Tests;

public sealed class VisualizationBoundaryRulesTests
{
    [Fact] public void DomainDoesNotReferenceVisualizationOrPresentationAssemblies() => Assert.DoesNotContain(Assembly.Load("AIQuantTradingResearch.Domain").GetReferencedAssemblies(), x => x.Name!.Contains("Visualization", StringComparison.Ordinal));
    [Fact] public void ApplicationDoesNotReferenceInfrastructure() => Assert.DoesNotContain(Assembly.Load("AIQuantTradingResearch.Application").GetReferencedAssemblies(), x => x.Name == "AIQuantTradingResearch.Infrastructure");
    [Fact] public void InfrastructureOwnsTheAtomicJsonPublisher() => Assert.Equal("AIQuantTradingResearch.Infrastructure", Type.GetType("AIQuantTradingResearch.Infrastructure.Visualization.VisualizationReadModelFilePublisher, AIQuantTradingResearch.Infrastructure", true)!.Assembly.GetName().Name);
    [Fact] public void WorkerDoesNotLaunchStreamlit() => Assert.DoesNotContain("Streamlit", Read("src/AIQuantTradingResearch.Worker/Program.cs"), StringComparison.OrdinalIgnoreCase);
    [Fact] public void PresentationDoesNotAccessSqliteOrProviders() { string source = Read("python/presentation/realtime_financial_visualization.py"); Assert.DoesNotContain("sqlite", source, StringComparison.OrdinalIgnoreCase); Assert.DoesNotContain("twelvedata", source, StringComparison.OrdinalIgnoreCase); }
    [Fact] public void PresentationDoesNotSuperviseProcesses() { string source = Read("python/presentation/realtime_financial_visualization.py"); Assert.DoesNotContain("subprocess", source, StringComparison.OrdinalIgnoreCase); Assert.DoesNotContain("Popen", source, StringComparison.Ordinal); Assert.DoesNotContain("os.system", source, StringComparison.Ordinal); }
    [Fact] public void PresentationDoesNotReuseWp08ProbeOrRelease18Endpoint() { string source = Read("python/presentation/realtime_financial_visualization.py"); Assert.DoesNotContain("wp08_presentation_chain_probe", source, StringComparison.Ordinal); Assert.DoesNotContain("python/capability", source, StringComparison.OrdinalIgnoreCase); }
    [Fact] public void PermanentIntegrationTestsUseCanonicalProductionBoundaries() { string tests = Read("tests/AIQuantTradingResearch.Infrastructure.Tests/VisualizationPermanentIntegrationTests.cs"); string replay = Read("src/AIQuantTradingResearch.Worker/SimulatedLiveVisualizationExecution.cs"); Assert.Contains("Worker__Mode", tests, StringComparison.Ordinal); Assert.Contains("PipelineExecution", tests, StringComparison.Ordinal); Assert.Contains("SimulatedLiveVisualizationExecution", replay, StringComparison.Ordinal); Assert.DoesNotContain("JsonSerializer.Serialize", tests, StringComparison.Ordinal); }

    private static string Read(string relative) => File.ReadAllText(Path.Combine(RepositoryRoot(), relative));
    private static string RepositoryRoot() { for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent) if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName; throw new InvalidOperationException("Repository root was not found."); }
}
