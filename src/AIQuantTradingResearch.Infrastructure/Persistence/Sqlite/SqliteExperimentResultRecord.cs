namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed record SqliteExperimentResultRecord(
    string ExperimentResultIdentity,
    string ExperimentIdentityScheme,
    string ExperimentDefinitionName,
    string ExperimentDefinitionIdentity,
    string FeatureIdentityScheme,
    string FeatureSetIdentity,
    string FeatureDefinitionIdentity,
    string DatasetIdentityScheme,
    string SnapshotIdentity,
    string DatasetDefinitionIdentity,
    string ResearchDatasetIdentity,
    string SourceStateIdentity,
    int SourceAuthority,
    int DatasetObservationCount,
    int SummaryCount,
    int AggregatesPresent,
    string? ArithmeticMeanCanonical,
    string? MinimumCanonical,
    string? MaximumCanonical);
