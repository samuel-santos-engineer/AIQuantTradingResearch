using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class DurableExperimentDiscoveryConfiguration
{
    private const string SnapshotIdentityPath = "DurableExperimentDiscovery:SnapshotIdentity";
    private const string ExperimentDefinitionIdentityPath =
        "DurableExperimentDiscovery:ExperimentDefinitionIdentity";
    private const string MaximumResultCountPath = "DurableExperimentDiscovery:MaximumResultCount";

    private DurableExperimentDiscoveryConfiguration(DurableExperimentDiscoveryRequest request) =>
        Request = request;

    public DurableExperimentDiscoveryRequest Request { get; }

    public static DurableExperimentDiscoveryConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var snapshotIdentity = new DatasetSnapshotIdentity(
            RequireValue(configuration[SnapshotIdentityPath], SnapshotIdentityPath));
        var experimentDefinitionIdentity = new ExperimentDefinitionIdentity(
            RequireValue(
                configuration[ExperimentDefinitionIdentityPath],
                ExperimentDefinitionIdentityPath));
        var maximumResultCount = ParsePositiveInteger(
            configuration[MaximumResultCountPath],
            MaximumResultCountPath);

        return new DurableExperimentDiscoveryConfiguration(new DurableExperimentDiscoveryRequest(
            snapshotIdentity,
            experimentDefinitionIdentity,
            maximumResultCount));
    }

    private static string RequireValue(string? value, string path)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Missing mandatory configuration: {path}.", path);
        }

        return value;
    }

    private static int ParsePositiveInteger(string? value, string path)
    {
        if (!int.TryParse(
                value,
                NumberStyles.None,
                CultureInfo.InvariantCulture,
                out var maximumResultCount)
            || maximumResultCount <= 0)
        {
            throw new ArgumentException($"Missing or invalid mandatory configuration: {path}.", path);
        }

        return maximumResultCount;
    }
}
