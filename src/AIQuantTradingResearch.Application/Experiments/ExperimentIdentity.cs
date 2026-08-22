using System.Globalization;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

public static class ExperimentIdentityScheme
{
    public const string Name = "aiq-experiment-identity-v1";
}

public sealed record ExperimentDefinitionIdentity
{
    public ExperimentDefinitionIdentity(string fingerprint)
    {
        Fingerprint = ExperimentFingerprint.Validate(fingerprint);
        Scheme = ExperimentIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record ExperimentResultIdentity
{
    public ExperimentResultIdentity(string fingerprint)
    {
        Fingerprint = ExperimentFingerprint.Validate(fingerprint);
        Scheme = ExperimentIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

internal static class ExperimentFingerprint
{
    public static string Validate(string fingerprint)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fingerprint);

        if (fingerprint.Length != 64
            || fingerprint.Any(static character => !((character >= '0' && character <= '9')
                || (character >= 'a' && character <= 'f'))))
        {
            throw new ArgumentException(
                "Experiment fingerprints must contain exactly 64 lowercase hexadecimal characters.",
                nameof(fingerprint));
        }

        return fingerprint;
    }
}

internal static class ExperimentIdentityComputer
{
    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

    public static ExperimentDefinitionIdentity ComputeDefinitionIdentity(
        ExperimentDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        if (definition != ExperimentDefinition.SimpleReturnDescriptiveSummaryV1)
        {
            throw new ArgumentException("The experiment definition is not supported.", nameof(definition));
        }

        return new ExperimentDefinitionIdentity(Fingerprint(
            "experiment-definition",
            [
                ExperimentIdentityScheme.Name,
                ExperimentDefinition.SimpleReturnDescriptiveSummaryV1Name,
                FeatureDefinition.SimpleReturnLag1V1Name,
                "complete-feature-set-cardinality",
                "(x[0] + ... + x[N-1]) / N",
                "min(x[0], ..., x[N-1])",
                "max(x[0], ..., x[N-1])",
                "count-zero-aggregates-absent",
                "count-positive-all-aggregates-present",
                "decimal-exact-no-rounding",
                "exact-feature-set-no-reorder-no-filter",
            ]));
    }

    public static ExperimentResultIdentity ComputeResultIdentity(
        ExperimentDefinitionIdentity definitionIdentity,
        FeatureSetIdentity featureSetIdentity,
        ExperimentSummaryEvidence summary)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(featureSetIdentity);
        ArgumentNullException.ThrowIfNull(summary);

        var aggregatesPresent = summary.HasAggregates;

        return new ExperimentResultIdentity(Fingerprint(
            "experiment-result",
            [
                ExperimentIdentityScheme.Name,
                definitionIdentity.Scheme,
                definitionIdentity.Fingerprint,
                featureSetIdentity.Scheme,
                featureSetIdentity.Fingerprint,
                summary.Count.ToString(CultureInfo.InvariantCulture),
                aggregatesPresent ? "1" : "0",
                aggregatesPresent ? CanonicalDecimal(summary.ArithmeticMean!.Value) : string.Empty,
                aggregatesPresent ? CanonicalDecimal(summary.Minimum!.Value) : string.Empty,
                aggregatesPresent ? CanonicalDecimal(summary.Maximum!.Value) : string.Empty,
            ]));
    }

    private static string Fingerprint(string typeDomain, IReadOnlyList<string> semanticFields)
    {
        var canonical = new StringBuilder();
        AppendField(canonical, typeDomain);
        AppendField(canonical, ExperimentIdentityScheme.Name);
        AppendField(canonical, semanticFields.Count.ToString(CultureInfo.InvariantCulture));

        foreach (var field in semanticFields)
        {
            ArgumentNullException.ThrowIfNull(field);
            AppendField(canonical, field);
        }

        return Convert.ToHexString(SHA256.HashData(Utf8.GetBytes(canonical.ToString())))
            .ToLowerInvariant();
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

        return string.Concat(
            (bits[3] & int.MinValue) == 0 ? "0" : "1",
            ",",
            coefficient.ToString(CultureInfo.InvariantCulture),
            ",",
            scale.ToString(CultureInfo.InvariantCulture));
    }
}
