using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed class TwelveDataClient
{
    private const string DailyInterval = "1day";

    private readonly HttpClient httpClient;
    private readonly string apiKey;
    private readonly TimeSpan requestTimeout;

    public TwelveDataClient(HttpClient httpClient, string apiKey, TimeSpan? requestTimeout = null)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        var effectiveRequestTimeout = requestTimeout ?? TimeSpan.FromSeconds(TwelveDataConfiguration.DefaultRequestTimeoutSeconds);
        if (effectiveRequestTimeout <= TimeSpan.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(requestTimeout));
        }

        this.httpClient = httpClient;
        this.apiKey = apiKey;
        this.requestTimeout = effectiveRequestTimeout;
    }

    public async Task<TwelveDataTransportResult> GetTimeSeriesAsync(
        string symbol,
        int outputSize,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(symbol);

        using var request = new HttpRequestMessage(
            HttpMethod.Get,
            BuildRequestUri(symbol, outputSize));
        request.Headers.Authorization = new AuthenticationHeaderValue("apikey", apiKey);

        using var deadline = new CancellationTokenSource(requestTimeout);
        using var linkedCancellation = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            deadline.Token);

        try
        {
            using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                linkedCancellation.Token);
            var content = await response.Content.ReadAsStringAsync(linkedCancellation.Token);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    var payload = JsonSerializer.Deserialize<TwelveDataTimeSeriesResponse>(content);

                    return new TwelveDataTransportResult(
                        response.StatusCode,
                        payload,
                        null,
                        payload is null,
                        null);
                }

                var error = JsonSerializer.Deserialize<TwelveDataErrorResponse>(content);

                return new TwelveDataTransportResult(
                    response.StatusCode,
                    null,
                    error,
                    error is null,
                    null);
            }
            catch (JsonException)
            {
                return new TwelveDataTransportResult(
                    response.StatusCode,
                    null,
                    null,
                    true,
                    null);
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (OperationCanceledException) when (deadline.IsCancellationRequested)
        {
            return new TwelveDataTransportResult(
                null,
                null,
                null,
                false,
                null,
                IsDeadlineExceeded: true);
        }
        catch (HttpRequestException exception)
        {
            return new TwelveDataTransportResult(
                null,
                null,
                null,
                false,
                exception);
        }
    }

    private static Uri BuildRequestUri(string symbol, int outputSize)
    {
        var encodedSymbol = Uri.EscapeDataString(symbol);
        var encodedOutputSize = outputSize.ToString(CultureInfo.InvariantCulture);

        return new Uri(
            $"/time_series?symbol={encodedSymbol}&interval={DailyInterval}&outputsize={encodedOutputSize}&adjust=splits",
            UriKind.Relative);
    }
}
