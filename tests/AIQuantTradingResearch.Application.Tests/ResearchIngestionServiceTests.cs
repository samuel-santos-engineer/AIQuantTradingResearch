using AIQuantTradingResearch.Application.MarketData;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class ResearchIngestionServiceTests
{
    private static readonly DateTimeOffset Now = new(2026, 10, 1, 12, 0, 0, TimeSpan.Zero);
    [Fact] public async Task FairAlternationAndQuotaBoundFailuresWithoutImmediateRetries()
    {
        var source = new Source();var memory = new Memory();var service = new ResearchIngestionService(source, memory, memory, 2);
        await service.PollNextAsync(Now, TimeSpan.Zero);await service.PollNextAsync(Now, TimeSpan.Zero);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => service.PollNextAsync(Now, TimeSpan.Zero));
        Assert.Equal(new[] { ResearchInstrument.BtcUsd, ResearchInstrument.EthUsd }, source.Calls);
        await service.PollNextAsync(Now.AddMinutes(1), TimeSpan.Zero);Assert.Equal(ResearchInstrument.BtcUsd, source.Calls[^1]);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => service.PollNextAsync(Now, TimeSpan.Zero));
    }
    [Fact] public async Task ProviderFailureSpendsCreditAndNextInstrumentKeepsItsTurn()
    {
        var source = new Source { Fail = true };var memory = new Memory();var service = new ResearchIngestionService(source, memory, memory, 2);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => service.PollNextAsync(Now, TimeSpan.Zero));source.Fail = false;
        await service.PollNextAsync(Now, TimeSpan.Zero);Assert.Equal(ResearchInstrument.EthUsd, source.Calls[^1]);Assert.Equal(1, memory.Commits);
    }
    [Fact] public async Task OneActivePollAndCancellationDoNotPublishPartialState()
    {
        var source = new Source { Wait = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously) };var memory = new Memory();
        var service = new ResearchIngestionService(source, memory, memory, 2);using var stop = new CancellationTokenSource();
        var first = service.PollNextAsync(Now, TimeSpan.Zero, stop.Token);
        await Assert.ThrowsAsync<ResearchUnavailableException>(() => service.PollNextAsync(Now, TimeSpan.Zero));
        stop.Cancel();await Assert.ThrowsAnyAsync<OperationCanceledException>(() => first);Assert.Equal(0, memory.Commits);
    }
    [Fact] public async Task InvalidProviderBatchCannotReachWriter()
    {
        var source = new Source { Batch = new([Candle(Now)]) };var memory = new Memory();var service = new ResearchIngestionService(source, memory, memory, 1);
        await Assert.ThrowsAsync<ArgumentException>(() => service.PollNextAsync(Now, TimeSpan.Zero));Assert.Equal(0, memory.Commits);
    }
    [Fact] public void CompletionBoundaryAndGraceAreExact()
    {
        var candle = Candle(Now.AddMinutes(-1));var batch = new ResearchPollBatch([candle]);
        ResearchInputValidation.Validate(new(ResearchInstrument.BtcUsd, Now, TimeSpan.Zero), batch);
        Assert.Throws<ArgumentException>(() => ResearchInputValidation.Validate(new(ResearchInstrument.BtcUsd, Now.AddTicks(-1), TimeSpan.Zero), batch));
        Assert.Throws<ArgumentException>(() => ResearchInputValidation.Validate(new(ResearchInstrument.BtcUsd, Now, TimeSpan.FromSeconds(1)), batch));
        ResearchInputValidation.Validate(new(ResearchInstrument.BtcUsd, Now.AddSeconds(1), TimeSpan.FromSeconds(1)), batch);
    }
    [Theory] [InlineData("low")] [InlineData("high")] [InlineData("volume")] [InlineData("precision")] [InlineData("grid")] [InlineData("offset")] [InlineData("instrument")]
    public void RejectsInvalidRawCandles(string bad)
    {
        Assert.Throws<ArgumentException>(() => new CompletedResearchCandle(bad == "instrument" ? (ResearchInstrument)9 : ResearchInstrument.BtcUsd,
            bad == "grid" ? Now.AddSeconds(1) : bad == "offset" ? Now.ToOffset(TimeSpan.FromHours(1)) : Now,
            100, bad == "high" ? 1_000_000_001m : 101, bad == "low" ? 0 : 99,
            bad == "precision" ? 100.0000000000001m : 100, bad == "volume" ? -1 : 1));
    }
    [Fact] public void BatchBoundsOrderInstrumentAndDuplicateChecks()
    {
        Assert.Throws<ArgumentException>(() => new ResearchPollBatch(Enumerable.Repeat(Candle(Now), 1001)));
        var request = new ResearchPollRequest(ResearchInstrument.BtcUsd, Now.AddMinutes(1), TimeSpan.Zero);
        Assert.Throws<ArgumentException>(() => ResearchInputValidation.Validate(request, new([Candle(Now), Candle(Now)])));
        Assert.Throws<ArgumentException>(() => ResearchInputValidation.Validate(request, new([Candle(Now), Candle(Now.AddMinutes(-1))])));
        Assert.Throws<ArgumentException>(() => ResearchInputValidation.Validate(request, new([new(ResearchInstrument.EthUsd, Now, 100, 101, 99, 100, 1)])));
    }
    private static CompletedResearchCandle Candle(DateTimeOffset time) => new(ResearchInstrument.BtcUsd, time, 100, 101, 99, 100, 1);
    private sealed class Source : IResearchCandleSource
    {
        internal List<ResearchInstrument> Calls { get; } = [];internal bool Fail;internal TaskCompletionSource? Wait;internal ResearchPollBatch Batch = new([]);
        public async Task<ResearchPollBatch> ReadCompletedAsync(ResearchPollRequest request, CancellationToken cancellationToken)
        {
            Calls.Add(request.Instrument);if (Wait is not null) await Wait.Task.WaitAsync(cancellationToken);
            if (Fail) throw new ResearchUnavailableException();return Batch;
        }
    }
    private sealed class Memory : IResearchCommittedStateReader, IResearchStateWriter
    {
        private readonly Guid generation = Guid.NewGuid();internal int Commits;
        public ResearchCommittedState Read(ResearchInstrument instrument) => new(generation, instrument, 0, [], null, null, ResearchIntegrity.Protected);
        public ResearchCommittedState Commit(ResearchPollRequest request, ResearchPollBatch batch, Guid expectedGeneration, long expectedVersion, CancellationToken cancellationToken)
        { cancellationToken.ThrowIfCancellationRequested();Commits++;return Read(request.Instrument); }
    }
}
