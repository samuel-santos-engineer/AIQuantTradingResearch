using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

internal sealed class ExperimentGenerationUseCase : IExperimentGenerationUseCase
{
    private readonly IFeatureGenerationUseCase featureGenerationUseCase;
    private readonly IExperimentGenerationValidator validator;
    private readonly IExperimentSummaryComputer summaryComputer;

    public ExperimentGenerationUseCase(
        IFeatureGenerationUseCase featureGenerationUseCase,
        IExperimentGenerationValidator validator,
        IExperimentSummaryComputer summaryComputer)
    {
        ArgumentNullException.ThrowIfNull(featureGenerationUseCase);
        ArgumentNullException.ThrowIfNull(validator);
        ArgumentNullException.ThrowIfNull(summaryComputer);

        this.featureGenerationUseCase = featureGenerationUseCase;
        this.validator = validator;
        this.summaryComputer = summaryComputer;
    }

    public ExperimentGenerationResult Execute(ExperimentGenerationRequest request)
    {
        var requestFailure = validator.ValidateRequest(request);
        if (requestFailure is not null)
        {
            return ExperimentGenerationResult.Failed(requestFailure.Value);
        }

        var featureResult = featureGenerationUseCase.Execute(new FeatureGenerationRequest(
            FeatureDefinition.SimpleReturnLag1V1,
            request.SnapshotIdentity,
            request.SnapshotVersion));
        if (!featureResult.IsSuccess)
        {
            return ExperimentGenerationResult.Failed(MapFeatureFailure(featureResult.Failure));
        }

        var featureSet = featureResult.FeatureSet
            ?? throw new InvalidOperationException("A successful feature-generation result contained no Feature Set.");
        var featureSetFailure = validator.ValidateFeatureSet(featureSet);
        if (featureSetFailure is not null)
        {
            return ExperimentGenerationResult.Failed(featureSetFailure.Value);
        }

        ExperimentSummaryEvidence summary;
        try
        {
            summary = summaryComputer.Compute(featureSet);
        }
        catch (OverflowException)
        {
            return ExperimentGenerationResult.Failed(ExperimentGenerationFailure.InvalidNumericEvidence);
        }

        var definitionIdentity = ExperimentIdentityComputer.ComputeDefinitionIdentity(request.Definition);
        var resultIdentity = ExperimentIdentityComputer.ComputeResultIdentity(
            definitionIdentity,
            featureSet.Identity,
            summary);
        return ExperimentGenerationResult.Generated(new ExperimentResult(
            request.Definition,
            definitionIdentity,
            resultIdentity,
            featureSet,
            summary,
            new ExperimentProvenance(definitionIdentity, featureSet),
            new ExperimentLineage(definitionIdentity, featureSet)));
    }

    private static ExperimentGenerationFailure MapFeatureFailure(FeatureGenerationFailure? failure) => failure switch
    {
        FeatureGenerationFailure.InvalidRequest => ExperimentGenerationFailure.InvalidRequest,
        FeatureGenerationFailure.UnsupportedDefinition => ExperimentGenerationFailure.InvalidFeatureEvidence,
        FeatureGenerationFailure.SnapshotNotFound => ExperimentGenerationFailure.FeatureSetNotFound,
        FeatureGenerationFailure.DependencyUnavailable => ExperimentGenerationFailure.DependencyUnavailable,
        FeatureGenerationFailure.InvalidSnapshotEvidence => ExperimentGenerationFailure.InvalidFeatureEvidence,
        FeatureGenerationFailure.InvalidNumericInput => ExperimentGenerationFailure.InvalidNumericEvidence,
        FeatureGenerationFailure.IntegrityConflict => ExperimentGenerationFailure.IntegrityConflict,
        _ => throw new InvalidOperationException("The feature-generation use case returned an unknown failure."),
    };
}
