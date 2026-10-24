using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AIQuantTradingResearch.Application.MarketData;

public sealed class ResearchSnapshotException(string code) : Exception("Request unavailable.")
{
    public string Code { get; } = code;
}
public sealed record ResearchSnapshotIdentity(string Generation, string Version, string DisplayDate, string TransformEpoch);
public sealed record ResearchSnapshotRequest(string SchemaVersion, string Instrument, string RequestEpoch, ResearchSnapshotIdentity? KnownIdentity)
{
    public ResearchInstrument ResearchInstrument => Instrument == "BTC/USD" ? ResearchInstrument.BtcUsd : ResearchInstrument.EthUsd;
}

/// <summary>Bounded strict JSON shared by request parsing and serialized-public-buffer checks.</summary>
public static partial class ResearchSnapshotJson
{
    [GeneratedRegex(@"\A(?:0|[1-9][0-9]*)\z", RegexOptions.CultureInvariant)]
    private static partial Regex CounterPattern();
    [GeneratedRegex(@"\A1\.(?:0|[1-9][0-9]?)\z", RegexOptions.CultureInvariant)]
    private static partial Regex SchemaPattern();
    public const int RequestLimit = 4096;
    public const int ResponseLimit = 4194304;
    public static readonly string[] Collections = ["candles", "analytics", "classifications", "events", "researchUnavailableIntervals", "marketDataGapIntervals"];
    public static bool Counter(string? value, bool positive = false) => value is not null &&
        CounterPattern().IsMatch(value) &&
        long.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out var n) && (!positive || n > 0);
    public static bool Uuid(string? value) => Guid.TryParseExact(value, "D", out var g) && g != Guid.Empty && value == g.ToString("D");
    public static bool Date(string? value) => DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d) && d.Year is >= 2000 and <= 2099;
    public static bool Schema(string? value) => value is not null && SchemaPattern().IsMatch(value);
    public static ResearchSnapshotIdentity Identity(JsonElement e)
    {
        Exact(e, "generation", "version", "displayDate", "transformEpoch");
        var result = new ResearchSnapshotIdentity(Text(e, "generation"), Text(e, "version"), Text(e, "displayDate"), Text(e, "transformEpoch"));
        if (!Uuid(result.Generation) || !Counter(result.Version) || !Date(result.DisplayDate) || !Counter(result.TransformEpoch, true))
            throw new ResearchSnapshotException("INVALID_IDENTITY");
        return result;
    }
    public static ResearchSnapshotRequest Request(byte[] bytes)
    {
        try
        {
            using var doc = Parse(bytes, RequestLimit);
            var e = doc.RootElement; Exact(e, "schemaVersion", "instrument", "requestEpoch", "knownIdentity");
            var schema = Text(e, "schemaVersion"); if (!Schema(schema)) throw new ResearchSnapshotException("UNSUPPORTED_SCHEMA");
            var instrument = Text(e, "instrument"); if (instrument is not ("BTC/USD" or "ETH/USD")) throw new ResearchSnapshotException("UNSUPPORTED_INSTRUMENT");
            var epoch = Text(e, "requestEpoch"); if (!Counter(epoch)) throw new ResearchSnapshotException("INVALID_EPOCH");
            ResearchSnapshotIdentity? identity;
            try { identity = e.GetProperty("knownIdentity").ValueKind == JsonValueKind.Null ? null : Identity(e.GetProperty("knownIdentity")); }
            catch (Exception ex) when (ex is JsonException or InvalidOperationException or ResearchSnapshotException) { throw new ResearchSnapshotException("INVALID_IDENTITY"); }
            return new(schema, instrument, epoch, identity);
        }
        catch (ResearchSnapshotException) { throw; }
        catch (Exception ex) when (ex is JsonException or InvalidOperationException or DecoderFallbackException or FormatException or OverflowException)
        { throw new ResearchSnapshotException("MALFORMED_REQUEST"); }
    }
    public static void Exact(JsonElement e, params string[] keys)
    {
        if (e.ValueKind != JsonValueKind.Object || !e.EnumerateObject().Select(p => p.Name).Order().SequenceEqual(keys.Order())) throw new JsonException();
    }
    public static string Text(JsonElement e, string key) => e.GetProperty(key).GetString() ?? throw new JsonException();
    public static JsonDocument Parse(byte[] bytes, int limit = ResponseLimit)
    {
        if (bytes.Length > limit) throw new ResearchSnapshotException(limit == RequestLimit ? "REQUEST_TOO_LARGE" : "RESPONSE_TOO_LARGE");
        _ = new UTF8Encoding(false, true).GetString(bytes);
        var reader = new Utf8JsonReader(bytes, new JsonReaderOptions { MaxDepth = 8 });
        var objects = new Stack<HashSet<string>?>(); var counts = new Stack<int>(); var tokens = 0;
        while (reader.Read())
        {
            if (++tokens > 250000) throw new JsonException();
            if (reader.TokenType is JsonTokenType.StartObject or JsonTokenType.StartArray)
            {
                CountValue(); objects.Push(reader.TokenType == JsonTokenType.StartObject ? new(StringComparer.Ordinal) : null); counts.Push(0);
            }
            else if (reader.TokenType is JsonTokenType.EndObject or JsonTokenType.EndArray) { objects.Pop(); counts.Pop(); }
            else if (reader.TokenType == JsonTokenType.PropertyName)
            {
                if (reader.ValueSpan.Length > 384) throw new JsonException();
                var key = reader.GetString()!; Unicode(key);
                if (Encoding.UTF8.GetByteCount(key) > 64 || objects.Peek() is not { } set || !set.Add(key) || set.Count > 32) throw new JsonException();
            }
            else
            {
                CountValue();
                if (reader.TokenType == JsonTokenType.String)
                { if (reader.ValueSpan.Length > 1536) throw new JsonException(); var value = reader.GetString()!; Unicode(value); if (Encoding.UTF8.GetByteCount(value) > 256) throw new JsonException(); }
                if (reader.TokenType == JsonTokenType.Number)
                {
                    var lexeme = Encoding.UTF8.GetString(reader.ValueSpan);
                    if (lexeme.Length > 64) throw new JsonException();
                    var parts = lexeme.ToLowerInvariant().Split('e'); var exponent = parts.Length == 1 ? 0 : int.TryParse(parts[1], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var parsedExponent) ? parsedExponent : throw new JsonException();
                    if (exponent is < -24 or > 24) throw new JsonException();
                    var dot = parts[0].IndexOf('.'); var fraction = dot < 0 ? 0 : parts[0].Length - dot - 1;
                    var integer = BigInteger.Parse(parts[0].Replace(".", "", StringComparison.Ordinal), CultureInfo.InvariantCulture);
                    var shift = exponent - fraction;
                    if (shift >= 0 ? BigInteger.Abs(integer) * BigInteger.Pow(10, shift) > BigInteger.Pow(10, 24) :
                        BigInteger.Abs(integer) > BigInteger.Pow(10, 24 - shift)) throw new JsonException();
                }
            }
        }
        return JsonDocument.Parse(bytes, new JsonDocumentOptions { MaxDepth = 8 });
        void CountValue()
        {
            if (objects.Count == 0 || objects.Peek() is not null) return;
            var n = counts.Pop() + 1; if (n > 2048) throw new JsonException(); counts.Push(n);
        }
    }
    private static void Unicode(string value)
    {
        for (var i = 0; i < value.Length; i++)
            if (char.IsSurrogate(value[i]) && (!char.IsHighSurrogate(value[i]) || ++i >= value.Length || !char.IsLowSurrogate(value[i]))) throw new JsonException();
    }
    public static byte[] Serialize(object value)
    {
        using var stream = new BoundedStream();
        JsonSerializer.Serialize(stream, value); var bytes = stream.ToArray(); using var _ = Parse(bytes); return bytes;
    }
    private sealed class BoundedStream : MemoryStream
    {
        public override void Write(byte[] buffer, int offset, int count) { Check(count); base.Write(buffer, offset, count); }
        public override void Write(ReadOnlySpan<byte> buffer) { Check(buffer.Length); base.Write(buffer); }
        private void Check(int count) { if (Length + count > ResponseLimit) throw new ResearchSnapshotException("RESPONSE_TOO_LARGE"); }
    }
}
