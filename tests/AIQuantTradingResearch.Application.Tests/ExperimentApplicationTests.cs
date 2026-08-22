using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class ExperimentApplicationTests
{
    [Fact]
    public void BuiltInDefinitionIdentityIsDeterministicAndCanonical()
    {
        var definition = ExperimentDefinition.SimpleReturnDescriptiveSummaryV1;
        var first = ExperimentIdentityComputer.ComputeDefinitionIdentity(definition);
        var second = ExperimentIdentityComputer.ComputeDefinitionIdentity(definition);

        Assert.Equal("simple-return-descriptive-summary-v1", definition.Name);
        Assert.Equal(ExperimentIdentityScheme.Name, first.Scheme);
        Assert.Equal(first, second);
        Assert.Matches("^[0-9a-f]{64}$", first.Fingerprint);
    }

    [Fact]
    public void SummaryComputerCoversEmptySingleAndNonEmptyExactDecimalEvidence()
    {
        var computer = new SimpleReturnDescriptiveSummaryComputer();
        Assert.Equal(new ExperimentSummaryEvidence(0, null, null, null), computer.Compute(FeatureSetFor([])));

        var single = computer.Compute(FeatureSetFor([1.25m]));
        Assert.Equal(1, single.Count);
        Assert.Equal(1.25m, single.ArithmeticMean);
        Assert.Equal(1.25m, single.Minimum);
        Assert.Equal(1.25m, single.Maximum);

        var many = computer.Compute(FeatureSetFor([-1.5m, 0.25m, 2.75m]));
        Assert.Equal(3, many.Count);
        Assert.Equal(0.5m, many.ArithmeticMean);
        Assert.Equal(-1.5m, many.Minimum);
        Assert.Equal(2.75m, many.Maximum);
    }

    [Fact]
    public void UseCaseForwardsExactRequestAndInvokesEachDependencyOnce()
    {
        var featureSet = FeatureSetFor([0.25m, -0.2m]);
        var request = new ExperimentGenerationRequest(
            ExperimentDefinition.SimpleReturnDescriptiveSummaryV1,
            featureSet.SnapshotIdentity,
            featureSet.SnapshotVersion);
        var featureUseCase = new CountingFeatureUseCase(FeatureGenerationResult.Generated(featureSet));
        var summary = new CountingSummaryComputer(new ExperimentSummaryEvidence(2, 0.025m, -0.2m, 0.25m));

        var result = UseCase(featureUseCase, summary).Execute(request);

        Assert.True(result.IsSuccess);
        Assert.Equal(1, featureUseCase.Calls);
        Assert.Equal(request.SnapshotIdentity, featureUseCase.LastRequest!.SnapshotIdentity);
        Assert.Equal(request.SnapshotVersion, featureUseCase.LastRequest.SnapshotVersion);
        Assert.Equal(FeatureDefinition.SimpleReturnLag1V1, featureUseCase.LastRequest.Definition);
        Assert.Equal(1, summary.Calls);
        Assert.Equal(featureSet.Identity, result.Experiment!.Provenance.FeatureSetIdentity);
        Assert.Equal(featureSet.Provenance, result.Experiment.Provenance.FeatureProvenance);
        Assert.Equal(featureSet.Lineage, result.Experiment.Lineage.FeatureLineage);
    }

    [Fact]
    public void EmptyAndSingleObservationDerivedFeatureSetsSucceedWithDistinctResultIdentities()
    {
        var empty = FeatureSetFromObservations("EMPTY", []);
        var single = FeatureSetFromObservations("SINGLE", [new PriceObservation(Instant(1), 10m)]);
        var emptyResult = ExecuteWith(empty);
        var singleResult = ExecuteWith(single);

        Assert.True(emptyResult.IsSuccess && singleResult.IsSuccess);
        Assert.Equal(0, emptyResult.Experiment!.Summary.Count);
        Assert.False(emptyResult.Experiment.Summary.HasAggregates);
        Assert.Equal(0, singleResult.Experiment!.Summary.Count);
        Assert.False(singleResult.Experiment.Summary.HasAggregates);
        Assert.NotEqual(empty.Identity, single.Identity);
        Assert.NotEqual(emptyResult.Experiment.Identity, singleResult.Experiment.Identity);
    }

    [Fact]
    public void EquivalentEvidenceProducesEquivalentResultIdentityAndDifferentFeatureIdentityDoesNotCollapseIt()
    {
        var first = FeatureSetFromObservations("A", [new PriceObservation(Instant(1), 10m), new PriceObservation(Instant(2), 12m)]);
        var equivalent = FeatureSetFromObservations("A", [new PriceObservation(Instant(1), 10m), new PriceObservation(Instant(2), 12m)]);
        var distinct = FeatureSetFromObservations("B", [new PriceObservation(Instant(1), 10m), new PriceObservation(Instant(2), 12m)]);

        var firstResult = ExecuteWith(first).Experiment!;
        var equivalentResult = ExecuteWith(equivalent).Experiment!;
        var distinctResult = ExecuteWith(distinct).Experiment!;

        Assert.Equal(first.Identity, equivalent.Identity);
        Assert.Equal(firstResult.Identity, equivalentResult.Identity);
        Assert.Equal(firstResult.Summary, equivalentResult.Summary);
        Assert.NotEqual(first.Identity, distinct.Identity);
        Assert.Equal(firstResult.Summary, distinctResult.Summary);
        Assert.NotEqual(firstResult.Identity, distinctResult.Identity);
    }

    [Fact]
    public void FeatureSetValuesAndExperimentEvidenceAreImmutableSnapshots()
    {
        var values = new List<FeatureValue> { new(Instant(1), 2m), new(Instant(2), 4m) };
        var featureSet = FeatureSetForValues(values);
        var result = ExecuteWith(featureSet).Experiment!;
        values.Clear();

        Assert.Equal(2, featureSet.Count);
        Assert.Equal(2, result.Summary.Count);
        Assert.Equal(featureSet.Identity, result.Provenance.FeatureSetIdentity);
        Assert.Throws<NotSupportedException>(() => ((IList<FeatureValue>)featureSet.Values).Add(new FeatureValue(Instant(3), 1m)));
    }

    [Fact]
    public void InvalidRequestPrecedesUpstreamAndSummary()
    {
        var featureUseCase = new CountingFeatureUseCase(FeatureGenerationResult.Failed(FeatureGenerationFailure.DependencyUnavailable));
        var summary = new CountingSummaryComputer(new ExperimentSummaryEvidence(0, null, null, null));

        var result = UseCase(featureUseCase, summary).Execute(null!);

        Assert.Equal(ExperimentGenerationFailure.InvalidRequest, result.Failure);
        Assert.Null(result.Experiment);
        Assert.Equal(0, featureUseCase.Calls);
        Assert.Equal(0, summary.Calls);
    }

    [Fact]
    public void InvalidFeatureEvidencePrecedesSummaryAndFabricatedIdentity()
    {
        var valid = FeatureSetFor([1m]);
        var wrongDefinition = new FeatureDefinitionIdentity(Fingerprint('9'));
        var invalid = new FeatureSet(
            wrongDefinition,
            valid.Identity,
            new FeatureProvenance(wrongDefinition, valid.SnapshotIdentity, valid.SnapshotVersion, valid.Provenance.DatasetProvenance),
            new FeatureLineage(wrongDefinition, valid.Lineage.DatasetLineage),
            valid.Values);
        var featureUseCase = new CountingFeatureUseCase(FeatureGenerationResult.Generated(invalid));
        var summary = new CountingSummaryComputer(new ExperimentSummaryEvidence(1, 1m, 1m, 1m));

        var result = Execute(new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, invalid.SnapshotIdentity, invalid.SnapshotVersion), featureUseCase, summary);

        Assert.Equal(ExperimentGenerationFailure.InvalidFeatureEvidence, result.Failure);
        Assert.Null(result.Experiment);
        Assert.Equal(0, summary.Calls);
    }

    [Theory]
    [InlineData(FeatureGenerationFailure.SnapshotNotFound, ExperimentGenerationFailure.FeatureSetNotFound)]
    [InlineData(FeatureGenerationFailure.DependencyUnavailable, ExperimentGenerationFailure.DependencyUnavailable)]
    [InlineData(FeatureGenerationFailure.InvalidSnapshotEvidence, ExperimentGenerationFailure.InvalidFeatureEvidence)]
    [InlineData(FeatureGenerationFailure.InvalidNumericInput, ExperimentGenerationFailure.InvalidNumericEvidence)]
    [InlineData(FeatureGenerationFailure.IntegrityConflict, ExperimentGenerationFailure.IntegrityConflict)]
    public void BoundedUpstreamFailuresMapWithoutSummaryOrPartialResult(FeatureGenerationFailure failure, ExperimentGenerationFailure expected)
    {
        var featureSet = FeatureSetFor([]);
        var featureUseCase = new CountingFeatureUseCase(FeatureGenerationResult.Failed(failure));
        var summary = new CountingSummaryComputer(new ExperimentSummaryEvidence(0, null, null, null));

        var result = Execute(new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, featureSet.SnapshotIdentity, featureSet.SnapshotVersion), featureUseCase, summary);

        Assert.Equal(expected, result.Failure);
        Assert.Null(result.Experiment);
        Assert.Equal(1, featureUseCase.Calls);
        Assert.Equal(0, summary.Calls);
    }

    [Fact]
    public void DecimalOverflowFailsBeforeResultIdentityAndDirectComputerThrows()
    {
        var overflow = FeatureSetForValues([new FeatureValue(Instant(1), decimal.MaxValue), new FeatureValue(Instant(2), decimal.MaxValue)]);
        Assert.Throws<OverflowException>(() => new SimpleReturnDescriptiveSummaryComputer().Compute(overflow));

        var result = ExecuteWith(overflow);
        Assert.Equal(ExperimentGenerationFailure.InvalidNumericEvidence, result.Failure);
        Assert.Null(result.Experiment);
    }

    [Fact]
    public void CultureDoesNotChangeSummaryOrIdentity()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            var invariant = ExecuteWith(FeatureSetFromValues("CULTURE", [1.20m, -0.20m])).Experiment!;
            CultureInfo.CurrentCulture = new CultureInfo("pt-BR");
            var localized = ExecuteWith(FeatureSetFromValues("CULTURE", [1.2m, -0.2m])).Experiment!;
            Assert.Equal(invariant.Summary, localized.Summary);
            Assert.Equal(invariant.Identity, localized.Identity);
        }
        finally
        {
            CultureInfo.CurrentCulture = previous;
        }
    }

    [Fact]
    public void UnknownUpstreamAndSummaryExceptionsPropagate()
    {
        var featureSet = FeatureSetFor([1m]);
        var request = new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, featureSet.SnapshotIdentity, featureSet.SnapshotVersion);
        Assert.Throws<UnknownProbeException>(() => UseCase(new ThrowingFeatureUseCase(), new CountingSummaryComputer(new ExperimentSummaryEvidence(1, 1m, 1m, 1m))).Execute(request));
        Assert.Throws<UnknownProbeException>(() => UseCase(new CountingFeatureUseCase(FeatureGenerationResult.Generated(featureSet)), new ThrowingSummaryComputer()).Execute(request));
    }

    private static ExperimentGenerationUseCase UseCase(IFeatureGenerationUseCase featureUseCase, IExperimentSummaryComputer summary) => new(featureUseCase, new ExperimentGenerationValidator(), summary);
    private static ExperimentGenerationResult ExecuteWith(FeatureSet featureSet) => Execute(new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, featureSet.SnapshotIdentity, featureSet.SnapshotVersion), new CountingFeatureUseCase(FeatureGenerationResult.Generated(featureSet)), new SimpleReturnDescriptiveSummaryComputer());
    private static ExperimentGenerationResult Execute(ExperimentGenerationRequest request, IFeatureGenerationUseCase featureUseCase, IExperimentSummaryComputer summary) => UseCase(featureUseCase, summary).Execute(request);

    private static FeatureSet FeatureSetFor(decimal[] values) => FeatureSetFromValues("VALUES", values.Select((value, index) => new FeatureValue(Instant(index + 1), value)).ToArray());
    private static FeatureSet FeatureSetFromValues(string target, IReadOnlyList<FeatureValue> values) => FeatureSetForSnapshot(new MaterializeDatasetUseCase(new HistoryStub([])).Execute(new DatasetDefinition(target, Instant(0), Instant(10))).Snapshot!, values);
    private static FeatureSet FeatureSetFromValues(string target, IReadOnlyList<decimal> values) => FeatureSetFromValues(target, values.Select((value, index) => new FeatureValue(Instant(index + 1), value)).ToArray());
    private static FeatureSet FeatureSetFromObservations(string target, IReadOnlyList<PriceObservation> observations)
    {
        var from = Instant(0);
        var snapshot = new MaterializeDatasetUseCase(new HistoryStub(observations)).Execute(new DatasetDefinition(target, from, Instant(10))).Snapshot!;
        return new SimpleReturnFeatureComputer().Compute(new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, snapshot.SnapshotIdentity, snapshot.Version), snapshot);
    }
    private static FeatureSet FeatureSetFor(IReadOnlyList<decimal> values) => FeatureSetForValues(values.Select((value, index) => new FeatureValue(Instant(index + 1), value)).ToArray());
    private static FeatureSet FeatureSetForValues(IReadOnlyList<FeatureValue> values) => FeatureSetForSnapshot(new MaterializeDatasetUseCase(new HistoryStub([])).Execute(new DatasetDefinition("FEATURE", Instant(0), Instant(10))).Snapshot!, values);
    private static FeatureSet FeatureSetForSnapshot(DatasetSnapshotCandidate snapshot, IReadOnlyList<FeatureValue> values)
    {
        var definitionIdentity = FeatureIdentityComputer.ComputeDefinitionIdentity();
        var identity = FeatureIdentityComputer.ComputeSetIdentity(definitionIdentity, snapshot.SnapshotIdentity, snapshot.Version, values);
        return new FeatureSet(definitionIdentity, identity, new FeatureProvenance(definitionIdentity, snapshot.SnapshotIdentity, snapshot.Version, snapshot.Provenance), new FeatureLineage(definitionIdentity, snapshot.Lineage), values);
    }
    private static DateTimeOffset Instant(int day) => new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero).AddDays(day);
    private static string Fingerprint(char character) => new(character, 64);

    private sealed class HistoryStub(IReadOnlyList<PriceObservation> observations) : IHistoricalObservationStore
    {
        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> values) => throw new NotSupportedException();
        public HistoricalObservationResult Retrieve(string target) => HistoricalObservationResult.Retrieved(observations);
    }
    private sealed class CountingFeatureUseCase(FeatureGenerationResult result) : IFeatureGenerationUseCase
    {
        public int Calls { get; private set; }
        public FeatureGenerationRequest? LastRequest { get; private set; }
        public FeatureGenerationResult Execute(FeatureGenerationRequest request) { Calls++; LastRequest = request; return result; }
    }
    private sealed class CountingSummaryComputer(ExperimentSummaryEvidence result) : IExperimentSummaryComputer
    {
        public int Calls { get; private set; }
        public ExperimentSummaryEvidence Compute(FeatureSet featureSet) { Calls++; return result; }
    }
    private sealed class ThrowingFeatureUseCase : IFeatureGenerationUseCase
    {
        public FeatureGenerationResult Execute(FeatureGenerationRequest request) => throw new UnknownProbeException();
    }
    private sealed class ThrowingSummaryComputer : IExperimentSummaryComputer
    {
        public ExperimentSummaryEvidence Compute(FeatureSet featureSet) => throw new UnknownProbeException();
    }
    private sealed class UnknownProbeException : Exception;
}
