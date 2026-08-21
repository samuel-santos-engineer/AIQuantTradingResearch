using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

internal static class PipelineValidation
{
    public static (DatasetDefinitionIdentity DatasetDefinitionIdentity, PipelineDefinitionIdentity DefinitionIdentity)
        ValidateRequest(PipelineRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        DatasetDefinitionIdentity datasetDefinitionIdentity =
            DatasetIdentityComputer.ComputeDefinitionIdentity(request.DatasetDefinition);
        PipelineDefinitionIdentity expectedDefinitionIdentity =
            PipelineIdentityComputer.ComputeDefinitionIdentity(datasetDefinitionIdentity);

        if (request.Definition.Identity != expectedDefinitionIdentity)
        {
            throw new ArgumentException(
                "The pipeline definition identity does not match its dataset definition and fixed topology.",
                nameof(request));
        }

        return (datasetDefinitionIdentity, expectedDefinitionIdentity);
    }
}
