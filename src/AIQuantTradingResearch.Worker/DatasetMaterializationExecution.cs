using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Worker;

internal sealed class DatasetMaterializationExecution
{
    private readonly IDatasetMaterializationIntegrationUseCase integrationUseCase;

    public DatasetMaterializationExecution(IDatasetMaterializationIntegrationUseCase integrationUseCase)
    {
        ArgumentNullException.ThrowIfNull(integrationUseCase);
        this.integrationUseCase = integrationUseCase;
    }

    public int Execute(DatasetDefinition definition)
    {
        ArgumentNullException.ThrowIfNull(definition);

        var result = integrationUseCase.Execute(definition)
            ?? throw new InvalidOperationException("The dataset materialization integration use case returned no result.");

        if (result.IsSuccess)
        {
            var outcome = result.Outcome
                ?? throw new InvalidOperationException("A successful dataset materialization result must contain an outcome.");

            Console.WriteLine($"Dataset materialization outcome: {outcome}");
            return 0;
        }

        var failure = result.Failure
            ?? throw new InvalidOperationException("A failed dataset materialization result must contain a failure.");

        Console.Error.WriteLine($"Dataset materialization failed: {failure}");
        return 1;
    }
}
