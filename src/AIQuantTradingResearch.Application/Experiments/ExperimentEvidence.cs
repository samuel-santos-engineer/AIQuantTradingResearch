using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

public sealed record ExperimentSummaryEvidence
{
    public ExperimentSummaryEvidence(
        int count,
        decimal? arithmeticMean,
        decimal? minimum,
        decimal? maximum)
    {
        if (count < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Experiment count cannot be negative.");
        }

        var aggregatesPresent = arithmeticMean.HasValue && minimum.HasValue && maximum.HasValue;
        var aggregatesAbsent = !arithmeticMean.HasValue && !minimum.HasValue && !maximum.HasValue;

        if ((count == 0 && !aggregatesAbsent) || (count > 0 && !aggregatesPresent))
        {
            throw new ArgumentException(
                "Experiment summary requires all aggregates to be absent for an empty result and present otherwise.");
        }

        Count = count;
        ArithmeticMean = arithmeticMean;
        Minimum = minimum;
        Maximum = maximum;
    }

    public int Count { get; }

    public decimal? ArithmeticMean { get; }

    public decimal? Minimum { get; }

    public decimal? Maximum { get; }

    public bool HasAggregates => ArithmeticMean.HasValue;
}

public sealed record ExperimentProvenance
{
    public ExperimentProvenance(
        ExperimentDefinitionIdentity definitionIdentity,
        FeatureSet featureSet)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(featureSet);

        DefinitionIdentity = definitionIdentity;
        FeatureSetIdentity = featureSet.Identity;
        FeatureProvenance = featureSet.Provenance;
    }

    public ExperimentDefinitionIdentity DefinitionIdentity { get; }

    public FeatureSetIdentity FeatureSetIdentity { get; }

    public FeatureProvenance FeatureProvenance { get; }
}

public sealed record ExperimentLineage
{
    public ExperimentLineage(
        ExperimentDefinitionIdentity definitionIdentity,
        FeatureSet featureSet)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(featureSet);

        DefinitionIdentity = definitionIdentity;
        FeatureLineage = featureSet.Lineage;
    }

    public ExperimentDefinitionIdentity DefinitionIdentity { get; }

    public FeatureLineage FeatureLineage { get; }
}

public sealed record ExperimentResult
{
    public ExperimentResult(
        ExperimentDefinition definition,
        ExperimentDefinitionIdentity definitionIdentity,
        ExperimentResultIdentity identity,
        FeatureSet featureSet,
        ExperimentSummaryEvidence summary,
        ExperimentProvenance provenance,
        ExperimentLineage lineage)
    {
        ArgumentNullException.ThrowIfNull(definition);
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(identity);
        ArgumentNullException.ThrowIfNull(featureSet);
        ArgumentNullException.ThrowIfNull(summary);
        ArgumentNullException.ThrowIfNull(provenance);
        ArgumentNullException.ThrowIfNull(lineage);

        var expectedDefinitionIdentity = ExperimentIdentityComputer.ComputeDefinitionIdentity(definition);
        var expectedResultIdentity = ExperimentIdentityComputer.ComputeResultIdentity(
            definitionIdentity,
            featureSet.Identity,
            summary);

        if (definition != ExperimentDefinition.SimpleReturnDescriptiveSummaryV1
            || definitionIdentity != expectedDefinitionIdentity
            || identity != expectedResultIdentity
            || provenance.DefinitionIdentity != definitionIdentity
            || provenance.FeatureSetIdentity != featureSet.Identity
            || lineage.DefinitionIdentity != definitionIdentity
            || provenance.FeatureProvenance != featureSet.Provenance
            || lineage.FeatureLineage != featureSet.Lineage)
        {
            throw new ArgumentException(
                "Experiment result evidence must retain coherent definition, feature, provenance, lineage, and identity references.");
        }

        Definition = definition;
        DefinitionIdentity = definitionIdentity;
        Identity = identity;
        FeatureSet = featureSet;
        Summary = summary;
        Provenance = provenance;
        Lineage = lineage;
    }

    public ExperimentDefinition Definition { get; }

    public ExperimentDefinitionIdentity DefinitionIdentity { get; }

    public ExperimentResultIdentity Identity { get; }

    public FeatureSet FeatureSet { get; }

    public ExperimentSummaryEvidence Summary { get; }

    public ExperimentProvenance Provenance { get; }

    public ExperimentLineage Lineage { get; }
}
