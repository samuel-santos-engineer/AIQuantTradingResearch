using System.Reflection;
using Xunit;

namespace AIQuantTradingResearch.Architecture.Tests;

public sealed class Release110ObservabilityNoBypassTests
{
    [Fact]
    public void ForbiddenPersistenceSymbolsRemainUninstrumented()
    {
        var catalog = Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteDatasetCatalog.cs");
        var history = Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs");
        Assert.DoesNotContain("StartPersistence", catalog, StringComparison.Ordinal);
        var persist = history[..history.IndexOf("public HistoricalObservationResult Retrieve", StringComparison.Ordinal)];
        Assert.DoesNotContain("StartProviderRetrieve", persist, StringComparison.Ordinal);
    }

    [Fact]
    public void WorkerHasNoExporterDependency()
    {
        var workerProject = Read("src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj");
        var infrastructureProject = Read("src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj");
        Assert.DoesNotContain("OpenTelemetry", workerProject, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("OpenTelemetry", infrastructureProject, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CanonicalHandoffAndIndependentPresentationArePreserved()
    {
        var publisher = Read("src/AIQuantTradingResearch.Infrastructure/Visualization/VisualizationReadModelFilePublisher.cs");
        var presentation = Read("python/presentation/realtime_financial_visualization.py");
        Assert.Contains("aiq-visualization-read-model-v1", publisher, StringComparison.Ordinal);
        Assert.DoesNotContain("sqlite", presentation, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("subprocess", presentation, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("OpenTelemetry", presentation, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void TelemetrySourcesContainNoSensitiveDimensions()
    {
        var application = Read("src/AIQuantTradingResearch.Application/Pipelines/PipelineObservability.cs");
        var infrastructure = Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs");
        Assert.DoesNotContain("connection string", application, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("exception.Message", infrastructure, StringComparison.Ordinal);
        Assert.DoesNotContain("apiKey", infrastructure, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void NoExporterPackageOrConfigurationIsIntroduced() => WorkerHasNoExporterDependency();

    [Fact]
    public void SchemaAndReleaseBoundaryRemainStable()
    {
        Assert.Contains("CurrentVersion = 4", Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteSchemaBootstrapper.cs"), StringComparison.Ordinal);
        Assert.DoesNotContain("python/capability", Read("python/presentation/realtime_financial_visualization.py"), StringComparison.OrdinalIgnoreCase);
    }

    private static string Read(string relative)
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return File.ReadAllText(Path.Combine(directory.FullName, relative));
        }
        throw new InvalidOperationException("Repository root was not found.");
    }
}
