using System.Globalization;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;

namespace AIQuantTradingResearch.Worker;

public sealed class HistoricalMarketDataQueryExecution
{
    public const string ModeName = "HistoricalQuery";
    public const string ContractVersion = "aiq-historical-query-v1";
    private const int MaximumRequestBytes = 4096;
    private static readonly JsonSerializerOptions ProtocolJsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HistoricalMarketDataReadService service;
    private readonly AtomicFileHistoricalMarketDataQueryLock queryLock;

    public HistoricalMarketDataQueryExecution(HistoricalMarketDataReadService service, AtomicFileHistoricalMarketDataQueryLock queryLock)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
        this.queryLock = queryLock ?? throw new ArgumentNullException(nameof(queryLock));
    }

    public async Task<int> ExecuteAsync(TextReader input, TextWriter output, TextWriter error, CancellationToken cancellationToken = default)
    {
        try
        {
            var raw = await input.ReadToEndAsync(cancellationToken);
            if (Encoding.UTF8.GetByteCount(raw) > MaximumRequestBytes) return await WriteAsync(output, Response.Invalid());
            var request = Parse(raw);
            if (request is null) return await WriteAsync(output, Response.Invalid());

            var key = new HistoricalMarketDataCacheKey(request.Symbol, request.Interval, request.Range, "Vike", HistoricalMarketDataReadService.CacheContractVersion);
            using var acquired = await queryLock.AcquireAsync(key, request.Deadline, cancellationToken);
            if (acquired is null) return await WriteAsync(output, Response.Unavailable("Timeout"));

            // The read service performs the post-lock cache recheck and all freshness/provider decisions.
            var result = await service.ReadAsync(request, cancellationToken);
            return await WriteAsync(output, Response.From(request, result));
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return await WriteAsync(output, Response.Unavailable("Cancelled"));
        }
        catch (Exception)
        {
            await error.WriteLineAsync("historical-query: controlled failure");
            return await WriteAsync(output, Response.Unavailable("BridgeFailure"));
        }
    }

    private static HistoricalMarketDataRequest? Parse(string raw)
    {
        try
        {
            var value = JsonSerializer.Deserialize<WireRequest>(raw, ProtocolJsonOptions);
            var deadlineSeconds = value?.DeadlineSeconds;
            if (value is null || value.ContractVersion != ContractVersion || value.Symbol is null || value.Interval is null || value.Range is null || deadlineSeconds is null || deadlineSeconds is < 1 or > 30) return null;
            if (!Symbols.TryGetValue(value.Symbol, out var symbol) || !Intervals.TryGetValue(value.Interval, out var interval) || !Ranges.TryGetValue(value.Range, out var range)) return null;
            return new HistoricalMarketDataRequest(symbol, interval, range, TimeSpan.FromSeconds(deadlineSeconds.Value));
        }
        catch (JsonException) { return null; }
    }

    private static async Task<int> WriteAsync(TextWriter output, Response response)
    {
        await output.WriteAsync(JsonSerializer.Serialize(response, ProtocolJsonOptions));
        await output.FlushAsync();
        return 0;
    }

    private static readonly Dictionary<string, CanonicalMarketSymbol> Symbols = new(StringComparer.Ordinal) { ["BTC/USD"] = CanonicalMarketSymbol.BtcUsd, ["ETH/USD"] = CanonicalMarketSymbol.EthUsd };
    private static readonly Dictionary<string, HistoricalCandleInterval> Intervals = new(StringComparer.Ordinal) { ["1h"] = HistoricalCandleInterval.OneHour, ["4h"] = HistoricalCandleInterval.FourHours, ["1d"] = HistoricalCandleInterval.OneDay };
    private static readonly Dictionary<string, HistoricalCandleRange> Ranges = new(StringComparer.Ordinal) { ["1D"] = HistoricalCandleRange.OneDay, ["7D"] = HistoricalCandleRange.SevenDays, ["30D"] = HistoricalCandleRange.ThirtyDays, ["90D"] = HistoricalCandleRange.NinetyDays };

    private sealed record WireRequest(string? ContractVersion, string? Symbol, string? Interval, string? Range, int? DeadlineSeconds);
    private sealed record Candle(string OpenTimeUtc, string Open, string High, string Low, string Close, string Volume);
    private sealed record Response(string ContractVersion, string State, string? Symbol, string? Interval, string? Range, IReadOnlyList<Candle>? Candles, string? Provider, string? LastUpdatedUtc, string? LastValidatedUtc, bool IsStale, string? Failure)
    {
        public static Response Invalid() => new(HistoricalMarketDataQueryExecution.ContractVersion, "Unavailable", null, null, null, null, null, null, null, false, "InvalidRequest");
        public static Response Unavailable(string failure) => new(HistoricalMarketDataQueryExecution.ContractVersion, "Unavailable", null, null, null, null, null, null, null, false, failure);
        public static Response From(HistoricalMarketDataRequest request, HistoricalMarketDataReadResult result) => new(HistoricalMarketDataQueryExecution.ContractVersion, result.State.ToString(), ToSymbol(request.Symbol), ToInterval(request.Interval), ToRange(request.Range), result.Candles?.Select(c => new Candle(c.OpenTimeUtc.ToString("O"), Decimal(c.Open), Decimal(c.High), Decimal(c.Low), Decimal(c.Close), Decimal(c.Volume))).ToArray(), result.Provenance?.Provider, result.LastUpdatedUtc?.ToString("O"), result.LastValidatedUtc?.ToString("O"), result.State == HistoricalMarketDataReadState.Stale, result.Failure?.ToString());
        private static string Decimal(decimal value) => value.ToString(CultureInfo.InvariantCulture);
        private static string ToSymbol(CanonicalMarketSymbol value) => value == CanonicalMarketSymbol.BtcUsd ? "BTC/USD" : "ETH/USD";
        private static string ToInterval(HistoricalCandleInterval value) => value switch { HistoricalCandleInterval.OneHour => "1h", HistoricalCandleInterval.FourHours => "4h", _ => "1d" };
        private static string ToRange(HistoricalCandleRange value) => value switch { HistoricalCandleRange.OneDay => "1D", HistoricalCandleRange.SevenDays => "7D", HistoricalCandleRange.ThirtyDays => "30D", _ => "90D" };
    }
}
