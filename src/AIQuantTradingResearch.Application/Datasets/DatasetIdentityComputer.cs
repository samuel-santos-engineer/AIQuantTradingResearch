using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Datasets;

internal static class DatasetIdentityComputer
{
    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

    public static DatasetDefinitionIdentity ComputeDefinitionIdentity(DatasetDefinition definition) =>
        new(ComputeFingerprint("dataset-definition", DefinitionFields(definition)));

    public static ResearchDatasetIdentity ComputeResearchDatasetIdentity(DatasetDefinition definition) =>
        new(ComputeFingerprint("research-dataset", DefinitionFields(definition)));

    public static SourceStateIdentity ComputeSourceStateIdentity(
        string target,
        IReadOnlyList<PriceObservation> observations)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);
        ArgumentNullException.ThrowIfNull(observations);

        var fields = new List<string> { target, observations.Count.ToString(CultureInfo.InvariantCulture) };

        foreach (var observation in observations)
        {
            ArgumentNullException.ThrowIfNull(observation);
            fields.Add(observation.Instant.UtcTicks.ToString(CultureInfo.InvariantCulture));
            fields.Add((observation.Instant.Offset.Ticks / TimeSpan.TicksPerMinute).ToString(CultureInfo.InvariantCulture));
            fields.Add(CanonicalDecimal(observation.Price));
        }

        return new SourceStateIdentity(ComputeFingerprint("source-state", fields));
    }

    public static DatasetSnapshotIdentity ComputeSnapshotIdentity(
        DatasetDefinitionIdentity definitionIdentity,
        SourceStateIdentity sourceStateIdentity)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(sourceStateIdentity);

        return new DatasetSnapshotIdentity(ComputeFingerprint(
            "dataset-snapshot",
            [
                "dataset-definition",
                definitionIdentity.Scheme,
                definitionIdentity.Fingerprint,
                "source-state",
                sourceStateIdentity.Scheme,
                sourceStateIdentity.Fingerprint,
            ]));
    }

    private static IReadOnlyList<string> DefinitionFields(DatasetDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        return
        [
            definition.Target,
            definition.From.UtcTicks.ToString(CultureInfo.InvariantCulture),
            definition.To.UtcTicks.ToString(CultureInfo.InvariantCulture),
            "lower-inclusive",
            "upper-exclusive",
            "semantic-instant-ascending",
            "0",
        ];
    }

    private static string ComputeFingerprint(string typeDomain, IReadOnlyList<string> semanticFields)
    {
        var canonical = new StringBuilder();
        AppendField(canonical, typeDomain);
        AppendField(canonical, DatasetIdentityScheme.Name);
        AppendField(canonical, semanticFields.Count.ToString(CultureInfo.InvariantCulture));

        foreach (var field in semanticFields)
        {
            ArgumentNullException.ThrowIfNull(field);
            AppendField(canonical, field);
        }

        var digest = SHA256.HashData(Utf8.GetBytes(canonical.ToString()));
        return Convert.ToHexString(digest).ToLowerInvariant();
    }

    private static void AppendField(StringBuilder canonical, string value)
    {
        canonical.Append(Utf8.GetByteCount(value).ToString(CultureInfo.InvariantCulture));
        canonical.Append(':');
        canonical.Append(value);
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

        var sign = (bits[3] & int.MinValue) == 0 ? "0" : "1";
        return string.Concat(
            sign,
            ",",
            coefficient.ToString(CultureInfo.InvariantCulture),
            ",",
            scale.ToString(CultureInfo.InvariantCulture));
    }
}
