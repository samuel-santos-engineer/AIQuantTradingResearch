using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;
using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class HistoricalMarketDataQueryExecutionTests
{
    [Fact]
    public async Task ValidCanonicalRequestProducesOneSafeProtocolResponse()
    {
        using var fixture = new Fixture();
        var output = new StringWriter();
        var exit = await fixture.Execution.ExecuteAsync(new StringReader("{\"contractVersion\":\"aiq-historical-query-v1\",\"symbol\":\"BTC/USD\",\"interval\":\"1h\",\"range\":\"30D\",\"deadlineSeconds\":10}"), output, new StringWriter());
        using var json = JsonDocument.Parse(output.ToString());
        Assert.Equal(0, exit); Assert.True(string.Equals("Fresh", json.RootElement.GetProperty("state").GetString(), StringComparison.Ordinal), output.ToString()); Assert.Equal("BTC/USD", json.RootElement.GetProperty("symbol").GetString()); Assert.Equal(1, fixture.Provider.Calls); Assert.DoesNotContain("secret", output.ToString(), StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task MalformedRequestCannotFallThroughToLegacyOutput()
    {
        using var fixture = new Fixture(); var output = new StringWriter();
        await fixture.Execution.ExecuteAsync(new StringReader("not-json"), output, new StringWriter());
        using var json = JsonDocument.Parse(output.ToString());
        Assert.Equal("Unavailable", json.RootElement.GetProperty("state").GetString()); Assert.Equal("InvalidRequest", json.RootElement.GetProperty("failure").GetString()); Assert.Equal(0, fixture.Provider.Calls);
    }

    private sealed class Fixture : IDisposable
    {
        private readonly string directory = Path.Combine(Path.GetTempPath(), "aiq-wp05-query-" + Guid.NewGuid().ToString("N"));
        public Fixture() { Directory.CreateDirectory(directory); Provider = new Provider(); var cache = new Cache(); Execution = new HistoricalMarketDataQueryExecution(new HistoricalMarketDataReadService(Provider, cache, "Vike"), new AtomicFileHistoricalMarketDataQueryLock(directory)); }
        public Provider Provider { get; } public HistoricalMarketDataQueryExecution Execution { get; }
        public void Dispose() { if (Directory.Exists(directory)) Directory.Delete(directory, true); }
    }
    private sealed class Cache : IHistoricalMarketDataCache { public Task<HistoricalMarketDataCacheLookup> RetrieveAsync(HistoricalMarketDataCacheKey key, CancellationToken ct = default) => Task.FromResult(HistoricalMarketDataCacheLookup.Missing()); public Task StoreAsync(HistoricalMarketDataCacheEntry entry, CancellationToken ct = default) => Task.CompletedTask; }
    private sealed class Provider : IHistoricalMarketDataProvider { public int Calls { get; private set; } public Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(HistoricalMarketDataRequest request, CancellationToken ct = default) { Calls++; var now = DateTimeOffset.UnixEpoch; return Task.FromResult(HistoricalMarketDataResponse.Available(request, [new HistoricalCandle(request.Symbol, request.Interval, now, 1m, 2m, 1m, 2m, 3m)], new HistoricalMarketDataProvenance("Vike", now, now, false, true))); } }
}
