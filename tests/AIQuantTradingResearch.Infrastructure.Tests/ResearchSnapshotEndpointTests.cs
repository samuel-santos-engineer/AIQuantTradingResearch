using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Worker;
using Xunit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ResearchSnapshotEndpointTests(Xunit.Abstractions.ITestOutputHelper evidence)
{
    private const string Key = "offline-test-service-key-12345678";
    private static readonly DateTimeOffset Now = new(2026, 10, 23, 12, 1, 15, TimeSpan.Zero);
    private sealed class Clock : TimeProvider
    {
        public DateTimeOffset Now { get; set; } = ResearchSnapshotEndpointTests.Now;
        public bool Rollover { get; set; }
        public override DateTimeOffset GetUtcNow() { var result = Now; if (Rollover) Now = Now.AddDays(1); return result; }
    }
    private static X509Certificate2 Certificate()
    {
        using var rsa = RSA.Create(2048); var request = new CertificateRequest("CN=localhost", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        var san = new SubjectAlternativeNameBuilder(); san.AddDnsName("localhost"); san.AddIpAddress(IPAddress.Loopback);
        request.CertificateExtensions.Add(san.Build()); request.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
        using var issued = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddDays(-1), DateTimeOffset.UtcNow.AddDays(1));
        return X509CertificateLoader.LoadPkcs12(issued.Export(X509ContentType.Pfx), null);
    }
    private static HttpClient Client(string url, X509Certificate2 certificate)
    {
        var handler = new HttpClientHandler { UseProxy = false, AllowAutoRedirect = false };
        handler.ServerCertificateCustomValidationCallback = (_, cert, _, errors) =>
        {
            if (cert is null || (errors & ~SslPolicyErrors.RemoteCertificateChainErrors) != SslPolicyErrors.None) return false;
            using var chain = new X509Chain(); chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;
            chain.ChainPolicy.CustomTrustStore.Add(certificate); chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
            return chain.Build(cert);
        };
        return new HttpClient(handler) { BaseAddress = new Uri(url), Timeout = TimeSpan.FromSeconds(5) };
    }
    private static StringContent Body(string instrument = "BTC/USD", string epoch = "1", JsonElement? identity = null) =>
        new(JsonSerializer.Serialize(new { schemaVersion = "1.0", instrument, requestEpoch = epoch, knownIdentity = identity }), Encoding.UTF8, "application/json");
    private static void Provision(SqliteResearchStateStore store) => store.ActivateTransform(new(store.Read(ResearchInstrument.BtcUsd).Generation,
        Enumerable.Range(0, 32).Select(i => (byte)i).ToArray(), Guid.Parse("11111111-1111-4111-8111-111111111111"), 1), 0);
    private static void Seed(SqliteResearchStateStore store)
    {
        foreach (var instrument in Enum.GetValues<ResearchInstrument>())
        {
            var old = store.Read(instrument);
            store.Commit(new(instrument, Now, TimeSpan.Zero), new(Enumerable.Range(0, 401).Select(i => ResearchTestDirectory.Candle(i, instrument))), old.Generation, 0);
        }
    }
    [Fact] public async Task RealCommittedCalculatedBtcAndEthFlowThroughTlsAndIndependentPythonClient()
    {
        using var dir = new ResearchTestDirectory(); using var store = new SqliteResearchStateStore(dir.Path, true); Seed(store); Provision(store);
        using var cert = Certificate(); await using var app = ResearchSnapshotEndpoint.Create(new(store, new Clock()), Key, cert);
        await app.StartAsync(); var url = app.Urls.Single(); using var client = Client(url, cert); client.DefaultRequestHeaders.Add("X-API-Key", Key);
        using var response = await client.PostAsync("/v1/research/snapshot", Body()); Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var raw = await response.Content.ReadAsByteArrayAsync(); using var doc = JsonDocument.Parse(raw);
        Assert.Equal(401, doc.RootElement.GetProperty("candles").GetArrayLength());
        Assert.Equal("91.602434113300", doc.RootElement.GetProperty("analytics")[400].GetProperty("sma20").GetString());
        var text = Encoding.UTF8.GetString(raw);
        foreach (var canary in new[] { Key, Convert.ToBase64String(Enumerable.Range(0, 32).Select(i => (byte)i).ToArray()), "11111111-1111-4111-8111-111111111111", "Checkpoint", "100.000000000000", "keyEpoch", "factor", "\"Provider\"" }) Assert.DoesNotContain(canary, text);
        using var unchanged = await client.PostAsync("/v1/research/snapshot", Body(epoch: "2", identity: doc.RootElement.GetProperty("identity")));
        using var noArrays = JsonDocument.Parse(await unchanged.Content.ReadAsByteArrayAsync());
        Assert.Equal("unchanged", noArrays.RootElement.GetProperty("type").GetString());
        foreach (var name in ResearchSnapshotJson.Collections) Assert.False(noArrays.RootElement.TryGetProperty(name, out _));
        var ca = Path.Combine(Path.GetTempPath(), "aiq-wp05-ca-" + Guid.NewGuid().ToString("N") + ".pem");
        try
        {
            await File.WriteAllTextAsync(ca, cert.ExportCertificatePem(), new UTF8Encoding(false));
            var root = FindRoot(); var start = new ProcessStartInfo("python") { UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, CreateNoWindow = true, WorkingDirectory = root };
            foreach (var argument in new[] { "-B", Path.Combine(AppContext.BaseDirectory, "PythonIntegrationFixtures", "research_snapshot_reference.py"), root, url, ca, Key }) start.ArgumentList.Add(argument);
            using var process = Process.Start(start)!; var stdout = process.StandardOutput.ReadToEndAsync(); var stderr = process.StandardError.ReadToEndAsync();
            using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            try { await process.WaitForExitAsync(deadline.Token); } catch { process.Kill(true); throw; }
            var output = await stdout; evidence.WriteLine(output); Assert.True(process.ExitCode == 0, await stderr); Assert.Contains("BTC/USD full401/unchanged/independent-D5 PASS", output); Assert.Contains("ETH/USD full401/unchanged/independent-D5 PASS", output);
        }
        finally { File.Delete(ca); }
        await app.StopAsync();
    }
    [Fact]
    public void ExportOwnedLinuxQualificationFixture()
    {
        var requested = Environment.GetEnvironmentVariable("AIQ_WP07_EXPORT");
        if (string.IsNullOrEmpty(requested)) return; // Explicit fixture export, never normal startup.
        var root = Path.GetFullPath(requested);
        Assert.StartsWith(Path.GetFullPath(Path.GetTempPath()), root);
        Assert.False(Directory.Exists(root));
        Directory.CreateDirectory(root);
        var privateDirectory = Path.Combine(root, "private");
        using (var store = new SqliteResearchStateStore(privateDirectory, true))
        {
            Provision(store);
            var source = new ResearchSnapshotSource(store, new Clock());
            foreach (var instrument in Enum.GetValues<ResearchInstrument>())
            {
                var state = store.Read(instrument);
                store.Commit(new(instrument, Now, TimeSpan.Zero), new(Enumerable.Range(0, 400).Select(n => ResearchTestDirectory.Candle(n, instrument))), state.Generation, state.Version);
                using var before = JsonDocument.Parse(source.Read(new("1.0", instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD", "1", null)));
                Assert.Equal("Unavailable", before.RootElement.GetProperty("status").GetProperty("classificationValidity").GetString());
                state = store.Read(instrument);
                store.Commit(new(instrument, Now, TimeSpan.Zero), new([ResearchTestDirectory.Candle(400, instrument)]), state.Generation, state.Version);
                using var after = JsonDocument.Parse(source.Read(new("1.0", instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD", "2", null)));
                Assert.Equal("Valid", after.RootElement.GetProperty("status").GetProperty("classificationValidity").GetString());
                state = store.Read(instrument);
                store.Commit(new(instrument, Now, TimeSpan.Zero), new(Enumerable.Range(401, 605).Where(n => n != 600).Select(n => ResearchTestDirectory.Candle(n, instrument))), state.Generation, state.Version);
                Assert.Equal(1000, store.Read(instrument).Candles.Count);
            }
        } // Close writer/WAL before exporting; the runtime validates its witness normally.
        var tls = Path.Combine(root, "tls"); Directory.CreateDirectory(tls);
        using var rsa = RSA.Create(2048);
        var request = new CertificateRequest("CN=wp07-research", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        var san = new SubjectAlternativeNameBuilder(); san.AddDnsName("wp07-research");
        request.CertificateExtensions.Add(san.Build());
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
        using var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddMinutes(-5), DateTimeOffset.UtcNow.AddDays(1));
        File.WriteAllBytes(Path.Combine(tls, "server.pfx"), cert.Export(X509ContentType.Pfx));
        File.WriteAllText(Path.Combine(tls, "ca.pem"), cert.ExportCertificatePem() + "\n", new UTF8Encoding(false));
        File.WriteAllText(Path.Combine(root, "service.key"), Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)) + "\n", new UTF8Encoding(false));
        File.WriteAllText(Path.Combine(root, "fixture.json"), "{\"testOnly\":true,\"rowsPerInstrument\":1000,\"readiness400\":false,\"readiness401\":true}\n", new UTF8Encoding(false));
        evidence.WriteLine("Closed offline Linux fixture: both instruments 1000 rows; readiness 400/401 verified; no provider calls.");
    }
    private sealed class BrowserReader(SqliteResearchStateStore initial) : IResearchTransformStateReader
    {
        public SqliteResearchStateStore Current { get; set; } = initial;
        public TResult UseTransformSnapshot<TResult>(ResearchInstrument instrument, Func<ResearchCommittedState, ResearchTransformConfiguration, TResult> callback) =>
            Current.UseTransformSnapshot(instrument, callback);
    }
    [Fact]
    public async Task OwnedBrowserQualificationAgainstRealHttpsSnapshot()
    {
        // Explicit opt-in: ordinary unit test runs never launch a browser.
        var python = Environment.GetEnvironmentVariable("AIQ_WP07_BROWSER_PYTHON");
        if (string.IsNullOrEmpty(python)) return;
        using var dir = new ResearchTestDirectory();
        using var store = new SqliteResearchStateStore(dir.Path, true);
        Seed(store); Provision(store);
        using var cert = Certificate();
        using var alternateDirectory = new ResearchTestDirectory();
        using var alternate = new SqliteResearchStateStore(alternateDirectory.Path, true);
        Seed(alternate); Provision(alternate);
        foreach (var instrument in Enum.GetValues<ResearchInstrument>())
        {
            var state = alternate.Read(instrument);
            alternate.Commit(new(instrument, Now, TimeSpan.Zero), new([ResearchTestDirectory.Candle(400, instrument, close: 104)]), state.Generation, state.Version);
        }
        var reader = new BrowserReader(store);
        var clock = new Clock();
        var app = ResearchSnapshotEndpoint.Create(new(reader, clock), Key, cert);
        await app.StartAsync();
        var endpointPort = new Uri(app.Urls.Single()).Port;
        using var control = new ResearchTestDirectory();
        Directory.CreateDirectory(control.Path);
        var ca = Path.Combine(control.Path, "browser-ca.pem");
        await File.WriteAllTextAsync(ca, cert.ExportCertificatePem(), new UTF8Encoding(false));
        var start = new ProcessStartInfo(python) { UseShellExecute = false, CreateNoWindow = true,
            RedirectStandardOutput = true, RedirectStandardError = true, WorkingDirectory = FindRoot() };
        start.ArgumentList.Add("-B");
        start.ArgumentList.Add(Path.Combine(FindRoot(), "tests", "runtime", "release_1_15", "wp07_browser_qualification.py"));
        start.Environment["AIQ_RESEARCH_BASE_URL"] = app.Urls.Single();
        start.Environment["AIQ_RESEARCH_API_KEY"] = Key;
        start.Environment["AIQ_RESEARCH_CA_FILE"] = ca;
        start.Environment["AIQ_WP07_CONTROL"] = control.Path;
        using var process = Process.Start(start)!;
        var stdout = process.StandardOutput.ReadToEndAsync();
        var stderr = process.StandardError.ReadToEndAsync();
        using var deadline = new CancellationTokenSource(TimeSpan.FromMinutes(5));
        try
        {
            var commandPath = Path.Combine(control.Path, "command.json");
            var ackPath = Path.Combine(control.Path, "ack.json");
            var sequence = 0;
            while (!process.HasExited)
            {
                deadline.Token.ThrowIfCancellationRequested();
                if (File.Exists(commandPath))
                {
                    using var command = JsonDocument.Parse(await File.ReadAllTextAsync(commandPath, deadline.Token));
                    var next = command.RootElement.GetProperty("sequence").GetInt32();
                    if (next > sequence)
                    {
                        var action = command.RootElement.GetProperty("action").GetString();
                        if (action is "correction" or "gap")
                        {
                            foreach (var instrument in Enum.GetValues<ResearchInstrument>())
                            {
                                var state = store.Read(instrument);
                                var candle = ResearchTestDirectory.Candle(action == "gap" ? 405 : 400, instrument, close: action == "gap" ? 100 : 101);
                                store.Commit(new(instrument, clock.Now, TimeSpan.Zero), new([candle]), state.Generation, state.Version);
                            }
                        }
                        else if (action == "generation") reader.Current = alternate;
                        else if (action == "day") clock.Now = clock.Now.AddDays(1);
                        else if (action == "epoch")
                            store.ActivateTransform(new(store.Read(ResearchInstrument.BtcUsd).Generation,
                                Enumerable.Repeat((byte)9, 32).ToArray(), Guid.Parse("55555555-5555-4555-8555-555555555555"), 2), 1);
                        else if (action is "offline" or "unauthorized" or "malformed" or "online")
                        {
                            await app.StopAsync();
                            if (action != "offline")
                            {
                                await app.DisposeAsync();
                                if (action == "malformed")
                                {
                                    // Same owned TLS fixture role/port, deliberately invalid transport body.
                                    // This is test fault injection, never an application/admin route.
                                    var builder = WebApplication.CreateSlimBuilder(); builder.Logging.ClearProviders();
                                    builder.WebHost.ConfigureKestrel(o => o.Listen(IPAddress.Loopback, endpointPort, listener => listener.UseHttps(cert)));
                                    app = builder.Build();
                                    app.Run(async context => { context.Response.ContentType = "application/json"; await context.Response.WriteAsync("{"); });
                                }
                                else app = ResearchSnapshotEndpoint.Create(new(reader, clock), action == "unauthorized" ? "wrong-test-service-key-12345678" : Key, cert, endpointPort);
                                await app.StartAsync();
                            }
                        }
                        else throw new InvalidOperationException("Unknown qualification command.");
                        sequence = next;
                        await File.WriteAllTextAsync(ackPath + ".pending", JsonSerializer.Serialize(new { sequence }), new UTF8Encoding(false), deadline.Token);
                        File.Move(ackPath + ".pending", ackPath, true);
                    }
                }
                await Task.Delay(50, deadline.Token);
            }
        }
        catch { if (!process.HasExited) process.Kill(true); throw; }
        finally { await app.DisposeAsync(); }
        evidence.WriteLine(await stdout);
        Assert.True(process.ExitCode == 0, await stderr);
    }
    private static string FindRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir is not null && !File.Exists(Path.Combine(dir.FullName, "AGENTS.md"))) dir = dir.Parent;
        return dir?.FullName ?? throw new InvalidOperationException();
    }
    [Theory] [InlineData(null)] [InlineData("wrong-offline-service-key")]
    public async Task AuthenticationFailsBeforePrivateStateAccess(string? key)
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); using var cert = Certificate();
        await using var app = ResearchSnapshotEndpoint.Create(new(store, new Clock()), Key, cert); await app.StartAsync();
        using var client = Client(app.Urls.Single(), cert); if (key is not null) client.DefaultRequestHeaders.Add("X-API-Key", key);
        using var response = await client.PostAsync("/v1/research/snapshot", Body()); Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.DoesNotContain(Key, await response.Content.ReadAsStringAsync()); await app.StopAsync();
    }
    [Fact] public async Task MalformedOversizeMethodEncodingAndUnconfiguredStateFailClosed()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); using var cert = Certificate();
        await using var app = ResearchSnapshotEndpoint.Create(new(store, new Clock()), Key, cert); await app.StartAsync();
        using var client = Client(app.Urls.Single(), cert); client.DefaultRequestHeaders.Add("X-API-Key", Key);
        using var absent = await client.PostAsync("/v1/research/snapshot", Body()); Assert.Equal(HttpStatusCode.ServiceUnavailable, absent.StatusCode);
        using var method = await client.GetAsync("/v1/research/snapshot"); Assert.Equal(HttpStatusCode.MethodNotAllowed, method.StatusCode);
        using var malformed = await client.PostAsync("/v1/research/snapshot", new StringContent("{}", Encoding.UTF8, "application/json")); Assert.Equal(HttpStatusCode.BadRequest, malformed.StatusCode);
        using var huge = await client.PostAsync("/v1/research/snapshot", new StringContent(new string('x', 4097), Encoding.UTF8, "application/json")); Assert.Equal(HttpStatusCode.RequestEntityTooLarge, huge.StatusCode);
        using var encodedBody = Body(); encodedBody.Headers.ContentEncoding.Add("gzip");
        using var encoded = await client.PostAsync("/v1/research/snapshot", encodedBody); Assert.Equal(HttpStatusCode.UnsupportedMediaType, encoded.StatusCode);
        using var wrongType = await client.PostAsync("/v1/research/snapshot", new StringContent("{}")); Assert.Equal(HttpStatusCode.UnsupportedMediaType, wrongType.StatusCode);
        using var badUtf = new ByteArrayContent([0xff]); badUtf.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        using var utf = await client.PostAsync("/v1/research/snapshot", badUtf); Assert.Equal(HttpStatusCode.BadRequest, utf.StatusCode);
        using var dup = new HttpRequestMessage(HttpMethod.Post, "/v1/research/snapshot") { Content = Body() }; dup.Headers.Add("X-API-Key", new[] { Key, Key });
        using var duplicate = await client.SendAsync(dup); Assert.Equal(HttpStatusCode.Unauthorized, duplicate.StatusCode);
        await app.StopAsync();
    }
    private sealed class ChunkedContent : HttpContent
    {
        public ChunkedContent() { Headers.ContentType = new MediaTypeHeaderValue("application/json"); }
        protected override bool TryComputeLength(out long length) { length = 0; return false; }
        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
        {
            for (var i = 0; i < 5; i++) await stream.WriteAsync(new byte[1024]);
        }
    }
    [Fact] public async Task ChunkedOversizeIsRejectedWithoutContentLength()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); using var cert = Certificate();
        await using var app = ResearchSnapshotEndpoint.Create(new(store, new Clock()), Key, cert); await app.StartAsync();
        using var client = Client(app.Urls.Single(), cert); client.DefaultRequestHeaders.Add("X-API-Key", Key);
        using var body = new ChunkedContent(); using var response = await client.PostAsync("/v1/research/snapshot", body);
        Assert.Equal(HttpStatusCode.RequestEntityTooLarge, response.StatusCode); await app.StopAsync();
    }
    [Fact] public void MaximumRetainedSnapshotFitsTheWireBudgetAndKeepsWarmupTruthful()
    {
        using var dir = new ResearchTestDirectory(); using var store = new SqliteResearchStateStore(dir.Path, true);
        var old = store.Read(ResearchInstrument.BtcUsd);
        store.Commit(new(ResearchInstrument.BtcUsd, Now, TimeSpan.Zero), new(Enumerable.Range(0, 1000).Select(i => ResearchTestDirectory.Candle(i))), old.Generation, 0);
        Provision(store); var bytes = new ResearchSnapshotSource(store, new Clock()).Read(new("1.0", "BTC/USD", "1", null));
        using var doc = JsonDocument.Parse(bytes);
        Assert.InRange(bytes.Length, 1, ResearchSnapshotJson.ResponseLimit);
        Assert.Equal(1000, doc.RootElement.GetProperty("candles").GetArrayLength());
        Assert.Equal("Unavailable", doc.RootElement.GetProperty("classifications")[399].GetProperty("validity").GetString());
        Assert.Equal("Valid", doc.RootElement.GetProperty("classifications")[400].GetProperty("validity").GetString());
        evidence.WriteLine("Maximum retained snapshot bytes=" + bytes.Length);
    }
    [Fact] public void RecoveryAndPostOpenTamperingNeverProduceSuccessfulSnapshot()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); Provision(store);
        using (var c = new Microsoft.Data.Sqlite.SqliteConnection("Pooling=False;Data Source=" + Path.Combine(dir.Path, "research.sqlite")))
        {
            c.Open(); using var command = c.CreateCommand(); command.CommandText = "UPDATE transform_configuration SET transform_epoch=2"; command.ExecuteNonQuery();
        }
        Assert.Throws<ResearchUnavailableException>(() => new ResearchSnapshotSource(store, new Clock()).Read(new("1.0", "BTC/USD", "1", null)));
    }
    [Fact] public void MidnightAndBackwardClockDiscardStagedBuffersAndRotationForcesFull()
    {
        using var dir = new ResearchTestDirectory(); using var store = dir.Open(); Provision(store);
        var clock = new Clock(); var source = new ResearchSnapshotSource(store, clock); var request = new ResearchSnapshotRequest("1.0", "BTC/USD", "1", null);
        using var first = JsonDocument.Parse(source.Read(request)); var id = ResearchSnapshotJson.Identity(first.RootElement.GetProperty("identity"));
        store.ActivateTransform(new(store.Read(ResearchInstrument.BtcUsd).Generation, new byte[32], Guid.NewGuid(), 2), 1);
        using var next = JsonDocument.Parse(source.Read(request with { KnownIdentity = id })); Assert.Equal("full", next.RootElement.GetProperty("type").GetString());
        clock.Now = Now.AddDays(-1); Assert.Equal("UNTRUSTED_TIME", Assert.Throws<ResearchSnapshotException>(() => source.Read(request)).Code);
        clock.Now = Now; clock.Rollover = true; Assert.Equal("DATA_UNAVAILABLE", Assert.Throws<ResearchSnapshotException>(() => source.Read(request)).Code);
    }
}
