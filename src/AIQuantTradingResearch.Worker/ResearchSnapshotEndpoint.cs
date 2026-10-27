using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AIQuantTradingResearch.Worker;

/// <summary>Explicit platform opt-in, not attestation of Azure ingress configuration.</summary>
public sealed record ResearchSnapshotTransport
{
    public bool PlatformHttp { get; }
    private ResearchSnapshotTransport(bool platformHttp) => PlatformHttp = platformHttp;
    public static ResearchSnapshotTransport FromSettings(Func<string, string?> setting)
    {
        var mode = setting("AIQ_RESEARCH_TRANSPORT") ?? "local-https";
        if (mode == "local-https") return new(false);
        if (mode != "azure-app-service-http" ||
            setting("WEBSITE_SITE_NAME") != "aiqr115researchstagingwus27f5eabb5" ||
            setting("WEBSITE_HOSTNAME") != "aiqr115researchstagingwus27f5eabb5.azurewebsites.net" ||
            string.IsNullOrWhiteSpace(setting("WEBSITE_INSTANCE_ID")) ||
            setting("AIQ_RESEARCH_PLATFORM_HTTPS_ONLY") != "1" ||
            setting("AIQ_RESEARCH_BIND_ADDRESS") != "0.0.0.0")
            throw new ArgumentException("Research configuration unavailable.");
        return new(true);
    }
}

