using System.Net;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed record TwelveDataTransportResult(
    HttpStatusCode? StatusCode,
    TwelveDataTimeSeriesResponse? Response,
    TwelveDataErrorResponse? Error,
    bool IsPayloadUnreadable,
    HttpRequestException? TransportException);
