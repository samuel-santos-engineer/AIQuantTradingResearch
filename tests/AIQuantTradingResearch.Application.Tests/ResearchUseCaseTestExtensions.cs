using AIQuantTradingResearch.Application.Research;

namespace AIQuantTradingResearch.Application.Tests;

internal static class ResearchUseCaseTestExtensions
{
    public static ResearchOutcome Execute(this ResearchUseCase useCase, ResearchRequest request) =>
        useCase.ExecuteAsync(request).GetAwaiter().GetResult();
}
