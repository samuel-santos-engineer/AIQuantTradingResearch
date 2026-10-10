using System.Globalization;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Cache;

namespace AIQuantTradingResearch.Worker;

public sealed class HistoricalMarketDataQueryExecution
{
    public const string ModeName = "HistoricalQuery";
    public const string ContractVersion = SyntheticPublicationPolicy.QueryVersion;
    private const int MaximumRequestBytes = 4096;
    private static readonly JsonSerializerOptions ProtocolJsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly UTF8Encoding StrictUtf8 = new(false, true);

    private readonly HistoricalMarketDataReadService service;
    private readonly AtomicFileHistoricalMarketDataQueryLock queryLock;

    public HistoricalMarketDataQueryExecution(HistoricalMarketDataReadService service, AtomicFileHistoricalMarketDataQueryLock queryLock)
    {
        this.service = service ?? throw new ArgumentNullException(nameof(service));
        this.queryLock = queryLock ?? throw new ArgumentNullException(nameof(queryLock));
    }

    /// <summary>WP07 must wire Console.OpenStandardInput here. Does not own the input stream.</summary>
    public async Task<int> ExecuteAsync(Stream input, TextWriter output, TextWriter error, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(input);
        var bytes = new byte[MaximumRequestBytes + 1];
        var length = 0;
        using var readDeadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        readDeadline.CancelAfter(TimeSpan.FromSeconds(10));
        try
        {
            while (length <= MaximumRequestBytes)
            {
                var count = await input.ReadAsync(bytes.AsMemory(length, bytes.Length - length), readDeadline.Token);
                if (count == 0) return await ExecuteRawAsync(StrictUtf8.GetString(bytes, 0, length), output, error, readDeadline.Token);
                length += count;
                if (length > MaximumRequestBytes) return await WriteAsync(output, Response.Invalid());
            }
        }
        catch (DecoderFallbackException) { return await WriteAsync(output, Response.Invalid()); }
        catch (OperationCanceledException) { return await WriteAsync(output, Response.Unavailable("Timeout")); }
        catch (IOException) { return await WriteAsync(output, Response.Unavailable("Unavailable")); }
        catch (UnauthorizedAccessException) { return await WriteAsync(output, Response.Unavailable("Unavailable")); }
        return await WriteAsync(output, Response.Invalid());
    }

    /// <summary>Compatibility entry point for current Program.cs; cannot certify original UTF-8 bytes.</summary>
    public async Task<int> ExecuteAsync(TextReader input, TextWriter output, TextWriter error, CancellationToken cancellationToken = default)
    {
        var characters = new char[MaximumRequestBytes + 1];
        var length = 0;
        try
        {
            while (length <= MaximumRequestBytes)
            {
                var count = await input.ReadAsync(characters.AsMemory(length, characters.Length - length), cancellationToken);
                if (count == 0) return await ExecuteRawAsync(new string(characters, 0, length), output, error, cancellationToken);
                length += count;
                if (length > MaximumRequestBytes) return await WriteAsync(output, Response.Invalid());
            }
        }
        catch (OperationCanceledException) { return await WriteAsync(output, Response.Unavailable("Timeout")); }
        return await WriteAsync(output, Response.Invalid());
    }

    private async Task<int> ExecuteRawAsync(string raw, TextWriter output, TextWriter error, CancellationToken cancellationToken)
    {
        try
        {
            if (StrictUtf8.GetByteCount(raw) > MaximumRequestBytes) return await WriteAsync(output, Response.Invalid());
            var request = Parse(raw);
            if (request is null) return await WriteAsync(output, Response.Invalid());

            if (!service.IsPublicSynthetic) return await WriteAsync(output, Response.Unavailable("Unavailable"));
            using var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            deadline.CancelAfter(request.Deadline < TimeSpan.FromSeconds(10) ? request.Deadline : TimeSpan.FromSeconds(10));
            var key = service.GetPublicKey(request);
            using var acquired = await queryLock.AcquireAsync(key, request.Deadline, deadline.Token);
            if (acquired is null) return await WriteAsync(output, Response.Unavailable("Timeout"));

            // The read service performs the post-lock cache recheck and all freshness/provider decisions.
            var result = await service.ReadAsync(request, deadline.Token);
            if (result.State == HistoricalMarketDataReadState.Unavailable) return await WriteAsync(output, Response.Unavailable(result.Failure == HistoricalMarketDataFailure.Timeout ? "Timeout" : "Unavailable"));
            return await WriteAsync(output, Response.From(request, result));
        }
        catch (EncoderFallbackException) { return await WriteAsync(output, Response.Invalid()); }
        catch (OperationCanceledException)
        {
            return await WriteAsync(output, Response.Unavailable("Timeout"));
        }
        catch (Exception)
        {
            await error.WriteLineAsync("historical-query: controlled failure");
            return await WriteAsync(output, Response.Unavailable("Unavailable"));
        }
    }

