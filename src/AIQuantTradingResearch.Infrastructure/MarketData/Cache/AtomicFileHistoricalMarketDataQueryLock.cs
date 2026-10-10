using System.Globalization;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.Cache;

/// <summary>Bounded, schema-free coordination for one historical cache key.</summary>
public sealed class AtomicFileHistoricalMarketDataQueryLock
{
    private readonly string directory;
    private readonly string suppliedDirectory;

    public AtomicFileHistoricalMarketDataQueryLock(string directory)
    {
        if (!Path.IsPathFullyQualified(directory)) throw new ArgumentException("The lock directory must be absolute.", nameof(directory));
        suppliedDirectory = directory;
        this.directory = Path.GetFullPath(directory);
    }

    public async Task<IDisposable?> AcquireAsync(HistoricalMarketDataCacheKey key, TimeSpan deadline, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(key);
        key.ValidateNamespace();
        if (key.IsPublic)
        {
            if (suppliedDirectory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Contains("..", StringComparer.Ordinal)) throw new ArgumentException("Lock traversal is forbidden.");
            AtomicFileHistoricalMarketDataCache.ValidatePublicPath(directory);
        }
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, $".{key.Fingerprint}.query.lock");
        var owner = Guid.NewGuid().ToString("N");
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(deadline, TimeSpan.Zero);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(deadline, TimeSpan.FromSeconds(30));
        var acquisitionDeadline = DateTimeOffset.UtcNow + deadline;
        while (!cancellationToken.IsCancellationRequested && DateTimeOffset.UtcNow < acquisitionDeadline)
        {
            try
            {
                if (new FileInfo(path).LinkTarget is not null) throw new UnauthorizedAccessException("Linked query locks are forbidden.");
                var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 256, FileOptions.Asynchronous | FileOptions.WriteThrough);
                try
                {
                    stream.SetLength(0);
                    var bytes = System.Text.Encoding.UTF8.GetBytes($"{owner}|{DateTimeOffset.UtcNow:O}");
                    await stream.WriteAsync(bytes, cancellationToken); await stream.FlushAsync(cancellationToken);
                    return new Releaser(stream);
                }
                catch { stream.Dispose(); throw; }
            }
            catch (IOException)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(25), cancellationToken);
            }
        }

        cancellationToken.ThrowIfCancellationRequested();
        return null;
    }

    // Persistent inode and exclusive live handle avoid timestamp expiry and unlink/recreate races.
    // OS releases the handle on process exit. WP07 still owns child lifetime/global admission.
    private sealed class Releaser(FileStream stream) : IDisposable
    {
        public void Dispose()
        {
            stream.Dispose();
        }
    }
}
