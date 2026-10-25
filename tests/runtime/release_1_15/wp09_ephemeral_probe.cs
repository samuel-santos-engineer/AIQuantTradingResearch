// Test-only friend-assembly driver; never copy into a release image.
using System;
using System.IO;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Worker;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
internal static class EphemeralProbe
{
    public static int Main(string[] args)
    {
        var directory = ResearchStorageBoundary.StagingDirectory;
        var stage = "entry";
        try
        {
            if (args[0] == "reject")
            {
                try { using var unexpected = new SqliteResearchStateStore(directory, true); return 2; }
                catch (ResearchUnavailableException) { Console.WriteLine("REJECTED"); return 0; }
            }
            if (args[0] == "symlink")
            {
                Directory.CreateDirectory("/tmp/wp09-target", UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute);
                Directory.CreateSymbolicLink(directory, "/tmp/wp09-target");
                try { using var unexpected = new SqliteResearchStateStore(directory, true); return 2; }
                catch (ResearchUnavailableException) { Console.WriteLine("SYMLINK_REJECTED"); return 0; }
            }
            if (args[0] == "permissions")
            {
                Directory.CreateDirectory(directory);
                File.SetUnixFileMode(directory, UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute | UnixFileMode.OtherRead);
                try { using var unexpected = new SqliteResearchStateStore(directory, true); return 2; }
                catch (ResearchUnavailableException) { Console.WriteLine("PERMISSIONS_REJECTED"); return 0; }
            }
            if (args[0] == "invalid-db")
            {
                ResearchStorageBoundary.Validate(directory);
                File.WriteAllText(Path.Combine(directory, "research.sqlite"), "not sqlite");
                Environment.SetEnvironmentVariable("AIQ_RESEARCH_PRIVATE_DIRECTORY", directory);
                Environment.SetEnvironmentVariable("AIQ_RESEARCH_API_KEY", "offline-nonsecret-test-api-key-12345678");
                Environment.SetEnvironmentVariable("AIQ_RESEARCH_TRANSPORT", "azure-app-service-http");
                Environment.SetEnvironmentVariable("AIQ_RESEARCH_PLATFORM_HTTPS_ONLY", "1");
                Environment.SetEnvironmentVariable("AIQ_RESEARCH_BIND_ADDRESS", "0.0.0.0");
                if (ResearchSnapshotEndpoint.RunAsync().GetAwaiter().GetResult() != 1) return 2;
                Console.WriteLine("INVALID_DATABASE_STARTUP_REJECTED"); return 0;
            }
            if (args[0] != "pass") return 2;
            var generation = Guid.NewGuid();
            stage = "open";
            using (var store = new SqliteResearchStateStore(directory, true, generation))
            {
                stage = "activate";
                store.ActivateTransform(new(generation, new byte[32], Guid.NewGuid(), 1), 0);
                try { using var second = new SqliteResearchStateStore(directory, true); return 2; }
                catch (ResearchUnavailableException) { }
            }
            stage = "reopen";
            using (var reopened = new SqliteResearchStateStore(directory, true))
                if (!reopened.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, active) => active.Generation == generation)) return 2;
            if (File.GetUnixFileMode(directory) != (UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute)) return 2;
            foreach (var file in Directory.EnumerateFiles(directory))
                if (File.GetUnixFileMode(file) != (UnixFileMode.UserRead | UnixFileMode.UserWrite)) return 2;
            Console.WriteLine("EPHEMERAL_STORE_REOPEN_AND_SINGLE_WRITER_PASS"); return 0;
        }
        catch (Exception e) { Console.WriteLine("PROBE_FAILED:" + stage + ":" + e.GetType().Name); return 1; }
    }
}
