using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Reflection;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class WorkerObservabilityLifecycleTests
{
    [Fact]
    public void CoordinatorIsBoundedExactlyOnceAndDisposesIdempotently()
    {
        var activities = new List<Activity>();
        using var activityListener = new ActivityListener
        {
            ShouldListenTo = source => source.Name == "AIQuantTradingResearch.Worker",
            Sample = static (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
            ActivityStopped = activity => activities.Add(activity)
        };
        ActivitySource.AddActivityListener(activityListener);

        var measurements = new List<long>();
        using var meterListener = new MeterListener();
        meterListener.InstrumentPublished = static (instrument, listener) =>
        {
            if (instrument.Meter.Name == "AIQuantTradingResearch.Worker")
            {
                listener.EnableMeasurementEvents(instrument);
            }
        };
        meterListener.SetMeasurementEventCallback<long>((instrument, measurement, _, _) =>
        {
            if (instrument.Name == "worker.lifecycle.events") measurements.Add(measurement);
        });
        meterListener.Start();

        var type = Assembly.Load("AIQuantTradingResearch.Worker")
            .GetType("AIQuantTradingResearch.Worker.WorkerObservabilityLifecycle", throwOnError: true)!;
        var lifecycle = Activator.CreateInstance(type)!;
        var cancellation = new CancellationTokenSource();
        Invoke(type, lifecycle, "MarkReady");
        Invoke(type, lifecycle, "ObserveCancellation", cancellation.Token);
        cancellation.Cancel();
        Invoke(type, lifecycle, "MarkRestart");
        Invoke(type, lifecycle, "MarkFailed");
        Invoke(type, lifecycle, "Dispose");
        Invoke(type, lifecycle, "Dispose");

        var lifecycleActivities = activities
            .Where(activity => activity.OperationName == "worker.lifecycle")
            .ToArray();

        Assert.Equal(7, lifecycleActivities.Length);
        Assert.Equal(7, measurements.Sum());
        Assert.All(lifecycleActivities, activity =>
        {
            Assert.Equal("worker", activity.GetTagItem("aiq.component"));
            Assert.Equal("worker.lifecycle", activity.GetTagItem("aiq.operation"));
            Assert.DoesNotContain(activity.Tags, tag => tag.Key is "aiq.error_class" or "aiq.target" or "aiq.path");
        });
        Assert.Contains(lifecycleActivities, activity => activity.Events.Any(@event => @event.Name == "disabled"));
        Assert.Single(lifecycleActivities, activity => activity.Events.Any(@event => @event.Name == "shutdown"));
    }

    private static void Invoke(Type type, object instance, string name, params object[] arguments) =>
        type.GetMethod(name, BindingFlags.Instance | BindingFlags.Public)!
            .Invoke(instance, arguments);
}
