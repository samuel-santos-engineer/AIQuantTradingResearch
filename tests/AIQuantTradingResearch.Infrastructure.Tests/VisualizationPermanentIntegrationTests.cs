using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Infrastructure.Visualization;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class VisualizationPermanentIntegrationTests
{
    [Fact]
    public void PiReadyRealReplayPipelinePublishesCanonicalReadyEnvelope()
    {
        using var fixture = new ReplayWorkerFixture(3);
        using JsonDocument json = fixture.Run();
        AssertEnvelope(json.RootElement, "ReplayLogicalTick", "Replay", 1, "Ready", 3);
        Assert.Equal(100.75m, json.RootElement.GetProperty("latest").GetProperty("price").GetDecimal());
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("feature").ValueKind);
    }

    [Fact]
    public void PiWarmUpRealReplayPipelinePublishesCanonicalWarmUpEnvelope()
    {
        using var fixture = new ReplayWorkerFixture(1);
        using JsonDocument json = fixture.Run();
        AssertEnvelope(json.RootElement, "ReplayLogicalTick", "Replay", 1, "WarmUp", 3);
        Assert.Equal(1, json.RootElement.GetProperty("feature").GetProperty("observationCount").GetInt32());
        Assert.Equal(2, json.RootElement.GetProperty("feature").GetProperty("requiredObservationCount").GetInt32());
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("feature").GetProperty("value").ValueKind);
    }

    [Fact]
    public void PiEmptyHistoricalCanonicalCompositionPublishesEmptyEnvelope()
    {
        using var fixture = new HistoricalCompositionFixture(HistoricalObservationResult.Retrieved([]));
        using JsonDocument json = fixture.Execute();
        AssertEnvelope(json.RootElement, "HistoricalPresentation", "Historical", 0, "Empty", 0);
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("latest").ValueKind);
        Assert.Equal(JsonValueKind.Null, json.RootElement.GetProperty("feature").ValueKind);
    }

    [Fact]
    public void PiFailedHistoricalCanonicalCompositionPublishesSafeFailedEnvelope()
    {
        using var fixture = new HistoricalCompositionFixture(HistoricalObservationResult.Failed(PersistenceFailure.Unavailable));
        using JsonDocument json = fixture.Execute(expectedExitCode: 1);
        AssertEnvelope(json.RootElement, "HistoricalPresentation", "Historical", 0, "Failed", 0);
        Assert.Equal("DependencyUnavailable", json.RootElement.GetProperty("failure").GetProperty("category").GetString());
        Assert.Contains("historical pipeline", json.RootElement.GetProperty("failure").GetProperty("message").GetString(), StringComparison.OrdinalIgnoreCase);
    }

    private static void AssertEnvelope(JsonElement root, string revisionKind, string sourceMode, int sourceAuthority, string state, int count)
    {
        Assert.Equal(VisualizationReadModel.ContractVersion, root.GetProperty("contractVersion").GetString());
        Assert.Equal(revisionKind, root.GetProperty("revision").GetProperty("kind").GetString());
        Assert.Equal(sourceMode, root.GetProperty("sourceMode").GetString());
        Assert.Equal(sourceAuthority, root.GetProperty("sourceAuthority").GetInt32());
        Assert.Equal(state, root.GetProperty("state").GetString());
        Assert.Equal(count, root.GetProperty("observationCount").GetInt32());
        Assert.Equal(count, root.GetProperty("window").GetArrayLength());
        Assert.Equal(state == "Failed" ? "Unavailable" : "NewlyPersisted", root.GetProperty("idempotencyStatus").GetString());
        Assert.Equal(state == "Failed" ? "Unavailable" : "Valid", root.GetProperty("dataQualityStatus").GetString());
    }

    private sealed class ReplayWorkerFixture : IDisposable
    {
        private readonly string root = Path.Combine(Path.GetTempPath(), $"aiq-wp09-replay-{Guid.NewGuid():N}");
        private readonly int count;
        public ReplayWorkerFixture(int count) { this.count = count; Directory.CreateDirectory(root); }
        public JsonDocument Run()
        {
            string repository = FindRepositoryRoot(); string handoff = Path.Combine(root, "visualization-read-model.json");
            var start = new ProcessStartInfo("dotnet") { WorkingDirectory = repository, RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false };
            start.ArgumentList.Add("run"); start.ArgumentList.Add("--project"); start.ArgumentList.Add(Path.Combine(repository, "src", "AIQuantTradingResearch.Worker", "AIQuantTradingResearch.Worker.csproj")); start.ArgumentList.Add("--no-build");
            start.Environment["TwelveData__ApiKey"] = "wp09-offline-key"; start.Environment["Persistence__DatabasePath"] = Path.Combine(root, "replay.sqlite"); start.Environment["Visualization__HandoffPath"] = handoff;
            start.Environment["Worker__Mode"] = "Replay"; start.Environment["Worker__Replay__ReplayIdentity"] = "simulated-live-replay-v1"; start.Environment["Worker__Replay__Target"] = "SIMULATED-USD"; start.Environment["Worker__Replay__StartingTick"] = "0"; start.Environment["Worker__Replay__RequestedObservationCount"] = count.ToString(CultureInfo.InvariantCulture);
            start.Environment["Dataset__Target"] = "SIMULATED-USD"; start.Environment["Dataset__From"] = "2024-01-01T00:00:00.0000000+00:00"; start.Environment["Dataset__To"] = "2024-01-01T00:03:00.0000000+00:00";
            using Process process = Process.Start(start) ?? throw new InvalidOperationException("Worker did not start."); string output = process.StandardOutput.ReadToEnd(); string error = process.StandardError.ReadToEnd();
            Assert.True(process.WaitForExit(30_000), "Replay Worker did not terminate."); Assert.True(process.ExitCode == 0, $"Replay Worker failed: {error}{output}"); Assert.True(File.Exists(handoff)); return JsonDocument.Parse(File.ReadAllText(handoff));
        }
        public void Dispose() { SqliteConnection.ClearAllPools(); if (Directory.Exists(root)) Directory.Delete(root, true); }
    }

    private sealed class HistoricalCompositionFixture : IDisposable
    {
        private readonly string directory = Path.Combine(Path.GetTempPath(), $"aiq-wp09-historical-{Guid.NewGuid():N}");
        private readonly ServiceProvider provider; private readonly object execution; private readonly object configuration; private readonly MethodInfo execute; private readonly string handoff;
        public HistoricalCompositionFixture(HistoricalObservationResult result)
        {
            Directory.CreateDirectory(directory); handoff = Path.Combine(directory, "visualization-read-model.json");
            var services = new ServiceCollection(); services.AddApplication(); services.AddInfrastructure(new TwelveDataConfiguration("wp09-offline-key"), new SqliteStorageConfiguration(Path.Combine(directory, "historical.sqlite")));
            services.RemoveAll<IHistoricalObservationStore>(); services.AddSingleton<IHistoricalObservationStore>(new FixedHistoricalObservationStore(result));
            var publisher = new VisualizationReadModelFilePublisher(handoff); publisher.StartSession(); services.RemoveAll<IVisualizationReadModelStore>(); services.AddSingleton<IVisualizationReadModelStore>(provider => new VisualizationReadModelFilePublishingStore(new AtomicVisualizationReadModelStore(), publisher));
            Assembly worker = Assembly.Load("AIQuantTradingResearch.Worker"); Type executionType = worker.GetType("AIQuantTradingResearch.Worker.PipelineExecution", true)!; Type configurationType = worker.GetType("AIQuantTradingResearch.Worker.PipelineExecutionConfiguration", true)!;
            services.AddTransient(executionType); provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true }); execution = provider.GetRequiredService(executionType);
            configuration = Activator.CreateInstance(configurationType, BindingFlags.Instance | BindingFlags.NonPublic, null, [new DatasetDefinition("BTC", DateTimeOffset.UnixEpoch, DateTimeOffset.UnixEpoch.AddDays(1))], null) ?? throw new InvalidOperationException("Historical configuration was not created."); execute = executionType.GetMethod("Execute") ?? throw new InvalidOperationException("Historical execution was not found.");
        }
        public JsonDocument Execute(int expectedExitCode = 0) { Assert.Equal(expectedExitCode, (int)(execute.Invoke(execution, [configuration]) ?? -1)); Assert.True(File.Exists(handoff)); return JsonDocument.Parse(File.ReadAllText(handoff)); }
        public void Dispose() { provider.Dispose(); SqliteConnection.ClearAllPools(); if (Directory.Exists(directory)) Directory.Delete(directory, true); }
    }

    private sealed class FixedHistoricalObservationStore(HistoricalObservationResult result) : IHistoricalObservationStore
    {
        public ObservationPersistenceResult Persist(string target, IReadOnlyList<AIQuantTradingResearch.Domain.PriceObservation> observations) => throw new NotSupportedException();
        public HistoricalObservationResult Retrieve(string target) => result;
    }

    private static string FindRepositoryRoot()
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return directory.FullName;
        throw new InvalidOperationException("Repository root was not found.");
    }
}
