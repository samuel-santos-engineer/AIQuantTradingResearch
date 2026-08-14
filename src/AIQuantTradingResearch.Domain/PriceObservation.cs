namespace AIQuantTradingResearch.Domain;

public sealed record PriceObservation
{
    public PriceObservation(DateTimeOffset instant, decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(price),
                price,
                "The observed price must be greater than zero.");
        }

        Instant = instant;
        Price = price;
    }

    public DateTimeOffset Instant { get; }

    public decimal Price { get; }
}