    private static HistoricalMarketDataRequest? Parse(string raw)
    {
        try
        {
            if (raw.Any(c => c > 127)) return null;
            using var document = JsonDocument.Parse(raw, new JsonDocumentOptions { MaxDepth = 4 });
            var root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Object) return null;
            var expected = new HashSet<string>(StringComparer.Ordinal) { "contractVersion", "instrument", "interval", "range", "deadlineSeconds" };
            foreach (var property in root.EnumerateObject()) if (!expected.Remove(property.Name)) return null;
            if (expected.Count != 0 || root.GetProperty("contractVersion").GetString() != ContractVersion ||
                root.GetProperty("instrument").GetString() != SyntheticPublicationPolicy.Instrument ||
                !root.GetProperty("deadlineSeconds").TryGetInt32(out var seconds) || seconds is < 1 or > 30 ||
                !Intervals.TryGetValue(root.GetProperty("interval").GetString()!, out var interval) ||
                !Ranges.TryGetValue(root.GetProperty("range").GetString()!, out var range)) return null;
            return new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation, interval, range, TimeSpan.FromSeconds(seconds));
        }
        catch (JsonException) { return null; }
        catch (InvalidOperationException) { return null; }
        catch (ArgumentException) { return null; }
    }

    private static async Task<int> WriteAsync(TextWriter output, Response response)
    {
        await output.WriteAsync(JsonSerializer.Serialize(response, ProtocolJsonOptions));
        await output.FlushAsync();
        return 0;
    }

    private static readonly Dictionary<string, HistoricalCandleInterval> Intervals = new(StringComparer.Ordinal) { ["1h"] = HistoricalCandleInterval.OneHour, ["4h"] = HistoricalCandleInterval.FourHours, ["1d"] = HistoricalCandleInterval.OneDay };
    private static readonly Dictionary<string, HistoricalCandleRange> Ranges = new(StringComparer.Ordinal) { ["1D"] = HistoricalCandleRange.OneDay, ["7D"] = HistoricalCandleRange.SevenDays, ["30D"] = HistoricalCandleRange.ThirtyDays, ["90D"] = HistoricalCandleRange.NinetyDays };

    private sealed record Candle(string OpenTimeUtc, string Open, string High, string Low, string Close, string Volume);
    private sealed record Response(string ContractVersion, string State, string? Instrument, string? Interval, string? Range, IReadOnlyList<Candle>? Candles, SyntheticMarketDataIdentity? Provenance, string? GeneratedAtUtc, string? Failure)
    {
        public static Response Invalid() => Unavailable("InvalidRequest");
        public static Response Unavailable(string failure) => new(HistoricalMarketDataQueryExecution.ContractVersion, "Unavailable", null, null, null, null, null, null, failure);
        public static Response From(HistoricalMarketDataRequest request, HistoricalMarketDataReadResult result)
        {
            SyntheticPublicationPolicy.ValidateWindow(request, result.Candles!, result.Provenance!);
            return new(HistoricalMarketDataQueryExecution.ContractVersion, "Available", SyntheticPublicationPolicy.Instrument, ToInterval(request.Interval), ToRange(request.Range), result.Candles!.Skip(250).Select(c => new Candle(SyntheticPublicationPolicy.Timestamp(c.OpenTimeUtc), c.Open.ToString("F4", CultureInfo.InvariantCulture), c.High.ToString("F4", CultureInfo.InvariantCulture), c.Low.ToString("F4", CultureInfo.InvariantCulture), c.Close.ToString("F4", CultureInfo.InvariantCulture), c.Volume.ToString("F3", CultureInfo.InvariantCulture))).ToArray(), result.Provenance!.SyntheticIdentity, SyntheticPublicationPolicy.Timestamp(result.Provenance.AcquiredAtUtc), null);
        }
        private static string ToInterval(HistoricalCandleInterval value) => value switch { HistoricalCandleInterval.OneHour => "1h", HistoricalCandleInterval.FourHours => "4h", _ => "1d" };
        private static string ToRange(HistoricalCandleRange value) => value switch { HistoricalCandleRange.OneDay => "1D", HistoricalCandleRange.SevenDays => "7D", HistoricalCandleRange.ThirtyDays => "30D", _ => "90D" };
    }
}
