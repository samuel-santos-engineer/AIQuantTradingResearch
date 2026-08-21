namespace AIQuantTradingResearch.Application.Pipelines;

public static class PipelineIdentityScheme
{
    public const string Name = "aiq-pipeline-identity-v1";
}

public sealed record PipelineDefinitionIdentity
{
    public PipelineDefinitionIdentity(string fingerprint)
    {
        Fingerprint = PipelineFingerprint.Validate(fingerprint);
        Scheme = PipelineIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record PipelineExecutionIdentity
{
    public PipelineExecutionIdentity(string fingerprint)
    {
        Fingerprint = PipelineFingerprint.Validate(fingerprint);
        Scheme = PipelineIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

internal static class PipelineFingerprint
{
    public static string Validate(string fingerprint)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fingerprint);

        if (fingerprint.Length != 64
            || fingerprint.Any(static character => !((character >= '0' && character <= '9')
                || (character >= 'a' && character <= 'f'))))
        {
            throw new ArgumentException(
                "Pipeline fingerprints must contain exactly 64 lowercase hexadecimal characters.",
                nameof(fingerprint));
        }

        return fingerprint;
    }
}
