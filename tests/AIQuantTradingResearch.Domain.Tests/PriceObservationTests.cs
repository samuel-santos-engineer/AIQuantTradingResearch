using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Domain.Tests;

public sealed class PriceObservationTests
{
    [Fact]
    public void ConstructorWhenPriceIsPositiveExposesObservationValues()
    {
        var instant = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

        var observation = new PriceObservation(instant, 100.00m);

        Assert.Equal(instant, observation.Instant);
        Assert.Equal(100.00m, observation.Price);
    }

    [Fact]
    public void ConstructorWhenPriceIsZeroThrowsArgumentOutOfRangeException()
    {
        var instant = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Assert.Throws<ArgumentOutOfRangeException>(() => new PriceObservation(instant, 0m));
    }

    [Fact]
    public void ConstructorWhenPriceIsNegativeThrowsArgumentOutOfRangeException()
    {
        var instant = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

        Assert.Throws<ArgumentOutOfRangeException>(() => new PriceObservation(instant, -1.25m));
    }
}
