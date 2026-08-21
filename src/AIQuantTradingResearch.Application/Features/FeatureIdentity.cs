namespace AIQuantTradingResearch.Application.Features;

public static class FeatureIdentityScheme
{
    public const string Name = "aiq-feature-identity-v1";
}

public sealed record FeatureDefinitionIdentity
{
    public FeatureDefinitionIdentity(string fingerprint)
    {
        Fingerprint = FeatureFingerprint.Validate(fingerprint);
        Scheme = FeatureIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record FeatureSetIdentity
{
    public FeatureSetIdentity(string fingerprint)
    {
        Fingerprint = FeatureFingerprint.Validate(fingerprint);
        Scheme = FeatureIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

internal static class FeatureFingerprint
{
    public static string Validate(string fingerprint)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fingerprint);

        if (fingerprint.Length != 64
            || fingerprint.Any(static character => !((character >= '0' && character <= '9')
                || (character >= 'a' && character <= 'f'))))
        {
            throw new ArgumentException(
                "Feature fingerprints must contain exactly 64 lowercase hexadecimal characters.",
                nameof(fingerprint));
        }

        return fingerprint;
    }
}
