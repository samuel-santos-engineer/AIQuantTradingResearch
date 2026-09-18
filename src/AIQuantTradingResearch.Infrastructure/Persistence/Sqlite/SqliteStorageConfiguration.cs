namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

public sealed class SqliteStorageConfiguration
{
    public const string SectionName = "Persistence";
    public const string DatabasePathName = "DatabasePath";
    public const string CreateParentDirectoryForInitializationName = "CreateParentDirectoryForInitialization";

    public SqliteStorageConfiguration(string databasePath, bool createParentDirectoryForInitialization = false)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(databasePath);
        DatabasePath = databasePath;
        CreateParentDirectoryForInitialization = createParentDirectoryForInitialization;
    }

    internal string DatabasePath { get; }

    internal bool CreateParentDirectoryForInitialization { get; }
}
