using System.Globalization;
using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Worker;

internal sealed class FeatureExecution
{
    private readonly IFeatureGenerationUseCase featureGenerationUseCase;

    public FeatureExecution(IFeatureGenerationUseCase featureGenerationUseCase)
    {
        ArgumentNullException.ThrowIfNull(featureGenerationUseCase);
        this.featureGenerationUseCase = featureGenerationUseCase;
    }

    public int Execute(FeatureExecutionConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var result = featureGenerationUseCase.Execute(configuration.Request)
            ?? throw new InvalidOperationException("The feature-generation use case returned no result.");
        if (!result.IsSuccess)
        {
            Console.Error.WriteLine($"Feature failure: {result.Failure}");
            return 1;
        }

        var featureSet = result.FeatureSet
            ?? throw new InvalidOperationException("A successful feature-generation result contained no feature set.");
        Console.WriteLine($"Feature definition: {FeatureDefinition.SimpleReturnLag1V1Name}");
        Console.WriteLine($"Feature definition identity: {featureSet.DefinitionIdentity.Fingerprint}");
        Console.WriteLine($"Feature set identity: {featureSet.Identity.Fingerprint}");
        Console.WriteLine($"Snapshot identity: {featureSet.SnapshotIdentity.Fingerprint}");
        Console.WriteLine($"Dataset version identity: {featureSet.SnapshotVersion.SnapshotIdentity.Fingerprint}");
        Console.WriteLine($"Feature value count: {featureSet.Count.ToString(CultureInfo.InvariantCulture)}");

        for (var index = 0; index < featureSet.Values.Count; index++)
        {
            var value = featureSet.Values[index];
            Console.WriteLine(
                $"Feature value {index.ToString(CultureInfo.InvariantCulture)}: "
                + $"{value.Instant.ToString("O", CultureInfo.InvariantCulture)} = "
                + value.Value.ToString(CultureInfo.InvariantCulture));
        }

        return 0;
    }
}
