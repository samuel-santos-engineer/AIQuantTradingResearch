using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Visualization;

namespace AIQuantTradingResearch.Application.Pipelines
{
    internal sealed class PipelineExecutionUseCase : IPipelineExecutionUseCase
    {
        private readonly IMaterializeDatasetUseCase materializeDataset;
        private readonly IDatasetSnapshotStore snapshotStore;
        private readonly IDatasetCatalog datasetCatalog;
        private readonly IFeatureComputer featureComputer;

        public PipelineExecutionUseCase(
            IMaterializeDatasetUseCase materializeDataset,
            IDatasetSnapshotStore snapshotStore,
            IDatasetCatalog datasetCatalog,
            IFeatureComputer? featureComputer = null)
        {
            ArgumentNullException.ThrowIfNull(materializeDataset);
            ArgumentNullException.ThrowIfNull(snapshotStore);
            ArgumentNullException.ThrowIfNull(datasetCatalog);

            this.materializeDataset = materializeDataset;
            this.snapshotStore = snapshotStore;
            this.datasetCatalog = datasetCatalog;
            this.featureComputer = featureComputer ?? new SimpleReturnFeatureComputer();
        }

        public PipelineExecutionResult Execute(PipelineRequest request)
        {
            using var executionActivity = PipelineObservability.StartPipelineExecution();
            long executionStarted = PipelineObservability.GetTimestamp();

            try
            {
                (DatasetDefinitionIdentity datasetDefinitionIdentity, PipelineDefinitionIdentity definitionIdentity) =
                    PipelineValidation.ValidateRequest(request);
                DatasetDefinition definition = request.DatasetDefinition;
                DatasetMaterializationResult materialization = materializeDataset.Execute(definition)
                    ?? throw new InvalidOperationException("The dataset materialization use case returned no result.");
                PipelineExecutionResult result = ExecuteCanonical(
                    request,
                    datasetDefinitionIdentity,
                    definitionIdentity,
                    definition,
                    materialization);

                PipelineObservability.Complete(
                    executionActivity,
                    executionStarted,
                    PipelineObservability.PipelineExecutionActivityName,
                    null,
                    result.IsSuccess ? "success" : "failed",
                    result.FailureCategory);
                return result;
            }
            catch (OperationCanceledException)
            {
                PipelineObservability.Complete(
                    executionActivity,
                    executionStarted,
                    PipelineObservability.PipelineExecutionActivityName,
                    null,
                    "cancelled");
                throw;
            }
            catch
            {
                PipelineObservability.Complete(
                    executionActivity,
                    executionStarted,
                    PipelineObservability.PipelineExecutionActivityName,
                    null,
                    "failed");
                throw;
            }
        }

        public PipelineExecutionResult Execute(
            PipelineRequest request,
            IReadOnlyList<AIQuantTradingResearch.Domain.PriceObservation> observations)
        {
            using var executionActivity = PipelineObservability.StartPipelineExecution();
            long executionStarted = PipelineObservability.GetTimestamp();

            try
            {
                (DatasetDefinitionIdentity datasetDefinitionIdentity, PipelineDefinitionIdentity definitionIdentity) =
                    PipelineValidation.ValidateRequest(request);
                DatasetDefinition definition = request.DatasetDefinition;
                DatasetMaterializationResult materialization = materializeDataset.Execute(definition, observations)
                    ?? throw new InvalidOperationException("The dataset materialization use case returned no result.");
                PipelineExecutionResult result = ExecuteCanonical(
                    request,
                    datasetDefinitionIdentity,
                    definitionIdentity,
                    definition,
                    materialization);

                PipelineObservability.Complete(
                    executionActivity,
                    executionStarted,
                    PipelineObservability.PipelineExecutionActivityName,
                    null,
                    result.IsSuccess ? "success" : "failed",
                    result.FailureCategory);
                return result;
            }
            catch (OperationCanceledException)
            {
                PipelineObservability.Complete(
                    executionActivity,
                    executionStarted,
                    PipelineObservability.PipelineExecutionActivityName,
                    null,
                    "cancelled");
                throw;
            }
            catch
            {
                PipelineObservability.Complete(
                    executionActivity,
                    executionStarted,
                    PipelineObservability.PipelineExecutionActivityName,
                    null,
                    "failed");
                throw;
            }
        }

