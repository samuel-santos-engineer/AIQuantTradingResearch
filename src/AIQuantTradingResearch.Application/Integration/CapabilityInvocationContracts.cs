namespace AIQuantTradingResearch.Application.Integration;

public sealed record CapabilityInvocationRequest(
    int ContractVersion,
    string Operation,
    string CorrelationId,
    IReadOnlyDictionary<string, string> Payload);

public enum CapabilityInvocationFailure
{
    None = 0,
    ConfigurationUnavailable = 1,
    InvalidRequest = 2,
    UnsupportedContractVersion = 3,
    MalformedResponse = 4,
    DependencyFailure = 5,
    Timeout = 6,
    Cancelled = 7
}

public sealed record CapabilityInvocationResult
{
    private CapabilityInvocationResult(
        bool isSuccess,
        IReadOnlyDictionary<string, string>? payload,
        CapabilityInvocationFailure failure,
        string? message)
    {
        IsSuccess = isSuccess;
        Payload = payload;
        Failure = failure;
        Message = message;
    }

    public bool IsSuccess { get; }

    public IReadOnlyDictionary<string, string>? Payload { get; }

    public CapabilityInvocationFailure Failure { get; }

    public string? Message { get; }

    public static CapabilityInvocationResult Success(IReadOnlyDictionary<string, string> payload) =>
        new(true, payload, CapabilityInvocationFailure.None, null);

    public static CapabilityInvocationResult Failed(
        CapabilityInvocationFailure failure,
        string message)
    {
        if (failure is CapabilityInvocationFailure.None)
        {
            throw new ArgumentOutOfRangeException(nameof(failure));
        }

        return new(false, null, failure, message);
    }
}

public interface ICapabilityInvoker
{
    Task<CapabilityInvocationResult> InvokeAsync(
        CapabilityInvocationRequest request,
        TimeSpan timeout,
        CancellationToken cancellationToken = default);
}
