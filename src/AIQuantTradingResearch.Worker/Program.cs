using System.Globalization;
using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);
var apiKeyPath = $"{TwelveDataConfiguration.SectionName}:{TwelveDataConfiguration.ApiKeyName}";

TwelveDataConfiguration twelveDataConfiguration;

try
{
    twelveDataConfiguration = new TwelveDataConfiguration(
        builder.Configuration[apiKeyPath] ?? string.Empty);
}
catch (ArgumentException)
{
    Console.Error.WriteLine($"Missing mandatory configuration: {apiKeyPath}.");
    return 1;
}

builder.Services.AddApplication();
builder.Services.AddInfrastructure(twelveDataConfiguration);

using var host = builder.Build();
var researchUseCase = host.Services.GetRequiredService<IResearchUseCase>();
var request = new ResearchRequest("AAPL", 3);
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
