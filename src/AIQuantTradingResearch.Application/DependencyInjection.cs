using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Application.Persistence;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Application.Research;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IResearchUseCase, ResearchUseCase>();
        services.AddTransient<IPersistHistoricalObservationsUseCase, PersistHistoricalObservationsUseCase>();
        services.AddTransient<IMaterializeDatasetUseCase, MaterializeDatasetUseCase>();
        services.AddTransient<IDatasetMaterializationIntegrationUseCase, DatasetMaterializationIntegrationUseCase>();
        services.AddTransient<IPipelineExecutionUseCase, PipelineExecutionUseCase>();
        services.AddSingleton<IPipelineRequestFactory, PipelineRequestFactory>();
        services.AddTransient<IFeatureGenerationUseCase, FeatureGenerationUseCase>();
        services.AddTransient<IFeatureComputer, SimpleReturnFeatureComputer>();
        services.AddTransient<IFeatureGenerationValidator, FeatureGenerationValidator>();
        services.AddTransient<IExperimentGenerationUseCase, ExperimentGenerationUseCase>();
        services.AddTransient<IExperimentSummaryComputer, SimpleReturnDescriptiveSummaryComputer>();
        services.AddTransient<IExperimentGenerationValidator, ExperimentGenerationValidator>();

        return services;
    }
}

public interface IPipelineRequestFactory
{
    PipelineRequest Create(DatasetDefinition definition);
}

internal sealed class PipelineRequestFactory : IPipelineRequestFactory
{
    public PipelineRequest Create(DatasetDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        DatasetDefinitionIdentity datasetDefinitionIdentity =
            DatasetIdentityComputer.ComputeDefinitionIdentity(definition);
        PipelineDefinitionIdentity pipelineDefinitionIdentity =
            PipelineIdentityComputer.ComputeDefinitionIdentity(datasetDefinitionIdentity);

        return new PipelineRequest(new PipelineDefinition(definition, pipelineDefinitionIdentity));
    }
}
