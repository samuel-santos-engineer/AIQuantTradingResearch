namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteExperimentResultSchema
{
    public const int Version = 3;

    public const string TableName = "experiment_results";

    public const string CreateTableStatement = """
        CREATE TABLE IF NOT EXISTS experiment_results (
            experiment_result_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(experiment_result_identity) = 64 AND experiment_result_identity NOT GLOB '*[^0-9a-f]*'),
            experiment_identity_scheme TEXT COLLATE BINARY NOT NULL
                CHECK (experiment_identity_scheme = 'aiq-experiment-identity-v1'),
            experiment_definition_name TEXT COLLATE BINARY NOT NULL
                CHECK (experiment_definition_name = 'simple-return-descriptive-summary-v1'),
            experiment_definition_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(experiment_definition_identity) = 64 AND experiment_definition_identity NOT GLOB '*[^0-9a-f]*'),
            feature_identity_scheme TEXT COLLATE BINARY NOT NULL
                CHECK (feature_identity_scheme = 'aiq-feature-identity-v1'),
            feature_set_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(feature_set_identity) = 64 AND feature_set_identity NOT GLOB '*[^0-9a-f]*'),
            feature_definition_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(feature_definition_identity) = 64 AND feature_definition_identity NOT GLOB '*[^0-9a-f]*'),
            dataset_identity_scheme TEXT COLLATE BINARY NOT NULL
                CHECK (dataset_identity_scheme = 'aiq-dataset-identity-v1'),
            snapshot_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(snapshot_identity) = 64 AND snapshot_identity NOT GLOB '*[^0-9a-f]*'),
            dataset_definition_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(dataset_definition_identity) = 64 AND dataset_definition_identity NOT GLOB '*[^0-9a-f]*'),
            research_dataset_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(research_dataset_identity) = 64 AND research_dataset_identity NOT GLOB '*[^0-9a-f]*'),
            source_state_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(source_state_identity) = 64 AND source_state_identity NOT GLOB '*[^0-9a-f]*'),
            source_authority INTEGER NOT NULL CHECK (source_authority IN (0, 1)),
            dataset_observation_count INTEGER NOT NULL CHECK (dataset_observation_count >= 0),
            summary_count INTEGER NOT NULL CHECK (summary_count >= 0),
            aggregates_present INTEGER NOT NULL CHECK (aggregates_present IN (0, 1)),
            arithmetic_mean_canonical TEXT COLLATE BINARY,
            minimum_canonical TEXT COLLATE BINARY,
            maximum_canonical TEXT COLLATE BINARY,
            PRIMARY KEY (experiment_result_identity),
            FOREIGN KEY (snapshot_identity) REFERENCES dataset_snapshots(snapshot_identity)
                ON UPDATE RESTRICT ON DELETE RESTRICT,
            CHECK (
                (summary_count = 0
                    AND aggregates_present = 0
                    AND arithmetic_mean_canonical IS NULL
                    AND minimum_canonical IS NULL
                    AND maximum_canonical IS NULL)
                OR
                (summary_count > 0
                    AND aggregates_present = 1
                    AND arithmetic_mean_canonical IS NOT NULL
                    AND length(arithmetic_mean_canonical) >= 5
                    AND arithmetic_mean_canonical NOT GLOB '*[^0-9,]*'
                    AND minimum_canonical IS NOT NULL
                    AND length(minimum_canonical) >= 5
                    AND minimum_canonical NOT GLOB '*[^0-9,]*'
                    AND maximum_canonical IS NOT NULL
                    AND length(maximum_canonical) >= 5
                    AND maximum_canonical NOT GLOB '*[^0-9,]*'))
        ) STRICT, WITHOUT ROWID;
        """;
}
