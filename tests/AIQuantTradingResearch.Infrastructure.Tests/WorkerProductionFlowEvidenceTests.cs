using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class WorkerProductionFlowEvidenceTests
{
    [Fact]
    public void ActualWorkerEntryPointRunsReplayThroughProductionCompositionInProcess()
    {
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp03-evidence-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        string database = Path.Combine(root, "replay.sqlite");
        try
        {
            string rootPath = FindRepositoryRoot();
            var start = new ProcessStartInfo("dotnet")
            {
                WorkingDirectory = rootPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            start.ArgumentList.Add("run");
            start.ArgumentList.Add("--project");
            start.ArgumentList.Add(Path.Combine(rootPath, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj"));
            start.ArgumentList.Add("--no-build");
            start.ArgumentList.Add("--configuration");
            start.ArgumentList.Add("Release");
            start.Environment["TwelveData__ApiKey"] = "wp03-evidence-key";
            start.Environment["Persistence__DatabasePath"] = database;
            start.Environment["Visualization__HandoffPath"] = Path.Combine(root, "visualization-read-model.json");
            start.Environment["Worker__Mode"] = "Replay";
            start.Environment["Worker__Replay__ReplayIdentity"] = "simulated-live-replay-v1";
            start.Environment["Worker__Replay__Target"] = "SIMULATED-USD";
            start.Environment["Worker__Replay__StartingTick"] = "0";
            start.Environment["Worker__Replay__RequestedObservationCount"] = "1";
            start.Environment["Dataset__Target"] = "SIMULATED-USD";
            start.Environment["Dataset__From"] = "2024-01-01T00:00:00.0000000+00:00";
            start.Environment["Dataset__To"] = "2024-01-01T00:03:00.0000000+00:00";

            using var process = Process.Start(start) ?? throw new InvalidOperationException("Worker process did not start.");
            string standardOutput = process.StandardOutput.ReadToEnd();
            string standardError = process.StandardError.ReadToEnd();
            Assert.True(process.WaitForExit(30_000), "Worker process did not terminate.");
            Assert.True(process.ExitCode == 0, $"Worker failed: {standardError}{standardOutput}");

            using var connection = new SqliteConnection($"Data Source={database}");
            connection.Open();
            Assert.Equal(3L, Scalar(connection, "SELECT COUNT(*) FROM dataset_snapshots;"));
            Assert.Equal(3L, Scalar(connection, "SELECT COUNT(*) FROM dataset_snapshots WHERE source_authority = 1;"));
            Assert.Equal(0L, Scalar(connection, "SELECT COUNT(*) FROM dataset_snapshots WHERE source_authority = 0;"));
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }

    private static long Scalar(SqliteConnection connection, string sql)
    {
        using var command = connection.CreateCommand();
        command.CommandText = sql;
        return (long)(command.ExecuteScalar() ?? 0L);
    }

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx")))
            {
                return directory.FullName;
            }
        }

        throw new InvalidOperationException("Repository root was not found.");
    }
}
