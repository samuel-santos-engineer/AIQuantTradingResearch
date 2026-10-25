using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

/// <summary>Local qualification boundary only. No supported App Service durable writer backend exists.</summary>
internal static class ResearchStorageBoundary
{
    private static readonly string[] AzureMarkers = ["WEBSITE_INSTANCE_ID", "WEBSITE_SITE_NAME", "WEBSITE_HOSTNAME", "WEBSITE_ROLE_INSTANCE_ID"];
    internal static void ValidateHost(Func<string, string?> setting)
    {
        // No assertion/override can turn a local lock into a cross-instance fence.
        if (AzureMarkers.Any(name => setting(name) is not null) || setting("AIQ_RESEARCH_TRANSPORT") == "azure-app-service-http" ||
            setting("AIQ_RESEARCH_STORAGE_SCOPE") is { } scope && scope != "local-qualification")
            throw new ResearchUnavailableException();
    }
    internal static void Validate(string directory)
    {
        ValidateHost(Environment.GetEnvironmentVariable); // Before directory creation, lock acquisition or SQLite open.
        if (!OperatingSystem.IsLinux()) return; // Windows tests are not Linux storage qualification.
        try
        {
            var lines = File.ReadLines("/proc/self/mountinfo").Take(4097).ToArray();
            if (lines.Length > 4096) throw new ResearchUnavailableException();
            _ = ValidateLinux(Path.GetFullPath(directory), lines, File.ReadAllText("/proc/self/status"),
                Environment.GetEnvironmentVariable("AIQ_RESEARCH_EXPECTED_MOUNT_PATH"));
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException or ArgumentException or FormatException or OverflowException)
        { throw new ResearchUnavailableException(); }
    }
    internal sealed record Mount(string Device, string Root, string Path, string FileSystem, bool Writable);
    internal static Mount ValidateLinux(string directory, IEnumerable<string> lines, string status, string? expectedMount)
    {
        var uid = status.Split('\n').SingleOrDefault(line => line.StartsWith("Uid:", StringComparison.Ordinal))?
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        if (uid is null || uid.Length != 5 || uid.Skip(1).Any(value => value != "1654")) throw new ResearchUnavailableException();
        var mounts = new List<Mount>();
        foreach (var line in lines)
        {
            if (line.Length > 16384) throw new ResearchUnavailableException();
            var sides = line.Split(" - ", StringSplitOptions.None);
            if (sides.Length != 2) throw new ResearchUnavailableException();
            var left = sides[0].Split(' '); var right = sides[1].Split(' ');
            if (left.Length < 6 || right.Length < 3 || !int.TryParse(left[0], out var id) || id < 1 ||
                left[2].Split(':') is not { Length: 2 } device || device.Any(x => !uint.TryParse(x, out _)))
                throw new ResearchUnavailableException();
            var path = Decode(left[4]); var root = Decode(left[3]);
            if (!path.StartsWith('/') || !root.StartsWith('/')) throw new ResearchUnavailableException();
            if (path == "/" || directory == path || directory.StartsWith(path + "/", StringComparison.Ordinal))
                mounts.Add(new(left[2], root, path, right[0], left[5].Split(',').Contains("rw", StringComparer.Ordinal)));
        }
        var mount = mounts.OrderByDescending(x => x.Path.Length).FirstOrDefault() ?? throw new ResearchUnavailableException();
        // Overlay is accepted ONLY for disposable local tests, never a claim of durable hosting.
        if (!mount.Writable || mount.FileSystem is not ("ext4" or "xfs" or "btrfs" or "overlay") ||
            expectedMount is not null && expectedMount != mount.Path) throw new ResearchUnavailableException();
        return mount;
    }
    private static string Decode(string value) => value.Replace("\\040", " ", StringComparison.Ordinal)
        .Replace("\\011", "\t", StringComparison.Ordinal).Replace("\\012", "\n", StringComparison.Ordinal)
        .Replace("\\134", "\\", StringComparison.Ordinal);
}
