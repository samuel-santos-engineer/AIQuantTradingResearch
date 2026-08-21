using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using AIQuantTradingResearch.Application.Datasets;

namespace AIQuantTradingResearch.Application.Pipelines;

internal static class PipelineIdentityComputer
{
    private static readonly Encoding Utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

    public static PipelineDefinitionIdentity ComputeDefinitionIdentity(
        DatasetDefinitionIdentity datasetDefinitionIdentity) =>
        new(ComputeFingerprint(
            "pipeline-definition",
            [
                PipelineDefinition.SemanticModelVersion,
                datasetDefinitionIdentity.Scheme,
                datasetDefinitionIdentity.Fingerprint,
                "1", nameof(ResearchPipelineStage.HistoricalObservationRetrieval),
                "2", nameof(ResearchPipelineStage.DatasetMaterialization),
                "3", nameof(ResearchPipelineStage.SnapshotPersistence),
                "4", nameof(ResearchPipelineStage.CatalogRegistration),
                "5", nameof(ResearchPipelineStage.StructuredResultEvidence),
            ]));

    public static PipelineExecutionIdentity ComputeSuccessIdentity(
        PipelineDefinitionIdentity definitionIdentity,
        DatasetDefinition definition,
        SourceStateIdentity sourceStateIdentity,
        DatasetSnapshotIdentity snapshotIdentity) =>
        new(ComputeFingerprint(
            "pipeline-execution-success",
            ExecutionPrefix(definitionIdentity, definition, sourceStateIdentity)
                .Append("success")
                .Append(snapshotIdentity.Scheme)
                .Append(snapshotIdentity.Fingerprint)
                .ToArray()));

    public static PipelineExecutionIdentity ComputeFailureIdentity(
        PipelineDefinitionIdentity definitionIdentity,
        DatasetDefinition definition,
        SourceStateIdentity? sourceStateIdentity,
        ResearchPipelineStage failingStage,
        PipelineFailureCategory failureCategory) =>
        new(ComputeFingerprint(
            "pipeline-execution-failure",
            ExecutionPrefix(definitionIdentity, definition, sourceStateIdentity)
                .Append("failure")
                .Append(((int)failingStage).ToString(CultureInfo.InvariantCulture))
                .Append(failingStage.ToString())
                .Append(failureCategory.ToString())
                .ToArray()));

    private static List<string> ExecutionPrefix(
        PipelineDefinitionIdentity definitionIdentity,
        DatasetDefinition definition,
        SourceStateIdentity? sourceStateIdentity)
    {
        ArgumentNullException.ThrowIfNull(definitionIdentity);
        ArgumentNullException.ThrowIfNull(definition);

        return
        [
            definitionIdentity.Scheme,
            definitionIdentity.Fingerprint,
            definition.Target,
            definition.From.UtcTicks.ToString(CultureInfo.InvariantCulture),
            definition.To.UtcTicks.ToString(CultureInfo.InvariantCulture),
            sourceStateIdentity is null ? "source-state-absent" : "source-state-present",
            sourceStateIdentity?.Scheme ?? string.Empty,
            sourceStateIdentity?.Fingerprint ?? string.Empty,
        ];
    }

    private static string ComputeFingerprint(string typeDomain, string[] fields)
    {
        var canonical = new StringBuilder();
        AppendField(canonical, typeDomain);
        AppendField(canonical, PipelineIdentityScheme.Name);
        AppendField(canonical, fields.Length.ToString(CultureInfo.InvariantCulture));

        foreach (var field in fields)
        {
            AppendField(canonical, field);
        }

        return Convert.ToHexString(SHA256.HashData(Utf8.GetBytes(canonical.ToString()))).ToLowerInvariant();
    }

    private static void AppendField(StringBuilder canonical, string value)
    {
        canonical.Append(Utf8.GetByteCount(value).ToString(CultureInfo.InvariantCulture));
        canonical.Append(':');
        canonical.Append(value);
    }
}
