using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteConnectionFactory : ISqliteConnectionFactory
{
    private readonly string connectionString;

    public SqliteConnectionFactory(SqliteStorageConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = configuration.DatabasePath,
            Mode = SqliteOpenMode.ReadWriteCreate,
        }.ToString();
    }

    public SqliteConnection OpenConnection()
    {
        var connection = new SqliteConnection(connectionString);

        try
        {
            connection.Open();
            SqliteSchemaBootstrapper.Bootstrap(connection);
            return connection;
        }
        catch
        {
            connection.Dispose();
            throw;
        }
    }
}
