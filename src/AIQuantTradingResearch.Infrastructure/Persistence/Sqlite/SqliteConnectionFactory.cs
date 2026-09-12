using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteConnectionFactory : ISqliteConnectionFactory
{
    private const string DeleteJournalMode = "delete";
    private readonly bool createParentDirectoryForInitialization;
    private readonly string? databaseDirectory;
    private readonly string connectionString;

    public SqliteConnectionFactory(SqliteStorageConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = configuration.DatabasePath,
            Mode = SqliteOpenMode.ReadWriteCreate,
        }.ToString();
        databaseDirectory = Path.GetDirectoryName(configuration.DatabasePath);
        createParentDirectoryForInitialization = configuration.CreateParentDirectoryForInitialization;
    }

    public SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(connectionString);

        try
        {
            if (createParentDirectoryForInitialization && !string.IsNullOrWhiteSpace(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }

            connection.Open();
            EnsureDeleteJournalMode(connection);
            SqliteSchemaBootstrapper.Bootstrap(connection);
            return connection;
        }
        catch (SqliteSchemaValidationException exception)
        {
            connection.Dispose();
            throw new InvalidOperationException(exception.Message, exception);
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }

    private static void EnsureDeleteJournalMode(SqliteConnection connection)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA journal_mode = DELETE;";
        var actualMode = command.ExecuteScalar()?.ToString();
        if (!string.Equals(actualMode, DeleteJournalMode, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("SQLite DELETE journal mode could not be established.");
        }
    }
}
