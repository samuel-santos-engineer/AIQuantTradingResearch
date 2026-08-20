namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteSchemaValidationException : InvalidOperationException
{
    public SqliteSchemaValidationException(string message)
        : base(message)
    {
    }
}
