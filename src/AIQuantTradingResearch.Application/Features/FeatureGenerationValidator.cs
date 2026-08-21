using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Features;

public interface IFeatureGenerationValidator
{
    FeatureGenerationFailure? ValidateRequest(FeatureGenerationRequest? request);

    FeatureGenerationFailure? ValidateSnapshot(
        FeatureGenerationRequest request,
        DatasetSnapshotCandidate snapshot);
}

internal sealed class FeatureGenerationValidator : IFeatureGenerationValidator
{
    public FeatureGenerationFailure? ValidateRequest(FeatureGenerationRequest? request)
    {
        if (request is null)
        {
            return FeatureGenerationFailure.InvalidRequest;
        }

        if (request.Definition != FeatureDefinition.SimpleReturnLag1V1)
        {
            return FeatureGenerationFailure.UnsupportedDefinition;
        }

        return null;
    }

    public FeatureGenerationFailure? ValidateSnapshot(
        FeatureGenerationRequest request,
        DatasetSnapshotCandidate snapshot)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(snapshot);

        if (request.SnapshotIdentity != snapshot.SnapshotIdentity
            || request.SnapshotVersion != snapshot.Version)
        {
            return FeatureGenerationFailure.InvalidSnapshotEvidence;
        }

        for (var index = 1; index < snapshot.Observations.Count; index++)
        {
            var predecessor = snapshot.Observations[index - 1].Price;
            if (predecessor == 0m)
            {
                return FeatureGenerationFailure.InvalidNumericInput;
            }

            try
            {
                _ = snapshot.Observations[index].Price / predecessor;
            }
            catch (OverflowException)
            {
                return FeatureGenerationFailure.InvalidNumericInput;
            }
        }

        return null;
    }
}
