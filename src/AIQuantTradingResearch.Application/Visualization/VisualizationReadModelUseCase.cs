using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Pipelines;

namespace AIQuantTradingResearch.Application.Visualization;

public sealed class VisualizationReadModelUseCase
{
    private ulong nextHistoricalRevision;
    private readonly IVisualizationReadModelStore store;
    private readonly List<VisualizationObservation> accumulated = [];
    private VisualizationSourceMode? accumulatedMode;

    public VisualizationReadModelUseCase(IVisualizationReadModelStore store)
    {
        this.store = store ?? throw new ArgumentNullException(nameof(store));
    }

    public VisualizationReadModel PublishHistorical(
        string target, DatasetSourceAuthority authority, VisualizationPresentationState state,
        IEnumerable<VisualizationObservation>? observations = null, DatasetSnapshotIdentity? snapshotIdentity = null,
        DatasetVersion? datasetVersion = null, VisualizationFeature? feature = null,
        PipelineExecutionEvidence? pipeline = null, string? category = null, string? safeMessage = null)
    {
        var prior = store.Current;
        var revision = state == VisualizationPresentationState.Stale
            ? RequireHistoricalPrior(prior)
            : new HistoricalPresentationRevision(checked(nextHistoricalRevision + 1));
        if (state != VisualizationPresentationState.Stale) nextHistoricalRevision = revision.Value;
        var identity = state == VisualizationPresentationState.Stale
            ? prior!.Revision.Identity
            : snapshotIdentity?.Fingerprint ?? VisualizationRevision.StateIdentity(state, category ?? "none");
        var tagged = VisualizationRevision.Historical(revision, identity);
        var failure = state == VisualizationPresentationState.Failed
            ? new VisualizationFailure(category ?? "Unknown", safeMessage ?? "The presentation operation failed.", tagged, true) : null;
        var model = VisualizationReadModel.Create(tagged, VisualizationSourceMode.Historical, authority, target, state,
            Window(VisualizationSourceMode.Historical, state, prior, observations), snapshotIdentity ?? prior?.SnapshotIdentity, datasetVersion ?? prior?.DatasetVersion, feature, pipeline, failure,
            state == VisualizationPresentationState.Stale ? category ?? "Structural staleness." : null,
            state == VisualizationPresentationState.Stale ? prior?.IdempotencyStatus ?? PresentationIdempotencyStatus.Unavailable : pipeline?.PresentationIdempotencyStatus ?? PresentationIdempotencyStatus.Unavailable,
            state == VisualizationPresentationState.Stale ? prior?.DataQualityStatus ?? PresentationDataQualityStatus.Unavailable : pipeline?.PresentationDataQualityStatus ?? PresentationDataQualityStatus.Unavailable);
        store.Publish(model);
        return model;
    }

