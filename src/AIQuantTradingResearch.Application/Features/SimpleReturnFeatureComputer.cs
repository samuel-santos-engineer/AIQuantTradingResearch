using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Features;

internal sealed class SimpleReturnFeatureComputer : IFeatureComputer
{
    public FeatureSet Compute(FeatureGenerationRequest request, DatasetSnapshotCandidate snapshot)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(snapshot);

        if (request.Definition != FeatureDefinition.SimpleReturnLag1V1
            || request.SnapshotIdentity != snapshot.SnapshotIdentity
            || request.SnapshotVersion != snapshot.Version)
        {
            throw new ArgumentException("The request must reference the accepted simple-return definition and exact snapshot.");
        }

        var values = new List<FeatureValue>(Math.Max(0, snapshot.Observations.Count - 1));
        for (var index = 1; index < snapshot.Observations.Count; index++)
        {
            var prior = snapshot.Observations[index - 1];
            var current = snapshot.Observations[index];
            if (prior.Price == 0m)
            {
                throw new ArgumentException("A zero predecessor price is invalid numeric evidence.", nameof(snapshot));
            }

            try { values.Add(new FeatureValue(current.Instant, (current.Price / prior.Price) - 1m)); }
            catch (OverflowException exception) { throw new ArgumentException("Feature arithmetic exceeded decimal representation.", nameof(snapshot), exception); }
        }

        var definitionIdentity = FeatureIdentityComputer.ComputeDefinitionIdentity();
        var setIdentity = FeatureIdentityComputer.ComputeSetIdentity(definitionIdentity, snapshot.SnapshotIdentity, snapshot.Version, values);
        return new FeatureSet(definitionIdentity, setIdentity,
            new FeatureProvenance(definitionIdentity, snapshot.SnapshotIdentity, snapshot.Version, snapshot.Provenance),
            new FeatureLineage(definitionIdentity, snapshot.Lineage), values);
    }
}

internal static class FeatureIdentityComputer
{
    private static readonly Encoding Utf8 = new UTF8Encoding(false);
    public static FeatureDefinitionIdentity ComputeDefinitionIdentity() => new(Fingerprint("feature-definition", [FeatureIdentityScheme.Name, FeatureDefinition.SimpleReturnLag1V1Name, "1", "(p[i] / p[i-1]) - 1", "current-observation-timestamp-offset", "decimal", "zero-predecessor-invalid", "empty-success", "single-success"]));
    public static FeatureSetIdentity ComputeSetIdentity(FeatureDefinitionIdentity definition, DatasetSnapshotIdentity snapshot, DatasetVersion version, IReadOnlyList<FeatureValue> values)
    {
        var fields = new List<string> { FeatureIdentityScheme.Name, definition.Scheme, definition.Fingerprint, snapshot.Scheme, snapshot.Fingerprint, version.SnapshotIdentity.Scheme, version.SnapshotIdentity.Fingerprint, values.Count.ToString(CultureInfo.InvariantCulture) };
        for (var i = 0; i < values.Count; i++) { var v = values[i]; fields.Add(i.ToString(CultureInfo.InvariantCulture)); fields.Add(v.Instant.UtcTicks.ToString(CultureInfo.InvariantCulture)); fields.Add((v.Instant.Offset.Ticks / TimeSpan.TicksPerMinute).ToString(CultureInfo.InvariantCulture)); fields.Add(Decimal(v.Value)); }
        return new FeatureSetIdentity(Fingerprint("feature-set", fields));
    }
    private static string Fingerprint(string domain, List<string> fields) { var b = new StringBuilder(); Add(b, domain); Add(b, FeatureIdentityScheme.Name); Add(b, fields.Count.ToString(CultureInfo.InvariantCulture)); foreach (var f in fields) Add(b, f); return Convert.ToHexString(SHA256.HashData(Utf8.GetBytes(b.ToString()))).ToLowerInvariant(); }
    private static void Add(StringBuilder b, string s) { b.Append(Utf8.GetByteCount(s).ToString(CultureInfo.InvariantCulture)).Append(':').Append(s); }
    private static string Decimal(decimal value) { var bits = decimal.GetBits(value); var c = new BigInteger((uint)bits[0]) | ((BigInteger)(uint)bits[1] << 32) | ((BigInteger)(uint)bits[2] << 64); var scale = (bits[3] >> 16) & 0x7f; while (scale > 0 && c % 10 == 0) { c /= 10; scale--; } return string.Concat((bits[3] & int.MinValue) == 0 ? "0" : "1", ",", c.ToString(CultureInfo.InvariantCulture), ",", scale.ToString(CultureInfo.InvariantCulture)); }
}
