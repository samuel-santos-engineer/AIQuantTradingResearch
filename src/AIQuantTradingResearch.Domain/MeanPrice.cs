namespace AIQuantTradingResearch.Domain;

public sealed record MeanPrice
{
    internal MeanPrice(decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(value),
                value,
                "The mean price must be greater than zero.");
        }

        Value = value;
    }

    public decimal Value { get; }
}
