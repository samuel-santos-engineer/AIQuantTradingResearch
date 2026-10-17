using System.Net;
using System.Globalization;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Synthetic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Worker;

/// <summary>Explicit Windows loopback data source for the existing Streamlit development UI.</summary>
public static class LocalSyntheticChart
{
    public const string Address = "http://127.0.0.1:8766";
    public const string Label = "BTC-like Market Simulation — Synthetic Historical OHLCV";
    public static bool IsAllowed(string[] args, IReadOnlyDictionary<string, string?> environment, bool windows) =>
        windows && args.SequenceEqual(new[] { "--local-synthetic-chart" }) &&
        environment.TryGetValue("DOTNET_ENVIRONMENT", out var mode) && mode == "Development" &&
        !environment.Keys.Any(key => key.StartsWith("WEBSITE", StringComparison.OrdinalIgnoreCase));

    public static async Task<int> RunAsync(string[] args, TextWriter error)
    {
        var environment = Environment.GetEnvironmentVariables().Keys.Cast<string>()
            .ToDictionary(key => key, Environment.GetEnvironmentVariable, StringComparer.OrdinalIgnoreCase);
        if (!IsAllowed(args, environment, OperatingSystem.IsWindows()))
        {
            await error.WriteLineAsync("Local source requires Windows, the exact local command, Development mode and no Azure App Service environment.");
            return 1;
        }
        // No provider configuration, persistence, public bridge or normal Worker DI.
        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
        {
            Args = [], EnvironmentName = "Development", ApplicationName = typeof(LocalSyntheticChart).Assembly.FullName,
            ContentRootPath = AppContext.BaseDirectory,
        });
        builder.WebHost.UseKestrel(options => options.Listen(IPAddress.Loopback, 8766));
        builder.Services.AddRouting();
        await using var app = builder.Build();
        app.Use(async (context, next) =>
        {
            if (context.Connection.RemoteIpAddress is not { } peer || !IPAddress.IsLoopback(peer) ||
                context.Request.Host.Value != "127.0.0.1:8766")
            { context.Response.StatusCode = 403; return; }
            context.Response.Headers.CacheControl = "no-store";
            context.Response.Headers.XContentTypeOptions = "nosniff";
            await next(context);
        });
        app.MapGet("/local-chart/data", async (HttpContext context) =>
        {
            var query = context.Request.Query;
            if (query.Count != 2 || query["interval"].Count != 1 || query["range"].Count != 1 ||
                !TrySelection(query["interval"].ToString(), query["range"].ToString(), out var interval, out var range))
                return (IResult)Results.BadRequest(new { code = "INVALID_LOCAL_SELECTION" });
            using var deadline = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted);
            deadline.CancelAfter(TimeSpan.FromSeconds(10));
            return Results.Json(await CreatePayloadAsync(interval, range, deadline.Token));
        });
        foreach (var route in new[] { "dataset", "candles", "indicators" })
            app.MapGet("/api/v1/" + route, () => Results.Json(new { state = "unavailable", code = "DATASET_NOT_READY", data = (object?)null }, statusCode: 503));
        await error.WriteLineAsync($"Local Streamlit data source: {Address}/local-chart/data — PUBLIC_RUNTIME_VERIFIED=False.");
        await app.RunAsync();
        return 0;
    }

    public static bool TrySelection(string intervalText, string rangeText, out HistoricalCandleInterval interval, out HistoricalCandleRange range)
    {
        interval = intervalText switch { "1h" => HistoricalCandleInterval.OneHour, "4h" => HistoricalCandleInterval.FourHours,
            "1d" => HistoricalCandleInterval.OneDay, _ => (HistoricalCandleInterval)(-1) };
        range = rangeText switch { "1D" => HistoricalCandleRange.OneDay, "7D" => HistoricalCandleRange.SevenDays,
            "30D" => HistoricalCandleRange.ThirtyDays, "90D" => HistoricalCandleRange.NinetyDays, _ => (HistoricalCandleRange)(-1) };
        return Enum.IsDefined(interval) && Enum.IsDefined(range);
    }

    public sealed record Row(string TimeUtc, string Open, string High, string Low, string Close, string Volume, string Sma20, string Ema20,
        string BollingerLower, string BollingerMiddle, string BollingerUpper,
        string Rsi14, string Macd, string MacdSignal, string MacdHistogram, string Atr14);
    public sealed record Payload(string Label, string Instrument, string Interval, string Range, string CoverageStartUtc,
        string EndExclusiveUtc, string DatasetFingerprint, string WindowFingerprint, string CalculationVersion,
        string AtrCalculationVersion, string CalculationStartUtc, int Warmup, int CalculationCount, bool PublicRuntimeVerified, IReadOnlyList<Row> Candles);

    public static async Task<Payload> CreatePayloadAsync(HistoricalCandleInterval interval, HistoricalCandleRange range,
        CancellationToken cancellationToken = default)
    {
        var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation, interval, range, TimeSpan.FromSeconds(10));
        var result = await new SyntheticHistoricalMarketDataProvider().GetHistoricalCandlesAsync(request, cancellationToken);
        if (!result.IsSuccess || result.Candles is null || result.Provenance is null)
            throw new InvalidOperationException("Local synthetic input unavailable.");
        SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy().Validate(request, result.Candles, result.Provenance);
        var analytics = TechnicalIndicatorCalculator.CalculatePublic(request, result.Candles, result.Provenance, cancellationToken);
        var atr = WilderAverageTrueRangeCalculator.CalculatePublic(request, result.Candles, result.Provenance, cancellationToken);
        var rows = result.Candles.Skip(250).Select((c, i) => new Row(SyntheticPublicationPolicy.Timestamp(c.OpenTimeUtc),
            Number(c.Open, 4), Number(c.High, 4), Number(c.Low, 4), Number(c.Close, 4), Number(c.Volume, 3),
            Number(analytics.Rows[i + 250].Sma20 ?? throw new InvalidOperationException("Missing warmed SMA.")),
            Number(analytics.Rows[i + 250].Ema20 ?? throw new InvalidOperationException("Missing warmed EMA.")),
            Number(analytics.Rows[i + 250].BollingerLower ?? throw new InvalidOperationException("Missing warmed bands.")),
            Number(analytics.Rows[i + 250].BollingerMiddle ?? throw new InvalidOperationException("Missing warmed bands.")),
            Number(analytics.Rows[i + 250].BollingerUpper ?? throw new InvalidOperationException("Missing warmed bands.")),
            Number(analytics.Rows[i + 250].Rsi14 ?? throw new InvalidOperationException("Missing warmed RSI.")),
            Number(analytics.Rows[i + 250].Macd ?? throw new InvalidOperationException("Missing warmed MACD.")),
            Number(analytics.Rows[i + 250].MacdSignal ?? throw new InvalidOperationException("Missing warmed MACD signal.")),
            Number(analytics.Rows[i + 250].MacdHistogram ?? throw new InvalidOperationException("Missing warmed MACD histogram.")),
            Number(atr[i + 250] ?? throw new InvalidOperationException("Missing warmed ATR.")))).ToArray();
        if (rows.Length != SyntheticPublicationPolicy.VisibleCount(request)) throw new InvalidOperationException("Invalid local visible window.");
        return new(Label, "btc-like-simulation", SyntheticPublicationPolicy.IntervalLiteral(interval), SyntheticPublicationPolicy.RangeLiteral(range),
            "2025-10-26T00:00:00Z", "2026-10-01T00:00:00Z", result.Provenance.SyntheticIdentity!.DatasetFingerprint,
            result.Provenance.SyntheticIdentity.WindowFingerprint, "aiq-ta-v1", WilderAverageTrueRangeCalculator.Version, SyntheticPublicationPolicy.Timestamp(result.Candles[0].OpenTimeUtc),
            250, result.Candles.Count, false, rows);
    }
    private static string Number(decimal value, int digits = 12) => value.ToString("F" + digits, CultureInfo.InvariantCulture);
}