        private PipelineExecutionResult ExecuteCanonical(
            PipelineRequest request,
            DatasetDefinitionIdentity datasetDefinitionIdentity,
            PipelineDefinitionIdentity definitionIdentity,
            DatasetDefinition definition,
            DatasetMaterializationResult materialization)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (!materialization.IsSuccess)
            {
                DatasetMaterializationFailure failure = materialization.Failure!.Value;
                ResearchPipelineStage failingStage = failure == DatasetMaterializationFailure.SnapshotStoreUnavailable
                    ? ResearchPipelineStage.DatasetMaterialization
                    : ResearchPipelineStage.HistoricalObservationRetrieval;
                PipelineFailureCategory category = MapMaterializationFailure(failure);
                IReadOnlyList<PipelineStageEvidence> stages = failingStage
                    == ResearchPipelineStage.HistoricalObservationRetrieval
                    ? [new PipelineStageEvidence(failingStage, PipelineStageOutcome.Failed, failureCategory: category)]
                    :
                    [
                        new PipelineStageEvidence(
                            ResearchPipelineStage.HistoricalObservationRetrieval,
                            PipelineStageOutcome.NewlyAccepted),
                        new PipelineStageEvidence(failingStage, PipelineStageOutcome.Failed, failureCategory: category),
                    ];

                return Failure(
                    definitionIdentity,
                    definition,
                    datasetDefinitionIdentity,
                    null,
                    failingStage,
                    category,
                    stages);
            }

            DatasetSnapshotCandidate snapshot = materialization.Snapshot!;
            var establishedStages = new List<PipelineStageEvidence>
            {
                new(ResearchPipelineStage.HistoricalObservationRetrieval, PipelineStageOutcome.NewlyAccepted),
                new(ResearchPipelineStage.DatasetMaterialization, PipelineStageOutcome.NewlyAccepted),
            };

            DatasetSnapshotStoreResult persistence;
            using (var persistenceActivity = PipelineObservability.StartStage(ResearchPipelineStage.SnapshotPersistence))
            {
                long persistenceStarted = PipelineObservability.GetTimestamp();

                try
                {
                    persistence = snapshotStore.Store(snapshot)
                        ?? throw new InvalidOperationException("The dataset snapshot store returned no result.");
                }
                catch
                {
                    PipelineObservability.Complete(
                        persistenceActivity,
                        persistenceStarted,
                        PipelineObservability.PipelineStageActivityName,
                        ResearchPipelineStage.SnapshotPersistence,
                        "failed");
                    throw;
                }

                if (!persistence.HasOutcome || persistence.Outcome == DatasetSnapshotStoreOutcome.IntegrityConflict)
                {
                    PipelineFailureCategory category = persistence.HasOutcome
                        ? PipelineFailureCategory.IntegrityConflict
                        : MapStoreFailure(persistence.Failure!.Value);
                    establishedStages.Add(new PipelineStageEvidence(
                        ResearchPipelineStage.SnapshotPersistence,
                        PipelineStageOutcome.Failed,
                        failureCategory: category));

                    PipelineObservability.Complete(
                        persistenceActivity,
                        persistenceStarted,
                        PipelineObservability.PipelineStageActivityName,
                        ResearchPipelineStage.SnapshotPersistence,
                        "failed",
                        category);
                    return Failure(
                        definitionIdentity,
                        definition,
                        datasetDefinitionIdentity,
                        snapshot.SourceStateIdentity,
                        ResearchPipelineStage.SnapshotPersistence,
                        category,
                        establishedStages);
                }

                establishedStages.Add(new PipelineStageEvidence(
                    ResearchPipelineStage.SnapshotPersistence,
                    persistence.Outcome == DatasetSnapshotStoreOutcome.NewlyAccepted
                        ? PipelineStageOutcome.NewlyAccepted
                        : PipelineStageOutcome.EquivalentExisting,
                    outputIdentityReferences: [snapshot.SnapshotIdentity.Fingerprint]));
                PipelineObservability.Complete(
                    persistenceActivity,
                    persistenceStarted,
                    PipelineObservability.PipelineStageActivityName,
                    ResearchPipelineStage.SnapshotPersistence,
                    "success");
            }

