using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var host = builder.Build();

await host.RunAsync();
