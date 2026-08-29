using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Visualization;

public enum VisualizationRevisionKind { HistoricalPresentation, ReplayLogicalTick }
public enum VisualizationSourceMode { Historical, Replay }
public enum VisualizationPresentationState { Ready, Empty, WarmUp, Stale, Failed }
public enum SystemHealthState { Ready, WarmUp, Empty, Failed, Stale, Unavailable }

public sealed record SystemHealthSnapshot(SystemHealthState State, string Provenance, string? Reason)
{
    public static SystemHealthSnapshot From(VisualizationPresentationState state, VisualizationSourceMode sourceMode, DatasetSourceAuthority sourceAuthority)
    {
        if (!Enum.IsDefined(state) || !Enum.IsDefined(sourceMode) || !Enum.IsDefined(sourceAuthority))
            throw new ArgumentOutOfRangeException(nameof(state));

        var provenance = sourceAuthority switch
        {
            DatasetSourceAuthority.AcceptedRelease11HistoricalObservations => "historical",
            DatasetSourceAuthority.Release19SimulatedLiveReplay => "simulated",
            _ => throw new ArgumentOutOfRangeException(nameof(sourceAuthority), sourceAuthority, "Unknown dataset source authority."),
        };

        return state switch
        {
            VisualizationPresentationState.Ready => new(SystemHealthState.Ready, provenance, null),
            VisualizationPresentationState.WarmUp => new(SystemHealthState.WarmUp, provenance, null),
            VisualizationPresentationState.Empty => new(SystemHealthState.Empty, provenance, null),
            VisualizationPresentationState.Failed => new(SystemHealthState.Failed, provenance, "pipeline-failed"),
            VisualizationPresentationState.Stale => new(SystemHealthState.Stale, provenance, "structural-staleness"),
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, "Unknown visualization presentation state."),
        };
    }
}

public readonly record struct HistoricalPresentationRevision
{
    public HistoricalPresentationRevision(ulong value) => Value = value;
    public ulong Value { get; }
}

public sealed record VisualizationRevision(
    VisualizationRevisionKind Kind,
    ulong Value,
    string Identity)
{
    public static VisualizationRevision Historical(HistoricalPresentationRevision revision, string identity) =>
        new(VisualizationRevisionKind.HistoricalPresentation, revision.Value, ValidateIdentity(identity));

    public static VisualizationRevision Replay(int logicalTick, string identity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(logicalTick);
        return new(VisualizationRevisionKind.ReplayLogicalTick, (ulong)logicalTick, ValidateIdentity(identity));
    }

    internal static string StateIdentity(VisualizationPresentationState state, string category) =>
        Fingerprint($"aiq-visualization-state-v1|{state}|{category}");

    private static string ValidateIdentity(string identity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identity);
        return identity;
    }

    internal static string Fingerprint(string value) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))).ToLowerInvariant();
}

public static class VisualizationRevisionComparer
{
    public static int Compare(VisualizationRevision left, VisualizationRevision right)
    {
        ArgumentNullException.ThrowIfNull(left); ArgumentNullException.ThrowIfNull(right);
        if (left.Kind != right.Kind) throw new InvalidOperationException("Visualization revisions from different source modes are not ordered.");
        return left.Value.CompareTo(right.Value);
    }

    public static bool IsEquivalent(VisualizationRevision left, VisualizationRevision right) =>
        Compare(left, right) == 0 && string.Equals(left.Identity, right.Identity, StringComparison.Ordinal);
}

public sealed record VisualizationObservation(DateTimeOffset SourceTime, decimal Price)
{
    public static VisualizationObservation From(PriceObservation observation)
    {
        ArgumentNullException.ThrowIfNull(observation);
        return new(observation.Instant, observation.Price);
    }
}

public sealed record VisualizationFeature(string Identity, decimal? Value, int ObservationCount, int RequiredObservationCount)
{
    public const string SimpleReturnLag1V1 = "simple-return-lag-1-v1";
    public static VisualizationFeature WarmUp(int observationCount) => new(SimpleReturnLag1V1, null, observationCount, 2);
}

public sealed record VisualizationFailure
{
    public VisualizationFailure(string category, string message, VisualizationRevision failedRevision, bool isRecoverable)
    {
        Category = Require(category, nameof(category)); Message = Require(message, nameof(message));
        FailedRevision = failedRevision ?? throw new ArgumentNullException(nameof(failedRevision)); IsRecoverable = isRecoverable;
    }
    public string Category { get; }
    public string Message { get; }
    public VisualizationRevision FailedRevision { get; }
    public bool IsRecoverable { get; }
    private static string Require(string value, string name) { ArgumentException.ThrowIfNullOrWhiteSpace(value, name); return value; }
}

