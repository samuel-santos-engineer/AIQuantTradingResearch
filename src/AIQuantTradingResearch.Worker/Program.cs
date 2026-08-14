using System.Globalization;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

using var host = builder.Build();
var researchUseCase = host.Services.GetRequiredService<IResearchUseCase>();
var request = new ResearchRequest("SAMPLE-USD", 3);
var outcome = researchUseCase.Execute(request);

if (outcome.IsSuccess)
{
    var result = outcome.Result
        ?? throw new InvalidOperationException("A successful research outcome must contain a result.");

    Console.WriteLine($"Target: {result.Target}");
    Console.WriteLine($"Observation count: {result.ObservationCount}");
    Console.WriteLine($"Mean price: {result.MeanPrice.Value.ToString("0.00", CultureInfo.InvariantCulture)}");

    return 0;
}

var failure = outcome.Failure
    ?? throw new InvalidOperationException("A failed research outcome must contain a failure.");

Console.Error.WriteLine($"Research failed: {failure}");
return 1;
