using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Domain;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class HistoricalProductionCompositionAcceptanceTests
{
    [Theory]
    [InlineData("valid", "Available")]
    [InlineData("utf8", "Unavailable")]
    [InlineData("overflow", "Unavailable")]
    [InlineData("private-root", "Unavailable")]
    public async Task ActualPublicWorkerIsSecretFreeAndUsesBoundedRawStdin(string scenario, string expected)
    {
        var directory = Path.Combine(Path.GetTempPath(), "aiq-wp07-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        try
        {
            var start = new ProcessStartInfo("dotnet") { UseShellExecute = false, CreateNoWindow = true,
                RedirectStandardInput = true, RedirectStandardOutput = true, RedirectStandardError = true, WorkingDirectory = directory };
            start.ArgumentList.Add(Path.Combine(AppContext.BaseDirectory, "AIQuantTradingResearch.Worker.dll"));
            start.Environment.Clear();
            if (OperatingSystem.IsWindows()) start.Environment["SystemRoot"] = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            start.Environment["Worker__Mode"] = "HistoricalQuery";
            start.Environment["DOTNET_PROCESSOR_COUNT"] = "1";
            start.Environment["HistoricalMarketQuery__CacheDirectory"] = Path.Combine(directory,
                scenario == "private-root" ? "aiq-private-research-cache" : "aiq-public-synthetic-cache");
            using var process = Process.Start(start)!;
            using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(15));
            try
            {
                var stdout = process.StandardOutput.ReadToEndAsync(timeout.Token);
                var stderr = process.StandardError.ReadToEndAsync(timeout.Token);
                var request = scenario == "utf8" ? new byte[] { 0xc0, 0xaf } : Encoding.UTF8.GetBytes(
                    scenario == "overflow" ? new string(' ', 4097) : SyntheticTestFixture.RequestJson("1d", "1D"));
                await process.StandardInput.BaseStream.WriteAsync(request, timeout.Token); process.StandardInput.Close();
                await process.WaitForExitAsync(timeout.Token);
                var raw = await stdout;
                Assert.Equal(0, process.ExitCode); Assert.True(raw.Length < 65536);
                Assert.DoesNotContain("Twelve", raw); Assert.DoesNotContain("Vike", raw); Assert.DoesNotContain(directory, raw);
                Assert.Equal("", await stderr);
                using var json = JsonDocument.Parse(raw);
                Assert.Equal("aiq-historical-query-v2", json.RootElement.GetProperty("contractVersion").GetString());
                Assert.Equal(expected, json.RootElement.GetProperty("state").GetString());
                if (scenario == "valid")
                {
                    Assert.Single(json.RootElement.GetProperty("candles").EnumerateArray());
                    Assert.Single(json.RootElement.GetProperty("analytics").GetProperty("rows").EnumerateArray());
                }
            }
            finally { if (!process.HasExited) { process.Kill(entireProcessTree: true); await process.WaitForExitAsync(); } }
        }
        finally { if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true); }
    }

    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset To = From.AddDays(1);

    [Fact]
    public void HistoricalPipelineExecutionPublishesReadyEnvelopeFromCanonicalInputs()
    {
        var observations = new[]
        {
            new PriceObservation(From.AddHours(1), 10m),
            new PriceObservation(From.AddHours(2), 12.5m),
        };

        using var fixture = new HistoricalCompositionFixture(HistoricalObservationResult.Retrieved(observations));
        Assert.Equal(0, fixture.Execute());

        VisualizationReadModel model = Assert.IsType<VisualizationReadModel>(fixture.Store.Current);
        Assert.Equal(VisualizationPresentationState.Ready, model.State);
        Assert.Equal(VisualizationSourceMode.Historical, model.SourceMode);
        Assert.Equal(DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, model.SourceAuthority);
        Assert.Equal(2, model.ObservationCount);
        Assert.Equal(observations[^1].Instant, model.Latest!.SourceTime);
        Assert.Equal(12.5m, model.Latest.Price);
        Assert.Equal(VisualizationFeature.SimpleReturnLag1V1, model.Feature!.Identity);
        Assert.Equal(0.25m, model.Feature.Value);
        Assert.Equal(2, model.Feature.ObservationCount);
        Assert.Equal(2, model.Feature.RequiredObservationCount);
        Assert.NotNull(model.SnapshotIdentity);
        Assert.NotNull(model.DatasetVersion);
        Assert.Equal(model.SnapshotIdentity, model.DatasetVersion!.SnapshotIdentity);
        Assert.NotNull(model.Pipeline);
        Assert.True(model.Pipeline!.IsSuccess);
        Assert.Equal(5, model.Pipeline.Stages.Count);
        Assert.Equal(VisualizationRevisionKind.HistoricalPresentation, model.Revision.Kind);
        Assert.Equal((ulong)1, model.Revision.Value);
        Assert.Equal(observations.Select(static item => item.Instant), model.Window.Select(static item => item.SourceTime));
        Assert.Equal(observations.Select(static item => item.Price), model.Window.Select(static item => item.Price));
    }

    [Fact]
    public void HistoricalPipelineExecutionPublishesWarmUpEnvelopeFromCanonicalInputs()
    {
        var observation = new PriceObservation(From.AddHours(1), 10m);
        using var fixture = new HistoricalCompositionFixture(HistoricalObservationResult.Retrieved([observation]));
        Assert.Equal(0, fixture.Execute());

        VisualizationReadModel model = Assert.IsType<VisualizationReadModel>(fixture.Store.Current);
        Assert.Equal(VisualizationPresentationState.WarmUp, model.State);
        Assert.Equal(DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, model.SourceAuthority);
        Assert.Equal(1, model.ObservationCount);
        Assert.Equal(observation.Price, model.Latest!.Price);
        Assert.Equal(VisualizationFeature.SimpleReturnLag1V1, model.Feature!.Identity);
        Assert.Null(model.Feature.Value);
        Assert.Equal(1, model.Feature.ObservationCount);
        Assert.Equal(2, model.Feature.RequiredObservationCount);
        Assert.Equal(VisualizationRevisionKind.HistoricalPresentation, model.Revision.Kind);
        Assert.Equal((ulong)1, model.Revision.Value);
        Assert.NotNull(model.Pipeline);
        Assert.True(model.Pipeline!.IsSuccess);
    }

    [Fact]
    public void HistoricalPipelineExecutionPublishesGenuineEmptyEnvelopeFromCanonicalInputs()
    {
        using var fixture = new HistoricalCompositionFixture(HistoricalObservationResult.Retrieved([]));
        Assert.Equal(0, fixture.Execute());

        VisualizationReadModel model = Assert.IsType<VisualizationReadModel>(fixture.Store.Current);
        Assert.Equal(VisualizationPresentationState.Empty, model.State);
        Assert.Equal(DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, model.SourceAuthority);
        Assert.Empty(model.Window);
        Assert.Null(model.Feature);
        Assert.NotNull(model.SnapshotIdentity);
        Assert.NotNull(model.DatasetVersion);
        Assert.NotNull(model.Pipeline);
        Assert.True(model.Pipeline!.IsSuccess);
        Assert.Equal(VisualizationRevisionKind.HistoricalPresentation, model.Revision.Kind);
        Assert.Equal((ulong)1, model.Revision.Value);
    }

    [Fact]
    public void HistoricalPipelineExecutionPublishesSafeFailureEnvelopeFromCanonicalFailure()
    {
        using var fixture = new HistoricalCompositionFixture(HistoricalObservationResult.Failed(PersistenceFailure.Unavailable));
        Assert.Equal(1, fixture.Execute());

        VisualizationReadModel model = Assert.IsType<VisualizationReadModel>(fixture.Store.Current);
        Assert.Equal(VisualizationPresentationState.Failed, model.State);
        Assert.Equal(DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, model.SourceAuthority);
        Assert.Empty(model.Window);
        Assert.Null(model.Feature);
        Assert.NotNull(model.Failure);
        Assert.Equal("DependencyUnavailable", model.Failure!.Category);
        Assert.DoesNotContain("Exception", model.Failure.Message, StringComparison.OrdinalIgnoreCase);
        Assert.NotNull(model.Pipeline);
        Assert.False(model.Pipeline!.IsSuccess);
        Assert.Equal(VisualizationRevisionKind.HistoricalPresentation, model.Revision.Kind);
        Assert.Equal((ulong)1, model.Revision.Value);
    }

    private sealed class HistoricalCompositionFixture : IDisposable
    {
        private readonly string directory;
        private readonly ServiceProvider provider;
        private readonly object execution;
        private readonly object configuration;
        private readonly MethodInfo execute;

        public HistoricalCompositionFixture(HistoricalObservationResult result)
        {
            directory = Path.Combine(Path.GetTempPath(), $"aiq-wp04-historical-{Guid.NewGuid():N}");
            Directory.CreateDirectory(directory);
            var database = new SqliteStorageConfiguration(Path.Combine(directory, "historical.sqlite"));
            var services = new ServiceCollection();
            services.AddApplication();
            services.AddInfrastructure(new TwelveDataConfiguration("wp04-acceptance-key"), database);
            services.RemoveAll<IHistoricalObservationStore>();
            services.AddSingleton<IHistoricalObservationStore>(new FixedHistoricalObservationStore(result));

            Assembly workerAssembly = Assembly.Load("AIQuantTradingResearch.Worker");
            Type executionType = workerAssembly.GetType("AIQuantTradingResearch.Worker.PipelineExecution", throwOnError: true)!;
            Type configurationType = workerAssembly.GetType("AIQuantTradingResearch.Worker.PipelineExecutionConfiguration", throwOnError: true)!;
            services.AddTransient(executionType);
            provider = services.BuildServiceProvider(new ServiceProviderOptions { ValidateOnBuild = true, ValidateScopes = true });
            Store = provider.GetRequiredService<IVisualizationReadModelStore>();
            execution = provider.GetRequiredService(executionType);
            configuration = Activator.CreateInstance(
                configurationType,
                BindingFlags.Instance | BindingFlags.NonPublic,
                binder: null,
                args: [new DatasetDefinition("BTC", From, To)],
                culture: null)
                ?? throw new InvalidOperationException("Historical worker configuration was not created.");
            execute = executionType.GetMethod("Execute", BindingFlags.Instance | BindingFlags.Public)
                ?? throw new InvalidOperationException("Historical worker execution entry point was not found.");
        }

        public IVisualizationReadModelStore Store { get; }

        public int Execute() => (int)(execute.Invoke(execution, [configuration])
            ?? throw new InvalidOperationException("Historical worker execution returned no exit code."));

        public void Dispose()
        {
            provider.Dispose();
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(directory)) Directory.Delete(directory, recursive: true);
        }
    }

    private sealed class FixedHistoricalObservationStore(HistoricalObservationResult result) : IHistoricalObservationStore
    {
        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> observations) =>
            throw new NotSupportedException("The production-composition test only supplies the external Historical read boundary.");

        public HistoricalObservationResult Retrieve(string target) => result;
    }
}
