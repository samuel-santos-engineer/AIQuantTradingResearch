using System.Collections.Concurrent;

namespace AIQuantTradingResearch.Application.MarketData;

public sealed class HistoricalMarketDataReadService
{
    public const string CacheContractVersion = "aiq-historical-market-data-cache-v1";
    private readonly IHistoricalMarketDataProvider provider; private readonly IHistoricalMarketDataCache cache; private readonly string providerIdentity; private readonly TimeProvider clock;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> gates = new(StringComparer.Ordinal);
    public HistoricalMarketDataReadService(IHistoricalMarketDataProvider provider, IHistoricalMarketDataCache cache, string providerIdentity, TimeProvider? clock = null) { this.provider = provider ?? throw new ArgumentNullException(nameof(provider)); this.cache = cache ?? throw new ArgumentNullException(nameof(cache)); ArgumentException.ThrowIfNullOrWhiteSpace(providerIdentity); this.providerIdentity = providerIdentity; this.clock = clock ?? TimeProvider.System; }
    public async Task<HistoricalMarketDataReadResult> ReadAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request); var key = new HistoricalMarketDataCacheKey(request.Symbol, request.Interval, request.Range, providerIdentity, CacheContractVersion);
        var cached = await RetrieveSafelyAsync(key, cancellationToken); if (IsFresh(cached.Entry, request.Interval)) return HistoricalMarketDataReadResult.FromCache(cached.Entry!, false);
        var gate = gates.GetOrAdd(key.Fingerprint, static _ => new SemaphoreSlim(1, 1)); await gate.WaitAsync(cancellationToken);
        try { cached = await RetrieveSafelyAsync(key, cancellationToken); if (IsFresh(cached.Entry, request.Interval)) return HistoricalMarketDataReadResult.FromCache(cached.Entry!, false);
            var response = await AcquireAsync(request, cancellationToken); if (response.IsSuccess) { var entry = new HistoricalMarketDataCacheEntry(key, request, response.Candles!, response.Provenance!, clock.GetUtcNow()); await StoreSafelyAsync(entry, cancellationToken); return HistoricalMarketDataReadResult.FromCache(entry, false); }
            return cached.Entry is not null ? HistoricalMarketDataReadResult.FromCache(cached.Entry, true, response.Failure) : HistoricalMarketDataReadResult.Unavailable(response.Failure!.Value); }
        finally { gate.Release(); }
    }
    public static TimeSpan GetFreshnessWindow(HistoricalCandleInterval interval) => interval switch { HistoricalCandleInterval.OneHour => TimeSpan.FromMinutes(15), HistoricalCandleInterval.FourHours => TimeSpan.FromMinutes(30), HistoricalCandleInterval.OneDay => TimeSpan.FromHours(2), _ => throw new ArgumentOutOfRangeException(nameof(interval)) };
    private bool IsFresh(HistoricalMarketDataCacheEntry? entry, HistoricalCandleInterval interval) { if (entry is null) return false; var age = clock.GetUtcNow() - entry.Provenance.LastValidatedAtUtc; return age >= TimeSpan.Zero && age <= GetFreshnessWindow(interval); }
    private async Task<HistoricalMarketDataCacheLookup> RetrieveSafelyAsync(HistoricalMarketDataCacheKey key, CancellationToken cancellationToken)
    { try { return await cache.RetrieveAsync(key, cancellationToken); } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; } catch (IOException) { return HistoricalMarketDataCacheLookup.Corrupt(); } catch (UnauthorizedAccessException) { return HistoricalMarketDataCacheLookup.Corrupt(); } }
    private async Task StoreSafelyAsync(HistoricalMarketDataCacheEntry entry, CancellationToken cancellationToken)
    { try { await cache.StoreAsync(entry, cancellationToken); } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; } catch (IOException) { } catch (UnauthorizedAccessException) { } }
    private async Task<HistoricalMarketDataResponse> AcquireAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken)
    { try { var response = await provider.GetHistoricalCandlesAsync(request, cancellationToken); return response.IsSuccess && !string.Equals(response.Provenance!.Provider, providerIdentity, StringComparison.Ordinal) ? HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse) : response; }
      catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Cancelled); }
      catch (OperationCanceledException) { return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Timeout); }
      catch (Exception) { return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Unavailable); } }
}
