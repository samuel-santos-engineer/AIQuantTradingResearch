using System.Globalization;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

[Collection("Culture-sensitive tests")]
public sealed class TwelveDataTimeSeriesNormalizerTests
{
    [Fact]
    public void NormalizeUsesCloseExchangeOffsetsAndAscendingAbsoluteOrder()
    {
        var response = CreateResponse(
            "America/New_York",
            CreateValue("2026-07-15", "20.25", open: "999.00"),
            CreateValue("2026-01-15", "10.50", open: "888.00"));

        var result = TwelveDataTimeSeriesNormalizer.Normalize(response);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Failure);
        Assert.NotNull(result.Observations);
        Assert.Equal(2, result.Observations.Count);
        Assert.Equal(new DateTimeOffset(2026, 1, 15, 0, 0, 0, TimeSpan.FromHours(-5)), result.Observations[0].Instant);
        Assert.Equal(10.50m, result.Observations[0].Price);
        Assert.Equal(new DateTimeOffset(2026, 7, 15, 0, 0, 0, TimeSpan.FromHours(-4)), result.Observations[1].Instant);
        Assert.Equal(20.25m, result.Observations[1].Price);
    }

    [Fact]
    public void NormalizeUsesInvariantCultureWhenCurrentCultureUsesDecimalComma()
    {
        var originalCulture = CultureInfo.CurrentCulture;
        var originalUiCulture = CultureInfo.CurrentUICulture;

        try
        {
            CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("pt-BR");
            var response = CreateResponse(
                "America/New_York",
                CreateValue("2026-01-15", "1234.56"));

            var result = TwelveDataTimeSeriesNormalizer.Normalize(response);

            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Observations);
            Assert.Equal(1234.56m, Assert.Single(result.Observations).Price);
        }
        finally
        {
            CultureInfo.CurrentCulture = originalCulture;
            CultureInfo.CurrentUICulture = originalUiCulture;
        }
    }

    [Fact]
    public void NormalizeWhenValuesAreEmptyReturnsSuccessfulEmptyCollection()
    {
        var result = TwelveDataTimeSeriesNormalizer.Normalize(CreateResponse("America/New_York"));

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Observations);
        Assert.Empty(result.Observations);
    }

    [Fact]
    public void NormalizeWhenMetadataIsMissingReturnsExpectedFailure()
    {
        AssertFailure(
            new TwelveDataTimeSeriesResponse { Values = [] },
            TwelveDataNormalizationFailure.MissingMetadata);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void NormalizeWhenExchangeTimezoneIsMissingReturnsExpectedFailure(string? timezone)
    {
        AssertFailure(
            CreateResponse(timezone, CreateValue("2026-01-15", "10.00")),
            TwelveDataNormalizationFailure.MissingExchangeTimezone);
    }

    [Fact]
    public void NormalizeWhenExchangeTimezoneCannotBeResolvedReturnsExpectedFailure()
    {
        AssertFailure(
            CreateResponse("Synthetic/Unknown", CreateValue("2026-01-15", "10.00")),
            TwelveDataNormalizationFailure.UnresolvableExchangeTimezone);
    }

    [Fact]
    public void NormalizeWhenValuesAreMissingReturnsExpectedFailure()
    {
        AssertFailure(
            new TwelveDataTimeSeriesResponse
            {
                Metadata = new TwelveDataTimeSeriesMetadata
                {
                    ExchangeTimezone = "America/New_York",
                },
            },
            TwelveDataNormalizationFailure.MissingValues);
    }

    [Fact]
    public void NormalizeWhenValueIsNullReturnsExpectedFailure()
    {
        AssertFailure(
            CreateResponse(
                "America/New_York",
                new TwelveDataTimeSeriesValue[] { null! }),
            TwelveDataNormalizationFailure.MissingValue);
    }

    [Theory]
    [InlineData(null, (int)TwelveDataNormalizationFailure.MissingDateTime)]
    [InlineData("", (int)TwelveDataNormalizationFailure.MissingDateTime)]
    [InlineData("2026/01/15", (int)TwelveDataNormalizationFailure.InvalidDateTime)]
    [InlineData("2026-1-15", (int)TwelveDataNormalizationFailure.InvalidDateTime)]
    public void NormalizeWhenDateIsInvalidReturnsExpectedFailure(
        string? date,
        int expectedFailure)
    {
        AssertFailure(
            CreateResponse("America/New_York", CreateValue(date, "10.00")),
            (TwelveDataNormalizationFailure)expectedFailure);
    }

    [Fact]
    public void NormalizeWhenLocalMidnightIsInvalidReturnsExpectedFailure()
    {
        AssertFailure(
            CreateResponse("America/Sao_Paulo", CreateValue("2018-11-04", "10.00")),
            TwelveDataNormalizationFailure.InvalidLocalTime);
    }

    [Fact]
    public void NormalizeWhenLocalMidnightIsAmbiguousReturnsExpectedFailure()
    {
        AssertFailure(
            CreateResponse("America/Havana", CreateValue("2015-11-01", "10.00")),
            TwelveDataNormalizationFailure.AmbiguousLocalTime);
    }

    [Theory]
    [InlineData(null, (int)TwelveDataNormalizationFailure.MissingClose)]
    [InlineData("", (int)TwelveDataNormalizationFailure.MissingClose)]
    [InlineData("not-a-price", (int)TwelveDataNormalizationFailure.InvalidClose)]
    [InlineData("1,25", (int)TwelveDataNormalizationFailure.InvalidClose)]
    [InlineData("0", (int)TwelveDataNormalizationFailure.NonPositiveClose)]
    [InlineData("-1.25", (int)TwelveDataNormalizationFailure.NonPositiveClose)]
    public void NormalizeWhenCloseIsInvalidReturnsExpectedFailure(
        string? close,
        int expectedFailure)
    {
        AssertFailure(
            CreateResponse("America/New_York", CreateValue("2026-01-15", close)),
            (TwelveDataNormalizationFailure)expectedFailure);
    }

    [Fact]
    public void NormalizeWhenInstantsAreDuplicatedReturnsExpectedFailureWithoutDroppingRows()
    {
        var response = CreateResponse(
            "America/New_York",
            CreateValue("2026-01-15", "10.00"),
            CreateValue("2026-01-15", "20.00"));

        AssertFailure(response, TwelveDataNormalizationFailure.DuplicateInstant);
    }

    internal static TwelveDataTimeSeriesResponse CreateResponse(
        string? timezone,
        params TwelveDataTimeSeriesValue[] values) =>
        new()
        {
            Status = "ok",
            Metadata = new TwelveDataTimeSeriesMetadata
            {
                Symbol = "SYNTHETIC",
                Interval = "1day",
                ExchangeTimezone = timezone,
            },
            Values = values,
        };

    internal static TwelveDataTimeSeriesValue CreateValue(
        string? date,
        string? close,
        string? open = "1.00") =>
        new()
        {
            DateTime = date,
            Open = open,
            High = "2.00",
            Low = "0.50",
            Close = close,
            Volume = "100",
        };

    private static void AssertFailure(
        TwelveDataTimeSeriesResponse response,
        TwelveDataNormalizationFailure expectedFailure)
    {
        var result = TwelveDataTimeSeriesNormalizer.Normalize(response);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Observations);
        Assert.Equal(expectedFailure, result.Failure);
    }
}

[CollectionDefinition("Culture-sensitive tests", DisableParallelization = true)]
public sealed class CultureSensitiveTestsGroup;
