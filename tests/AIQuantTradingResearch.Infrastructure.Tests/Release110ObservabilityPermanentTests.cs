using System.Diagnostics;
using System.Diagnostics.Metrics;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class Release110ObservabilityPermanentTests
{
    [Fact]
    public void InfrastructureOwnersEmitExactObservations()
    {
        var source = Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs");
        var snapshot = Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteDatasetSnapshotStore.cs");

        Assert.Contains("StartProviderRetrieve()", source, StringComparison.Ordinal);
        Assert.Contains("InfrastructureObservability.StartPersistence(InfrastructureObservability.SnapshotStoreOperation)", snapshot, StringComparison.Ordinal);
        Assert.Contains("InfrastructureObservability.StartPersistence(InfrastructureObservability.SnapshotRetrieveOperation)", snapshot, StringComparison.Ordinal);
        Assert.Contains("provider.operation", source, StringComparison.Ordinal);
        Assert.Contains("persistence.operation", source, StringComparison.Ordinal);
        Assert.Contains("provider.operations", source, StringComparison.Ordinal);
        Assert.Contains("persistence.duration", source, StringComparison.Ordinal);
    }

    [Fact]
    public void InfrastructureFailureAndParentContractsAreBounded()
    {
        var source = Read("src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs");

        Assert.Contains("AIQuantTradingResearch.Infrastructure", source, StringComparison.Ordinal);
        Assert.Contains("failure", source, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("exception.Message", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SetTag(\"aiq.target\"", source, StringComparison.Ordinal);
        Assert.DoesNotContain("SetTag(\"aiq.path\"", source, StringComparison.Ordinal);
    }

    [Fact]
    public void ProviderFailureProducesBoundedActivityAndMetric()
    {
        var activities = new List<Activity>();
        var instruments = new List<(string Name, string? Unit)>();
        using var parent = new Activity(nameof(ProviderFailureProducesBoundedActivityAndMetric)).Start();
        using var activityListener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "AIQuantTradingResearch.Infrastructure",
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
            ActivityStopped = activity => activities.Add(activity),
        };
        ActivitySource.AddActivityListener(activityListener);
        using var meterListener = new MeterListener();
        meterListener.InstrumentPublished = (instrument, listener) =>
        {
            if (instrument.Meter.Name == "AIQuantTradingResearch.Infrastructure")
            {
                instruments.Add((instrument.Name, instrument.Unit));
                listener.EnableMeasurementEvents(instrument);
            }
        };
        meterListener.Start();

        var result = new SqliteHistoricalObservationStore(new UnavailableConnectionFactory()).Retrieve("SIMULATED-USD");

        Assert.False(result.IsSuccess);
        var activity = Assert.Single(activities, item => item.OperationName == "provider.operation");
        Assert.Equal(parent.SpanId, activity.ParentSpanId);
        Assert.Equal("historical-observation.retrieve", activity.GetTagItem("aiq.operation"));
        Assert.Equal("failed", activity.GetTagItem("aiq.outcome"));
        Assert.Equal("unavailable", activity.GetTagItem("aiq.error_class"));
        Assert.DoesNotContain(activity.Tags, tag => tag.Key is "aiq.target" or "aiq.path");
        Assert.Contains(("provider.operations", "{operation}"), instruments);
        Assert.Contains(("provider.duration", "ms"), instruments);
        Assert.Contains(("provider.failures", "{operation}"), instruments);
    }

    [Fact]
    public void WorkerLifecycleIsBoundedAndExporterFree()
    {
        var worker = Read("src/AIQuantTradingResearch.Worker/WorkerObservabilityLifecycle.cs");
        var program = Read("src/AIQuantTradingResearch.Worker/Program.cs");

        Assert.Contains("worker.lifecycle", worker, StringComparison.Ordinal);
        Assert.Contains("worker.lifecycle.events", worker, StringComparison.Ordinal);
        Assert.Contains("using var observability = new WorkerObservabilityLifecycle", program, StringComparison.Ordinal);
        Assert.DoesNotContain("OpenTelemetry.Exporter", worker, StringComparison.Ordinal);
        Assert.DoesNotContain("Streamlit", program, StringComparison.OrdinalIgnoreCase);
    }

    private static string Read(string relative)
    {
        for (DirectoryInfo? directory = new(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "AIQuantTradingResearch.slnx"))) return File.ReadAllText(Path.Combine(directory.FullName, relative));
        }
        throw new InvalidOperationException("Repository root was not found.");
    }

    private sealed class UnavailableConnectionFactory : ISqliteConnectionFactory
    {
        public SqliteConnection OpenConnection() => throw new InvalidOperationException("offline");
    }
}
