namespace AIQuantTradingResearch.Application.Datasets;

using AIQuantTradingResearch.Domain;

public interface IMaterializeDatasetUseCase
{
    DatasetMaterializationResult Execute(DatasetDefinition definition);

    DatasetMaterializationResult Execute(
        DatasetDefinition definition,
        IReadOnlyList<PriceObservation> observations) =>
        throw new NotSupportedException("This materializer does not accept explicit observations.");
}
