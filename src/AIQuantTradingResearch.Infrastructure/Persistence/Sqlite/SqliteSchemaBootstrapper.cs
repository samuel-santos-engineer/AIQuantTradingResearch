using System.Data;
using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteSchemaBootstrapper
{
    private const string EnableForeignKeysStatement = "PRAGMA foreign_keys = ON;";
    private const string ReadSchemaVersionStatement = "PRAGMA user_version;";
    private const string WriteSchemaVersionStatement = "PRAGMA user_version = 2;";

    private static readonly ExpectedColumn[] ExpectedHistoricalObservationColumns =
    {
        new("target", "TEXT", true, 1),
        new("instant_utc_ticks", "INTEGER", true, 2),
        new("offset_minutes", "INTEGER", true, 0),
        new("price_text", "TEXT", true, 0),
    };

    private static readonly ExpectedColumn[] ExpectedDatasetSnapshotColumns =
    {
        new("snapshot_identity", "TEXT", true, 1),
        new("definition_identity", "TEXT", true, 0),
        new("research_dataset_identity", "TEXT", true, 0),
        new("source_state_identity", "TEXT", true, 0),
        new("identity_scheme", "TEXT", true, 0),
        new("target", "TEXT", true, 0),
        new("requested_from_utc_ticks", "INTEGER", true, 0),
        new("requested_from_offset_minutes", "INTEGER", true, 0),
        new("requested_to_utc_ticks", "INTEGER", true, 0),
        new("requested_to_offset_minutes", "INTEGER", true, 0),
        new("ordering", "INTEGER", true, 0),
        new("observation_count", "INTEGER", true, 0),
        new("first_observation_utc_ticks", "INTEGER", false, 0),
        new("first_observation_offset_minutes", "INTEGER", false, 0),
        new("last_observation_utc_ticks", "INTEGER", false, 0),
        new("last_observation_offset_minutes", "INTEGER", false, 0),
        new("source_authority", "INTEGER", true, 0),
    };

    private static readonly ExpectedColumn[] ExpectedDatasetObservationColumns =
    {
        new("snapshot_identity", "TEXT", true, 1),
        new("ordinal", "INTEGER", true, 2),
        new("instant_utc_ticks", "INTEGER", true, 0),
        new("offset_minutes", "INTEGER", true, 0),
        new("price_text", "TEXT", true, 0),
    };

    private static readonly string[] RequiredHistoricalObservationFragments =
    {
        "target TEXT COLLATE BINARY NOT NULL",
        "offset_minutes INTEGER NOT NULL CHECK (offset_minutes BETWEEN -840 AND 840)",
        "price_text TEXT NOT NULL CHECK (length(price_text) > 0)",
        "PRIMARY KEY (target, instant_utc_ticks)",
        "STRICT, WITHOUT ROWID",
    };

    private static readonly string[] RequiredDatasetSnapshotFragments =
    {
        "snapshot_identity TEXT COLLATE BINARY NOT NULL",
        "identity_scheme = 'aiq-dataset-identity-v1'",
        "target TEXT COLLATE BINARY NOT NULL",
        "PRIMARY KEY (snapshot_identity)",
        "observation_count = 0",
        "first_observation_utc_ticks IS NULL",
        "STRICT, WITHOUT ROWID",
    };

    private static readonly string[] RequiredDatasetObservationFragments =
    {
        "PRIMARY KEY (snapshot_identity, ordinal)",
        "UNIQUE (snapshot_identity, instant_utc_ticks)",
        "FOREIGN KEY (snapshot_identity) REFERENCES dataset_snapshots(snapshot_identity)",
        "ON UPDATE RESTRICT ON DELETE RESTRICT",
        "STRICT, WITHOUT ROWID",
    };

    public static void Bootstrap(SqliteConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        if (connection.State != ConnectionState.Open)
        {
            throw new InvalidOperationException("SQLite schema bootstrap requires an open connection.");
        }

        EnableForeignKeys(connection);

        using var transaction = connection.BeginTransaction();
        var version = ReadSchemaVersion(connection, transaction);

        if (version == 0)
        {
            Execute(connection, transaction, SqliteHistoricalObservationSchema.CreateTableStatement);
            ValidateHistoricalObservationSchema(connection, transaction);
            CreateDatasetSchema(connection, transaction);
            ValidateDatasetSchema(connection, transaction);
            Execute(connection, transaction, WriteSchemaVersionStatement);
        }
        else if (version == SqliteHistoricalObservationSchema.Version)
        {
            ValidateHistoricalObservationSchema(connection, transaction);
            CreateDatasetSchema(connection, transaction);
            ValidateDatasetSchema(connection, transaction);
            Execute(connection, transaction, WriteSchemaVersionStatement);
        }
        else if (version == SqliteDatasetSchema.Version)
        {
            ValidateHistoricalObservationSchema(connection, transaction);
            ValidateDatasetSchema(connection, transaction);
        }
        else
        {
            throw new SqliteSchemaValidationException(
                $"Unsupported SQLite schema version '{version}'. Expected version '{SqliteDatasetSchema.Version}'.");
        }

        transaction.Commit();
    }

    private static long ReadSchemaVersion(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadSchemaVersionStatement;
        return (long)(command.ExecuteScalar()
            ?? throw new SqliteSchemaValidationException("SQLite returned no schema version."));
    }

    private static void EnableForeignKeys(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = EnableForeignKeysStatement;
        command.ExecuteNonQuery();
    }

    private static void CreateDatasetSchema(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        Execute(connection, transaction, SqliteDatasetSchema.CreateSnapshotTableStatement);
        Execute(connection, transaction, SqliteDatasetSchema.CreateObservationTableStatement);
    }

    private static void ValidateHistoricalObservationSchema(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        ValidateColumns(
            connection,
            transaction,
            SqliteHistoricalObservationSchema.TableName,
            ExpectedHistoricalObservationColumns);

        ValidateTableDefinition(
            connection,
            transaction,
            SqliteHistoricalObservationSchema.TableName,
            RequiredHistoricalObservationFragments);
    }

    private static void ValidateDatasetSchema(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        ValidateColumns(
            connection,
            transaction,
            SqliteDatasetSchema.SnapshotTableName,
            ExpectedDatasetSnapshotColumns);
        ValidateColumns(
            connection,
            transaction,
            SqliteDatasetSchema.ObservationTableName,
            ExpectedDatasetObservationColumns);

        ValidateTableDefinition(
            connection,
            transaction,
            SqliteDatasetSchema.SnapshotTableName,
            RequiredDatasetSnapshotFragments);
        ValidateTableDefinition(
            connection,
            transaction,
            SqliteDatasetSchema.ObservationTableName,
            RequiredDatasetObservationFragments);
        ValidateDatasetObservationForeignKey(connection, transaction);
    }

    private static void ValidateColumns(
        SqliteConnection connection,
        SqliteTransaction transaction,
        string tableName,
        IReadOnlyList<ExpectedColumn> expectedColumns)
    {
        var columns = ReadColumns(connection, transaction, tableName);

        if (!columns.SequenceEqual(expectedColumns))
        {
            throw new SqliteSchemaValidationException($"The SQLite '{tableName}' table has an incompatible column definition.");
        }
    }

    private static void ValidateTableDefinition(
        SqliteConnection connection,
        SqliteTransaction transaction,
        string tableName,
        IReadOnlyList<string> requiredFragments)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = """
            SELECT sql
            FROM sqlite_schema
            WHERE type = 'table' AND name = $tableName;
            """;
        command.Parameters.AddWithValue("$tableName", tableName);
        var definition = command.ExecuteScalar() as string
            ?? throw new SqliteSchemaValidationException($"The SQLite '{tableName}' table is missing.");

        if (requiredFragments.Any(fragment =>
            !definition.Contains(fragment, StringComparison.OrdinalIgnoreCase)))
        {
            throw new SqliteSchemaValidationException($"The SQLite '{tableName}' table has incompatible constraints.");
        }
    }

    private static List<ExpectedColumn> ReadColumns(
        SqliteConnection connection,
        SqliteTransaction transaction,
        string tableName)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = $"PRAGMA table_info({tableName});";
        using var reader = command.ExecuteReader();
        var columns = new List<ExpectedColumn>();

        while (reader.Read())
        {
            var isNotNull = reader.GetInt64(3) == 1;
            columns.Add(new ExpectedColumn(
                reader.GetString(1),
                reader.GetString(2),
                isNotNull,
                checked((int)reader.GetInt64(5))));
        }

        return columns;
    }

    private static void ValidateDatasetObservationForeignKey(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = $"PRAGMA foreign_key_list({SqliteDatasetSchema.ObservationTableName});";
        using var reader = command.ExecuteReader();

        if (!reader.Read()
            || !string.Equals(reader.GetString(2), SqliteDatasetSchema.SnapshotTableName, StringComparison.Ordinal)
            || !string.Equals(reader.GetString(3), "snapshot_identity", StringComparison.Ordinal)
            || !string.Equals(reader.GetString(4), "snapshot_identity", StringComparison.Ordinal)
            || !string.Equals(reader.GetString(5), "RESTRICT", StringComparison.OrdinalIgnoreCase)
            || !string.Equals(reader.GetString(6), "RESTRICT", StringComparison.OrdinalIgnoreCase)
            || reader.Read())
        {
            throw new SqliteSchemaValidationException("The SQLite dataset-observation foreign key is incompatible.");
        }
    }

    private static void Execute(
        SqliteConnection connection,
        SqliteTransaction transaction,
        string statement)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = statement;
        command.ExecuteNonQuery();
    }

    private sealed record ExpectedColumn(
        string Name,
        string Type,
        bool IsNotNull,
        int PrimaryKeyOrdinal);
}
