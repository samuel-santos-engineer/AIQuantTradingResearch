using System.Net;
using AIQuantTradingResearch.Application.Research;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed class TwelveDataObservationSource : IObservationSource
{
    private readonly TwelveDataClient client;

    public TwelveDataObservationSource(TwelveDataClient client)
    {
        ArgumentNullException.ThrowIfNull(client);
        this.client = client;
    }

    public ObservationSourceResult GetObservations(ResearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Target))
        {
            return Failed(ObservationSourceFailure.UnsupportedTarget);
        }

        if (request.RequestedObservationCount <= 0)
        {
            return Failed(ObservationSourceFailure.InsufficientObservations);
        }

        var transportResult = client.GetTimeSeriesAsync(
                request.Target,
                request.RequestedObservationCount)
            .GetAwaiter()
            .GetResult();

        if (transportResult.TransportException is not null)
        {
            return Failed(ObservationSourceFailure.SourceUnavailable);
        }

        var statusFailure = ClassifyAuthoritativeStatus(transportResult.StatusCode);
        if (statusFailure is not null)
        {
            return Failed(statusFailure.Value);
        }

        if (transportResult.IsPayloadUnreadable)
        {
            return Failed(ObservationSourceFailure.InvalidSourceResponse);
        }

        if (transportResult.StatusCode is null)
        {
            return Failed(ObservationSourceFailure.InvalidSourceResponse);
        }

        if (!IsSuccessStatusCode(transportResult.StatusCode.Value))
        {
            return Failed(ClassifyProviderError(transportResult.Error));
        }

        if (transportResult.Error is not null ||
            transportResult.Response is null ||
            !string.Equals(transportResult.Response.Status, "ok", StringComparison.Ordinal))
        {
            return Failed(ObservationSourceFailure.InvalidSourceResponse);
        }

        var normalizationResult = TwelveDataTimeSeriesNormalizer.Normalize(
            transportResult.Response);

        if (!normalizationResult.IsSuccess || normalizationResult.Observations is null)
        {
            return Failed(ObservationSourceFailure.InvalidSourceResponse);
        }

        if (normalizationResult.Observations.Count < request.RequestedObservationCount)
        {
            return Failed(ObservationSourceFailure.InsufficientObservations);
        }

        return ObservationSourceResult.ObservationsAvailable(
            normalizationResult.Observations);
    }

    private static ObservationSourceFailure? ClassifyAuthoritativeStatus(
        HttpStatusCode? statusCode) =>
        statusCode switch
        {
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden =>
                ObservationSourceFailure.AccessDenied,
            HttpStatusCode.TooManyRequests =>
                ObservationSourceFailure.UsageLimitReached,
            HttpStatusCode.NotFound =>
                ObservationSourceFailure.UnsupportedTarget,
            not null when (int)statusCode.Value >= 500 =>
                ObservationSourceFailure.SourceUnavailable,
            _ => null,
        };

    private static ObservationSourceFailure ClassifyProviderError(
        TwelveDataErrorResponse? error) =>
        error?.Code switch
        {
            401 or 403 => ObservationSourceFailure.AccessDenied,
            404 => ObservationSourceFailure.UnsupportedTarget,
            429 => ObservationSourceFailure.UsageLimitReached,
            >= 500 and <= 599 => ObservationSourceFailure.SourceUnavailable,
            _ => ObservationSourceFailure.InvalidSourceResponse,
        };

    private static bool IsSuccessStatusCode(HttpStatusCode statusCode) =>
        (int)statusCode is >= 200 and <= 299;

    private static ObservationSourceResult Failed(ObservationSourceFailure failure) =>
        ObservationSourceResult.Failed(failure);
}
