namespace AIQuantTradingResearch.Application.Experiments;

public interface IDurableExperimentDiscoveryUseCase
{
    DurableExperimentDiscoveryResult Execute(DurableExperimentDiscoveryRequest request);
}

internal sealed class DurableExperimentDiscoveryUseCase : IDurableExperimentDiscoveryUseCase
{
    private readonly IDurableExperimentEvidenceDiscoveryStore discoveryStore;

    public DurableExperimentDiscoveryUseCase(IDurableExperimentEvidenceDiscoveryStore discoveryStore)
    {
        ArgumentNullException.ThrowIfNull(discoveryStore);
        this.discoveryStore = discoveryStore;
    }

    public DurableExperimentDiscoveryResult Execute(DurableExperimentDiscoveryRequest request)
    {
        if (request is null || request.MaximumResultCount <= 0)
        {
            return DurableExperimentDiscoveryResult.Failed(DurableExperimentEvidenceFailure.InvalidRequest);
        }

        return discoveryStore.Discover(request)
            ?? throw new InvalidOperationException("The durable experiment evidence discovery store returned no result.");
    }
}
