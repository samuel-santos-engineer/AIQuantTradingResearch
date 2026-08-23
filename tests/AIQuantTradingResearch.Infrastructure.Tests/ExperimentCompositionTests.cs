using System.Diagnostics;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ExperimentCompositionTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));

    [Fact]
    public void CompositionRegistersAndResolvesExperimentGraphWithoutDatabaseSideEffects()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure(new TwelveDataConfiguration("wp11-dummy-api-key"), database.Configuration);

        AssertSingle(services, typeof(IExperimentGenerationUseCase), ServiceLifetime.Transient, "ExperimentGenerationUseCase");
        AssertSingle(services, typeof(IExperimentSummaryComputer), ServiceLifetime.Transient, "SimpleReturnDescriptiveSummaryComputer");
        AssertSingle(services, typeof(IExperimentGenerationValidator), ServiceLifetime.Transient, "ExperimentGenerationValidator");

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        object first = provider.GetRequiredService<IExperimentGenerationUseCase>();
        object second = provider.GetRequiredService<IExperimentGenerationUseCase>();
        _ = provider.GetRequiredService<IExperimentSummaryComputer>();
        _ = provider.GetRequiredService<IExperimentGenerationValidator>();
        _ = provider.GetRequiredService<IFeatureGenerationUseCase>();

        Assert.Equal("ExperimentGenerationUseCase", first.GetType().Name);
        Assert.NotSame(first, second);
        Assert.False(File.Exists(database.Path));
        Assert.False(File.Exists(database.Path + "-wal"));
        Assert.False(File.Exists(database.Path + "-shm"));
        Assert.False(File.Exists(database.Path + "-journal"));
    }

    [Fact]
    public void WorkerExecutesNonEmptyExperimentAndEquivalentRerunPreservesIdentity()
    {
        using var database = new TemporaryDatabase();
        var snapshot = Snapshot('a',
        [
            new PriceObservation(From.AddHours(1), 10m),
            new PriceObservation(From.AddHours(2), 12.5m),
            new PriceObservation(From.AddHours(3), 10m),
        ]);
        Seed(database, snapshot);

        WorkerResult first = RunWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);
        WorkerResult second = RunWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);

        Assert.Equal(0, first.ExitCode);
        Assert.Equal(0, second.ExitCode);
        Assert.Contains("Experiment definition: simple-return-descriptive-summary-v1", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Experiment value count: 2", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Experiment arithmetic mean: 0.025", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Experiment minimum: -0.2", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Experiment maximum: 0.25", first.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(snapshot.SnapshotIdentity.Fingerprint, ReadValue(first.StandardOutput, "Snapshot identity: "));
        Assert.Equal(ReadValue(first.StandardOutput, "Feature set identity: "), ReadValue(second.StandardOutput, "Feature set identity: "));
        Assert.Equal(ReadValue(first.StandardOutput, "Experiment result identity: "), ReadValue(second.StandardOutput, "Experiment result identity: "));
        Assert.Empty(first.StandardError);
        AssertExpectedExperimentResultTable(database);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void WorkerTreatsEmptyAndSingleSnapshotsAsSuccessfulEmptyExperiments(bool empty)
    {
        using var database = new TemporaryDatabase();
        var observations = empty
            ? Array.Empty<PriceObservation>()
            : [new PriceObservation(From.AddHours(1), 10m)];
        var snapshot = Snapshot(empty ? 'b' : 'c', observations);
        Seed(database, snapshot);

        WorkerResult result = RunWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);

        Assert.Equal(0, result.ExitCode);
        Assert.Contains("Experiment value count: 0", result.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Experiment aggregates: absent", result.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Experiment result identity: ", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Experiment arithmetic mean:", result.StandardOutput, StringComparison.Ordinal);
        Assert.Empty(result.StandardError);
        AssertExpectedExperimentResultTable(database);
    }

    [Theory]
    [InlineData("malformed")]
    [InlineData("partial-with-feature")]
    public void WorkerRejectsMalformedOrPartialExperimentIntentWithoutFallback(string scenario)
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        string identity = scenario == "malformed" ? "not-a-fingerprint" : Fingerprint('d');
        WorkerResult result = RunWorker(
            database.Path,
            identity,
            scenario == "partial-with-feature" ? null : Fingerprint('d'),
            featureIdentity: Fingerprint('a'),
            featureVersion: Fingerprint('a'));

        Assert.Equal(1, result.ExitCode);
        Assert.Contains("Invalid mandatory experiment configuration.", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Experiment result identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature definition:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Pipeline definition identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void WorkerReportsNotFoundWithoutProviderFallbackOrFabricatedIdentity()
    {
        using var database = new TemporaryDatabase();

        WorkerResult result = RunWorker(database.Path, Fingerprint('e'), Fingerprint('e'));

        Assert.Equal(1, result.ExitCode);
        Assert.Contains("Experiment failure: FeatureSetNotFound", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Experiment result identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("wp11-dummy-api-key", string.Concat(result.StandardOutput, result.StandardError), StringComparison.Ordinal);
        AssertExpectedExperimentResultTable(database);
    }

    [Fact]
    public void WorkerReportsUnavailableStorageWithoutProviderFallbackOrFabricatedIdentity()
    {
        using var database = new TemporaryDatabase(createDirectory: false);

        WorkerResult result = RunWorker(database.Path, Fingerprint('f'), Fingerprint('f'));

        Assert.Equal(1, result.ExitCode);
        Assert.Contains("Experiment failure: DependencyUnavailable", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Experiment result identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("wp11-dummy-api-key", string.Concat(result.StandardOutput, result.StandardError), StringComparison.Ordinal);
    }

    private static void AssertSingle(IServiceCollection services, Type type, ServiceLifetime lifetime, string implementation)
    {
        ServiceDescriptor descriptor = Assert.Single(services, item => item.ServiceType == type);
        Assert.Equal(lifetime, descriptor.Lifetime);
        Assert.Equal(implementation, descriptor.ImplementationType?.Name);
    }

    private static DatasetSnapshotCandidate Snapshot(char identity, PriceObservation[] observations)
    {
        DateTimeOffset to = From.AddDays(3);
        var definition = new DatasetDefinition("AAPL", From, to);
        var definitionIdentity = new DatasetDefinitionIdentity(Fingerprint('1'));
        var researchIdentity = new ResearchDatasetIdentity(Fingerprint('2'));
        var sourceIdentity = new SourceStateIdentity(Fingerprint('3'));
        var snapshotIdentity = new DatasetSnapshotIdentity(Fingerprint(identity));
        var version = new DatasetVersion(snapshotIdentity);
        DateTimeOffset? firstObservation = observations.Length == 0 ? null : observations[0].Instant;
        DateTimeOffset? lastObservation = observations.Length == 0 ? null : observations[^1].Instant;
        var coverage = new DatasetCoverage(From, to, observations.Length, firstObservation, lastObservation);
        var provenance = new DatasetProvenance(definition, definitionIdentity, researchIdentity, sourceIdentity, snapshotIdentity, version, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, observations.Length);
        var lineage = new DatasetLineage(definitionIdentity, sourceIdentity, observations);
        return new DatasetSnapshotCandidate(definition, definitionIdentity, researchIdentity, sourceIdentity, snapshotIdentity, version, observations, coverage, provenance, lineage);
    }

    private static void Seed(TemporaryDatabase database, DatasetSnapshotCandidate snapshot)
    {
        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, new SqliteDatasetSnapshotStore(database.Factory).Store(snapshot).Outcome);
        using var connection = database.Factory.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA user_version;";
        Assert.Equal(3L, (long)command.ExecuteScalar()!);
    }

    private static void AssertExpectedExperimentResultTable(TemporaryDatabase database)
    {
        using var connection = database.Factory.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table' AND name = 'experiment_results';";
        Assert.Equal(1L, (long)command.ExecuteScalar()!);
        command.CommandText = "SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table' AND (name LIKE 'experiment%' AND name <> 'experiment_results');";
        Assert.Equal(0L, (long)command.ExecuteScalar()!);
    }

    private static WorkerResult RunWorker(string databasePath, string identity, string? version, string? featureIdentity = null, string? featureVersion = null)
    {
        string root = FindRepositoryRoot();
        var startInfo = new ProcessStartInfo("dotnet") { WorkingDirectory = root, RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false };
        startInfo.ArgumentList.Add("run");
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(Path.Combine(root, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj"));
        startInfo.ArgumentList.Add("--no-build");
        startInfo.ArgumentList.Add("--configuration");
        startInfo.ArgumentList.Add("Release");
        startInfo.Environment["TwelveData__ApiKey"] = "wp11-dummy-api-key";
        startInfo.Environment["Persistence__DatabasePath"] = databasePath;
        startInfo.Environment["Experiment__SnapshotIdentity"] = identity;
        if (version is not null) startInfo.Environment["Experiment__SnapshotVersion"] = version;
        if (featureIdentity is not null) startInfo.Environment["Feature__SnapshotIdentity"] = featureIdentity;
        if (featureVersion is not null) startInfo.Environment["Feature__SnapshotVersion"] = featureVersion;

        using Process process = Process.Start(startInfo) ?? throw new InvalidOperationException("Worker process did not start.");
        string standardOutput = process.StandardOutput.ReadToEnd();
        string standardError = process.StandardError.ReadToEnd();
        Assert.True(process.WaitForExit(30_000), "Worker did not terminate within the bounded timeout.");
        return new WorkerResult(process.ExitCode, standardOutput, standardError);
    }

    private static string ReadValue(string output, string marker) => output.Split('\n').Single(line => line.StartsWith(marker, StringComparison.Ordinal))[marker.Length..].Trim();
    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName;
        throw new InvalidOperationException("Repository root was not found.");
    }
    private static string Fingerprint(char character) => new(character, 64);
    private sealed record WorkerResult(int ExitCode, string StandardOutput, string StandardError);
    private sealed class TemporaryDatabase : IDisposable
    {
        private readonly string directory;

        public TemporaryDatabase(bool createDirectory = true)
        {
            directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"aiq-wp11-experiment-{Guid.NewGuid():N}");
            if (createDirectory) Directory.CreateDirectory(directory);
            Path = System.IO.Path.Combine(directory, "experiment.sqlite");
            Configuration = new SqliteStorageConfiguration(Path);
            Factory = new SqliteConnectionFactory(Configuration);
        }

        public string Path { get; }
        public SqliteStorageConfiguration Configuration { get; }
        public SqliteConnectionFactory Factory { get; }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
        }
    }
}
