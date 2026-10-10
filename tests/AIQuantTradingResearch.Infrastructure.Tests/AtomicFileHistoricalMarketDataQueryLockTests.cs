using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class AtomicFileHistoricalMarketDataQueryLockTests
{
    [Fact]
    public async Task PublicLocksRejectPrivateRootsAndTraversalBeforeCreatingFiles()
    {
        using var fixture = new SyntheticTestFixture(); var key = SyntheticTestFixture.Entry().Key;
        var traversal = new AtomicFileHistoricalMarketDataQueryLock(Path.Combine(fixture.Directory, "..", "aiq-public-synthetic-cache"));
        await Assert.ThrowsAsync<ArgumentException>(() => traversal.AcquireAsync(key, TimeSpan.FromSeconds(1), CancellationToken.None));
        var privateRoot = new AtomicFileHistoricalMarketDataQueryLock(Path.Combine(Path.GetTempPath(), "aiq-private-research-cache"));
        await Assert.ThrowsAsync<ArgumentException>(() => privateRoot.AcquireAsync(key, TimeSpan.FromSeconds(1), CancellationToken.None));
        Assert.Empty(Directory.EnumerateFiles(fixture.Directory));
    }
    [Fact]
    public async Task StaleTimestampCannotStealLiveHandleAndCancellationDoesNotReleaseOwner()
    {
        using var fixture = new Fixture(); var key = Key(HistoricalCandleRange.OneDay); var path = fixture.LockPath(key);
        await File.WriteAllTextAsync(path, $"old|{DateTimeOffset.UtcNow.AddHours(-1):O}");
        using var owner = await fixture.Lock.AcquireAsync(key, TimeSpan.FromSeconds(30), CancellationToken.None);
        Assert.NotNull(owner);
        File.SetLastWriteTimeUtc(path, DateTime.UtcNow.AddHours(-1));
        Assert.Null(await fixture.Lock.AcquireAsync(key, TimeSpan.FromMilliseconds(50), CancellationToken.None));
        using var cancellation = new CancellationTokenSource();
        var waiting = fixture.Lock.AcquireAsync(key, TimeSpan.FromSeconds(30), cancellation.Token);
        cancellation.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => waiting);
        Assert.Null(await fixture.Lock.AcquireAsync(key, TimeSpan.FromMilliseconds(50), CancellationToken.None));
    }

    [Fact]
    public async Task ConcurrentContendersNeverOverlapAndDoubleDisposalCannotReleaseNextOwner()
    {
        using var fixture = new Fixture(); var key = Key(HistoricalCandleRange.OneDay); var active = 0;
        await Task.WhenAll(Enumerable.Range(0, 6).Select(async _ =>
        {
            using var owner = await fixture.Lock.AcquireAsync(key, TimeSpan.FromSeconds(30), CancellationToken.None); Assert.NotNull(owner);
            Assert.Equal(1, Interlocked.Increment(ref active)); await Task.Delay(10); Interlocked.Decrement(ref active);
        }));
        var previous = await fixture.Lock.AcquireAsync(key, TimeSpan.FromSeconds(1), CancellationToken.None); previous!.Dispose();
        using var next = await fixture.Lock.AcquireAsync(key, TimeSpan.FromSeconds(1), CancellationToken.None); previous.Dispose();
        Assert.Null(await fixture.Lock.AcquireAsync(key, TimeSpan.FromMilliseconds(50), CancellationToken.None));
    }
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
