using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Features;
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
var isFeatureExecutionRequested = builder.Configuration["Feature:SnapshotIdentity"] is not null
    || builder.Configuration["Feature:SnapshotVersion"] is not null;

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
builder.Services.AddTransient<PipelineExecution>();
builder.Services.AddTransient<FeatureExecution>();

using var host = builder.Build();
if (isFeatureExecutionRequested)
{
    try
    {
        var featureConfiguration = FeatureExecutionConfiguration.From(builder.Configuration);
        return host.Services.GetRequiredService<FeatureExecution>().Execute(featureConfiguration);
    }
    catch (ArgumentException)
    {
        Console.Error.WriteLine("Invalid mandatory feature configuration.");
        return 1;
    }
}

PipelineExecutionConfiguration pipelineExecutionConfiguration;
try
{
    pipelineExecutionConfiguration = PipelineExecutionConfiguration.From(builder.Configuration);
}
catch (ArgumentException)
{
    Console.Error.WriteLine("Invalid mandatory dataset configuration.");
    return 1;
}

var execution = host.Services.GetRequiredService<PipelineExecution>();
return execution.Execute(pipelineExecutionConfiguration);
