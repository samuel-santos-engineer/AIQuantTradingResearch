using AIQuantTradingResearch.Application.Research;
using Microsoft.Extensions.DependencyInjection;

namespace AIQuantTradingResearch.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IResearchUseCase, ResearchUseCase>();

        return services;
    }
}
