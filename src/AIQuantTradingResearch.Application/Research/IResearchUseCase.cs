namespace AIQuantTradingResearch.Application.Research;

public interface IResearchUseCase
{
    Task<ResearchOutcome> ExecuteAsync(
        ResearchRequest request,
        CancellationToken cancellationToken = default);
}