            DatasetCatalogRegistrationResult registration;
            using (var catalogActivity = PipelineObservability.StartStage(ResearchPipelineStage.CatalogRegistration))
            {
                long catalogStarted = PipelineObservability.GetTimestamp();

                try
                {
                    registration = datasetCatalog.Register(new DatasetCatalogEntry(snapshot))
                        ?? throw new InvalidOperationException("The dataset catalog returned no registration result.");
                }
                catch
                {
                    PipelineObservability.Complete(
                        catalogActivity,
                        catalogStarted,
                        PipelineObservability.PipelineStageActivityName,
                        ResearchPipelineStage.CatalogRegistration,
                        "failed");
                    throw;
                }

                if (!registration.HasOutcome || registration.Outcome == DatasetCatalogRegistrationOutcome.IntegrityConflict)
                {
                    PipelineFailureCategory category = registration.HasOutcome
                        ? PipelineFailureCategory.IntegrityConflict
                        : MapStoreFailure(registration.Failure!.Value);
                    establishedStages.Add(new PipelineStageEvidence(
                        ResearchPipelineStage.CatalogRegistration,
                        PipelineStageOutcome.Failed,
                        failureCategory: category));

                    PipelineObservability.Complete(
                        catalogActivity,
                        catalogStarted,
                        PipelineObservability.PipelineStageActivityName,
                        ResearchPipelineStage.CatalogRegistration,
                        "failed",
                        category);
                    return Failure(
                        definitionIdentity,
                        definition,
                        datasetDefinitionIdentity,
                        snapshot.SourceStateIdentity,
                        ResearchPipelineStage.CatalogRegistration,
                        category,
                        establishedStages,
                        snapshot.SnapshotIdentity,
                        snapshot.Version);
                }

                establishedStages.Add(new PipelineStageEvidence(
                    ResearchPipelineStage.CatalogRegistration,
                    registration.Outcome == DatasetCatalogRegistrationOutcome.NewlyRegistered
                        ? PipelineStageOutcome.NewlyAccepted
                        : PipelineStageOutcome.EquivalentExisting,
                    outputIdentityReferences: [snapshot.SnapshotIdentity.Fingerprint]));
                PipelineObservability.Complete(
                    catalogActivity,
                    catalogStarted,
                    PipelineObservability.PipelineStageActivityName,
                    ResearchPipelineStage.CatalogRegistration,
                    "success");
            }

