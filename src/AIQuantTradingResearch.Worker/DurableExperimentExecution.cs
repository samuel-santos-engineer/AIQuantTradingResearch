using System.Globalization;
using AIQuantTradingResearch.Application.Experiments;

namespace AIQuantTradingResearch.Worker;

internal sealed class DurableExperimentExecution(IDurableExperimentUseCase useCase)
{
    public int Execute(DurableExperimentExecutionConfiguration configuration)
    {
        var result = useCase.Execute(configuration.Request) ?? throw new InvalidOperationException("The durable experiment use case returned no result.");
        if (!result.IsSuccess) { Console.Error.WriteLine($"Durable experiment failure: {result.Failure}"); return 1; }
        var evidence = result.Evidence ?? throw new InvalidOperationException("A successful durable experiment result contained no evidence.");
        var summary = evidence.Summary;
        Console.WriteLine($"Durable experiment disposition: {result.Disposition}");
        Console.WriteLine($"Experiment result identity: {evidence.Identity.Fingerprint}");
        Console.WriteLine($"Feature set identity: {evidence.Provenance.FeatureSetIdentity.Fingerprint}");
        Console.WriteLine($"Snapshot identity: {evidence.Provenance.SnapshotIdentity.Fingerprint}");
        Console.WriteLine($"Dataset version identity: {evidence.Provenance.SnapshotVersion.SnapshotIdentity.Fingerprint}");
        Console.WriteLine($"Experiment value count: {summary.Count.ToString(CultureInfo.InvariantCulture)}");
        if (!summary.HasAggregates) { Console.WriteLine("Experiment aggregates: absent"); return 0; }
        Console.WriteLine($"Experiment arithmetic mean: {summary.ArithmeticMean!.Value.ToString(CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Experiment minimum: {summary.Minimum!.Value.ToString(CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Experiment maximum: {summary.Maximum!.Value.ToString(CultureInfo.InvariantCulture)}");
        return 0;
    }
}
