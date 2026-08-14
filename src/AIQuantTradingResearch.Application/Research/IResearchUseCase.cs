namespace AIQuantTradingResearch.Application.Research;

public interface IResearchUseCase
{
    ResearchOutcome Execute(ResearchRequest request);
}
