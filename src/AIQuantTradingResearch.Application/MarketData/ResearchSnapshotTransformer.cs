using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AIQuantTradingResearch.Application.MarketData;

/// <summary>Only whitelisted public dictionaries leave this projection. No private calculation is recomputed.</summary>
public static partial class ResearchSnapshotTransformer
{
    [GeneratedRegex(@"\A-?(?:0|[1-9][0-9]*)\.[0-9]{12}\z", RegexOptions.CultureInvariant)]
    private static partial Regex DecimalPattern();
    public const string ResearchAlgorithm = "aiq-r115-research-v1";
    private static readonly BigInteger Scale = BigInteger.Pow(10, 12);
    public static BigInteger RoundEven(BigInteger numerator, BigInteger denominator)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(denominator);
        var n = BigInteger.Abs(numerator); var q = BigInteger.DivRem(n, denominator, out var r);
        if (2 * r > denominator || 2 * r == denominator && !q.IsEven) q++;
        return numerator.Sign < 0 ? -q : q;
    }
    public static BigInteger DailyFactor(ResearchTransformConfiguration config, DateOnly day)
    {
        if (day.Year is < 2000 or > 2099) throw new ResearchSnapshotException("UNTRUSTED_TIME");
        var message = $"{ResearchTransformConfiguration.Algorithm}\n{day.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}\n{config.KeyEpoch:D}\n";
        var digest = HMACSHA256.HashData(config.CopyKey(), Encoding.ASCII.GetBytes(message));
        return 900000000000 + new BigInteger(digest, true, true) * 200000000001 / (BigInteger.One << 256);
    }
    public static string TransformPrice(string price, BigInteger factor) => Format(RoundEven(Units(price) * factor, Scale), 4000000000 * Scale);
    public static BigInteger Units(string value)
    {
        if (!DecimalPattern().IsMatch(value) || value.Length > 64 || value == "-0.000000000000") throw new ResearchSnapshotException("DATA_UNAVAILABLE");
        var n = BigInteger.Parse(value.Replace(".", "", StringComparison.Ordinal), CultureInfo.InvariantCulture);
        if (BigInteger.Abs(n) > BigInteger.Pow(10, 36)) throw new ResearchSnapshotException("DATA_UNAVAILABLE"); return n;
    }
    private static string Format(BigInteger n, BigInteger bound)
    {
        if (BigInteger.Abs(n) > bound) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
        var a = BigInteger.Abs(n); return (n.Sign < 0 ? "-" : "") + (a / Scale).ToString(CultureInfo.InvariantCulture) + "." + (a % Scale).ToString("D12", CultureInfo.InvariantCulture);
    }
    private static string Fixed(decimal n) => n.ToString("F12", CultureInfo.InvariantCulture);
    public static string Minute(DateTimeOffset t) => t.ToString("yyyy-MM-dd'T'HH:mm:00'Z'", CultureInfo.InvariantCulture);
    public static string Observation(DateTimeOffset t) => t.ToString("yyyy-MM-dd'T'HH:mm:ss.ffffff'Z'", CultureInfo.InvariantCulture);
    public static Dictionary<string, object?> Map(params (string Key, object? Value)[] pairs) => pairs.ToDictionary(x => x.Key, x => x.Value);
    public static byte[] Project(ResearchCommittedState state, ResearchTransformConfiguration config, ResearchSnapshotRequest request, DateTimeOffset now)
    {
        if (state.Generation == Guid.Empty || state.Generation != config.Generation || state.Instrument != request.ResearchInstrument ||
            !Enum.IsDefined(state.Integrity) || state.Integrity == ResearchIntegrity.Recovery || state.Version < 0 || state.Candles.Count > 1000 ||
            now.Offset != TimeSpan.Zero || now.Year is < 2000 or > 2099) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
        now = now.AddTicks(-(now.Ticks % 10)); // Wire observations have exactly microsecond precision.
        var day = DateOnly.FromDateTime(now.UtcDateTime); var factor = DailyFactor(config, day);
        var identity = new ResearchSnapshotIdentity(state.Generation.ToString("D"), state.Version.ToString(CultureInfo.InvariantCulture), day.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture), config.TransformEpoch.ToString(CultureInfo.InvariantCulture));
        if (request.KnownIdentity is { } known && known.Generation == identity.Generation &&
            (string.CompareOrdinal(known.DisplayDate, identity.DisplayDate) > 0 || long.Parse(known.TransformEpoch, CultureInfo.InvariantCulture) > config.TransformEpoch))
            throw new ResearchSnapshotException("UNTRUSTED_TIME");
        if (request.KnownIdentity is { } versionKnown && versionKnown.Generation == identity.Generation &&
            long.Parse(versionKnown.Version, CultureInfo.InvariantCulture) > state.Version) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
        var candles = new List<object>(); var analytics = new List<object>(); var classifications = new List<object>(); var events = new List<object>();
        var unavailable = new List<(DateTimeOffset Start, DateTimeOffset End)>(); var gaps = new List<(DateTimeOffset Start, DateTimeOffset End)>();
        ResearchStoredCandle? previous = null; DateTimeOffset? validAsOf = null;
        foreach (var row in state.Candles)
        {
            var c = row.Candle; var time = c.OpenTimeUtc; var calc = row.Calculation;
            if (c.Instrument != state.Instrument || row.Segment == Guid.Empty || row.SegmentIndex < 1 || time.AddMinutes(1) > now ||
                previous is not null && (time <= previous.Candle.OpenTimeUtc ||
                    row.Segment == previous.Segment && (row.SegmentIndex != previous.SegmentIndex + 1 || time != previous.Candle.OpenTimeUtc.AddMinutes(1)) ||
                    row.Segment != previous.Segment && row.SegmentIndex != 1)) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
            if (previous is not null && time > previous.Candle.OpenTimeUtc.AddMinutes(1)) gaps.Add((previous.Candle.OpenTimeUtc.AddMinutes(1), time));
            var t = Minute(time); var seg = row.Segment.ToString("D");
            var prices = new[] { c.Open, c.High, c.Low, c.Close }.Select(x => TransformPrice(Fixed(x), factor)).ToArray();
            if (prices.Any(x => Units(x) <= 0)) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
            candles.Add(Map(("time", t), ("segment", seg), ("segmentIndex", row.SegmentIndex.ToString(CultureInfo.InvariantCulture)),
                ("open", prices[0]), ("high", prices[1]), ("low", prices[2]), ("close", prices[3]), ("volume", Fixed(c.Volume))));
            var a = calc?.Indicators;
            var fields = new[] { "sma20", "ema20", "bollingerLower", "bollingerMiddle", "bollingerUpper", "rsi14", "macd", "macdSignal", "macdHistogram", "atr14", "normalizedAtr14", "bollingerWidth", "emaSlope5" };
            string?[] values = [a?.Sma20, a?.Ema20, a?.BollingerLower, a?.BollingerMiddle, a?.BollingerUpper, a?.Rsi14, a?.Macd, a?.MacdSignal, a?.MacdHistogram, a?.Atr14, a?.NormalizedAtr14, a?.BollingerWidth, a?.EmaSlope5];
            var output = Map(("time", t));
            for (var i = 0; i < fields.Length; i++)
            {
                var value = values[i]; if (value is not null)
                {
                    var units = Units(value);
                    if (i == 5 && (units < 0 || units > 100 * Scale)) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
                    if (i != 5 && i < 10) value = TransformPrice(value, factor);
                }
                output.Add(fields[i], value);
            }
            analytics.Add(output);
            var valid = calc?.Classification.Valid == true;
            var cl = calc?.Classification;
            if (valid && (row.SegmentIndex < 401 || values.Any(x => x is null) || cl!.TrendScore is < -3 or > 3 or null ||
                cl.Trend != (cl.TrendScore >= 2 ? "Bullish" : cl.TrendScore <= -2 ? "Bearish" : "Neutral") ||
                cl.Regime is not ("Volatile" or "Trending" or "Ranging" or "Undetermined"))) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
            if (valid) validAsOf = time; else AddUnavailable(time);
            classifications.Add(Map(("time", t), ("segment", seg), ("validity", valid ? "Valid" : "Unavailable"),
                ("trendScore", valid ? cl!.TrendScore : null), ("trend", valid ? cl!.Trend : null), ("regime", valid ? cl!.Regime : null)));
            if (calc?.SegmentStart is { } startEvent && (startEvent.Type != "SegmentStart" || row.SegmentIndex != 1) ||
                calc?.TrendTransition is { } trendEvent && trendEvent.Type != "TrendTransition") throw new ResearchSnapshotException("DATA_UNAVAILABLE");
            foreach (var e in new[] { calc?.SegmentStart, calc?.TrendTransition }.Where(x => x is not null))
            {
                if (e!.Time != time || e.Segment != row.Segment || e.Type is not ("SegmentStart" or "TrendTransition") ||
                    e.Type == "SegmentStart" && (e.PreviousTrend is not null || e.Trend is not null || e.Regime is not null) ||
                    e.Type == "TrendTransition" && (!valid || e.Trend != cl!.Trend || e.Regime != cl.Regime || e.PreviousTrend is not (null or "Bullish" or "Bearish" or "Neutral"))) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
                events.Add(Map(("time", t), ("segment", seg), ("type", e.Type), ("previousTrend", e.PreviousTrend), ("trend", e.Trend), ("regime", e.Regime)));
            }
            previous = row;
        }
        if (state.LastCommitAt > now || state.LastSuccessfulProviderPollAt > now ||
            state.Candles.Count == 0 && (state.Version != 0 || state.LastCommitAt is not null || state.Integrity != ResearchIntegrity.Protected) ||
            state.Candles.Count > 0 && (state.Version == 0 || state.LastCommitAt is null || state.LastSuccessfulProviderPollAt is null || state.LastSuccessfulProviderPollAt < previous!.Candle.OpenTimeUtc.AddMinutes(1))) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
        var last = previous?.Candle.OpenTimeUtc;
        var status = Map(("observedAt", Observation(now)), ("research", state.ResearchStatus),
            ("freshness", last is null ? "Unavailable" : now - last.Value.AddMinutes(1) <= TimeSpan.FromSeconds(120) ? "Current" : "Stale"),
            ("integrity", state.Integrity.ToString()), ("classificationValidity", previous?.Calculation?.Classification.Valid == true ? "Valid" : "Unavailable"),
            ("lastCompletedCandleAt", last is null ? null : Minute(last.Value)),
            ("lastSuccessfulProviderPollAt", state.LastSuccessfulProviderPollAt is null ? null : Observation(state.LastSuccessfulProviderPollAt.Value)),
            ("lastCommitAt", state.LastCommitAt is null ? null : Observation(state.LastCommitAt.Value)),
            ("researchAsOfCandleAt", validAsOf is null ? null : Minute(validAsOf.Value)));
        var unchanged = request.KnownIdentity == identity;
        var result = Map(("schemaVersion", "1.0"), ("type", unchanged ? "unchanged" : "full"), ("instrument", request.Instrument),
            ("requestEpoch", request.RequestEpoch), ("observedAt", Observation(now)),
            ("identity", Map(("generation", identity.Generation), ("version", identity.Version), ("displayDate", identity.DisplayDate), ("transformEpoch", identity.TransformEpoch))),
            ("contract", Map(("researchAlgorithm", ResearchAlgorithm), ("transformAlgorithm", ResearchTransformConfiguration.Algorithm), ("intervalSeconds", 60))), ("status", status));
        if (!unchanged)
        {
            result.Add("candles", candles); result.Add("analytics", analytics); result.Add("classifications", classifications); result.Add("events", events);
            result.Add("researchUnavailableIntervals", unavailable.Select(x => Map(("start", Minute(x.Start)), ("end", Minute(x.End)))).ToArray());
            result.Add("marketDataGapIntervals", gaps.Select(x => Map(("start", Minute(x.Start)), ("end", Minute(x.End)))).ToArray());
        }
        return ResearchSnapshotJson.Serialize(result);
        void AddUnavailable(DateTimeOffset time)
        {
            if (unavailable.Count > 0 && unavailable[^1].End == time) unavailable[^1] = (unavailable[^1].Start, time.AddMinutes(1));
            else unavailable.Add((time, time.AddMinutes(1)));
        }
    }
}
