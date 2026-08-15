using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed record TwelveDataNormalizationResult
{
    private TwelveDataNormalizationResult(
        IReadOnlyList<PriceObservation>? observations,
        TwelveDataNormalizationFailure? failure)
    {
        Observations = observations;
        Failure = failure;
    }

    public bool IsSuccess => Observations is not null;

    public IReadOnlyList<PriceObservation>? Observations { get; }

    public TwelveDataNormalizationFailure? Failure { get; }

    public static TwelveDataNormalizationResult Succeeded(
        IEnumerable<PriceObservation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        return new TwelveDataNormalizationResult(
            Array.AsReadOnly(observations.ToArray()),
            null);
    }

    public static TwelveDataNormalizationResult Failed(
        TwelveDataNormalizationFailure failure) =>
        new(null, failure);
}
