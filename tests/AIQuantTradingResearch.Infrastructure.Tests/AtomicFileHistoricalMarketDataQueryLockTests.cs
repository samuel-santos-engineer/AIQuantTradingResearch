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

    [Fact]
    public async Task ContenderDeadlineCannotRecoverAnActiveLease()
    {
        using var fixture = new Fixture();
        using var first = await fixture.Lock.AcquireAsync(Key(HistoricalCandleRange.ThirtyDays), TimeSpan.FromSeconds(30), CancellationToken.None);
        Assert.NotNull(first);
        var blocked = await fixture.Lock.AcquireAsync(Key(HistoricalCandleRange.ThirtyDays), TimeSpan.FromMilliseconds(50), CancellationToken.None);
        Assert.Null(blocked);
        Assert.True(File.Exists(fixture.LockPath(Key(HistoricalCandleRange.ThirtyDays))));
    }

    [Fact]
    public async Task ExpiredAbandonedLeaseIsRecovered()
    {
        using var fixture = new Fixture();
        var path = fixture.LockPath(Key(HistoricalCandleRange.ThirtyDays));
        File.WriteAllText(path, $"abandoned|{DateTimeOffset.UtcNow.AddMinutes(-2):O}");
        using var recovered = await fixture.Lock.AcquireAsync(Key(HistoricalCandleRange.ThirtyDays), TimeSpan.FromSeconds(5), CancellationToken.None);
        Assert.NotNull(recovered);
    }

    private static HistoricalMarketDataCacheKey Key(HistoricalCandleRange range) => new(CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, range, "Vike", HistoricalMarketDataReadService.CacheContractVersion);
    private sealed class Fixture : IDisposable
    {
        public Fixture() { Directory = Path.Combine(Path.GetTempPath(), "aiq-wp05-lock-" + Guid.NewGuid().ToString("N")); System.IO.Directory.CreateDirectory(Directory); Lock = new AtomicFileHistoricalMarketDataQueryLock(Directory); }
        public string Directory { get; } public AtomicFileHistoricalMarketDataQueryLock Lock { get; }
        public string LockPath(HistoricalMarketDataCacheKey key) => Path.Combine(Directory, $".{key.Fingerprint}.query.lock");
        public void Dispose() { if (System.IO.Directory.Exists(Directory)) System.IO.Directory.Delete(Directory, true); }
    }
}
