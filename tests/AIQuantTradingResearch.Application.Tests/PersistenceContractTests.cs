using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class PersistenceContractTests
{
    [Theory]
    [InlineData(ObservationPersistenceOutcome.NewlyAccepted)]
    [InlineData(ObservationPersistenceOutcome.Idempotent)]
    [InlineData(ObservationPersistenceOutcome.Conflict)]
    public void CompletedWhenOutcomeIsDefinedRepresentsOutcomeWithoutFailure(
        ObservationPersistenceOutcome outcome)
    {
        var result = ObservationPersistenceResult.Completed(outcome);

        Assert.True(result.HasOutcome);
        Assert.Equal(outcome, result.Outcome);
        Assert.Null(result.Failure);
    }

    [Theory]
    [InlineData(PersistenceFailure.Unavailable)]
    [InlineData(PersistenceFailure.InvalidData)]
    public void FailedWhenFailureIsDefinedRepresentsFailureWithoutOutcome(
        PersistenceFailure failure)
    {
        var result = ObservationPersistenceResult.Failed(failure);

        Assert.False(result.HasOutcome);
        Assert.Null(result.Outcome);
        Assert.Equal(failure, result.Failure);
    }

    [Fact]
    public void RetrievedWhenObservationsAreEmptyRepresentsSuccessfulEmptyHistory()
    {
        var result = HistoricalObservationResult.Retrieved([]);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Observations);
        Assert.Empty(result.Observations);
        Assert.Null(result.Failure);
    }

    [Theory]
    [InlineData(PersistenceFailure.Unavailable)]
    [InlineData(PersistenceFailure.InvalidData)]
    public void FailedWhenFailureIsDefinedRepresentsFailureWithoutObservations(
        PersistenceFailure failure)
    {
        var result = HistoricalObservationResult.Failed(failure);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Observations);
        Assert.Equal(failure, result.Failure);
    }

    [Fact]
    public void RetrievedWhenObservationsContainNullThrowsArgumentException()
    {
        IReadOnlyList<PriceObservation> observations = [null!];

        Assert.Throws<ArgumentException>(() => HistoricalObservationResult.Retrieved(observations));
    }

    [Fact]
    public void RetrievedWhenObservationsAreAscendingPreservesExactValues()
    {
        var first = new PriceObservation(
            new DateTimeOffset(2024, 3, 10, 0, 0, 0, TimeSpan.FromHours(-4)),
            0.0000000000000000000000000001m);
        var second = new PriceObservation(
            new DateTimeOffset(2024, 3, 11, 0, 0, 0, TimeSpan.FromHours(-4)),
            123.4567890123456789012345678m);

        var result = HistoricalObservationResult.Retrieved([first, second]);

        Assert.True(result.IsSuccess);
        Assert.Equal([first, second], result.Observations);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void RetrievedWhenInstantsAreNotStrictlyAscendingThrowsArgumentException(bool descending)
    {
        var first = new PriceObservation(
            new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.FromHours(-4)),
            100m);
        var second = descending
            ? new PriceObservation(
                new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.FromHours(-4)),
                110m)
            : new PriceObservation(
                new DateTimeOffset(2024, 1, 2, 1, 0, 0, TimeSpan.FromHours(-3)),
                110m);

        Assert.Throws<ArgumentException>(() => HistoricalObservationResult.Retrieved([first, second]));
    }
}
