namespace AIQuantTradingResearch.Application.Datasets;

public sealed record DatasetCatalogEntry
{
    public DatasetCatalogEntry(DatasetSnapshotCandidate snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        Definition = snapshot.Definition;
        DefinitionIdentity = snapshot.DefinitionIdentity;
        ResearchDatasetIdentity = snapshot.ResearchDatasetIdentity;
        SourceStateIdentity = snapshot.SourceStateIdentity;
        SnapshotIdentity = snapshot.SnapshotIdentity;
        Version = snapshot.Version;
        Coverage = snapshot.Coverage;
        Provenance = snapshot.Provenance;
        Lineage = snapshot.Lineage;
    }

    public DatasetDefinition Definition { get; }

    public DatasetDefinitionIdentity DefinitionIdentity { get; }

    public ResearchDatasetIdentity ResearchDatasetIdentity { get; }

    public SourceStateIdentity SourceStateIdentity { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion Version { get; }

    public DatasetCoverage Coverage { get; }

    public DatasetProvenance Provenance { get; }

    public DatasetLineage Lineage { get; }

    public string Target => Definition.Target;

    public DateTimeOffset RequestedFrom => Coverage.RequestedFrom;

    public DateTimeOffset RequestedTo => Coverage.RequestedTo;

    public int ObservationCount => Coverage.ObservationCount;

    public DateTimeOffset? FirstObservationInstant => Coverage.FirstObservationInstant;

    public DateTimeOffset? LastObservationInstant => Coverage.LastObservationInstant;

    public bool IsEmpty => ObservationCount == 0;

    public string IdentityScheme => Provenance.IdentityScheme;
}
