using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Features;

public sealed record FeatureValue
{
    public FeatureValue(DateTimeOffset instant, decimal value)
    {
        Instant = instant;
        Value = value;
    }

    public DateTimeOffset Instant { get; }

    public decimal Value { get; }
}

public sealed record FeatureProvenance
{
    public FeatureProvenance(
        FeatureDefinitionIdentity definitionIdentity,
        DatasetSnapshotIdentity snapshotIdentity,
        DatasetVersion snapshotVersion,
        DatasetProvenance datasetProvenance)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(snapshotIdentity);
        ArgumentNullException.ThrowIfNull(snapshotVersion);
        ArgumentNullException.ThrowIfNull(datasetProvenance);

        if (snapshotVersion.SnapshotIdentity != snapshotIdentity
            || datasetProvenance.SnapshotIdentity != snapshotIdentity
            || datasetProvenance.Version != snapshotVersion)
        {
            throw new ArgumentException("Feature provenance must reference one exact accepted dataset snapshot and version.");
        }

        DefinitionIdentity = definitionIdentity;
        SnapshotIdentity = snapshotIdentity;
        SnapshotVersion = snapshotVersion;
        DatasetProvenance = datasetProvenance;
    }

    public FeatureDefinitionIdentity DefinitionIdentity { get; }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }

    public DatasetVersion SnapshotVersion { get; }

    public DatasetProvenance DatasetProvenance { get; }
}

public sealed record FeatureLineage
{
    public FeatureLineage(
        FeatureDefinitionIdentity definitionIdentity,
        DatasetLineage datasetLineage)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(datasetLineage);

        DefinitionIdentity = definitionIdentity;
        DatasetLineage = datasetLineage;
    }

    public FeatureDefinitionIdentity DefinitionIdentity { get; }

    public DatasetLineage DatasetLineage { get; }
}

public sealed record FeatureSet
{
    public FeatureSet(
        FeatureDefinitionIdentity definitionIdentity,
        FeatureSetIdentity identity,
        FeatureProvenance provenance,
        FeatureLineage lineage,
        IEnumerable<FeatureValue> values)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(identity);
        ArgumentNullException.ThrowIfNull(provenance);
        ArgumentNullException.ThrowIfNull(lineage);
        ArgumentNullException.ThrowIfNull(values);

        var valueSnapshot = values.ToArray();

        if (valueSnapshot.Any(static value => value is null))
        {
            throw new ArgumentException("Feature values cannot contain null values.", nameof(values));
        }

        if (provenance.DefinitionIdentity != definitionIdentity
            || lineage.DefinitionIdentity != definitionIdentity
            || provenance.DatasetProvenance.DefinitionIdentity != lineage.DatasetLineage.DefinitionIdentity
            || provenance.DatasetProvenance.SourceStateIdentity != lineage.DatasetLineage.SourceStateIdentity)
        {
            throw new ArgumentException("Feature evidence must retain coherent definition and dataset lineage references.");
        }

        DefinitionIdentity = definitionIdentity;
        Identity = identity;
        Provenance = provenance;
        Lineage = lineage;
        Values = Array.AsReadOnly(valueSnapshot);
    }

    public FeatureDefinitionIdentity DefinitionIdentity { get; }

    public FeatureSetIdentity Identity { get; }

    public FeatureProvenance Provenance { get; }

    public FeatureLineage Lineage { get; }

    public IReadOnlyList<FeatureValue> Values { get; }

    public int Count => Values.Count;

    public DatasetSnapshotIdentity SnapshotIdentity => Provenance.SnapshotIdentity;

    public DatasetVersion SnapshotVersion => Provenance.SnapshotVersion;
}
