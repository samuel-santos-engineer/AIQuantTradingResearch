namespace AIQuantTradingResearch.Application.Datasets;

public static class DatasetIdentityScheme
{
    public const string Name = "aiq-dataset-identity-v1";
}

public sealed record DatasetDefinitionIdentity
{
    public DatasetDefinitionIdentity(string fingerprint)
    {
        Fingerprint = DatasetFingerprint.Validate(fingerprint);
        Scheme = DatasetIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record ResearchDatasetIdentity
{
    public ResearchDatasetIdentity(string fingerprint)
    {
        Fingerprint = DatasetFingerprint.Validate(fingerprint);
        Scheme = DatasetIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record SourceStateIdentity
{
    public SourceStateIdentity(string fingerprint)
    {
        Fingerprint = DatasetFingerprint.Validate(fingerprint);
        Scheme = DatasetIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record DatasetSnapshotIdentity
{
    public DatasetSnapshotIdentity(string fingerprint)
    {
        Fingerprint = DatasetFingerprint.Validate(fingerprint);
        Scheme = DatasetIdentityScheme.Name;
    }

    public string Scheme { get; }

    public string Fingerprint { get; }
}

public sealed record DatasetVersion
{
    public DatasetVersion(DatasetSnapshotIdentity snapshotIdentity)
    {
        ArgumentNullException.ThrowIfNull(snapshotIdentity);
        SnapshotIdentity = snapshotIdentity;
    }

    public DatasetSnapshotIdentity SnapshotIdentity { get; }
}

internal static class DatasetFingerprint
{
    public static string Validate(string fingerprint)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(fingerprint);

        if (fingerprint.Length != 64
            || fingerprint.Any(static character => !((character >= '0' && character <= '9')
                || (character >= 'a' && character <= 'f'))))
        {
            throw new ArgumentException(
                "Dataset fingerprints must contain exactly 64 lowercase hexadecimal characters.",
                nameof(fingerprint));
        }

        return fingerprint;
    }
}
