using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class PersistentSqliteQualificationEvidenceEndpointTests
{
    [Fact]
    public void ValidateEvidenceJsonAcceptsOnlyTheBoundedRecordForItsRun()
    {
        const string runId = "wp04-http-evidence-test";
        var content = """
            {"RecordVersion":1,"Phase":"initialize","RunId":"wp04-http-evidence-test","DatabasePathIdentity":"aiquant.db","SchemaVersion":4,"JournalMode":"delete","AcceptedEvidenceIdentity":"WP04-QUALIFICATION:1","AcceptedEvidenceCount":1,"IntegrityCheck":"ok","QuickCheck":"ok","PersistenceContinuity":true}
            """;

        var accepted = PersistentSqliteQualificationEvidenceEndpoint.ValidateEvidenceJson(content, runId);
        var stale = PersistentSqliteQualificationEvidenceEndpoint.ValidateEvidenceJson(content, "other-run");
        var extraProperty = PersistentSqliteQualificationEvidenceEndpoint.ValidateEvidenceJson(
            content.Replace("}", ",\"Unexpected\":true}", StringComparison.Ordinal), runId);

        Assert.Equal(200, accepted.StatusCode);
        Assert.Equal(content, accepted.Content);
        Assert.Equal(500, stale.StatusCode);
        Assert.Null(stale.Content);
        Assert.Equal(500, extraProperty.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-json")]
    [InlineData("{\"RecordVersion\":1}")]
    public void ValidateEvidenceJsonDoesNotExposeMissingOrMalformedArtifacts(string content)
    {
        var result = PersistentSqliteQualificationEvidenceEndpoint.ValidateEvidenceJson(content, "run");

        Assert.NotEqual(200, result.StatusCode);
        Assert.Null(result.Content);
    }
}
