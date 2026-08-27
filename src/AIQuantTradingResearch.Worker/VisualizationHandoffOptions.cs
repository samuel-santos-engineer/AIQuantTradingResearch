using Microsoft.Extensions.Configuration;

namespace AIQuantTradingResearch.Worker;

internal sealed class VisualizationHandoffOptions
{
    public const string HandoffPathKey = "Visualization:HandoffPath";
    public const string RefreshIntervalSecondsKey = "Visualization:RefreshIntervalSeconds";
    public const int DefaultRefreshIntervalSeconds = 2;
    public const int MinimumRefreshIntervalSeconds = 1;
    public const int MaximumRefreshIntervalSeconds = 60;

    private VisualizationHandoffOptions(string handoffPath, int refreshIntervalSeconds)
    {
        HandoffPath = handoffPath;
        RefreshIntervalSeconds = refreshIntervalSeconds;
    }

    public string HandoffPath { get; }
    public int RefreshIntervalSeconds { get; }

    public static VisualizationHandoffOptions From(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        string? overridePath = configuration[HandoffPathKey];
        string path = string.IsNullOrWhiteSpace(overridePath)
            ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AIQuantTradingResearch", "Release1.9", "runtime", "visualization-read-model.json")
            : overridePath;
        if (!Path.IsPathFullyQualified(path))
            throw new ArgumentException($"{HandoffPathKey} must be an absolute file path.", nameof(configuration));
        path = Path.GetFullPath(path);
        string? refreshValue = configuration[RefreshIntervalSecondsKey];
        int refresh = string.IsNullOrWhiteSpace(refreshValue) ? DefaultRefreshIntervalSeconds
            : int.TryParse(refreshValue, System.Globalization.NumberStyles.None, System.Globalization.CultureInfo.InvariantCulture, out int parsed) ? parsed
            : throw new ArgumentException($"{RefreshIntervalSecondsKey} must be an integer from {MinimumRefreshIntervalSeconds} to {MaximumRefreshIntervalSeconds}.", nameof(configuration));
        if (refresh < MinimumRefreshIntervalSeconds || refresh > MaximumRefreshIntervalSeconds)
            throw new ArgumentOutOfRangeException(nameof(configuration), $"{RefreshIntervalSecondsKey} must be from {MinimumRefreshIntervalSeconds} to {MaximumRefreshIntervalSeconds}.");
        return new VisualizationHandoffOptions(path, refresh);
    }
}
