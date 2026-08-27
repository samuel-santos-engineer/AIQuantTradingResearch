using System.Diagnostics;
using System.Reflection;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ExperimentDiscoveryTests
{
    private static readonly DateTimeOffset From =
        new(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));

    [Fact]
    public void DiscoverFiltersByBothIdentitiesOrdersBoundsAndPreservesExactReadOnlyEvidence()
    {
        using var database = new TemporaryDatabase();
        DatasetSnapshotCandidate matchingSnapshot = Snapshot('a', Observations());
        DatasetSnapshotCandidate otherSnapshot = Snapshot('b', Observations());
        Seed(database, matchingSnapshot);
        Seed(database, otherSnapshot);
        DurableExperimentEvidence first = AcceptGenerated(database, matchingSnapshot);
        DurableExperimentEvidence second = AlternateEvidence(first);
        DurableExperimentEvidence nonMatching = AcceptGenerated(database, otherSnapshot);
        var store = (IDurableExperimentEvidenceDiscoveryStore)new SqliteExperimentResultStore(database.Factory);
        Assert.Equal(
            DurableExperimentAcceptanceDisposition.NewlyAccepted,
            ((IDurableExperimentEvidenceStore)new SqliteExperimentResultStore(database.Factory))
                .Accept(new DurableExperimentAcceptanceRequest(second)).Disposition);
        string[] expected = new[] { first.Identity.Fingerprint, second.Identity.Fingerprint }
            .Order(StringComparer.Ordinal)
            .ToArray();
        string[] identitiesBefore = database.ResultIdentities();
        long tablesBefore = database.Scalar("SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table';");
        long indexesBefore = database.Scalar("SELECT COUNT(*) FROM sqlite_schema WHERE type = 'index';");

        DurableExperimentDiscoveryResult result = store.Discover(new DurableExperimentDiscoveryRequest(
            matchingSnapshot.SnapshotIdentity,
            first.DefinitionIdentity,
            1));

        Assert.True(result.IsSuccess);
        DurableExperimentEvidence returned = Assert.Single(result.Evidence!);
        Assert.Equal(expected[0], returned.Identity.Fingerprint);
        DurableExperimentEvidence expectedEvidence =
            first.Identity.Fingerprint == expected[0] ? first : second;
        Assert.Equal(expectedEvidence, returned);
        Assert.Equal(matchingSnapshot.SnapshotIdentity, returned.Provenance.SnapshotIdentity);
        Assert.Equal(matchingSnapshot.Version, returned.Provenance.SnapshotVersion);
        Assert.Equal(first.DefinitionIdentity, returned.DefinitionIdentity);
        Assert.DoesNotContain(result.Evidence!, item => item.Identity == nonMatching.Identity);
        Assert.Equal(identitiesBefore, database.ResultIdentities());
        Assert.Equal(tablesBefore, database.Scalar("SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table';"));
        Assert.Equal(indexesBefore, database.Scalar("SELECT COUNT(*) FROM sqlite_schema WHERE type = 'index';"));
        Assert.Equal(4L, database.SchemaVersion());
    }

    [Fact]
    public void DiscoverReturnsAllMatchingEvidenceInBinaryIdentityOrderAndSuccessfulEmptyForNoMatch()
    {
        using var database = new TemporaryDatabase();
        DatasetSnapshotCandidate snapshot = Snapshot('c', Observations());
        Seed(database, snapshot);
        DurableExperimentEvidence first = AcceptGenerated(database, snapshot);
        DurableExperimentEvidence second = AlternateEvidence(first);
        var acceptanceStore = new SqliteExperimentResultStore(database.Factory);
        _ = acceptanceStore.Accept(new DurableExperimentAcceptanceRequest(second));
        var store = (IDurableExperimentEvidenceDiscoveryStore)new SqliteExperimentResultStore(database.Factory);
        string[] expected = new[] { first.Identity.Fingerprint, second.Identity.Fingerprint }
            .Order(StringComparer.Ordinal)
            .ToArray();

        DurableExperimentDiscoveryResult all = store.Discover(new DurableExperimentDiscoveryRequest(
            snapshot.SnapshotIdentity,
            first.DefinitionIdentity,
            10));
        DurableExperimentDiscoveryResult empty = store.Discover(new DurableExperimentDiscoveryRequest(
            new DatasetSnapshotIdentity(new string('d', 64)),
            first.DefinitionIdentity,
            10));

        Assert.Equal(expected, all.Evidence!.Select(static item => item.Identity.Fingerprint));
        Assert.True(empty.IsSuccess);
        Assert.Empty(empty.Evidence!);
        Assert.Null(empty.Failure);
    }

    [Fact]
    public void DiscoverPreservesAcceptedEmptyExperimentResultFidelity()
    {
        using var database = new TemporaryDatabase();
        DatasetSnapshotCandidate snapshot = Snapshot('e', []);
        Seed(database, snapshot);
        DurableExperimentEvidence emptyEvidence = AcceptGenerated(database, snapshot);
        var store = (IDurableExperimentEvidenceDiscoveryStore)new SqliteExperimentResultStore(database.Factory);

        DurableExperimentDiscoveryResult result = store.Discover(new DurableExperimentDiscoveryRequest(
            snapshot.SnapshotIdentity,
            emptyEvidence.DefinitionIdentity,
            1));

        DurableExperimentEvidence returned = Assert.Single(result.Evidence!);
        Assert.Equal(emptyEvidence.Identity, returned.Identity);
        Assert.Equal(0, returned.Summary.Count);
        Assert.False(returned.Summary.HasAggregates);
        Assert.Null(returned.Summary.ArithmeticMean);
        Assert.Equal(emptyEvidence.Provenance, returned.Provenance);
        Assert.Equal(emptyEvidence.Lineage, returned.Lineage);
    }

    [Fact]
    public void DiscoverMapsUnavailableAndSchemaValidationFailuresWithoutCreationOrFallback()
    {
        using var unavailable = new TemporaryDatabase(createDirectory: false);
        var unavailableStore = (IDurableExperimentEvidenceDiscoveryStore)new SqliteExperimentResultStore(
            unavailable.Factory);
        var schemaStore = (IDurableExperimentEvidenceDiscoveryStore)new SqliteExperimentResultStore(
            new SchemaFailureFactory());
        var request = new DurableExperimentDiscoveryRequest(
            new DatasetSnapshotIdentity(new string('a', 64)),
            new ExperimentDefinitionIdentity(new string('b', 64)),
            1);

        DurableExperimentDiscoveryResult unavailableResult = unavailableStore.Discover(request);
        DurableExperimentDiscoveryResult schemaResult = schemaStore.Discover(request);

        Assert.Equal(DurableExperimentEvidenceFailure.DependencyUnavailable, unavailableResult.Failure);
        Assert.False(File.Exists(unavailable.Path));
        Assert.Equal(DurableExperimentEvidenceFailure.InvalidEvidence, schemaResult.Failure);
    }

    [Fact]
    public void DiscoverDoesNotNormalizeUnknownFactoryDefect()
    {
        var store = (IDurableExperimentEvidenceDiscoveryStore)new SqliteExperimentResultStore(
            new UnknownFailureFactory());

        Assert.Throws<UnknownProbeException>(() => store.Discover(new DurableExperimentDiscoveryRequest(
            new DatasetSnapshotIdentity(new string('a', 64)),
            new ExperimentDefinitionIdentity(new string('b', 64)),
            1)));
    }

    [Fact]
    public void CompositionRegistersSingleForwardedDiscoveryGraphWithoutResolutionSideEffects()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure(new TwelveDataConfiguration("wp11-dummy-api-key"), database.Configuration);

        Assert.Single(services, descriptor =>
            descriptor.ServiceType == typeof(IDurableExperimentDiscoveryUseCase));
        Assert.Single(services, descriptor =>
            descriptor.ServiceType == typeof(IDurableExperimentEvidenceDiscoveryStore));
        Assert.Single(services, descriptor => descriptor.ServiceType == typeof(SqliteExperimentResultStore));
        Assert.NotNull(services.Single(descriptor =>
            descriptor.ServiceType == typeof(IDurableExperimentEvidenceDiscoveryStore)).ImplementationFactory);
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = true,
        });
        Assert.Equal(
            "DurableExperimentDiscoveryUseCase",
            provider.GetRequiredService<IDurableExperimentDiscoveryUseCase>().GetType().Name);
        Assert.IsType<SqliteExperimentResultStore>(
            provider.GetRequiredService<IDurableExperimentEvidenceDiscoveryStore>());
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void WorkerDiscoversBoundedOrderedEvidenceAndPreservesLowerModePrecedence()
    {
        using var database = new TemporaryDatabase();
        DatasetSnapshotCandidate snapshot = Snapshot('f', Observations());
        Seed(database, snapshot);
        DurableExperimentEvidence first = AcceptGenerated(database, snapshot);
        DurableExperimentEvidence second = AlternateEvidence(first);
        _ = ((IDurableExperimentEvidenceStore)new SqliteExperimentResultStore(database.Factory))
            .Accept(new DurableExperimentAcceptanceRequest(second));
        string expected = new[] { first.Identity.Fingerprint, second.Identity.Fingerprint }
            .Order(StringComparer.Ordinal)
            .First();

        WorkerResult result = RunWorker(
            database.Path,
            snapshot.SnapshotIdentity.Fingerprint,
            first.DefinitionIdentity.Fingerprint,
            "1",
            includeLowerSelectors: true);

        Assert.Equal(0, result.ExitCode);
        Assert.Contains("Mode: Durable Experiment Evidence Discovery", result.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Returned count: 1", result.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(expected, ReadResultIdentities(result.StandardOutput).Single());
        Assert.DoesNotContain("Durable experiment disposition:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Experiment definition:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature definition:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Pipeline definition identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.Empty(result.StandardError);
    }

    [Fact]
    public void WorkerSupportsEmptyDiscoveryAndRejectsPartialDiscoveryWithoutFallback()
    {
        using var database = new TemporaryDatabase();
        WorkerResult empty = RunWorker(
            database.Path,
            new string('a', 64),
            new string('b', 64),
            "1");
        WorkerResult partial = RunWorker(
            database.Path,
            new string('a', 64),
            null,
            "1",
            includeLowerSelectors: true);

        Assert.Equal(0, empty.ExitCode);
        Assert.Contains("Returned count: 0", empty.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("NotFound", empty.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(1, partial.ExitCode);
        Assert.Contains(
            "Invalid mandatory durable experiment discovery configuration.",
            partial.StandardError,
            StringComparison.Ordinal);
        Assert.Empty(partial.StandardOutput);
    }

    private static PriceObservation[] Observations() =>
    [
        new PriceObservation(From.AddHours(1), 10m),
        new PriceObservation(From.AddHours(2), 12.5m),
        new PriceObservation(From.AddHours(3), 10m),
    ];

    private static DurableExperimentEvidence AcceptGenerated(
        TemporaryDatabase database,
        DatasetSnapshotCandidate snapshot)
    {
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure(new TwelveDataConfiguration("wp11-dummy-api-key"), database.Configuration);
        using var provider = services.BuildServiceProvider();
        DurableExperimentUseCaseResult result = provider
            .GetRequiredService<IDurableExperimentUseCase>()
            .Execute(new ExperimentGenerationRequest(
                ExperimentDefinition.SimpleReturnDescriptiveSummaryV1,
                snapshot.SnapshotIdentity,
                snapshot.Version));
        Assert.True(result.IsSuccess);
        return result.Evidence!;
    }

    private static DurableExperimentEvidence AlternateEvidence(DurableExperimentEvidence first)
    {
        var featureSetIdentity = new FeatureSetIdentity(new string('9', 64));
        var summary = new ExperimentSummaryEvidence(1, 2m, 2m, 2m);
        ExperimentResultIdentity identity = (ExperimentResultIdentity)typeof(ExperimentDefinition)
            .Assembly
            .GetType("AIQuantTradingResearch.Application.Experiments.ExperimentIdentityComputer", true)!
            .GetMethod(
                "ComputeResultIdentity",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)!
            .Invoke(null, [first.DefinitionIdentity, featureSetIdentity, summary])!;
        DurableExperimentProvenance provenance = first.Provenance;
        return new DurableExperimentEvidence(
            first.Definition,
            first.DefinitionIdentity,
            identity,
            summary,
            new DurableExperimentProvenance(
                provenance.DefinitionIdentity,
                featureSetIdentity,
                provenance.FeatureDefinitionIdentity,
                provenance.SnapshotIdentity,
                provenance.SnapshotVersion,
                provenance.DatasetDefinitionIdentity,
                provenance.ResearchDatasetIdentity,
                provenance.SourceStateIdentity,
                provenance.SourceAuthority,
                provenance.DatasetObservationCount),
            first.Lineage);
    }

    private static DatasetSnapshotCandidate Snapshot(char identity, PriceObservation[] observations)
    {
        DateTimeOffset to = From.AddDays(3);
        var definition = new DatasetDefinition("AAPL", From, to);
        var definitionIdentity = new DatasetDefinitionIdentity(new string('1', 64));
        var researchIdentity = new ResearchDatasetIdentity(new string('2', 64));
        var sourceIdentity = new SourceStateIdentity(new string('3', 64));
        var snapshotIdentity = new DatasetSnapshotIdentity(new string(identity, 64));
        var version = new DatasetVersion(snapshotIdentity);
        var coverage = new DatasetCoverage(
            From,
            to,
            observations.Length,
            observations.Length == 0 ? null : observations[0].Instant,
            observations.Length == 0 ? null : observations[^1].Instant);
        return new DatasetSnapshotCandidate(
            definition,
            definitionIdentity,
            researchIdentity,
            sourceIdentity,
            snapshotIdentity,
            version,
            observations,
            coverage,
            new DatasetProvenance(
                definition,
                definitionIdentity,
                researchIdentity,
                sourceIdentity,
                snapshotIdentity,
                version,
                DatasetSourceAuthority.AcceptedRelease11HistoricalObservations,
                observations.Length),
            new DatasetLineage(definitionIdentity, sourceIdentity, observations));
    }

    private static void Seed(TemporaryDatabase database, DatasetSnapshotCandidate snapshot) =>
        Assert.Equal(
            DatasetSnapshotStoreOutcome.NewlyAccepted,
            new SqliteDatasetSnapshotStore(database.Factory).Store(snapshot).Outcome);

    private static WorkerResult RunWorker(
        string databasePath,
        string snapshotIdentity,
        string? definitionIdentity,
        string? maximum,
        bool includeLowerSelectors = false)
    {
        string root = FindRepositoryRoot();
        string handoffPath = Path.Combine(Path.GetTempPath(), $"aiq-wp05-handoff-{Guid.NewGuid():N}.json");
        var startInfo = new ProcessStartInfo("dotnet")
        {
            WorkingDirectory = root,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
        };
        startInfo.ArgumentList.Add("run");
        startInfo.ArgumentList.Add("--project");
        startInfo.ArgumentList.Add(Path.Combine(root, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj"));
        startInfo.ArgumentList.Add("--no-build");
        startInfo.ArgumentList.Add("--configuration");
        startInfo.ArgumentList.Add("Release");
        startInfo.Environment["TwelveData__ApiKey"] = "wp11-dummy-api-key";
        startInfo.Environment["Persistence__DatabasePath"] = databasePath;
        startInfo.Environment["Visualization__HandoffPath"] = handoffPath;
        startInfo.Environment["DurableExperimentDiscovery__SnapshotIdentity"] = snapshotIdentity;
        if (definitionIdentity is not null) startInfo.Environment["DurableExperimentDiscovery__ExperimentDefinitionIdentity"] = definitionIdentity;
        if (maximum is not null) startInfo.Environment["DurableExperimentDiscovery__MaximumResultCount"] = maximum;
        if (includeLowerSelectors)
        {
            startInfo.Environment["DurableExperiment__SnapshotIdentity"] = snapshotIdentity;
            startInfo.Environment["DurableExperiment__SnapshotVersion"] = snapshotIdentity;
            startInfo.Environment["Experiment__SnapshotIdentity"] = snapshotIdentity;
            startInfo.Environment["Experiment__SnapshotVersion"] = snapshotIdentity;
            startInfo.Environment["Feature__SnapshotIdentity"] = snapshotIdentity;
            startInfo.Environment["Feature__SnapshotVersion"] = snapshotIdentity;
        }

        try
        {
            using Process process = Process.Start(startInfo) ?? throw new InvalidOperationException("Worker did not start.");
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            Assert.True(process.WaitForExit(30_000), "Worker did not terminate within the bounded timeout.");
            return new WorkerResult(process.ExitCode, output, error);
        }
        finally { File.Delete(handoffPath); }
    }

    private static string[] ReadResultIdentities(string output) => output.Split('\n')
        .Where(static line => line.StartsWith("Experiment result ", StringComparison.Ordinal)
            && line.Contains(" identity: ", StringComparison.Ordinal)
            && !line.Contains(" snapshot identity: ", StringComparison.Ordinal)
            && !line.Contains(" definition identity: ", StringComparison.Ordinal)
            && !line.Contains(" set identity: ", StringComparison.Ordinal)
            && !line.Contains(" dataset version identity: ", StringComparison.Ordinal)
            && !line.Contains(" research dataset identity: ", StringComparison.Ordinal)
            && !line.Contains(" source state identity: ", StringComparison.Ordinal))
        .Select(static line => line[(line.IndexOf(": ", StringComparison.Ordinal) + 2)..].Trim())
        .ToArray();

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName;
        }

        throw new InvalidOperationException("Repository root was not found.");
    }

    private sealed class SchemaFailureFactory : ISqliteConnectionFactory
    {
        public SqliteConnection OpenConnection() => throw new SqliteSchemaValidationException("safe test schema failure");
    }

    private sealed class UnknownFailureFactory : ISqliteConnectionFactory
    {
        public SqliteConnection OpenConnection() => throw new UnknownProbeException();
    }

    private sealed class UnknownProbeException : Exception;

    private sealed record WorkerResult(int ExitCode, string StandardOutput, string StandardError);

    private sealed class TemporaryDatabase : IDisposable
    {
        private readonly string directory;

        public TemporaryDatabase(bool createDirectory = true)
        {
            directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"aiq-wp11-discovery-{Guid.NewGuid():N}");
            if (createDirectory) Directory.CreateDirectory(directory);
            Path = System.IO.Path.Combine(directory, "discovery.sqlite");
            Configuration = new SqliteStorageConfiguration(Path);
            Factory = new SqliteConnectionFactory(Configuration);
        }

        public string Path { get; }
        public SqliteStorageConfiguration Configuration { get; }
        public SqliteConnectionFactory Factory { get; }

        public long Scalar(string sql)
        {
            using var connection = Factory.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = sql;
            return (long)command.ExecuteScalar()!;
        }

        public long SchemaVersion() => Scalar("PRAGMA user_version;");

        public string[] ResultIdentities()
        {
            using var connection = Factory.OpenConnection();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT experiment_result_identity FROM experiment_results ORDER BY experiment_result_identity COLLATE BINARY ASC;";
            using var reader = command.ExecuteReader();
            var identities = new List<string>();
            while (reader.Read()) identities.Add(reader.GetString(0));
            return identities.ToArray();
        }

        public void Dispose()
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
        }
    }
}
