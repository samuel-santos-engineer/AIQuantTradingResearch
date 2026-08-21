using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

public enum ResearchPipelineStage
{
    HistoricalObservationRetrieval = 1,
    DatasetMaterialization = 2,
    SnapshotPersistence = 3,
    CatalogRegistration = 4,
    StructuredResultEvidence = 5,
}

public sealed record PipelineDefinition
{
    public const string SemanticModelVersion = "1";

    private static readonly ResearchPipelineStage[] FixedStages =
    [
        ResearchPipelineStage.HistoricalObservationRetrieval,
        ResearchPipelineStage.DatasetMaterialization,
        ResearchPipelineStage.SnapshotPersistence,
        ResearchPipelineStage.CatalogRegistration,
        ResearchPipelineStage.StructuredResultEvidence,
    ];

    private static readonly IReadOnlyList<ResearchPipelineStage> FixedTopology = Array.AsReadOnly(FixedStages);

    public static IReadOnlyList<ResearchPipelineStage> Topology => FixedTopology;

    public PipelineDefinition(
        DatasetDefinition datasetDefinition,
        PipelineDefinitionIdentity identity)
    {
        ArgumentNullException.ThrowIfNull(datasetDefinition);
        ArgumentNullException.ThrowIfNull(identity);

        DatasetDefinition = datasetDefinition;
        Identity = identity;
        ModelVersion = SemanticModelVersion;
        Stages = Topology;
    }

    public DatasetDefinition DatasetDefinition { get; }

    public PipelineDefinitionIdentity Identity { get; }

    public string ModelVersion { get; }

    public IReadOnlyList<ResearchPipelineStage> Stages { get; }
}

public sealed record PipelineRequest
{
    public PipelineRequest(PipelineDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);
        Definition = definition;
    }

    public PipelineDefinition Definition { get; }

    public DatasetDefinition DatasetDefinition => Definition.DatasetDefinition;
}
