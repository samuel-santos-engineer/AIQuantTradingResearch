namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class ResearchStateSchema
{
    // Dedicated WP03 DB. Never pass the historical observation DB to this store.
    internal const string Sql = """
        CREATE TABLE metadata(id INTEGER PRIMARY KEY CHECK(id=1), schema_version INTEGER NOT NULL CHECK(schema_version=1),
          generation TEXT NOT NULL, revision INTEGER NOT NULL CHECK(revision>=0)) STRICT;
        CREATE TABLE instruments(instrument INTEGER PRIMARY KEY CHECK(instrument IN(0,1)), version INTEGER NOT NULL CHECK(version>=0),
          last_commit TEXT, last_poll TEXT, payload TEXT NOT NULL) STRICT;
        CREATE TABLE checkpoints(instrument INTEGER PRIMARY KEY REFERENCES instruments(instrument), version INTEGER NOT NULL,
          payload TEXT NOT NULL, checksum TEXT NOT NULL) STRICT;
        CREATE TABLE commits(revision INTEGER PRIMARY KEY, previous_hash TEXT NOT NULL, data_hash TEXT NOT NULL, hash TEXT NOT NULL) STRICT;
        """;
}
