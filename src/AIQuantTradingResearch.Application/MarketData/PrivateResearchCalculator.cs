using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AIQuantTradingResearch.Application.MarketData;

/// <summary>Private aiq-r115-research-v1 integer engine. No network, storage writer or transformed input.</summary>
public static class PrivateResearchCalculator
{
    public const string Algorithm = "aiq-r115-research-v1";
    private static readonly BigInteger Scale = BigInteger.Pow(10, 12);
    private static readonly JsonSerializerOptions Json = Options();
    private static JsonSerializerOptions Options()
    {
        var options = new JsonSerializerOptions { MaxDepth = 12 };
        options.Converters.Add(new IntegerConverter());return options;
    }
    private sealed class IntegerConverter : JsonConverter<BigInteger>
    {
        public override BigInteger Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var text = reader.TokenType == JsonTokenType.String ? reader.GetString() : null;
            if (text is null || text.Length is < 1 or > 40 || !BigInteger.TryParse(text, System.Globalization.NumberStyles.AllowLeadingSign,
                System.Globalization.CultureInfo.InvariantCulture, out var value) || value.ToString(System.Globalization.CultureInfo.InvariantCulture) != text)
                throw new JsonException("Invalid private integer.");
            return value;
        }
        public override void Write(Utf8JsonWriter writer, BigInteger value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }
    private sealed class Feature
    {
        public long Index { get; set; }
        public DateTimeOffset Time { get; set; }
        public BigInteger Atr { get; set; }
        public BigInteger Width { get; set; }
        public BigInteger Slope { get; set; }
    }
    private sealed class State
    {
        public Guid Segment { get; set; }
        public long Index { get; set; }
        public DateTimeOffset LastTime { get; set; }
        public BigInteger PreviousClose { get; set; }
        public List<BigInteger> Closes { get; set; } = [];
        public BigInteger Sum12 { get; set; }
        public BigInteger Sum20 { get; set; }
        public BigInteger Sum26 { get; set; }
        public BigInteger E12 { get; set; }
        public BigInteger E20 { get; set; }
        public BigInteger E26 { get; set; }
        public BigInteger GainSum { get; set; }
        public BigInteger LossSum { get; set; }
        public BigInteger Gain { get; set; }
        public BigInteger Loss { get; set; }
        public BigInteger TrSum { get; set; }
        public BigInteger Atr { get; set; }
        public BigInteger SignalSum { get; set; }
        public BigInteger Signal { get; set; }
        public List<BigInteger> EmaHistory { get; set; } = [];
        public List<Feature> Features { get; set; } = [];
        public string? LastTrend { get; set; }
    }
    private sealed class Frame
    {
        public State? Before { get; set; }
        public State? After { get; set; }
    }
    private static State? Copy(State? state) => state is null ? null : JsonSerializer.Deserialize<State>(JsonSerializer.Serialize(state, Json), Json)!;
    private static void Validate(State? state)
    {
        if (state is null) return;
        if (state.Index < 1 || state.Segment == Guid.Empty || state.LastTime.Offset != TimeSpan.Zero || state.LastTime.Ticks % TimeSpan.TicksPerMinute != 0 ||
            state.PreviousClose <= 0 || state.PreviousClose > 1_000_000_000 * Scale || state.Closes is null || state.EmaHistory is null || state.Features is null ||
            state.Closes.Count != Math.Min(20, state.Index) || state.Closes.Any(x => x <= 0 || x > 1_000_000_000 * Scale) ||
            state.EmaHistory.Count != Math.Min(6, Math.Max(0, state.Index - 19)) ||
            state.Features.Count != Math.Min(300, Math.Max(0, state.Index - 100)) || state.Closes[^1] != state.PreviousClose ||
            state.EmaHistory.Any(x => x <= 0 || x > 1_000_000_000 * Scale) ||
            new[] { state.Gain, state.Loss, state.Atr }.Any(x => x < 0 || x > 1_000_000_000 * Scale) ||
            state.LastTrend is not (null or "Bullish" or "Bearish" or "Neutral")) throw new ResearchUnavailableException();
        for (var i = 0; i < state.Features.Count; i++)
        {
            var f = state.Features[i];var index = state.Index - state.Features.Count + 1 + i;
            if (f is null || f.Index != index || f.Time != state.LastTime.AddMinutes(index - state.Index) ||
                BigInteger.Abs(f.Atr) > BigInteger.Pow(10, 36) || BigInteger.Abs(f.Width) > BigInteger.Pow(10, 36) || BigInteger.Abs(f.Slope) > BigInteger.Pow(10, 36)) throw new ResearchUnavailableException();
        }
    }
    private static Frame ReadFrame(ResearchCalculationCheckpoint checkpoint)
    {
        if (checkpoint.State.Length > 262144) throw new ResearchUnavailableException();
        try
        {
            using var document = JsonDocument.Parse(checkpoint.State, new JsonDocumentOptions { MaxDepth = 12 });
            if (document.RootElement.ValueKind != JsonValueKind.Object ||
                (document.RootElement.EnumerateObject().Count() != 2 || !document.RootElement.TryGetProperty("Before", out _) || !document.RootElement.TryGetProperty("After", out _))) throw new ResearchUnavailableException();
            var frame = JsonSerializer.Deserialize<Frame>(checkpoint.State, Json) ?? throw new ResearchUnavailableException();
            Validate(frame.Before);Validate(frame.After);
            if (frame.Before is not null && frame.Before.LastTime >= checkpoint.FirstRetainedTime ||
                frame.After is not null && frame.After.LastTime != checkpoint.LastTime) throw new ResearchUnavailableException();
            return frame;
        }
        catch (Exception e) when (e is JsonException or ArgumentException or InvalidOperationException) { throw new ResearchUnavailableException(); }
    }
    public static void ValidateCheckpoint(ResearchCalculationCheckpoint checkpoint, Guid generation, ResearchInstrument instrument, long version,
        DateTimeOffset firstTime, DateTimeOffset lastTime)
    {
        if (checkpoint.Generation != generation || checkpoint.Instrument != instrument || checkpoint.Version != version ||
            checkpoint.FirstRetainedTime != firstTime || checkpoint.LastTime != lastTime) throw new ResearchUnavailableException();
        _ = ReadFrame(checkpoint);
    }
    public static ResearchCalculatedProjection Project(ResearchCommittedState candidate, ResearchCalculationCheckpoint? previousCheckpoint = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(candidate);cancellationToken.ThrowIfCancellationRequested();
        if (candidate.Generation == Guid.Empty || candidate.Version < 1 || !Enum.IsDefined(candidate.Instrument) ||
            candidate.Integrity == ResearchIntegrity.Recovery || candidate.Candles.Count is < 1 or > 2000) throw new ResearchUnavailableException();
        State? state = null;
        if (previousCheckpoint is not null)
        {
            if (previousCheckpoint.Generation != candidate.Generation || previousCheckpoint.Instrument != candidate.Instrument ||
                previousCheckpoint.Version != candidate.Version - 1 || previousCheckpoint.FirstRetainedTime != candidate.Candles[0].Candle.OpenTimeUtc)
                throw new ResearchUnavailableException();
            state = ReadFrame(previousCheckpoint).Before;
        }
        var result = new List<ResearchStoredCandle>();var retainedStart = Math.Max(0, candidate.Candles.Count - 1000);State? before = null;
        ResearchStoredCandle? prior = null;
        for (var i = 0; i < candidate.Candles.Count; i++)
        {
            cancellationToken.ThrowIfCancellationRequested();var row = candidate.Candles[i];
            if (row is null || row.Candle is null || row.Candle.Instrument != candidate.Instrument || row.Segment == Guid.Empty || row.SegmentIndex < 1 ||
                candidate.LastSuccessfulProviderPollAt is null || row.Candle.OpenTimeUtc.AddMinutes(1) > candidate.LastSuccessfulProviderPollAt ||
                prior is not null && (row.Candle.OpenTimeUtc <= prior.Candle.OpenTimeUtc ||
                    row.Segment == prior.Segment && (row.SegmentIndex != prior.SegmentIndex + 1 || row.Candle.OpenTimeUtc != prior.Candle.OpenTimeUtc.AddMinutes(1)) ||
                    row.Segment != prior.Segment && row.SegmentIndex != 1)) throw new ResearchUnavailableException();
            if (i == retainedStart) before = Copy(state);
            if (row.SegmentIndex == 1) state = new State { Segment = row.Segment };
            else if (state is not null && (state.Segment != row.Segment || state.Index + 1 != row.SegmentIndex || state.LastTime.AddMinutes(1) != row.Candle.OpenTimeUtc))
                throw new ResearchUnavailableException();
            // No segment-origin/checkpoint evidence means no reseeding from a truncated visible window.
            ResearchCalculation? calculation = state is null ? null : Advance(state, row);
            if (i >= retainedStart) result.Add(row with { Calculation = calculation });
            prior = row;
        }
        var checkpoint = new ResearchCalculationCheckpoint(candidate.Generation, candidate.Instrument, candidate.Version,
            result[0].Candle.OpenTimeUtc, result[^1].Candle.OpenTimeUtc, JsonSerializer.Serialize(new Frame { Before = before, After = state }, Json));
        if (checkpoint.State.Length > 262144) throw new ResearchUnavailableException();
        return new ResearchCalculatedProjection(candidate.Generation, candidate.Instrument, candidate.Version, result, checkpoint);
    }
    internal static BigInteger Round(BigInteger numerator, BigInteger denominator)
    {
        if (denominator <= 0) throw new ResearchUnavailableException();
        var q = BigInteger.DivRem(BigInteger.Abs(numerator), denominator, out var r);
        var comparison = (2 * r).CompareTo(denominator);
        if (comparison > 0 || comparison == 0 && !q.IsEven) q++;
        return numerator.Sign * q;
    }
    private static string? Number(BigInteger? value)
    {
        if (value is null) return null;
        var magnitude = BigInteger.Abs(value.Value);var whole = BigInteger.DivRem(magnitude, Scale, out var fraction);
        return (value.Value.Sign < 0 ? "-" : "") + whole.ToString(System.Globalization.CultureInfo.InvariantCulture) + "." + fraction.ToString("D12", System.Globalization.CultureInfo.InvariantCulture);
    }
    private static BigInteger Units(decimal value) => new(value * 1_000_000_000_000m);
    private static BigInteger Ema(BigInteger p, int n, long t, ref BigInteger sum, BigInteger old)
    {
        if (t <= n) { sum += p;return t == n ? Round(sum, n) : 0; }
        return Round(2 * p + (n - 1) * old, n + 1);
    }
    private static BigInteger Deviation(IEnumerable<BigInteger> prices)
    {
        var values = prices.ToArray();var sum = values.Aggregate(BigInteger.Zero, (a, b) => a + b);
        var a = 20 * values.Aggregate(BigInteger.Zero, (acc, x) => acc + x * x) - sum * sum;
        BigInteger low = 0, high = BigInteger.Max(1, values.Max());
        while (low < high) { var mid = (low + high + 1) / 2;if (400 * mid * mid <= a) low = mid;else high = mid - 1; }
        var compare = (4 * a).CompareTo(400 * BigInteger.Pow(2 * low + 1, 2));
        return compare > 0 || compare == 0 && !low.IsEven ? low + 1 : low;
    }
    private static BigInteger Percentile(IEnumerable<BigInteger> values, int q)
    {
        var sorted = values.Order().ToArray();if (sorted.Length != 300) throw new ResearchUnavailableException();
        var z = 299 * q;var j = z / 100;var remainder = z % 100;
        return Round((100 - remainder) * sorted[j] + remainder * sorted[j + 1], 100);
    }
    private static ResearchCalculation Advance(State s, ResearchStoredCandle row)
    {
        var t = checked(s.Index + 1);if (t != row.SegmentIndex) throw new ResearchUnavailableException();
        var candle = row.Candle;var p = Units(candle.Close);var high = Units(candle.High);var low = Units(candle.Low);
        var change = t == 1 ? BigInteger.Zero : p - s.PreviousClose;
        var gain = BigInteger.Max(0, change);var loss = BigInteger.Max(0, -change);
        var tr = t == 1 ? high - low : BigInteger.Max(high - low, BigInteger.Max(BigInteger.Abs(high - s.PreviousClose), BigInteger.Abs(low - s.PreviousClose)));
        s.Closes.Add(p);if (s.Closes.Count > 20) s.Closes.RemoveAt(0);
        // Sums are properties in the serialized state; use locals for ref updates.
        var sum12 = s.Sum12;var sum20 = s.Sum20;var sum26 = s.Sum26;
        s.E12 = Ema(p, 12, t, ref sum12, s.E12);s.E20 = Ema(p, 20, t, ref sum20, s.E20);s.E26 = Ema(p, 26, t, ref sum26, s.E26);
        s.Sum12 = sum12;s.Sum20 = sum20;s.Sum26 = sum26;
        if (t <= 14) s.TrSum += tr;
        if (t == 14) s.Atr = Round(s.TrSum, 14);else if (t > 14) s.Atr = Round(13 * s.Atr + tr, 14);
        if (t is >= 2 and <= 15) { s.GainSum += gain;s.LossSum += loss; }
        if (t == 15) { s.Gain = Round(s.GainSum, 14);s.Loss = Round(s.LossSum, 14); }
        else if (t > 15) { s.Gain = Round(13 * s.Gain + gain, 14);s.Loss = Round(13 * s.Loss + loss, 14); }
        BigInteger? sma = null, bbLow = null, bbHigh = null, rsi = null, macd = null, signal = null, histogram = null, norm = null, width = null, slope = null;
        if (t >= 20) { sma = Round(s.Closes.Aggregate(BigInteger.Zero, (a, b) => a + b), 20);var sd = Deviation(s.Closes);bbLow = sma - 2 * sd;bbHigh = sma + 2 * sd;width = Round((bbHigh.Value - bbLow.Value) * Scale, sma.Value); }
        if (t >= 15) rsi = s.Gain + s.Loss == 0 ? 50 * Scale : Round(100 * Scale * s.Gain, s.Gain + s.Loss);
        if (t >= 14) norm = Round(s.Atr * Scale, p);
        if (t >= 26)
        {
            macd = s.E12 - s.E26;if (t <= 34) s.SignalSum += macd.Value;
            if (t == 34) s.Signal = Round(s.SignalSum, 9);else if (t > 34) s.Signal = Round(2 * macd.Value + 8 * s.Signal, 10);
            if (t >= 34) { signal = s.Signal;histogram = macd - signal; }
        }
        if (t >= 25) slope = Round((s.E20 - s.EmaHistory[^5]) * Scale, 5 * s.EmaHistory[^5]);
        if (t >= 20) { s.EmaHistory.Add(s.E20);if (s.EmaHistory.Count > 6) s.EmaHistory.RemoveAt(0); }
        foreach (var feature in new[] { norm, width, slope }) if (feature is not null && BigInteger.Abs(feature.Value) > BigInteger.Pow(10, 36)) throw new ResearchUnavailableException();
        int? score = null;string? trend = null, regime = null;
        if (t >= 401)
        {
            if (s.Features.Count != 300) throw new ResearchUnavailableException();
            score = p.CompareTo(s.E20) + histogram!.Value.Sign + (rsi > 55 * Scale ? 1 : rsi < 45 * Scale ? -1 : 0);
            trend = score >= 2 ? "Bullish" : score <= -2 ? "Bearish" : "Neutral";
            regime = norm > Percentile(s.Features.Select(x => x.Atr), 80) && width > Percentile(s.Features.Select(x => x.Width), 80) ? "Volatile" :
                Math.Abs(score.Value) >= 2 && BigInteger.Abs(slope!.Value) > Percentile(s.Features.Select(x => BigInteger.Abs(x.Slope)), 70) && Math.Sign(score.Value) == slope.Value.Sign ? "Trending" :
                Math.Abs(score.Value) <= 1 && BigInteger.Abs(slope!.Value) < Percentile(s.Features.Select(x => BigInteger.Abs(x.Slope)), 40) ? "Ranging" : "Undetermined";
        }
        var start = t == 1 ? new ResearchTransitionEvent(candle.OpenTimeUtc, row.Segment, "SegmentStart", null, null, null) : null;
        var transition = trend is not null && trend != s.LastTrend ? new ResearchTransitionEvent(candle.OpenTimeUtc, row.Segment, "TrendTransition", s.LastTrend, trend, regime) : null;
        if (trend is not null) s.LastTrend = trend;
        if (t >= 101)
        {
            s.Features.Add(new Feature { Index = t, Time = candle.OpenTimeUtc, Atr = norm!.Value, Width = width!.Value, Slope = slope!.Value });
            if (s.Features.Count > 300) s.Features.RemoveAt(0);
        }
        s.Index = t;s.LastTime = candle.OpenTimeUtc;s.PreviousClose = p;
        return new ResearchCalculation(new ResearchIndicators(Number(sma), t >= 20 ? Number(s.E20) : null, Number(bbLow), Number(sma), Number(bbHigh), Number(rsi), Number(macd), Number(signal), Number(histogram), t >= 14 ? Number(s.Atr) : null, Number(norm), Number(width), Number(slope)),
            new ResearchClassification(trend is not null, score, trend, regime), start, transition);
    }
}
