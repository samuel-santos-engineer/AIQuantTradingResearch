using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Microsoft.Data.Sqlite;

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
        if (string.IsNullOrWhiteSpace(target))
        {
            return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
        }

        SqliteConnection connection;

        try
        {
            connection = connectionFactory.OpenConnection();
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return HistoricalObservationResult.Failed(PersistenceFailure.Unavailable);
        }
        catch (SqliteException exception) when (IsInvalidData(exception))
        {
            return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
        }
        catch (InvalidOperationException)
        {
            return HistoricalObservationResult.Failed(PersistenceFailure.Unavailable);
        }

        using (connection)
        {
            try
            {
                return Retrieve(connection, target);
            }
            catch (SqliteException exception) when (IsUnavailable(exception))
            {
                return HistoricalObservationResult.Failed(PersistenceFailure.Unavailable);
            }
            catch (SqliteException exception) when (IsInvalidData(exception))
            {
                return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
            }
            catch (InvalidCastException)
            {
                return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
            }
            catch (OverflowException)
            {
                return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
            }
            catch (FormatException)
            {
                return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
            }
            catch (ArgumentOutOfRangeException)
            {
                return HistoricalObservationResult.Failed(PersistenceFailure.InvalidData);
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
