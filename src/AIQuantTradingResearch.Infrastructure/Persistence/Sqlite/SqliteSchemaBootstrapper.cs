using System.Data;
using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteSchemaBootstrapper
{
    private const string ReadSchemaVersionStatement = "PRAGMA user_version;";
    private const string WriteSchemaVersionStatement = "PRAGMA user_version = 1;";
    private const string ReadTableDefinitionStatement = """
        SELECT sql
        FROM sqlite_schema
        WHERE type = 'table' AND name = 'historical_observations';
        """;
    private const string ReadColumnsStatement = "PRAGMA table_info(historical_observations);";

    private static readonly ExpectedColumn[] ExpectedColumns =
    {
        new("target", "TEXT", 1),
        new("instant_utc_ticks", "INTEGER", 2),
        new("offset_minutes", "INTEGER", 0),
        new("price_text", "TEXT", 0),
    };

    public static void Bootstrap(SqliteConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        if (connection.State != ConnectionState.Open)
        {
            throw new InvalidOperationException("SQLite schema bootstrap requires an open connection.");
        }

        using var transaction = connection.BeginTransaction();
        var version = ReadSchemaVersion(connection, transaction);

        if (version == 0)
        {
            Execute(connection, transaction, SqliteHistoricalObservationSchema.CreateTableStatement);
            ValidateSchema(connection, transaction);
            Execute(connection, transaction, WriteSchemaVersionStatement);
        }
        else if (version == SqliteHistoricalObservationSchema.Version)
        {
            ValidateSchema(connection, transaction);
        }
        else
        {
            throw new InvalidOperationException(
                $"Unsupported SQLite schema version '{version}'. Expected version '{SqliteHistoricalObservationSchema.Version}'.");
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
            ?? throw new InvalidOperationException("SQLite returned no schema version."));
    }

    private static void ValidateSchema(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        var columns = ReadColumns(connection, transaction);

        if (!columns.SequenceEqual(ExpectedColumns))
        {
            throw new InvalidOperationException("The SQLite historical-observation table has an incompatible column definition.");
        }

        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadTableDefinitionStatement;
        var definition = command.ExecuteScalar() as string
            ?? throw new InvalidOperationException("The SQLite historical-observation table is missing.");

        var requiredFragments = new[]
        {
            "target TEXT COLLATE BINARY NOT NULL",
            "offset_minutes INTEGER NOT NULL CHECK (offset_minutes BETWEEN -840 AND 840)",
            "price_text TEXT NOT NULL CHECK (length(price_text) > 0)",
            "PRIMARY KEY (target, instant_utc_ticks)",
            "STRICT, WITHOUT ROWID",
        };

        if (requiredFragments.Any(fragment =>
            !definition.Contains(fragment, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("The SQLite historical-observation table has incompatible constraints.");
        }
    }

    private static List<ExpectedColumn> ReadColumns(
        SqliteConnection connection,
        SqliteTransaction transaction)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadColumnsStatement;
        using var reader = command.ExecuteReader();
        var columns = new List<ExpectedColumn>();

        while (reader.Read())
        {
            var isNotNull = reader.GetInt64(3) == 1;

            if (!isNotNull)
            {
                throw new InvalidOperationException("SQLite historical-observation columns must be non-nullable.");
            }

            columns.Add(new ExpectedColumn(
                reader.GetString(1),
                reader.GetString(2),
                checked((int)reader.GetInt64(5))));
        }

        return columns;
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

    private sealed record ExpectedColumn(string Name, string Type, int PrimaryKeyOrdinal);
}
