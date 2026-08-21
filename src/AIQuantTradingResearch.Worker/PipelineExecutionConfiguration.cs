using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class PipelineExecutionConfiguration
{
    private const string DatasetTargetPath = "Dataset:Target";
    private const string DatasetFromPath = "Dataset:From";
    private const string DatasetToPath = "Dataset:To";

    private PipelineExecutionConfiguration(DatasetDefinition datasetDefinition)
    {
        DatasetDefinition = datasetDefinition;
    }

    public DatasetDefinition DatasetDefinition { get; }

    public static PipelineExecutionConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return new PipelineExecutionConfiguration(new DatasetDefinition(
            RequireValue(configuration[DatasetTargetPath], DatasetTargetPath),
            ParseRequiredTimestamp(configuration[DatasetFromPath], DatasetFromPath),
            ParseRequiredTimestamp(configuration[DatasetToPath], DatasetToPath)));
    }

    private static string RequireValue(string? value, string configurationPath)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"Missing mandatory configuration: {configurationPath}.", configurationPath);
        }

        return value;
    }

    private static DateTimeOffset ParseRequiredTimestamp(string? value, string configurationPath)
    {
        if (string.IsNullOrWhiteSpace(value)
            || !DateTimeOffset.TryParseExact(
                value,
                "O",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTimeOffset timestamp))
        {
            throw new ArgumentException($"Missing or invalid mandatory configuration: {configurationPath}.", configurationPath);
        }

        return timestamp;
    }
}
