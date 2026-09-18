using System.Text;
using System.Text.Json;
using System.Diagnostics;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class PersistentSqliteQualificationExecution(
    IPersistHistoricalObservationsUseCase persistenceUseCase,
    IHistoricalObservationStore observationStore,
    SqlitePersistenceDiagnostics diagnostics)
{
    private const string Target = "WP04-QUALIFICATION";
    private static readonly PriceObservation QualificationObservation = new(
        new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero),
        123.456m);

    public int Execute(PersistentSqliteQualificationConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var lifecycle = Stopwatch.StartNew();
        var outcome = "Succeeded";
        PersistentSqliteQualificationDiagnostics.Emit("QUALIFICATION_ENTERED", configuration, lifecycle);

        try
        {
            if (configuration.Phase == PersistentSqliteQualificationPhase.Initialize)
            {
                PersistentSqliteQualificationDiagnostics.Emit("SQLITE_QUALIFICATION_STARTED", configuration, lifecycle);
                var persisted = persistenceUseCase.Execute(new PersistHistoricalObservationsRequest(
                    Target,
                    [QualificationObservation]));
                if (persisted.PersistenceResult?.Outcome is not (
                    ObservationPersistenceOutcome.NewlyAccepted or ObservationPersistenceOutcome.Idempotent))
                {
                    outcome = "UnhandledQualificationFailure";
                    return Fail("The application-owned qualification write failed.");
                }
            }

            var retrieved = observationStore.Retrieve(Target);
            if (!retrieved.IsSuccess
                || retrieved.Observations is not [var observation]
                || observation != QualificationObservation)
            {
                outcome = "UnhandledQualificationFailure";
                return Fail("The application-owned qualification read did not return the expected accepted observation.");
            }

            var metadata = diagnostics.Collect();
            var record = new PersistentSqliteQualificationRecord(
                1,
                configuration.Phase.ToString().ToLowerInvariant(),
                metadata.DatabasePathIdentity,
                metadata.SchemaVersion,
                metadata.JournalMode,
                QualificationEvidenceIdentity(),
                retrieved.Observations.Count,
                metadata.IntegrityCheck,
                metadata.QuickCheck,
                metadata.SchemaVersion == 4
                    && string.Equals(metadata.JournalMode, "delete", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(metadata.IntegrityCheck, "ok", StringComparison.OrdinalIgnoreCase)
                    && string.Equals(metadata.QuickCheck, "ok", StringComparison.OrdinalIgnoreCase),
                configuration.RunId);

            var serializedRecord = JsonSerializer.Serialize(record);
            if (configuration.EvidenceOutputPath is not null)
            {
                try
                {
                    WriteEvidenceArtifact(configuration.EvidenceOutputPath, serializedRecord);
                    PersistentSqliteQualificationDiagnostics.Emit("ARTIFACT_WRITE_SUCCEEDED", configuration, lifecycle);
                }
                catch (IOException)
                {
                    outcome = "ArtifactWriteFailure";
                    return Fail("The application-owned qualification evidence artifact could not be written.");
                }
                catch (UnauthorizedAccessException)
                {
                    outcome = "ArtifactWriteFailure";
                    return Fail("The application-owned qualification evidence artifact could not be written.");
                }
            }

            Console.WriteLine(serializedRecord);
            return 0;
        }
        finally
        {
            if (!configuration.HttpEvidenceEnabled)
            {
                PersistentSqliteQualificationDiagnostics.Emit("WORKER_EXITING", configuration, lifecycle, outcome);
            }
        }
    }

    private static void WriteEvidenceArtifact(string evidenceOutputPath, string serializedRecord)
    {
        var parent = Path.GetDirectoryName(evidenceOutputPath);
        if (string.IsNullOrWhiteSpace(parent))
        {
            throw new IOException("The qualification evidence output path has no parent directory.");
        }

        Directory.CreateDirectory(parent);
        var temporaryPath = Path.Combine(
            parent,
            $".{Path.GetFileName(evidenceOutputPath)}.{Guid.NewGuid():N}.tmp");
        try
        {
            File.WriteAllText(temporaryPath, serializedRecord, new UTF8Encoding(false));
            File.Move(temporaryPath, evidenceOutputPath, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporaryPath))
            {
                File.Delete(temporaryPath);
            }
        }
    }

    private static string QualificationEvidenceIdentity() =>
        $"{Target}:{QualificationObservation.Instant.UtcTicks}";

    private static int Fail(string message)
    {
        Console.Error.WriteLine(message);
        return 1;
    }
}

