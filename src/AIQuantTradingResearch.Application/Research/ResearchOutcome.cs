namespace AIQuantTradingResearch.Application.Research;

public sealed record ResearchOutcome
{
    private ResearchOutcome(ResearchResult? result, ResearchFailure? failure)
    {
        Result = result;
        Failure = failure;
    }

    public bool IsSuccess => Result is not null;

    public ResearchResult? Result { get; }

    public ResearchFailure? Failure { get; }

    internal static ResearchOutcome Succeeded(ResearchResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        return new ResearchOutcome(result, null);
    }

    internal static ResearchOutcome Failed(ResearchFailure failure)
    {
        if (!Enum.IsDefined(failure))
        {
            throw new ArgumentOutOfRangeException(nameof(failure), failure, "Unknown research failure.");
        }

        return new ResearchOutcome(null, failure);
    }
}
