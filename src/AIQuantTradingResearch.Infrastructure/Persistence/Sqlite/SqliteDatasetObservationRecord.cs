namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed record SqliteDatasetObservationRecord(
    string SnapshotIdentity,
    int Ordinal,
    long InstantUtcTicks,
    short OffsetMinutes,
    string PriceText);
