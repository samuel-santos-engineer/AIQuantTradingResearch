namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

public sealed class TwelveDataConfiguration
{
    public const string SectionName = "TwelveData";
    public const string ApiKeyName = "ApiKey";

    public TwelveDataConfiguration(string apiKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        ApiKey = apiKey;
    }

    internal string ApiKey { get; }
}
