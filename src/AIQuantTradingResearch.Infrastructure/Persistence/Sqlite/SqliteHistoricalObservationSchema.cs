namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteHistoricalObservationSchema
{
    public const int Version = 1;

    public const string TableName = "historical_observations";

    public const string CreateTableStatement = """
        CREATE TABLE IF NOT EXISTS historical_observations (
            target TEXT COLLATE BINARY NOT NULL,
            instant_utc_ticks INTEGER NOT NULL,
            offset_minutes INTEGER NOT NULL CHECK (offset_minutes BETWEEN -840 AND 840),
            price_text TEXT NOT NULL CHECK (length(price_text) > 0),
            PRIMARY KEY (target, instant_utc_ticks)
        ) STRICT, WITHOUT ROWID;
        """;
}
