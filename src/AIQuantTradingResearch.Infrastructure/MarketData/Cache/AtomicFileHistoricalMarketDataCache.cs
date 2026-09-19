using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.Cache;

public sealed class AtomicFileHistoricalMarketDataCache : IHistoricalMarketDataCache
{
    private readonly string directory; private readonly int retentionLimit;
    public AtomicFileHistoricalMarketDataCache(string directory, int retentionLimit = 32) { if (!Path.IsPathFullyQualified(directory)) throw new ArgumentException("Cache directory must be absolute.", nameof(directory)); ArgumentOutOfRangeException.ThrowIfLessThan(retentionLimit, 1); this.directory = Path.GetFullPath(directory); this.retentionLimit = retentionLimit; }
    public async Task<HistoricalMarketDataCacheLookup> RetrieveAsync(HistoricalMarketDataCacheKey key, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(key); var path = Path.Combine(directory, key.Fingerprint + ".json"); if (!File.Exists(path)) return HistoricalMarketDataCacheLookup.Missing();
        try { await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous); var record = await JsonSerializer.DeserializeAsync<Record>(stream, cancellationToken: cancellationToken); return record is null || record.Symbol != key.Symbol || record.Interval != key.Interval || record.Range != key.Range || record.Provider != key.ProviderIdentity || record.Version != key.ContractVersion ? HistoricalMarketDataCacheLookup.Corrupt() : HistoricalMarketDataCacheLookup.Found(ToEntry(record)); }
        catch (JsonException) { return HistoricalMarketDataCacheLookup.Corrupt(); } catch (ArgumentException) { return HistoricalMarketDataCacheLookup.Corrupt(); } catch (OverflowException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
    }
    public async Task StoreAsync(HistoricalMarketDataCacheEntry entry, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entry); Directory.CreateDirectory(directory); var target = Path.Combine(directory, entry.Key.Fingerprint + ".json"); var temporary = Path.Combine(directory, "." + entry.Key.Fingerprint + "." + Guid.NewGuid().ToString("N") + ".tmp");
        try { await using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.WriteThrough)) { await JsonSerializer.SerializeAsync(stream, FromEntry(entry), cancellationToken: cancellationToken); await stream.FlushAsync(cancellationToken); stream.Flush(true); } File.Move(temporary, target, true); foreach (var file in Directory.EnumerateFiles(directory, "*.json").Select(static p => new FileInfo(p)).OrderByDescending(static f => f.LastWriteTimeUtc).Skip(retentionLimit)) file.Delete(); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
    private static Record FromEntry(HistoricalMarketDataCacheEntry entry) => new(entry.Key.Symbol, entry.Key.Interval, entry.Key.Range, entry.Key.ProviderIdentity, entry.Key.ContractVersion, entry.Request.Deadline, entry.CachedAtUtc, entry.Provenance.AcquiredAtUtc, entry.Provenance.LastValidatedAtUtc, entry.Provenance.IsStale, entry.Provenance.ContainsOnlyClosedCandles, entry.Candles.Select(static c => new Row(c.OpenTimeUtc, c.Open, c.High, c.Low, c.Close, c.Volume)).ToArray());
    private static HistoricalMarketDataCacheEntry ToEntry(Record r) { var key = new HistoricalMarketDataCacheKey(r.Symbol, r.Interval, r.Range, r.Provider, r.Version); var request = new HistoricalMarketDataRequest(r.Symbol, r.Interval, r.Range, r.Deadline); var provenance = new HistoricalMarketDataProvenance(r.Provider, r.Acquired, r.Validated, r.IsStale, r.Closed); return new HistoricalMarketDataCacheEntry(key, request, r.Candles.Select(c => new HistoricalCandle(r.Symbol, r.Interval, c.OpenTimeUtc, c.Open, c.High, c.Low, c.Close, c.Volume)), provenance, r.Cached); }
    private sealed record Record(CanonicalMarketSymbol Symbol, HistoricalCandleInterval Interval, HistoricalCandleRange Range, string Provider, string Version, TimeSpan Deadline, DateTimeOffset Cached, DateTimeOffset Acquired, DateTimeOffset Validated, bool IsStale, bool Closed, IReadOnlyList<Row> Candles);
    private sealed record Row(DateTimeOffset OpenTimeUtc, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);
}
