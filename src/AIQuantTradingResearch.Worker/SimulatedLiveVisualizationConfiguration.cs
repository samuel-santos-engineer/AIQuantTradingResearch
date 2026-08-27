using System.Globalization;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Research;
using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal enum WorkerExecutionMode
{
    Historical,
    Replay,
}

internal sealed record WorkerReplayConfiguration(
    string ReplayIdentity,
    string Target,
    int StartingTick,
    int RequestedObservationCount);

internal sealed record SimulatedLiveVisualizationConfiguration(
    WorkerExecutionMode Mode,
    DatasetDefinition DatasetDefinition,
    WorkerReplayConfiguration? Replay)
{
    public static SimulatedLiveVisualizationConfiguration From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var modeValue = configuration["Worker:Mode"];
        WorkerExecutionMode mode = string.IsNullOrWhiteSpace(modeValue)
            ? WorkerExecutionMode.Historical
            : ParseMode(modeValue);
        DatasetDefinition dataset = PipelineExecutionConfiguration.From(configuration).DatasetDefinition;

        if (mode == WorkerExecutionMode.Historical)
        {
            return new(mode, dataset, null);
        }

        var identity = Require(configuration["Worker:Replay:ReplayIdentity"], "Worker:Replay:ReplayIdentity");
        var target = Require(configuration["Worker:Replay:Target"], "Worker:Replay:Target");
        if (!string.Equals(dataset.Target, target, StringComparison.Ordinal))
        {
            throw new ArgumentException("Dataset:Target must exactly match Worker:Replay:Target.");
        }

        int startingTick = ParseInt(configuration["Worker:Replay:StartingTick"], "Worker:Replay:StartingTick");
        int count = ParseInt(configuration["Worker:Replay:RequestedObservationCount"], "Worker:Replay:RequestedObservationCount");
        if (startingTick < 0)
        {
            throw new ArgumentException("Worker:Replay:StartingTick must be non-negative.");
        }

        if (count <= 0)
        {
            throw new ArgumentException("Worker:Replay:RequestedObservationCount must be positive.");
        }

        return new(mode, dataset, new(identity, target, startingTick, count));
    }

    private static WorkerExecutionMode ParseMode(string value) =>
        value switch
        {
            _ when string.Equals(value, "Historical", StringComparison.OrdinalIgnoreCase) => WorkerExecutionMode.Historical,
            _ when string.Equals(value, "Replay", StringComparison.OrdinalIgnoreCase) => WorkerExecutionMode.Replay,
            _ => throw new ArgumentException("Worker:Mode must be Historical or Replay."),
        };

    private static string Require(string? value, string path) =>
        string.IsNullOrWhiteSpace(value)
            ? throw new ArgumentException($"Missing mandatory configuration: {path}.", path)
            : value;

    private static int ParseInt(string? value, string path) =>
        int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var parsed)
            ? parsed
            : throw new ArgumentException($"Missing or invalid mandatory configuration: {path}.", path);
}
