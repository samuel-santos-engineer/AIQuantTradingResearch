using System.Diagnostics;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
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

public sealed class FeatureCompositionTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));

    [Fact]
    public void CompositionRegistersAndResolvesTheFeatureGraphWithoutDatabaseSideEffects()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure(new TwelveDataConfiguration("wp12-dummy-api-key"), database.Configuration);

        AssertSingle(services, typeof(IFeatureGenerationUseCase), ServiceLifetime.Transient, "FeatureGenerationUseCase");
        AssertSingle(services, typeof(IFeatureComputer), ServiceLifetime.Transient, "SimpleReturnFeatureComputer");
        AssertSingle(services, typeof(IFeatureGenerationValidator), ServiceLifetime.Transient, "FeatureGenerationValidator");
        AssertSingle(services, typeof(IDatasetSnapshotStore), ServiceLifetime.Transient, "SqliteDatasetSnapshotStore");

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        object first = provider.GetRequiredService<IFeatureGenerationUseCase>();
        object second = provider.GetRequiredService<IFeatureGenerationUseCase>();
        _ = provider.GetRequiredService<IFeatureComputer>();
        _ = provider.GetRequiredService<IFeatureGenerationValidator>();
        _ = provider.GetRequiredService<IDatasetSnapshotStore>();

        Assert.Equal("FeatureGenerationUseCase", first.GetType().Name);
        Assert.NotSame(first, second);
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void WorkerExecutesNonEmptyFeatureOnceAndEquivalentRerunPreservesIdentity()
    {
        using var database = new TemporaryDatabase();
        var snapshot = Snapshot('a',
        [
            new PriceObservation(From.AddHours(1), 10m),
            new PriceObservation(From.AddHours(2), 12.5m),
        ]);
        Seed(database, snapshot);

        WorkerResult first = RunWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);
        WorkerResult second = RunWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);

        Assert.Equal(0, first.ExitCode);
        Assert.Equal(0, second.ExitCode);
        Assert.Contains("Feature definition: simple-return-lag-1-v1", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Feature value count: 1", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("= 0.25", first.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(ReadValue(first.StandardOutput, "Feature definition identity: "), ReadValue(second.StandardOutput, "Feature definition identity: "));
        Assert.Equal(ReadValue(first.StandardOutput, "Feature set identity: "), ReadValue(second.StandardOutput, "Feature set identity: "));
        Assert.Equal(snapshot.SnapshotIdentity.Fingerprint, ReadValue(first.StandardOutput, "Snapshot identity: "));
        Assert.Equal(snapshot.Version.SnapshotIdentity.Fingerprint, ReadValue(first.StandardOutput, "Dataset version identity: "));
        Assert.Empty(first.StandardError);
        AssertNoFeatureTables(database);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void WorkerTreatsExistingEmptyAndSingleSnapshotsAsSuccessful(bool empty)
    {
        using var database = new TemporaryDatabase();
        var observations = empty
            ? Array.Empty<PriceObservation>()
            : [new PriceObservation(From.AddHours(1), 10m)];
        var snapshot = Snapshot(empty ? 'b' : 'c', observations);
        Seed(database, snapshot);

        WorkerResult result = RunWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);

        Assert.Equal(0, result.ExitCode);
        Assert.Contains("Feature value count: 0", result.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Feature set identity: ", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature failure:", result.StandardError, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData("missing-version")]
    [InlineData("malformed-identity")]
    public void WorkerRejectsInvalidFeatureConfigurationBeforeExecution(string scenario)
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        string identity = scenario == "malformed-identity" ? "not-a-fingerprint" : Fingerprint('d');
        WorkerResult result = RunWorker(database.Path, identity, Fingerprint('d'), scenario == "missing-version");

        Assert.NotEqual(0, result.ExitCode);
        Assert.Contains("Invalid mandatory feature configuration.", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature set identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void WorkerReportsExactSnapshotNotFoundWithoutFabricatedIdentity()
    {
        using var database = new TemporaryDatabase();
        WorkerResult result = RunWorker(database.Path, Fingerprint('e'), Fingerprint('e'));

        Assert.NotEqual(0, result.ExitCode);
        Assert.Contains("Feature failure: SnapshotNotFound", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature set identity:", result.StandardOutput, StringComparison.Ordinal);
    }

    [Fact]
    public void WorkerReportsUnavailableStorageWithoutProviderFallback()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        WorkerResult result = RunWorker(database.Path, Fingerprint('f'), Fingerprint('f'));

        Assert.NotEqual(0, result.ExitCode);
        Assert.Contains("Feature failure: DependencyUnavailable", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature set identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("wp12-dummy-api-key", result.StandardError, StringComparison.Ordinal);
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
        var result = new SqliteDatasetSnapshotStore(database.Factory).Store(snapshot);
        Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, result.Outcome);
        using var connection = database.Factory.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA user_version;";
        Assert.Equal(2L, (long)command.ExecuteScalar()!);
    }

    private static void AssertNoFeatureTables(TemporaryDatabase database)
    {
        using var connection = database.Factory.OpenConnection();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table' AND name LIKE 'feature%';";
        Assert.Equal(0L, (long)command.ExecuteScalar()!);
    }

    private static WorkerResult RunWorker(string databasePath, string identity, string version, bool omitVersion = false)
    {
        string root = FindRepositoryRoot();
        var startInfo = new ProcessStartInfo("dotnet") { WorkingDirectory = root, RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false };
        startInfo.ArgumentList.Add("run");
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(Path.Combine(root, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj"));
        startInfo.ArgumentList.Add("--no-build");
        startInfo.ArgumentList.Add("--configuration");
        startInfo.ArgumentList.Add("Release");
        startInfo.Environment["TwelveData__ApiKey"] = "wp12-dummy-api-key";
        startInfo.Environment["Persistence__DatabasePath"] = databasePath;
        startInfo.Environment["Feature__SnapshotIdentity"] = identity;
        if (!omitVersion) startInfo.Environment["Feature__SnapshotVersion"] = version;

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
            directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"aiq-wp12-feature-{Guid.NewGuid():N}");
            if (createDirectory) Directory.CreateDirectory(directory);
            Path = System.IO.Path.Combine(directory, "feature.sqlite");
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
