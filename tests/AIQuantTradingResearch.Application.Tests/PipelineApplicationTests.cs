using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class PipelineApplicationTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));
    private static readonly DateTimeOffset To = From.AddDays(2);

    [Fact]
    public void PipelineIdentitiesAreTypedCanonicalAndRejectMalformedFingerprints()
    {
        const string fingerprint = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";

        Assert.Equal(PipelineIdentityScheme.Name, new PipelineDefinitionIdentity(fingerprint).Scheme);
        Assert.Equal(fingerprint, new PipelineExecutionIdentity(fingerprint).Fingerprint);
        Assert.Throws<ArgumentException>(() => new PipelineDefinitionIdentity(fingerprint.ToUpperInvariant()));
        Assert.Throws<ArgumentException>(() => new PipelineExecutionIdentity("not-a-fingerprint"));
    }

    [Fact]
    public void PipelineDefinitionPreservesTheFixedFiveStageTopology()
    {
        var request = CreateRequest();

        Assert.Equal(
            [
                ResearchPipelineStage.HistoricalObservationRetrieval,
                ResearchPipelineStage.DatasetMaterialization,
                ResearchPipelineStage.SnapshotPersistence,
                ResearchPipelineStage.CatalogRegistration,
                ResearchPipelineStage.StructuredResultEvidence,
            ],
            request.Definition.Stages);
        Assert.Equal(PipelineDefinition.Topology, request.Definition.Stages);
        Assert.Same(request.DatasetDefinition, request.Definition.DatasetDefinition);
    }

    [Fact]
    public void PipelineIdentityIsCultureInvariantAndChangesOnlyForRelevantSemantics()
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;
        var datasetIdentity = DatasetIdentityComputer.ComputeDefinitionIdentity(CreateDefinition());

        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
            var first = PipelineIdentityComputer.ComputeDefinitionIdentity(datasetIdentity);
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
            var second = PipelineIdentityComputer.ComputeDefinitionIdentity(datasetIdentity);
            var changed = PipelineIdentityComputer.ComputeDefinitionIdentity(
                DatasetIdentityComputer.ComputeDefinitionIdentity(new DatasetDefinition("MSFT", From, To)));

            Assert.Equal(first, second);
            Assert.NotEqual(first, changed);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }

    [Fact]
    public void ExecuteReturnsNewlyAcceptedSuccessAndProjectsEstablishedEvidence()
    {
        var candidate = Candidate([new PriceObservation(From, 10m)]);
        var materializer = new StubMaterializer(DatasetMaterializationResult.Materialized(candidate));
        var store = new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted));
        var catalog = new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered));

        var result = new PipelineExecutionUseCase(materializer, store, catalog).Execute(CreateRequest());
        var evidence = PipelineExecutionEvidence.From(result);

        Assert.True(result.IsSuccess);
        Assert.Equal(PipelineSuccessDisposition.NewlyAccepted, result.Disposition);
        Assert.Equal(PipelineDefinition.SemanticModelVersion, evidence.SemanticModelVersion);
        Assert.Equal(PipelineDefinition.Topology, evidence.Topology);
        Assert.Equal(candidate.SnapshotIdentity, evidence.SnapshotIdentity);
        Assert.Equal(candidate.Version, evidence.DatasetVersion);
        Assert.Equal(candidate.SourceStateIdentity, evidence.SourceStateIdentity);
        Assert.Equal(5, evidence.Stages.Count);
        Assert.All(evidence.Stages, stage => Assert.NotEqual(PipelineStageOutcome.Failed, stage.Outcome));
        Assert.Equal(1, materializer.Calls);
        Assert.Equal(1, store.Calls);
        Assert.Equal(1, catalog.Calls);
    }

    [Fact]
    public void EquivalentExistingExecutionPreservesTheDeterministicExecutionIdentity()
    {
        var candidate = Candidate([new PriceObservation(From, 10m)]);
        var request = CreateRequest();

        var first = Execute(request, candidate, DatasetSnapshotStoreOutcome.NewlyAccepted, DatasetCatalogRegistrationOutcome.NewlyRegistered);
        var second = Execute(request, candidate, DatasetSnapshotStoreOutcome.EquivalentExisting, DatasetCatalogRegistrationOutcome.EquivalentExisting);

        Assert.Equal(PipelineSuccessDisposition.NewlyAccepted, first.Disposition);
        Assert.Equal(PipelineSuccessDisposition.EquivalentExisting, second.Disposition);
        Assert.Equal(first.ExecutionIdentity, second.ExecutionIdentity);
        Assert.Equal(first.DefinitionIdentity, second.DefinitionIdentity);
    }

    [Fact]
    public void EmptyDatasetIsAValidSuccessfulPipelineResult()
    {
        var result = Execute(
            CreateRequest(),
            Candidate([]),
            DatasetSnapshotStoreOutcome.NewlyAccepted,
            DatasetCatalogRegistrationOutcome.NewlyRegistered);

        Assert.True(result.IsSuccess);
        Assert.Equal(PipelineSuccessDisposition.NewlyAccepted, result.Disposition);
        Assert.True(result.SnapshotIdentity is not null);
    }

    [Theory]
    [InlineData(DatasetMaterializationFailure.SourceHistoryUnavailable, ResearchPipelineStage.HistoricalObservationRetrieval, PipelineFailureCategory.DependencyUnavailable)]
    [InlineData(DatasetMaterializationFailure.IntegrityConflict, ResearchPipelineStage.HistoricalObservationRetrieval, PipelineFailureCategory.InvalidEvidence)]
    [InlineData(DatasetMaterializationFailure.SnapshotStoreUnavailable, ResearchPipelineStage.DatasetMaterialization, PipelineFailureCategory.DependencyUnavailable)]
    public void MaterializationFailuresAreFirstFailureAndFailStop(
        DatasetMaterializationFailure failure,
        ResearchPipelineStage expectedStage,
        PipelineFailureCategory expectedCategory)
    {
        var materializer = new StubMaterializer(DatasetMaterializationResult.Failed(failure));
        var store = new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted));
        var catalog = new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered));

        var result = new PipelineExecutionUseCase(materializer, store, catalog).Execute(CreateRequest());

        Assert.False(result.IsSuccess);
        Assert.Equal(expectedStage, result.FailingStage);
        Assert.Equal(expectedCategory, result.FailureCategory);
        Assert.Equal(expectedStage, result.Provenance.Stages[^1].Stage);
        Assert.Equal(PipelineStageOutcome.Failed, result.Provenance.Stages[^1].Outcome);
        Assert.Equal(1, materializer.Calls);
        Assert.Equal(0, store.Calls);
        Assert.Equal(0, catalog.Calls);
    }

    [Theory]
    [InlineData(DatasetStoreFailure.Unavailable, PipelineFailureCategory.DependencyUnavailable)]
    [InlineData(DatasetStoreFailure.InvalidData, PipelineFailureCategory.InvalidEvidence)]
    public void SnapshotPersistenceFailuresAreFirstFailureAndDoNotRegisterCatalog(
        DatasetStoreFailure failure,
        PipelineFailureCategory expectedCategory)
    {
        var candidate = Candidate([]);
        var catalog = new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered));
        var result = new PipelineExecutionUseCase(
            new StubMaterializer(DatasetMaterializationResult.Materialized(candidate)),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Failed(failure)),
            catalog).Execute(CreateRequest());

        Assert.Equal(ResearchPipelineStage.SnapshotPersistence, result.FailingStage);
        Assert.Equal(expectedCategory, result.FailureCategory);
        Assert.Equal(3, result.Provenance.Stages.Count);
        Assert.Equal(0, catalog.Calls);
    }

    [Theory]
    [InlineData(DatasetCatalogRegistrationOutcome.IntegrityConflict, null, PipelineFailureCategory.IntegrityConflict)]
    [InlineData(null, DatasetStoreFailure.Unavailable, PipelineFailureCategory.DependencyUnavailable)]
    [InlineData(null, DatasetStoreFailure.InvalidData, PipelineFailureCategory.InvalidEvidence)]
    public void CatalogFailuresAreFirstFailureWithNoStructuredEvidenceStage(
        DatasetCatalogRegistrationOutcome? outcome,
        DatasetStoreFailure? failure,
        PipelineFailureCategory expectedCategory)
    {
        var registration = outcome is not null
            ? DatasetCatalogRegistrationResult.Completed(outcome.Value)
            : DatasetCatalogRegistrationResult.Failed(failure!.Value);
        var result = new PipelineExecutionUseCase(
            new StubMaterializer(DatasetMaterializationResult.Materialized(Candidate([]))),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)),
            new StubCatalog(registration)).Execute(CreateRequest());

        Assert.Equal(ResearchPipelineStage.CatalogRegistration, result.FailingStage);
        Assert.Equal(expectedCategory, result.FailureCategory);
        Assert.Equal(4, result.Provenance.Stages.Count);
        Assert.Equal(PipelineStageOutcome.Failed, result.Provenance.Stages[^1].Outcome);
    }

    [Fact]
    public void InvalidRequestIsRejectedBeforeAnyDependencyIsInvoked()
    {
        var definition = CreateDefinition();
        var request = new PipelineRequest(new PipelineDefinition(
            definition,
            new PipelineDefinitionIdentity(new string('a', 64))));
        var materializer = new StubMaterializer(DatasetMaterializationResult.Materialized(Candidate([])));

        Assert.Throws<ArgumentException>(() => new PipelineExecutionUseCase(
            materializer,
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)),
            new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered))).Execute(request));
        Assert.Equal(0, materializer.Calls);
    }

    [Fact]
    public void ContractEvidenceRejectsOutOfOrderStagesAndIdentityMismatches()
    {
        var definitionIdentity = new PipelineDefinitionIdentity(new string('b', 64));
        var executionIdentity = new PipelineExecutionIdentity(new string('c', 64));
        var datasetIdentity = new DatasetDefinitionIdentity(new string('d', 64));

        Assert.Throws<ArgumentException>(() => new PipelineProvenance(
            definitionIdentity,
            executionIdentity,
            datasetIdentity,
            null,
            [new PipelineStageEvidence(ResearchPipelineStage.DatasetMaterialization, PipelineStageOutcome.NewlyAccepted)]));

        Assert.Throws<ArgumentException>(() => new PipelineProvenance(
            definitionIdentity,
            executionIdentity,
            datasetIdentity,
            null,
            [
                new PipelineStageEvidence(ResearchPipelineStage.HistoricalObservationRetrieval, PipelineStageOutcome.Failed, failureCategory: PipelineFailureCategory.InvalidInput),
                new PipelineStageEvidence(ResearchPipelineStage.DatasetMaterialization, PipelineStageOutcome.NewlyAccepted),
            ]));
    }

    [Fact]
    public void UnexpectedDependencyExceptionsPropagateWithoutSyntheticFailureEvidence()
    {
        var exception = Assert.Throws<InvalidOperationException>(() => new PipelineExecutionUseCase(
            new ThrowingMaterializer(),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)),
            new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered))).Execute(CreateRequest()));

        Assert.Equal("probe failure", exception.Message);
    }

    private static PipelineExecutionResult Execute(
        PipelineRequest request,
        DatasetSnapshotCandidate candidate,
        DatasetSnapshotStoreOutcome storeOutcome,
        DatasetCatalogRegistrationOutcome catalogOutcome) =>
        new PipelineExecutionUseCase(
            new StubMaterializer(DatasetMaterializationResult.Materialized(candidate)),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(storeOutcome)),
            new StubCatalog(DatasetCatalogRegistrationResult.Completed(catalogOutcome))).Execute(request);

    private static DatasetDefinition CreateDefinition() => new("AAPL", From, To);

    private static PipelineRequest CreateRequest()
    {
        var definition = CreateDefinition();
        var datasetDefinitionIdentity = DatasetIdentityComputer.ComputeDefinitionIdentity(definition);
        return new PipelineRequest(new PipelineDefinition(
            definition,
            PipelineIdentityComputer.ComputeDefinitionIdentity(datasetDefinitionIdentity)));
    }

    private static DatasetSnapshotCandidate Candidate(IReadOnlyList<PriceObservation> observations) =>
        new MaterializeDatasetUseCase(new StubHistoryStore(HistoricalObservationResult.Retrieved(observations)))
            .Execute(CreateDefinition()).Snapshot!;

    private sealed class StubHistoryStore(HistoricalObservationResult result) : IHistoricalObservationStore
    {
        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> observations) =>
            throw new NotSupportedException();

        public HistoricalObservationResult Retrieve(string target) => result;
    }

    private sealed class StubMaterializer(DatasetMaterializationResult result) : IMaterializeDatasetUseCase
    {
        public int Calls { get; private set; }

        public DatasetMaterializationResult Execute(DatasetDefinition definition)
        {
            Calls++;
            return result;
        }
    }

    private sealed class ThrowingMaterializer : IMaterializeDatasetUseCase
    {
        public DatasetMaterializationResult Execute(DatasetDefinition definition) =>
            throw new InvalidOperationException("probe failure");
    }

    private sealed class StubSnapshotStore(DatasetSnapshotStoreResult result) : IDatasetSnapshotStore
    {
        public int Calls { get; private set; }

        public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot)
        {
            Calls++;
            return result;
        }

        public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity) =>
            throw new NotSupportedException();
    }

    private sealed class StubCatalog(DatasetCatalogRegistrationResult result) : IDatasetCatalog
    {
        public int Calls { get; private set; }

        public DatasetCatalogRegistrationResult Register(DatasetCatalogEntry entry)
        {
            Calls++;
            return result;
        }

        public DatasetCatalogLookupResult Find(DatasetSnapshotIdentity snapshotIdentity) =>
            throw new NotSupportedException();
    }
}
