using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

namespace AIQuantTradingResearch.Worker;

/// <summary>One-shot local acquisition. No background loop, retry, credentials file or state replacement.</summary>
public static class ResearchLocalHistory
{
    /// <summary>Fixed operator metadata only. Never accepts exception text or provider content.</summary>
    public sealed class AcquisitionTrace
    {
        public string Stage { get; private set; } = "preflight";
        public string Code { get; private set; } = "UNKNOWN";
        public string? Instrument { get; private set; }
        public int? HttpStatus { get; private set; }
        public int RequestsStarted { get; private set; }
        public int ResponsesReceived { get; private set; }
        private static readonly HashSet<string> Stages = ["preflight", "private_input", "request_start", "http_response", "response_parse", "observation_validation", "bundle_publication", "unknown"];
        private static readonly HashSet<string> Codes = ["MISSING_APPROVAL", "INVALID_DESTINATION", "DESTINATION_EXISTS", "INVALID_PRIVATE_INPUT", "VOLUME_UNCONFIRMED", "TIMEOUT", "CANCELLED", "TRANSPORT_ERROR", "HTTP_UNAUTHORIZED", "HTTP_FORBIDDEN", "HTTP_RATE_LIMITED", "HTTP_SERVER_ERROR", "HTTP_OTHER", "INVALID_RESPONSE_HEADERS", "INVALID_PROVIDER_PAYLOAD", "INVALID_PROVIDER_METADATA", "INVALID_OBSERVATION", "INVALID_VOLUME", "ZERO_ACCEPTED_CANDLES", "BUNDLE_PUBLICATION_FAILED", "UNKNOWN"];
        public void Step(string stage) => Stage = Stages.Contains(stage) ? stage : "unknown";
        public void Fail(string stage, string code)
        {
            if (Code != "UNKNOWN") return; // Preserve the first classified failure.
            Step(stage); Code = Codes.Contains(code) ? code : "UNKNOWN";
        }
        public void Select(ResearchInstrument instrument)
        { Instrument = instrument switch { ResearchInstrument.BtcUsd => "BTC/USD", ResearchInstrument.EthUsd => "ETH/USD", _ => null }; }
        public void Observe(TwelveDataReadDiagnostic signal)
        {
            Select(signal.Instrument);
            switch (signal.Event)
            {
                case TwelveDataReadEvent.Preflight: Step("preflight"); HttpStatus = null; break;
                case TwelveDataReadEvent.RequestStarted: Step("request_start"); RequestsStarted++; HttpStatus = null; break;
                case TwelveDataReadEvent.ResponseReceived: Step("http_response"); ResponsesReceived++; HttpStatus = signal.HttpStatus; break;
                case TwelveDataReadEvent.ResponseParse: Step("response_parse"); break;
                case TwelveDataReadEvent.ObservationValidation: Step("observation_validation"); break;
                case TwelveDataReadEvent.InvalidPrivateInput: Fail("preflight", "INVALID_PRIVATE_INPUT"); break;
                case TwelveDataReadEvent.VolumeUnconfirmed: Fail("preflight", "VOLUME_UNCONFIRMED"); break;
                case TwelveDataReadEvent.HttpFailure:
                    Fail("http_response", HttpStatus switch { 401 => "HTTP_UNAUTHORIZED", 403 => "HTTP_FORBIDDEN", 429 => "HTTP_RATE_LIMITED", >= 500 and <= 599 => "HTTP_SERVER_ERROR", _ => "HTTP_OTHER" }); break;
                case TwelveDataReadEvent.InvalidHeaders: Fail("http_response", "INVALID_RESPONSE_HEADERS"); break;
                case TwelveDataReadEvent.InvalidPayload: Fail(Stage, "INVALID_PROVIDER_PAYLOAD"); break;
                case TwelveDataReadEvent.InvalidMetadata: Fail("response_parse", "INVALID_PROVIDER_METADATA"); break;
                case TwelveDataReadEvent.InvalidObservation: Fail("observation_validation", "INVALID_OBSERVATION"); break;
                case TwelveDataReadEvent.InvalidVolume: Fail("observation_validation", "INVALID_VOLUME"); break;
                case TwelveDataReadEvent.TransportFailure: Fail(Stage, "TRANSPORT_ERROR"); break;
                case TwelveDataReadEvent.Timeout: Fail(Stage, "TIMEOUT"); break;
                case TwelveDataReadEvent.Cancelled: Fail(Stage, "CANCELLED"); break;
                default: Fail("unknown", "UNKNOWN"); break;
            }
        }
        public string FailureJson() => JsonSerializer.Serialize(new { version = 1, @event = "WP39_ACQUIRE_FAILED",
            failedStage = Stage, instrument = Instrument, httpStatus = HttpStatus, requestsStarted = RequestsStarted,
            responsesReceived = ResponsesReceived, outcomeUnknown = RequestsStarted > ResponsesReceived,
            failureCode = Code, bundlePublished = false });
    }

