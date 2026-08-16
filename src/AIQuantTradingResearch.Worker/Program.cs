using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Worker;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);
var apiKeyPath = $"{TwelveDataConfiguration.SectionName}:{TwelveDataConfiguration.ApiKeyName}";
var databasePath = $"{SqliteStorageConfiguration.SectionName}:{SqliteStorageConfiguration.DatabasePathName}";

TwelveDataConfiguration twelveDataConfiguration;
SqliteStorageConfiguration sqliteStorageConfiguration;

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

try
{
    sqliteStorageConfiguration = new SqliteStorageConfiguration(
        builder.Configuration[databasePath] ?? string.Empty);
}
catch (ArgumentException)
{
    Console.Error.WriteLine($"Missing mandatory configuration: {databasePath}.");
    return 1;
}

builder.Services.AddApplication();
builder.Services.AddInfrastructure(twelveDataConfiguration, sqliteStorageConfiguration);
builder.Services.AddTransient<PersistentMarketDataExecution>();

using var host = builder.Build();
var execution = host.Services.GetRequiredService<PersistentMarketDataExecution>();
var request = new ResearchRequest("AAPL", 3);
return execution.Execute(request);
