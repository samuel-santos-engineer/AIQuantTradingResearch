using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

/// <summary>Opt-in minute transport. Tests supply an offline handler; this class never obtains credentials.</summary>
public sealed class TwelveDataMinuteCandleSource : IResearchCandleSource, IDisposable
{
    public const int MaximumResponseBytes = 1_048_576;
    private readonly HttpClient client;
    private readonly string key;
    private readonly TimeSpan timeout;
    private readonly bool baseAssetVolumeConfirmed;
    public TwelveDataMinuteCandleSource(HttpMessageHandler handler, string key, bool baseAssetVolumeConfirmed, TimeSpan? timeout = null)
    {
        ArgumentNullException.ThrowIfNull(handler);
        if (handler is HttpClientHandler http) { http.AllowAutoRedirect = false; http.UseProxy = false; }
        if (handler is SocketsHttpHandler sockets) { sockets.AllowAutoRedirect = false; sockets.UseProxy = false; }
        if (string.IsNullOrWhiteSpace(key) || key.Length > 256 || key.Any(char.IsControl)) throw new ArgumentException("Invalid provider configuration.");
        this.key = key; this.baseAssetVolumeConfirmed = baseAssetVolumeConfirmed;
        this.timeout = timeout ?? TimeSpan.FromSeconds(5);
        if (this.timeout <= TimeSpan.Zero || this.timeout > TimeSpan.FromSeconds(30)) throw new ArgumentException("Invalid provider configuration.");
        client = new HttpClient(handler, disposeHandler: false) { Timeout = Timeout.InfiniteTimeSpan };
    }
    public async Task<ResearchPollBatch> ReadCompletedAsync(ResearchPollRequest request, CancellationToken cancellationToken = default)
    {
        ResearchInputValidation.Validate(request, new ResearchPollBatch([]));
        cancellationToken.ThrowIfCancellationRequested();
        if (!baseAssetVolumeConfirmed) throw new ResearchUnavailableException();
        var symbol = request.Instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD";
        using var message = new HttpRequestMessage(HttpMethod.Get,
            "https://api.twelvedata.com/time_series?symbol=" + Uri.EscapeDataString(symbol) + "&interval=1min&outputsize=1000&timezone=UTC&order=ASC");
        message.Headers.Authorization = new AuthenticationHeaderValue("apikey", key);
        using var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        deadline.CancelAfter(timeout);
        try
        {
            using var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, deadline.Token).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode || response.RequestMessage?.RequestUri != message.RequestUri ||
                response.Content.Headers.ContentLength > MaximumResponseBytes || response.Content.Headers.ContentEncoding.Count != 0 ||
                response.Content.Headers.ContentType?.MediaType != "application/json" ||
                (response.Content.Headers.ContentType.CharSet is string charset && !string.Equals(charset, "utf-8", StringComparison.OrdinalIgnoreCase))) throw new ResearchUnavailableException();
            using var stream = await response.Content.ReadAsStreamAsync(deadline.Token).ConfigureAwait(false);
            using var bytes = new MemoryStream(); var buffer = new byte[4096]; int count;
            while ((count = await stream.ReadAsync(buffer, deadline.Token).ConfigureAwait(false)) != 0)
            {
                if (bytes.Length + count > MaximumResponseBytes) throw new ResearchUnavailableException();
                bytes.Write(buffer, 0, count);
            }
            var text = new UTF8Encoding(false, true).GetString(bytes.ToArray());
            using var doc = JsonDocument.Parse(text, new JsonDocumentOptions { MaxDepth = 8 });
            Unique(doc.RootElement);
            var root = doc.RootElement;
            if (root.GetProperty("status").GetString() != "ok") throw new ResearchUnavailableException();
            var meta = root.GetProperty("meta");
            if (meta.GetProperty("symbol").GetString() != symbol || meta.GetProperty("interval").GetString() != "1min" ||
                meta.GetProperty("exchange_timezone").GetString() != "UTC") throw new ResearchUnavailableException();
            var values = root.GetProperty("values");
            if (values.ValueKind != JsonValueKind.Array || values.GetArrayLength() > 1000) throw new ResearchUnavailableException();
            var candles = new List<CompletedResearchCandle>();
            foreach (var row in values.EnumerateArray())
            {
                if (!DateTimeOffset.TryParseExact(row.GetProperty("datetime").GetString(), "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var time)) throw new ResearchUnavailableException();
                candles.Add(new CompletedResearchCandle(request.Instrument, time, Value(row, "open"), Value(row, "high"),
                    Value(row, "low"), Value(row, "close"), Value(row, "volume")));
            }
            DateTimeOffset? previous = null;
            foreach (var candle in candles)
            {
                if (candle.OpenTimeUtc > request.ObservedAtUtc || (previous is not null && candle.OpenTimeUtc <= previous)) throw new ResearchUnavailableException();
                previous = candle.OpenTimeUtc;
            }
            // Provider latest windows can include a forming bar: validate it, but never commit it.
            var batch = new ResearchPollBatch(candles.Where(c => c.OpenTimeUtc.AddMinutes(1) <= request.ObservedAtUtc - request.CompletionGrace));
            ResearchInputValidation.Validate(request, batch);
            return batch;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) { throw; }
        catch (Exception e) when (e is HttpRequestException or IOException or JsonException or InvalidOperationException or
            KeyNotFoundException or FormatException or ArgumentException or OperationCanceledException)
        { throw new ResearchUnavailableException(); }
    }
    private static decimal Value(JsonElement row, string name)
    {
        var text = row.GetProperty(name).GetString();
        if (text is null || text.Length > 40 || text.Length == 0 || text.Any(x => x is not (>= '0' and <= '9') and not '.') ||
            (text.Contains('.') && text.Length - text.IndexOf('.') - 1 > 12) ||
            !decimal.TryParse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var value)) throw new ResearchUnavailableException();
        return value;
    }
    public void Dispose() => client.Dispose();
    private static void Unique(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);
            foreach (var p in element.EnumerateObject()) { if (!names.Add(p.Name)) throw new ResearchUnavailableException(); Unique(p.Value); }
        }
        else if (element.ValueKind == JsonValueKind.Array) foreach (var item in element.EnumerateArray()) Unique(item);
    }
}
