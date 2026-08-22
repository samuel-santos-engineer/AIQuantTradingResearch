using AIQuantTradingResearch.Application.Features;

namespace AIQuantTradingResearch.Application.Experiments;

internal sealed class SimpleReturnDescriptiveSummaryComputer : IExperimentSummaryComputer
{
    public ExperimentSummaryEvidence Compute(FeatureSet featureSet)
    {
        ArgumentNullException.ThrowIfNull(featureSet);

        if (featureSet.Values.Count == 0)
        {
            return new ExperimentSummaryEvidence(0, null, null, null);
        }

        var sum = featureSet.Values[0].Value;
        var minimum = sum;
        var maximum = sum;

        for (var index = 1; index < featureSet.Values.Count; index++)
        {
            var value = featureSet.Values[index].Value;
            sum += value;

            if (value < minimum)
            {
                minimum = value;
            }

            if (value > maximum)
            {
                maximum = value;
            }
        }

        return new ExperimentSummaryEvidence(
            featureSet.Values.Count,
            sum / featureSet.Values.Count,
            minimum,
            maximum);
    }
}
