using System.Text.Json.Serialization;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed record TwelveDataTimeSeriesResponse
{
    [JsonPropertyName("meta")]
    public TwelveDataTimeSeriesMetadata? Metadata { get; init; }

    [JsonPropertyName("values")]
    public IReadOnlyList<TwelveDataTimeSeriesValue>? Values { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
