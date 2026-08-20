using System.Collections.ObjectModel;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Datasets;

public enum DatasetSourceAuthority
{
    AcceptedRelease11HistoricalObservations,
}

public sealed record DatasetCoverage
{
    public DatasetCoverage(
        DateTimeOffset requestedFrom,
        DateTimeOffset requestedTo,
        int observationCount,
        DateTimeOffset? firstObservationInstant,
        DateTimeOffset? lastObservationInstant)
    {
        if (requestedFrom >= requestedTo)
        {
            throw new ArgumentException("Coverage boundaries must define a valid [from, to) interval.", nameof(requestedTo));
        }

        if (observationCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(observationCount), observationCount, "Observation count cannot be negative.");
        }

        if (observationCount == 0 && (firstObservationInstant is not null || lastObservationInstant is not null))
        {
            throw new ArgumentException("Empty coverage cannot have observed boundaries.");
        }

        if (observationCount > 0 && (firstObservationInstant is null || lastObservationInstant is null))
        {
            throw new ArgumentException("Non-empty coverage requires first and last observed instants.");
        }

        if (firstObservationInstant is not null
            && (firstObservationInstant < requestedFrom || firstObservationInstant >= requestedTo
                || lastObservationInstant < firstObservationInstant || lastObservationInstant >= requestedTo))
        {
            throw new ArgumentException("Observed coverage must remain within the requested [from, to) interval.");
        }

        RequestedFrom = requestedFrom;
        RequestedTo = requestedTo;
        ObservationCount = observationCount;
        FirstObservationInstant = firstObservationInstant;
        LastObservationInstant = lastObservationInstant;
    }

    public DateTimeOffset RequestedFrom { get; }

    public DateTimeOffset RequestedTo { get; }

    public int ObservationCount { get; }

    public DateTimeOffset? FirstObservationInstant { get; }

    public DateTimeOffset? LastObservationInstant { get; }
}

public sealed record DatasetProvenance
{
    public DatasetProvenance(
        DatasetDefinition definition,
        DatasetDefinitionIdentity definitionIdentity,
        ResearchDatasetIdentity researchDatasetIdentity,
        SourceStateIdentity sourceStateIdentity,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion version,
        DatasetSourceAuthority sourceAuthority,
        int observationCount)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(researchDatasetIdentity);
        ArgumentNullException.ThrowIfNull(sourceStateIdentity);
        ArgumentNullException.ThrowIfNull(snapshotIdentity);
        ArgumentNullException.ThrowIfNull(version);

        if (version.SnapshotIdentity != snapshotIdentity)
        {
            throw new ArgumentException("Dataset version must represent the same snapshot identity.", nameof(version));
        }

        if (!Enum.IsDefined(sourceAuthority))
        {
            throw new ArgumentOutOfRangeException(nameof(sourceAuthority), sourceAuthority, "Unknown dataset source authority.");
        }

        if (observationCount < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(observationCount), observationCount, "Observation count cannot be negative.");
        }

        Definition = definition;
        DefinitionIdentity = definitionIdentity;
        ResearchDatasetIdentity = researchDatasetIdentity;
        SourceStateIdentity = sourceStateIdentity;
        SnapshotIdentity = snapshotIdentity;
        Version = version;
        SourceAuthority = sourceAuthority;
        ObservationCount = observationCount;
        IdentityScheme = DatasetIdentityScheme.Name;
    }

    public DatasetDefinition Definition { get; }

    public DatasetDefinitionIdentity DefinitionIdentity { get; }

    public ResearchDatasetIdentity ResearchDatasetIdentity { get; }

    public SourceStateIdentity SourceStateIdentity { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion Version { get; }

    public DatasetSourceAuthority SourceAuthority { get; }

    public int ObservationCount { get; }

    public string IdentityScheme { get; }
}

public sealed record DatasetLineage
{
    public DatasetLineage(
        DatasetDefinitionIdentity definitionIdentity,
        SourceStateIdentity sourceStateIdentity,
        IEnumerable<PriceObservation> sourceObservations)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(sourceStateIdentity);
        ArgumentNullException.ThrowIfNull(sourceObservations);

        var observations = sourceObservations.ToArray();

        if (observations.Any(static observation => observation is null))
        {
            throw new ArgumentException("Lineage observations cannot contain null values.", nameof(sourceObservations));
        }

        ValidateObservations(observations, nameof(sourceObservations));

        DefinitionIdentity = definitionIdentity;
        SourceStateIdentity = sourceStateIdentity;
        SourceObservations = Array.AsReadOnly(observations);
    }

    public DatasetDefinitionIdentity DefinitionIdentity { get; }

    public SourceStateIdentity SourceStateIdentity { get; }

    public IReadOnlyList<PriceObservation> SourceObservations { get; }

    internal static void ValidateObservations(IReadOnlyList<PriceObservation> observations, string parameterName)
    {
        for (var index = 1; index < observations.Count; index++)
        {
            if (observations[index].Instant <= observations[index - 1].Instant)
            {
                throw new ArgumentException(
                    "Observations must be unique and strictly ascending by semantic instant.",
                    parameterName);
            }
        }
    }
}

