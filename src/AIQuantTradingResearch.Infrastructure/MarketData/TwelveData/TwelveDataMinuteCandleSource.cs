using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

// Operator-only diagnostic signals contain no URI, header, body or exception text.
public enum TwelveDataReadEvent
{
    Preflight, RequestStarted, ResponseReceived, ResponseParse, ObservationValidation,
    VolumeUnconfirmed, InvalidPrivateInput, HttpFailure, InvalidHeaders, InvalidPayload, InvalidMetadata,
    InvalidObservation, InvalidVolume, TransportFailure, Timeout, Cancelled, Unknown, InvalidJson, InvalidShape, ProviderErrorEnvelope,
    InvalidStatus, SymbolMismatch, IntervalMismatch, TimezoneMismatch, InvalidCandles, PayloadTooLarge, InvalidTimestamp, InvalidOhlc, InvalidMetadataObject, InvalidSymbolField, InvalidIntervalField, InvalidTimezoneField,
    VolumeMissing, VolumeNull, VolumeWrongType, VolumeInvalidFormat, VolumePrecisionExceeded, VolumeOutOfRange
}
public readonly record struct TwelveDataReadDiagnostic(TwelveDataReadEvent Event, ResearchInstrument Instrument, int? HttpStatus = null);

/// <summary>Opt-in minute transport. Tests supply an offline handler; this class never obtains credentials.</summary>
public sealed class TwelveDataMinuteCandleSource : IResearchCandleSource, IDisposable
{
    public const int MaximumResponseBytes = 1_048_576;
    private readonly HttpClient client;
    private readonly string key;
    private readonly TimeSpan timeout;
    private readonly bool baseAssetVolumeConfirmed;
    private readonly bool ohlcOnly;
    private readonly Action<TwelveDataReadDiagnostic>? diagnostic;
    public TwelveDataMinuteCandleSource(HttpMessageHandler handler, string key, bool baseAssetVolumeConfirmed, TimeSpan? timeout = null, Action<TwelveDataReadDiagnostic>? diagnostic = null, bool ohlcOnly = false)
    {
        ArgumentNullException.ThrowIfNull(handler);
        if (handler is HttpClientHandler http) { http.AllowAutoRedirect = false; http.UseProxy = false; }
        if (handler is SocketsHttpHandler sockets) { sockets.AllowAutoRedirect = false; sockets.UseProxy = false; }
        if (string.IsNullOrWhiteSpace(key) || key.Length > 256 || key.Any(char.IsControl)) throw new ArgumentException("Invalid provider configuration.");
        this.diagnostic = diagnostic;
        this.ohlcOnly = ohlcOnly;
        this.key = key; this.baseAssetVolumeConfirmed = baseAssetVolumeConfirmed;
        this.timeout = timeout ?? TimeSpan.FromSeconds(5);
        if (this.timeout <= TimeSpan.Zero || this.timeout > TimeSpan.FromSeconds(30)) throw new ArgumentException("Invalid provider configuration.");
        client = new HttpClient(handler, disposeHandler: false) { Timeout = Timeout.InfiniteTimeSpan };
    }
    public async Task<ResearchPollBatch> ReadCompletedAsync(ResearchPollRequest request, CancellationToken cancellationToken = default)
    {
        var stage = TwelveDataReadEvent.Preflight;
        var failureReported = false;
        void Signal(TwelveDataReadEvent value, int? status = null) => diagnostic?.Invoke(new(value, request.Instrument, status));
        void Reject(TwelveDataReadEvent value) { failureReported = true; Signal(value); throw new ResearchUnavailableException(); }
        Signal(stage);
        try { ResearchInputValidation.Validate(request, new ResearchPollBatch([])); }
        catch (ArgumentException) { Signal(TwelveDataReadEvent.InvalidPrivateInput); throw; }
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!baseAssetVolumeConfirmed && !ohlcOnly) Reject(TwelveDataReadEvent.VolumeUnconfirmed);
            var symbol = request.Instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD";
            using var message = new HttpRequestMessage(HttpMethod.Get,
                "https://api.twelvedata.com/time_series?symbol=" + Uri.EscapeDataString(symbol) + "&interval=1min&outputsize=1000&timezone=UTC&order=ASC");
            message.Headers.Authorization = new AuthenticationHeaderValue("apikey", key);
            using var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            deadline.CancelAfter(timeout);
            stage = TwelveDataReadEvent.RequestStarted;
            // This means SendAsync was invoked, not that the provider received/charged it.
            Signal(stage);
            using var response = await client.SendAsync(message, HttpCompletionOption.ResponseHeadersRead, deadline.Token).ConfigureAwait(false);
            stage = TwelveDataReadEvent.ResponseReceived;
            Signal(stage, (int)response.StatusCode);
            if (!response.IsSuccessStatusCode) Reject(TwelveDataReadEvent.HttpFailure);
            if (response.RequestMessage?.RequestUri != message.RequestUri ||
                response.Content.Headers.ContentEncoding.Count != 0 ||
                response.Content.Headers.ContentType?.MediaType != "application/json" ||
                (response.Content.Headers.ContentType.CharSet is string charset && !string.Equals(charset, "utf-8", StringComparison.OrdinalIgnoreCase))) Reject(TwelveDataReadEvent.InvalidHeaders);
            if (response.Content.Headers.ContentLength > MaximumResponseBytes) Reject(TwelveDataReadEvent.PayloadTooLarge);
            using var stream = await response.Content.ReadAsStreamAsync(deadline.Token).ConfigureAwait(false);
            using var bytes = new MemoryStream(); var buffer = new byte[4096]; int count;
            while ((count = await stream.ReadAsync(buffer, deadline.Token).ConfigureAwait(false)) != 0)
            {
                if (bytes.Length + count > MaximumResponseBytes) Reject(TwelveDataReadEvent.PayloadTooLarge);
                bytes.Write(buffer, 0, count);
            }
            stage = TwelveDataReadEvent.ResponseParse; Signal(stage);
            JsonDocument parsed;
            try
            {
                var text = new UTF8Encoding(false, true).GetString(bytes.ToArray());
                parsed = JsonDocument.Parse(text, new JsonDocumentOptions { MaxDepth = 8 });
            }
            catch (Exception e) when (e is JsonException or DecoderFallbackException)
            { Reject(TwelveDataReadEvent.InvalidJson); throw; }
            using var doc = parsed;
            try { Unique(doc.RootElement); }
            catch (ResearchUnavailableException) { Reject(TwelveDataReadEvent.InvalidShape); throw; }
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object) Reject(TwelveDataReadEvent.InvalidShape);
            if (!root.TryGetProperty("status", out var status) || status.ValueKind != JsonValueKind.String)
                Reject(TwelveDataReadEvent.InvalidStatus);
            // Recognize only the fixed structural marker; never inspect provider code/message text.
            if (status.GetString() == "error") Reject(TwelveDataReadEvent.ProviderErrorEnvelope);
            if (status.GetString() != "ok") Reject(TwelveDataReadEvent.InvalidStatus);
            // Official twelvedata-java GetTimeSeries200ResponseMeta marks exchangeTimezone optional.
            // Only BTC/ETH are admitted, and this request fixes intraday output timezone=UTC;
            // absent exchange metadata is not fabricated or used as the timestamp timezone.
            if (!root.TryGetProperty("meta", out var meta) || meta.ValueKind != JsonValueKind.Object)
                Reject(TwelveDataReadEvent.InvalidMetadataObject);
            if (!meta.TryGetProperty("symbol", out var metaSymbol) || metaSymbol.ValueKind != JsonValueKind.String)
                Reject(TwelveDataReadEvent.InvalidSymbolField);
            if (!meta.TryGetProperty("interval", out var interval) || interval.ValueKind != JsonValueKind.String)
                Reject(TwelveDataReadEvent.InvalidIntervalField);
            var hasExchangeTimezone = meta.TryGetProperty("exchange_timezone", out var timezone);
            if (hasExchangeTimezone && timezone.ValueKind != JsonValueKind.String)
                Reject(TwelveDataReadEvent.InvalidTimezoneField);
            if (meta.GetProperty("symbol").GetString() != symbol) Reject(TwelveDataReadEvent.SymbolMismatch);
            if (meta.GetProperty("interval").GetString() != "1min") Reject(TwelveDataReadEvent.IntervalMismatch);
            if (hasExchangeTimezone && timezone.GetString() != "UTC") Reject(TwelveDataReadEvent.TimezoneMismatch);
            if (!root.TryGetProperty("values", out var values) || values.ValueKind != JsonValueKind.Array || values.GetArrayLength() > 1000)
                Reject(TwelveDataReadEvent.InvalidCandles);
            stage = TwelveDataReadEvent.ObservationValidation; Signal(stage);
            var candles = new List<CompletedResearchCandle>();
            foreach (var row in values.EnumerateArray())
            {
                if (row.ValueKind != JsonValueKind.Object) Reject(TwelveDataReadEvent.InvalidCandles);
                if (!row.TryGetProperty("datetime", out var stamp) || stamp.ValueKind != JsonValueKind.String ||
                    !DateTimeOffset.TryParseExact(stamp.GetString(), "yyyy-MM-dd HH:mm:ss",
                    CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out _)) Reject(TwelveDataReadEvent.InvalidTimestamp);
                var time = DateTimeOffset.ParseExact(stamp.GetString()!, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
                decimal? volume = null;
                if (!ohlcOnly)
                {
                    if (!row.TryGetProperty("volume", out var volumeField)) Reject(TwelveDataReadEvent.VolumeMissing);
                    if (volumeField.ValueKind == JsonValueKind.Null) Reject(TwelveDataReadEvent.VolumeNull);
                    if (volumeField.ValueKind != JsonValueKind.String) Reject(TwelveDataReadEvent.VolumeWrongType);
                    var volumeText = volumeField.GetString()!;
                    // Preserve Value's acceptance and check order; only fixed diagnostics differ.
                    if (volumeText.Length == 0 || volumeText.Length > 40 ||
                        volumeText.Any(x => x is not (>= '0' and <= '9') and not '.')) Reject(TwelveDataReadEvent.VolumeInvalidFormat);
                    if (volumeText.Contains('.') && volumeText.Length - volumeText.IndexOf('.') - 1 > 12)
                        Reject(TwelveDataReadEvent.VolumePrecisionExceeded);
                    if (!decimal.TryParse(volumeText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var parsedVolume))
                    {
                        // Multiple points are format errors; a well-formed digit string can overflow decimal.
                        Reject(volumeText.Count(x => x == '.') > 1 || volumeText == "."
                            ? TwelveDataReadEvent.VolumeInvalidFormat : TwelveDataReadEvent.VolumeOutOfRange);
                    }
                    volume = parsedVolume;
                }
                try
                {
                    var open = Value(row, "open"); var high = Value(row, "high");
                    var low = Value(row, "low"); var close = Value(row, "close");
                    // OHLC encoding still precedes constructor-domain rejection.
                    if (volume > 1_000_000_000_000m) Reject(TwelveDataReadEvent.VolumeOutOfRange);
                    candles.Add(new CompletedResearchCandle(request.Instrument, time, open, high, low, close, volume));
                }
                catch (Exception e) when (e is ResearchUnavailableException or InvalidOperationException or KeyNotFoundException)
                { if (!failureReported) Reject(TwelveDataReadEvent.InvalidOhlc); throw; }
            }
            DateTimeOffset? previous = null;
            foreach (var candle in candles)
            {
                if (candle.OpenTimeUtc > request.ObservedAtUtc || (previous is not null && candle.OpenTimeUtc <= previous)) Reject(TwelveDataReadEvent.InvalidObservation);
                previous = candle.OpenTimeUtc;
            }
            // Validate forming bars but do not commit them; no synthetic gap filling.
            var batch = new ResearchPollBatch(candles.Where(c => c.OpenTimeUtc.AddMinutes(1) <= request.ObservedAtUtc - request.CompletionGrace));
            ResearchInputValidation.Validate(request, batch);
            return batch;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        { Signal(TwelveDataReadEvent.Cancelled); throw; }
        catch (OperationCanceledException) { Signal(TwelveDataReadEvent.Timeout); throw new ResearchUnavailableException(); }
        catch (Exception e) when (e is HttpRequestException or IOException)
        { Signal(TwelveDataReadEvent.TransportFailure); throw new ResearchUnavailableException(); }
        catch (ResearchUnavailableException)
        { if (!failureReported) Signal(stage == TwelveDataReadEvent.ObservationValidation ? TwelveDataReadEvent.InvalidObservation : TwelveDataReadEvent.InvalidPayload); throw; }
        catch (Exception e) when (e is JsonException or InvalidOperationException or KeyNotFoundException or FormatException or ArgumentException)
        {
            Signal(stage switch
            {
                TwelveDataReadEvent.ObservationValidation => TwelveDataReadEvent.InvalidObservation,
                TwelveDataReadEvent.ResponseParse => TwelveDataReadEvent.InvalidPayload,
                _ => TwelveDataReadEvent.Unknown
            });
            throw new ResearchUnavailableException();
        }
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
