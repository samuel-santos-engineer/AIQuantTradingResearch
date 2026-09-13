using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace AIQuantTradingResearch.Worker;

/// <summary>
/// Serves the qualification record already emitted atomically by the application.
/// This endpoint never opens SQLite and does not accept an artifact path from callers.
/// </summary>
public static class PersistentSqliteQualificationEvidenceEndpoint
{
    public const string Route = "/internal/wp04/persistence-qualification";
    public const string EvidenceTokenHeader = "X-WP04-Evidence-Token";
    private static readonly HashSet<string> RequiredProperties =
    [
        "RecordVersion", "Phase", "RunId", "DatabasePathIdentity", "SchemaVersion",
        "JournalMode", "AcceptedEvidenceIdentity", "AcceptedEvidenceCount", "IntegrityCheck",
        "QuickCheck", "PersistenceContinuity",
    ];

    internal static async Task<int> ServeAsync(
        PersistentSqliteQualificationConfiguration configuration,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        if (!configuration.HttpEvidenceEnabled
            || string.IsNullOrWhiteSpace(configuration.HttpEvidenceToken)
            || string.IsNullOrWhiteSpace(configuration.EvidenceOutputPath))
        {
            return 1;
        }

        var builder = WebApplication.CreateSlimBuilder();
        builder.WebHost.UseUrls("http://0.0.0.0:8501");
        var app = builder.Build();
        var retrieved = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

        app.MapGet(Route, async context =>
        {
            if (!string.Equals(context.Request.Headers[EvidenceTokenHeader], configuration.HttpEvidenceToken, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (!string.Equals(context.Request.Query["runId"], configuration.RunId, StringComparison.Ordinal))
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                return;
            }

            var result = ReadEvidenceArtifact(configuration.EvidenceOutputPath, configuration.RunId);
            context.Response.StatusCode = result.StatusCode;
            if (result.Content is null)
            {
                return;
            }

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result.Content, context.RequestAborted);
            context.Response.OnCompleted(() =>
            {
                retrieved.TrySetResult();
                return Task.CompletedTask;
            });
        });

        await app.StartAsync(cancellationToken);
        try
        {
            var timeout = Task.Delay(TimeSpan.FromSeconds(180), cancellationToken);
            var completed = await Task.WhenAny(retrieved.Task, timeout);
            return completed == retrieved.Task ? 0 : 1;
        }
        finally
        {
            await app.StopAsync(CancellationToken.None);
            await app.DisposeAsync();
        }
    }

    public static PersistentSqliteQualificationEvidenceReadResult ValidateEvidenceJson(
        string content,
        string expectedRunId)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status404NotFound, null);
        }

        try
        {
            using var document = JsonDocument.Parse(content);
            var root = document.RootElement;
            if (root.ValueKind != JsonValueKind.Object
                || root.EnumerateObject().Select(property => property.Name).ToHashSet(StringComparer.Ordinal).SetEquals(RequiredProperties) is false
                || root.EnumerateObject().Count() != RequiredProperties.Count
                || root.GetProperty("RecordVersion").GetInt32() != 1
                || !string.Equals(root.GetProperty("RunId").GetString(), expectedRunId, StringComparison.Ordinal)
                || string.IsNullOrWhiteSpace(root.GetProperty("Phase").GetString())
                || string.IsNullOrWhiteSpace(root.GetProperty("DatabasePathIdentity").GetString())
                || string.IsNullOrWhiteSpace(root.GetProperty("JournalMode").GetString())
                || string.IsNullOrWhiteSpace(root.GetProperty("AcceptedEvidenceIdentity").GetString())
                || root.GetProperty("AcceptedEvidenceCount").GetInt64() < 1
                || string.IsNullOrWhiteSpace(root.GetProperty("IntegrityCheck").GetString())
                || string.IsNullOrWhiteSpace(root.GetProperty("QuickCheck").GetString())
                || (root.GetProperty("PersistenceContinuity").ValueKind is not JsonValueKind.True and not JsonValueKind.False))
            {
                return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status500InternalServerError, null);
            }

            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status200OK, content);
        }
        catch (JsonException)
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status500InternalServerError, null);
        }
        catch (InvalidOperationException)
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status500InternalServerError, null);
        }
    }

    private static PersistentSqliteQualificationEvidenceReadResult ReadEvidenceArtifact(string path, string expectedRunId)
    {
        try
        {
            return ValidateEvidenceJson(File.ReadAllText(path), expectedRunId);
        }
        catch (FileNotFoundException)
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status404NotFound, null);
        }
        catch (DirectoryNotFoundException)
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status404NotFound, null);
        }
        catch (IOException)
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status500InternalServerError, null);
        }
        catch (UnauthorizedAccessException)
        {
            return new PersistentSqliteQualificationEvidenceReadResult(StatusCodes.Status500InternalServerError, null);
        }
    }
}

public sealed record PersistentSqliteQualificationEvidenceReadResult(int StatusCode, string? Content);
