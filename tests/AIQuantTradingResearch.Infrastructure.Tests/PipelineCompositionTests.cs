using System.Diagnostics;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class PipelineCompositionTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));
    private static readonly DateTimeOffset To = From.AddDays(2);

    [Fact]
    public void CompositionRegistersOnePipelineGraphWithAcceptedLifetimesAndNoResolutionSideEffects()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure(new TwelveDataConfiguration("wp11-dummy-api-key"), database.Configuration);

        AssertSingle(services, typeof(IPipelineExecutionUseCase), ServiceLifetime.Transient, "PipelineExecutionUseCase");
        AssertSingle(services, typeof(IPipelineRequestFactory), ServiceLifetime.Singleton, "PipelineRequestFactory");
        AssertSingle(services, typeof(IMaterializeDatasetUseCase), ServiceLifetime.Transient, "MaterializeDatasetUseCase");
        AssertSingle(services, typeof(IDatasetSnapshotStore), ServiceLifetime.Transient, "SqliteDatasetSnapshotStore");
        AssertSingle(services, typeof(IDatasetCatalog), ServiceLifetime.Transient, "SqliteDatasetCatalog");
        AssertSingle(services, typeof(IHistoricalObservationStore), ServiceLifetime.Transient, "SqliteHistoricalObservationStore");

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = true,
        });

        var firstPipeline = provider.GetRequiredService<IPipelineExecutionUseCase>();
        var secondPipeline = provider.GetRequiredService<IPipelineExecutionUseCase>();
        var firstFactory = provider.GetRequiredService<IPipelineRequestFactory>();
        var secondFactory = provider.GetRequiredService<IPipelineRequestFactory>();
        _ = provider.GetRequiredService<IMaterializeDatasetUseCase>();
        _ = provider.GetRequiredService<IDatasetSnapshotStore>();
        _ = provider.GetRequiredService<IDatasetCatalog>();
        _ = provider.GetRequiredService<IHistoricalObservationStore>();
        _ = provider.GetRequiredService<IObservationSource>();

        Assert.Equal("PipelineExecutionUseCase", firstPipeline.GetType().Name);
        Assert.NotSame(firstPipeline, secondPipeline);
        Assert.Same(firstFactory, secondFactory);
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void WorkerExecutesOnePipelinePerProcessAndPreservesNewAndEquivalentEvidence()
    {
        using var database = new TemporaryDatabase();
        var observation = new PriceObservation(From.AddHours(1), 12.34567890123456789012345678m);
        database.History.Persist(" AAPL ", [observation]);

        WorkerResult first = RunWorker(database.Path, " AAPL ", From, To);
        WorkerResult second = RunWorker(database.Path, " AAPL ", From, To);

        Assert.Equal(0, first.ExitCode);
        Assert.Contains("Pipeline outcome: NewlyAccepted", first.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(5, CountStageLines(first.StandardOutput));
        Assert.Equal(0, first.StandardError.Length);

        Assert.Equal(0, second.ExitCode);
        Assert.Contains("Pipeline outcome: EquivalentExisting", second.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(5, CountStageLines(second.StandardOutput));
        Assert.Equal(0, second.StandardError.Length);

        string firstExecutionIdentity = ReadEvidenceValue(first.StandardOutput, "Pipeline execution identity: ");
        string secondExecutionIdentity = ReadEvidenceValue(second.StandardOutput, "Pipeline execution identity: ");
        Assert.Equal(firstExecutionIdentity, secondExecutionIdentity);
        Assert.Equal(64, firstExecutionIdentity.Length);
        Assert.Contains("Snapshot identity: ", first.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("wp11-dummy-api-key", first.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain(database.Path, first.StandardOutput, StringComparison.Ordinal);
    }

    [Fact]
    public void WorkerTreatsAnEmptyAcceptedDatasetAsSuccessful()
    {
        using var database = new TemporaryDatabase();

        WorkerResult result = RunWorker(database.Path, "AAPL", From, To);

        Assert.Equal(0, result.ExitCode);
        Assert.Contains("Pipeline outcome: NewlyAccepted", result.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Snapshot identity: ", result.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(5, CountStageLines(result.StandardOutput));
        Assert.Equal(0, result.StandardError.Length);
    }

    [Theory]
    [InlineData("Dataset__Target")]
    [InlineData("Dataset__From")]
    [InlineData("Dataset__To")]
    public void WorkerRejectsMissingRequiredDatasetConfigurationBeforeExecution(string missingVariable)
    {
        using var database = new TemporaryDatabase(createDirectory: false);

        WorkerResult result = RunWorker(database.Path, "AAPL", From, To, missingVariable);

        Assert.NotEqual(0, result.ExitCode);
        Assert.Contains("Invalid mandatory dataset configuration.", result.StandardError, StringComparison.Ordinal);
        Assert.False(File.Exists(database.Path));
        Assert.DoesNotContain("wp11-dummy-api-key", result.StandardError, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("2024-01-01", "2024-01-03T00:00:00.0000000-03:00")]
    [InlineData("2024-01-03T00:00:00.0000000-03:00", "2024-01-01T00:00:00.0000000-03:00")]
    public void WorkerRejectsMalformedOrInvalidDatasetIntervalBeforeExecution(string from, string to)
    {
        using var database = new TemporaryDatabase(createDirectory: false);

        WorkerResult result = RunWorker(database.Path, "AAPL", from, to);

        Assert.NotEqual(0, result.ExitCode);
        Assert.Contains("Invalid mandatory dataset configuration.", result.StandardError, StringComparison.Ordinal);
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void WorkerPresentsOnlyTheFirstBoundedStorageFailure()
    {
        using var database = new TemporaryDatabase(createDirectory: false);

        WorkerResult result = RunWorker(database.Path, "AAPL", From, To);

        Assert.NotEqual(0, result.ExitCode);
        Assert.Contains("Stage 1: HistoricalObservationRetrieval = Failed (DependencyUnavailable)", result.StandardError, StringComparison.Ordinal);
        Assert.Contains("Pipeline failure stage: HistoricalObservationRetrieval", result.StandardError, StringComparison.Ordinal);
        Assert.Contains("Pipeline failure category: DependencyUnavailable", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Stage 2:", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Snapshot identity:", result.StandardError, StringComparison.Ordinal);
    }

    private static void AssertSingle(
        IServiceCollection services,
        Type serviceType,
        ServiceLifetime expectedLifetime,
        string expectedImplementationTypeName)
    {
        ServiceDescriptor descriptor = Assert.Single(services, item => item.ServiceType == serviceType);
        Assert.Equal(expectedLifetime, descriptor.Lifetime);
        Assert.Equal(expectedImplementationTypeName, descriptor.ImplementationType?.Name);
    }

    private static WorkerResult RunWorker(
        string databasePath,
        string target,
        DateTimeOffset from,
        DateTimeOffset to,
        string? missingVariable = null) =>
        RunWorker(databasePath, target, from.ToString("O"), to.ToString("O"), missingVariable);

    private static WorkerResult RunWorker(
        string databasePath,
        string target,
        string from,
        string to,
        string? missingVariable = null)
    {
        string repositoryRoot = FindRepositoryRoot();
        string workerProject = Path.Combine(repositoryRoot, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj");
        var startInfo = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = repositoryRoot,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        startInfo.ArgumentList.Add("run");
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(workerProject);
        startInfo.ArgumentList.Add("--no-build");
        startInfo.ArgumentList.Add("--configuration");
        startInfo.ArgumentList.Add("Release");
        startInfo.Environment["TwelveData__ApiKey"] = "wp11-dummy-api-key";
        startInfo.Environment["Persistence__DatabasePath"] = databasePath;
        startInfo.Environment["Dataset__Target"] = target;
        startInfo.Environment["Dataset__From"] = from;
        startInfo.Environment["Dataset__To"] = to;
        if (missingVariable is not null)
        {
            startInfo.Environment.Remove(missingVariable);
        }

        using Process process = Process.Start(startInfo)
            ?? throw new InvalidOperationException("The Worker process could not be started.");
        string standardOutput = process.StandardOutput.ReadToEnd();
        string standardError = process.StandardError.ReadToEnd();
        Assert.True(process.WaitForExit(30_000), "The bounded Worker process did not terminate.");

        return new WorkerResult(process.ExitCode, standardOutput, standardError);
    }

    private static int CountStageLines(string output) => output.Split('\n').Count(static line => line.StartsWith("Stage ", StringComparison.Ordinal));

    private static string ReadEvidenceValue(string output, string marker) =>
        output.Split('\n')
            .Single(line => line.StartsWith(marker, StringComparison.Ordinal))
            .Substring(marker.Length)
            .Trim();

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx")))
            {
                return directory.FullName;
            }
        }

        throw new InvalidOperationException("The repository root could not be located for Worker process validation.");
    }

    private sealed record WorkerResult(int ExitCode, string StandardOutput, string StandardError);

    private sealed class TemporaryDatabase : IDisposable
    {
        private readonly string directory;

        public TemporaryDatabase(bool createDirectory = true)
        {
            directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"aiq-wp11-{Guid.NewGuid():N}");
            if (createDirectory)
            {
                Directory.CreateDirectory(directory);
            }

            Path = System.IO.Path.Combine(directory, "pipeline.sqlite");
            Configuration = new SqliteStorageConfiguration(Path);
            Factory = new SqliteConnectionFactory(Configuration);
            History = new SqliteHistoricalObservationStore(Factory);
        }

        public string Path { get; }

        public SqliteStorageConfiguration Configuration { get; }

        public SqliteConnectionFactory Factory { get; }

        public SqliteHistoricalObservationStore History { get; }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, recursive: true);
            }
        }
    }
}
