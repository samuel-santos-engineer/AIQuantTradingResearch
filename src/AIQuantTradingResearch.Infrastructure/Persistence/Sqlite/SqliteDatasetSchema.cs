namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteDatasetSchema
{
    public const int Version = 2;

    public const string SnapshotTableName = "dataset_snapshots";

    public const string ObservationTableName = "dataset_snapshot_observations";

    public const string CreateSnapshotTableStatement = """
        CREATE TABLE IF NOT EXISTS dataset_snapshots (
            snapshot_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(snapshot_identity) = 64 AND snapshot_identity NOT GLOB '*[^0-9a-f]*'),
            definition_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(definition_identity) = 64 AND definition_identity NOT GLOB '*[^0-9a-f]*'),
            research_dataset_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(research_dataset_identity) = 64 AND research_dataset_identity NOT GLOB '*[^0-9a-f]*'),
            source_state_identity TEXT COLLATE BINARY NOT NULL
                CHECK (length(source_state_identity) = 64 AND source_state_identity NOT GLOB '*[^0-9a-f]*'),
            identity_scheme TEXT COLLATE BINARY NOT NULL CHECK (identity_scheme = 'aiq-dataset-identity-v1'),
            target TEXT COLLATE BINARY NOT NULL CHECK (length(target) > 0),
            requested_from_utc_ticks INTEGER NOT NULL,
            requested_from_offset_minutes INTEGER NOT NULL
                CHECK (requested_from_offset_minutes BETWEEN -840 AND 840),
            requested_to_utc_ticks INTEGER NOT NULL,
            requested_to_offset_minutes INTEGER NOT NULL
                CHECK (requested_to_offset_minutes BETWEEN -840 AND 840),
            ordering INTEGER NOT NULL CHECK (ordering = 0),
            observation_count INTEGER NOT NULL CHECK (observation_count >= 0),
            first_observation_utc_ticks INTEGER,
            first_observation_offset_minutes INTEGER
                CHECK (first_observation_offset_minutes BETWEEN -840 AND 840),
            last_observation_utc_ticks INTEGER,
            last_observation_offset_minutes INTEGER
                CHECK (last_observation_offset_minutes BETWEEN -840 AND 840),
            source_authority INTEGER NOT NULL CHECK (source_authority IN (0, 1)),
            PRIMARY KEY (snapshot_identity),
            CHECK (requested_from_utc_ticks < requested_to_utc_ticks),
            CHECK (
                requested_from_utc_ticks + requested_from_offset_minutes * 600000000
                    BETWEEN 0 AND 3155378975999999999
                AND requested_to_utc_ticks + requested_to_offset_minutes * 600000000
                    BETWEEN 0 AND 3155378975999999999),
            CHECK (
                (observation_count = 0
                    AND first_observation_utc_ticks IS NULL
                    AND first_observation_offset_minutes IS NULL
                    AND last_observation_utc_ticks IS NULL
                    AND last_observation_offset_minutes IS NULL)
                OR
                (observation_count > 0
                    AND first_observation_utc_ticks IS NOT NULL
                    AND first_observation_offset_minutes IS NOT NULL
                    AND last_observation_utc_ticks IS NOT NULL
                    AND last_observation_offset_minutes IS NOT NULL
                    AND first_observation_utc_ticks <= last_observation_utc_ticks
                    AND first_observation_utc_ticks >= requested_from_utc_ticks
                    AND last_observation_utc_ticks < requested_to_utc_ticks
                    AND first_observation_utc_ticks + first_observation_offset_minutes * 600000000
                        BETWEEN 0 AND 3155378975999999999
                    AND last_observation_utc_ticks + last_observation_offset_minutes * 600000000
                        BETWEEN 0 AND 3155378975999999999))
        ) STRICT, WITHOUT ROWID;
        """;

    public const string CreateObservationTableStatement = """
        CREATE TABLE IF NOT EXISTS dataset_snapshot_observations (
            snapshot_identity TEXT COLLATE BINARY NOT NULL,
            ordinal INTEGER NOT NULL CHECK (ordinal >= 0),
            instant_utc_ticks INTEGER NOT NULL,
            offset_minutes INTEGER NOT NULL CHECK (offset_minutes BETWEEN -840 AND 840),
            price_text TEXT NOT NULL CHECK (length(price_text) > 0),
            PRIMARY KEY (snapshot_identity, ordinal),
            UNIQUE (snapshot_identity, instant_utc_ticks),
            FOREIGN KEY (snapshot_identity) REFERENCES dataset_snapshots(snapshot_identity)
                ON UPDATE RESTRICT ON DELETE RESTRICT,
            CHECK (instant_utc_ticks + offset_minutes * 600000000
                BETWEEN 0 AND 3155378975999999999)
        ) STRICT, WITHOUT ROWID;
        """;
}
