using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class ExperimentPersistenceApplicationTests
{
    [Fact]
    public void DurableUseCaseProjectsExactEvidenceAndAcceptsNewlyAcceptedOnce()
    {
        var experiment = ExperimentFor([0.25m, -0.2m]);
        var generation = new GenerationStub(ExperimentGenerationResult.Generated(experiment));
        var store = new StoreStub(DurableExperimentAcceptanceResult.Accepted(DurableExperimentAcceptanceDisposition.NewlyAccepted));

        var result = new DurableExperimentUseCase(generation, store).Execute(Request(experiment));

        Assert.True(result.IsSuccess);
        Assert.Equal(DurableExperimentAcceptanceDisposition.NewlyAccepted, result.Disposition);
        Assert.Equal(1, generation.Calls);
        Assert.Equal(1, store.AcceptCalls);
        DurableExperimentEvidence evidence = Assert.IsType<DurableExperimentEvidence>(store.LastAcceptance!.Evidence);
        Assert.Equal(experiment.Identity, evidence.Identity);
        Assert.Equal(experiment.Provenance.FeatureSetIdentity, evidence.Provenance.FeatureSetIdentity);
        Assert.Equal(experiment.Provenance.FeatureProvenance.SnapshotIdentity, evidence.Provenance.SnapshotIdentity);
        Assert.Equal(2, evidence.Summary.Count);
        Assert.Equal(0.025m, evidence.Summary.ArithmeticMean);
    }

    [Fact]
    public void DurableUseCasePreservesEmptyEvidenceAndEquivalentExistingDisposition()
    {
        var experiment = ExperimentFor([]);
        var store = new StoreStub(DurableExperimentAcceptanceResult.Accepted(DurableExperimentAcceptanceDisposition.EquivalentExisting));
        var result = new DurableExperimentUseCase(new GenerationStub(ExperimentGenerationResult.Generated(experiment)), store).Execute(Request(experiment));

        Assert.True(result.IsSuccess);
        Assert.Equal(DurableExperimentAcceptanceDisposition.EquivalentExisting, result.Disposition);
        Assert.Equal(experiment.Identity, result.Evidence!.Identity);
        Assert.Equal(0, result.Evidence.Summary.Count);
        Assert.False(result.Evidence.Summary.HasAggregates);
        Assert.Null(result.Evidence.Summary.ArithmeticMean);
    }

    [Theory]
    [InlineData(ExperimentGenerationFailure.FeatureSetNotFound, DurableExperimentEvidenceFailure.NotFound)]
    [InlineData(ExperimentGenerationFailure.DependencyUnavailable, DurableExperimentEvidenceFailure.DependencyUnavailable)]
    [InlineData(ExperimentGenerationFailure.InvalidFeatureEvidence, DurableExperimentEvidenceFailure.InvalidEvidence)]
    public void UpstreamFailureShortCircuitsStore(ExperimentGenerationFailure failure, DurableExperimentEvidenceFailure expected)
    {
        var experiment = ExperimentFor([1m]);
        var generation = new GenerationStub(ExperimentGenerationResult.Failed(failure));
        var store = new StoreStub(DurableExperimentAcceptanceResult.Accepted(DurableExperimentAcceptanceDisposition.NewlyAccepted));

        var result = new DurableExperimentUseCase(generation, store).Execute(Request(experiment));

        Assert.False(result.IsSuccess);
        Assert.Equal(expected, result.Failure);
        Assert.Equal(1, generation.Calls);
        Assert.Equal(0, store.AcceptCalls);
    }

    [Theory]
    [InlineData(DurableExperimentEvidenceFailure.DependencyUnavailable)]
    [InlineData(DurableExperimentEvidenceFailure.InvalidEvidence)]
    [InlineData(DurableExperimentEvidenceFailure.IntegrityConflict)]
    public void StoreFailurePropagatesWithoutFabricatedEvidence(DurableExperimentEvidenceFailure failure)
    {
        var experiment = ExperimentFor([1m]);
        var store = new StoreStub(DurableExperimentAcceptanceResult.Failed(failure));
        var result = new DurableExperimentUseCase(new GenerationStub(ExperimentGenerationResult.Generated(experiment)), store).Execute(Request(experiment));

        Assert.False(result.IsSuccess);
        Assert.Equal(failure, result.Failure);
        Assert.Null(result.Evidence);
        Assert.Equal(1, store.AcceptCalls);
    }

    [Fact]
    public void UnknownGenerationOrStoreDefectPropagates()
    {
        var experiment = ExperimentFor([1m]);
        Assert.Throws<UnknownProbeException>(() => new DurableExperimentUseCase(new ThrowingGeneration(), new StoreStub(DurableExperimentAcceptanceResult.Accepted(DurableExperimentAcceptanceDisposition.NewlyAccepted))).Execute(Request(experiment)));
        Assert.Throws<UnknownProbeException>(() => new DurableExperimentUseCase(new GenerationStub(ExperimentGenerationResult.Generated(experiment)), new ThrowingStore()).Execute(Request(experiment)));
    }

    private static ExperimentGenerationRequest Request(ExperimentResult experiment) => new(experiment.Definition, experiment.FeatureSet.SnapshotIdentity, experiment.FeatureSet.SnapshotVersion);
    private static ExperimentResult ExperimentFor(decimal[] values)
    {
        var from = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var snapshot = new MaterializeDatasetUseCase(new HistoryStub()).Execute(new DatasetDefinition("DURABLE", from, from.AddDays(1))).Snapshot!;
        var definitionIdentity = FeatureIdentityComputer.ComputeDefinitionIdentity();
        var featureValues = values.Select((value, index) => new FeatureValue(from.AddHours(index + 1), value)).ToArray();
        var featureSet = new FeatureSet(definitionIdentity, FeatureIdentityComputer.ComputeSetIdentity(definitionIdentity, snapshot.SnapshotIdentity, snapshot.Version, featureValues), new FeatureProvenance(definitionIdentity, snapshot.SnapshotIdentity, snapshot.Version, snapshot.Provenance), new FeatureLineage(definitionIdentity, snapshot.Lineage), featureValues);
        return new ExperimentGenerationUseCase(new GenerationFeatureStub(featureSet), new ExperimentGenerationValidator(), new SimpleReturnDescriptiveSummaryComputer()).Execute(new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, snapshot.SnapshotIdentity, snapshot.Version)).Experiment!;
    }

    private sealed class HistoryStub : AIQuantTradingResearch.Application.Persistence.IHistoricalObservationStore
    {
        public AIQuantTradingResearch.Application.Persistence.ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> values) => throw new NotSupportedException();
        public AIQuantTradingResearch.Application.Persistence.HistoricalObservationResult Retrieve(string target) => AIQuantTradingResearch.Application.Persistence.HistoricalObservationResult.Retrieved([]);
    }
    private sealed class GenerationFeatureStub(FeatureSet set) : IFeatureGenerationUseCase { public FeatureGenerationResult Execute(FeatureGenerationRequest request) => FeatureGenerationResult.Generated(set); }
    private sealed class GenerationStub(ExperimentGenerationResult result) : IExperimentGenerationUseCase { public int Calls { get; private set; } public ExperimentGenerationResult Execute(ExperimentGenerationRequest request) { Calls++; return result; } }
    private sealed class ThrowingGeneration : IExperimentGenerationUseCase { public ExperimentGenerationResult Execute(ExperimentGenerationRequest request) => throw new UnknownProbeException(); }
    private class StoreStub(DurableExperimentAcceptanceResult acceptance) : IDurableExperimentEvidenceStore { public int AcceptCalls { get; private set; } public DurableExperimentAcceptanceRequest? LastAcceptance { get; private set; } public virtual DurableExperimentAcceptanceResult Accept(DurableExperimentAcceptanceRequest request) { AcceptCalls++; LastAcceptance = request; return acceptance; } public DurableExperimentRetrievalResult Retrieve(DurableExperimentRetrievalRequest request) => throw new NotSupportedException(); }
    private sealed class ThrowingStore : StoreStub { public ThrowingStore() : base(DurableExperimentAcceptanceResult.Accepted(DurableExperimentAcceptanceDisposition.NewlyAccepted)) { } public override DurableExperimentAcceptanceResult Accept(DurableExperimentAcceptanceRequest request) => throw new UnknownProbeException(); }
    private sealed class UnknownProbeException : Exception;
}
