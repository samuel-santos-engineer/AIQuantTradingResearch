using System.Text.Json;
using AIQuantTradingResearch.Application.Datasets;
using AIQuantTradingResearch.Application.Pipelines;
using AIQuantTradingResearch.Application.Visualization;
using AIQuantTradingResearch.Infrastructure.Visualization;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class VisualizationSemanticExposureTests
{
    [Fact]
    public void PublisherPreservesAdditiveCanonicalSemanticValues()
    {
        string directory = Path.Combine(Path.GetTempPath(), $"aiq-wp07-{Guid.NewGuid():N}"); Directory.CreateDirectory(directory);
        try
        {
            string path = Path.Combine(directory, "frame.json"); var publisher = new VisualizationReadModelFilePublisher(path);
            var model = VisualizationReadModel.Create(VisualizationRevision.Historical(new HistoricalPresentationRevision(1), new string('a', 64)), VisualizationSourceMode.Historical, DatasetSourceAuthority.AcceptedRelease11HistoricalObservations, "BTC", VisualizationPresentationState.Ready, idempotencyStatus: PresentationIdempotencyStatus.EquivalentExisting, dataQualityStatus: PresentationDataQualityStatus.Valid);
            publisher.Publish(model);
            using var json = JsonDocument.Parse(File.ReadAllText(path));
            Assert.Equal("EquivalentExisting", json.RootElement.GetProperty("idempotencyStatus").GetString());
            Assert.Equal("Valid", json.RootElement.GetProperty("dataQualityStatus").GetString());
            Assert.Equal("Ready", json.RootElement.GetProperty("state").GetString());
        }
        finally { Directory.Delete(directory, true); }
    }
}
