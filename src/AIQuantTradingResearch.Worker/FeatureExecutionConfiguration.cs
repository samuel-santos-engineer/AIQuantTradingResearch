using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Features;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class FeatureExecutionConfiguration
{
    private const string SnapshotIdentityPath = "Feature:SnapshotIdentity";
    private const string SnapshotVersionPath = "Feature:SnapshotVersion";

    private FeatureExecutionConfiguration(FeatureGenerationRequest request)
    {
        Request = request;
    }

    public FeatureGenerationRequest Request { get; }

    public static FeatureExecutionConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var snapshotIdentity = new DatasetSnapshotIdentity(RequireValue(configuration[SnapshotIdentityPath], SnapshotIdentityPath));
        var snapshotVersion = new DatasetVersion(new DatasetSnapshotIdentity(RequireValue(configuration[SnapshotVersionPath], SnapshotVersionPath)));
        return new FeatureExecutionConfiguration(new FeatureGenerationRequest(
            FeatureDefinition.SimpleReturnLag1V1,
            snapshotIdentity,
            snapshotVersion));
    }

    private static string RequireValue(string? value, string path)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Missing mandatory configuration: {path}.", path);
        }

        return value;
    }
}
