using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Microsoft.Data.Sqlite;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteHistoricalObservationStore : IHistoricalObservationStore
{
    private const string ReadExistingStatement = """
        SELECT offset_minutes, price_text
        FROM historical_observations
        WHERE target = $target AND instant_utc_ticks = $instantUtcTicks;
        """;

    private const string InsertStatement = """
        INSERT INTO historical_observations (
            target,
            instant_utc_ticks,
            offset_minutes,
            price_text)
        VALUES (
            $target,
            $instantUtcTicks,
            $offsetMinutes,
            $priceText);
        """;

    private const string RetrieveStatement = """
        SELECT target, instant_utc_ticks, offset_minutes, price_text
        FROM historical_observations
        WHERE target = $target
        ORDER BY instant_utc_ticks ASC;
        """;

    private readonly ISqliteConnectionFactory connectionFactory;

    public SqliteHistoricalObservationStore(ISqliteConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        this.connectionFactory = connectionFactory;
    }

    public ObservationPersistenceResult Persist(
        string target,
        IReadOnlyList<PriceObservation> observations)
    {
        if (string.IsNullOrWhiteSpace(target)
            || observations is null
            || observations.Count == 0
            || observations.Any(static observation => observation is null))
        {
            return ObservationPersistenceResult.Failed(PersistenceFailure.InvalidData);
        }

        var records = observations
            .Select(observation => SqliteHistoricalObservationMapper.ToRecord(target, observation))
            .ToArray();

        SqliteConnection connection;

        try
        {
            connection = connectionFactory.OpenConnection();
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return ObservationPersistenceResult.Failed(PersistenceFailure.Unavailable);
        }
        catch (SqliteException exception) when (IsInvalidData(exception))
        {
            return ObservationPersistenceResult.Failed(PersistenceFailure.InvalidData);
        }
        catch (InvalidOperationException)
        {
            return ObservationPersistenceResult.Failed(PersistenceFailure.Unavailable);
        }

        using (connection)
        {
            try
            {
                return Persist(connection, records);
            }
            catch (SqliteException exception) when (IsUnavailable(exception))
            {
                return ObservationPersistenceResult.Failed(PersistenceFailure.Unavailable);
            }
            catch (SqliteException exception) when (IsInvalidData(exception))
            {
                return ObservationPersistenceResult.Failed(PersistenceFailure.InvalidData);
            }
        }
    }

    public HistoricalObservationResult Retrieve(string target)
    {
        using var observation = InfrastructureObservability.StartProviderRetrieve();
        HistoricalObservationResult Complete(HistoricalObservationResult result)
        {
            var failure = result.IsSuccess ? null : result.Failure == PersistenceFailure.Unavailable ? "unavailable" : "invalid-data";
            InfrastructureObservability.Complete(observation, result.IsSuccess ? (result.Observations!.Count == 0 ? "empty" : "success") : "failed", failure);
            return result;
        }

        if (string.IsNullOrWhiteSpace(target))
        {
            return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
        }

        SqliteConnection connection;

        try
        {
            connection = connectionFactory.OpenConnection();
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return Complete(HistoricalObservationResult.Failed(PersistenceFailure.Unavailable));
        }
        catch (SqliteException exception) when (IsInvalidData(exception))
        {
            return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
        }
        catch (InvalidOperationException)
        {
            return Complete(HistoricalObservationResult.Failed(PersistenceFailure.Unavailable));
        }

        using (connection)
        {
            try
            {
                return Complete(Retrieve(connection, target));
            }
            catch (SqliteException exception) when (IsUnavailable(exception))
            {
                return Complete(HistoricalObservationResult.Failed(PersistenceFailure.Unavailable));
            }
            catch (SqliteException exception) when (IsInvalidData(exception))
            {
                return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
            }
            catch (InvalidCastException)
            {
                return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
            }
            catch (OverflowException)
            {
                return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
            }
            catch (FormatException)
            {
                return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
            }
            catch (ArgumentOutOfRangeException)
            {
                return Complete(HistoricalObservationResult.Failed(PersistenceFailure.InvalidData));
            }
        }
    }

    private static ObservationPersistenceResult Persist(
        SqliteConnection connection,
        IReadOnlyList<SqliteHistoricalObservationRecord> records)
    {
        using var transaction = connection.BeginTransaction(deferred: false);
        var insertedAny = false;

        foreach (var record in records)
        {
            var existing = ReadExisting(connection, transaction, record);

            if (existing is not null)
            {
                if (!IsEquivalent(existing, record))
                {
                    transaction.Rollback();
                    return ObservationPersistenceResult.Completed(
                        ObservationPersistenceOutcome.Conflict);
                }

                continue;
            }

            Insert(connection, transaction, record);
            insertedAny = true;
        }

        transaction.Commit();

        return ObservationPersistenceResult.Completed(
            insertedAny
                ? ObservationPersistenceOutcome.NewlyAccepted
                : ObservationPersistenceOutcome.Idempotent);
    }

    private static HistoricalObservationResult Retrieve(
        SqliteConnection connection,
        string target)
    {
        using var command = connection.CreateCommand();
        command.CommandText = RetrieveStatement;
        command.Parameters.AddWithValue("$target", target);
        using var reader = command.ExecuteReader();
        var observations = new List<PriceObservation>();

        while (reader.Read())
        {
            var record = new SqliteHistoricalObservationRecord(
                reader.GetString(0),
                reader.GetInt64(1),
                checked((short)reader.GetInt64(2)),
                reader.GetString(3));

            observations.Add(SqliteHistoricalObservationMapper.ToObservation(record));
        }

        return HistoricalObservationResult.Retrieved(observations);
    }

    private static SqliteHistoricalObservationRecord? ReadExisting(
        SqliteConnection connection,
        SqliteTransaction transaction,
        SqliteHistoricalObservationRecord record)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadExistingStatement;
        command.Parameters.AddWithValue("$target", record.Target);
        command.Parameters.AddWithValue("$instantUtcTicks", record.InstantUtcTicks);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new SqliteHistoricalObservationRecord(
            record.Target,
            record.InstantUtcTicks,
            checked((short)reader.GetInt64(0)),
            reader.GetString(1));
    }

    private static bool IsEquivalent(
        SqliteHistoricalObservationRecord existing,
        SqliteHistoricalObservationRecord incoming) =>
        existing.OffsetMinutes == incoming.OffsetMinutes
        && string.Equals(existing.PriceText, incoming.PriceText, StringComparison.Ordinal);

    private static bool IsUnavailable(SqliteException exception) =>
        exception.SqliteErrorCode is 5 or 6 or 7 or 8 or 10 or 13 or 14 or 15;

    private static bool IsInvalidData(SqliteException exception) =>
        exception.SqliteErrorCode is 11 or 19 or 20 or 26;

    private static void Insert(
        SqliteConnection connection,
        SqliteTransaction transaction,
        SqliteHistoricalObservationRecord record)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = InsertStatement;
        command.Parameters.AddWithValue("$target", record.Target);
        command.Parameters.AddWithValue("$instantUtcTicks", record.InstantUtcTicks);
        command.Parameters.AddWithValue("$offsetMinutes", record.OffsetMinutes);
        command.Parameters.AddWithValue("$priceText", record.PriceText);
        command.ExecuteNonQuery();
    }
}

