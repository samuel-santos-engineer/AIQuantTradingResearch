using System.Diagnostics;
using System.Globalization;
using AIQuantTradingResearch.Application.Integration;
using AIQuantTradingResearch.Infrastructure.PythonIntegration;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class PythonCapabilityInvokerTests
{
    [Fact]
    public async Task GovernedInvocationEmitsBoundedInteropActivityWithoutProtocolContamination()
    {
        var activities = new List<Activity>();
        using var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "AIQuantTradingResearch.Worker",
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
            ActivityStopped = activity => activities.Add(activity)
        };
        ActivitySource.AddActivityListener(listener);

        var result = await CreateGovernedInvoker().InvokeAsync(
            new CapabilityInvocationRequest(1, "health", "SECRET-CORRELATION", new Dictionary<string, string>()),
            TimeSpan.FromSeconds(10));

        var activity = Assert.Single(activities, item => item.OperationName == "interop.invoke");
        Assert.True(result.IsSuccess);
        Assert.Equal("interop", activity.GetTagItem("aiq.component"));
        Assert.Equal("interop.invoke", activity.GetTagItem("aiq.operation"));
        Assert.Equal("success", activity.GetTagItem("aiq.outcome"));
        Assert.DoesNotContain(activity.Tags, tag => tag.Value?.Contains("SECRET-CORRELATION", StringComparison.Ordinal) == true);
    }

    [Fact]
    public async Task GovernedEndpointSupportsDeterministicHealthAndEchoRoundTrips()
    {
        var invoker = CreateGovernedInvoker();

        var health = await invoker.InvokeAsync(Request("health"), TimeSpan.FromSeconds(10));
        var echo = await invoker.InvokeAsync(Request("echo", new Dictionary<string, string>
        {
            ["accent"] = "café",
            ["value"] = "safe"
        }), TimeSpan.FromSeconds(10));

        Assert.True(health.IsSuccess);
        Assert.Equal("available", health.Payload!["status"]);
        Assert.True(echo.IsSuccess);
        Assert.Equal("café", echo.Payload!["accent"]);
        Assert.Equal("safe", echo.Payload!["value"]);
    }

    [Fact]
    public async Task MissingInterpreterAndEntrypointAreBoundedConfigurationFailures()
    {
        var missingInterpreter = new PythonCapabilityInvoker(
            new PythonIntegrationConfiguration(Path.Combine(RepositoryRoot(), "missing-runtime")));
        var missingEntrypoint = CreateFixtureInvoker("missing.py");

        Assert.Equal(
            CapabilityInvocationFailure.ConfigurationUnavailable,
            (await missingInterpreter.InvokeAsync(Request("health"), TimeSpan.FromSeconds(1))).Failure);
        Assert.Equal(
            CapabilityInvocationFailure.ConfigurationUnavailable,
            (await missingEntrypoint.InvokeAsync(Request("health"), TimeSpan.FromSeconds(1))).Failure);
    }

    [Fact]
    public async Task UnsupportedContractVersionIsRejectedWithoutDowngrade()
    {
        var result = await CreateGovernedInvoker().InvokeAsync(Request("health", version: 99), TimeSpan.FromSeconds(10));

        Assert.Equal(CapabilityInvocationFailure.UnsupportedContractVersion, result.Failure);
    }

    [Theory]
    [InlineData("empty.py")]
    [InlineData("invalid_json.py")]
    [InlineData("multiple_frames.py")]
    [InlineData("wrong_version.py")]
    public async Task MalformedOrInvalidStdoutFailsClosed(string fixture)
    {
        var result = await CreateFixtureInvoker(fixture).InvokeAsync(Request("health"), TimeSpan.FromSeconds(10));

        Assert.Equal(CapabilityInvocationFailure.MalformedResponse, result.Failure);
    }

    [Fact]
    public async Task NonzeroExitWithStderrMapsToDependencyFailure()
    {
        var result = await CreateFixtureInvoker("nonzero.py").InvokeAsync(Request("health"), TimeSpan.FromSeconds(10));

        Assert.Equal(CapabilityInvocationFailure.DependencyFailure, result.Failure);
    }

    [Fact]
    public async Task ConcurrentStdoutAndStderrIsReadWithoutDeadlock()
    {
        var result = await CreateFixtureInvoker("concurrent_io.py").InvokeAsync(Request("health"), TimeSpan.FromSeconds(10));

        Assert.True(result.IsSuccess);
        Assert.Equal("available", result.Payload!["status"]);
    }

    [Fact]
    public async Task TimeoutTerminatesOnlyTheOwnedChildAndLeavesNoOrphan()
    {
        using var fixture = new TemporaryFixtureDirectory();
        var pidPath = Path.Combine(fixture.Path, "timeout.pid");
        var result = await CreateFixtureInvoker("wait_with_pid.py").InvokeAsync(
            Request("health", new Dictionary<string, string> { ["pidPath"] = pidPath }),
            TimeSpan.FromSeconds(2));

        Assert.Equal(CapabilityInvocationFailure.Timeout, result.Failure);
        Assert.True(File.Exists(pidPath));
        AssertProcessExited(int.Parse(File.ReadAllText(pidPath), CultureInfo.InvariantCulture));
    }

    [Fact]
    public async Task CancellationTerminatesOnlyTheOwnedChildAndPreservesAnUnrelatedPythonProcess()
    {
        using var fixture = new TemporaryFixtureDirectory();
        using var unrelated = StartUnrelatedFixture();
        try
        {
            var pidPath = Path.Combine(fixture.Path, "cancel.pid");
            using var cancellation = new CancellationTokenSource();
            var invocation = CreateFixtureInvoker("wait_with_pid.py").InvokeAsync(
                Request("health", new Dictionary<string, string> { ["pidPath"] = pidPath }),
                TimeSpan.FromSeconds(10),
                cancellation.Token);

            await WaitForFileAsync(pidPath);
            cancellation.Cancel();
            var result = await invocation;

            Assert.Equal(CapabilityInvocationFailure.Cancelled, result.Failure);
            AssertProcessExited(int.Parse(File.ReadAllText(pidPath), CultureInfo.InvariantCulture));
            Assert.False(unrelated.HasExited);
        }
        finally
        {
            if (!unrelated.HasExited) unrelated.Kill(entireProcessTree: true);
            unrelated.WaitForExit();
        }
    }

    private static PythonCapabilityInvoker CreateGovernedInvoker() =>
        new(new PythonIntegrationConfiguration(RepositoryRoot()));

    private static PythonCapabilityInvoker CreateFixtureInvoker(string fixture) =>
        new(RepositoryRoot(), GovernedInterpreter(), Path.Combine(AppContext.BaseDirectory, "PythonIntegrationFixtures", fixture));

    private static CapabilityInvocationRequest Request(
        string operation,
        IReadOnlyDictionary<string, string>? payload = null,
        int version = 1) =>
        new(version, operation, "wp11-test", payload ?? new Dictionary<string, string>());

    private static string GovernedInterpreter() => Path.Combine(
        RepositoryRoot(), ".venv", OperatingSystem.IsWindows() ? "Scripts\\python.exe" : "bin/python");

    private static string RepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName;
        }

        throw new InvalidOperationException("Repository root was not found.");
    }

    private static Process StartUnrelatedFixture()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = GovernedInterpreter(),
            WorkingDirectory = RepositoryRoot(),
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        startInfo.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory, "PythonIntegrationFixtures", "hold.py"));
        return Process.Start(startInfo) ?? throw new InvalidOperationException("Unrelated fixture did not start.");
    }

    private static async Task WaitForFileAsync(string path)
    {
        var deadline = DateTime.UtcNow + TimeSpan.FromSeconds(5);
        while (!File.Exists(path) && DateTime.UtcNow < deadline)
        {
            await Task.Delay(20);
        }

        Assert.True(File.Exists(path), "The controlled child did not report readiness.");
    }

    private static void AssertProcessExited(int processId)
    {
        Assert.Throws<ArgumentException>(() => Process.GetProcessById(processId));
    }

    private sealed class TemporaryFixtureDirectory : IDisposable
    {
        public TemporaryFixtureDirectory()
        {
            Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "aiq-wp11-" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(Path);
        }

        public string Path { get; }

        public void Dispose()
        {
            if (Directory.Exists(Path)) Directory.Delete(Path, recursive: true);
        }
    }
}
