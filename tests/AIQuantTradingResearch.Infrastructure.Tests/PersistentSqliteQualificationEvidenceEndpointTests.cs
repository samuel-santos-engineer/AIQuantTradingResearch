using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class PersistentSqliteQualificationEvidenceEndpointTests
{
    [Fact]
    public void DiagnosticFormattingUsesOnlyTheApprovedSafeFields()
    {
        var diagnostic = PersistentSqliteQualificationDiagnostics.Format(
            "LISTENER_STARTED",
            "run-safe-001",
            "initialize",
            42,
            includeListenerMetadata: true);

        Assert.Equal(
            "WP04_DIAG_EVENT=LISTENER_STARTED WP04_DIAG_RUN_ID=run-safe-001 WP04_DIAG_PHASE=initialize WP04_DIAG_ELAPSED_MS=42 WP04_DIAG_OUTCOME=Succeeded WP04_DIAG_LISTENER_HOST=0.0.0.0 WP04_DIAG_LISTENER_PORT=8501 WP04_DIAG_ROUTE=/internal/wp04/persistence-qualification WP04_DIAG_HTTP_METHOD=GET",
            diagnostic);
        Assert.DoesNotContain("Token", diagnostic, StringComparison.Ordinal);
        Assert.DoesNotContain("X-WP04-Evidence-Token", diagnostic, StringComparison.Ordinal);
        Assert.DoesNotContain("?", diagnostic, StringComparison.Ordinal);
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            PersistentSqliteQualificationDiagnostics.Format("UNAPPROVED_EVENT", "run", "initialize", 0));
    }

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
