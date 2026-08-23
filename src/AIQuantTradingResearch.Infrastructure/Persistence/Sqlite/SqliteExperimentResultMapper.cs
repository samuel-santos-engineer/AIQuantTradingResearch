using System.Globalization;
using System.Numerics;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class SqliteExperimentResultMapper
{
    public static SqliteExperimentResultRecord ToRecord(DurableExperimentEvidence evidence)
    {
        ArgumentNullException.ThrowIfNull(evidence);

        var provenance = evidence.Provenance;
        var summary = evidence.Summary;

        return new SqliteExperimentResultRecord(
            evidence.Identity.Fingerprint,
            ExperimentIdentityScheme.Name,
            evidence.Definition.Name,
            evidence.DefinitionIdentity.Fingerprint,
            FeatureIdentityScheme.Name,
            provenance.FeatureSetIdentity.Fingerprint,
            provenance.FeatureDefinitionIdentity.Fingerprint,
            DatasetIdentityScheme.Name,
            provenance.SnapshotIdentity.Fingerprint,
            provenance.DatasetDefinitionIdentity.Fingerprint,
            provenance.ResearchDatasetIdentity.Fingerprint,
            provenance.SourceStateIdentity.Fingerprint,
            (int)provenance.SourceAuthority,
            provenance.DatasetObservationCount,
            summary.Count,
            summary.HasAggregates ? 1 : 0,
            summary.ArithmeticMean is null ? null : CanonicalDecimal(summary.ArithmeticMean.Value),
            summary.Minimum is null ? null : CanonicalDecimal(summary.Minimum.Value),
            summary.Maximum is null ? null : CanonicalDecimal(summary.Maximum.Value));
    }

    public static DurableExperimentEvidence ToEvidence(SqliteExperimentResultRecord record)
    {
        ArgumentNullException.ThrowIfNull(record);

        if (record.ExperimentIdentityScheme != ExperimentIdentityScheme.Name
            || record.FeatureIdentityScheme != FeatureIdentityScheme.Name
            || record.DatasetIdentityScheme != DatasetIdentityScheme.Name
            || record.ExperimentDefinitionName != ExperimentDefinition.SimpleReturnDescriptiveSummaryV1Name
            || !Enum.IsDefined((DatasetSourceAuthority)record.SourceAuthority)
            || record.DatasetObservationCount < 0
            || record.AggregatesPresent is not 0 and not 1)
        {
            throw new ArgumentException("Stored experiment evidence has an unsupported semantic representation.", nameof(record));
        }

        var summary = record.AggregatesPresent == 0
            ? new ExperimentSummaryEvidence(record.SummaryCount, null, null, null)
            : new ExperimentSummaryEvidence(
                record.SummaryCount,
                ParseCanonicalDecimal(record.ArithmeticMeanCanonical),
                ParseCanonicalDecimal(record.MinimumCanonical),
                ParseCanonicalDecimal(record.MaximumCanonical));

        if (record.AggregatesPresent == 0
            && (record.ArithmeticMeanCanonical is not null
                || record.MinimumCanonical is not null
                || record.MaximumCanonical is not null))
        {
            throw new ArgumentException("Empty experiment evidence cannot persist aggregate values.", nameof(record));
        }

        var definitionIdentity = new ExperimentDefinitionIdentity(record.ExperimentDefinitionIdentity);
        var featureDefinitionIdentity = new FeatureDefinitionIdentity(record.FeatureDefinitionIdentity);
        var snapshotIdentity = new DatasetSnapshotIdentity(record.SnapshotIdentity);
        var provenance = new DurableExperimentProvenance(
            definitionIdentity,
            new FeatureSetIdentity(record.FeatureSetIdentity),
            featureDefinitionIdentity,
            snapshotIdentity,
            new DatasetVersion(snapshotIdentity),
            new DatasetDefinitionIdentity(record.DatasetDefinitionIdentity),
            new ResearchDatasetIdentity(record.ResearchDatasetIdentity),
            new SourceStateIdentity(record.SourceStateIdentity),
            (DatasetSourceAuthority)record.SourceAuthority,
            record.DatasetObservationCount);
        var lineage = new DurableExperimentLineage(
            definitionIdentity,
            featureDefinitionIdentity,
            provenance.DatasetDefinitionIdentity,
            provenance.SourceStateIdentity);

        return new DurableExperimentEvidence(
            ExperimentDefinition.SimpleReturnDescriptiveSummaryV1,
            definitionIdentity,
            new ExperimentResultIdentity(record.ExperimentResultIdentity),
            summary,
            provenance,
            lineage);
    }

    private static string CanonicalDecimal(decimal value)
    {
        var bits = decimal.GetBits(value);
        var coefficient = new BigInteger((uint)bits[0])
            | ((BigInteger)(uint)bits[1] << 32)
            | ((BigInteger)(uint)bits[2] << 64);
        var scale = (bits[3] >> 16) & 0x7f;

        while (scale > 0 && coefficient % 10 == BigInteger.Zero)
        {
            coefficient /= 10;
            scale--;
        }

        return string.Concat(
            (bits[3] & int.MinValue) == 0 ? "0" : "1",
            ",",
            coefficient,
            ",",
            scale);
    }

    private static decimal ParseCanonicalDecimal(string? value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var fields = value.Split(',');
        if (fields.Length != 3
            || (fields[0] is not "0" and not "1")
            || !BigInteger.TryParse(fields[1], NumberStyles.None, CultureInfo.InvariantCulture, out var coefficient)
            || coefficient < BigInteger.Zero
            || !int.TryParse(fields[2], NumberStyles.None, CultureInfo.InvariantCulture, out var scale)
            || scale is < 0 or > 28
            || coefficient > ((BigInteger.One << 96) - BigInteger.One))
        {
            throw new ArgumentException("Stored decimal is not canonical durable evidence.", nameof(value));
        }

        var bits = new[]
        {
            (int)(uint)(coefficient & uint.MaxValue),
            (int)(uint)((coefficient >> 32) & uint.MaxValue),
            (int)(uint)((coefficient >> 64) & uint.MaxValue),
            (scale << 16) | (fields[0] == "1" ? int.MinValue : 0),
        };
        var result = new decimal(bits);

        if (CanonicalDecimal(result) != value)
        {
            throw new ArgumentException("Stored decimal is not in canonical durable form.", nameof(value));
        }

        return result;
    }
}
