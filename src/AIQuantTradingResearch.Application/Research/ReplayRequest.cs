namespace AIQuantTradingResearch.Application.Research;

public sealed record ReplayRequest(
    string ReplayIdentity,
    string Target,
    int StartingTick,
    int RequestedObservationCount);
