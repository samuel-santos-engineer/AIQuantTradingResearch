using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure.Research;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class SimulatedLiveObservationSourceTests
{
    private static ReplayRequest Request(int start, int count) =>
        new("simulated-live-replay-v1", "SIMULATED-USD", start, count);

    [Fact]
    public void ReplayIsOrderedAndFinite() 
    {
        var result = new SimulatedLiveObservationSource().Replay(Request(0, 3));
        Assert.True(result.IsSuccess);
        Assert.Equal(0, result.FirstTick);
        Assert.Equal(3, result.NextTick);
        Assert.True(result.IsEndOfReplay);
        Assert.Equal([100.00m, 101.25m, 100.75m], result.Observations!.Select(x => x.Price));
    }

    [Fact]
    public void ReplaySupportsResumeAndRestart()
    {
        var source = new SimulatedLiveObservationSource();
        var resumed = source.Replay(Request(1, 1));
        var restarted = source.Replay(Request(0, 1));
        Assert.Equal(101.25m, resumed.Observations![0].Price);
        Assert.Equal(100.00m, restarted.Observations![0].Price);
    }

    [Fact]
    public void ReplayIsDeterministicForDuplicateRequests()
    {
        var source = new SimulatedLiveObservationSource();
        var first = source.Replay(Request(0, 2));
        var second = source.Replay(Request(0, 2));
        Assert.Equal(first.FirstTick, second.FirstTick);
        Assert.Equal(first.NextTick, second.NextTick);
        Assert.Equal(first.IsEndOfReplay, second.IsEndOfReplay);
        Assert.Equal(first.Observations, second.Observations);
    }

    [Fact]
    public void ReplayHonorsCancellation()
    {
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();
        Assert.Throws<OperationCanceledException>(() =>
            new SimulatedLiveObservationSource().Replay(Request(0, 1), cancellation.Token));
    }

    [Fact]
    public void ReplayRejectsInvalidBoundsAndIdentity()
    {
        var source = new SimulatedLiveObservationSource();
        Assert.Equal(ObservationSourceFailure.InsufficientObservations, source.Replay(Request(-1, 1)).Failure);
        Assert.Equal(ObservationSourceFailure.UnsupportedTarget,
            source.Replay(new ReplayRequest("other", "SIMULATED-USD", 0, 1)).Failure);
    }

    [Fact]
    public void ReplayAtFixtureEndReturnsSuccessfulTerminalState()
    {
        var result = new SimulatedLiveObservationSource().Replay(Request(3, 1));
        Assert.True(result.IsSuccess);
        Assert.True(result.IsEndOfReplay);
        Assert.Empty(result.Observations!);
        Assert.Equal(3, result.NextTick);
    }
}
