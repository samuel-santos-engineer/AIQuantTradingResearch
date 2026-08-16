using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class PersistHistoricalObservationsUseCaseTests
{
    private static readonly DateTimeOffset FirstInstant =
        new(2024, 5, 1, 0, 0, 0, TimeSpan.FromHours(-4));

    [Fact]
    public void ExecuteWhenRequestIsNullThrowsArgumentNullException()
    {
        var useCase = new PersistHistoricalObservationsUseCase(
            new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(
                ObservationPersistenceOutcome.NewlyAccepted)));

        Assert.Throws<ArgumentNullException>(() => useCase.Execute(null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ExecuteWhenTargetIsBlankReturnsInvalidRequestWithoutInvokingStore(string target)
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(
            ObservationPersistenceOutcome.NewlyAccepted));
        var useCase = new PersistHistoricalObservationsUseCase(store);

        var result = useCase.Execute(new PersistHistoricalObservationsRequest(target, Observations()));

        AssertInvalidRequest(result, store);
    }

    [Fact]
    public void ExecuteWhenObservationsAreNullReturnsInvalidRequestWithoutInvokingStore()
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(
            ObservationPersistenceOutcome.NewlyAccepted));
        var useCase = new PersistHistoricalObservationsUseCase(store);

        var result = useCase.Execute(new PersistHistoricalObservationsRequest("TARGET", null!));

        AssertInvalidRequest(result, store);
    }

    [Fact]
    public void ExecuteWhenObservationsAreEmptyReturnsInvalidRequestWithoutInvokingStore()
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(
            ObservationPersistenceOutcome.NewlyAccepted));
        var useCase = new PersistHistoricalObservationsUseCase(store);

        var result = useCase.Execute(new PersistHistoricalObservationsRequest("TARGET", []));

        AssertInvalidRequest(result, store);
    }

    [Fact]
    public void ExecuteWhenObservationsContainNullReturnsInvalidRequestWithoutInvokingStore()
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(
            ObservationPersistenceOutcome.NewlyAccepted));
        var useCase = new PersistHistoricalObservationsUseCase(store);
        IReadOnlyList<PriceObservation> observations = [null!];

        var result = useCase.Execute(new PersistHistoricalObservationsRequest("TARGET", observations));

        AssertInvalidRequest(result, store);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ExecuteWhenObservationInstantsAreNotStrictlyAscendingReturnsInvalidRequestWithoutInvokingStore(
        bool descending)
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(
            ObservationPersistenceOutcome.NewlyAccepted));
        var useCase = new PersistHistoricalObservationsUseCase(store);
        var observations = descending
            ? new[] { Observation(1, 100m), Observation(0, 110m) }
            : new[]
            {
                Observation(0, 100m),
                new PriceObservation(FirstInstant.ToOffset(TimeSpan.FromHours(-3)), 110m),
            };

        var result = useCase.Execute(new PersistHistoricalObservationsRequest("TARGET", observations));

        AssertInvalidRequest(result, store);
    }

    [Theory]
    [InlineData(ObservationPersistenceOutcome.NewlyAccepted)]
    [InlineData(ObservationPersistenceOutcome.Idempotent)]
    [InlineData(ObservationPersistenceOutcome.Conflict)]
    public void ExecuteWhenStoreReturnsOutcomeForwardsExactRequestAndPreservesOutcome(
        ObservationPersistenceOutcome outcome)
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Completed(outcome));
        var useCase = new PersistHistoricalObservationsUseCase(store);
        var observations = Observations();

        var result = useCase.Execute(new PersistHistoricalObservationsRequest(" AAPL/Exact ", observations));

        Assert.True(result.IsValidRequest);
        Assert.NotNull(result.PersistenceResult);
        Assert.Equal(outcome, result.PersistenceResult.Outcome);
        Assert.Null(result.PersistenceResult.Failure);
        Assert.Equal(1, store.CallCount);
        Assert.Equal(" AAPL/Exact ", store.Target);
        Assert.Same(observations, store.Observations);
        Assert.Equal(FirstInstant, store.Observations![0].Instant);
        Assert.Equal(0.0000000000000000000000000001m, store.Observations[0].Price);
    }

    [Theory]
    [InlineData(PersistenceFailure.Unavailable)]
    [InlineData(PersistenceFailure.InvalidData)]
    public void ExecuteWhenStoreReturnsFailurePreservesDistinctFailure(PersistenceFailure failure)
    {
        var store = new StubHistoricalObservationStore(ObservationPersistenceResult.Failed(failure));
        var useCase = new PersistHistoricalObservationsUseCase(store);

        var result = useCase.Execute(new PersistHistoricalObservationsRequest("TARGET", Observations()));

        Assert.True(result.IsValidRequest);
        Assert.NotNull(result.PersistenceResult);
        Assert.False(result.PersistenceResult.HasOutcome);
        Assert.Equal(failure, result.PersistenceResult.Failure);
        Assert.Equal(1, store.CallCount);
    }

    [Fact]
    public void ConstructorDependsOnlyOnHistoricalObservationStore()
    {
        var constructor = Assert.Single(typeof(PersistHistoricalObservationsUseCase).GetConstructors());

        var parameter = Assert.Single(constructor.GetParameters());
        Assert.Equal(typeof(IHistoricalObservationStore), parameter.ParameterType);
    }

    private static IReadOnlyList<PriceObservation> Observations() =>
    [
        new PriceObservation(FirstInstant, 0.0000000000000000000000000001m),
        new PriceObservation(FirstInstant.AddDays(1), 123.4567890123456789012345678m),
    ];

    private static PriceObservation Observation(int dayOffset, decimal price) =>
        new(FirstInstant.AddDays(dayOffset), price);

    private static void AssertInvalidRequest(
        PersistHistoricalObservationsResult result,
        StubHistoricalObservationStore store)
    {
        Assert.False(result.IsValidRequest);
        Assert.Null(result.PersistenceResult);
        Assert.Equal(PersistHistoricalObservationsFailure.InvalidRequest, result.Failure);
        Assert.Equal(0, store.CallCount);
    }

    private sealed class StubHistoricalObservationStore(ObservationPersistenceResult result)
        : IHistoricalObservationStore
    {
        public int CallCount { get; private set; }

        public string? Target { get; private set; }

        public IReadOnlyList<PriceObservation>? Observations { get; private set; }

        public ObservationPersistenceResult Persist(
            string target,
            IReadOnlyList<PriceObservation> observations)
        {
            CallCount++;
            Target = target;
            Observations = observations;
            return result;
        }

        public HistoricalObservationResult Retrieve(string target) =>
            HistoricalObservationResult.Retrieved([]);
    }
}
