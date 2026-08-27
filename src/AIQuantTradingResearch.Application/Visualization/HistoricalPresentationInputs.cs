using System.Collections.ObjectModel;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Pipelines;

namespace AIQuantTradingResearch.Application.Visualization;

public sealed record HistoricalPresentationObservation(DateTimeOffset SourceTime, decimal Price);

public enum HistoricalPresentationFeatureState { Available, WarmUp }

public sealed record HistoricalPresentationFeature
{
    public const int RequiredObservationCount = 2;

    private HistoricalPresentationFeature(
        string identity, HistoricalPresentationFeatureState state, decimal? latestValue,
        DateTimeOffset? latestTimestamp, int observationCount)
    {
        Identity = identity; State = state; LatestValue = latestValue;
        LatestTimestamp = latestTimestamp; ObservationCount = observationCount;
    }

    public string Identity { get; }
    public HistoricalPresentationFeatureState State { get; }
    public decimal? LatestValue { get; }
    public DateTimeOffset? LatestTimestamp { get; }
    public int ObservationCount { get; }
    public const int RequiredCount = RequiredObservationCount;

    public static HistoricalPresentationFeature From(FeatureSet featureSet, int observationCount)
    {
        ArgumentNullException.ThrowIfNull(featureSet);
        if (observationCount < RequiredObservationCount || featureSet.Values.Count == 0)
            return WarmUp(observationCount);
        var latest = featureSet.Values[^1];
        return new(FeatureDefinition.SimpleReturnLag1V1Name, HistoricalPresentationFeatureState.Available,
            latest.Value, latest.Instant, observationCount);
    }

    public static HistoricalPresentationFeature WarmUp(int observationCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(observationCount);
        return new(FeatureDefinition.SimpleReturnLag1V1Name, HistoricalPresentationFeatureState.WarmUp,
            null, null, observationCount);
    }
}

public sealed record HistoricalPresentationInputs
{
    public HistoricalPresentationInputs(
        IEnumerable<HistoricalPresentationObservation> observations,
        HistoricalPresentationFeature feature,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion snapshotVersion,
        IEnumerable<PipelineStageEvidence> pipelineStages)
    {
        ArgumentNullException.ThrowIfNull(observations); ArgumentNullException.ThrowIfNull(feature);
        ArgumentNullException.ThrowIfNull(snapshotIdentity); ArgumentNullException.ThrowIfNull(snapshotVersion);
        ArgumentNullException.ThrowIfNull(pipelineStages);
        if (snapshotVersion.SnapshotIdentity != snapshotIdentity) throw new ArgumentException("Snapshot identity and version must agree.");
        var rows = observations.ToArray();
        if (rows.Any(static row => row is null)) throw new ArgumentException("Observation projection cannot contain null values.");
        for (var i = 1; i < rows.Length; i++) if (rows[i].SourceTime <= rows[i - 1].SourceTime) throw new ArgumentException("Observation projection must be strictly ordered.");
        Observations = new ReadOnlyCollection<HistoricalPresentationObservation>(rows);
        Feature = feature; SnapshotIdentity = snapshotIdentity; SnapshotVersion = snapshotVersion;
        PipelineStages = new ReadOnlyCollection<PipelineStageEvidence>(pipelineStages.ToArray());
    }

    public IReadOnlyList<HistoricalPresentationObservation> Observations { get; }
    public HistoricalPresentationFeature Feature { get; }
    public DatasetSnapshotIdentity SnapshotIdentity { get; }
    public DatasetVersion SnapshotVersion { get; }
    public IReadOnlyList<PipelineStageEvidence> PipelineStages { get; }
}
