using System.Diagnostics;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.Integration;

namespace AIQuantTradingResearch.Infrastructure.PythonIntegration;

public sealed class PythonCapabilityInvoker : ICapabilityInvoker
{
    private const int ContractVersion = 1;
    private const int MaximumRequestCharacters = 65_536;
    private const int MaximumResponseCharacters = 65_536;
    private const int MaximumDiagnosticCharacters = 16_384;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);
    private static readonly ActivitySource InteropActivitySource = new("AIQuantTradingResearch.Worker");

    private readonly string repositoryRoot;
    private readonly string interpreterPath;
    private readonly string entrypointPath;

    public PythonCapabilityInvoker(PythonIntegrationConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        repositoryRoot = Path.GetFullPath(configuration.RepositoryRoot);
        interpreterPath = ResolveContainedPath(
            repositoryRoot,
            OperatingSystem.IsWindows()
                ? Path.Combine(".venv", "Scripts", "python.exe")
                : Path.Combine(".venv", "bin", "python"));
        entrypointPath = ResolveContainedPath(
            repositoryRoot,
            Path.Combine("python", "integration", "protocol_endpoint.py"));
    }

    internal PythonCapabilityInvoker(
        string repositoryRoot,
        string interpreterPath,
        string entrypointPath)
    {
        this.repositoryRoot = Path.GetFullPath(repositoryRoot);
        this.interpreterPath = Path.GetFullPath(interpreterPath);
        this.entrypointPath = Path.GetFullPath(entrypointPath);
    }

    public async Task<CapabilityInvocationResult> InvokeAsync(
        CapabilityInvocationRequest request,
        TimeSpan timeout,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (timeout <= TimeSpan.Zero || timeout > TimeSpan.FromMinutes(5))
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.InvalidRequest,
                "Execution timeout must be positive and bounded to five minutes.");
        }

        if (!File.Exists(interpreterPath) || !File.Exists(Path.Combine(repositoryRoot, ".venv", "pyvenv.cfg")))
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.ConfigurationUnavailable,
                "The governed project interpreter is unavailable.");
        }

        if (!File.Exists(entrypointPath))
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.ConfigurationUnavailable,
                "The governed integration entrypoint is unavailable.");
        }

        var serializedRequest = JsonSerializer.Serialize(
            new ProtocolRequest(
                request.ContractVersion,
                request.Operation,
                request.CorrelationId,
                request.Payload),
            JsonOptions);

        if (serializedRequest.Length > MaximumRequestCharacters)
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.InvalidRequest,
                "The integration request exceeds the bounded protocol size.");
        }

        using var activity = InteropActivitySource.StartActivity("interop.invoke");
        CapabilityInvocationResult Complete(CapabilityInvocationResult result)
        {
            var outcome = result.IsSuccess
                ? "success"
                : result.Failure == CapabilityInvocationFailure.Cancelled
                    ? "cancelled"
                    : "failed";
            activity?.SetTag("aiq.release", "1.10");
            activity?.SetTag("aiq.component", "interop");
            activity?.SetTag("aiq.operation", "interop.invoke");
            activity?.SetTag("aiq.outcome", outcome);
            if (!result.IsSuccess)
            {
                activity?.SetTag("aiq.error_class", result.Failure.ToString());
            }

            activity?.SetStatus(result.IsSuccess ? ActivityStatusCode.Ok : ActivityStatusCode.Error);
            return result;
        }

        using var process = new Process { StartInfo = CreateStartInfo() };

        try
        {
            if (!process.Start())
            {
                return Complete(CapabilityInvocationResult.Failed(
                    CapabilityInvocationFailure.DependencyFailure,
                    "The integration process could not be started."));
            }
        }
        catch (Exception exception) when (exception is System.ComponentModel.Win32Exception or InvalidOperationException)
        {
            return Complete(CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.DependencyFailure,
                "The integration process could not be started."));
        }

        var stdoutTask = ReadBoundedAsync(process.StandardOutput, MaximumResponseCharacters);
        var stderrTask = ReadBoundedAsync(process.StandardError, MaximumDiagnosticCharacters);

        try
        {
            await process.StandardInput.WriteAsync(serializedRequest.AsMemory(), cancellationToken);
            await process.StandardInput.FlushAsync(cancellationToken);
            process.StandardInput.Close();

            using var timeoutSource = new CancellationTokenSource(timeout);
            using var completionSource = CancellationTokenSource.CreateLinkedTokenSource(
                cancellationToken,
                timeoutSource.Token);

            try
            {
                await process.WaitForExitAsync(completionSource.Token);
            }
            catch (OperationCanceledException)
            {
                await TerminateOwnedProcessAsync(process);
                await DrainWithoutFailureAsync(stdoutTask, stderrTask);

                if (cancellationToken.IsCancellationRequested)
                {
                    return Complete(CapabilityInvocationResult.Failed(
                        CapabilityInvocationFailure.Cancelled,
                        "The integration invocation was cancelled."));
                }

                return Complete(CapabilityInvocationResult.Failed(
                    CapabilityInvocationFailure.Timeout,
                    "The integration invocation exceeded its bounded timeout."));
            }

            string stdout;
            string stderr;
            try
            {
                stdout = await stdoutTask;
                stderr = await stderrTask;
            }
            catch (InvalidDataException)
            {
                return Complete(CapabilityInvocationResult.Failed(
                    CapabilityInvocationFailure.MalformedResponse,
                    "The integration process exceeded a bounded output limit."));
            }

            if (process.ExitCode != 0)
            {
                return Complete(CapabilityInvocationResult.Failed(
                    CapabilityInvocationFailure.DependencyFailure,
                    BuildSafeExitMessage(process.ExitCode, stderr)));
            }

            return Complete(DeserializeResponse(stdout, request.CorrelationId));
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            await TerminateOwnedProcessAsync(process);
            await DrainWithoutFailureAsync(stdoutTask, stderrTask);
            return Complete(CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.Cancelled,
                "The integration invocation was cancelled."));
        }
        catch
        {
            await TerminateOwnedProcessAsync(process);
            await DrainWithoutFailureAsync(stdoutTask, stderrTask);
            throw;
        }
    }

    private ProcessStartInfo CreateStartInfo()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = interpreterPath,
            WorkingDirectory = repositoryRoot,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            StandardInputEncoding = Encoding.UTF8,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        startInfo.ArgumentList.Add(entrypointPath);
        startInfo.Environment["PYTHONNOUSERSITE"] = "1";
        startInfo.Environment["PYTHONDONTWRITEBYTECODE"] = "1";
        startInfo.Environment["PYTHONIOENCODING"] = "utf-8";
        return startInfo;
    }

    private static CapabilityInvocationResult DeserializeResponse(string stdout, string correlationId)
    {
        ProtocolResponse? response;
        try
        {
            response = JsonSerializer.Deserialize<ProtocolResponse>(stdout, JsonOptions);
        }
        catch (JsonException)
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.MalformedResponse,
                "The integration process returned malformed protocol output.");
        }

        if (response is null || response.ContractVersion != ContractVersion ||
            !string.Equals(response.CorrelationId, correlationId, StringComparison.Ordinal))
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.MalformedResponse,
                "The integration process returned an invalid protocol response.");
        }

        if (string.Equals(response.Status, "success", StringComparison.Ordinal))
        {
            return response.Result is not null
                ? CapabilityInvocationResult.Success(response.Result)
                : CapabilityInvocationResult.Failed(
                    CapabilityInvocationFailure.MalformedResponse,
                    "The integration success response omitted its result.");
        }

        if (!string.Equals(response.Status, "failure", StringComparison.Ordinal) || response.Code is null)
        {
            return CapabilityInvocationResult.Failed(
                CapabilityInvocationFailure.MalformedResponse,
                "The integration process returned an unknown response status.");
        }

        var failure = response.Code switch
        {
            "InvalidRequest" => CapabilityInvocationFailure.InvalidRequest,
            "UnsupportedContractVersion" => CapabilityInvocationFailure.UnsupportedContractVersion,
            _ => CapabilityInvocationFailure.DependencyFailure
        };
        return CapabilityInvocationResult.Failed(failure, response.Message ?? "The integration process reported failure.");
    }

    private static async Task<string> ReadBoundedAsync(StreamReader reader, int maximumCharacters)
    {
        var builder = new StringBuilder(Math.Min(maximumCharacters, 4096));
        var buffer = new char[1024];
        var exceeded = false;
        int read;
        while ((read = await reader.ReadAsync(buffer.AsMemory())) > 0)
        {
            if (builder.Length + read <= maximumCharacters)
            {
                builder.Append(buffer, 0, read);
            }
            else
            {
                exceeded = true;
            }
        }

        return exceeded
            ? throw new InvalidDataException("Process output exceeded its bounded limit.")
            : builder.ToString();
    }

    private static async Task TerminateOwnedProcessAsync(Process process)
    {
        if (process.HasExited)
        {
            return;
        }

        process.Kill(entireProcessTree: true);
        await process.WaitForExitAsync();
    }

    private static async Task DrainWithoutFailureAsync(params Task<string>[] streams)
    {
        try
        {
            await Task.WhenAll(streams);
        }
        catch (InvalidDataException)
        {
        }
    }

    private static string ResolveContainedPath(string root, string relativePath)
    {
        var candidate = Path.GetFullPath(Path.Combine(root, relativePath));
        var rootedPrefix = root.EndsWith(Path.DirectorySeparatorChar)
            ? root
            : root + Path.DirectorySeparatorChar;
        if (!candidate.StartsWith(rootedPrefix, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Integration paths must remain within the repository root.");
        }

        return candidate;
    }

    private static string BuildSafeExitMessage(int exitCode, string stderr) =>
        string.IsNullOrWhiteSpace(stderr)
            ? $"The integration process exited with code {exitCode}."
            : $"The integration process exited with code {exitCode} and bounded diagnostics.";

    private sealed record ProtocolRequest(
        int ContractVersion,
        string Operation,
        string CorrelationId,
        IReadOnlyDictionary<string, string> Payload);

    private sealed record ProtocolResponse(
        int ContractVersion,
        string Status,
        string CorrelationId,
        IReadOnlyDictionary<string, string>? Result,
        string? Code,
        string? Message,
        bool? Retryable);
}
