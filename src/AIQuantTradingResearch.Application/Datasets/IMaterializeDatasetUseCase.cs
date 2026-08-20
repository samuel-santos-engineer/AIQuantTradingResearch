namespace AIQuantTradingResearch.Application.Datasets;

public interface IMaterializeDatasetUseCase
{
    DatasetMaterializationResult Execute(DatasetDefinition definition);
}
