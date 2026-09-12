using System.Text.Json;
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

        if (configuration.Phase == PersistentSqliteQualificationPhase.Initialize)
        {
            var persisted = persistenceUseCase.Execute(new PersistHistoricalObservationsRequest(
                Target,
                [QualificationObservation]));
            if (persisted.PersistenceResult?.Outcome is not (
                ObservationPersistenceOutcome.NewlyAccepted or ObservationPersistenceOutcome.Idempotent))
            {
                return Fail("The application-owned qualification write failed.");
            }
        }

        var retrieved = observationStore.Retrieve(Target);
        if (!retrieved.IsSuccess
            || retrieved.Observations is not [var observation]
            || observation != QualificationObservation)
        {
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
                && string.Equals(metadata.QuickCheck, "ok", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine(JsonSerializer.Serialize(record));
        return 0;
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

    private PersistentSqliteQualificationConfiguration(PersistentSqliteQualificationPhase phase) => Phase = phase;

    public PersistentSqliteQualificationPhase Phase { get; }

    public static PersistentSqliteQualificationConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var configured = configuration[PhasePath];
        return configured?.ToLowerInvariant() switch
        {
            "initialize" => new PersistentSqliteQualificationConfiguration(PersistentSqliteQualificationPhase.Initialize),
            "reopen" => new PersistentSqliteQualificationConfiguration(PersistentSqliteQualificationPhase.Reopen),
            _ => throw new ArgumentException($"Missing or invalid mandatory configuration: {PhasePath}."),
        };
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
    bool PersistenceContinuity);