internal static class InfrastructureObservability
{
    internal const string SourceName = "AIQuantTradingResearch.Infrastructure";
    internal const string ProviderRetrieveOperation = "historical-observation.retrieve";
    internal const string SnapshotStoreOperation = "dataset-snapshot.store";
    internal const string SnapshotRetrieveOperation = "dataset-snapshot.retrieve";

    private static readonly ActivitySource ActivitySource = new(SourceName);
    private static readonly Meter Meter = new(SourceName);
    private static readonly Counter<long> ProviderOperations = Meter.CreateCounter<long>("provider.operations", "{operation}");
    private static readonly Histogram<double> ProviderDuration = Meter.CreateHistogram<double>("provider.duration", "ms");
    private static readonly Counter<long> ProviderFailures = Meter.CreateCounter<long>("provider.failures", "{operation}");
    private static readonly Counter<long> PersistenceOperations = Meter.CreateCounter<long>("persistence.operations", "{operation}");
    private static readonly Histogram<double> PersistenceDuration = Meter.CreateHistogram<double>("persistence.duration", "ms");
    private static readonly Counter<long> PersistenceFailures = Meter.CreateCounter<long>("persistence.failures", "{operation}");

    internal static Observation StartProviderRetrieve() => new(ActivitySource.StartActivity("provider.operation"), ProviderRetrieveOperation, true);
    internal static Observation StartPersistence(string operation) => new(ActivitySource.StartActivity("persistence.operation"), operation, false);

    internal static void Complete(Observation observation, string outcome, string? failure = null)
    {
        var tags = new TagList { { "aiq.release", "1.10" }, { "aiq.component", "infrastructure" }, { "aiq.operation", observation.Operation }, { "aiq.outcome", outcome } };
        if (failure is not null) tags.Add("aiq.error_class", failure);
        observation.Activity?.SetTag("aiq.release", "1.10");
        observation.Activity?.SetTag("aiq.component", "infrastructure");
        observation.Activity?.SetTag("aiq.operation", observation.Operation);
        observation.Activity?.SetTag("aiq.outcome", outcome);
        if (failure is null) observation.Activity?.SetStatus(ActivityStatusCode.Ok); else { observation.Activity?.SetTag("aiq.error_class", failure); observation.Activity?.SetStatus(ActivityStatusCode.Error, failure); }
        var duration = Stopwatch.GetElapsedTime(observation.Start).TotalMilliseconds;
        if (observation.IsProvider) { ProviderOperations.Add(1, tags); ProviderDuration.Record(duration, tags); if (failure is not null) ProviderFailures.Add(1, tags); }
        else { PersistenceOperations.Add(1, tags); PersistenceDuration.Record(duration, tags); if (failure is not null) PersistenceFailures.Add(1, tags); }
    }

    internal sealed class Observation(Activity? activity, string operation, bool isProvider) : IDisposable
    {
        internal Activity? Activity { get; } = activity;
        internal string Operation { get; } = operation;
        internal bool IsProvider { get; } = isProvider;
        internal long Start { get; } = Stopwatch.GetTimestamp();
        public void Dispose() => Activity?.Dispose();
    }
}
