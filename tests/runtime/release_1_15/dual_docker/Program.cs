using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;

// WP02 OFFLINE SECURITY FIXTURE ONLY. No ingestion, database, research or D4 server.
if (args.Length == 2 && args[0] == "--make-cert")
{
    Directory.CreateDirectory(args[1]);
    using var rsa = RSA.Create(2048);
    var request = new CertificateRequest("CN=research", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    var names = new SubjectAlternativeNameBuilder();
    names.AddDnsName("research");
    names.AddDnsName("localhost");
    names.AddIpAddress(IPAddress.Loopback);
    request.CertificateExtensions.Add(names.Build());
    request.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
    request.CertificateExtensions.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.KeyCertSign, true));
    using var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddMinutes(-1), DateTimeOffset.UtcNow.AddHours(2));
    File.WriteAllBytes(Path.Combine(args[1], "server.pfx"), cert.Export(X509ContentType.Pfx, ""));
    File.WriteAllText(Path.Combine(args[1], "trust.pem"), cert.ExportCertificatePem() + "\n", new UTF8Encoding(false));
    return;
}

static byte[] KeyDigest(string? value)
{
    if (value is null || value.Length != 44) throw new InvalidOperationException("fixture_configuration_unavailable");
    var bytes = Convert.FromBase64String(value);
    if (bytes.Length != 32 || Convert.ToBase64String(bytes) != value) throw new InvalidOperationException("fixture_configuration_unavailable");
    return SHA256.HashData(bytes);
}
byte[] active;
int fixtureDelayMs = 0;
string fixtureDelayStage;
try
{
    active = KeyDigest(Environment.GetEnvironmentVariable("AIQ_FIXTURE_ACTIVE_KEY"));
    // Obsolete overlap settings are rejected, never silently accepted as fallback.
    if (Environment.GetEnvironmentVariable("AIQ_FIXTURE_PREVIOUS_KEY") is not null ||
        Environment.GetEnvironmentVariable("AIQ_FIXTURE_PREVIOUS_EXPIRES") is not null)
        throw new InvalidOperationException();
    var delay = Environment.GetEnvironmentVariable("AIQ_FIXTURE_STATUS_DELAY_MS");
    if (delay is not null && (!int.TryParse(delay, out fixtureDelayMs) || fixtureDelayMs < 0 || fixtureDelayMs > 1000))
        throw new InvalidOperationException();
    fixtureDelayStage = Environment.GetEnvironmentVariable("AIQ_FIXTURE_DELAY_STAGE") ?? "headers";
    if (fixtureDelayStage is not ("headers" or "body")) throw new InvalidOperationException();
}
catch
{
    Console.Error.WriteLine("fixture_configuration_unavailable");
    Environment.ExitCode = 64;
    return;
}
var builder = WebApplication.CreateBuilder(Array.Empty<string>());
builder.Logging.ClearProviders(); // Never log request headers, targets or exception values.
builder.WebHost.ConfigureKestrel(options =>
{
    options.AddServerHeader = false;
    options.Limits.MaxRequestBodySize = 4096;
    options.Limits.MaxRequestHeadersTotalSize = 4096;
    options.Limits.MaxRequestHeaderCount = 16;
    options.Limits.MaxConcurrentConnections = 8;
    options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(4);
    options.ListenAnyIP(52731, listen =>
    {
        listen.Protocols = HttpProtocols.Http1;
        listen.UseHttps(Environment.GetEnvironmentVariable("AIQ_FIXTURE_CERT") ?? "/certs/server.pfx", "");
    });
});
builder.Services.Configure<HostOptions>(options => options.ShutdownTimeout = TimeSpan.FromSeconds(5));
var app = builder.Build();
using var admission = new SemaphoreSlim(4);
app.Run(async context =>
{
    context.Response.Headers.CacheControl = "no-store";
    try
    {
        if (context.Request.Path == "/health/live" && context.Request.Method == "GET")
        {
            await context.Response.WriteAsync("ok");
            return;
        }
        // All non-liveness paths authenticate before inspecting body or returning status.
        var values = context.Request.Headers["X-API-Key"];
        byte[] supplied;
        try
        {
            if (values.Count != 1) throw new InvalidOperationException();
            supplied = KeyDigest(values[0]);
        }
        catch
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Request unavailable.");
            return;
        }
        if (!CryptographicOperations.FixedTimeEquals(supplied, active))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Request unavailable.");
            return;
        }
        if (!await admission.WaitAsync(0))
        {
            context.Response.StatusCode = 429;
            return;
        }
        try
        {
            using var deadline = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted);
            deadline.CancelAfter(TimeSpan.FromSeconds(4));
            if (context.Request.Path == "/fixture/status" && context.Request.Method == "GET")
            {
                context.Response.ContentType = "application/json";
                const string status = "{\"fixture\":true,\"status\":\"unavailable\"}";
                if (fixtureDelayStage == "body")
                {
                    context.Response.ContentLength = Encoding.UTF8.GetByteCount(status);
                    await context.Response.StartAsync(deadline.Token);
                }
                // Owned offline timeout test only, no public timing-control endpoint.
                await Task.Delay(fixtureDelayMs, deadline.Token);
                await context.Response.WriteAsync(status, deadline.Token);
            }
            else if (context.Request.Path == "/v1/research/snapshot" && context.Request.Method == "POST")
            {
                var buffer = new byte[1024];
                using var body = new MemoryStream();
                int size;
                while ((size = await context.Request.Body.ReadAsync(buffer, deadline.Token)) != 0)
                {
                    if (body.Length + size > 4096) { context.Response.StatusCode = 413; return; }
                    await body.WriteAsync(buffer.AsMemory(0, size), deadline.Token);
                }
                var text = new UTF8Encoding(false, true).GetString(body.ToArray());
                using var document = JsonDocument.Parse(text, new JsonDocumentOptions { MaxDepth = 8 });
                if (document.RootElement.ValueKind != JsonValueKind.Object || document.RootElement.EnumerateObject().Any())
                { context.Response.StatusCode = 400; return; }
                context.Response.StatusCode = 503;
                await context.Response.WriteAsync("Fixture unavailable.", deadline.Token);
            }
            else context.Response.StatusCode = 404;
        }
        finally { admission.Release(); }
    }
    catch (Microsoft.AspNetCore.Http.BadHttpRequestException) { if (!context.Response.HasStarted) context.Response.StatusCode = 413; }
    catch (JsonException) { if (!context.Response.HasStarted) context.Response.StatusCode = 400; }
    catch (DecoderFallbackException) { if (!context.Response.HasStarted) context.Response.StatusCode = 400; }
    catch (OperationCanceledException) { context.Abort(); }
    catch { if (!context.Response.HasStarted) context.Response.StatusCode = 500; else context.Abort(); }
});
await app.RunAsync();