    public const string Endpoint = "https://api.twelvedata.com/time_series";
    private static string Utc(DateTimeOffset value) => value.ToUniversalTime().ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
    private static string Number(decimal value) => value.ToString("0.############", CultureInfo.InvariantCulture);

    public static Task AcquireAsync(IResearchCandleSource source, string destination, DateTimeOffset observed,
        bool testOnly, CancellationToken token = default) => AcquireCoreAsync(source, destination, observed, testOnly, null, token);

    private static async Task AcquireCoreAsync(IResearchCandleSource source, string destination, DateTimeOffset observed,
        bool testOnly, AcquisitionTrace? trace, CancellationToken token)
    {
        trace?.Step("preflight");
        if (observed.Offset != TimeSpan.Zero || observed > DateTimeOffset.UtcNow)
        { trace?.Fail("preflight", "INVALID_PRIVATE_INPUT"); throw new ResearchUnavailableException(); }
        string root;
        try { root = Path.GetFullPath(destination); }
        catch { trace?.Fail("preflight", "INVALID_DESTINATION"); throw; }
        if (Directory.Exists(root) || File.Exists(root))
        { trace?.Fail("preflight", "DESTINATION_EXISTS"); throw new ResearchUnavailableException(); }
        var imports = new Dictionary<string, object?> { ["source"] = "twelvedata", ["observedAtUtc"] = Utc(observed) };
        var summaries = new Dictionary<string, object>();
        // Sequential, exactly one bounded latest-window request per symbol; never paginate or retry.
        foreach (var instrument in Enum.GetValues<ResearchInstrument>())
        {
            token.ThrowIfCancellationRequested();
            trace?.Select(instrument);
            var request = new ResearchPollRequest(instrument, observed, TimeSpan.FromSeconds(15));
            var batch = await source.ReadCompletedAsync(request, token);
            trace?.Step("observation_validation");
            if (batch.Candles.Count == 0)
            { trace?.Fail("observation_validation", "ZERO_ACCEPTED_CANDLES"); throw new ResearchUnavailableException(); }
            try
            {
                ResearchInputValidation.Validate(request, batch);
                if (batch.Candles.Count > 1000 || batch.Candles.Any(c => c.OpenTimeUtc < observed.AddHours(-24))) throw new ResearchUnavailableException();
            }
            catch { trace?.Fail("observation_validation", "INVALID_OBSERVATION"); throw; }
            var symbol = instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD";
            imports[symbol] = batch.Candles.Select(c => new { openTimeUtc = Utc(c.OpenTimeUtc), open = Number(c.Open),
                high = Number(c.High), low = Number(c.Low), close = Number(c.Close), volume = Number(c.Volume) }).ToArray();
            summaries[symbol] = new { originalSymbol = symbol, rows = batch.Candles.Count,
                firstUtc = Utc(batch.Candles[0].OpenTimeUtc), lastUtc = Utc(batch.Candles[^1].OpenTimeUtc),
                gaps = batch.Candles.Zip(batch.Candles.Skip(1)).Count(pair => pair.Second.OpenTimeUtc - pair.First.OpenTimeUtc != TimeSpan.FromMinutes(1)) };
        }
        trace?.Step("bundle_publication");
        var bytes = JsonSerializer.SerializeToUtf8Bytes(imports);
        var evidence = JsonSerializer.SerializeToUtf8Bytes(new { version = 1, provider = "twelvedata", endpoint = Endpoint,
            interval = "1min", kind = testOnly ? "SyntheticFixture" : "ObservedProvider", status = "AcquiredNotImported",
            acquiredAtUtc = Utc(DateTimeOffset.UtcNow), observedAtUtc = Utc(observed), completionGraceSeconds = 15,
            importSha256 = Convert.ToHexString(SHA256.HashData(bytes)), symbols = summaries });
        var staging = root + ".pending-" + Guid.NewGuid().ToString("N");
        try
        {
            Directory.CreateDirectory(staging);
            if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(staging, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
            await File.WriteAllBytesAsync(Path.Combine(staging, "observations.json"), bytes, token);
            await File.WriteAllBytesAsync(Path.Combine(staging, "provenance.json"), evidence, token);
            if (!OperatingSystem.IsWindows()) foreach (var file in Directory.GetFiles(staging)) File.SetUnixFileMode(file, UnixFileMode.UserRead | UnixFileMode.UserWrite);
            Directory.Move(staging, root); // Publish only after both instruments validate; never overwrite.
        }
        catch (OperationCanceledException) { trace?.Fail("bundle_publication", "CANCELLED"); throw; }
        catch { trace?.Fail("bundle_publication", "BUNDLE_PUBLICATION_FAILED"); throw; }
        finally { if (Directory.Exists(staging)) Directory.Delete(staging, true); }
    }

    public static string VerifyBundle(string directory, bool allowTestFixture = false)
    {
        var root = Path.GetFullPath(directory);
        for (var parent = new DirectoryInfo(root); parent is not null; parent = parent.Parent)
            if ((parent.Attributes & FileAttributes.ReparsePoint) != 0) throw new ResearchUnavailableException();
        byte[] Read(string name, int limit)
        {
            var file = new FileInfo(Path.Combine(root, name));
            if ((file.Attributes & FileAttributes.ReparsePoint) != 0 || file.Length < 1 || file.Length > limit) throw new ResearchUnavailableException();
            using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var memory = new MemoryStream(); var chunk = new byte[4096]; int count;
            while ((count = stream.Read(chunk)) != 0)
            { if (memory.Length + count > limit) throw new ResearchUnavailableException(); memory.Write(chunk,0,count); }
            return memory.ToArray();
        }
        var bytes = Read("observations.json", 1_048_576);
        using var evidence = JsonDocument.Parse(Read("provenance.json", 8192));
        var e = evidence.RootElement;
        if (e.GetProperty("version").GetInt32() != 1 || e.GetProperty("provider").GetString() != "twelvedata" ||
            e.GetProperty("endpoint").GetString() != Endpoint || e.GetProperty("interval").GetString() != "1min" ||
            e.GetProperty("status").GetString() != "AcquiredNotImported" ||
            e.GetProperty("importSha256").GetString() != Convert.ToHexString(SHA256.HashData(bytes))) throw new ResearchUnavailableException();
        var kind = e.GetProperty("kind").GetString();
        if (kind != "ObservedProvider" && !(allowTestFixture && kind == "SyntheticFixture")) throw new ResearchUnavailableException();
        using var imported = JsonDocument.Parse(bytes);
        var rowsRoot = imported.RootElement;
        if (rowsRoot.GetProperty("source").GetString() != "twelvedata" ||
            rowsRoot.GetProperty("observedAtUtc").GetString() != e.GetProperty("observedAtUtc").GetString()) throw new ResearchUnavailableException();
        foreach (var symbol in new[] { "BTC/USD", "ETH/USD" })
        {
            var rows = rowsRoot.GetProperty(symbol); var summary = e.GetProperty("symbols").GetProperty(symbol);
            if (rows.GetArrayLength() is < 1 or > 1000 || rows.GetArrayLength() != summary.GetProperty("rows").GetInt32() ||
                summary.GetProperty("originalSymbol").GetString() != symbol ||
                rows[0].GetProperty("openTimeUtc").GetString() != summary.GetProperty("firstUtc").GetString() ||
                rows[rows.GetArrayLength()-1].GetProperty("openTimeUtc").GetString() != summary.GetProperty("lastUtc").GetString()) throw new ResearchUnavailableException();
        }
        return kind!;
    }

    public static void Prepare(string destination)
    {
        var root = Path.GetFullPath(destination);
        if (Directory.Exists(root) || File.Exists(root)) throw new ResearchUnavailableException();
        Directory.CreateDirectory(root);
        if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(root, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
        using var rsa = RSA.Create(2048);
        var request = new CertificateRequest("CN=wp07-research", rsa, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        var san = new SubjectAlternativeNameBuilder(); san.AddDnsName("wp07-research");
        request.CertificateExtensions.Add(san.Build());
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
        using var cert = request.CreateSelfSigned(DateTimeOffset.UtcNow.AddMinutes(-1), DateTimeOffset.UtcNow.AddDays(1));
        var tls = Path.Combine(root, "tls"); Directory.CreateDirectory(tls);
        File.WriteAllBytes(Path.Combine(tls, "server.pfx"), cert.Export(X509ContentType.Pfx));
        File.WriteAllText(Path.Combine(tls, "ca.pem"), cert.ExportCertificatePem(), new UTF8Encoding(false));
        var api = Convert.ToHexString(RandomNumberGenerator.GetBytes(32)).ToLowerInvariant();
        File.WriteAllText(Path.Combine(root,"service.key"), api, new UTF8Encoding(false));
        File.WriteAllBytes(Path.Combine(root,"initialize.json"), JsonSerializer.SerializeToUtf8Bytes(new {
            generation = Guid.NewGuid(), keyEpoch = Guid.NewGuid(), transformEpoch = 1,
            hmacKeyBase64 = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)), apiKey = api }));
        if (!OperatingSystem.IsWindows())
        {
            File.SetUnixFileMode(tls, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
            foreach (var file in Directory.GetFiles(root).Concat(Directory.GetFiles(tls))) File.SetUnixFileMode(file, UnixFileMode.UserRead | UnixFileMode.UserWrite);
        }
    }

    public static int VerifyCommand(string? directory)
    {
        try { Console.WriteLine(VerifyBundle(directory!)); return 0; }
        catch { Console.Error.WriteLine("Local provenance verification unavailable."); return 1; }
    }

    public static async Task<int> RunAsync(Stream input, TextWriter diagnostics, string? destination,
        Func<HttpMessageHandler>? handlerFactory = null)
    {
        var trace = new AcquisitionTrace();
        try
        {
            if (Environment.GetEnvironmentVariable("AIQ_WP39_PROVIDER_APPROVED") != "1")
            { trace.Fail("preflight", "MISSING_APPROVAL"); throw new ResearchUnavailableException(); }
            if (string.IsNullOrWhiteSpace(destination))
            { trace.Fail("preflight", "INVALID_DESTINATION"); throw new ResearchUnavailableException(); }
            trace.Step("private_input");
            using var bytes = new MemoryStream(); var buffer = new byte[1024];
            using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(20));
            int n;
            while ((n = await input.ReadAsync(buffer, deadline.Token)) != 0)
            { if (bytes.Length + n > 4096) throw new ResearchUnavailableException(); bytes.Write(buffer, 0, n); }
            using var config = JsonDocument.Parse(bytes.ToArray());
            var c = config.RootElement;
            var names = c.EnumerateObject().Select(x => x.Name).ToArray();
            if (names.Length != 2 || names.Distinct(StringComparer.Ordinal).Count() != 2 || !names.Contains("providerKey") || !names.Contains("baseAssetVolumeConfirmed")) throw new ResearchUnavailableException();
            if (c.GetProperty("baseAssetVolumeConfirmed").ValueKind == JsonValueKind.False)
            { trace.Fail("private_input", "VOLUME_UNCONFIRMED"); throw new ResearchUnavailableException(); }
            if (c.GetProperty("baseAssetVolumeConfirmed").ValueKind != JsonValueKind.True) throw new ResearchUnavailableException();
            using var handler = handlerFactory?.Invoke() ?? new SocketsHttpHandler { AllowAutoRedirect = false, UseProxy = false };
            using var source = new TwelveDataMinuteCandleSource(handler, c.GetProperty("providerKey").GetString()!, true, diagnostic: trace.Observe);
            await AcquireCoreAsync(source, destination, DateTimeOffset.UtcNow, handlerFactory is not null, trace, deadline.Token);
        }
        catch (OperationCanceledException)
        {
            trace.Fail(trace.Stage, "TIMEOUT");
            await diagnostics.WriteLineAsync(trace.FailureJson()); return 1;
        }
        catch
        {
            if (trace.Stage == "private_input") trace.Fail("private_input", "INVALID_PRIVATE_INPUT");
            await diagnostics.WriteLineAsync(trace.FailureJson()); return 1;
        }
        await diagnostics.WriteLineAsync("Local provider bundle acquired; import and snapshot verification still required.");
        return 0;
    }
}
