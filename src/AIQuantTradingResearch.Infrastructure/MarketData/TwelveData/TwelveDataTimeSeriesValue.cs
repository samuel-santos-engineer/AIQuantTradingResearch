using System.Text.Json.Serialization;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed record TwelveDataTimeSeriesValue
{
    [JsonPropertyName("datetime")]
    public string? DateTime { get; init; }

    [JsonPropertyName("open")]
    public string? Open { get; init; }

    [JsonPropertyName("high")]
    public string? High { get; init; }

    [JsonPropertyName("low")]
    public string? Low { get; init; }

    [JsonPropertyName("close")]
    public string? Close { get; init; }

    [JsonPropertyName("volume")]
    public string? Volume { get; init; }
}
