using System.Text.Json;
using AIQuantTradingResearch.Application.Visualization;

namespace AIQuantTradingResearch.Infrastructure.Visualization;

public sealed class VisualizationReadModelFilePublisher
{
    public const string ContractVersion = "aiq-visualization-read-model-v1";
    private const int ReplaceAttemptCount = 20;
    private const int ReplaceRetryDelayMilliseconds = 10;
    private readonly string handoffPath;
    private readonly string directory;
    private readonly string temporaryPrefix;

    public VisualizationReadModelFilePublisher(string handoffPath)
    {
        if (!Path.IsPathFullyQualified(handoffPath)) throw new ArgumentException("The handoff path must be absolute.", nameof(handoffPath));
        this.handoffPath = Path.GetFullPath(handoffPath);
        directory = Path.GetDirectoryName(this.handoffPath) ?? throw new ArgumentException("The handoff path must have a parent directory.", nameof(handoffPath));
        temporaryPrefix = $".{Path.GetFileName(this.handoffPath)}.";
    }

    public string HandoffPath => handoffPath;

    public void StartSession()
    {
        Directory.CreateDirectory(directory);
        if (File.Exists(handoffPath)) File.Delete(handoffPath);
        foreach (string path in Directory.EnumerateFiles(directory, $"{temporaryPrefix}*.tmp")) File.Delete(path);
    }

    public void Publish(VisualizationReadModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        Directory.CreateDirectory(directory);
        string temporaryPath = Path.Combine(directory, $"{temporaryPrefix}{Guid.NewGuid():N}.tmp");
        try
        {
            var payload = new
            {
                contractVersion = ContractVersion,
                revision = new { kind = model.Revision.Kind.ToString(), value = model.Revision.Value, identity = model.Revision.Identity },
                sourceMode = model.SourceMode.ToString(), sourceAuthority = (int)model.SourceAuthority, target = model.Target, state = model.State.ToString(),
                window = model.Window.Select(x => new { sourceTime = x.SourceTime.ToString("O"), price = x.Price }),
                latest = model.Latest is null ? null : new { sourceTime = model.Latest.SourceTime.ToString("O"), price = model.Latest.Price },
                observationCount = model.ObservationCount,
                idempotencyStatus = model.IdempotencyStatus.ToString(), dataQualityStatus = model.DataQualityStatus.ToString(),
                snapshotIdentity = model.SnapshotIdentity?.Fingerprint, datasetVersion = model.DatasetVersion?.SnapshotIdentity.Fingerprint,
                feature = model.Feature is null ? null : new { identity = model.Feature.Identity, value = model.Feature.Value, observationCount = model.Feature.ObservationCount, requiredObservationCount = model.Feature.RequiredObservationCount },
                pipeline = model.Pipeline is null ? null : new { isSuccess = model.Pipeline.IsSuccess, definitionIdentity = model.Pipeline.DefinitionIdentity.Fingerprint, executionIdentity = model.Pipeline.ExecutionIdentity.Fingerprint, datasetDefinitionIdentity = model.Pipeline.DatasetDefinitionIdentity.Fingerprint, sourceStateIdentity = model.Pipeline.SourceStateIdentity?.Fingerprint, snapshotIdentity = model.Pipeline.SnapshotIdentity?.Fingerprint, datasetVersion = model.Pipeline.DatasetVersion?.SnapshotIdentity.Fingerprint, stages = model.Pipeline.Stages.Select(s => new { stage = s.Stage.ToString(), outcome = s.Outcome.ToString(), failureCategory = s.FailureCategory?.ToString() }) },
                failure = model.Failure is null ? null : new { category = model.Failure.Category, message = model.Failure.Message, recoverable = model.Failure.IsRecoverable },
                staleReason = model.StaleReason,
            };
            using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.WriteThrough))
            {
                JsonSerializer.Serialize(stream, payload);
                stream.Flush(flushToDisk: true);
            }
            ReplaceAtomically(temporaryPath);
        }
        finally { if (File.Exists(temporaryPath)) File.Delete(temporaryPath); }
    }

    private void ReplaceAtomically(string temporaryPath)
    {
        for (int attempt = 1; ; attempt++)
        {
            try
            {
                File.Move(temporaryPath, handoffPath, overwrite: true);
                return;
            }
            catch (UnauthorizedAccessException) when (attempt < ReplaceAttemptCount)
            {
                Thread.Sleep(ReplaceRetryDelayMilliseconds);
            }
            catch (IOException) when (attempt < ReplaceAttemptCount)
            {
                Thread.Sleep(ReplaceRetryDelayMilliseconds);
            }
        }
    }
}
