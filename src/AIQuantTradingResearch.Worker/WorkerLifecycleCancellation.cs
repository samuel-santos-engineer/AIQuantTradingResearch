namespace AIQuantTradingResearch.Worker;

internal interface IWorkerLifecycleLivenessGate
{
    Task Entered { get; }
    Task AwaitReleaseAsync(CancellationToken cancellationToken);
}

internal sealed class NoOpWorkerLifecycleLivenessGate : IWorkerLifecycleLivenessGate
{
    public Task Entered => Task.CompletedTask;
    public Task AwaitReleaseAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

internal sealed class TestWorkerLifecycleLivenessGate : IWorkerLifecycleLivenessGate
{
    private readonly TaskCompletionSource entered = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private int publicationCount;

    public Task Entered => entered.Task;

    public Task AwaitReleaseAsync(CancellationToken cancellationToken)
    {
        if (Interlocked.Increment(ref publicationCount) == 1)
        {
            return Task.CompletedTask;
        }

        entered.TrySetResult();
        return Task.Delay(Timeout.InfiniteTimeSpan, cancellationToken);
    }
}

internal sealed class WorkerLifetimeCancellation : IDisposable
{
    private readonly CancellationTokenSource source = new();
    private readonly ConsoleCancelEventHandler handler;

    public WorkerLifetimeCancellation()
    {
        handler = (_, args) =>
        {
            args.Cancel = true;
            source.Cancel();
        };
        Console.CancelKeyPress += handler;
    }

    public CancellationToken Token => source.Token;

    public void Dispose()
    {
        Console.CancelKeyPress -= handler;
        source.Dispose();
    }
}
