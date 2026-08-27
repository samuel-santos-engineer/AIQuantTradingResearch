using AIQuantTradingResearch.Application;
using AIQuantTradingResearch.Application.Experiments;
using AIQuantTradingResearch.Application.Features;
using AIQuantTradingResearch.Infrastructure;
using AIQuantTradingResearch.Infrastructure.MarketData.TwelveData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using AIQuantTradingResearch.Infrastructure.Visualization;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Worker;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);
var apiKeyPath = $"{TwelveDataConfiguration.SectionName}:{TwelveDataConfiguration.ApiKeyName}";
var databasePath = $"{SqliteStorageConfiguration.SectionName}:{SqliteStorageConfiguration.DatabasePathName}";

TwelveDataConfiguration twelveDataConfiguration;
SqliteStorageConfiguration sqliteStorageConfiguration;
var isExperimentExecutionRequested = builder.Configuration["Experiment:SnapshotIdentity"] is not null
    || builder.Configuration["Experiment:SnapshotVersion"] is not null;
var isFeatureExecutionRequested = builder.Configuration["Feature:SnapshotIdentity"] is not null
    || builder.Configuration["Feature:SnapshotVersion"] is not null;
var isDurableExperimentExecutionRequested = builder.Configuration["DurableExperiment:SnapshotIdentity"] is not null
    || builder.Configuration["DurableExperiment:SnapshotVersion"] is not null;
var isDurableExperimentDiscoveryRequested =
    builder.Configuration["DurableExperimentDiscovery:SnapshotIdentity"] is not null
    || builder.Configuration["DurableExperimentDiscovery:ExperimentDefinitionIdentity"] is not null
    || builder.Configuration["DurableExperimentDiscovery:MaximumResultCount"] is not null;

ExperimentExecutionConfiguration? experimentExecutionConfiguration = null;
DurableExperimentExecutionConfiguration? durableExperimentExecutionConfiguration = null;
DurableExperimentDiscoveryConfiguration? durableExperimentDiscoveryConfiguration = null;
if (isDurableExperimentDiscoveryRequested)
{
    try
    {
        durableExperimentDiscoveryConfiguration =
            DurableExperimentDiscoveryConfiguration.From(builder.Configuration);
    }
    catch (ArgumentException)
    {
        Console.Error.WriteLine("Invalid mandatory durable experiment discovery configuration.");
        return 1;
    }
}
else if (isDurableExperimentExecutionRequested)
{
    try { durableExperimentExecutionConfiguration = DurableExperimentExecutionConfiguration.From(builder.Configuration); }
    catch (ArgumentException) { Console.Error.WriteLine("Invalid mandatory durable experiment configuration."); return 1; }
}
if (!isDurableExperimentDiscoveryRequested && isExperimentExecutionRequested)
{
    try
    {
        experimentExecutionConfiguration = ExperimentExecutionConfiguration.From(builder.Configuration);
    }
    catch (ArgumentException)
    {
        Console.Error.WriteLine("Invalid mandatory experiment configuration.");
        return 1;
    }
}

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

VisualizationHandoffOptions visualizationHandoffOptions;
try
{
    visualizationHandoffOptions = VisualizationHandoffOptions.From(builder.Configuration);
}
catch (ArgumentException)
{
    Console.Error.WriteLine("Invalid visualization handoff configuration.");
    return 1;
}

builder.Services.AddApplication();
builder.Services.AddInfrastructure(twelveDataConfiguration, sqliteStorageConfiguration);
builder.Services.AddSingleton(new VisualizationReadModelFilePublisher(visualizationHandoffOptions.HandoffPath));
builder.Services.AddSingleton<AtomicVisualizationReadModelStore>();
builder.Services.AddSingleton<IVisualizationReadModelStore>(serviceProvider =>
    new VisualizationReadModelFilePublishingStore(
        serviceProvider.GetRequiredService<AtomicVisualizationReadModelStore>(),
        serviceProvider.GetRequiredService<VisualizationReadModelFilePublisher>()));
builder.Services.AddTransient<PipelineExecution>();
builder.Services.AddTransient<FeatureExecution>();
builder.Services.AddTransient<ExperimentExecution>();
builder.Services.AddTransient<DurableExperimentExecution>();
builder.Services.AddTransient<DurableExperimentDiscoveryExecution>();
builder.Services.AddSingleton<IWorkerLifecycleLivenessGate>(_ => args.Contains("--wp08-test-liveness", StringComparer.Ordinal)
    ? new TestWorkerLifecycleLivenessGate()
    : new NoOpWorkerLifecycleLivenessGate());
builder.Services.AddTransient<SimulatedLiveVisualizationExecution>();

using var host = builder.Build();
if (durableExperimentDiscoveryConfiguration is not null)
{
    return host.Services.GetRequiredService<DurableExperimentDiscoveryExecution>()
        .Execute(durableExperimentDiscoveryConfiguration);
}
if (durableExperimentExecutionConfiguration is not null)
{
    return host.Services.GetRequiredService<DurableExperimentExecution>().Execute(durableExperimentExecutionConfiguration);
}
if (experimentExecutionConfiguration is not null)
{
    return host.Services.GetRequiredService<ExperimentExecution>().Execute(experimentExecutionConfiguration);
}

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

var workerMode = builder.Configuration["Worker:Mode"];
if (string.Equals(workerMode, "Replay", StringComparison.OrdinalIgnoreCase)
    || (!string.IsNullOrWhiteSpace(workerMode)
        && !string.Equals(workerMode, "Historical", StringComparison.OrdinalIgnoreCase)))
{
    try
    {
        var workerConfiguration = SimulatedLiveVisualizationConfiguration.From(builder.Configuration);
        using var lifetime = new WorkerLifetimeCancellation();
        host.Services.GetRequiredService<VisualizationReadModelFilePublisher>().StartSession();
        try
        {
            return host.Services.GetRequiredService<SimulatedLiveVisualizationExecution>()
                .Execute(workerConfiguration, lifetime.Token);
        }
        catch (OperationCanceledException) when (lifetime.Token.IsCancellationRequested)
        {
            return 0;
        }
    }
    catch (ArgumentException)
    {
        Console.Error.WriteLine("Invalid Worker or Dataset configuration.");
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
host.Services.GetRequiredService<VisualizationReadModelFilePublisher>().StartSession();
return execution.Execute(pipelineExecutionConfiguration);
