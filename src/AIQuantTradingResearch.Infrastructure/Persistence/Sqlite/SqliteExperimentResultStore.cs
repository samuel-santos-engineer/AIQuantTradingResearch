using AIQuantTradingResearch.Application.Experiments;
using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal sealed class SqliteExperimentResultStore : IDurableExperimentEvidenceStore
{
    private const string ReadStatement = """
        SELECT
            experiment_result_identity,
            experiment_identity_scheme,
            experiment_definition_name,
            experiment_definition_identity,
            feature_identity_scheme,
            feature_set_identity,
            feature_definition_identity,
            dataset_identity_scheme,
            snapshot_identity,
            dataset_definition_identity,
            research_dataset_identity,
            source_state_identity,
            source_authority,
            dataset_observation_count,
            summary_count,
            aggregates_present,
            arithmetic_mean_canonical,
            minimum_canonical,
            maximum_canonical
        FROM experiment_results
        WHERE experiment_result_identity = $identity;
        """;

    private const string InsertStatement = """
        INSERT INTO experiment_results (
            experiment_result_identity,
            experiment_identity_scheme,
            experiment_definition_name,
            experiment_definition_identity,
            feature_identity_scheme,
            feature_set_identity,
            feature_definition_identity,
            dataset_identity_scheme,
            snapshot_identity,
            dataset_definition_identity,
            research_dataset_identity,
            source_state_identity,
            source_authority,
            dataset_observation_count,
            summary_count,
            aggregates_present,
            arithmetic_mean_canonical,
            minimum_canonical,
            maximum_canonical)
        VALUES (
            $experimentResultIdentity,
            $experimentIdentityScheme,
            $experimentDefinitionName,
            $experimentDefinitionIdentity,
            $featureIdentityScheme,
            $featureSetIdentity,
            $featureDefinitionIdentity,
            $datasetIdentityScheme,
            $snapshotIdentity,
            $datasetDefinitionIdentity,
            $researchDatasetIdentity,
            $sourceStateIdentity,
            $sourceAuthority,
            $datasetObservationCount,
            $summaryCount,
            $aggregatesPresent,
            $arithmeticMeanCanonical,
            $minimumCanonical,
            $maximumCanonical);
        """;

    private readonly ISqliteConnectionFactory connectionFactory;

    public SqliteExperimentResultStore(ISqliteConnectionFactory connectionFactory)
    {
        ArgumentNullException.ThrowIfNull(connectionFactory);
        this.connectionFactory = connectionFactory;
    }

    public DurableExperimentAcceptanceResult Accept(DurableExperimentAcceptanceRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var incoming = SqliteExperimentResultMapper.ToRecord(request.Evidence);
        SqliteConnection connection;

        try
        {
            connection = connectionFactory.OpenConnection();
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return DurableExperimentAcceptanceResult.Failed(DurableExperimentEvidenceFailure.DependencyUnavailable);
        }
        catch (SqliteSchemaValidationException)
        {
            return DurableExperimentAcceptanceResult.Failed(DurableExperimentEvidenceFailure.InvalidEvidence);
        }
        catch (InvalidOperationException exception)
            when (exception.InnerException is SqliteSchemaValidationException)
        {
            return DurableExperimentAcceptanceResult.Failed(DurableExperimentEvidenceFailure.InvalidEvidence);
        }

        using (connection)
        using (var transaction = connection.BeginTransaction(deferred: false))
        {
            try
            {
                var existing = Read(connection, transaction, incoming.ExperimentResultIdentity);
                if (existing is not null)
                {
                    return existing == incoming
                        ? DurableExperimentAcceptanceResult.Accepted(
                            DurableExperimentAcceptanceDisposition.EquivalentExisting)
                        : DurableExperimentAcceptanceResult.Failed(
                            DurableExperimentEvidenceFailure.IntegrityConflict);
                }

                Insert(connection, transaction, incoming);
                transaction.Commit();
                return DurableExperimentAcceptanceResult.Accepted(
                    DurableExperimentAcceptanceDisposition.NewlyAccepted);
            }
            catch (SqliteException exception) when (IsUnavailable(exception))
            {
                return DurableExperimentAcceptanceResult.Failed(DurableExperimentEvidenceFailure.DependencyUnavailable);
            }
            catch (SqliteException exception) when (IsInvalidEvidence(exception))
            {
                return DurableExperimentAcceptanceResult.Failed(DurableExperimentEvidenceFailure.InvalidEvidence);
            }
        }
    }

    DurableExperimentRetrievalResult IDurableExperimentEvidenceStore.Retrieve(
        DurableExperimentRetrievalRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        try
        {
            using var connection = connectionFactory.OpenConnection();
            var record = Read(connection, null, request.Identity.Fingerprint);
            return record is null
                ? DurableExperimentRetrievalResult.Failed(DurableExperimentEvidenceFailure.NotFound)
                : DurableExperimentRetrievalResult.Found(SqliteExperimentResultMapper.ToEvidence(record));
        }
        catch (SqliteException exception) when (IsUnavailable(exception))
        {
            return DurableExperimentRetrievalResult.Failed(DurableExperimentEvidenceFailure.DependencyUnavailable);
        }
        catch (SqliteSchemaValidationException)
        {
            return DurableExperimentRetrievalResult.Failed(DurableExperimentEvidenceFailure.InvalidEvidence);
        }
        catch (InvalidOperationException exception)
            when (exception.InnerException is SqliteSchemaValidationException)
        {
            return DurableExperimentRetrievalResult.Failed(DurableExperimentEvidenceFailure.InvalidEvidence);
        }
        catch (ArgumentException)
        {
            return DurableExperimentRetrievalResult.Failed(DurableExperimentEvidenceFailure.InvalidEvidence);
        }
    }

    private static SqliteExperimentResultRecord? Read(
        SqliteConnection connection,
        SqliteTransaction? transaction,
        string identity)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = ReadStatement;
        command.Parameters.AddWithValue("$identity", identity);
        using var reader = command.ExecuteReader();

        if (!reader.Read())
        {
            return null;
        }

        return new SqliteExperimentResultRecord(
            reader.GetString(0),
            reader.GetString(1),
            reader.GetString(2),
            reader.GetString(3),
            reader.GetString(4),
            reader.GetString(5),
            reader.GetString(6),
            reader.GetString(7),
            reader.GetString(8),
            reader.GetString(9),
            reader.GetString(10),
            reader.GetString(11),
            checked((int)reader.GetInt64(12)),
            checked((int)reader.GetInt64(13)),
            checked((int)reader.GetInt64(14)),
            checked((int)reader.GetInt64(15)),
            reader.IsDBNull(16) ? null : reader.GetString(16),
            reader.IsDBNull(17) ? null : reader.GetString(17),
            reader.IsDBNull(18) ? null : reader.GetString(18));
    }

    private static void Insert(
        SqliteConnection connection,
        SqliteTransaction transaction,
        SqliteExperimentResultRecord record)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = InsertStatement;
        command.Parameters.AddWithValue("$experimentResultIdentity", record.ExperimentResultIdentity);
        command.Parameters.AddWithValue("$experimentIdentityScheme", record.ExperimentIdentityScheme);
        command.Parameters.AddWithValue("$experimentDefinitionName", record.ExperimentDefinitionName);
        command.Parameters.AddWithValue("$experimentDefinitionIdentity", record.ExperimentDefinitionIdentity);
        command.Parameters.AddWithValue("$featureIdentityScheme", record.FeatureIdentityScheme);
        command.Parameters.AddWithValue("$featureSetIdentity", record.FeatureSetIdentity);
        command.Parameters.AddWithValue("$featureDefinitionIdentity", record.FeatureDefinitionIdentity);
        command.Parameters.AddWithValue("$datasetIdentityScheme", record.DatasetIdentityScheme);
        command.Parameters.AddWithValue("$snapshotIdentity", record.SnapshotIdentity);
        command.Parameters.AddWithValue("$datasetDefinitionIdentity", record.DatasetDefinitionIdentity);
        command.Parameters.AddWithValue("$researchDatasetIdentity", record.ResearchDatasetIdentity);
        command.Parameters.AddWithValue("$sourceStateIdentity", record.SourceStateIdentity);
        command.Parameters.AddWithValue("$sourceAuthority", record.SourceAuthority);
        command.Parameters.AddWithValue("$datasetObservationCount", record.DatasetObservationCount);
        command.Parameters.AddWithValue("$summaryCount", record.SummaryCount);
        command.Parameters.AddWithValue("$aggregatesPresent", record.AggregatesPresent);
        command.Parameters.AddWithValue("$arithmeticMeanCanonical", (object?)record.ArithmeticMeanCanonical ?? DBNull.Value);
        command.Parameters.AddWithValue("$minimumCanonical", (object?)record.MinimumCanonical ?? DBNull.Value);
        command.Parameters.AddWithValue("$maximumCanonical", (object?)record.MaximumCanonical ?? DBNull.Value);
        command.ExecuteNonQuery();
    }

    private static bool IsUnavailable(SqliteException exception) =>
        exception.SqliteErrorCode is 5 or 6 or 7 or 8 or 10 or 13 or 14 or 15;

    private static bool IsInvalidEvidence(SqliteException exception) =>
        exception.SqliteErrorCode is 11 or 19 or 20 or 26;
}
