using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Datasets;

internal sealed class MaterializeDatasetUseCase : IMaterializeDatasetUseCase
{
    private readonly IHistoricalObservationStore historicalObservationStore;

    public MaterializeDatasetUseCase(IHistoricalObservationStore historicalObservationStore)
    {
        ArgumentNullException.ThrowIfNull(historicalObservationStore);
        this.historicalObservationStore = historicalObservationStore;
    }

    public DatasetMaterializationResult Execute(DatasetDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        var retrievalActivity = AIQuantTradingResearch.Application.Pipelines.PipelineObservability.StartStage(
            AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.HistoricalObservationRetrieval);
        long retrievalStarted = AIQuantTradingResearch.Application.Pipelines.PipelineObservability.GetTimestamp();
        HistoricalObservationResult sourceResult;

        try
        {
            sourceResult = historicalObservationStore.Retrieve(definition.Target)
                ?? throw new InvalidOperationException("The historical observation store returned no result.");
        }
        catch
        {
            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
                retrievalActivity,
                retrievalStarted,
                AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
                AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.HistoricalObservationRetrieval,
                "failed");
            retrievalActivity?.Dispose();
            throw;
        }

        if (!sourceResult.IsSuccess)
        {
            var result = sourceResult.Failure switch
            {
                PersistenceFailure.Unavailable =>
                    DatasetMaterializationResult.Failed(DatasetMaterializationFailure.SourceHistoryUnavailable),
                PersistenceFailure.InvalidData =>
                    DatasetMaterializationResult.Failed(DatasetMaterializationFailure.IntegrityConflict),
                _ => throw new InvalidOperationException("The historical observation store returned an unknown failure."),
            };

            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
                retrievalActivity,
                retrievalStarted,
                AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
                AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.HistoricalObservationRetrieval,
                "failed",
                result.Failure == DatasetMaterializationFailure.SourceHistoryUnavailable
                    ? AIQuantTradingResearch.Application.Pipelines.PipelineFailureCategory.DependencyUnavailable
                    : AIQuantTradingResearch.Application.Pipelines.PipelineFailureCategory.InvalidEvidence);
            retrievalActivity?.Dispose();
            return result;
        }

        AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
            retrievalActivity,
            retrievalStarted,
            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
            AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.HistoricalObservationRetrieval,
            "success");
        retrievalActivity?.Dispose();

        using var materializationActivity = AIQuantTradingResearch.Application.Pipelines.PipelineObservability.StartStage(
            AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.DatasetMaterialization);
        long materializationStarted = AIQuantTradingResearch.Application.Pipelines.PipelineObservability.GetTimestamp();
        var history = sourceResult.Observations
            ?? throw new InvalidOperationException("A successful historical observation result contained no observations.");

        try
        {
            var observations = history
                .Where(observation => observation.Instant >= definition.From && observation.Instant < definition.To)
                .OrderBy(static observation => observation.Instant.UtcTicks)
                .ToArray();

            DatasetMaterializationResult result = CreateSnapshot(
                definition,
                observations,
                DatasetSourceAuthority.AcceptedRelease11HistoricalObservations);
            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
                materializationActivity,
                materializationStarted,
                AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
                AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.DatasetMaterialization,
                "success",
                sourceAuthority: DatasetSourceAuthority.AcceptedRelease11HistoricalObservations);
            return result;
        }
        catch
        {
            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
                materializationActivity,
                materializationStarted,
                AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
                AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.DatasetMaterialization,
                "failed");
            throw;
        }
    }

    public DatasetMaterializationResult Execute(
        DatasetDefinition definition,
        IReadOnlyList<PriceObservation> observations)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(observations);

        using var materializationActivity = AIQuantTradingResearch.Application.Pipelines.PipelineObservability.StartStage(
            AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.DatasetMaterialization);
        long materializationStarted = AIQuantTradingResearch.Application.Pipelines.PipelineObservability.GetTimestamp();

        try
        {
            DatasetMaterializationResult result = CreateSnapshot(
                definition,
                observations,
                DatasetSourceAuthority.Release19SimulatedLiveReplay);
            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
                materializationActivity,
                materializationStarted,
                AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
                AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.DatasetMaterialization,
                "success",
                sourceAuthority: DatasetSourceAuthority.Release19SimulatedLiveReplay);
            return result;
        }
        catch
        {
            AIQuantTradingResearch.Application.Pipelines.PipelineObservability.Complete(
                materializationActivity,
                materializationStarted,
                AIQuantTradingResearch.Application.Pipelines.PipelineObservability.PipelineStageActivityName,
                AIQuantTradingResearch.Application.Pipelines.ResearchPipelineStage.DatasetMaterialization,
                "failed");
            throw;
        }
    }

    private static DatasetMaterializationResult CreateSnapshot(
        DatasetDefinition definition,
        IReadOnlyList<PriceObservation> observations,
        DatasetSourceAuthority sourceAuthority)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(observations);

        DatasetLineage.ValidateObservations(observations, nameof(observations));

        var definitionIdentity = DatasetIdentityComputer.ComputeDefinitionIdentity(definition);
        var researchDatasetIdentity = DatasetIdentityComputer.ComputeResearchDatasetIdentity(definition);
        var sourceStateIdentity = DatasetIdentityComputer.ComputeSourceStateIdentity(definition.Target, observations);
        var snapshotIdentity = DatasetIdentityComputer.ComputeSnapshotIdentity(definitionIdentity, sourceStateIdentity);
        var version = new DatasetVersion(snapshotIdentity);
        var coverage = new DatasetCoverage(
            definition.From,
            definition.To,
            observations.Count,
            observations.Count == 0 ? null : observations[0].Instant,
            observations.Count == 0 ? null : observations[^1].Instant);
        var provenance = new DatasetProvenance(
            definition,
            definitionIdentity,
            researchDatasetIdentity,
            sourceStateIdentity,
            snapshotIdentity,
            version,
            sourceAuthority,
            observations.Count);
        var lineage = new DatasetLineage(definitionIdentity, sourceStateIdentity, observations);
        var snapshot = new DatasetSnapshotCandidate(
            definition,
            definitionIdentity,
            researchDatasetIdentity,
            sourceStateIdentity,
            snapshotIdentity,
            version,
            observations,
            coverage,
            provenance,
            lineage);

        return DatasetMaterializationResult.Materialized(snapshot);
    }
}
