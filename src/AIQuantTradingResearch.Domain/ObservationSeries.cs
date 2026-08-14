using System.Collections.ObjectModel;

namespace AIQuantTradingResearch.Domain;

public sealed class ObservationSeries
{
    private readonly ReadOnlyCollection<PriceObservation> observations;

    public ObservationSeries(IEnumerable<PriceObservation> observations)
    {
        ArgumentNullException.ThrowIfNull(observations);

        var snapshot = observations.ToArray();

        if (snapshot.Length == 0)
        {
            throw new ArgumentException(
                "An observation series must contain at least one observation.",
                nameof(observations));
        }

        if (snapshot.Any(static observation => observation is null))
        {
            throw new ArgumentException(
                "An observation series cannot contain a null observation.",
                nameof(observations));
        }

        for (var index = 1; index < snapshot.Length; index++)
        {
            if (snapshot[index].Instant <= snapshot[index - 1].Instant)
            {
                throw new ArgumentException(
                    "Observation instants must be unique and strictly increasing.",
                    nameof(observations));
            }
        }

        this.observations = Array.AsReadOnly(snapshot);
    }

    public IReadOnlyList<PriceObservation> Observations => observations;

    public MeanPrice CalculateMeanPrice()
    {
        var total = 0m;

        foreach (var observation in observations)
        {
            total += observation.Price;
        }

        return new MeanPrice(total / observations.Count);
    }
}
