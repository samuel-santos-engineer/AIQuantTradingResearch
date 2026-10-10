using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class AtomicFileHistoricalMarketDataCacheTests
{
    [Fact]
    public async Task PublicRoundTripAtomicReplacementAndNoTemporaryFiles()
    {
        using var fixture = new SyntheticTestFixture(); var entry = SyntheticTestFixture.Entry();
        await fixture.Cache.StoreAsync(entry); await fixture.Cache.StoreAsync(entry);
        var result = await fixture.Cache.RetrieveAsync(entry.Key);
        Assert.Equal(HistoricalMarketDataCacheLookupState.Found, result.State); Assert.Equal(entry.Candles, result.Entry!.Candles);
        Assert.Single(Directory.EnumerateFiles(fixture.Directory, "*.json")); Assert.Empty(Directory.EnumerateFiles(fixture.Directory, "*.tmp"));
    }

    [Fact]
    public async Task PublicCacheRejectsOversizedCorruptMixedVersionDuplicateAndFingerprintMismatch()
    {
        using var fixture = new SyntheticTestFixture(); var entry = SyntheticTestFixture.Entry(); var path = Path.Combine(fixture.Directory, entry.Key.Fingerprint + ".json");
        await fixture.Cache.StoreAsync(entry); var original = await File.ReadAllTextAsync(path);
        foreach (var corrupt in new[] { "not-json", new string('x', 2 * 1024 * 1024 + 1), original.Replace("aiq-candles-v2", "aiq-candles-v1", StringComparison.Ordinal), original.Replace(entry.Provenance.SyntheticIdentity!.WindowFingerprint, new string('b', 64), StringComparison.Ordinal), original.Insert(1, "\"DeadlineSeconds\":10,") })
        {
            await File.WriteAllTextAsync(path, corrupt);
            Assert.Equal(HistoricalMarketDataCacheLookupState.Corrupt, (await fixture.Cache.RetrieveAsync(entry.Key)).State);
        }
    }

    [Fact]
    public async Task PublicCacheCannotReadOrWritePrivateKeysAndRejectsPrivateRoots()
    {
        using var fixture = new SyntheticTestFixture(); var privateEntry = Entry();
        Assert.Equal(HistoricalMarketDataCacheLookupState.Corrupt, (await fixture.Cache.RetrieveAsync(privateEntry.Key)).State);
        await Assert.ThrowsAsync<ArgumentException>(() => fixture.Cache.StoreAsync(privateEntry));
        Assert.Throws<ArgumentException>(() => new AtomicFileHistoricalMarketDataCache(Path.Combine(Path.GetTempPath(), "aiq-private-research-cache"), publicSynthetic: true));
        Assert.Throws<ArgumentException>(() => new AtomicFileHistoricalMarketDataCache(Path.Combine(Path.GetTempPath(), "aiq-private-research-cache", "aiq-public-synthetic-cache"), publicSynthetic: true));
        Assert.Throws<ArgumentException>(() => new AtomicFileHistoricalMarketDataCache(Path.Combine(fixture.Directory, "..", "aiq-public-synthetic-cache"), publicSynthetic: true));
    }

    [Fact]
    public async Task PublicCancelledWritePreservesExistingEntryAndCleansTemps()
    {
        using var fixture = new SyntheticTestFixture(); var entry = SyntheticTestFixture.Entry(); await fixture.Cache.StoreAsync(entry);
        var before = await File.ReadAllBytesAsync(Path.Combine(fixture.Directory, entry.Key.Fingerprint + ".json"));
        using var cancelled = new CancellationTokenSource(); cancelled.Cancel();
        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => fixture.Cache.StoreAsync(entry, cancelled.Token));
        Assert.Equal(before, await File.ReadAllBytesAsync(Path.Combine(fixture.Directory, entry.Key.Fingerprint + ".json"))); Assert.Empty(Directory.EnumerateFiles(fixture.Directory, "*.tmp"));
    }

    [Fact]
    public async Task PublicTtlIsWriteBasedAndSelectionRetentionIsTwelve()
    {
        using var fixture = new SyntheticTestFixture(); var clock = new MutableClock(); var cache = new AtomicFileHistoricalMarketDataCache(fixture.Directory, publicSynthetic: true, clock: clock);
        var first = SyntheticTestFixture.Entry(); await cache.StoreAsync(first);
        clock.Now = clock.Now.AddHours(23); Assert.Equal(HistoricalMarketDataCacheLookupState.Found, (await cache.RetrieveAsync(first.Key)).State);
        clock.Now = clock.Now.AddHours(1); Assert.Equal(HistoricalMarketDataCacheLookupState.Missing, (await cache.RetrieveAsync(first.Key)).State);
        foreach (var interval in Enum.GetValues<HistoricalCandleInterval>()) foreach (var range in Enum.GetValues<HistoricalCandleRange>()) await cache.StoreAsync(SyntheticTestFixture.Entry(SyntheticTestFixture.Request(interval, range)));
        Assert.Equal(12, Directory.EnumerateFiles(fixture.Directory, "*.json").Count());
        Assert.True(Directory.EnumerateFiles(fixture.Directory).Sum(p => new FileInfo(p).Length) <= 32 * 1024 * 1024);
    }

    private sealed class MutableClock : TimeProvider { public DateTimeOffset Now { get; set; } = DateTimeOffset.UtcNow; public override DateTimeOffset GetUtcNow() => Now; }
    [Fact] public async Task RoundTripsCanonicalSnapshot() { using var fixture = new Fixture(); var entry = Entry(); await fixture.Cache.StoreAsync(entry); var result = await fixture.Cache.RetrieveAsync(entry.Key); Assert.Equal(HistoricalMarketDataCacheLookupState.Found, result.State); Assert.Equal(entry.Candles, result.Entry!.Candles); }
    [Fact] public async Task CorruptFileIsUnusable() { using var fixture = new Fixture(); var key = Entry().Key; await File.WriteAllTextAsync(Path.Combine(fixture.Directory, key.Fingerprint + ".json"), "not-json"); var result = await fixture.Cache.RetrieveAsync(key); Assert.Equal(HistoricalMarketDataCacheLookupState.Corrupt, result.State); Assert.Null(result.Entry); }
    [Fact] public async Task RetentionIsBounded() { using var fixture = new Fixture(2); await fixture.Cache.StoreAsync(Entry(HistoricalCandleRange.OneDay)); await fixture.Cache.StoreAsync(Entry(HistoricalCandleRange.SevenDays)); await fixture.Cache.StoreAsync(Entry(HistoricalCandleRange.ThirtyDays)); Assert.Equal(2, Directory.EnumerateFiles(fixture.Directory, "*.json").Count()); }
    private static HistoricalMarketDataCacheEntry Entry(HistoricalCandleRange range = HistoricalCandleRange.ThirtyDays) { var now = new DateTimeOffset(2026, 9, 19, 12, 0, 0, TimeSpan.Zero); var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, range, TimeSpan.FromSeconds(10)); var key = new HistoricalMarketDataCacheKey(request.Symbol, request.Interval, request.Range, "Vike", HistoricalMarketDataReadService.CacheContractVersion); return new HistoricalMarketDataCacheEntry(key, request, [new HistoricalCandle(request.Symbol, request.Interval, now.AddHours(-1), 10m, 12m, 9m, 11m, 100m)], new HistoricalMarketDataProvenance("Vike", now, now, false, true), now); }
    private sealed class Fixture : IDisposable { public Fixture(int limit = 32) { Directory = Path.Combine(Path.GetTempPath(), "aiq-wp04-" + Guid.NewGuid().ToString("N")); System.IO.Directory.CreateDirectory(Directory); Cache = new AtomicFileHistoricalMarketDataCache(Directory, limit); } public string Directory { get; } public AtomicFileHistoricalMarketDataCache Cache { get; } public void Dispose() { if (System.IO.Directory.Exists(Directory)) System.IO.Directory.Delete(Directory, true); } }
}
