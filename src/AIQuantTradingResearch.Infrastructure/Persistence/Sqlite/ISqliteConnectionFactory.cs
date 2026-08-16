using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal interface ISqliteConnectionFactory
{
    SqliteConnection OpenConnection();
}
