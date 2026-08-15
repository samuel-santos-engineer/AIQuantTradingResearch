using System.Net;
using System.Text;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class TwelveDataObservationSourceTests
{
    private const string PlaceholderApiKey = "wp13-placeholder-key";

    [Fact]
    public void GetObservationsWhenPayloadIsEligibleReturnsAllCanonicalObservations()
    {
        using var handler = RespondWith(
            HttpStatusCode.OK,
            SuccessPayload(
                ("2026-01-16", "20.00"),
                ("2026-01-15", "10.00")));
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        Assert.True(result.IsSuccess);
        Assert.Null(result.Failure);
        Assert.NotNull(result.Observations);
        Assert.Equal(2, result.Observations.Count);
        Assert.Equal(10.00m, result.Observations[0].Price);
        Assert.Equal(20.00m, result.Observations[1].Price);
        Assert.Equal(1, handler.CallCount);
    }

    [Theory]
    [InlineData(HttpStatusCode.Unauthorized, ObservationSourceFailure.AccessDenied)]
    [InlineData(HttpStatusCode.Forbidden, ObservationSourceFailure.AccessDenied)]
    [InlineData(HttpStatusCode.NotFound, ObservationSourceFailure.UnsupportedTarget)]
    [InlineData(HttpStatusCode.TooManyRequests, ObservationSourceFailure.UsageLimitReached)]
    [InlineData(HttpStatusCode.InternalServerError, ObservationSourceFailure.SourceUnavailable)]
    [InlineData(HttpStatusCode.ServiceUnavailable, ObservationSourceFailure.SourceUnavailable)]
    public void GetObservationsWhenHttpStatusIsAuthoritativeReturnsExpectedFailure(
        HttpStatusCode statusCode,
        ObservationSourceFailure expectedFailure)
    {
        using var handler = RespondWith(statusCode, "{not-json");
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        AssertFailure(result, expectedFailure);
        Assert.Equal(1, handler.CallCount);
    }

    [Theory]
    [InlineData(401, ObservationSourceFailure.AccessDenied)]
    [InlineData(403, ObservationSourceFailure.AccessDenied)]
    [InlineData(404, ObservationSourceFailure.UnsupportedTarget)]
    [InlineData(429, ObservationSourceFailure.UsageLimitReached)]
    [InlineData(500, ObservationSourceFailure.SourceUnavailable)]
    [InlineData(599, ObservationSourceFailure.SourceUnavailable)]
    [InlineData(418, ObservationSourceFailure.InvalidSourceResponse)]
    public void GetObservationsWhenProviderCodeIsPresentReturnsExpectedFailure(
        int providerCode,
        ObservationSourceFailure expectedFailure)
    {
        var payload = $$"""
            { "status": "error", "code": {{providerCode}}, "message": "Synthetic provider error." }
            """;
        using var handler = RespondWith(HttpStatusCode.BadRequest, payload);
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        AssertFailure(result, expectedFailure);
    }

    [Fact]
    public void GetObservationsWhenTransportFailsReturnsSourceUnavailable()
    {
        using var handler = new TwelveDataTestHttpMessageHandler(
            static (_, _) => throw new HttpRequestException("Synthetic transport failure."));
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        AssertFailure(result, ObservationSourceFailure.SourceUnavailable);
    }

    [Theory]
    [InlineData("{not-json")]
    [InlineData("null")]
    [InlineData("{ \"status\": \"error\", \"meta\": {}, \"values\": [] }")]
    public void GetObservationsWhenSuccessfulPayloadIsInvalidReturnsInvalidSourceResponse(string payload)
    {
        using var handler = RespondWith(HttpStatusCode.OK, payload);
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        AssertFailure(result, ObservationSourceFailure.InvalidSourceResponse);
    }

    [Fact]
    public void GetObservationsWhenNormalizationFailsReturnsInvalidSourceResponse()
    {
        using var handler = RespondWith(
            HttpStatusCode.OK,
            SuccessPayload(("2026-01-15", "not-a-price")));
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        AssertFailure(result, ObservationSourceFailure.InvalidSourceResponse);
    }

    [Fact]
    public void GetObservationsWhenNormalizedCountIsBelowRequestedReturnsInsufficientObservations()
    {
        using var handler = RespondWith(
            HttpStatusCode.OK,
            SuccessPayload(("2026-01-15", "10.00")));
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 2));

        AssertFailure(result, ObservationSourceFailure.InsufficientObservations);
    }

    [Fact]
    public void GetObservationsWhenNormalizedCollectionIsEmptyReturnsInsufficientObservations()
    {
        using var handler = RespondWith(HttpStatusCode.OK, SuccessPayload());
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest("SYNTHETIC", 1));

        AssertFailure(result, ObservationSourceFailure.InsufficientObservations);
    }

    [Theory]
    [InlineData("", 1, ObservationSourceFailure.UnsupportedTarget)]
    [InlineData("   ", 1, ObservationSourceFailure.UnsupportedTarget)]
    [InlineData("SYNTHETIC", 0, ObservationSourceFailure.InsufficientObservations)]
    [InlineData("SYNTHETIC", -1, ObservationSourceFailure.InsufficientObservations)]
    public void GetObservationsWhenDirectRequestIsUnsupportedDoesNotCallProvider(
        string target,
        int count,
        ObservationSourceFailure expectedFailure)
    {
        using var handler = RespondWith(HttpStatusCode.OK, SuccessPayload());
        var source = CreateSource(handler);

        var result = source.GetObservations(new ResearchRequest(target, count));

        AssertFailure(result, expectedFailure);
        Assert.Equal(0, handler.CallCount);
    }

    private static TwelveDataObservationSource CreateSource(HttpMessageHandler handler)
    {
        var httpClient = TwelveDataClientTests.CreateHttpClient(handler);
        var client = new TwelveDataClient(httpClient, PlaceholderApiKey);
        return new TwelveDataObservationSource(client);
    }

    private static TwelveDataTestHttpMessageHandler RespondWith(
        HttpStatusCode statusCode,
        string payload) =>
        TwelveDataClientTests.RespondWith(statusCode, payload);

    private static string SuccessPayload(params (string Date, string Close)[] values)
    {
        var rows = string.Join(
            ",",
            values.Select(
                static value => $$"""
                    {
                      "datetime": "{{value.Date}}",
                      "open": "1.00",
                      "high": "2.00",
                      "low": "0.50",
                      "close": "{{value.Close}}",
                      "volume": "100"
                    }
                    """));

        return $$"""
            {
              "status": "ok",
              "meta": {
                "symbol": "SYNTHETIC",
                "interval": "1day",
                "exchange_timezone": "America/New_York"
              },
              "values": [{{rows}}]
            }
            """;
    }

    private static void AssertFailure(
        ObservationSourceResult result,
        ObservationSourceFailure expectedFailure)
    {
        Assert.False(result.IsSuccess);
        Assert.Null(result.Observations);
        Assert.Equal(expectedFailure, result.Failure);
    }
}
