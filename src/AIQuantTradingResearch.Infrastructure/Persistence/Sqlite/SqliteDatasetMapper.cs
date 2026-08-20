using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteDatasetMapper
{
    public static SqliteDatasetSnapshotRecord ToSnapshotRecord(DatasetCatalogEntry entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        return new SqliteDatasetSnapshotRecord(
            entry.SnapshotIdentity.Fingerprint,
            entry.DefinitionIdentity.Fingerprint,
            entry.ResearchDatasetIdentity.Fingerprint,
            entry.SourceStateIdentity.Fingerprint,
            entry.IdentityScheme,
            entry.Target,
            entry.RequestedFrom.UtcTicks,
            OffsetMinutes(entry.RequestedFrom),
            entry.RequestedTo.UtcTicks,
            OffsetMinutes(entry.RequestedTo),
            (int)entry.Definition.Ordering,
            entry.ObservationCount,
            entry.FirstObservationInstant?.UtcTicks,
            entry.FirstObservationInstant is null ? null : OffsetMinutes(entry.FirstObservationInstant.Value),
            entry.LastObservationInstant?.UtcTicks,
            entry.LastObservationInstant is null ? null : OffsetMinutes(entry.LastObservationInstant.Value),
            (int)entry.Provenance.SourceAuthority);
    }

    public static IReadOnlyList<SqliteDatasetObservationRecord> ToObservationRecords(
        DatasetSnapshotCandidate snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        return snapshot.Observations
            .Select((observation, ordinal) => new SqliteDatasetObservationRecord(
                snapshot.SnapshotIdentity.Fingerprint,
                ordinal,
                observation.Instant.UtcTicks,
                OffsetMinutes(observation.Instant),
                observation.Price.ToString("G29", CultureInfo.InvariantCulture)))
            .ToArray();
    }

    public static DatasetSnapshotCandidate ToSnapshotCandidate(
        SqliteDatasetSnapshotRecord snapshotRecord,
        IReadOnlyList<SqliteDatasetObservationRecord> observationRecords)
    {
        ArgumentNullException.ThrowIfNull(snapshotRecord);
        ArgumentNullException.ThrowIfNull(observationRecords);

        if (!string.Equals(snapshotRecord.IdentityScheme, DatasetIdentityScheme.Name, StringComparison.Ordinal))
        {
            throw new SqliteDatasetEvidenceException("The stored dataset identity scheme is incompatible.");
        }

        if (snapshotRecord.Ordering != (int)DatasetOrdering.SemanticInstantAscending)
        {
            throw new SqliteDatasetEvidenceException("The stored dataset ordering is incompatible.");
        }

        if (!Enum.IsDefined(typeof(DatasetSourceAuthority), snapshotRecord.SourceAuthority))
        {
            throw new SqliteDatasetEvidenceException("The stored dataset source authority is incompatible.");
        }

        if (snapshotRecord.ObservationCount != observationRecords.Count)
        {
            throw new SqliteDatasetEvidenceException("The stored dataset observation count is inconsistent.");
        }

        var snapshotIdentity = new DatasetSnapshotIdentity(snapshotRecord.SnapshotIdentity);
        var definitionIdentity = new DatasetDefinitionIdentity(snapshotRecord.DefinitionIdentity);
        var researchDatasetIdentity = new ResearchDatasetIdentity(snapshotRecord.ResearchDatasetIdentity);
        var sourceStateIdentity = new SourceStateIdentity(snapshotRecord.SourceStateIdentity);
        var definition = new DatasetDefinition(
            snapshotRecord.Target,
            ToInstant(snapshotRecord.RequestedFromUtcTicks, snapshotRecord.RequestedFromOffsetMinutes),
            ToInstant(snapshotRecord.RequestedToUtcTicks, snapshotRecord.RequestedToOffsetMinutes));
        var observations = ToObservations(snapshotIdentity, observationRecords);
        var version = new DatasetVersion(snapshotIdentity);
        var coverage = new DatasetCoverage(
            definition.From,
            definition.To,
            snapshotRecord.ObservationCount,
            ToNullableInstant(
                snapshotRecord.FirstObservationUtcTicks,
                snapshotRecord.FirstObservationOffsetMinutes),
            ToNullableInstant(
                snapshotRecord.LastObservationUtcTicks,
                snapshotRecord.LastObservationOffsetMinutes));
        var sourceAuthority = (DatasetSourceAuthority)snapshotRecord.SourceAuthority;
        var provenance = new DatasetProvenance(
            definition,
            definitionIdentity,
            researchDatasetIdentity,
            sourceStateIdentity,
            snapshotIdentity,
            version,
            sourceAuthority,
            snapshotRecord.ObservationCount);
        var lineage = new DatasetLineage(definitionIdentity, sourceStateIdentity, observations);

        return new DatasetSnapshotCandidate(
            definition,
            definitionIdentity,
            researchDatasetIdentity,
            sourceStateIdentity,
            snapshotIdentity,
            version,
            observations,
            coverage,
            provenance,
            lineage);
    }

    public static DatasetCatalogEntry ToCatalogEntry(
        SqliteDatasetSnapshotRecord snapshotRecord,
        IReadOnlyList<SqliteDatasetObservationRecord> observationRecords) =>
        new(ToSnapshotCandidate(snapshotRecord, observationRecords));

    private static PriceObservation[] ToObservations(
        DatasetSnapshotIdentity snapshotIdentity,
        IReadOnlyList<SqliteDatasetObservationRecord> records)
    {
        var observations = new PriceObservation[records.Count];

        for (var index = 0; index < records.Count; index++)
        {
            var record = records[index]
                ?? throw new SqliteDatasetEvidenceException("Stored dataset observations cannot contain null records.");

            if (!string.Equals(record.SnapshotIdentity, snapshotIdentity.Fingerprint, StringComparison.Ordinal)
                || record.Ordinal != index)
            {
                throw new SqliteDatasetEvidenceException("Stored dataset observation membership or ordering is inconsistent.");
            }

            var price = decimal.Parse(record.PriceText, NumberStyles.Float, CultureInfo.InvariantCulture);
            observations[index] = new PriceObservation(
                ToInstant(record.InstantUtcTicks, record.OffsetMinutes),
                price);
        }

        return observations;
    }

    private static DateTimeOffset? ToNullableInstant(long? utcTicks, short? offsetMinutes)
    {
        if (utcTicks is null && offsetMinutes is null)
        {
            return null;
        }

        if (utcTicks is null || offsetMinutes is null)
        {
            throw new SqliteDatasetEvidenceException("Stored dataset coverage has an incomplete instant representation.");
        }

        return ToInstant(utcTicks.Value, offsetMinutes.Value);
    }

    private static DateTimeOffset ToInstant(long utcTicks, short offsetMinutes) =>
        new DateTimeOffset(utcTicks, TimeSpan.Zero).ToOffset(TimeSpan.FromMinutes(offsetMinutes));

    private static short OffsetMinutes(DateTimeOffset instant) =>
        checked((short)instant.Offset.TotalMinutes);
}