            using (var evidenceActivity = PipelineObservability.StartStage(ResearchPipelineStage.StructuredResultEvidence))
            {
                long evidenceStarted = PipelineObservability.GetTimestamp();

                try
                {
                    establishedStages.Add(new PipelineStageEvidence(
                        ResearchPipelineStage.StructuredResultEvidence,
                        persistence.Outcome == DatasetSnapshotStoreOutcome.NewlyAccepted
                            ? PipelineStageOutcome.NewlyAccepted
                            : PipelineStageOutcome.EquivalentExisting,
                        outputIdentityReferences: [snapshot.SnapshotIdentity.Fingerprint]));

                    PipelineExecutionIdentity executionIdentity = PipelineIdentityComputer.ComputeSuccessIdentity(
                        definitionIdentity,
                        definition,
                        snapshot.SourceStateIdentity,
                        snapshot.SnapshotIdentity);
                    var provenance = new PipelineProvenance(
                        definitionIdentity,
                        executionIdentity,
                        datasetDefinitionIdentity,
                        snapshot.SourceStateIdentity,
                        establishedStages,
                        snapshot.SnapshotIdentity,
                        snapshot.Version);

                    var featureSet = featureComputer.Compute(
                        new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, snapshot.SnapshotIdentity, snapshot.Version), snapshot);
                    var presentationInputs = new HistoricalPresentationInputs(
                        snapshot.Observations.Select(static observation => new HistoricalPresentationObservation(observation.Instant, observation.Price)),
                        HistoricalPresentationFeature.From(featureSet, snapshot.Observations.Count), snapshot.SnapshotIdentity, snapshot.Version, establishedStages);

                    PipelineExecutionResult result = PipelineExecutionResult.Succeeded(
                        provenance,
                        persistence.Outcome == DatasetSnapshotStoreOutcome.NewlyAccepted
                            ? PipelineSuccessDisposition.NewlyAccepted
                            : PipelineSuccessDisposition.EquivalentExisting,
                        presentationInputs);
                    PipelineObservability.Complete(
                        evidenceActivity,
                        evidenceStarted,
                        PipelineObservability.PipelineStageActivityName,
                        ResearchPipelineStage.StructuredResultEvidence,
                        "success");
                    return result;
                }
                catch
                {
                    PipelineObservability.Complete(
                        evidenceActivity,
                        evidenceStarted,
                        PipelineObservability.PipelineStageActivityName,
                        ResearchPipelineStage.StructuredResultEvidence,
                        "failed");
                    throw;
                }
            }
        }

        private static PipelineExecutionResult Failure(
            PipelineDefinitionIdentity definitionIdentity,
            DatasetDefinition definition,
            DatasetDefinitionIdentity datasetDefinitionIdentity,
            SourceStateIdentity? sourceStateIdentity,
            ResearchPipelineStage failingStage,
            PipelineFailureCategory category,
            IReadOnlyList<PipelineStageEvidence> stages,
            DatasetSnapshotIdentity? snapshotIdentity = null,
            DatasetVersion? datasetVersion = null)
        {
            PipelineExecutionIdentity executionIdentity = PipelineIdentityComputer.ComputeFailureIdentity(
                definitionIdentity,
                definition,
                sourceStateIdentity,
                failingStage,
                category);
            var provenance = new PipelineProvenance(
                definitionIdentity,
                executionIdentity,
                datasetDefinitionIdentity,
                sourceStateIdentity,
                stages,
                snapshotIdentity,
                datasetVersion);

            return PipelineExecutionResult.Failed(provenance, failingStage, category);
        }

        private static PipelineFailureCategory MapMaterializationFailure(
            DatasetMaterializationFailure failure)
        {
            return failure switch
            {
                DatasetMaterializationFailure.InvalidDefinition => PipelineFailureCategory.InvalidInput,
                DatasetMaterializationFailure.SourceHistoryUnavailable => PipelineFailureCategory.DependencyUnavailable,
                DatasetMaterializationFailure.IntegrityConflict => PipelineFailureCategory.InvalidEvidence,
                DatasetMaterializationFailure.SnapshotStoreUnavailable => PipelineFailureCategory.DependencyUnavailable,
                _ => throw new InvalidOperationException("The dataset materialization use case returned an unknown failure."),
            };
        }

        private static PipelineFailureCategory MapStoreFailure(DatasetStoreFailure failure)
        {
            return failure switch
            {
                DatasetStoreFailure.Unavailable => PipelineFailureCategory.DependencyUnavailable,
                DatasetStoreFailure.InvalidData => PipelineFailureCategory.InvalidEvidence,
                _ => throw new InvalidOperationException("The dataset store returned an unknown failure."),
            };
        }
    }
}
