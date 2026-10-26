using AIQuantTradingResearch.Application.MarketData;
using System.Runtime.Versioning;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

/// <summary>Default local boundary, with one explicit non-durable staging exception; never App Service durability.</summary>
public static class ResearchStorageBoundary
{
    /// <summary>Operator-only bounded probe; no SQLite, provider, secrets or public endpoint.</summary>
    public static IReadOnlyDictionary<string, bool> ProbeStaging()
    {
        var result = new Dictionary<string, bool>
        {
            ["linux"] = OperatingSystem.IsLinux(), ["hostOptIn"] = false, ["kernel"] = false,
            ["privateWritable"] = false, ["accepted"] = false, ["probeCleanup"] = true
        };
        var created = false;
        try
        {
            if (!ValidateHost(Environment.GetEnvironmentVariable)) return result;
            result["hostOptIn"] = true;
            if (!OperatingSystem.IsLinux()) return result;
            var lines = File.ReadLines("/proc/self/mountinfo").Take(4097).ToArray();
            if (lines.Length > 4096) return result;
            _ = ValidateLinux(StagingDirectory, lines, File.ReadAllText("/proc/self/status"), "/", true);
            result["kernel"] = true;
            created = !Directory.Exists(StagingDirectory);
            Validate(StagingDirectory); // Unchanged real guard, including create/write/delete probe; never opens SQLite.
            result["privateWritable"] = result["accepted"] = true;
        }
        catch { } // Only fixed booleans, never exception/path/settings interpolation.
        finally
        {
            if (created)
            {
                try
                {
                    if (Directory.Exists(StagingDirectory) && !Directory.EnumerateFileSystemEntries(StagingDirectory).Any())
                        Directory.Delete(StagingDirectory); // Exact new empty probe directory only; no recursive deletion.
                    else if (Directory.Exists(StagingDirectory)) result["probeCleanup"] = false;
                }
                catch { result["probeCleanup"] = false; }
            }
        }
        return result;
    }
    private static readonly string[] AzureMarkers = ["WEBSITE_INSTANCE_ID", "WEBSITE_SITE_NAME", "WEBSITE_HOSTNAME", "WEBSITE_ROLE_INSTANCE_ID"];
    internal const string StagingScope = "azure-f1-staging-ephemeral";
    internal const string StagingDirectory = "/tmp/aiq-research";
    internal static bool ValidateHost(Func<string, string?> setting)
    {
        var scope = setting("AIQ_RESEARCH_STORAGE_SCOPE");
        var optIn = setting("AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING");
        if (scope == StagingScope && optIn == "1" &&
            setting("WEBSITE_SITE_NAME") == "aiqr115researchstagingwus27f5eabb5" &&
            setting("WEBSITE_HOSTNAME") == "aiqr115researchstagingwus27f5eabb5.azurewebsites.net" &&
            !string.IsNullOrWhiteSpace(setting("WEBSITE_INSTANCE_ID")) &&
            setting("WEBSITES_ENABLE_APP_SERVICE_STORAGE") == "false" &&
            setting("AIQ_RESEARCH_EXPECTED_MOUNT_PATH") == "/" &&
            setting("WEBSITE_ROLE_INSTANCE_ID") == "0") return true;
        // Presence of any marker, unknown scope, or partial opt-in still denies by default.
        if (AzureMarkers.Any(name => setting(name) is not null) || optIn is not null ||
            setting("AIQ_RESEARCH_TRANSPORT") == "azure-app-service-http" ||
            scope is not null && scope != "local-qualification")
            throw new ResearchUnavailableException();
        return false;
    }
    internal static void Validate(string directory)
    {
        var staging = ValidateHost(Environment.GetEnvironmentVariable);
        if (!OperatingSystem.IsLinux())
        {
            if (staging) throw new ResearchUnavailableException();
            return; // Existing Windows local tests are not Linux storage qualification.
        }
        try
        {
            var full = Path.GetFullPath(directory);
            if (staging && (directory != StagingDirectory || full != StagingDirectory)) throw new ResearchUnavailableException();
            var lines = File.ReadLines("/proc/self/mountinfo").Take(4097).ToArray();
            if (lines.Length > 4096) throw new ResearchUnavailableException();
            _ = ValidateLinux(full, lines, File.ReadAllText("/proc/self/status"),
                Environment.GetEnvironmentVariable("AIQ_RESEARCH_EXPECTED_MOUNT_PATH"), staging);
            if (staging) VerifyPrivateWritableDirectory(full);
        }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException or ArgumentException or FormatException or OverflowException or InvalidOperationException)
        { throw new ResearchUnavailableException(); }
    }
    [SupportedOSPlatform("linux")]
    internal static void VerifyPrivateWritableDirectory(string directory)
    {
        // Kernel mount and UID checks must precede this method. No symlink ancestor or entry is accepted.
        for (var current = new DirectoryInfo(directory); current is not null; current = current.Parent)
            if (current.Exists && (current.Attributes & FileAttributes.ReparsePoint) != 0) throw new ResearchUnavailableException();
        const UnixFileMode mode = UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute;
        Directory.CreateDirectory(directory, mode);
        if (File.GetUnixFileMode(directory) != mode) throw new ResearchUnavailableException();
        foreach (var entry in Directory.EnumerateFileSystemEntries(directory))
            if ((File.GetAttributes(entry) & FileAttributes.ReparsePoint) != 0) throw new ResearchUnavailableException();
        var probe = Path.Combine(directory, ".writable-" + Guid.NewGuid().ToString("N"));
        using var stream = new FileStream(probe, new FileStreamOptions
        {
            Mode = FileMode.CreateNew, Access = FileAccess.ReadWrite, Share = FileShare.None,
            Options = FileOptions.DeleteOnClose, UnixCreateMode = UnixFileMode.UserRead | UnixFileMode.UserWrite
        });
        stream.WriteByte(0); stream.Flush(); // No database/configuration contents; exact probe removed on close.
    }
    internal sealed record Mount(string Device, string Root, string Path, string FileSystem, bool Writable);
    internal static Mount ValidateLinux(string directory, IEnumerable<string> lines, string status, string? expectedMount, bool stagingEphemeral = false)
    {
        var uid = status.Split('\n').SingleOrDefault(line => line.StartsWith("Uid:", StringComparison.Ordinal))?
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        if (uid is null || uid.Length != 5 || uid.Skip(1).Any(value => value != "1654")) throw new ResearchUnavailableException();
        if (stagingEphemeral && (directory != StagingDirectory || expectedMount != "/")) throw new ResearchUnavailableException();
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
            if (stagingEphemeral && (left.Skip(6).Any(x => x.StartsWith("shared:", StringComparison.Ordinal) || x.StartsWith("master:", StringComparison.Ordinal)) ||
                path.StartsWith(directory + "/", StringComparison.Ordinal))) throw new ResearchUnavailableException();
            if (!path.StartsWith('/') || !root.StartsWith('/')) throw new ResearchUnavailableException();
            if (path == "/" || directory == path || directory.StartsWith(path + "/", StringComparison.Ordinal))
                mounts.Add(new(left[2], root, path, right[0], left[5].Split(',').Contains("rw", StringComparer.Ordinal)));
        }
        if (stagingEphemeral && mounts.Select(x => x.Path).Distinct(StringComparer.Ordinal).Count() != mounts.Count) throw new ResearchUnavailableException();
        var mount = mounts.OrderByDescending(x => x.Path.Length).FirstOrDefault() ?? throw new ResearchUnavailableException();
        if (stagingEphemeral && (mount.Path != "/" || mount.Root != "/" || mount.FileSystem != "overlay" ||
            !mount.Device.StartsWith("0:", StringComparison.Ordinal))) throw new ResearchUnavailableException();
        // Staging accepts ONLY the writable container-root overlay, never a bind/network/durable volume.
        if (!mount.Writable || mount.FileSystem is not ("ext4" or "xfs" or "btrfs" or "overlay") ||
            expectedMount is not null && expectedMount != mount.Path) throw new ResearchUnavailableException();
        return mount;
    }
    private static string Decode(string value) => value.Replace("\\040", " ", StringComparison.Ordinal)
        .Replace("\\011", "\t", StringComparison.Ordinal).Replace("\\012", "\n", StringComparison.Ordinal)
        .Replace("\\134", "\\", StringComparison.Ordinal);
}
