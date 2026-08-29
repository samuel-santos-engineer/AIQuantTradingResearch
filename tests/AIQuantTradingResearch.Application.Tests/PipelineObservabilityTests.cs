using System.Diagnostics;
using System.Diagnostics.Metrics;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class PipelineObservabilityTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
    private static readonly DateTimeOffset To = From.AddDays(2);

    [Fact]
    public void RetrievalPipelineEmitsOneRootAndFiveTruthfulStageActivities()
    {
        var activities = new List<ActivitySnapshot>();
        using var parent = new Activity(nameof(RetrievalPipelineEmitsOneRootAndFiveTruthfulStageActivities)).Start();
        using var listener = CreateActivityListener(activities, parent.TraceId);
        var history = new ObservingHistoryStore(HistoricalObservationResult.Retrieved([new PriceObservation(From, 10m)]));

        var result = CreatePipeline(history).Execute(CreateRequest());

        Assert.True(result.IsSuccess);
        Assert.Equal(PipelineObservability.ActivitySourceName, Assert.Single(
            activities,
            activity => activity.OperationName == PipelineObservability.PipelineExecutionActivityName).SourceName);
        Assert.Equal(
            [
                ResearchPipelineStage.HistoricalObservationRetrieval.ToString(),
                ResearchPipelineStage.DatasetMaterialization.ToString(),
                ResearchPipelineStage.SnapshotPersistence.ToString(),
                ResearchPipelineStage.CatalogRegistration.ToString(),
                ResearchPipelineStage.StructuredResultEvidence.ToString(),
            ],
            activities.Where(activity => activity.OperationName == PipelineObservability.PipelineStageActivityName)
                .Select(activity => activity.Tags["aiq.stage"]));
        Assert.All(
            activities.Where(activity => activity.OperationName == PipelineObservability.PipelineStageActivityName),
            activity => Assert.Equal(PipelineObservability.PipelineExecutionActivityName, activity.ParentOperationName));
        Assert.Equal(PipelineObservability.PipelineStageActivityName, history.AmbientOperationName);
        Assert.All(activities, activity => Assert.Equal("1.10", activity.Tags["aiq.release"]));
        Assert.All(activities, activity => Assert.Equal("application", activity.Tags["aiq.component"]));
        Assert.All(activities, activity => Assert.Equal("success", activity.Tags["aiq.outcome"]));
        Assert.DoesNotContain(activities, activity => activity.OperationName is "provider.operation" or "persistence.operation");
    }

    [Fact]
    public void ExplicitObservationMaterializationDoesNotEmitRetrievalActivity()
    {
        var activities = new List<ActivitySnapshot>();
        using var parent = new Activity(nameof(ExplicitObservationMaterializationDoesNotEmitRetrievalActivity)).Start();
        using var listener = CreateActivityListener(activities, parent.TraceId);
        var observations = new[] { new PriceObservation(From, 10m) };

        var result = CreatePipeline(new ObservingHistoryStore(HistoricalObservationResult.Retrieved([])))
            .Execute(CreateRequest(), observations);

        Assert.True(result.IsSuccess);
        Assert.DoesNotContain(activities, activity => activity.Tags.TryGetValue("aiq.stage", out var stage)
            && stage == ResearchPipelineStage.HistoricalObservationRetrieval.ToString());
        Assert.Contains(activities, activity => activity.Tags.TryGetValue("aiq.stage", out var stage)
            && stage == ResearchPipelineStage.DatasetMaterialization.ToString());
    }

    [Fact]
    public void FailureMapsToBoundedOutcomeAndDoesNotCreateMaterializationActivity()
    {
        var activities = new List<ActivitySnapshot>();
        using var parent = new Activity(nameof(FailureMapsToBoundedOutcomeAndDoesNotCreateMaterializationActivity)).Start();
        using var listener = CreateActivityListener(activities, parent.TraceId);

        var result = CreatePipeline(new ObservingHistoryStore(HistoricalObservationResult.Failed(PersistenceFailure.Unavailable)))
            .Execute(CreateRequest());

        Assert.False(result.IsSuccess);
        var retrieval = Assert.Single(activities, activity => activity.Tags.TryGetValue("aiq.stage", out var stage)
            && stage == ResearchPipelineStage.HistoricalObservationRetrieval.ToString());
        Assert.Equal("failed", retrieval.Tags["aiq.outcome"]);
        Assert.Equal(PipelineFailureCategory.DependencyUnavailable.ToString(), retrieval.Tags["aiq.error_class"]);
        Assert.DoesNotContain(activities, activity => activity.Tags.TryGetValue("aiq.stage", out var stage)
            && stage == ResearchPipelineStage.DatasetMaterialization.ToString());
    }

    [Fact]
    public void UnexpectedExceptionsPropagateAndProduceNoRawExceptionTelemetry()
    {
        var activities = new List<ActivitySnapshot>();
        using var parent = new Activity(nameof(UnexpectedExceptionsPropagateAndProduceNoRawExceptionTelemetry)).Start();
        using var listener = CreateActivityListener(activities, parent.TraceId);

        var exception = Assert.Throws<InvalidOperationException>(() => new PipelineExecutionUseCase(
            new ThrowingMaterializer(),
            new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)),
            new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered)))
            .Execute(CreateRequest()));

        Assert.Equal("probe failure", exception.Message);
        var root = Assert.Single(activities, activity => activity.OperationName == PipelineObservability.PipelineExecutionActivityName);
        Assert.Equal("failed", root.Tags["aiq.outcome"]);
        Assert.DoesNotContain(root.Tags.Keys, key => key.Contains("exception", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void MetricsUseCanonicalNamesUnitsAndBoundedAttributes()
    {
        var measurements = new List<MeasurementSnapshot>();
        var measurementLock = new object();
        using var listener = CreateMeterListener(measurements, measurementLock);

        var result = CreatePipeline(new ObservingHistoryStore(HistoricalObservationResult.Retrieved([]))).Execute(CreateRequest());
        MeasurementSnapshot[] snapshot;
        lock (measurementLock)
        {
            snapshot = measurements.ToArray();
        }

        Assert.True(result.IsSuccess);
        Assert.Contains(snapshot, measurement => measurement.Name == "pipeline.operations" && measurement.Unit == "{operation}");
        Assert.Contains(snapshot, measurement => measurement.Name == "pipeline.duration" && measurement.Unit == "ms");
        Assert.All(snapshot, measurement =>
        {
            Assert.Equal(PipelineObservability.MeterName, measurement.MeterName);
            Assert.Equal("1.10", measurement.Tags["aiq.release"]);
            Assert.Equal("application", measurement.Tags["aiq.component"]);
            Assert.DoesNotContain(measurement.Tags.Keys, key => key.Contains("path", StringComparison.OrdinalIgnoreCase)
                || key.Contains("token", StringComparison.OrdinalIgnoreCase)
                || key.Contains("secret", StringComparison.OrdinalIgnoreCase)
                || key.Contains("exception", StringComparison.OrdinalIgnoreCase));
        });
    }

    [Fact]
    public void NoListenerPreservesCanonicalExecutionResult()
    {
        var first = CreatePipeline(new ObservingHistoryStore(HistoricalObservationResult.Retrieved([]))).Execute(CreateRequest());
        var second = CreatePipeline(new ObservingHistoryStore(HistoricalObservationResult.Retrieved([]))).Execute(CreateRequest());

        Assert.True(first.IsSuccess);
        Assert.True(second.IsSuccess);
        Assert.Equal(first.ExecutionIdentity, second.ExecutionIdentity);
        Assert.Equal(
            first.Provenance.Stages.Select(static stage => (stage.Stage, stage.Outcome, stage.FailureCategory)),
            second.Provenance.Stages.Select(static stage => (stage.Stage, stage.Outcome, stage.FailureCategory)));
    }

    private static ActivityListener CreateActivityListener(List<ActivitySnapshot> activities, ActivityTraceId traceId)
    {
        var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == PipelineObservability.ActivitySourceName,
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
            ActivityStopped = activity =>
            {
                if (activity.TraceId == traceId)
                {
                    activities.Add(new ActivitySnapshot(
                        activity.Source.Name,
                        activity.OperationName,
                        activity.Parent?.OperationName,
                        activity.Tags.ToDictionary(static pair => pair.Key, static pair => pair.Value ?? string.Empty)));
                }
            },
        };
        ActivitySource.AddActivityListener(listener);
        return listener;
    }

    private static MeterListener CreateMeterListener(List<MeasurementSnapshot> measurements, object measurementLock)
    {
        var listener = new MeterListener();
        listener.InstrumentPublished = static (instrument, meterListener) =>
        {
            if (instrument.Meter.Name == PipelineObservability.MeterName)
            {
                meterListener.EnableMeasurementEvents(instrument);
            }
        };
        listener.SetMeasurementEventCallback<long>((instrument, measurement, tags, _) =>
        {
            lock (measurementLock)
            {
                measurements.Add(MeasurementSnapshot.Create(instrument, tags));
            }
        });
        listener.SetMeasurementEventCallback<double>((instrument, measurement, tags, _) =>
        {
            lock (measurementLock)
            {
                measurements.Add(MeasurementSnapshot.Create(instrument, tags));
            }
        });
        listener.Start();
        return listener;
    }

    private static PipelineExecutionUseCase CreatePipeline(IHistoricalObservationStore historyStore) => new(
        new MaterializeDatasetUseCase(historyStore),
        new StubSnapshotStore(DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted)),
        new StubCatalog(DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered)),
        new SimpleReturnFeatureComputer());

    private static PipelineRequest CreateRequest()
    {
        var definition = new DatasetDefinition("AAPL", From, To);
        var datasetDefinitionIdentity = DatasetIdentityComputer.ComputeDefinitionIdentity(definition);
        return new PipelineRequest(new PipelineDefinition(
            definition,
            PipelineIdentityComputer.ComputeDefinitionIdentity(datasetDefinitionIdentity)));
    }

    private sealed record ActivitySnapshot(
        string SourceName,
        string OperationName,
        string? ParentOperationName,
        IReadOnlyDictionary<string, string> Tags);

    private sealed record MeasurementSnapshot(
        string MeterName,
        string Name,
        string? Unit,
        IReadOnlyDictionary<string, string> Tags)
    {
        public static MeasurementSnapshot Create(Instrument instrument, ReadOnlySpan<KeyValuePair<string, object?>> tags) => new(
            instrument.Meter.Name,
            instrument.Name,
            instrument.Unit,
            tags.ToArray().ToDictionary(static pair => pair.Key, static pair => pair.Value?.ToString() ?? string.Empty));
    }

    private sealed class ObservingHistoryStore(HistoricalObservationResult result) : IHistoricalObservationStore
    {
        public string? AmbientOperationName { get; private set; }

        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> observations) =>
            throw new NotSupportedException();

        public HistoricalObservationResult Retrieve(string target)
        {
            AmbientOperationName = Activity.Current?.OperationName;
            return result;
        }
    }

    private sealed class ThrowingMaterializer : IMaterializeDatasetUseCase
    {
        public DatasetMaterializationResult Execute(DatasetDefinition definition) =>
            throw new InvalidOperationException("probe failure");
    }

    private sealed class StubSnapshotStore(DatasetSnapshotStoreResult result) : IDatasetSnapshotStore
    {
        public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot) => result;

        public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity) =>
            throw new NotSupportedException();
    }

    private sealed class StubCatalog(DatasetCatalogRegistrationResult result) : IDatasetCatalog
    {
        public DatasetCatalogRegistrationResult Register(DatasetCatalogEntry entry) => result;

        public DatasetCatalogLookupResult Find(DatasetSnapshotIdentity snapshotIdentity) =>
            throw new NotSupportedException();
    }
}
