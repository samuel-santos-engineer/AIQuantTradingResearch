using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class ExperimentExecutionConfiguration
{
    private const string SnapshotIdentityPath = "Experiment:SnapshotIdentity";
    private const string SnapshotVersionPath = "Experiment:SnapshotVersion";

    private ExperimentExecutionConfiguration(ExperimentGenerationRequest request)
    {
        Request = request;
    }

    public ExperimentGenerationRequest Request { get; }

    public static ExperimentExecutionConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var snapshotIdentity = new DatasetSnapshotIdentity(
            RequireValue(configuration[SnapshotIdentityPath], SnapshotIdentityPath));
        var snapshotVersion = new DatasetVersion(new DatasetSnapshotIdentity(
            RequireValue(configuration[SnapshotVersionPath], SnapshotVersionPath)));
        return new ExperimentExecutionConfiguration(new ExperimentGenerationRequest(
            ExperimentDefinition.SimpleReturnDescriptiveSummaryV1,
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
