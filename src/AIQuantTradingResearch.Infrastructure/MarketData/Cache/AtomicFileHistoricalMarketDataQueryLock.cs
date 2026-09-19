using System.Globalization;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.Cache;

/// <summary>Bounded, schema-free coordination for one historical cache key.</summary>
public sealed class AtomicFileHistoricalMarketDataQueryLock
{
    private readonly string directory;

    public AtomicFileHistoricalMarketDataQueryLock(string directory)
    {
        if (!Path.IsPathFullyQualified(directory)) throw new ArgumentException("The lock directory must be absolute.", nameof(directory));
        this.directory = Path.GetFullPath(directory);
    }

    public async Task<IDisposable?> AcquireAsync(HistoricalMarketDataCacheKey key, TimeSpan deadline, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(key);
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, $".{key.Fingerprint}.query.lock");
        var owner = Guid.NewGuid().ToString("N");
        var expires = DateTimeOffset.UtcNow + deadline + TimeSpan.FromSeconds(5);
        while (!cancellationToken.IsCancellationRequested && DateTimeOffset.UtcNow < expires)
        {
            try
            {
                await using var stream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None, 256, FileOptions.Asynchronous | FileOptions.WriteThrough);
                await using var writer = new StreamWriter(stream);
                await writer.WriteAsync($"{owner}|{DateTimeOffset.UtcNow:O}");
                await writer.FlushAsync(cancellationToken);
                return new Releaser(path, owner);
            }
            catch (IOException)
            {
                TryRecoverExpired(path, deadline + TimeSpan.FromSeconds(5));
                await Task.Delay(TimeSpan.FromMilliseconds(25), cancellationToken);
            }
        }

        return null;
    }

    private static void TryRecoverExpired(string path, TimeSpan lease)
    {
        try
        {
            var parts = File.ReadAllText(path).Split('|');
            if (parts.Length == 2 && DateTimeOffset.TryParse(parts[1], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var created) && DateTimeOffset.UtcNow - created > lease)
                File.Delete(path);
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
    }

    private sealed class Releaser(string path, string owner) : IDisposable
    {
        public void Dispose()
        {
            try
            {
                if (File.Exists(path) && File.ReadAllText(path).StartsWith(owner + "|", StringComparison.Ordinal)) File.Delete(path);
            }
            catch (IOException) { }
            catch (UnauthorizedAccessException) { }
        }
    }
}
