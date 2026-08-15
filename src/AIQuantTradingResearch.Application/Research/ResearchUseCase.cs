using AIQuantTradingResearch.Domain;

namespace AIQuantTradingResearch.Application.Research;

internal sealed class ResearchUseCase : IResearchUseCase
{
    private readonly IObservationSource observationSource;

    public ResearchUseCase(IObservationSource observationSource)
    {
        ArgumentNullException.ThrowIfNull(observationSource);
        this.observationSource = observationSource;
    }

    public ResearchOutcome Execute(ResearchRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Target) || request.RequestedObservationCount <= 0)
        {
            return ResearchOutcome.Failed(ResearchFailure.InvalidRequest);
        }

        var sourceResult = observationSource.GetObservations(request)
            ?? throw new InvalidOperationException("The observation source returned no result.");

        if (!sourceResult.IsSuccess)
        {
            return sourceResult.Failure switch
            {
                ObservationSourceFailure.UnsupportedTarget =>
                    ResearchOutcome.Failed(ResearchFailure.UnsupportedTarget),
                ObservationSourceFailure.InsufficientObservations =>
                    ResearchOutcome.Failed(ResearchFailure.InsufficientObservations),
                ObservationSourceFailure.SourceUnavailable =>
                    ResearchOutcome.Failed(ResearchFailure.SourceUnavailable),
                ObservationSourceFailure.AccessDenied =>
                    ResearchOutcome.Failed(ResearchFailure.AccessDenied),
                ObservationSourceFailure.UsageLimitReached =>
                    ResearchOutcome.Failed(ResearchFailure.UsageLimitReached),
                ObservationSourceFailure.InvalidSourceResponse =>
                    ResearchOutcome.Failed(ResearchFailure.InvalidSourceResponse),
                _ => throw new InvalidOperationException("The observation source returned an unknown failure."),
            };
        }

        var observations = sourceResult.Observations
            ?? throw new InvalidOperationException("A successful observation-source result contained no observations.");

        if (observations.Count != request.RequestedObservationCount)
        {
            return ResearchOutcome.Failed(ResearchFailure.InsufficientObservations);
        }

        var series = new ObservationSeries(observations);
        var meanPrice = series.CalculateMeanPrice();
        var result = new ResearchResult(request.Target, observations.Count, meanPrice);

        return ResearchOutcome.Succeeded(result);
    }
}
