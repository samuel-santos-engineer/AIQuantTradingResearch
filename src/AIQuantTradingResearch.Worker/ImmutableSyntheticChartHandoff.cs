using System.Security.Cryptography;
using System.Text.Json;

namespace AIQuantTradingResearch.Worker;

/// <summary>Explicit offline candidate writer. Never called by presentation or startup.</summary>
public static class ImmutableSyntheticChartHandoff
{
    public const string Schema = "aiq-chart-handoff-candidate-v1";
    public const int MemberLimit = 1_048_576;
    public const int TotalLimit = 4_194_304;
    private static readonly JsonSerializerOptions Json = new(JsonSerializerDefaults.Web);
    private static readonly string[] ForbiddenEnvironmentPrefixes = ["WEBSITE", "TWELVEDATA", "VIKE", "PERSISTENCE__", "WORKER__REPLAY__", "VISUALIZATION__"];
    public sealed record Member(string Name, int Bytes, string Sha256);
    public sealed record Manifest(string Schema, string GeneratorVersion, int Seed, IReadOnlyList<Member> Files);

    public static async Task<int> RunAsync(string[] args, TextWriter error)
    {
        if (args.Length != 2 || args[0] != "--write-immutable-chart-candidate" ||
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") != "Development" ||
            Environment.GetEnvironmentVariable("AIQ_IMMUTABLE_EXPORT") != "1" ||
            Environment.GetEnvironmentVariables().Keys.Cast<string>().Any(k =>
                ForbiddenEnvironmentPrefixes.Any(prefix => k.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))))
        { await error.WriteLineAsync("Offline candidate export requires explicit Development operator authority, never Azure."); return 1; }
        try
        {
            using var deadline = new CancellationTokenSource(TimeSpan.FromSeconds(60));
            var manifest = await WriteAsync(args[1], deadline.Token);
            await error.WriteLineAsync($"Candidate only; manifest SHA256={Hash(manifest)}; PUBLIC_RUNTIME_VERIFIED=False.");
            return 0;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or OperationCanceledException)
        { await error.WriteLineAsync("Candidate export failed; no promotion authorized."); return 1; }
    }

    public static async Task<byte[]> WriteAsync(string outputDirectory, CancellationToken cancellationToken = default)
    {
        var root = Path.GetFullPath(outputDirectory);
        if (Directory.Exists(root) || File.Exists(root)) throw new IOException("Output must be new.");
        for (var parent = Directory.GetParent(root); parent is not null; parent = parent.Parent)
            if (parent.Exists && (parent.Attributes & FileAttributes.ReparsePoint) != 0) throw new IOException("Linked ancestor.");
        Directory.CreateDirectory(root);
        if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(root, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
        var members = new List<Member>();
        var total = 0;
        foreach (var interval in new[] { "1h", "4h", "1d" })
        foreach (var range in new[] { "1D", "7D", "30D", "90D" })
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!LocalSyntheticChart.TrySelection(interval, range, out var i, out var r)) throw new InvalidOperationException();
            var payload = await LocalSyntheticChart.CreatePayloadAsync(i, r, cancellationToken);
            var bytes = Encode(payload);
            total += bytes.Length;
            if (bytes.Length > MemberLimit || total > TotalLimit - 8192) throw new IOException("Candidate size cap.");
            var name = $"{interval}-{range}.json";
            await WriteNewAsync(Path.Combine(root, name), bytes, cancellationToken);
            members.Add(new(name, bytes.Length, Hash(bytes)));
        }
        var manifest = Encode(new Manifest(Schema, "aiq-synthetic-ohlcv-v1", 114, members));
        if (manifest.Length > 8192) throw new IOException("Manifest cap.");
        await WriteNewAsync(Path.Combine(root, "manifest.json"), manifest, cancellationToken);
        return manifest;
    }

    private static byte[] Encode<T>(T value) => [.. JsonSerializer.SerializeToUtf8Bytes(value, Json), (byte)'\n'];
    public static string Hash(byte[] bytes) => Convert.ToHexString(SHA256.HashData(bytes)).ToLowerInvariant();
    private static async Task WriteNewAsync(string path, byte[] bytes, CancellationToken token)
    {
        await using var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await stream.WriteAsync(bytes, token);
        await stream.FlushAsync(token);
        if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(path, UnixFileMode.UserRead);
    }
}
