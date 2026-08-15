using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed class TwelveDataClient
{
    private const string DailyInterval = "1day";

    private readonly HttpClient httpClient;
    private readonly string apiKey;

    public TwelveDataClient(HttpClient httpClient, string apiKey)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);

        this.httpClient = httpClient;
        this.apiKey = apiKey;
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

        try
        {
            using var response = await httpClient.SendAsync(
                request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken);
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

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
