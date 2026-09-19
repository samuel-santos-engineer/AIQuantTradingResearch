using System.Globalization;
using System.Net;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.Vike;

internal sealed class VikeHistoricalMarketDataProvider : IHistoricalMarketDataProvider
{
    private const string ProviderName = "Vike";

    private readonly HttpClient httpClient;
    private readonly VikeConfiguration configuration;
    private readonly TimeProvider timeProvider;

    public VikeHistoricalMarketDataProvider(
        HttpClient httpClient,
        VikeConfiguration configuration,
        TimeProvider? timeProvider = null)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentNullException.ThrowIfNull(configuration);

        this.httpClient = httpClient;
        this.configuration = configuration;
        this.timeProvider = timeProvider ?? TimeProvider.System;
    }

    public async Task<HistoricalMarketDataResponse> GetHistoricalCandlesAsync(
        HistoricalMarketDataRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        using var deadline = new CancellationTokenSource(request.Deadline);
        using var linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            deadline.Token);

        try
        {
            using var message = CreateRequestMessage(request);
            using var response = await httpClient.SendAsync(
                message,
                HttpCompletionOption.ResponseHeadersRead,
                linkedCancellation.Token);
            var payload = await response.Content.ReadAsStringAsync(linkedCancellation.Token);

            if (!response.IsSuccessStatusCode)
            {
                return HistoricalMarketDataResponse.Failed(MapStatusCode(response.StatusCode));
            }

            return ParseResponse(request, payload, timeProvider.GetUtcNow());
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Cancelled);
        }
        catch (OperationCanceledException) when (deadline.IsCancellationRequested)
        {
            return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Timeout);
        }
        catch (HttpRequestException)
        {
            return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.Unavailable);
        }
    }

    private HttpRequestMessage CreateRequestMessage(HistoricalMarketDataRequest request)
    {
        var now = timeProvider.GetUtcNow();
        var start = now - GetRangeDuration(request.Range);
        var path = string.Create(
            CultureInfo.InvariantCulture,
            $"/api/ohlcv?symbol={GetVikeSymbol(request.Symbol)}&interval={GetVikeInterval(request.Interval)}&start={start:yyyy-MM-dd}&end={now:yyyy-MM-dd}&include_partial=false");

        var message = new HttpRequestMessage(HttpMethod.Get, new Uri(configuration.BaseAddress, path));
        message.Headers.TryAddWithoutValidation("X-API-Key", configuration.ApiKey);
        return message;
    }

    private static HistoricalMarketDataResponse ParseResponse(
        HistoricalMarketDataRequest request,
        string payload,
        DateTimeOffset acquiredAtUtc)
    {
        try
        {
            using var document = JsonDocument.Parse(payload);
            var root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Object ||
                !root.TryGetProperty("candles", out var candlesElement) ||
                candlesElement.ValueKind != JsonValueKind.Array ||
                root.TryGetProperty("next_cursor", out var cursor) &&
                cursor.ValueKind is not JsonValueKind.Null and not JsonValueKind.Undefined)
            {
                return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse);
            }

            var candles = new List<HistoricalCandle>();
            foreach (var item in candlesElement.EnumerateArray())
            {
                candles.Add(ParseCandle(request, item));
            }

            var provenance = new HistoricalMarketDataProvenance(
                ProviderName,
                acquiredAtUtc,
                acquiredAtUtc,
                isStale: false,
                containsOnlyClosedCandles: true);
            return HistoricalMarketDataResponse.Available(request, candles, provenance);
        }
        catch (JsonException)
        {
            return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse);
        }
        catch (ArgumentException)
        {
            return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse);
        }
        catch (OverflowException)
        {
            return HistoricalMarketDataResponse.Failed(HistoricalMarketDataFailure.MalformedResponse);
        }
    }

    private static HistoricalCandle ParseCandle(
        HistoricalMarketDataRequest request,
        JsonElement item)
    {
        if (item.ValueKind != JsonValueKind.Array || item.GetArrayLength() != 6)
        {
            throw new ArgumentException("A Vike candle must contain exactly six values.");
        }

        var values = item.EnumerateArray().ToArray();
        var openTimeUtc = DateTimeOffset.FromUnixTimeMilliseconds(ReadInt64(values[0]));
        return new HistoricalCandle(
            request.Symbol,
            request.Interval,
            openTimeUtc,
            ReadDecimal(values[1]),
            ReadDecimal(values[2]),
            ReadDecimal(values[3]),
            ReadDecimal(values[4]),
            ReadDecimal(values[5]));
    }

    private static decimal ReadDecimal(JsonElement value) =>
        value.ValueKind switch
        {
            JsonValueKind.Number when value.TryGetDecimal(out var number) => number,
            JsonValueKind.String when decimal.TryParse(
                value.GetString(),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out var text) => text,
            _ => throw new ArgumentException("Candle value is not a decimal."),
        };

    private static long ReadInt64(JsonElement value) =>
        value.ValueKind switch
        {
            JsonValueKind.Number when value.TryGetInt64(out var number) => number,
            JsonValueKind.String when long.TryParse(
                value.GetString(),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var text) => text,
            _ => throw new ArgumentException("Candle timestamp is not an epoch-millisecond integer."),
        };

    private static HistoricalMarketDataFailure MapStatusCode(HttpStatusCode statusCode) =>
        statusCode switch
        {
            HttpStatusCode.TooManyRequests => HistoricalMarketDataFailure.RateLimited,
            HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden =>
                HistoricalMarketDataFailure.AuthenticationOrConfiguration,
            HttpStatusCode.BadRequest => HistoricalMarketDataFailure.MalformedResponse,
            _ => HistoricalMarketDataFailure.Unavailable,
        };

    private static string GetVikeSymbol(CanonicalMarketSymbol symbol) =>
        symbol switch
        {
            CanonicalMarketSymbol.BtcUsd => "BTC",
            CanonicalMarketSymbol.EthUsd => "ETH",
            _ => throw new ArgumentOutOfRangeException(nameof(symbol), symbol, "Unsupported canonical market symbol."),
        };

    private static string GetVikeInterval(HistoricalCandleInterval interval) =>
        interval switch
        {
            HistoricalCandleInterval.OneHour => "1h",
            HistoricalCandleInterval.FourHours => "4h",
            HistoricalCandleInterval.OneDay => "1d",
            _ => throw new ArgumentOutOfRangeException(nameof(interval), interval, "Unsupported canonical interval."),
        };

    private static TimeSpan GetRangeDuration(HistoricalCandleRange range) =>
        range switch
        {
            HistoricalCandleRange.OneDay => TimeSpan.FromDays(1),
            HistoricalCandleRange.SevenDays => TimeSpan.FromDays(7),
            HistoricalCandleRange.ThirtyDays => TimeSpan.FromDays(30),
            HistoricalCandleRange.NinetyDays => TimeSpan.FromDays(90),
            _ => throw new ArgumentOutOfRangeException(nameof(range), range, "Unsupported canonical range."),
        };
}
