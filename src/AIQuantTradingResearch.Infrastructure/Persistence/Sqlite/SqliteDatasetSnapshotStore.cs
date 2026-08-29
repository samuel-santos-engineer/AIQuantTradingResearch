using AIQuantTradingResearch.Application.Datasets;
using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteDatasetSnapshotStore : IDatasetSnapshotStore
{
    private const string ReadSnapshotStatement = """
        SELECT
            snapshot_identity,
            definition_identity,
            research_dataset_identity,
            source_state_identity,
            identity_scheme,
            target,
            requested_from_utc_ticks,
            requested_from_offset_minutes,
            requested_to_utc_ticks,
            requested_to_offset_minutes,
            ordering,
            observation_count,
            first_observation_utc_ticks,
            first_observation_offset_minutes,
            last_observation_utc_ticks,
            last_observation_offset_minutes,
            source_authority
        FROM dataset_snapshots
        WHERE snapshot_identity = $snapshotIdentity;
        """;

    private const string ReadObservationsStatement = """
        SELECT
            snapshot_identity,
            ordinal,
            instant_utc_ticks,
            offset_minutes,
            price_text
        FROM dataset_snapshot_observations
        WHERE snapshot_identity = $snapshotIdentity
        ORDER BY ordinal ASC;
        """;

    private const string InsertSnapshotStatement = """
        INSERT INTO dataset_snapshots (
            snapshot_identity,
            definition_identity,
            research_dataset_identity,
            source_state_identity,
            identity_scheme,
            target,
            requested_from_utc_ticks,
            requested_from_offset_minutes,
            requested_to_utc_ticks,
            requested_to_offset_minutes,
            ordering,
            observation_count,
            first_observation_utc_ticks,
            first_observation_offset_minutes,
            last_observation_utc_ticks,
            last_observation_offset_minutes,
            source_authority)
        VALUES (
            $snapshotIdentity,
            $definitionIdentity,
            $researchDatasetIdentity,
            $sourceStateIdentity,
            $identityScheme,
            $target,
            $requestedFromUtcTicks,
            $requestedFromOffsetMinutes,
            $requestedToUtcTicks,
            $requestedToOffsetMinutes,
            $ordering,
            $observationCount,
            $firstObservationUtcTicks,
            $firstObservationOffsetMinutes,
            $lastObservationUtcTicks,
            $lastObservationOffsetMinutes,
            $sourceAuthority);
        """;

    private const string InsertObservationStatement = """
        INSERT INTO dataset_snapshot_observations (
            snapshot_identity,
            ordinal,
            instant_utc_ticks,
            offset_minutes,
            price_text)
        VALUES (
            $snapshotIdentity,
            $ordinal,
            $instantUtcTicks,
            $offsetMinutes,
            $priceText);
        """;

    private readonly ISqliteConnectionFactory connectionFactory;

    public SqliteDatasetSnapshotStore(ISqliteConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        this.connectionFactory = connectionFactory;
    }

    public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot)
    {
        using var observation = InfrastructureObservability.StartPersistence(InfrastructureObservability.SnapshotStoreOperation);
        DatasetSnapshotStoreResult Complete(DatasetSnapshotStoreResult result)
        {
            var failure = result.HasOutcome ? (result.Outcome == DatasetSnapshotStoreOutcome.IntegrityConflict ? "conflict" : null) : result.Failure == DatasetStoreFailure.Unavailable ? "unavailable" : "invalid-data";
            InfrastructureObservability.Complete(observation, result.HasOutcome ? "success" : "failed", failure);
            return result;
        }
        ArgumentNullException.ThrowIfNull(snapshot);

        var snapshotRecord = SqliteDatasetMapper.ToSnapshotRecord(new DatasetCatalogEntry(snapshot));
        var observationRecords = SqliteDatasetMapper.ToObservationRecords(snapshot);

        SqliteConnection connection;

        try
        {
            connection = connectionFactory.OpenConnection();
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.Unavailable));
        }
        catch (SqliteException exception) when (IsInvalidData(exception))
        {
            return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
        }
        catch (SqliteSchemaValidationException)
        {
            return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
        }
        catch (InvalidOperationException exception)
            when (exception.InnerException is SqliteSchemaValidationException)
        {
            return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
        }

        using (connection)
        using (var transaction = connection.BeginTransaction(deferred: false))
        {
            try
            {
                var existingSnapshot = ReadSnapshot(connection, transaction, snapshotRecord.SnapshotIdentity);

                if (existingSnapshot is not null)
                {
                    var existingObservations = ReadObservations(
                        connection,
                        transaction,
                        snapshotRecord.SnapshotIdentity);
                    _ = SqliteDatasetMapper.ToSnapshotCandidate(existingSnapshot, existingObservations);

                    return Complete(DatasetSnapshotStoreResult.Completed(
                        IsEquivalent(existingSnapshot, existingObservations, snapshotRecord, observationRecords)
                            ? DatasetSnapshotStoreOutcome.EquivalentExisting
                            : DatasetSnapshotStoreOutcome.IntegrityConflict));
                }

                InsertSnapshot(connection, transaction, snapshotRecord);

                foreach (var observationRecord in observationRecords)
                {
                    InsertObservation(connection, transaction, observationRecord);
                }

                transaction.Commit();
                return Complete(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted));
            }
            catch (SqliteException exception) when (IsUnavailable(exception))
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.Unavailable));
            }
            catch (SqliteException exception) when (IsInvalidData(exception))
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (InvalidCastException)
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (OverflowException)
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (FormatException)
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (ArgumentException)
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (SqliteDatasetEvidenceException)
            {
                return Complete(DatasetSnapshotStoreResult.Failed(DatasetStoreFailure.InvalidData));
            }
        }
    }

    public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity)
    {
        using var observation = InfrastructureObservability.StartPersistence(InfrastructureObservability.SnapshotRetrieveOperation);
        DatasetSnapshotRetrievalResult Complete(DatasetSnapshotRetrievalResult result)
        {
            var failure = result.IsFound || result.IsNotFound ? null : result.Failure == DatasetStoreFailure.Unavailable ? "unavailable" : "invalid-data";
            InfrastructureObservability.Complete(observation, result.IsFound ? "success" : result.IsNotFound ? "empty" : "failed", failure);
            return result;
        }
        ArgumentNullException.ThrowIfNull(snapshotIdentity);

        SqliteConnection connection;

        try
        {
            connection = connectionFactory.OpenConnection();
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.Unavailable));
        }
        catch (SqliteException exception) when (IsInvalidData(exception))
        {
            return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
        }
        catch (SqliteSchemaValidationException)
        {
            return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
        }
        catch (InvalidOperationException exception)
            when (exception.InnerException is SqliteSchemaValidationException)
        {
            return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
        }

        using (connection)
        {
            try
            {
                var snapshotRecord = ReadSnapshot(connection, null, snapshotIdentity.Fingerprint);

                if (snapshotRecord is null)
                {
                    return Complete(DatasetSnapshotRetrievalResult.NotFound());
                }

                var observationRecords = ReadObservations(connection, null, snapshotIdentity.Fingerprint);
                return Complete(DatasetSnapshotRetrievalResult.Found(
                    SqliteDatasetMapper.ToSnapshotCandidate(snapshotRecord, observationRecords)));
            }
            catch (SqliteException exception) when (IsUnavailable(exception))
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.Unavailable));
            }
            catch (SqliteException exception) when (IsInvalidData(exception))
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (InvalidCastException)
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (OverflowException)
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (FormatException)
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (ArgumentException)
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
            }
            catch (SqliteDatasetEvidenceException)
            {
                return Complete(DatasetSnapshotRetrievalResult.Failed(DatasetStoreFailure.InvalidData));
            }
        }
    }

    private static bool IsEquivalent(
        SqliteDatasetSnapshotRecord existingSnapshot,
        IReadOnlyList<SqliteDatasetObservationRecord> existingObservations,
        SqliteDatasetSnapshotRecord incomingSnapshot,
        IReadOnlyList<SqliteDatasetObservationRecord> incomingObservations) =>
        existingSnapshot == incomingSnapshot
        && existingObservations.SequenceEqual(incomingObservations);

    private static SqliteDatasetSnapshotRecord? ReadSnapshot(
        SqliteConnection connection,
        SqliteTransaction? transaction,
        string snapshotIdentity)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadSnapshotStatement;
        command.Parameters.AddWithValue("$snapshotIdentity", snapshotIdentity);
        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new SqliteDatasetSnapshotRecord(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetString(4),
            reader.GetString(5),
            reader.GetInt64(6),
            checked((short)reader.GetInt64(7)),
            reader.GetInt64(8),
            checked((short)reader.GetInt64(9)),
            checked((int)reader.GetInt64(10)),
            checked((int)reader.GetInt64(11)),
            reader.IsDBNull(12) ? null : reader.GetInt64(12),
            reader.IsDBNull(13) ? null : checked((short)reader.GetInt64(13)),
            reader.IsDBNull(14) ? null : reader.GetInt64(14),
            reader.IsDBNull(15) ? null : checked((short)reader.GetInt64(15)),
            checked((int)reader.GetInt64(16)));
    }

    private static List<SqliteDatasetObservationRecord> ReadObservations(
        SqliteConnection connection,
        SqliteTransaction? transaction,
        string snapshotIdentity)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadObservationsStatement;
        command.Parameters.AddWithValue("$snapshotIdentity", snapshotIdentity);
        using var reader = command.ExecuteReader();
        var observations = new List<SqliteDatasetObservationRecord>();

        while (reader.Read())
        {
            observations.Add(new SqliteDatasetObservationRecord(
                reader.GetString(0),
                checked((int)reader.GetInt64(1)),
                reader.GetInt64(2),
                checked((short)reader.GetInt64(3)),
                reader.GetString(4)));
        }

        return observations;
    }

    private static void InsertSnapshot(
        SqliteConnection connection,
        SqliteTransaction transaction,
        SqliteDatasetSnapshotRecord record)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = InsertSnapshotStatement;
        command.Parameters.AddWithValue("$snapshotIdentity", record.SnapshotIdentity);
        command.Parameters.AddWithValue("$definitionIdentity", record.DefinitionIdentity);
        command.Parameters.AddWithValue("$researchDatasetIdentity", record.ResearchDatasetIdentity);
        command.Parameters.AddWithValue("$sourceStateIdentity", record.SourceStateIdentity);
        command.Parameters.AddWithValue("$identityScheme", record.IdentityScheme);
        command.Parameters.AddWithValue("$target", record.Target);
        command.Parameters.AddWithValue("$requestedFromUtcTicks", record.RequestedFromUtcTicks);
        command.Parameters.AddWithValue("$requestedFromOffsetMinutes", record.RequestedFromOffsetMinutes);
        command.Parameters.AddWithValue("$requestedToUtcTicks", record.RequestedToUtcTicks);
        command.Parameters.AddWithValue("$requestedToOffsetMinutes", record.RequestedToOffsetMinutes);
        command.Parameters.AddWithValue("$ordering", record.Ordering);
        command.Parameters.AddWithValue("$observationCount", record.ObservationCount);
        command.Parameters.AddWithValue("$firstObservationUtcTicks", (object?)record.FirstObservationUtcTicks ?? DBNull.Value);
        command.Parameters.AddWithValue("$firstObservationOffsetMinutes", (object?)record.FirstObservationOffsetMinutes ?? DBNull.Value);
        command.Parameters.AddWithValue("$lastObservationUtcTicks", (object?)record.LastObservationUtcTicks ?? DBNull.Value);
        command.Parameters.AddWithValue("$lastObservationOffsetMinutes", (object?)record.LastObservationOffsetMinutes ?? DBNull.Value);
        command.Parameters.AddWithValue("$sourceAuthority", record.SourceAuthority);
        command.ExecuteNonQuery();
    }

    private static void InsertObservation(
        SqliteConnection connection,
        SqliteTransaction transaction,
        SqliteDatasetObservationRecord record)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = InsertObservationStatement;
        command.Parameters.AddWithValue("$snapshotIdentity", record.SnapshotIdentity);
        command.Parameters.AddWithValue("$ordinal", record.Ordinal);
        command.Parameters.AddWithValue("$instantUtcTicks", record.InstantUtcTicks);
        command.Parameters.AddWithValue("$offsetMinutes", record.OffsetMinutes);
        command.Parameters.AddWithValue("$priceText", record.PriceText);
        command.ExecuteNonQuery();
    }

    private static bool IsUnavailable(SqliteException exception) =>
        exception.SqliteErrorCode is 5 or 6 or 7 or 8 or 10 or 13 or 14 or 15;

    private static bool IsInvalidData(SqliteException exception) =>
        exception.SqliteErrorCode is 11 or 19 or 20 or 26;
}
