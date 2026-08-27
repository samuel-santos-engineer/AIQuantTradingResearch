using System.Diagnostics;
using System.Net.Sockets;
using System.Text.Json;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class WP08LifecycleDemonstrationTests
{
    [Fact] public void RestartR0FreshSingleWorker() => RunRestartDiagnostic("R0", false, false, true);
    [Fact] public void RestartR1SecondWorkerOnly() => RunRestartDiagnostic("R1", false, false, false);
    [Fact] public void RestartR2SecondWorkerWithStreamlit() => RunRestartDiagnostic("R2", true, false, false);
    [Fact] public void RestartR3SecondWorkerWithProbe() => RunRestartDiagnostic("R3", false, true, false);
    [Fact] public void RestartR4SecondWorkerFullTopology() => RunRestartDiagnostic("R4", true, true, false);
    [Fact] public void RestartR1FFreshRuntimeSplit() => RunRestartDiagnostic("R1F", false, false, false);
    [Fact] public void RestartRFHandoffReuseSplit() => RunRestartDiagnostic("RF-HANDOFF", false, false, false);
    [Fact] public void RestartRFDatabaseReuseSplit() => RunRestartDiagnostic("RF-DB", false, false, false);
    [Fact] public void RestartRFRuntimeResidueSplit() => RunRestartDiagnostic("RF-RUNTIME", false, false, false);

    private static void RunRestartDiagnostic(string name, bool withStreamlit, bool withProbe, bool freshSingle)
    {
        if (!OperatingSystem.IsWindows()) return;
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp08-{name}-{Guid.NewGuid():N}"); Directory.CreateDirectory(root);
        string handoff = Path.Combine(root, "visualization-read-model.json"); string database = Path.Combine(root, "replay.sqlite");
        Process? streamlit = null; WindowsIsolatedProcessGroup? a = null; WindowsIsolatedProcessGroup? b = null;
        try
        {
            int port = ReserveLoopbackPort();
            if (withStreamlit) { streamlit = StartStreamlit(handoff, port); Assert.True(WaitUntil(() => !streamlit.HasExited && CanConnect(port), TimeSpan.FromSeconds(10))); }
            a = StartWorker(handoff, database); Assert.True(WaitUntil(() => ReadRevision(handoff) >= 1, TimeSpan.FromSeconds(8)));
            a.RequestCtrlBreak(); Assert.True(a.WaitForExit(TimeSpan.FromSeconds(5))); Assert.Equal(0, a.ExitCode); a.Dispose(); a = null;
            if (freshSingle)
            {
                Console.WriteLine($"WP08 {name}: control first-worker exit=0");
                return;
            }
            if (name == "R1F")
            {
                string freshRoot = Path.Combine(root, "fresh-runtime");
                Directory.CreateDirectory(freshRoot); handoff = Path.Combine(freshRoot, "visualization-read-model.json"); database = Path.Combine(freshRoot, "replay.sqlite");
            }
            if (name == "RF-HANDOFF") database = Path.Combine(root, "fresh-database", "replay.sqlite");
            if (name == "RF-DB") handoff = Path.Combine(root, "fresh-handoff", "visualization-read-model.json");
            if (name == "RF-RUNTIME") { handoff = Path.Combine(root, "fresh-runtime-handoff", "visualization-read-model.json"); database = Path.Combine(root, "fresh-runtime-db", "replay.sqlite"); }
            if (!name.StartsWith("RF", StringComparison.Ordinal) && name != "R1F") { Assert.True(File.Exists(handoff)); Assert.True(File.Exists(database)); }
            Directory.CreateDirectory(Path.GetDirectoryName(handoff)!); Directory.CreateDirectory(Path.GetDirectoryName(database)!);
            string priorHandoff = File.Exists(handoff) ? File.ReadAllText(handoff) : string.Empty;
            DateTime priorHandoffWriteTime = File.Exists(handoff) ? File.GetLastWriteTimeUtc(handoff) : DateTime.MinValue;
            b = StartWorker(handoff, database);
            bool requiresChangedPayload = !string.Equals(name, "RF-HANDOFF", StringComparison.Ordinal);
            Assert.True(WaitUntil(() => ReadRevision(handoff) >= 1 &&
                (requiresChangedPayload
                    ? ReadHandoff(handoff) != priorHandoff
                    : File.Exists(handoff) && File.GetLastWriteTimeUtc(handoff) > priorHandoffWriteTime), TimeSpan.FromSeconds(10)));
            if (withProbe) { using JsonDocument probe = RunProbe(handoff); }
            if (withStreamlit) Assert.True(CanConnect(port));
            b.RequestCtrlBreak(); Assert.True(b.WaitForExit(TimeSpan.FromSeconds(5)));
            var diagnostic = b.DiagnosticSnapshot;
            Console.WriteLine($"WP08 {name}: B={b.ExitCode}; signal={diagnostic.SignalResult}/{diagnostic.SignalWin32Error}; stdout={diagnostic.Stdout.Text}; stderr={diagnostic.Stderr.Text}");
            Assert.Equal(0, b.ExitCode);
            Console.WriteLine($"WP08 {name}: A disposed before B; streamlit={withStreamlit}; probe={withProbe}; B={b.ExitCode}; handoff={File.Exists(handoff)}; database={File.Exists(database)}");
        }
        finally
        {
            DisposeWorker(a); DisposeWorker(b);
            if (streamlit is not null) { if (!streamlit.HasExited) { streamlit.Kill(true); streamlit.WaitForExit(5000); } streamlit.Dispose(); }
            if (Directory.Exists(root)) Directory.Delete(root, true);
        }
    }

    [Fact] public void DiagnosticAWorkerOnly() => RunCtrlBreakTopologyScenario("A", false, false);
    [Fact] public void DiagnosticBWorkerAndStreamlit() => RunCtrlBreakTopologyScenario("B", true, false);
    [Fact] public void DiagnosticCWorkerAndProbe() => RunCtrlBreakTopologyScenario("C", false, true);
    [Fact] public void DiagnosticDWorkerStreamlitAndProbe() => RunCtrlBreakTopologyScenario("D", true, true);

    private static void RunCtrlBreakTopologyScenario(string name, bool launchStreamlit, bool runProbe)
    {
        if (!OperatingSystem.IsWindows()) return;
        Stopwatch clock = Stopwatch.StartNew();
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp08-diagnostic-{name}-{Guid.NewGuid():N}"); Directory.CreateDirectory(root);
        WindowsIsolatedProcessGroup? worker = null; Process? streamlit = null;
        try
        {
            string handoff = Path.Combine(root, "visualization-read-model.json"); string database = Path.Combine(root, "replay.sqlite");
            TimeSpan launch = clock.Elapsed;
            int port = ReserveLoopbackPort();
            if (launchStreamlit)
            {
                streamlit = StartStreamlit(handoff, port);
                Assert.True(WaitUntil(() => !streamlit.HasExited && CanConnect(port), TimeSpan.FromSeconds(10)));
            }
            worker = StartWorker(handoff, database);
            Assert.True(WaitUntil(() => File.Exists(handoff), TimeSpan.FromSeconds(10)));
            TimeSpan p1 = clock.Elapsed;
            Assert.True(WaitUntil(() => ReadRevision(handoff) >= 1, TimeSpan.FromSeconds(8)));
            TimeSpan p2 = clock.Elapsed;
            int? probePid = null; TimeSpan? probeExit = null;
            if (runProbe)
            {
                JsonDocument probe = RunProbe(handoff); probe.Dispose(); probeExit = clock.Elapsed;
            }
            TimeSpan signalAt = clock.Elapsed; worker.RequestCtrlBreak(); TimeSpan signalReturned = clock.Elapsed;
            Assert.True(worker.WaitForExit(TimeSpan.FromSeconds(5)));
            int exit = worker.ExitCode; TimeSpan workerExit = clock.Elapsed;
            var d = worker.DiagnosticSnapshot;
            Console.WriteLine($"WP08 {name}: worker={d.ProcessId}/{d.ProcessGroupId}; streamlit={streamlit?.Id}; probe={probePid}; signal={d.SignalResult}/{d.SignalWin32Error}; exit={exit}; p1={p1}; p2={p2}; signal={signalAt}->{signalReturned}; exitAt={workerExit}; stdoutTruncated={d.Stdout.Truncated}; stderrTruncated={d.Stderr.Truncated}; stdout={d.Stdout.Text}; stderr={d.Stderr.Text}");
        }
        finally
        {
            DisposeWorker(worker);
            if (streamlit is not null) { if (!streamlit.HasExited) { streamlit.Kill(true); streamlit.WaitForExit(5000); } streamlit.Dispose(); }
            if (Directory.Exists(root)) Directory.Delete(root, true);
        }
    }

    [Fact]
    public void WindowsFiniteDemonstrationProvesStreamlitProbeRestartAndOwnedResidue()
    {
        if (!OperatingSystem.IsWindows()) return;
        Stopwatch total = Stopwatch.StartNew();
        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp08-full-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        WindowsIsolatedProcessGroup? workerA = null;
        WindowsIsolatedProcessGroup? workerB = null;
        Process? streamlit = null;
        try
        {
            string handoff = Path.Combine(root, "visualization-read-model.json");
            string database = Path.Combine(root, "replay.sqlite");
            int port = ReserveLoopbackPort();
            streamlit = StartStreamlit(handoff, port);
            Assert.True(WaitUntil(() => streamlit is not null && !streamlit.HasExited && CanConnect(port), TimeSpan.FromSeconds(10)), "Streamlit did not become ready on the owned loopback port.");

            workerA = StartWorker(handoff, database);
            int p2 = WaitForP2(handoff);
            Assert.False(workerA.WaitForExit(TimeSpan.Zero));
            JsonDocument probe = RunProbe(handoff);
            using (probe)
            {
                Assert.Equal("aiq-wp08-presentation-chain-probe-v1", probe.RootElement.GetProperty("contract").GetString());
                Assert.Equal(p2, probe.RootElement.GetProperty("source").GetProperty("revisionValue").GetInt32());
                Assert.Equal(p2, probe.RootElement.GetProperty("frame").GetProperty("revisionValue").GetInt32());
                Assert.Equal(5, probe.RootElement.GetProperty("sections").GetArrayLength());
                Assert.Equal("Feature", probe.RootElement.GetProperty("sections")[0].GetProperty("label").GetString());
                Assert.Equal("Idempotency", probe.RootElement.GetProperty("sections")[4].GetProperty("label").GetString());
            }
            Assert.True(CanConnect(port), "Streamlit listener was not independently available during Worker/probe observation.");
            workerA.RequestCtrlBreak();
            Assert.True(workerA.WaitForExit(TimeSpan.FromSeconds(5)));
            Assert.Equal(0, workerA.ExitCode);
            workerA.Dispose();
            workerA = null;

            string priorWorkerAHandoff = File.Exists(handoff) ? File.ReadAllText(handoff) : string.Empty;
            workerB = StartWorker(handoff, database);
            Assert.True(WaitUntil(() => ReadRevision(handoff) >= 1 && ReadHandoff(handoff) != priorWorkerAHandoff, TimeSpan.FromSeconds(10)), "Worker B did not produce a new-session handoff.");
            workerB.RequestCtrlBreak();
            Assert.True(workerB.WaitForExit(TimeSpan.FromSeconds(5)));
            Assert.Equal(0, workerB.ExitCode);
            Assert.True(CanConnect(port));
        }
        finally
        {
            DisposeWorker(workerA); DisposeWorker(workerB);
            if (streamlit is not null)
            {
                if (!streamlit.HasExited) { streamlit.Kill(entireProcessTree: true); streamlit.WaitForExit(5_000); }
                streamlit.Dispose();
            }
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
        Assert.InRange(total.Elapsed, TimeSpan.Zero, TimeSpan.FromSeconds(30));
    }

    [Fact]
    public void WindowsWorkerPublishesP1ThenNewerP2BeforeGracefulTargetedCtrlBreak()
    {
        if (!OperatingSystem.IsWindows()) return;

        string root = Path.Combine(Path.GetTempPath(), $"aiq-wp08-{Guid.NewGuid():N}");
        Directory.CreateDirectory(root);
        WindowsIsolatedProcessGroup? worker = null;
        try
        {
            string handoff = Path.Combine(root, "visualization-read-model.json");
            string database = Path.Combine(root, "replay.sqlite");
            worker = WindowsIsolatedProcessGroup.Launch(WorkerExecutable(), ["--wp08-test-liveness"], RepositoryRoot(), WorkerEnvironment(handoff, database));
            Assert.NotEqual(Environment.ProcessId, worker.ProcessId);
            Assert.NotEqual(0, worker.ProcessGroupId);
            Assert.True(WaitUntil(() => File.Exists(handoff), TimeSpan.FromSeconds(10)), "Worker did not publish its first handoff within the binding timeout.");
            Assert.False(worker.WaitForExit(TimeSpan.Zero), "Worker naturally completed before liveness cancellation.");
            using (JsonDocument document = JsonDocument.Parse(File.ReadAllText(handoff)))
            {
                Assert.Equal("aiq-visualization-read-model-v1", document.RootElement.GetProperty("contractVersion").GetString());
            }

            Stopwatch refresh = Stopwatch.StartNew();
            Assert.True(WaitUntil(() => ReadRevision(handoff) >= 1, TimeSpan.FromSeconds(8)), "Worker did not publish the normal next-tick P2 handoff within the binding refresh window.");
            Assert.InRange(refresh.Elapsed, TimeSpan.Zero, TimeSpan.FromSeconds(8));
            Assert.False(worker.WaitForExit(TimeSpan.Zero), "Worker did not remain alive in its post-P2 cancellation hold.");

            worker.RequestCtrlBreak();
            Assert.True(worker.WaitForExit(TimeSpan.FromSeconds(5)), "Worker did not exit gracefully after targeted CTRL_BREAK.");
            Assert.Equal(0, worker.ExitCode);
        }
        finally
        {
            if (worker is not null)
            {
                try
                {
                    if (!worker.WaitForExit(TimeSpan.Zero))
                    {
                        worker.TerminateOwnedProcessForCleanup();
                        worker.WaitForExit(TimeSpan.FromSeconds(2));
                    }
                }
                finally { worker.Dispose(); }
            }
            if (Directory.Exists(root)) Directory.Delete(root, recursive: true);
        }
    }

    [Theory]
    [InlineData("plain", "plain")]
    [InlineData("with space", "\"with space\"")]
    [InlineData("a\\\"b", "\"a\\\\\\\"b\"")]
    public void WindowsCommandLineQuotingIsDeterministic(string input, string expected) => Assert.Equal(expected, WindowsIsolatedProcessGroup.Quote(input));

    private static Dictionary<string, string> WorkerEnvironment(string handoff, string database) => new(StringComparer.OrdinalIgnoreCase)
    {
        ["TwelveData__ApiKey"] = "wp08-test-key",
        ["Persistence__DatabasePath"] = database,
        ["Visualization__HandoffPath"] = handoff,
        ["Worker__Mode"] = "Replay",
        ["Worker__Replay__ReplayIdentity"] = "simulated-live-replay-v1",
        ["Worker__Replay__Target"] = "SIMULATED-USD",
        ["Worker__Replay__StartingTick"] = "0",
        ["Worker__Replay__RequestedObservationCount"] = "1",
        ["Dataset__Target"] = "SIMULATED-USD",
        ["Dataset__From"] = "2024-01-01T00:00:00.0000000+00:00",
        ["Dataset__To"] = "2024-01-01T00:03:00.0000000+00:00",
    };

    private static WindowsIsolatedProcessGroup StartWorker(string handoff, string database) =>
        WindowsIsolatedProcessGroup.Launch(WorkerExecutable(), ["--wp08-test-liveness"], RepositoryRoot(), WorkerEnvironment(handoff, database));

    private static int WaitForP2(string handoff)
    {
        Assert.True(WaitUntil(() => ReadRevision(handoff) >= 1, TimeSpan.FromSeconds(8)));
        return ReadRevision(handoff);
    }

    private static Process StartStreamlit(string handoff, int port)
    {
        var start = new ProcessStartInfo(Path.Combine(RepositoryRoot(), ".venv", "Scripts", "python.exe")) { WorkingDirectory = Path.Combine(RepositoryRoot(), "python", "presentation"), UseShellExecute = false };
        start.ArgumentList.Add("-m"); start.ArgumentList.Add("streamlit"); start.ArgumentList.Add("run"); start.ArgumentList.Add("realtime_financial_visualization.py"); start.ArgumentList.Add("--server.address"); start.ArgumentList.Add("127.0.0.1"); start.ArgumentList.Add("--server.port"); start.ArgumentList.Add(port.ToString(System.Globalization.CultureInfo.InvariantCulture)); start.ArgumentList.Add("--server.headless"); start.ArgumentList.Add("true");
        start.Environment["Visualization__HandoffPath"] = handoff;
        return Process.Start(start) ?? throw new InvalidOperationException("Streamlit process did not start.");
    }

    private static JsonDocument RunProbe(string handoff)
    {
        var start = new ProcessStartInfo(Path.Combine(RepositoryRoot(), ".venv", "Scripts", "python.exe")) { WorkingDirectory = Path.Combine(RepositoryRoot(), "python", "presentation"), UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true };
        start.ArgumentList.Add("wp08_presentation_chain_probe.py"); start.ArgumentList.Add("--handoff"); start.ArgumentList.Add(handoff);
        using Process probe = Process.Start(start) ?? throw new InvalidOperationException("Probe did not start.");
        string stdout = probe.StandardOutput.ReadToEnd(); string stderr = probe.StandardError.ReadToEnd();
        Assert.True(probe.WaitForExit(2_000), "Probe exceeded its two-second bound.");
        Assert.True(probe.ExitCode == 0, stderr);
        return JsonDocument.Parse(stdout);
    }

    private static int ReserveLoopbackPort()
    {
        var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0); listener.Start();
        int port = ((System.Net.IPEndPoint)listener.LocalEndpoint).Port; listener.Stop(); return port;
    }

    private static bool CanConnect(int port)
    {
        try { using var client = new TcpClient(); client.Connect("127.0.0.1", port); return true; } catch (SocketException) { return false; }
    }

    private static void DisposeWorker(WindowsIsolatedProcessGroup? worker)
    {
        if (worker is null) return;
        try { if (!worker.WaitForExit(TimeSpan.Zero)) { worker.TerminateOwnedProcessForCleanup(); worker.WaitForExit(TimeSpan.FromSeconds(2)); } } finally { worker.Dispose(); }
    }

    private static bool WaitUntil(Func<bool> condition, TimeSpan timeout)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        while (stopwatch.Elapsed < timeout)
        {
            if (condition()) return true;
            Thread.Sleep(250);
        }
        return condition();
    }

    private static int ReadRevision(string handoff)
    {
        if (!File.Exists(handoff)) return -1;
        try
        {
            using JsonDocument document = JsonDocument.Parse(File.ReadAllText(handoff));
            return document.RootElement.GetProperty("revision").GetProperty("value").GetInt32();
        }
        catch (IOException) { return -1; }
        catch (JsonException) { return -1; }
    }

    private static string ReadHandoff(string handoff)
    {
        try { return File.Exists(handoff) ? File.ReadAllText(handoff) : string.Empty; }
        catch (IOException) { return string.Empty; }
    }

    private static string RepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName;
        }
        throw new InvalidOperationException("Repository root was not found.");
    }

    private static string WorkerExecutable() => Path.Combine(RepositoryRoot(), "src", "AIQuantTradingResearch.Worker", "bin", "Debug", "net10.0", "AIQuantTradingResearch.Worker.exe");
}
