using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class FeatureApplicationTests
{
    [Fact]
    public void SimpleReturnUsesExactDecimalOrderingAndCurrentOffset()
    {
        var first = new PriceObservation(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 10m);
        var second = new PriceObservation(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.FromHours(5)), 12.5m);
        var result = Compute([first, second]);
        Assert.Equal(0.25m, result.Values.Single().Value);
        Assert.Equal(second.Instant, result.Values.Single().Instant);
        Assert.Equal(FeatureIdentityScheme.Name, result.DefinitionIdentity.Scheme);
    }

    [Fact]
    public void EmptyAndSingleSnapshotsSucceedWithZeroValuesAndStableIdentity()
    {
        var empty = Compute([]);
        var single = Compute([new PriceObservation(DateTimeOffset.Parse("2024-01-01T00:00:00+02:00", CultureInfo.InvariantCulture), 3m)]);
        Assert.True(empty.Count == 0 && single.Count == 0);
        Assert.Equal(empty.Identity, Compute([]).Identity);
        Assert.Equal(FeatureIdentityScheme.Name, single.Identity.Scheme);
    }

    [Fact]
    public void EquivalentDecimalScaleAndCulturePreserveIdentity()
    {
        var previous = CultureInfo.CurrentCulture;
        try
        {
            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
            var a = Compute([new PriceObservation(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 10.0m), new PriceObservation(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero), 12.50m)]);
            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            var b = Compute([new PriceObservation(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 10m), new PriceObservation(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero), 12.5m)]);
            Assert.Equal(a.Identity, b.Identity);
            Assert.Equal(a.Values.Single().Value, b.Values.Single().Value);
        }
        finally { CultureInfo.CurrentCulture = previous; }
    }

    [Fact]
    public void FeatureSetPreservesProvenanceAndValuesAreImmutable()
    {
        var result = Compute([new PriceObservation(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 2m), new PriceObservation(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero), 4m)]);
        Assert.Equal(result.SnapshotIdentity, result.Provenance.SnapshotIdentity);
        Assert.Equal(result.SnapshotVersion, result.Provenance.SnapshotVersion);
        Assert.Throws<NotSupportedException>(() => ((IList<FeatureValue>)result.Values).Add(new FeatureValue(DateTimeOffset.UnixEpoch, 1m)));
    }

    [Fact]
    public void UseCaseForwardsExactLookupAndComputesOnce()
    {
        var snapshot = Snapshot([new PriceObservation(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 2m), new PriceObservation(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero), 4m)]);
        var store = new StoreStub(DatasetSnapshotRetrievalResult.Found(snapshot));
        var computer = new CountingComputer(Compute(snapshot.Observations));
        var request = new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, snapshot.SnapshotIdentity, snapshot.Version);
        var result = new FeatureGenerationUseCase(store, new FeatureGenerationValidator(), computer).Execute(request);
        Assert.True(result.IsSuccess);
        Assert.Equal(request.SnapshotIdentity, store.LastIdentity);
        Assert.Equal(1, computer.Calls);
    }

    [Theory]
    [InlineData(DatasetStoreFailure.Unavailable, FeatureGenerationFailure.DependencyUnavailable)]
    [InlineData(DatasetStoreFailure.InvalidData, FeatureGenerationFailure.IntegrityConflict)]
    public void StoreFailuresPreventComputation(DatasetStoreFailure failure, FeatureGenerationFailure expected)
    {
        var snapshot = Snapshot([]);
        var computer = new CountingComputer(Compute([]));
        var request = new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, snapshot.SnapshotIdentity, snapshot.Version);
        var result = new FeatureGenerationUseCase(new StoreStub(DatasetSnapshotRetrievalResult.Failed(failure)), new FeatureGenerationValidator(), computer).Execute(request);
        Assert.Equal(expected, result.Failure);
        Assert.Equal(0, computer.Calls);
        Assert.Null(result.FeatureSet);
    }

    [Fact]
    public void NotFoundAndUnknownStoreFailureAreHandledOrPropagated()
    {
        var snapshot = Snapshot([]);
        var request = new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, snapshot.SnapshotIdentity, snapshot.Version);
        var notFound = new FeatureGenerationUseCase(new StoreStub(DatasetSnapshotRetrievalResult.NotFound()), new FeatureGenerationValidator(), new CountingComputer(Compute([]))).Execute(request);
        Assert.Equal(FeatureGenerationFailure.SnapshotNotFound, notFound.Failure);
        Assert.Throws<InvalidOperationException>(() => new FeatureGenerationUseCase(new ThrowingStore(), new FeatureGenerationValidator(), new CountingComputer(Compute([]))).Execute(request));
    }

    [Fact]
    public void InvalidNumericEvidenceFailsBeforeComputerAndNoPartialSetExists()
    {
        var snapshot = Snapshot([new PriceObservation(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 0.0000000000000000000000000001m), new PriceObservation(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero), decimal.MaxValue)]);
        var computer = new CountingComputer(Compute([]));
        var request = new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, snapshot.SnapshotIdentity, snapshot.Version);
        var result = new FeatureGenerationUseCase(new StoreStub(DatasetSnapshotRetrievalResult.Found(snapshot)), new FeatureGenerationValidator(), computer).Execute(request);
        Assert.Equal(FeatureGenerationFailure.InvalidNumericInput, result.Failure);
        Assert.Null(result.FeatureSet);
        Assert.Equal(0, computer.Calls);
    }

    private static FeatureSet Compute(IReadOnlyList<PriceObservation> observations) => new SimpleReturnFeatureComputer().Compute(new FeatureGenerationRequest(FeatureDefinition.SimpleReturnLag1V1, Snapshot(observations).SnapshotIdentity, Snapshot(observations).Version), Snapshot(observations));
    private static DatasetSnapshotCandidate Snapshot(IReadOnlyList<PriceObservation> observations)
    {
        var from = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
        var to = from.AddYears(1);
        return new MaterializeDatasetUseCase(new HistoryStub(observations)).Execute(new DatasetDefinition("SAMPLE", from, to)).Snapshot!;
    }

    private sealed class HistoryStub(IReadOnlyList<PriceObservation> observations) : IHistoricalObservationStore
    {
        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> observations) => throw new NotSupportedException();
        public HistoricalObservationResult Retrieve(string target) => HistoricalObservationResult.Retrieved(observations);
    }
    private sealed class StoreStub(DatasetSnapshotRetrievalResult result) : IDatasetSnapshotStore
    {
        public DatasetSnapshotIdentity? LastIdentity { get; private set; }
        public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot) => throw new NotSupportedException();
        public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity) { LastIdentity = snapshotIdentity; return result; }
    }
    private sealed class ThrowingStore : IDatasetSnapshotStore
    {
        public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot) => throw new NotSupportedException();
        public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity) => throw new InvalidOperationException("unknown");
    }
    private sealed class CountingComputer(FeatureSet result) : IFeatureComputer
    {
        public int Calls { get; private set; }
        public FeatureSet Compute(FeatureGenerationRequest request, DatasetSnapshotCandidate snapshot) { Calls++; return result; }
    }
}
