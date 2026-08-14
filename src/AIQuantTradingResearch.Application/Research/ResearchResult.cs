using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Research;

public sealed record ResearchResult
{
    internal ResearchResult(string target, int observationCount, MeanPrice meanPrice)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);

        if (observationCount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(observationCount),
                observationCount,
                "The observation count must be greater than zero.");
        }

        ArgumentNullException.ThrowIfNull(meanPrice);

        Target = target;
        ObservationCount = observationCount;
        MeanPrice = meanPrice;
    }

    public string Target { get; }

    public int ObservationCount { get; }

    public MeanPrice MeanPrice { get; }
}
