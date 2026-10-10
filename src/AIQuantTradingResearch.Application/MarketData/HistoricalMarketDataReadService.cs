using System.Collections.Concurrent;

namespace AIQuantTradingResearch.Application.MarketData;

public sealed class HistoricalMarketDataReadService : IDisposable
{
    public const string CacheContractVersion = "aiq-historical-market-data-cache-v1";
    private readonly IHistoricalMarketDataProvider provider; private readonly IHistoricalMarketDataCache cache; private readonly string providerIdentity; private readonly TimeProvider clock;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> gates = new(StringComparer.Ordinal);
    private SyntheticPublicationPolicy? publicationPolicy;
    private readonly SemaphoreSlim publicGate = new(1, 1);
    public bool IsPublicSynthetic => publicationPolicy is not null;
    public static HistoricalMarketDataReadService PublicSynthetic(IIndependentSyntheticHistoricalMarketDataProvider provider, IHistoricalMarketDataCache cache, SyntheticPublicationPolicy policy, TimeProvider? clock = null)
    {
        ArgumentNullException.ThrowIfNull(policy);
        return new(provider, cache, SyntheticPublicationPolicy.Source, clock) { publicationPolicy = policy };
    }
    public HistoricalMarketDataCacheKey GetPublicKey(HistoricalMarketDataRequest request) => HistoricalMarketDataCacheKey.Public(request, publicationPolicy ?? throw new InvalidOperationException("Public composition required."));
    public void Dispose() { publicGate.Dispose(); foreach (var gate in gates.Values) gate.Dispose(); }
    public HistoricalMarketDataReadService(IHistoricalMarketDataProvider provider, IHistoricalMarketDataCache cache, string providerIdentity, TimeProvider? clock = null) { this.provider = provider ?? throw new ArgumentNullException(nameof(provider)); this.cache = cache ?? throw new ArgumentNullException(nameof(cache)); ArgumentException.ThrowIfNullOrWhiteSpace(providerIdentity); this.providerIdentity = providerIdentity; this.clock = clock ?? TimeProvider.System; }
    public async Task<HistoricalMarketDataReadResult> ReadAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (publicationPolicy is not null) return await ReadPublicAsync(request, cancellationToken);
        if (request.Symbol == CanonicalMarketSymbol.BtcLikeSimulation) return HistoricalMarketDataReadResult.Unavailable(HistoricalMarketDataFailure.InvalidRequest);
        var key = new HistoricalMarketDataCacheKey(request.Symbol, request.Interval, request.Range, providerIdentity, CacheContractVersion);
        var cached = await RetrieveSafelyAsync(key, cancellationToken); if (IsFresh(cached.Entry, request.Interval)) return HistoricalMarketDataReadResult.FromCache(cached.Entry!, false);
        var gate = gates.GetOrAdd(key.Fingerprint, static _ => new SemaphoreSlim(1, 1)); await gate.WaitAsync(cancellationToken);
        try { cached = await RetrieveSafelyAsync(key, cancellationToken); if (IsFresh(cached.Entry, request.Interval)) return HistoricalMarketDataReadResult.FromCache(cached.Entry!, false);
            var response = await AcquireAsync(request, cancellationToken); if (response.IsSuccess) { var entry = new HistoricalMarketDataCacheEntry(key, request, response.Candles!, response.Provenance!, clock.GetUtcNow()); await StoreSafelyAsync(entry, cancellationToken); return HistoricalMarketDataReadResult.FromCache(entry, false); }
            return cached.Entry is not null ? HistoricalMarketDataReadResult.FromCache(cached.Entry, true, response.Failure) : HistoricalMarketDataReadResult.Unavailable(response.Failure!.Value); }
        finally { gate.Release(); }
    }
    private async Task<HistoricalMarketDataReadResult> ReadPublicAsync(HistoricalMarketDataRequest request, CancellationToken cancellationToken)
    {
        if (request.Symbol != CanonicalMarketSymbol.BtcLikeSimulation) return HistoricalMarketDataReadResult.Unavailable(HistoricalMarketDataFailure.InvalidRequest);
        using var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        deadline.CancelAfter(request.Deadline < TimeSpan.FromSeconds(10) ? request.Deadline : TimeSpan.FromSeconds(10));
        var entered = false;
        try
        {
            await publicGate.WaitAsync(deadline.Token); entered = true;
            var key = GetPublicKey(request);
            var cached = await RetrieveSafelyAsync(key, deadline.Token);
            if (cached.Entry is { } entry)
            {
                try
                {
                    if (entry.Key != key || entry.Request.Symbol != request.Symbol || entry.Request.Interval != request.Interval || entry.Request.Range != request.Range)
                        throw new ArgumentException("Cache identity mismatch.");
                    publicationPolicy!.Validate(request, entry.Candles, entry.Provenance);
                    var age = clock.GetUtcNow() - entry.CachedAtUtc;
                    if (age >= TimeSpan.Zero && age < TimeSpan.FromHours(24)) return HistoricalMarketDataReadResult.FromCache(entry, false);
                }
                catch (ArgumentException) { /* Invalid hits never become fallback candidates. */ }
            }
            var response = await AcquireAsync(request, deadline.Token);
            if (!response.IsSuccess) return HistoricalMarketDataReadResult.Unavailable(response.Failure!.Value);
            publicationPolicy!.Validate(request, response.Candles!, response.Provenance!);
            var fresh = new HistoricalMarketDataCacheEntry(key, request, response.Candles!, response.Provenance!, clock.GetUtcNow());
            await StoreSafelyAsync(fresh, deadline.Token);
            return HistoricalMarketDataReadResult.FromCache(fresh, false);
        }
        catch (OperationCanceledException) { return HistoricalMarketDataReadResult.Unavailable(cancellationToken.IsCancellationRequested ? HistoricalMarketDataFailure.Cancelled : HistoricalMarketDataFailure.Timeout); }
        catch (ArgumentException) { return HistoricalMarketDataReadResult.Unavailable(HistoricalMarketDataFailure.MalformedResponse); }
        finally { if (entered) publicGate.Release(); }
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
