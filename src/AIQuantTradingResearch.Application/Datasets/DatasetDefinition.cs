namespace AIQuantTradingResearch.Application.Datasets;

public sealed record DatasetDefinition
{
    public DatasetDefinition(string target, DateTimeOffset from, DateTimeOffset to)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(target);

        if (from >= to)
        {
            throw new ArgumentException(
                "Dataset selection requires an inclusive lower boundary before its exclusive upper boundary.",
                nameof(to));
        }

        Target = target;
        From = from;
        To = to;
        Ordering = DatasetOrdering.SemanticInstantAscending;
    }

    public string Target { get; }

    public DateTimeOffset From { get; }

    public DateTimeOffset To { get; }

    public DatasetOrdering Ordering { get; }
}

public enum DatasetOrdering
{
    SemanticInstantAscending,
}
