namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteDatasetEvidenceException : InvalidOperationException
{
    public SqliteDatasetEvidenceException(string message)
        : base(message)
    {
    }
}
