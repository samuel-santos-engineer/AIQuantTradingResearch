using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

internal sealed class ExperimentGenerationValidator : IExperimentGenerationValidator
{
    public ExperimentGenerationFailure? ValidateRequest(ExperimentGenerationRequest request)
    {
        if (request is null)
        {
            return ExperimentGenerationFailure.InvalidRequest;
        }

        if (request.Definition != ExperimentDefinition.SimpleReturnDescriptiveSummaryV1)
        {
            return ExperimentGenerationFailure.UnsupportedDefinition;
        }

        return null;
    }

    public ExperimentGenerationFailure? ValidateFeatureSet(FeatureSet featureSet)
    {
        if (featureSet is null)
        {
            return ExperimentGenerationFailure.InvalidFeatureEvidence;
        }

        var expectedDefinitionIdentity = FeatureIdentityComputer.ComputeDefinitionIdentity();
        if (featureSet.DefinitionIdentity != expectedDefinitionIdentity
            || featureSet.Provenance.DefinitionIdentity != expectedDefinitionIdentity
            || featureSet.Lineage.DefinitionIdentity != expectedDefinitionIdentity
            || featureSet.Provenance.SnapshotVersion.SnapshotIdentity != featureSet.SnapshotIdentity
            || featureSet.Provenance.DatasetProvenance.SnapshotIdentity != featureSet.SnapshotIdentity
            || featureSet.Provenance.DatasetProvenance.Version != featureSet.SnapshotVersion
            || featureSet.Provenance.DatasetProvenance.DefinitionIdentity
                != featureSet.Lineage.DatasetLineage.DefinitionIdentity
            || featureSet.Provenance.DatasetProvenance.SourceStateIdentity
                != featureSet.Lineage.DatasetLineage.SourceStateIdentity)
        {
            return ExperimentGenerationFailure.InvalidFeatureEvidence;
        }

        if (HasInvalidNumericEvidence(featureSet.Values))
        {
            return ExperimentGenerationFailure.InvalidNumericEvidence;
        }

        var expectedSetIdentity = FeatureIdentityComputer.ComputeSetIdentity(
            expectedDefinitionIdentity,
            featureSet.SnapshotIdentity,
            featureSet.SnapshotVersion,
            featureSet.Values);

        return featureSet.Identity == expectedSetIdentity
            ? null
            : ExperimentGenerationFailure.IntegrityConflict;
    }

    private static bool HasInvalidNumericEvidence(IReadOnlyList<FeatureValue> values)
    {
        if (values.Count == 0)
        {
            return false;
        }

        try
        {
            var sum = values[0].Value;
            for (var index = 1; index < values.Count; index++)
            {
                sum += values[index].Value;
            }

            _ = sum / values.Count;
            return false;
        }
        catch (OverflowException)
        {
            return true;
        }
    }
}
