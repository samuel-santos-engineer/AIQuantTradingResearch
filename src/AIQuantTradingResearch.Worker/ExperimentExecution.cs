using System.Globalization;
using AIQuantTradingResearch.Application.Experiments;

namespace AIQuantTradingResearch.Worker;

internal sealed class ExperimentExecution
{
    private readonly IExperimentGenerationUseCase experimentGenerationUseCase;

    public ExperimentExecution(IExperimentGenerationUseCase experimentGenerationUseCase)
    {
        ArgumentNullException.ThrowIfNull(experimentGenerationUseCase);
        this.experimentGenerationUseCase = experimentGenerationUseCase;
    }

    public int Execute(ExperimentExecutionConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var result = experimentGenerationUseCase.Execute(configuration.Request)
            ?? throw new InvalidOperationException("The experiment-generation use case returned no result.");
        if (!result.IsSuccess)
        {
            Console.Error.WriteLine($"Experiment failure: {result.Failure}");
            return 1;
        }

        var experiment = result.Experiment
            ?? throw new InvalidOperationException("A successful experiment-generation result contained no experiment.");
        var summary = experiment.Summary;

        Console.WriteLine($"Experiment definition: {experiment.Definition.Name}");
        Console.WriteLine($"Experiment definition identity: {experiment.DefinitionIdentity.Fingerprint}");
        Console.WriteLine($"Feature set identity: {experiment.FeatureSet.Identity.Fingerprint}");
        Console.WriteLine($"Snapshot identity: {experiment.FeatureSet.SnapshotIdentity.Fingerprint}");
        Console.WriteLine($"Dataset version identity: {experiment.FeatureSet.SnapshotVersion.SnapshotIdentity.Fingerprint}");
        Console.WriteLine($"Experiment result identity: {experiment.Identity.Fingerprint}");
        Console.WriteLine($"Experiment value count: {summary.Count.ToString(CultureInfo.InvariantCulture)}");

        if (!summary.HasAggregates)
        {
            Console.WriteLine("Experiment aggregates: absent");
            return 0;
        }

        Console.WriteLine($"Experiment arithmetic mean: {summary.ArithmeticMean!.Value.ToString(CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Experiment minimum: {summary.Minimum!.Value.ToString(CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Experiment maximum: {summary.Maximum!.Value.ToString(CultureInfo.InvariantCulture)}");
        return 0;
    }
}