public static class ResearchSnapshotEndpoint
{
    public static WebApplication Create(ResearchSnapshotSource? source, string apiKey, X509Certificate2? certificate, int port = 0, IPAddress? listenAddress = null, ResearchSnapshotTransport? transport = null)
    {
        var platformHttp = transport?.PlatformHttp == true;
        if (platformHttp && !IPAddress.Any.Equals(listenAddress)) throw new ArgumentException("Research configuration unavailable.");
        if (string.IsNullOrWhiteSpace(apiKey) || apiKey.Length is < 16 or > 256 || apiKey.Any(c => c is < '!' or > '~') || (!platformHttp && certificate?.HasPrivateKey != true) || (platformHttp && certificate is not null))
            throw new ArgumentException("Research configuration unavailable.");
        var expected = SHA256.HashData(Encoding.ASCII.GetBytes(apiKey));
        var builder = WebApplication.CreateSlimBuilder(); builder.Logging.ClearProviders();
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.AddServerHeader = false;
            options.Limits.MaxRequestBodySize = ResearchSnapshotJson.RequestLimit;
            options.Limits.MaxRequestHeadersTotalSize = 8192;
            options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(4);
            options.Listen(listenAddress ?? IPAddress.Loopback, port, listen => { if (!platformHttp) listen.UseHttps(certificate!); });
        });
        var app = builder.Build(); var admission = new SemaphoreSlim(1, 1);
        app.Lifetime.ApplicationStopped.Register(admission.Dispose);
        app.Run(async context =>
        {
            using var deadline = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted);
            deadline.CancelAfter(TimeSpan.FromSeconds(4));
            var request = context.Request;
            if (request.Path == "/health" && request.Method == "GET") { context.Response.StatusCode = 200; await context.Response.WriteAsync("Alive", deadline.Token); return; }
            var key = request.Headers["X-API-Key"];
            if (key.Count != 1 || key[0] is not { Length: >= 16 and <= 256 } value || value.Any(c => c is < '!' or > '~') ||
                !CryptographicOperations.FixedTimeEquals(expected, SHA256.HashData(Encoding.ASCII.GetBytes(value))))
            { await Error(context, 401, "UNAUTHORIZED", deadline.Token); return; }
            if (request.Path != "/v1/research/snapshot") { await Error(context, 404, "NOT_FOUND", deadline.Token); return; }
            if (request.Method != "POST") { await Error(context, 405, "METHOD_NOT_ALLOWED", deadline.Token); return; }
            if (request.QueryString.HasValue) { await Error(context, 400, "MALFORMED_REQUEST", deadline.Token); return; }
            var content = request.ContentType?.Split(';', StringSplitOptions.TrimEntries);
            if (content is null || !content[0].Equals("application/json", StringComparison.OrdinalIgnoreCase) || content.Length > 2 ||
                content.Length == 2 && !content[1].Equals("charset=utf-8", StringComparison.OrdinalIgnoreCase) ||
                request.Headers.ContentEncoding.Count > 0 && !request.Headers.ContentEncoding.ToString().Equals("identity", StringComparison.OrdinalIgnoreCase))
            { await Error(context, 415, "UNSUPPORTED_MEDIA_TYPE", deadline.Token); return; }
            if (!admission.Wait(0)) { await Error(context, 429, "RATE_LIMITED", deadline.Token); return; }
            ResearchSnapshotRequest? parsed = null;
            try
            {
                if (request.ContentLength > ResearchSnapshotJson.RequestLimit) throw new ResearchSnapshotException("REQUEST_TOO_LARGE");
                using var buffer = new MemoryStream(); var chunk = new byte[1024];
                while (true)
                {
                    var count = await request.Body.ReadAsync(chunk, deadline.Token); if (count == 0) break;
                    if (buffer.Length + count > ResearchSnapshotJson.RequestLimit) throw new ResearchSnapshotException("REQUEST_TOO_LARGE");
                    buffer.Write(chunk, 0, count);
                }
                parsed = ResearchSnapshotJson.Request(buffer.ToArray());
                var response = source?.Read(parsed, deadline.Token) ?? throw new ResearchSnapshotException("DATASET_NOT_READY");
                deadline.Token.ThrowIfCancellationRequested();
                context.Response.ContentType = "application/json; charset=utf-8"; context.Response.ContentLength = response.Length;
                await context.Response.Body.WriteAsync(response, deadline.Token);
            }
            catch (OperationCanceledException) { if (!context.Response.HasStarted) await Error(context, 504, "DEADLINE_EXCEEDED", context.RequestAborted, parsed); }
            catch (BadHttpRequestException e) { await Error(context, e.StatusCode == 413 ? 413 : 400, e.StatusCode == 413 ? "REQUEST_TOO_LARGE" : "MALFORMED_REQUEST", context.RequestAborted, parsed); }
            catch (ResearchSnapshotException e)
            {
                var code = e.Code; var status = code switch
                {
                    "REQUEST_TOO_LARGE" => 413, "MALFORMED_REQUEST" => 400,
                    "UNSUPPORTED_SCHEMA" or "UNSUPPORTED_INSTRUMENT" or "INVALID_IDENTITY" or "INVALID_EPOCH" => 422,
                    "RESPONSE_TOO_LARGE" => 500, _ => 503
                };
                await Error(context, status, code, context.RequestAborted, parsed);
            }
            catch (ResearchUnavailableException) { await Error(context, 503, "DATA_UNAVAILABLE", context.RequestAborted, parsed); }
            catch (Exception) { if (!context.Response.HasStarted) await Error(context, 500, "INTERNAL_ERROR", context.RequestAborted, parsed); }
            finally { admission.Release(); }
        });
        return app;
    }
    private static async Task Error(HttpContext context, int status, string code, CancellationToken token, ResearchSnapshotRequest? request = null)
    {
        if (context.Response.HasStarted) { context.Abort(); return; }
        var bytes = ResearchSnapshotJson.Serialize(ResearchSnapshotTransformer.Map(("schemaVersion", "1.0"), ("type", "error"),
            ("instrument", request?.Instrument), ("requestEpoch", request?.RequestEpoch), ("observedAt", ResearchSnapshotTransformer.Observation(DateTimeOffset.UtcNow)),
            ("error", ResearchSnapshotTransformer.Map(("code", code), ("message", "Request unavailable."), ("retryAfterSeconds", status is 429 or 503 ? 1 : null)))));
        context.Response.Headers.Connection = "close";
        context.Response.StatusCode = status; context.Response.ContentType = "application/json; charset=utf-8"; context.Response.ContentLength = bytes.Length;
        await context.Response.Body.WriteAsync(bytes, token);
    }
    /// <summary>Missing state stays missing; operator initialization/restart is separate.</summary>
    public static SqliteResearchStateStore? OpenExistingStore(string directory)
    {
        if (string.IsNullOrWhiteSpace(directory) || !Path.IsPathFullyQualified(directory)) throw new ArgumentException("Research configuration unavailable.");
        ResearchStorageBoundary.ValidateServiceDirectory(directory);
        try { _ = File.GetAttributes(Path.Combine(directory, "research.sqlite")); }
        catch (FileNotFoundException) { return null; }
        catch (DirectoryNotFoundException) { return null; }
        return new SqliteResearchStateStore(directory, true);
    }
    public static async Task<int> RunAsync()
    {
        try
        {
            var directory = Environment.GetEnvironmentVariable("AIQ_RESEARCH_PRIVATE_DIRECTORY") ?? throw new ArgumentException();
            var key = Environment.GetEnvironmentVariable("AIQ_RESEARCH_API_KEY") ?? throw new ArgumentException();
            var transport = ResearchSnapshotTransport.FromSettings(Environment.GetEnvironmentVariable);
            using var certificate = transport.PlatformHttp ? null : X509CertificateLoader.LoadPkcs12FromFile(
                Environment.GetEnvironmentVariable("AIQ_RESEARCH_TLS_PFX") ?? throw new ArgumentException(),
                Environment.GetEnvironmentVariable("AIQ_RESEARCH_TLS_PASSWORD"));
            using var store = OpenExistingStore(directory);
            store?.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, _) => true); // Never auto-provision a tuple or provider.
            var addressText = Environment.GetEnvironmentVariable("AIQ_RESEARCH_BIND_ADDRESS") ?? "127.0.0.1";
            if (addressText is not ("127.0.0.1" or "0.0.0.0")) throw new ArgumentException();
            await using var app = Create(store is null ? null : new ResearchSnapshotSource(store), key, certificate, 52731, IPAddress.Parse(addressText), transport);
            await app.RunAsync(); return 0;
        }
        catch { Console.Error.WriteLine("Research snapshot service unavailable."); return 1; }
    }
}