public sealed record DatasetSnapshotCandidate
{
    public DatasetSnapshotCandidate(
        DatasetDefinition definition,
        DatasetDefinitionIdentity definitionIdentity,
        ResearchDatasetIdentity researchDatasetIdentity,
        SourceStateIdentity sourceStateIdentity,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion version,
        IEnumerable<PriceObservation> observations,
        DatasetCoverage coverage,
        DatasetProvenance provenance,
        DatasetLineage lineage)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(researchDatasetIdentity);
        ArgumentNullException.ThrowIfNull(sourceStateIdentity);
        ArgumentNullException.ThrowIfNull(snapshotIdentity);
        ArgumentNullException.ThrowIfNull(version);
        ArgumentNullException.ThrowIfNull(observations);
        ArgumentNullException.ThrowIfNull(coverage);
        ArgumentNullException.ThrowIfNull(provenance);
        ArgumentNullException.ThrowIfNull(lineage);

        var observationSnapshot = observations.ToArray();

        if (observationSnapshot.Any(static observation => observation is null))
        {
            throw new ArgumentException("Snapshot observations cannot contain null values.", nameof(observations));
        }

        DatasetLineage.ValidateObservations(observationSnapshot, nameof(observations));
        ValidateConsistency(
            definition,
            definitionIdentity,
            researchDatasetIdentity,
            sourceStateIdentity,
            snapshotIdentity,
            version,
            observationSnapshot,
            coverage,
            provenance,
            lineage);

        Definition = definition;
        DefinitionIdentity = definitionIdentity;
        ResearchDatasetIdentity = researchDatasetIdentity;
        SourceStateIdentity = sourceStateIdentity;
        SnapshotIdentity = snapshotIdentity;
        Version = version;
        Observations = Array.AsReadOnly(observationSnapshot);
        Coverage = coverage;
        Provenance = provenance;
        Lineage = lineage;
    }

    public DatasetDefinition Definition { get; }

    public DatasetDefinitionIdentity DefinitionIdentity { get; }

    public ResearchDatasetIdentity ResearchDatasetIdentity { get; }

    public SourceStateIdentity SourceStateIdentity { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion Version { get; }

    public IReadOnlyList<PriceObservation> Observations { get; }

    public DatasetCoverage Coverage { get; }

    public DatasetProvenance Provenance { get; }

    public DatasetLineage Lineage { get; }

    private static void ValidateConsistency(
        DatasetDefinition definition,
        DatasetDefinitionIdentity definitionIdentity,
        ResearchDatasetIdentity researchDatasetIdentity,
        SourceStateIdentity sourceStateIdentity,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion version,
        PriceObservation[] observations,
        DatasetCoverage coverage,
        DatasetProvenance provenance,
        DatasetLineage lineage)
    {
        if (version.SnapshotIdentity != snapshotIdentity
            || coverage.RequestedFrom != definition.From
            || coverage.RequestedTo != definition.To
            || coverage.ObservationCount != observations.Length
            || provenance.Definition != definition
            || provenance.DefinitionIdentity != definitionIdentity
            || provenance.ResearchDatasetIdentity != researchDatasetIdentity
            || provenance.SourceStateIdentity != sourceStateIdentity
            || provenance.SnapshotIdentity != snapshotIdentity
            || provenance.Version != version
            || provenance.ObservationCount != observations.Length
            || lineage.DefinitionIdentity != definitionIdentity
            || lineage.SourceStateIdentity != sourceStateIdentity
            || !HaveSameObservations(observations, lineage.SourceObservations))
        {
            throw new ArgumentException("Snapshot candidate semantic facts must agree.");
        }

        if (observations.Length == 0)
        {
            return;
        }

        if (coverage.FirstObservationInstant != observations[0].Instant
            || coverage.LastObservationInstant != observations[^1].Instant)
        {
            throw new ArgumentException("Coverage must preserve the first and last selected observation instants.");
        }
    }

    private static bool HaveSameObservations(
        PriceObservation[] left,
        IReadOnlyList<PriceObservation> right)
    {
        if (left.Length != right.Count)
        {
            return false;
        }

        for (var index = 0; index < left.Length; index++)
        {
            if (left[index].Instant.UtcTicks != right[index].Instant.UtcTicks
                || left[index].Instant.Offset != right[index].Instant.Offset
                || left[index].Price != right[index].Price)
            {
                return false;
            }
        }

        return true;
    }
}
