using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class DatasetApplicationTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-3));
    private static readonly DateTimeOffset To = From.AddDays(3);

    [Fact]
    public void DatasetDefinitionPreservesExactTargetAndRequiresValidHalfOpenInterval()
    {
        var definition = new DatasetDefinition("aapl ", From, To);

        Assert.Equal("aapl ", definition.Target);
        Assert.Equal(From, definition.From);
        Assert.Equal(To, definition.To);
        Assert.Equal(DatasetOrdering.SemanticInstantAscending, definition.Ordering);
        Assert.Throws<ArgumentException>(() => new DatasetDefinition("AAPL", From, From));
        Assert.Throws<ArgumentException>(() => new DatasetDefinition("AAPL", To, From));
        Assert.Throws<ArgumentException>(() => new DatasetDefinition(" ", From, To));
    }

    [Fact]
    public void TypedIdentitiesRequireCanonicalFingerprintsAndVersionPreservesSnapshotIdentity()
    {
        const string fingerprint = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";
        var definition = new DatasetDefinitionIdentity(fingerprint);
        var research = new ResearchDatasetIdentity(fingerprint);
        var source = new SourceStateIdentity(fingerprint);
        var snapshot = new DatasetSnapshotIdentity(fingerprint);
        var version = new DatasetVersion(snapshot);

        Assert.Equal(DatasetIdentityScheme.Name, definition.Scheme);
        Assert.Equal(fingerprint, research.Fingerprint);
        Assert.Equal(fingerprint, source.Fingerprint);
        Assert.Same(snapshot, version.SnapshotIdentity);
        Assert.Throws<ArgumentException>(() => new DatasetSnapshotIdentity(fingerprint.ToUpperInvariant()));
        Assert.Throws<ArgumentException>(() => new DatasetSnapshotIdentity("abc"));
    }

    [Fact]
    public void MaterializationSelectsHalfOpenWindowPreservesFidelityAndBuildsCatalogEvidence()
    {
        var before = new PriceObservation(From.AddTicks(-1), 1.0000000000000000000000000001m);
        var included = new PriceObservation(From, 2.1234567890123456789012345678m);
        var middle = new PriceObservation(From.AddDays(1), 3.5m);
        var excluded = new PriceObservation(To, 4m);
        var store = new StubHistoryStore(HistoricalObservationResult.Retrieved([before, included, middle, excluded]));
        var result = new MaterializeDatasetUseCase(store).Execute(new DatasetDefinition("AAPL", From, To));

        var snapshot = Assert.IsType<DatasetSnapshotCandidate>(result.Snapshot);
        Assert.True(result.IsSuccess);
        Assert.Equal("AAPL", store.LastTarget);
        Assert.Equal([included, middle], snapshot.Observations);
        Assert.Equal(included.Instant, snapshot.Coverage.FirstObservationInstant);
        Assert.Equal(middle.Instant, snapshot.Coverage.LastObservationInstant);
        Assert.Equal(included.Price, snapshot.Observations[0].Price);
        Assert.Equal(TimeSpan.FromHours(-3), snapshot.Observations[0].Instant.Offset);
        Assert.Equal(snapshot.DefinitionIdentity, snapshot.Provenance.DefinitionIdentity);
        Assert.Equal(snapshot.SourceStateIdentity, snapshot.Lineage.SourceStateIdentity);
        var catalog = new DatasetCatalogEntry(snapshot);
        Assert.Equal(snapshot.SnapshotIdentity, catalog.SnapshotIdentity);
        Assert.Equal(snapshot.Version, catalog.Version);
        Assert.Equal(2, catalog.ObservationCount);
        Assert.False(catalog.IsEmpty);
    }

    [Fact]
    public void MaterializationIsDeterministicAcrossCultureAndEmptySelection()
    {
        var definition = new DatasetDefinition("AAPL", From, To);
        var observations = new[] { new PriceObservation(From.AddDays(1), 12.3400m) };
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;

        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");
            var first = Materialize(definition, observations);
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("tr-TR");
            var second = Materialize(definition, observations);
            var empty = Materialize(definition, []);

            Assert.Equal(first.DefinitionIdentity, second.DefinitionIdentity);
            Assert.Equal(first.ResearchDatasetIdentity, second.ResearchDatasetIdentity);
            Assert.Equal(first.SourceStateIdentity, second.SourceStateIdentity);
            Assert.Equal(first.SnapshotIdentity, second.SnapshotIdentity);
            Assert.Equal(first.Version, second.Version);
            Assert.Empty(empty.Observations);
            Assert.True(new DatasetCatalogEntry(empty).IsEmpty);
            Assert.Null(empty.Coverage.FirstObservationInstant);
            Assert.Null(empty.Coverage.LastObservationInstant);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }

    [Fact]
    public void MaterializationIdentityChangesForRelevantInputButNotOutsideWindow()
    {
        var definition = new DatasetDefinition("AAPL", From, To);
        var baseline = Materialize(definition, [new PriceObservation(From.AddDays(1), 10m)]);
        var changed = Materialize(definition, [new PriceObservation(From.AddDays(1), 11m)]);
        var outside = Materialize(definition, [new PriceObservation(From.AddDays(1), 10m), new PriceObservation(To.AddDays(1), 99m)]);
        var changedDefinition = Materialize(new DatasetDefinition("AAPL", From, To.AddDays(1)), [new PriceObservation(From.AddDays(1), 10m)]);

        Assert.NotEqual(baseline.SourceStateIdentity, changed.SourceStateIdentity);
        Assert.NotEqual(baseline.SnapshotIdentity, changed.SnapshotIdentity);
        Assert.Equal(baseline.SourceStateIdentity, outside.SourceStateIdentity);
        Assert.Equal(baseline.SnapshotIdentity, outside.SnapshotIdentity);
        Assert.NotEqual(baseline.DefinitionIdentity, changedDefinition.DefinitionIdentity);
        Assert.NotEqual(baseline.ResearchDatasetIdentity, changedDefinition.ResearchDatasetIdentity);
        Assert.NotEqual(baseline.SnapshotIdentity, changedDefinition.SnapshotIdentity);
    }

    [Theory]
    [InlineData(PersistenceFailure.Unavailable, DatasetMaterializationFailure.SourceHistoryUnavailable)]
    [InlineData(PersistenceFailure.InvalidData, DatasetMaterializationFailure.IntegrityConflict)]
    public void MaterializationMapsHistoricalFailures(PersistenceFailure sourceFailure, DatasetMaterializationFailure expectedFailure)
    {
        var result = new MaterializeDatasetUseCase(new StubHistoryStore(HistoricalObservationResult.Failed(sourceFailure)))
            .Execute(new DatasetDefinition("AAPL", From, To));

        Assert.False(result.IsSuccess);
        Assert.Equal(expectedFailure, result.Failure);
    }

    [Fact]
    public void HistoricalResultRejectsDuplicateSemanticInstants()
    {
        var instant = From.AddDays(1);
        Assert.Throws<ArgumentException>(() => HistoricalObservationResult.Retrieved([
            new PriceObservation(instant, 1m),
            new PriceObservation(instant, 2m),
        ]));
    }

    [Theory]
    [InlineData(DatasetSnapshotStoreOutcome.NewlyAccepted, DatasetCatalogRegistrationOutcome.NewlyRegistered, DatasetMaterializationIntegrationOutcome.NewlyAccepted)]
    [InlineData(DatasetSnapshotStoreOutcome.NewlyAccepted, DatasetCatalogRegistrationOutcome.EquivalentExisting, DatasetMaterializationIntegrationOutcome.NewlyAccepted)]
    [InlineData(DatasetSnapshotStoreOutcome.EquivalentExisting, DatasetCatalogRegistrationOutcome.NewlyRegistered, DatasetMaterializationIntegrationOutcome.EquivalentExisting)]
    [InlineData(DatasetSnapshotStoreOutcome.EquivalentExisting, DatasetCatalogRegistrationOutcome.EquivalentExisting, DatasetMaterializationIntegrationOutcome.EquivalentExisting)]
    public void IntegrationComposesAcceptedOutcomes(
        DatasetSnapshotStoreOutcome snapshotOutcome,
        DatasetCatalogRegistrationOutcome catalogOutcome,
        DatasetMaterializationIntegrationOutcome expectedOutcome)
    {
        var candidate = Materialize(new DatasetDefinition("AAPL", From, To), []);
        var materializer = new StubMaterializer(DatasetMaterializationResult.Materialized(candidate));
        var store = new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(snapshotOutcome));
        var catalog = new StubCatalog(DatasetCatalogRegistrationResult.Completed(catalogOutcome));

        var result = new DatasetMaterializationIntegrationUseCase(materializer, store, catalog).Execute(candidate.Definition);

        Assert.True(result.IsSuccess);
        Assert.Equal(expectedOutcome, result.Outcome);
        Assert.Equal(1, store.StoreCalls);
        Assert.Equal(1, catalog.RegisterCalls);
        Assert.Equal(candidate.SnapshotIdentity, catalog.LastEntry!.SnapshotIdentity);
        Assert.Equal(candidate.Lineage, catalog.LastEntry.Lineage);
    }

    [Theory]
    [InlineData(DatasetStoreFailure.Unavailable, DatasetMaterializationFailure.SnapshotStoreUnavailable)]
    [InlineData(DatasetStoreFailure.InvalidData, DatasetMaterializationFailure.IntegrityConflict)]
    public void IntegrationMapsStoreFailuresWithoutCallingCatalog(DatasetStoreFailure storeFailure, DatasetMaterializationFailure expectedFailure)
    {
        var candidate = Materialize(new DatasetDefinition("AAPL", From, To), []);
        var store = new StubSnapshotStore(DatasetSnapshotStoreResult.Failed(storeFailure));
        var catalog = new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered));

        var result = new DatasetMaterializationIntegrationUseCase(
            new StubMaterializer(DatasetMaterializationResult.Materialized(candidate)), store, catalog).Execute(candidate.Definition);

        Assert.False(result.IsSuccess);
        Assert.Equal(expectedFailure, result.Failure);
        Assert.Equal(0, catalog.RegisterCalls);
    }

    [Theory]
    [InlineData(DatasetCatalogRegistrationOutcome.IntegrityConflict, null)]
    [InlineData(null, DatasetStoreFailure.Unavailable)]
    [InlineData(null, DatasetStoreFailure.InvalidData)]
    public void IntegrationMapsCatalogConflictAndFailures(
        DatasetCatalogRegistrationOutcome? catalogOutcome,
        DatasetStoreFailure? catalogFailure)
    {
        var candidate = Materialize(new DatasetDefinition("AAPL", From, To), []);
        var registration = catalogOutcome is not null
            ? DatasetCatalogRegistrationResult.Completed(catalogOutcome.Value)
            : DatasetCatalogRegistrationResult.Failed(catalogFailure!.Value);

        var result = new DatasetMaterializationIntegrationUseCase(
            new StubMaterializer(DatasetMaterializationResult.Materialized(candidate)),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)),
            new StubCatalog(registration)).Execute(candidate.Definition);

        Assert.False(result.IsSuccess);
        Assert.Equal(
            catalogOutcome == DatasetCatalogRegistrationOutcome.IntegrityConflict || catalogFailure == DatasetStoreFailure.InvalidData
                ? DatasetMaterializationFailure.IntegrityConflict
                : DatasetMaterializationFailure.SnapshotStoreUnavailable,
            result.Failure);
    }

    [Fact]
    public void IntegrationStopsOnMaterializationOrIntegrityConflict()
    {
        var definition = new DatasetDefinition("AAPL", From, To);
        var candidate = Materialize(definition, []);
        var failingStore = new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.IntegrityConflict));
        var catalog = new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered));
        var conflict = new DatasetMaterializationIntegrationUseCase(
            new StubMaterializer(DatasetMaterializationResult.Materialized(candidate)), failingStore, catalog).Execute(definition);
        var materializationFailure = new DatasetMaterializationIntegrationUseCase(
            new StubMaterializer(DatasetMaterializationResult.Failed(DatasetMaterializationFailure.SourceHistoryUnavailable)),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)), catalog).Execute(definition);

        Assert.Equal(DatasetMaterializationFailure.IntegrityConflict, conflict.Failure);
        Assert.Equal(0, catalog.RegisterCalls);
        Assert.Equal(DatasetMaterializationFailure.SourceHistoryUnavailable, materializationFailure.Failure);
    }

    private static DatasetSnapshotCandidate Materialize(DatasetDefinition definition, IReadOnlyList<PriceObservation> observations) =>
        new MaterializeDatasetUseCase(new StubHistoryStore(HistoricalObservationResult.Retrieved(observations)))
            .Execute(definition).Snapshot!;

    private sealed class StubHistoryStore(HistoricalObservationResult retrieval) : IHistoricalObservationStore
    {
        public string? LastTarget { get; private set; }

        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> observations) =>
            throw new NotSupportedException();

        public HistoricalObservationResult Retrieve(string target)
        {
            LastTarget = target;
            return retrieval;
        }
    }

    private sealed class StubMaterializer(DatasetMaterializationResult result) : IMaterializeDatasetUseCase
    {
        public DatasetMaterializationResult Execute(DatasetDefinition definition) => result;
    }

    private sealed class StubSnapshotStore(DatasetSnapshotStoreResult storeResult) : IDatasetSnapshotStore
    {
        public int StoreCalls { get; private set; }

        public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot)
        {
            StoreCalls++;
            return storeResult;
        }

        public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity) =>
            throw new NotSupportedException();
    }

    private sealed class StubCatalog(DatasetCatalogRegistrationResult registrationResult) : IDatasetCatalog
    {
        public int RegisterCalls { get; private set; }

        public DatasetCatalogEntry? LastEntry { get; private set; }

        public DatasetCatalogRegistrationResult Register(DatasetCatalogEntry entry)
        {
            RegisterCalls++;
            LastEntry = entry;
            return registrationResult;
        }

        public DatasetCatalogLookupResult Find(DatasetSnapshotIdentity snapshotIdentity) =>
            throw new NotSupportedException();
    }
}
