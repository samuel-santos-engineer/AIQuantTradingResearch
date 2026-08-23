namespace AIQuantTradingResearch.Application.Experiments;

internal sealed class DurableExperimentUseCase : IDurableExperimentUseCase
{
    private readonly IExperimentGenerationUseCase experimentGenerationUseCase;
    private readonly IDurableExperimentEvidenceStore durableEvidenceStore;

    public DurableExperimentUseCase(
        IExperimentGenerationUseCase experimentGenerationUseCase,
        IDurableExperimentEvidenceStore durableEvidenceStore)
    {
        ArgumentNullException.ThrowIfNull(experimentGenerationUseCase);
        ArgumentNullException.ThrowIfNull(durableEvidenceStore);

        this.experimentGenerationUseCase = experimentGenerationUseCase;
        this.durableEvidenceStore = durableEvidenceStore;
    }

    public DurableExperimentUseCaseResult Execute(ExperimentGenerationRequest request)
    {
        if (request is null)
        {
            return DurableExperimentUseCaseResult.Failed(DurableExperimentEvidenceFailure.InvalidRequest);
        }

        var generation = experimentGenerationUseCase.Execute(request)
            ?? throw new InvalidOperationException("The experiment-generation use case returned no result.");

        if (!generation.IsSuccess)
        {
            return DurableExperimentUseCaseResult.Failed(MapGenerationFailure(generation.Failure));
        }

        var experiment = generation.Experiment
            ?? throw new InvalidOperationException("A successful experiment-generation result contained no experiment.");
        var evidence = ProjectEvidence(experiment);
        var acceptance = durableEvidenceStore.Accept(new DurableExperimentAcceptanceRequest(evidence))
            ?? throw new InvalidOperationException("The durable experiment evidence store returned no result.");

        if (!acceptance.IsSuccess)
        {
            return DurableExperimentUseCaseResult.Failed(acceptance.Failure!.Value);
        }

        return DurableExperimentUseCaseResult.Accepted(evidence, acceptance.Disposition!.Value);
    }

    private static DurableExperimentEvidence ProjectEvidence(ExperimentResult experiment)
    {
        ArgumentNullException.ThrowIfNull(experiment);

        var featureProvenance = experiment.Provenance.FeatureProvenance;
        var datasetProvenance = featureProvenance.DatasetProvenance;
        var featureLineage = experiment.Lineage.FeatureLineage;
        var datasetLineage = featureLineage.DatasetLineage;

        return new DurableExperimentEvidence(
            experiment.Definition,
            experiment.DefinitionIdentity,
            experiment.Identity,
            experiment.Summary,
            new DurableExperimentProvenance(
                experiment.Provenance.DefinitionIdentity,
                experiment.Provenance.FeatureSetIdentity,
                featureProvenance.DefinitionIdentity,
                featureProvenance.SnapshotIdentity,
                featureProvenance.SnapshotVersion,
                datasetProvenance.DefinitionIdentity,
                datasetProvenance.ResearchDatasetIdentity,
                datasetProvenance.SourceStateIdentity,
                datasetProvenance.SourceAuthority,
                datasetProvenance.ObservationCount),
            new DurableExperimentLineage(
                experiment.Lineage.DefinitionIdentity,
                featureLineage.DefinitionIdentity,
                datasetLineage.DefinitionIdentity,
                datasetLineage.SourceStateIdentity));
    }

    private static DurableExperimentEvidenceFailure MapGenerationFailure(ExperimentGenerationFailure? failure) => failure switch
    {
        ExperimentGenerationFailure.InvalidRequest => DurableExperimentEvidenceFailure.InvalidRequest,
        ExperimentGenerationFailure.UnsupportedDefinition => DurableExperimentEvidenceFailure.InvalidEvidence,
        ExperimentGenerationFailure.FeatureSetNotFound => DurableExperimentEvidenceFailure.NotFound,
        ExperimentGenerationFailure.DependencyUnavailable => DurableExperimentEvidenceFailure.DependencyUnavailable,
        ExperimentGenerationFailure.InvalidFeatureEvidence => DurableExperimentEvidenceFailure.InvalidEvidence,
        ExperimentGenerationFailure.InvalidNumericEvidence => DurableExperimentEvidenceFailure.InvalidEvidence,
        ExperimentGenerationFailure.IntegrityConflict => DurableExperimentEvidenceFailure.IntegrityConflict,
        _ => throw new InvalidOperationException("The experiment-generation use case returned an unknown failure."),
    };
}