internal sealed class PersistentSqliteQualificationConfiguration
{
    internal const string ModeName = "PersistentSqliteQualification";
    private const string PhasePath = "PersistentSqliteQualification:Phase";
    private const string EvidenceOutputPathPath = "PersistentSqliteQualification:EvidenceOutputPath";
    private const string RunIdPath = "PersistentSqliteQualification:RunId";
    private const string HttpEvidenceEnabledPath = "PersistentSqliteQualification:HttpEvidenceEnabled";
    private const string HttpEvidenceTokenPath = "PersistentSqliteQualification:HttpEvidenceToken";

    private PersistentSqliteQualificationConfiguration(
        PersistentSqliteQualificationPhase phase,
        string? evidenceOutputPath,
        string runId,
        bool httpEvidenceEnabled,
        string? httpEvidenceToken)
    {
        Phase = phase;
        EvidenceOutputPath = evidenceOutputPath;
        RunId = runId;
        HttpEvidenceEnabled = httpEvidenceEnabled;
        HttpEvidenceToken = httpEvidenceToken;
    }

    public PersistentSqliteQualificationPhase Phase { get; }

    public string? EvidenceOutputPath { get; }

    public string RunId { get; }

    public bool HttpEvidenceEnabled { get; }

    public string? HttpEvidenceToken { get; }

    public static PersistentSqliteQualificationConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var phase = configuration[PhasePath]?.ToLowerInvariant() switch
        {
            "initialize" => PersistentSqliteQualificationPhase.Initialize,
            "reopen" => PersistentSqliteQualificationPhase.Reopen,
            _ => throw new ArgumentException($"Missing or invalid mandatory configuration: {PhasePath}."),
        };

        var evidenceOutputPath = configuration[EvidenceOutputPathPath];
        if (string.IsNullOrWhiteSpace(evidenceOutputPath))
        {
            if (bool.TryParse(configuration[HttpEvidenceEnabledPath], out var httpEvidenceEnabledWithoutArtifact)
                && httpEvidenceEnabledWithoutArtifact)
            {
                throw new ArgumentException($"Missing mandatory configuration: {EvidenceOutputPathPath}.");
            }

            return new PersistentSqliteQualificationConfiguration(phase, null, "stdout-only", false, null);
        }

        var runId = configuration[RunIdPath];
        if (string.IsNullOrWhiteSpace(runId))
        {
            throw new ArgumentException($"Missing mandatory configuration: {RunIdPath}.");
        }

        var httpEvidenceEnabledValue = configuration[HttpEvidenceEnabledPath];
        if (!string.IsNullOrWhiteSpace(httpEvidenceEnabledValue)
            && !bool.TryParse(httpEvidenceEnabledValue, out _))
        {
            throw new ArgumentException($"Invalid Boolean configuration: {HttpEvidenceEnabledPath}.");
        }

        var httpEvidenceEnabled = bool.TryParse(httpEvidenceEnabledValue, out var parsedHttpEvidenceEnabled)
            && parsedHttpEvidenceEnabled;
        var httpEvidenceToken = configuration[HttpEvidenceTokenPath];
        if (httpEvidenceEnabled && string.IsNullOrWhiteSpace(httpEvidenceToken))
        {
            throw new ArgumentException($"Missing mandatory configuration: {HttpEvidenceTokenPath}.");
        }

        return new PersistentSqliteQualificationConfiguration(
            phase,
            evidenceOutputPath,
            runId,
            httpEvidenceEnabled,
            httpEvidenceToken);
    }
}

internal enum PersistentSqliteQualificationPhase
{
    Initialize,
    Reopen,
}

internal sealed record PersistentSqliteQualificationRecord(
    int RecordVersion,
    string Phase,
    string DatabasePathIdentity,
    long SchemaVersion,
    string JournalMode,
    string AcceptedEvidenceIdentity,
    int AcceptedEvidenceCount,
    string IntegrityCheck,
    string QuickCheck,
    bool PersistenceContinuity,
    string RunId);
