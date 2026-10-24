namespace AIQuantTradingResearch.Application.MarketData;

/// <summary>Caller-driven, single-flight fair polling. No background task or default network activation.</summary>
public sealed class ResearchIngestionService : IDisposable
{
    private readonly IResearchCandleSource source;
    private readonly IResearchCommittedStateReader reader;
    private readonly IResearchStateWriter writer;
    private readonly int creditsPerMinute;
    private readonly SemaphoreSlim admission = new(1, 1);
    private long window = -1;
    private int spent;
    private ResearchInstrument next;
    public ResearchIngestionService(IResearchCandleSource source, IResearchCommittedStateReader reader,
        IResearchStateWriter writer, int creditsPerMinute)
    {
        this.source = source ?? throw new ArgumentNullException(nameof(source));
        this.reader = reader ?? throw new ArgumentNullException(nameof(reader));
        this.writer = writer ?? throw new ArgumentNullException(nameof(writer));
        if (creditsPerMinute is < 1 or > 60) throw new ArgumentOutOfRangeException(nameof(creditsPerMinute));
        this.creditsPerMinute = creditsPerMinute;
    }
    public void Dispose() => admission.Dispose();
    public async Task<ResearchCommittedState> PollNextAsync(DateTimeOffset now, TimeSpan grace, CancellationToken cancellationToken = default)
    {
        if (!await admission.WaitAsync(0, cancellationToken).ConfigureAwait(false)) throw new ResearchUnavailableException();
        try
        {
            var request = new ResearchPollRequest(next, now, grace);
            ResearchInputValidation.Validate(request, new ResearchPollBatch([]));
            var minute = now.ToUnixTimeSeconds() / 60;
            if (minute < window) throw new ResearchUnavailableException();
            if (minute > window) { window = minute; spent = 0; }
            if (spent >= creditsPerMinute) throw new ResearchUnavailableException();
            var prior = reader.Read(next);
            if (prior.Integrity == ResearchIntegrity.Recovery) throw new ResearchUnavailableException();
            spent++; // A failed provider call still consumes a configured credit. No immediate retry.
            next = next == ResearchInstrument.BtcUsd ? ResearchInstrument.EthUsd : ResearchInstrument.BtcUsd;
            var batch = await source.ReadCompletedAsync(request, cancellationToken).ConfigureAwait(false);
            ResearchInputValidation.Validate(request, batch);
            return writer.Commit(request, batch, prior.Generation, prior.Version, cancellationToken);
        }
        finally { admission.Release(); }
    }
}
