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

        var sourceResult = historicalObservationStore.Retrieve(definition.Target)
            ?? throw new InvalidOperationException("The historical observation store returned no result.");

        if (!sourceResult.IsSuccess)
        {
            return sourceResult.Failure switch
            {
                PersistenceFailure.Unavailable =>
                    DatasetMaterializationResult.Failed(DatasetMaterializationFailure.SourceHistoryUnavailable),
                PersistenceFailure.InvalidData =>
                    DatasetMaterializationResult.Failed(DatasetMaterializationFailure.IntegrityConflict),
                _ => throw new InvalidOperationException("The historical observation store returned an unknown failure."),
            };
        }

        var history = sourceResult.Observations
            ?? throw new InvalidOperationException("A successful historical observation result contained no observations.");

        var observations = history
            .Where(observation => observation.Instant >= definition.From && observation.Instant < definition.To)
            .OrderBy(static observation => observation.Instant.UtcTicks)
            .ToArray();

        return CreateSnapshot(
            definition,
            observations,
            DatasetSourceAuthority.AcceptedRelease11HistoricalObservations);
    }

    public DatasetMaterializationResult Execute(
        DatasetDefinition definition,
        IReadOnlyList<PriceObservation> observations)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(observations);

        return CreateSnapshot(definition, observations, DatasetSourceAuthority.Release19SimulatedLiveReplay);
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
