namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed record SqliteDatasetSnapshotRecord(
    string SnapshotIdentity,
    string DefinitionIdentity,
    string ResearchDatasetIdentity,
    string SourceStateIdentity,
    string IdentityScheme,
    string Target,
    long RequestedFromUtcTicks,
    short RequestedFromOffsetMinutes,
    long RequestedToUtcTicks,
    short RequestedToOffsetMinutes,
    int Ordering,
    int ObservationCount,
    long? FirstObservationUtcTicks,
    short? FirstObservationOffsetMinutes,
    long? LastObservationUtcTicks,
    short? LastObservationOffsetMinutes,
    int SourceAuthority);
