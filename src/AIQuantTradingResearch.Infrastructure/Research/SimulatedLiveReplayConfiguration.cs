namespace AIQuantTradingResearch.Infrastructure.Research;

public sealed record SimulatedLiveReplayConfiguration(
    string ReplayIdentity = "simulated-live-replay-v1",
    string Target = "SIMULATED-USD")
{
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(ReplayIdentity) || string.IsNullOrWhiteSpace(Target))
        {
            throw new ArgumentException("Replay identity and target are required.");
        }
    }
}
