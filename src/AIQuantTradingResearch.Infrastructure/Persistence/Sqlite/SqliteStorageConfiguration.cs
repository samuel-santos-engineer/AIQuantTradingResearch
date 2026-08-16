namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

public sealed class SqliteStorageConfiguration
{
    public const string SectionName = "Persistence";
    public const string DatabasePathName = "DatabasePath";

    public SqliteStorageConfiguration(string databasePath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(databasePath);
        DatabasePath = databasePath;
    }

    internal string DatabasePath { get; }
}
