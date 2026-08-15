using System.Text.Json.Serialization;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed record TwelveDataTimeSeriesMetadata
{
    [JsonPropertyName("symbol")]
    public string? Symbol { get; init; }

    [JsonPropertyName("interval")]
    public string? Interval { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("exchange_timezone")]
    public string? ExchangeTimezone { get; init; }

    [JsonPropertyName("exchange")]
    public string? Exchange { get; init; }

    [JsonPropertyName("mic_code")]
    public string? MicCode { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }
}
