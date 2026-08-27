using System.Threading;
using AIQuantTradingResearch.Application.Visualization;

namespace AIQuantTradingResearch.Infrastructure.Visualization;

public sealed class AtomicVisualizationReadModelStore : IVisualizationReadModelStore
{
    private VisualizationReadModel? current;
    public VisualizationReadModel? Current => Volatile.Read(ref current);

    public bool Publish(VisualizationReadModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        while (true)
        {
            var prior = Current;
            if (prior is not null)
            {
                if (model.Revision.Kind != prior.Revision.Kind)
                {
                    if (ReferenceEquals(Interlocked.CompareExchange(ref current, model, prior), prior)) return true;
                    continue;
                }
                var order = VisualizationRevisionComparer.Compare(model.Revision, prior.Revision);
                if (order < 0) return false;
                if (order == 0)
                {
                    if (VisualizationRevisionComparer.IsEquivalent(model.Revision, prior.Revision))
                    {
                        if (ReferenceEquals(Interlocked.CompareExchange(ref current, model, prior), prior)) return true;
                        continue;
                    }
                    throw new InvalidOperationException("Equal visualization revision with different identity is an integrity conflict.");
                }
            }
            if (ReferenceEquals(Interlocked.CompareExchange(ref current, model, prior), prior)) return true;
        }
    }
}
