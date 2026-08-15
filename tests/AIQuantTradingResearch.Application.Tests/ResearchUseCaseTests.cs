using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class ResearchUseCaseTests
{
    private static readonly DateTimeOffset FirstInstant =
        new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public void ExecuteWhenRequestIsValidInvokesSourceOnceWithRequestValues()
    {
        var source = new StubObservationSource(AvailableObservations(100.00m, 110.00m, 120.00m));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("SAMPLE-USD", 3));

        Assert.True(outcome.IsSuccess);
        Assert.Equal(1, source.CallCount);
        Assert.NotNull(source.LastRequest);
        Assert.Equal("SAMPLE-USD", source.LastRequest.Target);
        Assert.Equal(3, source.LastRequest.RequestedObservationCount);
    }

    [Fact]
    public void ExecuteWhenTargetIsBlankReturnsInvalidRequestWithoutInvokingSource()
    {
        AssertInvalidRequest(new ResearchRequest(string.Empty, 3));
    }

    [Fact]
    public void ExecuteWhenTargetIsWhitespaceReturnsInvalidRequestWithoutInvokingSource()
    {
        AssertInvalidRequest(new ResearchRequest("   ", 3));
    }

    [Fact]
    public void ExecuteWhenRequestedCountIsZeroReturnsInvalidRequestWithoutInvokingSource()
    {
        AssertInvalidRequest(new ResearchRequest("SAMPLE-USD", 0));
    }

    [Fact]
    public void ExecuteWhenRequestedCountIsNegativeReturnsInvalidRequestWithoutInvokingSource()
    {
        AssertInvalidRequest(new ResearchRequest("SAMPLE-USD", -1));
    }

    [Fact]
    public void ExecuteWhenSourceReportsUnsupportedTargetReturnsUnsupportedTarget()
    {
        var source = new StubObservationSource(
            ObservationSourceResult.Failed(ObservationSourceFailure.UnsupportedTarget));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("UNKNOWN", 3));

        Assert.False(outcome.IsSuccess);
        Assert.Null(outcome.Result);
        Assert.Equal(ResearchFailure.UnsupportedTarget, outcome.Failure);
        Assert.Equal(1, source.CallCount);
    }

    [Fact]
    public void ExecuteWhenSourceReportsInsufficientObservationsReturnsInsufficientObservations()
    {
        var source = new StubObservationSource(
            ObservationSourceResult.Failed(ObservationSourceFailure.InsufficientObservations));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("SAMPLE-USD", 3));

        Assert.False(outcome.IsSuccess);
        Assert.Null(outcome.Result);
        Assert.Equal(ResearchFailure.InsufficientObservations, outcome.Failure);
        Assert.Equal(1, source.CallCount);
    }

    [Theory]
    [InlineData(ObservationSourceFailure.SourceUnavailable, ResearchFailure.SourceUnavailable)]
    [InlineData(ObservationSourceFailure.AccessDenied, ResearchFailure.AccessDenied)]
    [InlineData(ObservationSourceFailure.UsageLimitReached, ResearchFailure.UsageLimitReached)]
    [InlineData(ObservationSourceFailure.InvalidSourceResponse, ResearchFailure.InvalidSourceResponse)]
    public void ExecuteWhenSourceReportsExtendedFailureReturnsCorrespondingResearchFailure(
        ObservationSourceFailure sourceFailure,
        ResearchFailure expectedFailure)
    {
        var source = new StubObservationSource(ObservationSourceResult.Failed(sourceFailure));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("SAMPLE-USD", 3));

        Assert.False(outcome.IsSuccess);
        Assert.Null(outcome.Result);
        Assert.Equal(expectedFailure, outcome.Failure);
        Assert.Equal(1, source.CallCount);
    }

    [Fact]
    public void ExecuteWhenSourceReturnsTooFewObservationsReturnsInsufficientObservations()
    {
        var source = new StubObservationSource(AvailableObservations(100.00m, 110.00m));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("SAMPLE-USD", 3));

        Assert.False(outcome.IsSuccess);
        Assert.Null(outcome.Result);
        Assert.Equal(ResearchFailure.InsufficientObservations, outcome.Failure);
    }

    [Fact]
    public void ExecuteWhenSourceReturnsTooManyObservationsReturnsInsufficientObservations()
    {
        var source = new StubObservationSource(
            AvailableObservations(90.00m, 100.00m, 110.00m, 120.00m));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("SAMPLE-USD", 3));

        Assert.False(outcome.IsSuccess);
        Assert.Null(outcome.Result);
        Assert.Equal(ResearchFailure.InsufficientObservations, outcome.Failure);
    }

    [Fact]
    public void ExecuteWhenCanonicalObservationsAreAvailableReturnsCanonicalResult()
    {
        var source = new StubObservationSource(AvailableObservations(100.00m, 110.00m, 120.00m));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("SAMPLE-USD", 3));

        Assert.True(outcome.IsSuccess);
        Assert.Null(outcome.Failure);
        Assert.NotNull(outcome.Result);
        Assert.Equal("SAMPLE-USD", outcome.Result.Target);
        Assert.Equal(3, outcome.Result.ObservationCount);
        Assert.Equal(110.00m, outcome.Result.MeanPrice.Value);
    }

    [Fact]
    public void ExecuteWhenAlternateObservationsAreAvailableReturnsDomainCalculatedMean()
    {
        var source = new StubObservationSource(AvailableObservations(10.00m, 20.00m, 90.00m));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(new ResearchRequest("ALTERNATE", 3));

        Assert.True(outcome.IsSuccess);
        Assert.NotNull(outcome.Result);
        Assert.Equal(40.00m, outcome.Result.MeanPrice.Value);
    }

    [Fact]
    public void ExecuteWhenSourceReturnsOutOfOrderObservationsPropagatesDomainFailure()
    {
        var observations = new[]
        {
            new PriceObservation(FirstInstant.AddDays(1), 110.00m),
            new PriceObservation(FirstInstant, 100.00m),
        };
        var source = new StubObservationSource(
            ObservationSourceResult.ObservationsAvailable(observations));
        var useCase = new ResearchUseCase(source);

        Assert.Throws<ArgumentException>(
            () => useCase.Execute(new ResearchRequest("SAMPLE-USD", 2)));
    }

    private static ObservationSourceResult AvailableObservations(params decimal[] prices) =>
        ObservationSourceResult.ObservationsAvailable(
            prices.Select(
                (price, index) => new PriceObservation(FirstInstant.AddDays(index), price)));

    private static void AssertInvalidRequest(ResearchRequest request)
    {
        var source = new StubObservationSource(
            ObservationSourceResult.Failed(ObservationSourceFailure.UnsupportedTarget));
        var useCase = new ResearchUseCase(source);

        var outcome = useCase.Execute(request);

        Assert.False(outcome.IsSuccess);
        Assert.Null(outcome.Result);
        Assert.Equal(ResearchFailure.InvalidRequest, outcome.Failure);
        Assert.Equal(0, source.CallCount);
        Assert.Null(source.LastRequest);
    }

    private sealed class StubObservationSource(ObservationSourceResult configuredResult)
        : IObservationSource
    {
        public int CallCount { get; private set; }

        public ResearchRequest? LastRequest { get; private set; }

        public ObservationSourceResult GetObservations(ResearchRequest request)
        {
            CallCount++;
            LastRequest = request;
            return configuredResult;
        }
    }
}
