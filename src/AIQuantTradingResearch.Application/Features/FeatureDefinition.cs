namespace AIQuantTradingResearch.Application.Features;

public sealed record FeatureDefinition
{
    public const string SimpleReturnLag1V1Name = "simple-return-lag-1-v1";

    private FeatureDefinition()
    {
        Name = SimpleReturnLag1V1Name;
    }

    public static FeatureDefinition SimpleReturnLag1V1 { get; } = new();

    public string Name { get; }
}
