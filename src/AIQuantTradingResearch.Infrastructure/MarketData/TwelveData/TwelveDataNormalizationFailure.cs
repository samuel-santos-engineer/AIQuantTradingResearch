namespace AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;

internal enum TwelveDataNormalizationFailure
{
    MissingMetadata,
    MissingExchangeTimezone,
    UnresolvableExchangeTimezone,
    MissingValues,
    MissingValue,
    MissingDateTime,
    InvalidDateTime,
    InvalidLocalTime,
    AmbiguousLocalTime,
    MissingClose,
    InvalidClose,
    NonPositiveClose,
    DuplicateInstant,
    DomainInvariantViolation,
}
