using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace AIQuantTradingResearch.Application.MarketData;

public sealed record HistoricalMarketDataCacheKey
{
    public HistoricalMarketDataCacheKey(CanonicalMarketSymbol symbol, HistoricalCandleInterval interval, HistoricalCandleRange range, string providerIdentity, string contractVersion)
    {
        if (!Enum.IsDefined(symbol) || !Enum.IsDefined(interval) || !Enum.IsDefined(range)) throw new ArgumentOutOfRangeException(nameof(symbol));
        ArgumentException.ThrowIfNullOrWhiteSpace(providerIdentity); ArgumentException.ThrowIfNullOrWhiteSpace(contractVersion);
        Symbol = symbol; Interval = interval; Range = range; ProviderIdentity = providerIdentity; ContractVersion = contractVersion;
    }
    public CanonicalMarketSymbol Symbol { get; } public HistoricalCandleInterval Interval { get; } public HistoricalCandleRange Range { get; }
    public string ProviderIdentity { get; } public string ContractVersion { get; }
    public string Fingerprint => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes($"aiq-historical-cache-key-v1|{Symbol}|{Interval}|{Range}|{ProviderIdentity}|{ContractVersion}"))).ToLowerInvariant();
}

public sealed record HistoricalMarketDataCacheEntry
{
    public HistoricalMarketDataCacheEntry(HistoricalMarketDataCacheKey key, HistoricalMarketDataRequest request, IEnumerable<HistoricalCandle> candles, HistoricalMarketDataProvenance provenance, DateTimeOffset cachedAtUtc)
    {
        ArgumentNullException.ThrowIfNull(key); ArgumentNullException.ThrowIfNull(request); ArgumentNullException.ThrowIfNull(candles); ArgumentNullException.ThrowIfNull(provenance);
        if (cachedAtUtc.Offset != TimeSpan.Zero || key.Symbol != request.Symbol || key.Interval != request.Interval || key.Range != request.Range || !string.Equals(key.ProviderIdentity, provenance.Provider, StringComparison.Ordinal)) throw new ArgumentException("Cache key, request, provenance, and timestamp must agree.");
        var rows = candles.ToArray(); _ = HistoricalMarketDataResponse.Available(request, rows, provenance);
        Key = key; Request = request; Candles = new ReadOnlyCollection<HistoricalCandle>(rows); Provenance = provenance; CachedAtUtc = cachedAtUtc;
    }
    public HistoricalMarketDataCacheKey Key { get; } public HistoricalMarketDataRequest Request { get; }
    public IReadOnlyList<HistoricalCandle> Candles { get; } public HistoricalMarketDataProvenance Provenance { get; } public DateTimeOffset CachedAtUtc { get; }
}

public enum HistoricalMarketDataCacheLookupState { Missing, Found, Corrupt }
public sealed record HistoricalMarketDataCacheLookup(HistoricalMarketDataCacheLookupState State, HistoricalMarketDataCacheEntry? Entry)
{
    public static HistoricalMarketDataCacheLookup Missing() => new(HistoricalMarketDataCacheLookupState.Missing, null);
    public static HistoricalMarketDataCacheLookup Corrupt() => new(HistoricalMarketDataCacheLookupState.Corrupt, null);
    public static HistoricalMarketDataCacheLookup Found(HistoricalMarketDataCacheEntry entry) => new(HistoricalMarketDataCacheLookupState.Found, entry ?? throw new ArgumentNullException(nameof(entry)));
}
public interface IHistoricalMarketDataCache { Task<HistoricalMarketDataCacheLookup> RetrieveAsync(HistoricalMarketDataCacheKey key, CancellationToken cancellationToken = default); Task StoreAsync(HistoricalMarketDataCacheEntry entry, CancellationToken cancellationToken = default); }
public enum HistoricalMarketDataReadState { Fresh, Stale, Unavailable }
public sealed record HistoricalMarketDataReadResult(HistoricalMarketDataReadState State, IReadOnlyList<HistoricalCandle>? Candles, HistoricalMarketDataProvenance? Provenance, DateTimeOffset? LastUpdatedUtc, DateTimeOffset? LastValidatedUtc, HistoricalMarketDataFailure? Failure)
{
    public static HistoricalMarketDataReadResult FromCache(HistoricalMarketDataCacheEntry entry, bool stale, HistoricalMarketDataFailure? failure = null) => new(stale ? HistoricalMarketDataReadState.Stale : HistoricalMarketDataReadState.Fresh, entry.Candles, entry.Provenance, entry.Provenance.AcquiredAtUtc, entry.Provenance.LastValidatedAtUtc, failure);
    public static HistoricalMarketDataReadResult Unavailable(HistoricalMarketDataFailure failure) => new(HistoricalMarketDataReadState.Unavailable, null, null, null, null, failure);
}
