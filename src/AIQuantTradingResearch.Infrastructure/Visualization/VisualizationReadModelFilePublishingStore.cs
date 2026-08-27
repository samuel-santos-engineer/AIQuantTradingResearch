using AIQuantTradingResearch.Application.Visualization;

namespace AIQuantTradingResearch.Infrastructure.Visualization;

public sealed class VisualizationReadModelFilePublishingStore : IVisualizationReadModelStore
{
    private readonly IVisualizationReadModelStore inner;
    private readonly VisualizationReadModelFilePublisher publisher;
    public VisualizationReadModelFilePublishingStore(IVisualizationReadModelStore inner, VisualizationReadModelFilePublisher publisher)
    { this.inner = inner ?? throw new ArgumentNullException(nameof(inner)); this.publisher = publisher ?? throw new ArgumentNullException(nameof(publisher)); }
    public VisualizationReadModel? Current => inner.Current;
    public bool Publish(VisualizationReadModel model)
    { bool accepted = inner.Publish(model); if (accepted) publisher.Publish(Current ?? throw new InvalidOperationException("Accepted read model was unavailable.")); return accepted; }
}
