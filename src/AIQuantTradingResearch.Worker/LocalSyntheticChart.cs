using System.Globalization;
using System.Net;
using System.Text;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.Synthetic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Worker;

/// <summary>Explicit, Windows-only loopback development page. Not public runtime composition.</summary>
public static class LocalSyntheticChart
{
    public const string Address = "http://127.0.0.1:8765";
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
            await error.WriteLineAsync("Local chart requires Windows, the exact local command, Development mode and no Azure App Service environment.");
            return 1;
        }

        // Empty builder deliberately excludes appsettings, provider credentials, DB/Worker DI,
        // environment URL overrides and the deployed Streamlit/public composition.
        var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
        {
            Args = [], EnvironmentName = "Development", ApplicationName = typeof(LocalSyntheticChart).Assembly.FullName,
            ContentRootPath = AppContext.BaseDirectory,
        });
        builder.WebHost.UseKestrel(options => options.Listen(IPAddress.Loopback, 8765));
        builder.Services.AddRouting();
        await using var app = builder.Build();
        using var budget = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        var payload = await CreatePayloadAsync(budget.Token);
        var page = Render(payload);
        app.Use(async (context, next) =>
        {
            // Loopback binding plus exact Host/peer check; no LAN access/DNS-rebinding host.
            if (context.Connection.RemoteIpAddress is not { } peer || !IPAddress.IsLoopback(peer) ||
                context.Request.Host.Value != "127.0.0.1:8765")
            { context.Response.StatusCode = 403; return; }
            context.Response.Headers.CacheControl = "no-store";
            context.Response.Headers.ContentSecurityPolicy = "default-src 'none'; style-src 'unsafe-inline'; img-src 'self'; frame-ancestors 'none'; base-uri 'none'";
            context.Response.Headers.XContentTypeOptions = "nosniff";
            await next(context);
        });
        app.MapGet("/", (HttpContext context) => context.Request.QueryString.HasValue
            ? Results.BadRequest() : Results.Content(page, "text/html; charset=utf-8"));
        app.MapGet("/local-chart/data", (HttpContext context) => context.Request.QueryString.HasValue
            ? Results.BadRequest() : Results.Json(payload));
        foreach (var route in new[] { "dataset", "candles", "indicators" })
            app.MapGet("/api/v1/" + route, () => Results.Json(new { state = "unavailable", code = "DATASET_NOT_READY", data = (object?)null }, statusCode: 503));
        await error.WriteLineAsync($"Local-only chart: {Address} — Ctrl+C to stop. PUBLIC_RUNTIME_VERIFIED=False.");
        await app.RunAsync();
        return 0;
    }

    public sealed record Row(string TimeUtc, string Open, string High, string Low, string Close, string Sma20);
    public sealed record Payload(string Label, string Interval, string Range, string EndExclusiveUtc,
        bool PublicRuntimeVerified, IReadOnlyList<Row> Candles);

    public static async Task<Payload> CreatePayloadAsync(CancellationToken cancellationToken = default)
    {
        var request = new HistoricalMarketDataRequest(CanonicalMarketSymbol.BtcLikeSimulation,
            HistoricalCandleInterval.OneHour, HistoricalCandleRange.OneDay, TimeSpan.FromSeconds(10));
        var result = await new SyntheticHistoricalMarketDataProvider().GetHistoricalCandlesAsync(request, cancellationToken);
        if (!result.IsSuccess || result.Candles is null || result.Provenance is null)
            throw new InvalidOperationException("Local synthetic input unavailable.");
        SyntheticHistoricalMarketDataProvider.CreatePublicationPolicy().Validate(request, result.Candles, result.Provenance);
        var analytics = TechnicalIndicatorCalculator.CalculatePublic(request, result.Candles, result.Provenance, cancellationToken);
        // Calculation uses all 274 rows. The only page/JSON projection is the final 24.
        var rows = result.Candles.Skip(250).Select((c, i) => new Row(
            SyntheticPublicationPolicy.Timestamp(c.OpenTimeUtc), Number(c.Open), Number(c.High), Number(c.Low), Number(c.Close),
            Number(analytics.Rows[i + 250].Sma20 ?? throw new InvalidOperationException("Missing warmed SMA.")))).ToArray();
        if (rows.Length != 24) throw new InvalidOperationException("Invalid local visible window.");
        return new(Label, "1h", "1D", "2026-10-01T00:00:00Z", false, rows);
    }

    private static string Number(decimal value) => value.ToString("F12", CultureInfo.InvariantCulture);

    public static string Render(Payload payload)
    {
        if (payload.Candles.Count != 24 || payload.Interval != "1h" || payload.Range != "1D" ||
            payload.Label != Label || payload.PublicRuntimeVerified)
            throw new ArgumentException("Only the fixed local synthetic view is supported.");
        var rows = payload.Candles;
        decimal Value(string s) => decimal.Parse(s, CultureInfo.InvariantCulture);
        var low = rows.Min(r => Math.Min(Value(r.Low), Value(r.Sma20)));
        var high = rows.Max(r => Math.Max(Value(r.High), Value(r.Sma20)));
        var padding = Math.Max((high - low) / 8, 1m); low -= padding; high += padding;
        string Y(decimal value) => (420m - (value - low) / (high - low) * 340m).ToString("F2", CultureInfo.InvariantCulture);
        var svg = new StringBuilder();
        for (var tick = 0; tick <= 4; tick++)
        {
            var value = low + (high - low) * tick / 4;
            var y = Y(value);
            svg.Append(CultureInfo.InvariantCulture, $"<line class='grid' x1='90' y1='{y}' x2='1050' y2='{y}'/><text class='axis' x='78' y='{y}' text-anchor='end'>{value:F2}</text>");
        }
        var points = new List<string>();
        for (var i = 0; i < rows.Count; i++)
        {
            var row = rows[i]; var x = 110 + 40 * i;
            var open = Value(row.Open); var close = Value(row.Close);
            var top = Math.Max(open, close); var bottom = Math.Min(open, close);
            var height = Math.Max(340m * (top - bottom) / (high - low), 1m);
            var color = close >= open ? "up" : "down";
            svg.Append(CultureInfo.InvariantCulture, $"<g class='candle {color}'><title>{WebUtility.HtmlEncode(row.TimeUtc)} | O {open:F4} H {Value(row.High):F4} L {Value(row.Low):F4} C {close:F4} | SMA20 {Value(row.Sma20):F4}</title><line x1='{x}' y1='{Y(Value(row.High))}' x2='{x}' y2='{Y(Value(row.Low))}'/><rect x='{x - 10}' y='{Y(top)}' width='20' height='{height:F2}'/></g>");
            points.Add(x.ToString(CultureInfo.InvariantCulture) + "," + Y(Value(row.Sma20)));
            if (i % 4 == 0)
                svg.Append(CultureInfo.InvariantCulture, $"<text class='axis' x='{x}' y='455' text-anchor='middle'>{i:00}:00</text>");
        }
        return $$"""
            <!doctype html><html lang="en"><meta charset="utf-8"><meta name="viewport" content="width=device-width,initial-scale=1">
            <title>Local synthetic candlestick chart</title><style>
            *{box-sizing:border-box}body{margin:0;background:#0b1220;color:#edf3fc;font:16px system-ui,sans-serif}
            main{max-width:1220px;margin:48px auto;padding:0 28px}header{border-bottom:1px solid #29344b;padding-bottom:24px}
            .tag{color:#68e3ca;font-size:13px;font-weight:700;letter-spacing:.12em}h1{font-size:28px;margin:16px 0 10px;line-height:1.4}
            p{color:#aebed4;line-height:1.7}.panel{background:#111d30;border:1px solid #293a56;border-radius:16px;padding:24px;margin-top:24px}
            .legend{display:flex;gap:28px;font-size:14px;color:#c9d8ec}.legend b{color:#f5c768}svg{width:100%;height:auto}
            .grid{stroke:#27354c;stroke-width:1}.axis{fill:#aabbd3;font:13px system-ui}.candle line{stroke-width:2}
            .up{fill:#47d7b0;stroke:#47d7b0}.down{fill:#f28b94;stroke:#f28b94}.sma{fill:none;stroke:#f5c768;stroke-width:3}
            footer{font-size:13px;color:#91a6c4;margin-top:20px}a{color:#93c5fd}
            </style><main><header><span class="tag">LOCAL DEVELOPMENT · SYNTHETIC DEMO</span>
            <h1>BTC-like Market Simulation — Synthetic Historical OHLCV</h1>
            <p>24 hourly candles · 30 September 2026 · fixed historical window · illustrative price units<br>
            Simulated data, not exchange quotes or live trading.</p></header>
            <section class="panel" aria-label="Synthetic price chart"><div class="legend"><span>OHLC · 1 hour</span><b>━ SMA (20)</b><span>UTC</span></div>
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1100 505" role="img" aria-labelledby="chart-title chart-description">
            <title id="chart-title">24 synthetic hourly candlesticks with SMA20</title>
            <desc id="chart-description">UTC 2026-09-30 00:00 through 23:00. Green rising and rose falling OHLC candles; gold SMA20 uses hidden calculation warmup.</desc>
            {{svg}}<polyline class="sma" points="{{string.Join(' ', points)}}"/>
            <text class="axis" x="570" y="490" text-anchor="middle">30 September 2026 · UTC</text></svg></section>
            <footer>250 preceding candles used only for C# SMA calculation; only 24 visible rows are served.
            Public data API remains disabled. PUBLIC_RUNTIME_VERIFIED=False. <a href="/local-chart/data">Local visible JSON</a></footer></main></html>
            """;
    }
}
