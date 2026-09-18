using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

public sealed class SqlitePersistenceDiagnostics
{
    private readonly SqliteConnectionFactory connectionFactory;
    private readonly string databasePathIdentity;

    public SqlitePersistenceDiagnostics(SqliteStorageConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        connectionFactory = new SqliteConnectionFactory(configuration);
        databasePathIdentity = Path.GetFileName(configuration.DatabasePath);
    }

    public SqlitePersistenceDiagnosticSnapshot Collect()
    {
        using var connection = connectionFactory.OpenConnection();
        return new SqlitePersistenceDiagnosticSnapshot(
            databasePathIdentity,
            Scalar<long>(connection, "PRAGMA user_version;"),
            Scalar<string>(connection, "PRAGMA journal_mode;"),
            Scalar<string>(connection, "PRAGMA integrity_check;"),
            Scalar<string>(connection, "PRAGMA quick_check;"));
    }

    private static T Scalar<T>(SqliteConnection connection, string statement)
    {
        using var command = connection.CreateCommand();
        command.CommandText = statement;
        return (T)command.ExecuteScalar()!;
    }
}

public sealed record SqlitePersistenceDiagnosticSnapshot(
    string DatabasePathIdentity,
    long SchemaVersion,
    string JournalMode,
    string IntegrityCheck,
    string QuickCheck);
