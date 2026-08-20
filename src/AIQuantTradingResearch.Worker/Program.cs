using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Research;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Worker;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

var builder = Host.CreateApplicationBuilder(args);
var apiKeyPath = $"{TwelveDataConfiguration.SectionName}:{TwelveDataConfiguration.ApiKeyName}";
var databasePath = $"{SqliteStorageConfiguration.SectionName}:{SqliteStorageConfiguration.DatabasePathName}";
const string datasetTargetPath = "Dataset:Target";
const string datasetFromPath = "Dataset:From";
const string datasetToPath = "Dataset:To";

TwelveDataConfiguration twelveDataConfiguration;
SqliteStorageConfiguration sqliteStorageConfiguration;
DatasetDefinition datasetDefinition;

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

try
{
    datasetDefinition = new DatasetDefinition(
        builder.Configuration[datasetTargetPath] ?? string.Empty,
        ParseRequiredTimestamp(builder.Configuration[datasetFromPath], datasetFromPath),
        ParseRequiredTimestamp(builder.Configuration[datasetToPath], datasetToPath));
}
catch (ArgumentException)
{
    Console.Error.WriteLine("Invalid mandatory dataset configuration.");
    return 1;
}

builder.Services.AddApplication();
builder.Services.AddInfrastructure(twelveDataConfiguration, sqliteStorageConfiguration);
builder.Services.AddTransient<DatasetMaterializationExecution>();

using var host = builder.Build();
var execution = host.Services.GetRequiredService<DatasetMaterializationExecution>();
return execution.Execute(datasetDefinition);

static DateTimeOffset ParseRequiredTimestamp(string? value, string configurationPath)
{
    if (string.IsNullOrWhiteSpace(value)
        || !DateTimeOffset.TryParseExact(
            value,
            "O",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var timestamp))
    {
        throw new ArgumentException($"Missing or invalid mandatory configuration: {configurationPath}.");
    }

    return timestamp;
}
