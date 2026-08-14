using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Domain.Tests;

public sealed class ObservationSeriesTests
{
    private static readonly DateTimeOffset FirstInstant =
        new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public void ConstructorWhenSeriesHasOneObservationPreservesObservation()
    {
        var observation = CreateObservation(0, 100.00m);

        var series = new ObservationSeries([observation]);

        Assert.Single(series.Observations);
        Assert.Equal(observation, series.Observations[0]);
    }

    [Fact]
    public void ConstructorWhenSeriesIsEmptyThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new ObservationSeries([]));
    }

    [Fact]
    public void ConstructorWhenTimestampsAreDuplicatedThrowsArgumentException()
    {
        var observations = new[]
        {
            CreateObservation(0, 100.00m),
            CreateObservation(0, 110.00m),
        };

        Assert.Throws<ArgumentException>(() => new ObservationSeries(observations));
    }

    [Fact]
    public void ConstructorWhenTimestampsAreOutOfOrderThrowsArgumentException()
    {
        var observations = new[]
        {
            CreateObservation(1, 110.00m),
            CreateObservation(0, 100.00m),
        };

        Assert.Throws<ArgumentException>(() => new ObservationSeries(observations));
    }

    [Fact]
    public void ConstructorWhenTimestampsAreOrderedPreservesOrdering()
    {
        var observations = new[]
        {
            CreateObservation(0, 100.00m),
            CreateObservation(1, 110.00m),
            CreateObservation(2, 120.00m),
        };

        var series = new ObservationSeries(observations);

        Assert.Equal(observations, series.Observations);
    }

    [Fact]
    public void CalculateMeanPriceWhenSeriesHasCanonicalObservationsReturnsArithmeticMean()
    {
        var series = new ObservationSeries(
        [
            CreateObservation(0, 100.00m),
            CreateObservation(1, 110.00m),
            CreateObservation(2, 120.00m),
        ]);

        MeanPrice meanPrice = series.CalculateMeanPrice();

        Assert.Equal(110.00m, meanPrice.Value);
    }

    [Fact]
    public void CalculateMeanPriceWhenEveryObservationAffectsResultUsesCompleteSeries()
    {
        var series = new ObservationSeries(
        [
            CreateObservation(0, 10.00m),
            CreateObservation(1, 20.00m),
            CreateObservation(2, 90.00m),
        ]);

        var meanPrice = series.CalculateMeanPrice();

        Assert.Equal(40.00m, meanPrice.Value);
    }

    [Fact]
    public void CalculateMeanPriceWhenSeriesHasOneObservationReturnsObservationPrice()
    {
        var series = new ObservationSeries([CreateObservation(0, 125.50m)]);

        var meanPrice = series.CalculateMeanPrice();

        Assert.Equal(125.50m, meanPrice.Value);
    }

    private static PriceObservation CreateObservation(int dayOffset, decimal price) =>
        new(FirstInstant.AddDays(dayOffset), price);
}
