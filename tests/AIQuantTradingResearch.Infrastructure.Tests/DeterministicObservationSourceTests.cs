using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure.Research;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class DeterministicObservationSourceTests
{
    private static readonly PriceObservation[] CanonicalObservations =
    [
        new(new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero), 100.00m),
        new(new DateTimeOffset(2024, 1, 2, 0, 0, 0, TimeSpan.Zero), 110.00m),
        new(new DateTimeOffset(2024, 1, 3, 0, 0, 0, TimeSpan.Zero), 120.00m),
    ];

    [Fact]
    public void GetObservationsWhenCountIsOneReturnsFirstCanonicalObservationOnly()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("SAMPLE-USD", 1));

        AssertSuccessfulObservations(result, CanonicalObservations[..1]);
    }

    [Fact]
    public void GetObservationsWhenCountIsTwoReturnsFirstTwoCanonicalObservationsOnly()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("SAMPLE-USD", 2));

        AssertSuccessfulObservations(result, CanonicalObservations[..2]);
    }

    [Fact]
    public void GetObservationsWhenCountIsThreeReturnsExactCanonicalFixture()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("SAMPLE-USD", 3));

        AssertSuccessfulObservations(result, CanonicalObservations);
    }

    [Fact]
    public void GetObservationsWhenCountExceedsCapacityReturnsInsufficientObservations()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("SAMPLE-USD", 4));

        AssertFailure(result, ObservationSourceFailure.InsufficientObservations);
    }

    [Fact]
    public void GetObservationsWhenTargetIsUnknownReturnsUnsupportedTarget()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("UNKNOWN", 1));

        AssertFailure(result, ObservationSourceFailure.UnsupportedTarget);
    }

    [Fact]
    public void GetObservationsWhenTargetCaseDoesNotMatchReturnsUnsupportedTarget()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("sample-usd", 1));

        AssertFailure(result, ObservationSourceFailure.UnsupportedTarget);
    }

    [Fact]
    public void GetObservationsWhenCountIsZeroReturnsInsufficientObservations()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("SAMPLE-USD", 0));

        AssertFailure(result, ObservationSourceFailure.InsufficientObservations);
    }

    [Fact]
    public void GetObservationsWhenCountIsNegativeReturnsInsufficientObservations()
    {
        var source = new DeterministicObservationSource();

        var result = source.GetObservations(new ResearchRequest("SAMPLE-USD", -1));

        AssertFailure(result, ObservationSourceFailure.InsufficientObservations);
    }

    [Fact]
    public void GetObservationsWhenRequestIsRepeatedReturnsEquivalentResults()
    {
        var source = new DeterministicObservationSource();
        var request = new ResearchRequest("SAMPLE-USD", 3);

        var firstResult = source.GetObservations(request);
        var secondResult = source.GetObservations(request);

        Assert.Equal(firstResult.IsSuccess, secondResult.IsSuccess);
        Assert.Equal(firstResult.Failure, secondResult.Failure);
        Assert.NotNull(firstResult.Observations);
        Assert.NotNull(secondResult.Observations);
        Assert.Equal(firstResult.Observations, secondResult.Observations);
    }

    private static void AssertSuccessfulObservations(
        ObservationSourceResult result,
        IReadOnlyList<PriceObservation> expected)
    {
        Assert.True(result.IsSuccess);
        Assert.Null(result.Failure);
        Assert.NotNull(result.Observations);
        Assert.Equal(expected, result.Observations);
    }

    private static void AssertFailure(
        ObservationSourceResult result,
        ObservationSourceFailure expectedFailure)
    {
        Assert.False(result.IsSuccess);
        Assert.Null(result.Observations);
        Assert.Equal(expectedFailure, result.Failure);
    }
}
