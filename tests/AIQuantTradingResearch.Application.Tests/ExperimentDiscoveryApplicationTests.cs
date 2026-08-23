using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class ExperimentDiscoveryApplicationTests
{
    private static readonly DatasetSnapshotIdentity SnapshotIdentity =
        new(new string('a', 64));

    private static readonly ExperimentDefinitionIdentity DefinitionIdentity =
        new(new string('b', 64));

    [Fact]
    public void ValidRequestForwardsExactDimensionsOnceAndPreservesNonEmptyEvidence()
    {
        DurableExperimentEvidence evidence = EvidenceFor([0.25m, -0.2m]);
        var expected = DurableExperimentDiscoveryResult.Discovered([evidence]);
        var store = new DiscoveryStore(expected);
        var request = new DurableExperimentDiscoveryRequest(
            SnapshotIdentity,
            DefinitionIdentity,
            7);

        DurableExperimentDiscoveryResult actual = new DurableExperimentDiscoveryUseCase(store)
            .Execute(request);

        Assert.Same(expected, actual);
        Assert.Equal(1, store.Calls);
        Assert.Same(request, store.LastRequest);
        Assert.Equal(SnapshotIdentity, store.LastRequest!.SnapshotIdentity);
        Assert.Equal(DefinitionIdentity, store.LastRequest.DefinitionIdentity);
        Assert.Equal(7, store.LastRequest.MaximumResultCount);
        DurableExperimentEvidence returned = Assert.Single(actual.Evidence!);
        Assert.Equal(evidence.Identity, returned.Identity);
        Assert.Equal(evidence.Provenance.SnapshotIdentity, returned.Provenance.SnapshotIdentity);
        Assert.Equal(evidence.Provenance.SnapshotVersion, returned.Provenance.SnapshotVersion);
        Assert.Equal(evidence.DefinitionIdentity, returned.DefinitionIdentity);
        Assert.Equal(evidence.Provenance.FeatureSetIdentity, returned.Provenance.FeatureSetIdentity);
        Assert.Equal(evidence.Provenance, returned.Provenance);
        Assert.Equal(evidence.Lineage, returned.Lineage);
        Assert.Equal(2, returned.Summary.Count);
        Assert.Equal(0.025m, returned.Summary.ArithmeticMean);
        Assert.Equal(-0.2m, returned.Summary.Minimum);
        Assert.Equal(0.25m, returned.Summary.Maximum);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void NonPositiveMaximumIsInvalidRequestWithoutStoreInvocation(int maximum)
    {
        var store = new DiscoveryStore(DurableExperimentDiscoveryResult.Discovered([]));
        var request = new DurableExperimentDiscoveryRequest(
            SnapshotIdentity,
            DefinitionIdentity,
            maximum);

        DurableExperimentDiscoveryResult result = new DurableExperimentDiscoveryUseCase(store)
            .Execute(request);

        Assert.False(result.IsSuccess);
        Assert.Equal(DurableExperimentEvidenceFailure.InvalidRequest, result.Failure);
        Assert.Equal(0, store.Calls);
    }

    [Fact]
    public void NullRequestIsInvalidRequestWithoutStoreInvocation()
    {
        var store = new DiscoveryStore(DurableExperimentDiscoveryResult.Discovered([]));

        DurableExperimentDiscoveryResult result = new DurableExperimentDiscoveryUseCase(store)
            .Execute(null!);

        Assert.False(result.IsSuccess);
        Assert.Equal(DurableExperimentEvidenceFailure.InvalidRequest, result.Failure);
        Assert.Equal(0, store.Calls);
    }

    [Fact]
    public void EmptyStoreSuccessRemainsSuccessfulEmptyDiscoveryNotNotFound()
    {
        var store = new DiscoveryStore(DurableExperimentDiscoveryResult.Discovered([]));

        DurableExperimentDiscoveryResult result = new DurableExperimentDiscoveryUseCase(store)
            .Execute(new DurableExperimentDiscoveryRequest(
                SnapshotIdentity,
                DefinitionIdentity,
                1));

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Evidence!);
        Assert.Null(result.Failure);
        Assert.Equal(1, store.Calls);
    }

    [Theory]
    [InlineData(DurableExperimentEvidenceFailure.DependencyUnavailable)]
    [InlineData(DurableExperimentEvidenceFailure.InvalidEvidence)]
    public void ClassifiedStoreFailurePassesThroughOnceWithoutFallback(
        DurableExperimentEvidenceFailure failure)
    {
        var store = new DiscoveryStore(DurableExperimentDiscoveryResult.Failed(failure));

        DurableExperimentDiscoveryResult result = new DurableExperimentDiscoveryUseCase(store)
            .Execute(new DurableExperimentDiscoveryRequest(
                SnapshotIdentity,
                DefinitionIdentity,
                1));

        Assert.False(result.IsSuccess);
        Assert.Equal(failure, result.Failure);
        Assert.Null(result.Evidence);
        Assert.Equal(1, store.Calls);
    }

    [Fact]
    public void UnknownStoreDefectPropagatesWithoutSyntheticFailureOrRetry()
    {
        var store = new ThrowingDiscoveryStore();

        Assert.Throws<UnknownProbeException>(() =>
            new DurableExperimentDiscoveryUseCase(store).Execute(
                new DurableExperimentDiscoveryRequest(
                    SnapshotIdentity,
                    DefinitionIdentity,
                    1)));
        Assert.Equal(1, store.Calls);
    }

    private static DurableExperimentEvidence EvidenceFor(decimal[] values)
    {
        var from = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var snapshot = new MaterializeDatasetUseCase(new EmptyHistoryStore()).Execute(
            new DatasetDefinition("DISCOVERY", from, from.AddDays(1))).Snapshot!;
        var featureDefinitionIdentity = FeatureIdentityComputer.ComputeDefinitionIdentity();
        var featureValues = values
            .Select((value, index) => new FeatureValue(from.AddHours(index + 1), value))
            .ToArray();
        var featureSet = new FeatureSet(
            featureDefinitionIdentity,
            FeatureIdentityComputer.ComputeSetIdentity(
                featureDefinitionIdentity,
                snapshot.SnapshotIdentity,
                snapshot.Version,
                featureValues),
            new FeatureProvenance(
                featureDefinitionIdentity,
                snapshot.SnapshotIdentity,
                snapshot.Version,
                snapshot.Provenance),
            new FeatureLineage(featureDefinitionIdentity, snapshot.Lineage),
            featureValues);
        ExperimentResult experiment = new ExperimentGenerationUseCase(
                new FixedFeatureUseCase(featureSet),
                new ExperimentGenerationValidator(),
                new SimpleReturnDescriptiveSummaryComputer())
            .Execute(new ExperimentGenerationRequest(
                ExperimentDefinition.SimpleReturnDescriptiveSummaryV1,
                snapshot.SnapshotIdentity,
                snapshot.Version))
            .Experiment!;
        FeatureProvenance featureProvenance = experiment.Provenance.FeatureProvenance;
        DatasetProvenance datasetProvenance = featureProvenance.DatasetProvenance;
        FeatureLineage featureLineage = experiment.Lineage.FeatureLineage;
        DatasetLineage datasetLineage = featureLineage.DatasetLineage;
        return new DurableExperimentEvidence(
            experiment.Definition,
            experiment.DefinitionIdentity,
            experiment.Identity,
            experiment.Summary,
            new DurableExperimentProvenance(
                experiment.Provenance.DefinitionIdentity,
                experiment.Provenance.FeatureSetIdentity,
                featureProvenance.DefinitionIdentity,
                featureProvenance.SnapshotIdentity,
                featureProvenance.SnapshotVersion,
                datasetProvenance.DefinitionIdentity,
                datasetProvenance.ResearchDatasetIdentity,
                datasetProvenance.SourceStateIdentity,
                datasetProvenance.SourceAuthority,
                datasetProvenance.ObservationCount),
            new DurableExperimentLineage(
                experiment.Lineage.DefinitionIdentity,
                featureLineage.DefinitionIdentity,
                datasetLineage.DefinitionIdentity,
                datasetLineage.SourceStateIdentity));
    }

    private sealed class DiscoveryStore(DurableExperimentDiscoveryResult result)
        : IDurableExperimentEvidenceDiscoveryStore
    {
        public int Calls { get; private set; }

        public DurableExperimentDiscoveryRequest? LastRequest { get; private set; }

        public DurableExperimentDiscoveryResult Discover(DurableExperimentDiscoveryRequest request)
        {
            Calls++;
            LastRequest = request;
            return result;
        }
    }

    private sealed class ThrowingDiscoveryStore : IDurableExperimentEvidenceDiscoveryStore
    {
        public int Calls { get; private set; }

        public DurableExperimentDiscoveryResult Discover(DurableExperimentDiscoveryRequest request)
        {
            Calls++;
            throw new UnknownProbeException();
        }
    }

    private sealed class FixedFeatureUseCase(FeatureSet featureSet) : IFeatureGenerationUseCase
    {
        public FeatureGenerationResult Execute(FeatureGenerationRequest request) =>
            FeatureGenerationResult.Generated(featureSet);
    }

    private sealed class EmptyHistoryStore : AIQuantTradingResearch.Application.Persistence.IHistoricalObservationStore
    {
        public AIQuantTradingResearch.Application.Persistence.ObservationPersistenceResult Persist(
            string target,
            IReadOnlyList<PriceObservation> values) => throw new NotSupportedException();

        public AIQuantTradingResearch.Application.Persistence.HistoricalObservationResult Retrieve(
            string target) => AIQuantTradingResearch.Application.Persistence.HistoricalObservationResult.Retrieved([]);
    }

    private sealed class UnknownProbeException : Exception;
}
