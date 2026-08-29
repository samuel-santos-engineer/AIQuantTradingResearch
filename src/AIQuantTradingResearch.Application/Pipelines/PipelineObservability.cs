using System.Diagnostics;
using System.Diagnostics.Metrics;
using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

internal static class PipelineObservability
{
    internal const string ActivitySourceName = "AIQuantTradingResearch.Pipeline";
    internal const string MeterName = "AIQuantTradingResearch.Pipeline";
    internal const string PipelineExecutionActivityName = "pipeline.execute";
    internal const string PipelineStageActivityName = "pipeline.stage";

    private static readonly ActivitySource Source = new(ActivitySourceName);
    private static readonly Meter Meter = new(MeterName);
    private static readonly Counter<long> Operations = Meter.CreateCounter<long>(
        "pipeline.operations", "{operation}", "Count of bounded pipeline observations.");
    private static readonly Counter<long> Failures = Meter.CreateCounter<long>(
        "pipeline.failures", "{operation}", "Count of bounded failed pipeline observations.");
    private static readonly Histogram<double> Duration = Meter.CreateHistogram<double>(
        "pipeline.duration", "ms", "Duration of bounded pipeline observations.");

    internal static Activity? StartPipelineExecution() => Start(PipelineExecutionActivityName, null);

    internal static Activity? StartStage(ResearchPipelineStage stage) => Start(PipelineStageActivityName, stage);

    internal static long GetTimestamp() => Stopwatch.GetTimestamp();

    internal static void Complete(
        Activity? activity,
        long startedTimestamp,
        string operation,
        ResearchPipelineStage? stage,
        string outcome,
        PipelineFailureCategory? failureCategory = null,
        DatasetSourceAuthority? sourceAuthority = null)
    {
        var tags = new TagList
        {
            { "aiq.release", "1.10" },
            { "aiq.component", "application" },
            { "aiq.operation", operation },
            { "aiq.outcome", outcome },
        };

        if (stage is not null)
        {
            tags.Add("aiq.stage", stage.Value.ToString());
        }

        if (failureCategory is not null)
        {
            tags.Add("aiq.error_class", failureCategory.Value.ToString());
        }

        if (sourceAuthority is not null)
        {
            tags.Add("aiq.provenance", MapProvenance(sourceAuthority.Value));
        }

        foreach (KeyValuePair<string, object?> tag in tags)
        {
            activity?.SetTag(tag.Key, tag.Value);
        }

        if (outcome == "failed")
        {
            activity?.SetStatus(ActivityStatusCode.Error);
            Failures.Add(1, tags);
        }
        else
        {
            activity?.SetStatus(ActivityStatusCode.Ok);
        }

        Operations.Add(1, tags);
        Duration.Record(Stopwatch.GetElapsedTime(startedTimestamp).TotalMilliseconds, tags);
    }

    private static Activity? Start(string operation, ResearchPipelineStage? stage)
    {
        Activity? activity = Source.StartActivity(operation, ActivityKind.Internal);
        activity?.SetTag("aiq.release", "1.10");
        activity?.SetTag("aiq.component", "application");
        activity?.SetTag("aiq.operation", operation);

        if (stage is not null)
        {
            activity?.SetTag("aiq.stage", stage.Value.ToString());
        }

        return activity;
    }

    private static string MapProvenance(DatasetSourceAuthority sourceAuthority) => sourceAuthority switch
    {
        DatasetSourceAuthority.AcceptedRelease11HistoricalObservations => "historical",
        DatasetSourceAuthority.Release19SimulatedLiveReplay => "simulated",
        _ => throw new ArgumentOutOfRangeException(nameof(sourceAuthority), sourceAuthority, "Unknown dataset source authority."),
    };
}
