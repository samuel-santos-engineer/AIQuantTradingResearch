using System.Diagnostics;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Domain;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class Release110ObservabilityPermanentTests
{
    private static readonly DateTimeOffset From = new(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);

    [Fact]
    public void RootAndStagesHaveExactOrder()
    {
        var stopped = new List<Activity>();
        using var parent = new Activity(nameof(RootAndStagesHaveExactOrder)).Start();
        using var listener = Listen(stopped, parent.TraceId);

        var result = Pipeline(new History(HistoricalObservationResult.Retrieved([new PriceObservation(From, 10m)]))).Execute(Request());

        Assert.True(result.IsSuccess);
        Assert.Equal(PipelineObservability.PipelineExecutionActivityName, Assert.Single(stopped, x => x.OperationName == PipelineObservability.PipelineExecutionActivityName).OperationName);
        Assert.Equal(
            Enum.GetValues<ResearchPipelineStage>().Select(x => x.ToString()),
            stopped.Where(x => x.OperationName == PipelineObservability.PipelineStageActivityName).Select(x => x.GetTagItem("aiq.stage")));
        Assert.All(stopped, x =>
        {
            Assert.Equal("1.10", x.GetTagItem("aiq.release"));
            Assert.Equal("application", x.GetTagItem("aiq.component"));
        });
    }

    [Fact]
    public void RetrievalAndMaterializationIntervalsDoNotOverlap()
    {
        var history = new History(HistoricalObservationResult.Retrieved([new PriceObservation(From, 10m)]));
        var stopped = new List<Activity>();
        using var parent = new Activity(nameof(RetrievalAndMaterializationIntervalsDoNotOverlap)).Start();
        using var listener = Listen(stopped, parent.TraceId);

        Assert.True(Pipeline(history).Execute(Request()).IsSuccess);

        Assert.Equal(PipelineObservability.PipelineStageActivityName, history.AmbientOperationName);
        var stages = stopped.Where(x => x.OperationName == PipelineObservability.PipelineStageActivityName).ToArray();
        var retrieval = Array.FindIndex(stages, x => Equals(x.GetTagItem("aiq.stage"), ResearchPipelineStage.HistoricalObservationRetrieval.ToString()));
        var materialization = Array.FindIndex(stages, x => Equals(x.GetTagItem("aiq.stage"), ResearchPipelineStage.DatasetMaterialization.ToString()));
        Assert.True(retrieval >= 0 && materialization > retrieval);
        Assert.True(stages[retrieval].Duration > TimeSpan.Zero);
        Assert.True(stages[materialization].Duration > TimeSpan.Zero);
    }

    [Fact]
    public void ApplicationObservationsAreBoundedAndNonAuthoritative()
    {
        var first = Pipeline(new History(HistoricalObservationResult.Retrieved([]))).Execute(Request());
        var second = Pipeline(new History(HistoricalObservationResult.Retrieved([]))).Execute(Request());

        Assert.True(first.IsSuccess);
        Assert.Equal(first.ExecutionIdentity, second.ExecutionIdentity);
        Assert.Equal(first.Provenance.Stages.Select(x => (x.Stage, x.Outcome, x.FailureCategory)), second.Provenance.Stages.Select(x => (x.Stage, x.Outcome, x.FailureCategory)));
    }

    [Fact]
    public void HealthVocabularyAndPrecedenceAreClosed()
    {
        var historical = DatasetSourceAuthority.AcceptedRelease11HistoricalObservations;
        Assert.Equal(SystemHealthState.Ready, SystemHealthSnapshot.From(VisualizationPresentationState.Ready, VisualizationSourceMode.Historical, historical).State);
        Assert.Equal(SystemHealthState.WarmUp, SystemHealthSnapshot.From(VisualizationPresentationState.WarmUp, VisualizationSourceMode.Historical, historical).State);
        Assert.Equal(SystemHealthState.Empty, SystemHealthSnapshot.From(VisualizationPresentationState.Empty, VisualizationSourceMode.Historical, historical).State);
        Assert.Equal("pipeline-failed", SystemHealthSnapshot.From(VisualizationPresentationState.Failed, VisualizationSourceMode.Historical, historical).Reason);
        Assert.Equal("structural-staleness", SystemHealthSnapshot.From(VisualizationPresentationState.Stale, VisualizationSourceMode.Historical, historical).Reason);
        Assert.False(Enum.GetNames<SystemHealthState>().Contains("Degraded", StringComparer.Ordinal));
    }

    [Fact]
    public void HealthV1CompatibilityAndNoIndependentFreshness()
    {
        var model = VisualizationReadModel.Create(
            VisualizationRevision.Historical(new HistoricalPresentationRevision(1), "wp06"),
            VisualizationSourceMode.Historical,
            DatasetSourceAuthority.AcceptedRelease11HistoricalObservations,
            "SIMULATED-USD",
            VisualizationPresentationState.Ready);

        Assert.Equal("aiq-visualization-read-model-v1", VisualizationReadModel.ContractVersion);
        Assert.Equal(SystemHealthState.Ready, model.SystemHealth!.State);
        Assert.Null(model.SystemHealth.Reason);
        Assert.DoesNotContain(typeof(SystemHealthSnapshot).GetProperties(), property => property.Name.Contains("time", StringComparison.OrdinalIgnoreCase) || property.Name.Contains("age", StringComparison.OrdinalIgnoreCase));
    }

    private static ActivityListener Listen(List<Activity> stopped, ActivityTraceId traceId)
    {
        var listener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == PipelineObservability.ActivitySourceName,
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
            ActivityStopped = activity => { if (activity.TraceId == traceId) stopped.Add(activity); },
        };
        ActivitySource.AddActivityListener(listener);
        return listener;
    }

    private static PipelineExecutionUseCase Pipeline(IHistoricalObservationStore history) => new(
        new MaterializeDatasetUseCase(history),
        new SnapshotStore(),
        new Catalog(),
        new SimpleReturnFeatureComputer());

    private static PipelineRequest Request()
    {
        var definition = new DatasetDefinition("SIMULATED-USD", From, From.AddDays(2));
        var identity = DatasetIdentityComputer.ComputeDefinitionIdentity(definition);
        return new PipelineRequest(new PipelineDefinition(definition, PipelineIdentityComputer.ComputeDefinitionIdentity(identity)));
    }

    private sealed class History(HistoricalObservationResult result) : IHistoricalObservationStore
    {
        public string? AmbientOperationName { get; private set; }
        public ObservationPersistenceResult Persist(string target, IReadOnlyList<PriceObservation> observations) => throw new NotSupportedException();
        public HistoricalObservationResult Retrieve(string target) { AmbientOperationName = Activity.Current?.OperationName; return result; }
    }

    private sealed class SnapshotStore : IDatasetSnapshotStore
    {
        public DatasetSnapshotStoreResult Store(DatasetSnapshotCandidate snapshot) => DatasetSnapshotStoreResult.Completed(DatasetSnapshotStoreOutcome.NewlyAccepted);
        public DatasetSnapshotRetrievalResult Retrieve(DatasetSnapshotIdentity snapshotIdentity) => throw new NotSupportedException();
    }

    private sealed class Catalog : IDatasetCatalog
    {
        public DatasetCatalogRegistrationResult Register(DatasetCatalogEntry entry) => DatasetCatalogRegistrationResult.Completed(DatasetCatalogRegistrationOutcome.NewlyRegistered);
        public DatasetCatalogLookupResult Find(DatasetSnapshotIdentity snapshotIdentity) => throw new NotSupportedException();
    }
}
