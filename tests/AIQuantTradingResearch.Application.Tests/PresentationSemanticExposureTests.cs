using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Pipelines;
using Xunit;

namespace AIQuantTradingResearch.Application.Tests;

public sealed class PresentationSemanticExposureTests
{
    [Theory]
    [InlineData(PipelineSuccessDisposition.NewlyAccepted, PresentationIdempotencyStatus.NewlyPersisted)]
    [InlineData(PipelineSuccessDisposition.EquivalentExisting, PresentationIdempotencyStatus.EquivalentExisting)]
    public void SuccessfulPipelinePersistenceMapsToTheCanonicalPresentationStatus(PipelineSuccessDisposition disposition, PresentationIdempotencyStatus expected)
    {
        var result = PipelineExecutionResult.Succeeded(SuccessProvenance(), disposition);
        Assert.Equal(expected, result.PresentationIdempotencyStatus);
        Assert.Equal(PresentationDataQualityStatus.Valid, result.PresentationDataQualityStatus);
    }

    [Fact]
    public void ValidationFailureIsInvalidWhileAnUnrelatedFailureIsUnavailable()
    {
        Assert.Equal(PresentationDataQualityStatus.Invalid, Failure(PipelineFailureCategory.InvalidInput).PresentationDataQualityStatus);
        Assert.Equal(PresentationDataQualityStatus.Unavailable, Failure(PipelineFailureCategory.DependencyUnavailable).PresentationDataQualityStatus);
        Assert.Equal(PresentationIdempotencyStatus.Unavailable, Failure(PipelineFailureCategory.InvalidInput).PresentationIdempotencyStatus);
    }

    private static PipelineExecutionResult Failure(PipelineFailureCategory category) => PipelineExecutionResult.Failed(
        new PipelineProvenance(new PipelineDefinitionIdentity(new string('a', 64)), new PipelineExecutionIdentity(new string('b', 64)), new DatasetDefinitionIdentity(new string('c', 64)), null,
            [new PipelineStageEvidence(ResearchPipelineStage.HistoricalObservationRetrieval, PipelineStageOutcome.Failed, failureCategory: category)]),
        ResearchPipelineStage.HistoricalObservationRetrieval, category);

    private static PipelineProvenance SuccessProvenance()
    {
        var snapshot = new DatasetSnapshotIdentity(new string('d', 64));
        return new PipelineProvenance(new PipelineDefinitionIdentity(new string('a', 64)), new PipelineExecutionIdentity(new string('b', 64)), new DatasetDefinitionIdentity(new string('c', 64)), new SourceStateIdentity(new string('e', 64)),
            Enum.GetValues<ResearchPipelineStage>().Select(stage => new PipelineStageEvidence(stage, PipelineStageOutcome.NewlyAccepted)), snapshot, new DatasetVersion(snapshot));
    }
}
