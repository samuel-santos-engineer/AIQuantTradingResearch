namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

public sealed class TwelveDataConfiguration
{
    public const string SectionName = "TwelveData";
    public const string ApiKeyName = "ApiKey";
    public const string RequestTimeoutSecondsName = "RequestTimeoutSeconds";
    public const int DefaultRequestTimeoutSeconds = 10;
    public const int MaximumRequestTimeoutSeconds = 30;

    public TwelveDataConfiguration(string apiKey, int requestTimeoutSeconds = DefaultRequestTimeoutSeconds)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        if (requestTimeoutSeconds <= 0 || requestTimeoutSeconds > MaximumRequestTimeoutSeconds)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requestTimeoutSeconds),
                requestTimeoutSeconds,
                $"{RequestTimeoutSecondsName} must be between 1 and {MaximumRequestTimeoutSeconds}.");
        }

        ApiKey = apiKey;
        RequestTimeout = TimeSpan.FromSeconds(requestTimeoutSeconds);
    }

    internal string ApiKey { get; }

    internal TimeSpan RequestTimeout { get; }
}
