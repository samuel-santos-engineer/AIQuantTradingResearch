using System.Globalization;
using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal sealed class TwelveDataTimeSeriesNormalizer
{
    private const string DailyDateFormat = "yyyy-MM-dd";
    private const NumberStyles PriceNumberStyles =
        NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint;

    public static TwelveDataNormalizationResult Normalize(TwelveDataTimeSeriesResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (response.Metadata is null)
        {
            return Failed(TwelveDataNormalizationFailure.MissingMetadata);
        }

        if (string.IsNullOrWhiteSpace(response.Metadata.ExchangeTimezone))
        {
            return Failed(TwelveDataNormalizationFailure.MissingExchangeTimezone);
        }

        TimeZoneInfo exchangeTimeZone;
        try
        {
            exchangeTimeZone = TimeZoneInfo.FindSystemTimeZoneById(
                response.Metadata.ExchangeTimezone);
        }
        catch (TimeZoneNotFoundException)
        {
            return Failed(TwelveDataNormalizationFailure.UnresolvableExchangeTimezone);
        }
        catch (InvalidTimeZoneException)
        {
            return Failed(TwelveDataNormalizationFailure.UnresolvableExchangeTimezone);
        }

        if (response.Values is null)
        {
            return Failed(TwelveDataNormalizationFailure.MissingValues);
        }

        var observations = new List<PriceObservation>(response.Values.Count);

        foreach (var value in response.Values)
        {
            if (value is null)
            {
                return Failed(TwelveDataNormalizationFailure.MissingValue);
            }

            if (string.IsNullOrWhiteSpace(value.DateTime))
            {
                return Failed(TwelveDataNormalizationFailure.MissingDateTime);
            }

            if (!System.DateTime.TryParseExact(
                    value.DateTime,
                    DailyDateFormat,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate))
            {
                return Failed(TwelveDataNormalizationFailure.InvalidDateTime);
            }

            var localMidnight = System.DateTime.SpecifyKind(
                parsedDate,
                DateTimeKind.Unspecified);

            if (exchangeTimeZone.IsInvalidTime(localMidnight))
            {
                return Failed(TwelveDataNormalizationFailure.InvalidLocalTime);
            }

            if (exchangeTimeZone.IsAmbiguousTime(localMidnight))
            {
                return Failed(TwelveDataNormalizationFailure.AmbiguousLocalTime);
            }

            if (string.IsNullOrWhiteSpace(value.Close))
            {
                return Failed(TwelveDataNormalizationFailure.MissingClose);
            }

            if (!decimal.TryParse(
                    value.Close,
                    PriceNumberStyles,
                    CultureInfo.InvariantCulture,
                    out var close))
            {
                return Failed(TwelveDataNormalizationFailure.InvalidClose);
            }

            if (close <= 0)
            {
                return Failed(TwelveDataNormalizationFailure.NonPositiveClose);
            }

            var instant = new DateTimeOffset(
                localMidnight,
                exchangeTimeZone.GetUtcOffset(localMidnight));

            try
            {
                observations.Add(new PriceObservation(instant, close));
            }
            catch (ArgumentException)
            {
                return Failed(TwelveDataNormalizationFailure.DomainInvariantViolation);
            }
        }

        var orderedObservations = observations
            .OrderBy(static observation => observation.Instant)
            .ToArray();

        for (var index = 1; index < orderedObservations.Length; index++)
        {
            if (orderedObservations[index - 1].Instant == orderedObservations[index].Instant)
            {
                return Failed(TwelveDataNormalizationFailure.DuplicateInstant);
            }
        }

        return TwelveDataNormalizationResult.Succeeded(orderedObservations);
    }

    private static TwelveDataNormalizationResult Failed(
        TwelveDataNormalizationFailure failure) =>
        TwelveDataNormalizationResult.Failed(failure);
}
