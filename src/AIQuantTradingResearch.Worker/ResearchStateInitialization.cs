using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

namespace AIQuantTradingResearch.Worker;

/// <summary>Operator-only bounded stdin configuration and private completed-observation import. No network.</summary>
public static class ResearchStateInitialization
{
    public static async Task<int> RunAsync(Stream input, TextWriter diagnostics, string? privateDirectory,
        string? importFile = null, CancellationToken cancellationToken = default)
    {
        using var deadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        deadline.CancelAfter(TimeSpan.FromSeconds(30));
        try
        {
            if (string.IsNullOrWhiteSpace(privateDirectory)) throw new ArgumentException("Research initialization unavailable.");
            using var secret = JsonDocument.Parse(await ReadBounded(input, 4096, deadline.Token));
            var config = secret.RootElement;
            Fields(config, "generation", "keyEpoch", "transformEpoch", "hmacKeyBase64", "apiKey");
            var generation = Uuid(config.GetProperty("generation").GetString());
            var keyEpoch = Uuid(config.GetProperty("keyEpoch").GetString());
            var epoch = config.GetProperty("transformEpoch").GetInt64();
            var key = Convert.FromBase64String(config.GetProperty("hmacKeyBase64").GetString()!);
            var apiKey = config.GetProperty("apiKey").GetString()!;
            if (apiKey.Length is < 32 or > 256 || apiKey.Any(c => c is < '!' or > '~') ||
                apiKey == Convert.ToBase64String(key) ||
                CryptographicOperations.FixedTimeEquals(SHA256.HashData(Encoding.ASCII.GetBytes(apiKey)), SHA256.HashData(key)))
                throw new ArgumentException("Research initialization unavailable.");
            var transform = new ResearchTransformConfiguration(generation, key, keyEpoch, epoch);
            // Parse and validate every import row before touching research state.
            var batches = importFile is null ? [] : await Import(importFile, deadline.Token);
            var existed = File.Exists(Path.Combine(privateDirectory, "research.sqlite"));
            if (!existed && epoch != 1) throw new ArgumentException("Research initialization unavailable.");
            using var store = new SqliteResearchStateStore(privateDirectory, true, generation);
            if (existed)
            {
                if (!store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, active) =>
                    active.Generation == generation && active.KeyEpoch == keyEpoch && active.TransformEpoch == epoch &&
                    CryptographicOperations.FixedTimeEquals(active.CopyKey(), key))) throw new ResearchUnavailableException();
            }
            else store.ActivateTransform(transform, 0, deadline.Token);
            foreach (var (request, batch) in batches)
            {
                deadline.Token.ThrowIfCancellationRequested();
                var prior = store.Read(request.Instrument);
                if (prior.Candles.Select(x => x.Candle).SequenceEqual(batch.Candles)) continue;
                // Existing initialized data may be retried exactly; incompatible re-import requires WP03 ingestion.
                if (prior.Version != 0) throw new ResearchUnavailableException();
                store.Commit(request, batch, generation, prior.Version, deadline.Token);
            }
            if (Enum.GetValues<ResearchInstrument>().Any(i => store.Read(i).Integrity != ResearchIntegrity.Protected))
                throw new ResearchUnavailableException();
            await diagnostics.WriteLineAsync("Research initialization complete; readiness depends on committed observations.");
            return 0;
        }
        catch (Exception)
        {
            // No exception, path, row, configuration or secret interpolation.
            await diagnostics.WriteLineAsync("Research initialization unavailable; preserve state for operator review.");
            return 1;
        }
    }
    private static async Task<byte[]> ReadBounded(Stream stream, int limit, CancellationToken token)
    {
        using var buffer = new MemoryStream(); var chunk = new byte[1024];
        while (true)
        {
            var n = await stream.ReadAsync(chunk, token); if (n == 0) break;
            if (buffer.Length + n > limit) throw new ArgumentException("Research initialization unavailable.");
            buffer.Write(chunk, 0, n);
        }
        var bytes = buffer.ToArray(); _ = new UTF8Encoding(false, true).GetString(bytes); return bytes;
    }
    private static void Fields(JsonElement value, params string[] names)
    {
        if (value.ValueKind != JsonValueKind.Object) throw new ArgumentException("Research initialization unavailable.");
        var actual = value.EnumerateObject().Select(x => x.Name).ToArray();
        if (actual.Length != names.Length || actual.Distinct(StringComparer.Ordinal).Count() != actual.Length ||
            actual.Except(names, StringComparer.Ordinal).Any()) throw new ArgumentException("Research initialization unavailable.");
    }
    private static Guid Uuid(string? text) => Guid.TryParseExact(text, "D", out var id) && id != Guid.Empty && text == id.ToString("D") ? id : throw new ArgumentException("Research initialization unavailable.");
    private static DateTimeOffset Utc(string? text) => DateTimeOffset.ParseExact(text!, "yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture,
        DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
    private static decimal Number(JsonElement value)
    {
        var text = value.GetString()!;
        if (text.Length is < 1 or > 32 || text.Any(c => c != '.' && (c < '0' || c > '9')) ||
            text.Count(c => c == '.') > 1 || text.EndsWith('.') || text.StartsWith('.') ||
            text.Contains('.') && text.Length - text.IndexOf('.') - 1 > 12) throw new ArgumentException("Research initialization unavailable.");
        return decimal.Parse(text, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
    }
    private static async Task<(ResearchPollRequest, ResearchPollBatch)[]> Import(string file, CancellationToken token)
    {
        var info = new FileInfo(Path.GetFullPath(file));
        for (var parent = info.Directory; parent is not null; parent = parent.Parent)
            if ((parent.Attributes & FileAttributes.ReparsePoint) != 0) throw new ArgumentException("Research initialization unavailable.");
        if ((info.Attributes & FileAttributes.ReparsePoint) != 0 || info.Length > 1_048_576 ||
            !OperatingSystem.IsWindows() && (File.GetUnixFileMode(info.FullName) &
                (UnixFileMode.GroupRead | UnixFileMode.GroupWrite | UnixFileMode.GroupExecute |
                 UnixFileMode.OtherRead | UnixFileMode.OtherWrite | UnixFileMode.OtherExecute)) != 0) throw new ArgumentException("Research initialization unavailable.");
        await using var stream = new FileStream(info.FullName, FileMode.Open, FileAccess.Read, FileShare.Read);
        using var doc = JsonDocument.Parse(await ReadBounded(stream, 1_048_576, token));
        var root = doc.RootElement; Fields(root, "source", "observedAtUtc", "BTC/USD", "ETH/USD");
        if (root.GetProperty("source").GetString() != "twelvedata") throw new ArgumentException("Research initialization unavailable.");
        var observed = Utc(root.GetProperty("observedAtUtc").GetString());
        if (observed > DateTimeOffset.UtcNow) throw new ArgumentException("Research initialization unavailable.");
        return Enum.GetValues<ResearchInstrument>().Select(instrument =>
        {
            var rows = root.GetProperty(instrument == ResearchInstrument.BtcUsd ? "BTC/USD" : "ETH/USD");
            if (rows.ValueKind != JsonValueKind.Array || rows.GetArrayLength() is < 1 or > 1000) throw new ArgumentException("Research initialization unavailable.");
            var candles = rows.EnumerateArray().Select(row =>
            {
                Fields(row, "openTimeUtc", "open", "high", "low", "close", "volume");
                return new CompletedResearchCandle(instrument, Utc(row.GetProperty("openTimeUtc").GetString()),
                    Number(row.GetProperty("open")), Number(row.GetProperty("high")), Number(row.GetProperty("low")),
                    Number(row.GetProperty("close")), Number(row.GetProperty("volume")));
            }).ToArray();
            var request = new ResearchPollRequest(instrument, observed, TimeSpan.Zero); var batch = new ResearchPollBatch(candles);
            ResearchInputValidation.Validate(request, batch); return (request, batch);
        }).ToArray();
    }
}
