using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class DurableExperimentExecutionConfiguration
{
    private const string SnapshotIdentityPath = "DurableExperiment:SnapshotIdentity";
    private const string SnapshotVersionPath = "DurableExperiment:SnapshotVersion";

    private DurableExperimentExecutionConfiguration(ExperimentGenerationRequest request) => Request = request;

    public ExperimentGenerationRequest Request { get; }

    public static DurableExperimentExecutionConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        var identity = new DatasetSnapshotIdentity(Require(configuration[SnapshotIdentityPath], SnapshotIdentityPath));
        var version = new DatasetVersion(new DatasetSnapshotIdentity(Require(configuration[SnapshotVersionPath], SnapshotVersionPath)));
        return new DurableExperimentExecutionConfiguration(new ExperimentGenerationRequest(ExperimentDefinition.SimpleReturnDescriptiveSummaryV1, identity, version));
    }

    private static string Require(string? value, string key) => string.IsNullOrWhiteSpace(value)
        ? throw new ArgumentException($"Missing mandatory configuration: {key}.", key)
        : value;
}
