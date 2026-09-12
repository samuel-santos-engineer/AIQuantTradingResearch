using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SqlitePersistenceDiagnosticsTests
{
    [Fact]
    public void CollectReportsBoundedMetadataForInitializedDatabaseWithoutChangingSchemaVersion()
    {
        using var database = new TemporaryDatabase();
        var diagnostics = new SqlitePersistenceDiagnostics(database.Configuration);

        var first = diagnostics.Collect();
        var second = diagnostics.Collect();

        Assert.Equal("qualification.sqlite", first.DatabasePathIdentity);
        Assert.Equal(4L, first.SchemaVersion);
        Assert.Equal("delete", first.JournalMode, ignoreCase: true);
        Assert.Equal("ok", first.IntegrityCheck, ignoreCase: true);
        Assert.Equal("ok", first.QuickCheck, ignoreCase: true);
        Assert.Equal(first.SchemaVersion, second.SchemaVersion);
        Assert.Equal(first.JournalMode, second.JournalMode, ignoreCase: true);
    }

    private sealed class TemporaryDatabase : IDisposable
    {
        private readonly string directory = Path.Combine(Path.GetTempPath(), $"aiq-wp04-diagnostics-{Guid.NewGuid():N}");

        public TemporaryDatabase()
        {
            Directory.CreateDirectory(directory);
            Configuration = new SqliteStorageConfiguration(Path.Combine(directory, "qualification.sqlite"));
        }

        public SqliteStorageConfiguration Configuration { get; }

        public void Dispose()
        {
            Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools();
            Directory.Delete(directory, recursive: true);
        }
    }
}
