using System.Globalization;
using AIQuantTradingResearch.Application.Experiments;

namespace AIQuantTradingResearch.Worker;

internal sealed class DurableExperimentDiscoveryExecution(
    IDurableExperimentDiscoveryUseCase discoveryUseCase)
{
    public int Execute(DurableExperimentDiscoveryConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        var request = configuration.Request;
        var result = discoveryUseCase.Execute(request)
            ?? throw new InvalidOperationException(
                "The durable experiment discovery use case returned no result.");

        if (!result.IsSuccess)
        {
            Console.Error.WriteLine($"Durable experiment discovery failure: {result.Failure}");
            return 1;
        }

        var evidence = result.Evidence
            ?? throw new InvalidOperationException(
                "A successful durable experiment discovery result contained no evidence collection.");

        Console.WriteLine("Mode: Durable Experiment Evidence Discovery");
        Console.WriteLine($"Snapshot identity: {request.SnapshotIdentity.Fingerprint}");
        Console.WriteLine(
            $"Experiment definition identity: {request.DefinitionIdentity.Fingerprint}");
        Console.WriteLine(
            $"Requested maximum: {request.MaximumResultCount.ToString(CultureInfo.InvariantCulture)}");
        Console.WriteLine($"Returned count: {evidence.Count.ToString(CultureInfo.InvariantCulture)}");

        for (var index = 0; index < evidence.Count; index++)
        {
            Present(index, evidence[index]);
        }

        return 0;
    }

    private static void Present(int index, DurableExperimentEvidence evidence)
    {
        var prefix = $"Experiment result {index.ToString(CultureInfo.InvariantCulture)}";
        var summary = evidence.Summary;

        Console.WriteLine($"{prefix} identity: {evidence.Identity.Fingerprint}");
        Console.WriteLine($"{prefix} snapshot identity: {evidence.Provenance.SnapshotIdentity.Fingerprint}");
        Console.WriteLine(
            $"{prefix} dataset version identity: "
            + evidence.Provenance.SnapshotVersion.SnapshotIdentity.Fingerprint);
        Console.WriteLine(
            $"{prefix} experiment definition identity: {evidence.DefinitionIdentity.Fingerprint}");
        Console.WriteLine($"{prefix} feature set identity: {evidence.Provenance.FeatureSetIdentity.Fingerprint}");
        Console.WriteLine(
            $"{prefix} feature definition identity: "
            + evidence.Provenance.FeatureDefinitionIdentity.Fingerprint);
        Console.WriteLine(
            $"{prefix} dataset definition identity: "
            + evidence.Provenance.DatasetDefinitionIdentity.Fingerprint);
        Console.WriteLine(
            $"{prefix} research dataset identity: "
            + evidence.Provenance.ResearchDatasetIdentity.Fingerprint);
        Console.WriteLine(
            $"{prefix} source state identity: {evidence.Provenance.SourceStateIdentity.Fingerprint}");
        Console.WriteLine($"{prefix} source authority: {evidence.Provenance.SourceAuthority}");
        Console.WriteLine(
            $"{prefix} dataset observation count: "
            + evidence.Provenance.DatasetObservationCount.ToString(CultureInfo.InvariantCulture));
        Console.WriteLine(
            $"{prefix} experiment value count: {summary.Count.ToString(CultureInfo.InvariantCulture)}");

        if (!summary.HasAggregates)
        {
            Console.WriteLine($"{prefix} aggregates: absent");
            return;
        }

        Console.WriteLine(
            $"{prefix} arithmetic mean: "
            + summary.ArithmeticMean!.Value.ToString(CultureInfo.InvariantCulture));
        Console.WriteLine(
            $"{prefix} minimum: {summary.Minimum!.Value.ToString(CultureInfo.InvariantCulture)}");
        Console.WriteLine(
            $"{prefix} maximum: {summary.Maximum!.Value.ToString(CultureInfo.InvariantCulture)}");
    }
}
