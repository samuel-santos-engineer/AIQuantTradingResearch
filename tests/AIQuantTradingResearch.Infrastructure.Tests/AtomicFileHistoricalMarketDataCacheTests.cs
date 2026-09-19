using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class AtomicFileHistoricalMarketDataCacheTests
{
    [Fact] public async Task RoundTripsCanonicalSnapshot() { using var fixture = new Fixture(); var entry = Entry(); await fixture.Cache.StoreAsync(entry); var result = await fixture.Cache.RetrieveAsync(entry.Key); Assert.Equal(HistoricalMarketDataCacheLookupState.Found, result.State); Assert.Equal(entry.Candles, result.Entry!.Candles); }
    [Fact] public async Task CorruptFileIsUnusable() { using var fixture = new Fixture(); var key = Entry().Key; await File.WriteAllTextAsync(Path.Combine(fixture.Directory, key.Fingerprint + ".json"), "not-json"); var result = await fixture.Cache.RetrieveAsync(key); Assert.Equal(HistoricalMarketDataCacheLookupState.Corrupt, result.State); Assert.Null(result.Entry); }
    [Fact] public async Task RetentionIsBounded() { using var fixture = new Fixture(2); await fixture.Cache.StoreAsync(Entry(HistoricalCandleRange.OneDay)); await fixture.Cache.StoreAsync(Entry(HistoricalCandleRange.SevenDays)); await fixture.Cache.StoreAsync(Entry(HistoricalCandleRange.ThirtyDays)); Assert.Equal(2, Directory.EnumerateFiles(fixture.Directory, "*.json").Count()); }
    private static HistoricalMarketDataCacheEntry Entry(HistoricalCandleRange range = HistoricalCandleRange.ThirtyDays) { var now = new DateTimeOffset(2026, 9, 19, 12, 0, 0, TimeSpan.Zero); var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcUsd, HistoricalCandleInterval.OneHour, range, TimeSpan.FromSeconds(10)); var key = new HistoricalMarketDataCacheKey(request.Symbol, request.Interval, request.Range, "Vike", HistoricalMarketDataReadService.CacheContractVersion); return new HistoricalMarketDataCacheEntry(key, request, [new HistoricalCandle(request.Symbol, request.Interval, now.AddHours(-1), 10m, 12m, 9m, 11m, 100m)], new HistoricalMarketDataProvenance("Vike", now, now, false, true), now); }
    private sealed class Fixture : IDisposable { public Fixture(int limit = 32) { Directory = Path.Combine(Path.GetTempPath(), "aiq-wp04-" + Guid.NewGuid().ToString("N")); System.IO.Directory.CreateDirectory(Directory); Cache = new AtomicFileHistoricalMarketDataCache(Directory, limit); } public string Directory { get; } public AtomicFileHistoricalMarketDataCache Cache { get; } public void Dispose() { if (System.IO.Directory.Exists(Directory)) System.IO.Directory.Delete(Directory, true); } }
}
