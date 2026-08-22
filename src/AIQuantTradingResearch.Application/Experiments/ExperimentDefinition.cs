namespace AIQuantTradingResearch.Application.Experiments;

public sealed record ExperimentDefinition
{
    public const string SimpleReturnDescriptiveSummaryV1Name =
        "simple-return-descriptive-summary-v1";

    private ExperimentDefinition()
    {
        Name = SimpleReturnDescriptiveSummaryV1Name;
    }

    public static ExperimentDefinition SimpleReturnDescriptiveSummaryV1 { get; } = new();

    public string Name { get; }
}