    public VisualizationReadModel PublishHistorical(string target, PipelineExecutionResult result)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);
        ArgumentNullException.ThrowIfNull(result);
        var evidence = PipelineExecutionEvidence.From(result);
        if (!result.IsSuccess)
        {
            return PublishHistorical(target, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations,
                VisualizationPresentationState.Failed, pipeline: evidence,
                category: result.FailureCategory?.ToString() ?? "PipelineFailure",
                safeMessage: "The historical pipeline did not produce a complete presentation result.");
        }

        var inputs = result.HistoricalPresentationInputs
            ?? throw new InvalidOperationException("A successful pipeline result must expose canonical historical presentation inputs.");
        var rows = inputs.Observations.Select(static row => new VisualizationObservation(row.SourceTime, row.Price));
        if (inputs.Observations.Count == 0)
            return PublishHistorical(target, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations,
                VisualizationPresentationState.Empty, rows, inputs.SnapshotIdentity, inputs.SnapshotVersion, pipeline: evidence);
        if (inputs.Feature.State == HistoricalPresentationFeatureState.WarmUp)
            return PublishHistorical(target, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations,
                VisualizationPresentationState.WarmUp, rows, inputs.SnapshotIdentity, inputs.SnapshotVersion,
                VisualizationFeature.WarmUp(inputs.Feature.ObservationCount), evidence);
        return PublishHistorical(target, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations,
            VisualizationPresentationState.Ready, rows, inputs.SnapshotIdentity, inputs.SnapshotVersion,
            new VisualizationFeature(inputs.Feature.Identity, inputs.Feature.LatestValue, inputs.Feature.ObservationCount, HistoricalPresentationFeature.RequiredCount), evidence);
    }

    public VisualizationReadModel PublishReplay(
        int logicalTick, string target, DatasetSourceAuthority authority, VisualizationPresentationState state,
        IEnumerable<VisualizationObservation>? observations = null, DatasetSnapshotIdentity? snapshotIdentity = null,
        DatasetVersion? datasetVersion = null, VisualizationFeature? feature = null,
        PipelineExecutionEvidence? pipeline = null, string? category = null, string? safeMessage = null)
    {
        var identity = snapshotIdentity?.Fingerprint ?? VisualizationRevision.StateIdentity(state, category ?? "none");
        var revision = VisualizationRevision.Replay(logicalTick, identity);
        var failure = state == VisualizationPresentationState.Failed
            ? new VisualizationFailure(category ?? "Unknown", safeMessage ?? "The presentation operation failed.", revision, true) : null;
        var prior = store.Current;
        var model = VisualizationReadModel.Create(revision, VisualizationSourceMode.Replay, authority, target, state,
            Window(VisualizationSourceMode.Replay, state, prior, observations), snapshotIdentity ?? prior?.SnapshotIdentity, datasetVersion ?? prior?.DatasetVersion, feature, pipeline, failure,
            state == VisualizationPresentationState.Stale ? category ?? "Structural staleness." : null,
            state == VisualizationPresentationState.Stale ? prior?.IdempotencyStatus ?? PresentationIdempotencyStatus.Unavailable : pipeline?.PresentationIdempotencyStatus ?? PresentationIdempotencyStatus.Unavailable,
            state == VisualizationPresentationState.Stale ? prior?.DataQualityStatus ?? PresentationDataQualityStatus.Unavailable : pipeline?.PresentationDataQualityStatus ?? PresentationDataQualityStatus.Unavailable);
        store.Publish(model);
        return model;
    }

    private static HistoricalPresentationRevision RequireHistoricalPrior(VisualizationReadModel? prior)
    {
        if (prior is null || prior.Revision.Kind != VisualizationRevisionKind.HistoricalPresentation)
            throw new InvalidOperationException("Historical stale publication requires a current Historical envelope.");
        return new HistoricalPresentationRevision(prior.Revision.Value);
    }

    private IReadOnlyList<VisualizationObservation> Window(VisualizationSourceMode mode, VisualizationPresentationState state, VisualizationReadModel? prior, IEnumerable<VisualizationObservation>? observations)
    {
        if (state is VisualizationPresentationState.Stale or VisualizationPresentationState.Failed)
            return prior?.Window ?? [];
        if (accumulatedMode != mode) { accumulated.Clear(); accumulatedMode = mode; }
        foreach (var observation in observations ?? [])
        {
            var same = accumulated.FindIndex(row => row.SourceTime == observation.SourceTime);
            if (same >= 0) { accumulated[same] = observation; continue; }
            if (accumulated.Count > 0 && observation.SourceTime < accumulated[^1].SourceTime) continue;
            accumulated.Add(observation);
        }
        var normalized = accumulated.GroupBy(x => x.SourceTime).Select(static group => group.Last()).OrderBy(x => x.SourceTime).TakeLast(VisualizationReadModel.WindowCapacity).ToArray();
        accumulated.Clear(); accumulated.AddRange(normalized);
        return normalized;
    }
}