public sealed record VisualizationReadModel(
    VisualizationRevision Revision,
    VisualizationSourceMode SourceMode,
    DatasetSourceAuthority SourceAuthority,
    string Target,
    VisualizationPresentationState State,
    IReadOnlyList<VisualizationObservation> Window,
    DatasetSnapshotIdentity? SnapshotIdentity,
    DatasetVersion? DatasetVersion,
    VisualizationFeature? Feature,
    PipelineExecutionEvidence? Pipeline,
    VisualizationFailure? Failure,
    string? StaleReason,
    PresentationIdempotencyStatus IdempotencyStatus,
    PresentationDataQualityStatus DataQualityStatus,
    SystemHealthSnapshot? SystemHealth)
{
    public const string ContractVersion = "aiq-visualization-read-model-v1";
    public const int WindowCapacity = 64;
    public VisualizationObservation? Latest => Window.Count == 0 ? null : Window[^1];
    public int ObservationCount => Window.Count;

    public static VisualizationReadModel Create(
        VisualizationRevision revision, VisualizationSourceMode sourceMode, DatasetSourceAuthority sourceAuthority,
        string target, VisualizationPresentationState state, IEnumerable<VisualizationObservation>? window = null,
        DatasetSnapshotIdentity? snapshotIdentity = null, DatasetVersion? datasetVersion = null,
        VisualizationFeature? feature = null, PipelineExecutionEvidence? pipeline = null,
        VisualizationFailure? failure = null, string? staleReason = null,
        PresentationIdempotencyStatus idempotencyStatus = PresentationIdempotencyStatus.Unavailable,
        PresentationDataQualityStatus dataQualityStatus = PresentationDataQualityStatus.Unavailable,
        SystemHealthSnapshot? systemHealth = null)
    {
        ArgumentNullException.ThrowIfNull(revision); ArgumentException.ThrowIfNullOrWhiteSpace(target);
        if (!Enum.IsDefined(sourceMode) || !Enum.IsDefined(sourceAuthority) || !Enum.IsDefined(state)) throw new ArgumentOutOfRangeException(nameof(state));
        if ((snapshotIdentity is null) != (datasetVersion is null) || (datasetVersion is not null && datasetVersion.SnapshotIdentity != snapshotIdentity)) throw new ArgumentException("Snapshot identity and version must agree.");
        if (state == VisualizationPresentationState.Failed && failure is null) throw new ArgumentException("Failed state requires safe failure metadata.");
        if (state != VisualizationPresentationState.Failed && failure is not null) throw new ArgumentException("Only Failed may carry failure metadata.");
        if (state == VisualizationPresentationState.Stale && string.IsNullOrWhiteSpace(staleReason)) throw new ArgumentException("Stale state requires structural reason.");
        var health = systemHealth ?? SystemHealthSnapshot.From(state, sourceMode, sourceAuthority);
        ValidateSystemHealth(health);
        var rows = (window ?? []).GroupBy(x => x.SourceTime).Select(static group => group.Last()).OrderBy(x => x.SourceTime).ToArray();
        if (rows.Length > WindowCapacity) rows = rows[^WindowCapacity..];
        if (!Enum.IsDefined(idempotencyStatus) || !Enum.IsDefined(dataQualityStatus)) throw new ArgumentOutOfRangeException(nameof(idempotencyStatus));
        return new(revision, sourceMode, sourceAuthority, target, state, new ReadOnlyCollection<VisualizationObservation>(rows), snapshotIdentity, datasetVersion, feature, pipeline, failure, staleReason, idempotencyStatus, dataQualityStatus, health);
    }

    private static void ValidateSystemHealth(SystemHealthSnapshot health)
    {
        ArgumentNullException.ThrowIfNull(health);
        if (!Enum.IsDefined(health.State) || health.Provenance is not ("historical" or "replay" or "simulated"))
            throw new ArgumentOutOfRangeException(nameof(health));
        var expectedReason = health.State switch
        {
            SystemHealthState.Ready or SystemHealthState.WarmUp or SystemHealthState.Empty => null,
            SystemHealthState.Failed => "pipeline-failed",
            SystemHealthState.Stale => "structural-staleness",
            SystemHealthState.Unavailable => "required-health-evidence-unavailable",
            _ => throw new ArgumentOutOfRangeException(nameof(health)),
        };
        if (!string.Equals(health.Reason, expectedReason, StringComparison.Ordinal))
            throw new ArgumentException("System Health reason is invalid for its state.", nameof(health));
    }
}

public interface IVisualizationReadModelStore
{
    VisualizationReadModel? Current { get; }
    bool Publish(VisualizationReadModel model);
}
