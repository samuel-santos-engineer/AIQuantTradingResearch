using System.Diagnostics;
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

public sealed class ExperimentPersistenceTests
{
    [Fact]
    public void WorkerDurableModeAcceptsThenReturnsEquivalentExistingWithOneLogicalRow()
    {
        using var database = new TemporaryDatabase();
        var snapshot = Snapshot('d', [new PriceObservation(From.AddHours(1), 10m), new PriceObservation(From.AddHours(2), 12.5m), new PriceObservation(From.AddHours(3), 10m)]);
        Seed(database, snapshot);
        WorkerResult first = RunDurableWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);
        WorkerResult second = RunDurableWorker(database.Path, snapshot.SnapshotIdentity.Fingerprint, snapshot.Version.SnapshotIdentity.Fingerprint);

        Assert.Equal(0, first.ExitCode); Assert.Equal(0, second.ExitCode);
        Assert.Contains("Durable experiment disposition: NewlyAccepted", first.StandardOutput, StringComparison.Ordinal);
        Assert.Contains("Durable experiment disposition: EquivalentExisting", second.StandardOutput, StringComparison.Ordinal);
        Assert.Equal(ReadValue(first.StandardOutput, "Experiment result identity: "), ReadValue(second.StandardOutput, "Experiment result identity: "));
        Assert.Equal(1L, Scalar(database, "SELECT COUNT(*) FROM experiment_results;"));
        Assert.Empty(first.StandardError); Assert.Empty(second.StandardError);
    }

    [Fact]
    public void WorkerDurablePartialIntentPreventsFeatureFallback()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        WorkerResult result = RunDurableWorker(database.Path, Fingerprint('e'), null, includeFeatureSelectors: true);

        Assert.Equal(1, result.ExitCode);
        Assert.Contains("Invalid mandatory durable experiment configuration.", result.StandardError, StringComparison.Ordinal);
        Assert.DoesNotContain("Feature definition:", result.StandardOutput, StringComparison.Ordinal);
        Assert.DoesNotContain("Pipeline definition identity:", result.StandardOutput, StringComparison.Ordinal);
        Assert.False(File.Exists(database.Path));
    }

    [Fact]
    public void FreshSchemaV3PersistsRetrievesAndEquatesExactEvidence()
    {
        using var database = new TemporaryDatabase();
        var snapshot = Snapshot('a', [new PriceObservation(From.AddHours(1), 10m), new PriceObservation(From.AddHours(2), 12.5m), new PriceObservation(From.AddHours(3), 10m)]);
        Seed(database, snapshot);
        DurableExperimentEvidence evidence = Evidence(snapshot);
        IDurableExperimentEvidenceStore store = new SqliteExperimentResultStore(database.Factory);

        Assert.Equal(DurableExperimentAcceptanceDisposition.NewlyAccepted, store.Accept(new DurableExperimentAcceptanceRequest(evidence)).Disposition);
        Assert.Equal(DurableExperimentAcceptanceDisposition.EquivalentExisting, store.Accept(new DurableExperimentAcceptanceRequest(evidence)).Disposition);
        DurableExperimentEvidence found = store.Retrieve(new DurableExperimentRetrievalRequest(evidence.Identity)).Evidence!;
        Assert.Equal(evidence, found);
        Assert.Equal(1L, Scalar(database, "SELECT COUNT(*) FROM experiment_results;"));
        Assert.Equal(3L, Scalar(database, "PRAGMA user_version;"));
        Assert.Equal(0L, Scalar(database, "SELECT COUNT(*) FROM sqlite_schema WHERE type = 'table' AND name LIKE 'feature%';"));
    }

    [Fact]
    public void ConflictingEvidenceDoesNotOverwriteAndMissingIdentityIsNotFound()
    {
        using var database = new TemporaryDatabase();
        var snapshot = Snapshot('b', []); Seed(database, snapshot);
        DurableExperimentEvidence evidence = Evidence(snapshot);
        IDurableExperimentEvidenceStore store = new SqliteExperimentResultStore(database.Factory);
        _ = store.Accept(new DurableExperimentAcceptanceRequest(evidence));
        var conflictingProvenance = new DurableExperimentProvenance(evidence.Provenance.DefinitionIdentity, evidence.Provenance.FeatureSetIdentity, evidence.Provenance.FeatureDefinitionIdentity, evidence.Provenance.SnapshotIdentity, evidence.Provenance.SnapshotVersion, evidence.Provenance.DatasetDefinitionIdentity, evidence.Provenance.ResearchDatasetIdentity, evidence.Provenance.SourceStateIdentity, evidence.Provenance.SourceAuthority, evidence.Provenance.DatasetObservationCount + 1);
        DurableExperimentEvidence conflict = new(evidence.Definition, evidence.DefinitionIdentity, evidence.Identity, evidence.Summary, conflictingProvenance, evidence.Lineage);
        Assert.Equal(DurableExperimentEvidenceFailure.IntegrityConflict, store.Accept(new DurableExperimentAcceptanceRequest(conflict)).Failure);
        Assert.Equal(1L, Scalar(database, "SELECT COUNT(*) FROM experiment_results;"));
        Assert.Equal(DurableExperimentEvidenceFailure.NotFound, store.Retrieve(new DurableExperimentRetrievalRequest(new ExperimentResultIdentity(new string('f', 64)))).Failure);
    }

    [Fact]
    public void CompositionRegistersDurableGraphOnceWithoutDatabaseSideEffects()
    {
        using var database = new TemporaryDatabase(createDirectory: false);
        var services = new ServiceCollection(); services.AddApplication(); services.AddInfrastructure(new TwelveDataConfiguration("wp12-dummy-key"), database.Configuration);
        Assert.Single(services, d => d.ServiceType == typeof(IDurableExperimentUseCase));
        Assert.Single(services, d => d.ServiceType == typeof(IDurableExperimentEvidenceStore));
        using var provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
        _ = provider.GetRequiredService<IDurableExperimentUseCase>(); _ = provider.GetRequiredService<IDurableExperimentEvidenceStore>();
        Assert.False(File.Exists(database.Path));
    }

    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static long Scalar(TemporaryDatabase database, string sql) { using var c = database.Factory.OpenConnection(); using var command = c.CreateCommand(); command.CommandText = sql; return (long)command.ExecuteScalar()!; }
    private static void Seed(TemporaryDatabase database, DatasetSnapshotCandidate snapshot) => Assert.Equal(DatasetSnapshotStoreOutcome.NewlyAccepted, new SqliteDatasetSnapshotStore(database.Factory).Store(snapshot).Outcome);
    private static DatasetSnapshotCandidate Snapshot(char identity, PriceObservation[] observations)
    {
        var definition = new DatasetDefinition("AAPL", From, From.AddDays(3)); var definitionIdentity = new DatasetDefinitionIdentity(Fingerprint('1')); var researchIdentity = new ResearchDatasetIdentity(Fingerprint('2')); var sourceIdentity = new SourceStateIdentity(Fingerprint('3')); var snapshotIdentity = new DatasetSnapshotIdentity(Fingerprint(identity)); var version = new DatasetVersion(snapshotIdentity);
        var coverage = new DatasetCoverage(From, From.AddDays(3), observations.Length, observations.Length == 0 ? null : observations[0].Instant, observations.Length == 0 ? null : observations[^1].Instant);
        return new DatasetSnapshotCandidate(definition, definitionIdentity, researchIdentity, sourceIdentity, snapshotIdentity, version, observations, coverage, new DatasetProvenance(definition, definitionIdentity, researchIdentity, sourceIdentity, snapshotIdentity, version, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, observations.Length), new DatasetLineage(definitionIdentity, sourceIdentity, observations));
    }
    private static DurableExperimentEvidence Evidence(DatasetSnapshotCandidate snapshot)
    {
        using var database = new TemporaryDatabase();
        Seed(database, snapshot);
        var services = new ServiceCollection(); services.AddApplication(); services.AddInfrastructure(new TwelveDataConfiguration("wp12-dummy-key"), database.Configuration);
        using var provider = services.BuildServiceProvider();
        var result = provider.GetRequiredService<IExperimentGenerationUseCase>().Execute(new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, snapshot.SnapshotIdentity, snapshot.Version));
        ExperimentResult experiment = result.Experiment!;
        var feature = experiment.FeatureSet;
        return new DurableExperimentEvidence(experiment.Definition, experiment.DefinitionIdentity, experiment.Identity, experiment.Summary, new DurableExperimentProvenance(experiment.Provenance.DefinitionIdentity, experiment.Provenance.FeatureSetIdentity, feature.DefinitionIdentity, feature.SnapshotIdentity, feature.SnapshotVersion, feature.Provenance.DatasetProvenance.DefinitionIdentity, feature.Provenance.DatasetProvenance.ResearchDatasetIdentity, feature.Provenance.DatasetProvenance.SourceStateIdentity, feature.Provenance.DatasetProvenance.SourceAuthority, feature.Provenance.DatasetProvenance.ObservationCount), new DurableExperimentLineage(experiment.Lineage.DefinitionIdentity, feature.DefinitionIdentity, feature.Lineage.DatasetLineage.DefinitionIdentity, feature.Lineage.DatasetLineage.SourceStateIdentity));
    }
    private static string Fingerprint(char c) => new(c, 64);
    private static WorkerResult RunDurableWorker(string databasePath, string identity, string? version, bool includeFeatureSelectors = false)
    {
        string root = FindRepositoryRoot();
        var start = new ProcessStartInfo("dotnet") { WorkingDirectory = root, RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false };
        start.ArgumentList.Add("run"); start.ArgumentList.Add("--project"); start.ArgumentList.Add(Path.Combine(root, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj")); start.ArgumentList.Add("--no-build"); start.ArgumentList.Add("--configuration"); start.ArgumentList.Add("Release");
        start.Environment["TwelveData__ApiKey"] = "wp12-dummy-api-key"; start.Environment["Persistence__DatabasePath"] = databasePath; start.Environment["DurableExperiment__SnapshotIdentity"] = identity;
        if (version is not null) start.Environment["DurableExperiment__SnapshotVersion"] = version;
        if (includeFeatureSelectors) { start.Environment["Feature__SnapshotIdentity"] = Fingerprint('a'); start.Environment["Feature__SnapshotVersion"] = Fingerprint('a'); }
        using Process process = Process.Start(start) ?? throw new InvalidOperationException("Worker did not start."); string output = process.StandardOutput.ReadToEnd(); string error = process.StandardError.ReadToEnd(); Assert.True(process.WaitForExit(30_000)); return new WorkerResult(process.ExitCode, output, error);
    }
    private static string ReadValue(string output, string marker) => output.Split('\n').Single(line => line.StartsWith(marker, StringComparison.Ordinal))[marker.Length..].Trim();
    private static string FindRepositoryRoot() { for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent) if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName; throw new InvalidOperationException("Repository root was not found."); }
    private sealed record WorkerResult(int ExitCode, string StandardOutput, string StandardError);
    private sealed class TemporaryDatabase : IDisposable { private readonly string directory; public TemporaryDatabase(bool createDirectory = true) { directory = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "aiq-wp12-" + Guid.NewGuid().ToString("N")); if (createDirectory) Directory.CreateDirectory(directory); Path = System.IO.Path.Combine(directory, "test.db"); Configuration = new SqliteStorageConfiguration(Path); Factory = new SqliteConnectionFactory(Configuration); } public string Path { get; } public SqliteStorageConfiguration Configuration { get; } public SqliteConnectionFactory Factory { get; } public void Dispose() { SqliteConnection.ClearAllPools(); if (Directory.Exists(directory)) Directory.Delete(directory, true); } }
}
