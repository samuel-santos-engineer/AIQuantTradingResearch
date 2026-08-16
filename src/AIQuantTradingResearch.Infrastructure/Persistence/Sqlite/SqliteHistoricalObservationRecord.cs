namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed record SqliteHistoricalObservationRecord(
    string Target,
    long InstantUtcTicks,
    short OffsetMinutes,
    string PriceText);
