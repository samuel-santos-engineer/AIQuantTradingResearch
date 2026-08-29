using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace AIQuantTradingResearch.Worker;

internal sealed class WorkerObservabilityLifecycle : IDisposable
{
    internal const string SourceName = "AIQuantTradingResearch.Worker";
    internal const string LifecycleActivityName = "worker.lifecycle";

    private static readonly ActivitySource ActivitySource = new(SourceName);
    private static readonly Meter Meter = new(SourceName);
    private static readonly Counter<long> LifecycleEvents =
        Meter.CreateCounter<long>("worker.lifecycle.events", "{event}");

    private CancellationTokenRegistration cancellationRegistration;
    private int cancellationObserved;
    private int disposed;
    private int lifecycleEventsRecorded;

    public WorkerObservabilityLifecycle()
    {
        Record("startup", "success");
        Record("disabled", "degraded");
    }

    public void MarkReady() => Record("ready", "success");

    public void MarkRestart() => Record("restart", "success");

    public void MarkFailed() => Record("failed", "failed");

    public void ObserveCancellation(CancellationToken cancellationToken)
    {
        if (!cancellationToken.CanBeCanceled || Interlocked.Exchange(ref cancellationObserved, 1) != 0)
        {
            return;
        }

        cancellationRegistration = cancellationToken.Register(static state =>
            ((WorkerObservabilityLifecycle)state!).Record("cancelled", "cancelled"), this);
    }

    public void Dispose()
    {
        if (Interlocked.Exchange(ref disposed, 1) != 0)
        {
            return;
        }

        cancellationRegistration.Dispose();
        Record("shutdown", "success");
    }

    private void Record(string lifecycleEvent, string outcome)
    {
        Interlocked.Increment(ref lifecycleEventsRecorded);
        var tags = new TagList
        {
            { "aiq.release", "1.10" },
            { "aiq.component", "worker" },
            { "aiq.operation", LifecycleActivityName },
            { "aiq.outcome", outcome }
        };

        using var activity = ActivitySource.StartActivity(LifecycleActivityName);
        activity?.SetTag("aiq.release", "1.10");
        activity?.SetTag("aiq.component", "worker");
        activity?.SetTag("aiq.operation", LifecycleActivityName);
        activity?.SetTag("aiq.outcome", outcome);
        activity?.AddEvent(new ActivityEvent(lifecycleEvent));
        activity?.SetStatus(outcome == "failed" ? ActivityStatusCode.Error : ActivityStatusCode.Ok);
        LifecycleEvents.Add(1, tags);
    }
}
