using System.Text.Json;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
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
    public const string AuthorizationHeader = "Authorization";
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
        var lifecycle = Stopwatch.StartNew();
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
            PersistentSqliteQualificationDiagnostics.Emit("REQUEST_ARRIVED", configuration, lifecycle);
            PersistentSqliteQualificationDiagnostics.Emit("HANDLER_ENTERED", configuration, lifecycle);
            var customToken = context.Request.Headers[EvidenceTokenHeader].ToString();
            var authorization = context.Request.Headers[AuthorizationHeader].ToString();
            var bearerToken = authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                ? authorization["Bearer ".Length..]
                : string.Empty;
            if (!string.Equals(customToken, configuration.HttpEvidenceToken, StringComparison.Ordinal)
                && !string.Equals(bearerToken, configuration.HttpEvidenceToken, StringComparison.Ordinal))
            {
                PersistentSqliteQualificationDiagnostics.Emit("HANDLER_ENTERED", configuration, lifecycle, "UnauthorizedRequest");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (!string.Equals(context.Request.Query["runId"], configuration.RunId, StringComparison.Ordinal))
            {
                PersistentSqliteQualificationDiagnostics.Emit("HANDLER_ENTERED", configuration, lifecycle, "RunIdMismatch");
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
            PersistentSqliteQualificationDiagnostics.Emit("EVIDENCE_RETRIEVAL_SUCCEEDED", configuration, lifecycle);
            context.Response.OnCompleted(() =>
            {
                retrieved.TrySetResult();
                return Task.CompletedTask;
            });
        });

        PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STARTING", configuration, lifecycle, "Started", includeListenerMetadata: true);
        var resultCode = 1;
        Exception? shutdownFailure = null;
        try
        {
            await app.StartAsync(cancellationToken);
            PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STARTED", configuration, lifecycle, "Succeeded", includeListenerMetadata: true);
            var timeout = Task.Delay(TimeSpan.FromSeconds(180), cancellationToken);
            var completed = await Task.WhenAny(retrieved.Task, timeout);
            if (completed != retrieved.Task)
            {
                PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STOPPING", configuration, lifecycle, "RetrievalTimeout");
            }
            resultCode = completed == retrieved.Task ? 0 : 1;
        }
        catch
        {
            PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STOPPING", configuration, lifecycle, "ListenerStartFailure");
            throw;
        }
        finally
        {
            PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STOPPING", configuration, lifecycle);
            try
            {
                await app.StopAsync(CancellationToken.None);
                PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STOPPED", configuration, lifecycle);
            }
            catch (Exception failure)
            {
                PersistentSqliteQualificationDiagnostics.Emit("LISTENER_STOPPED", configuration, lifecycle, "ListenerShutdownFailure");
                shutdownFailure = failure;
            }
            finally
            {
                await app.DisposeAsync();
            }
        }

        if (shutdownFailure is not null)
        {
            ExceptionDispatchInfo.Capture(shutdownFailure).Throw();
        }

        PersistentSqliteQualificationDiagnostics.Emit(
            "WORKER_EXITING",
            configuration,
            lifecycle,
            resultCode == 0 ? "Succeeded" : "RetrievalTimeout");

        return resultCode;
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

public static class PersistentSqliteQualificationDiagnostics
{
    private static readonly HashSet<string> EventNames =
    [
        "QUALIFICATION_ENTERED", "SQLITE_QUALIFICATION_STARTED", "ARTIFACT_WRITE_SUCCEEDED",
        "LISTENER_STARTING", "LISTENER_STARTED", "REQUEST_ARRIVED", "HANDLER_ENTERED",
        "EVIDENCE_RETRIEVAL_SUCCEEDED", "LISTENER_STOPPING", "LISTENER_STOPPED", "WORKER_EXITING",
    ];

    public static string Format(
        string eventName,
        string runId,
        string phase,
        long elapsedMilliseconds,
        string outcome = "Succeeded",
        bool includeListenerMetadata = false)
    {
        if (!EventNames.Contains(eventName))
        {
            throw new ArgumentOutOfRangeException(nameof(eventName));
        }

        return includeListenerMetadata
            ? $"WP04_DIAG_EVENT={eventName} WP04_DIAG_RUN_ID={runId} WP04_DIAG_PHASE={phase} WP04_DIAG_ELAPSED_MS={elapsedMilliseconds} WP04_DIAG_OUTCOME={outcome} WP04_DIAG_LISTENER_HOST=0.0.0.0 WP04_DIAG_LISTENER_PORT=8501 WP04_DIAG_ROUTE={PersistentSqliteQualificationEvidenceEndpoint.Route} WP04_DIAG_HTTP_METHOD=GET"
            : $"WP04_DIAG_EVENT={eventName} WP04_DIAG_RUN_ID={runId} WP04_DIAG_PHASE={phase} WP04_DIAG_ELAPSED_MS={elapsedMilliseconds} WP04_DIAG_OUTCOME={outcome}";
    }

    internal static void Emit(
        string eventName,
        PersistentSqliteQualificationConfiguration configuration,
        Stopwatch stopwatch,
        string outcome = "Succeeded",
        bool includeListenerMetadata = false) =>
        Console.Error.WriteLine(Format(
            eventName,
            configuration.RunId,
            configuration.Phase.ToString().ToLowerInvariant(),
            stopwatch.ElapsedMilliseconds,
            outcome,
            includeListenerMetadata));
}

public sealed record PersistentSqliteQualificationEvidenceReadResult(int StatusCode, string? Content);
