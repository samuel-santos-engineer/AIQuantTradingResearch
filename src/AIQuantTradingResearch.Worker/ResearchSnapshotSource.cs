using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Worker;

/// <summary>Only the completely serialized public buffer leaves the durable publication callback.</summary>
public sealed class ResearchSnapshotSource(IResearchTransformStateReader reader, TimeProvider? clock = null)
{
    private readonly TimeProvider time = clock ?? TimeProvider.System;
    private readonly object sync = new();
    private readonly Dictionary<Guid, (DateOnly Day, long Epoch)> observations = [];
    public byte[] Read(ResearchSnapshotRequest request, CancellationToken cancellationToken = default)
    {
        lock (sync)
        {
            return reader.UseTransformSnapshot(request.ResearchInstrument, (state, config) =>
            {
                cancellationToken.ThrowIfCancellationRequested();
                var before = time.GetUtcNow(); var day = DateOnly.FromDateTime(before.UtcDateTime);
                if (observations.TryGetValue(state.Generation, out var old) && (day < old.Day || config.TransformEpoch < old.Epoch))
                    throw new ResearchSnapshotException("UNTRUSTED_TIME");
                var bytes = ResearchSnapshotTransformer.Project(state, config, request, before);
                cancellationToken.ThrowIfCancellationRequested();
                var after = time.GetUtcNow();
                if (after < before) throw new ResearchSnapshotException("UNTRUSTED_TIME");
                if (DateOnly.FromDateTime(after.UtcDateTime) != day) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
                if (observations.Count > 8 && !observations.ContainsKey(state.Generation)) throw new ResearchSnapshotException("DATA_UNAVAILABLE");
                observations[state.Generation] = (day, config.TransformEpoch);
                return bytes;
            });
        }
    }
}
