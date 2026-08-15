using System.Text.Json.Serialization;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed record TwelveDataErrorResponse
{
    [JsonPropertyName("code")]
    public int? Code { get; init; }

    [JsonPropertyName("message")]
    public string? Message { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}
