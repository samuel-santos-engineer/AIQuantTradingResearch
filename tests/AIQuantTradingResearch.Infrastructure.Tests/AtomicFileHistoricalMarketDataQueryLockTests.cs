using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class AtomicFileHistoricalMarketDataQueryLockTests
{
    [Fact]
    public async Task SameKeyIsBoundedAndReleasesForNextContender()
    {
        using var fixture = new Fixture();
        var key = Key(HistoricalCandleRange.ThirtyDays);
        using var first = await fixture.Lock.AcquireAsync(key, TimeSpan.FromMilliseconds(100), CancellationToken.None);
        Assert.NotNull(first);
        var blocked = await fixture.Lock.AcquireAsync(key, TimeSpan.FromMilliseconds(50), CancellationToken.None);
        Assert.Null(blocked);
        first.Dispose();
        using var next = await fixture.Lock.AcquireAsync(key, TimeSpan.FromMilliseconds(100), CancellationToken.None);
        Assert.NotNull(next);
    }

    [Fact]
    public async Task DifferentKeysDoNotSerialize()
    {
        using var fixture = new Fixture();
        using var first = await fixture.Lock.AcquireAsync(Key(HistoricalCandleRange.OneDay), TimeSpan.FromMilliseconds(100), CancellationToken.None);
        using var other = await fixture.Lock.AcquireAsync(Key(HistoricalCandleRange.NinetyDays), TimeSpan.FromMilliseconds(100), CancellationToken.None);
        Assert.NotNull(first); Assert.NotNull(other);
    }

    private static HistoricalMarketDataCacheKey Key(HistoricalCandleRange range) => new(CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, range, "Vike", HistoricalMarketDataReadService.CacheContractVersion);
    private sealed class Fixture : IDisposable
    {
        public Fixture() { Directory = Path.Combine(Path.GetTempPath(), "aiq-wp05-lock-" + Guid.NewGuid().ToString("N")); System.IO.Directory.CreateDirectory(Directory); Lock = new AtomicFileHistoricalMarketDataQueryLock(Directory); }
        public string Directory { get; } public AtomicFileHistoricalMarketDataQueryLock Lock { get; }
        public void Dispose() { if (System.IO.Directory.Exists(Directory)) System.IO.Directory.Delete(Directory, true); }
    }
}
