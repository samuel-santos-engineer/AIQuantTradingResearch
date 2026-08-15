namespace AIQuantTradingResearch.Application.Research;

public enum ObservationSourceFailure
{
    UnsupportedTarget,
    InsufficientObservations,
    SourceUnavailable,
    AccessDenied,
    UsageLimitReached,
    InvalidSourceResponse,
}
