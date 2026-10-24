using System.Collections.ObjectModel;

namespace AIQuantTradingResearch.Application.MarketData;

public enum ResearchInstrument { BtcUsd, EthUsd }
public enum ResearchIntegrity { Protected, Degraded, Recovery }

/// <summary>Private raw observation; never a presentation DTO.</summary>
public sealed record CompletedResearchCandle
{
    public ResearchInstrument Instrument { get; }
    public DateTimeOffset OpenTimeUtc { get; }
    public decimal Open { get; }
    public decimal High { get; }
    public decimal Low { get; }
    public decimal Close { get; }
    public decimal Volume { get; }
    public string Provider { get; } = "twelvedata";
    public string VolumeUnit => Instrument == ResearchInstrument.BtcUsd ? "BTC" : "ETH";

    public CompletedResearchCandle(ResearchInstrument instrument, DateTimeOffset openTimeUtc,
        decimal open, decimal high, decimal low, decimal close, decimal volume)
    {
        if (!Enum.IsDefined(instrument) || openTimeUtc.Offset != TimeSpan.Zero ||
            openTimeUtc.Year is < 2000 or > 2099 || openTimeUtc.Ticks % TimeSpan.TicksPerMinute != 0 ||
            low <= 0 || high > 1_000_000_000m || low > Math.Min(open, close) || high < Math.Max(open, close) ||
            volume is < 0 or > 1_000_000_000_000m || !Exact(open) || !Exact(high) || !Exact(low) || !Exact(close) || !Exact(volume))
            throw new ArgumentException("Invalid research candle.");
        Instrument = instrument; OpenTimeUtc = openTimeUtc;
        Open = open; High = high; Low = low; Close = close; Volume = volume;
    }
    private static bool Exact(decimal value) => decimal.Round(value, 12) == value;
}

public sealed record ResearchStoredCandle(CompletedResearchCandle Candle, Guid Segment, long SegmentIndex);

/// <summary>An immutable, coherent SQLite commit. Calculated values are not implemented by WP03.</summary>
public sealed class ResearchCommittedState
{
    public Guid Generation { get; }
    public ResearchInstrument Instrument { get; }
    public long Version { get; }
    public IReadOnlyList<ResearchStoredCandle> Candles { get; }
    public DateTimeOffset? LastCommitAt { get; }
    public DateTimeOffset? LastSuccessfulProviderPollAt { get; }
    public ResearchIntegrity Integrity { get; }
    public bool ResearchReady { get; }
    public string ResearchStatus => Integrity == ResearchIntegrity.Recovery ? "Recovering" : "Unavailable";
    public ResearchCommittedState(Guid generation, ResearchInstrument instrument, long version,
        IEnumerable<ResearchStoredCandle> candles, DateTimeOffset? lastCommitAt,
        DateTimeOffset? lastSuccessfulProviderPollAt, ResearchIntegrity integrity)
    {
        Generation = generation; Instrument = instrument; Version = version;
        Candles = new ReadOnlyCollection<ResearchStoredCandle>(candles.ToArray());
        LastCommitAt = lastCommitAt; LastSuccessfulProviderPollAt = lastSuccessfulProviderPollAt; Integrity = integrity;
    }
}

public sealed record ResearchPollRequest(ResearchInstrument Instrument, DateTimeOffset ObservedAtUtc, TimeSpan CompletionGrace);
public sealed class ResearchPollBatch
{
    public IReadOnlyList<CompletedResearchCandle> Candles { get; }
    public ResearchPollBatch(IEnumerable<CompletedResearchCandle> candles)
    {
        ArgumentNullException.ThrowIfNull(candles);
        var copy = candles.Take(1001).ToArray();
        if (copy.Length > 1000 || copy.Any(x => x is null)) throw new ArgumentException("Invalid research batch.");
        Candles = Array.AsReadOnly(copy);
    }
}
public sealed class ResearchUnavailableException : Exception
{
    public ResearchUnavailableException() : base("Research unavailable.") { }
}
public interface IResearchCandleSource
{
    Task<ResearchPollBatch> ReadCompletedAsync(ResearchPollRequest request, CancellationToken cancellationToken = default);
}
public interface IResearchCommittedStateReader
{
    ResearchCommittedState Read(ResearchInstrument instrument);
}
public interface IResearchStateWriter
{
    ResearchCommittedState Commit(ResearchPollRequest request, ResearchPollBatch batch,
        Guid expectedGeneration, long expectedVersion, CancellationToken cancellationToken = default);
}

public static class ResearchInputValidation
{
    public static void Validate(ResearchPollRequest request, ResearchPollBatch batch)
    {
        ArgumentNullException.ThrowIfNull(request); ArgumentNullException.ThrowIfNull(batch);
        if (!Enum.IsDefined(request.Instrument) || request.ObservedAtUtc.Offset != TimeSpan.Zero ||
            request.ObservedAtUtc.Year is < 2000 or > 2099 || request.CompletionGrace < TimeSpan.Zero ||
            request.CompletionGrace > TimeSpan.FromMinutes(5)) throw new ArgumentException("Invalid research request.");
        DateTimeOffset? previous = null;
        foreach (var candle in batch.Candles)
        {
            if (candle.Instrument != request.Instrument || candle.OpenTimeUtc.AddMinutes(1) > request.ObservedAtUtc - request.CompletionGrace ||
                (previous is not null && candle.OpenTimeUtc <= previous)) throw new ArgumentException("Invalid research batch.");
            previous = candle.OpenTimeUtc;
        }
    }
}
