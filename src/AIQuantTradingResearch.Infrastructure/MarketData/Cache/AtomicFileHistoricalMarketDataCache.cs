using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;

namespace AIQuantTradingResearch.Infrastructure.MarketData.Cache;

public sealed class AtomicFileHistoricalMarketDataCache : IHistoricalMarketDataCache
{
    private readonly string directory; private readonly int retentionLimit;
    private readonly bool publicSynthetic;
    private readonly TimeProvider clock;
    private const int MaximumEntryBytes = 2 * 1024 * 1024;
    private const long MaximumDiskBytes = 32 * 1024 * 1024;
    private static readonly JsonSerializerOptions StrictJson = new() { UnmappedMemberHandling = System.Text.Json.Serialization.JsonUnmappedMemberHandling.Disallow, MaxDepth = 16 };
    public AtomicFileHistoricalMarketDataCache(string directory, int retentionLimit = 32, bool publicSynthetic = false, TimeProvider? clock = null)
    {
        if (!Path.IsPathFullyQualified(directory)) throw new ArgumentException("Cache directory must be absolute.", nameof(directory));
        if (publicSynthetic && directory.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar).Contains("..", StringComparer.Ordinal)) throw new ArgumentException("Cache traversal is forbidden.", nameof(directory));
        ArgumentOutOfRangeException.ThrowIfLessThan(retentionLimit, 1);
        this.directory = Path.GetFullPath(directory); this.publicSynthetic = publicSynthetic; this.clock = clock ?? TimeProvider.System;
        this.retentionLimit = publicSynthetic ? Math.Min(retentionLimit, 12) : retentionLimit;
        if (publicSynthetic) ValidatePublicPath(this.directory);
    }
    internal static void ValidatePublicPath(string directory)
    {
        if (Path.GetFileName(Path.TrimEndingDirectorySeparator(directory)) != "aiq-public-synthetic-cache" || directory.Contains("..", StringComparison.Ordinal)) throw new ArgumentException("An isolated public cache root is required.");
        for (var current = new DirectoryInfo(directory); current is not null; current = current.Parent)
        {
            if (current.Name is "aiq-private-research-cache" or "aiq-market-cache") throw new ArgumentException("Private/legacy cache ancestry is forbidden.");
            if (current.LinkTarget is not null || (current.Exists && (current.Attributes & FileAttributes.ReparsePoint) != 0)) throw new IOException("Linked cache roots are forbidden.");
        }
    }
    public async Task<HistoricalMarketDataCacheLookup> RetrieveAsync(HistoricalMarketDataCacheKey key, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(key); key.ValidateNamespace(); if (key.IsPublic != publicSynthetic) return HistoricalMarketDataCacheLookup.Corrupt(); if (publicSynthetic) ValidatePublicPath(directory); var path = Path.Combine(directory, key.Fingerprint + ".json"); if (!File.Exists(path)) return HistoricalMarketDataCacheLookup.Missing();
        if (publicSynthetic)
        {
            try
            {
                var info = new FileInfo(path);
                if (info.LinkTarget is not null || info.Length > MaximumEntryBytes) return HistoricalMarketDataCacheLookup.Corrupt();
                await using var source = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous);
                if (source.Length > MaximumEntryBytes) return HistoricalMarketDataCacheLookup.Corrupt();
                var bytes = new byte[(int)source.Length]; await source.ReadExactlyAsync(bytes, cancellationToken);
                using var document = JsonDocument.Parse(bytes, new JsonDocumentOptions { MaxDepth = 16 });
                RejectDuplicates(document.RootElement);
                RequireFields(document.RootElement, "Key", "DeadlineSeconds", "CachedAtUtc", "GeneratedAtUtc", "Identity", "Candles");
                RequireFields(document.RootElement.GetProperty("Key"), "Symbol", "Interval", "Range", "ProviderIdentity", "ContractVersion", "Context", "DatasetFingerprint", "WindowFingerprint", "IsPublic", "Fingerprint");
                RequireFields(document.RootElement.GetProperty("Identity"), "CanonicalVersion", "Instrument", "MarketType", "Source", "Status", "Venue", "Base", "Quote", "Settlement", "PriceUnit", "VolumeUnit", "GeneratorVersion", "Seed", "CoverageStartUtc", "CoverageEndExclusiveUtc", "DatasetFingerprint", "WindowFingerprint", "CalculationVersion");
                var wireRows = document.RootElement.GetProperty("Candles");
                if (wireRows.ValueKind != JsonValueKind.Array || wireRows.GetArrayLength() > 2410) return HistoricalMarketDataCacheLookup.Corrupt();
                foreach (var row in wireRows.EnumerateArray()) RequireFields(row, "OpenTimeUtc", "Open", "High", "Low", "Close", "Volume");
                var wireKey = document.RootElement.GetProperty("Key");
                if (wireKey.GetProperty("Fingerprint").GetString() != key.Fingerprint || wireKey.GetProperty("IsPublic").ValueKind != JsonValueKind.True) return HistoricalMarketDataCacheLookup.Corrupt();
                var record = JsonSerializer.Deserialize<PublicRecord>(bytes, StrictJson) ?? throw new ArgumentException("Empty cache.");
                if (record.Key != key || record.Identity is null || record.Candles is null || record.Candles.Any(c => c is null) || record.CachedAtUtc.Offset != TimeSpan.Zero) return HistoricalMarketDataCacheLookup.Corrupt();
                var age = clock.GetUtcNow() - record.CachedAtUtc;
                if (age < TimeSpan.Zero || age >= TimeSpan.FromHours(24)) return HistoricalMarketDataCacheLookup.Missing();
                var request = new HistoricalMarketDataRequest(key.Symbol, key.Interval, key.Range, TimeSpan.FromSeconds(record.DeadlineSeconds));
                var provenance = new HistoricalMarketDataProvenance(SyntheticPublicationPolicy.Source, record.GeneratedAtUtc, record.GeneratedAtUtc, false, true) { SyntheticIdentity = record.Identity };
                var entry = new HistoricalMarketDataCacheEntry(key, request, record.Candles.Select(c => new HistoricalCandle(key.Symbol, key.Interval, c.OpenTimeUtc, c.Open, c.High, c.Low, c.Close, c.Volume)), provenance, record.CachedAtUtc);
                return HistoricalMarketDataCacheLookup.Found(entry);
            }
            catch (JsonException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
            catch (ArgumentException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
            catch (OverflowException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
            catch (IOException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
            catch (UnauthorizedAccessException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
            catch (InvalidOperationException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
        }
        try { await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous); var record = await JsonSerializer.DeserializeAsync<Record>(stream, cancellationToken: cancellationToken); return record is null || record.Symbol != key.Symbol || record.Interval != key.Interval || record.Range != key.Range || record.Provider != key.ProviderIdentity || record.Version != key.ContractVersion ? HistoricalMarketDataCacheLookup.Corrupt() : HistoricalMarketDataCacheLookup.Found(ToEntry(record)); }
        catch (JsonException) { return HistoricalMarketDataCacheLookup.Corrupt(); } catch (ArgumentException) { return HistoricalMarketDataCacheLookup.Corrupt(); } catch (OverflowException) { return HistoricalMarketDataCacheLookup.Corrupt(); }
    }
    public async Task StoreAsync(HistoricalMarketDataCacheEntry entry, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entry); entry.Key.ValidateNamespace(); if (entry.Key.IsPublic != publicSynthetic) throw new ArgumentException("Cache context mismatch.");
        if (publicSynthetic) { await StorePublicAsync(entry, cancellationToken); return; }
        Directory.CreateDirectory(directory); var target = Path.Combine(directory, entry.Key.Fingerprint + ".json"); var temporary = Path.Combine(directory, "." + entry.Key.Fingerprint + "." + Guid.NewGuid().ToString("N") + ".tmp");
        try { await using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.WriteThrough)) { await JsonSerializer.SerializeAsync(stream, FromEntry(entry), cancellationToken: cancellationToken); await stream.FlushAsync(cancellationToken); stream.Flush(true); } File.Move(temporary, target, true); foreach (var file in Directory.EnumerateFiles(directory, "*.json").Select(static p => new FileInfo(p)).OrderByDescending(static f => f.LastWriteTimeUtc).Skip(retentionLimit)) file.Delete(); }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
    private async Task StorePublicAsync(HistoricalMarketDataCacheEntry entry, CancellationToken cancellationToken)
    {
        SyntheticPublicationPolicy.ValidateWindow(entry.Request, entry.Candles, entry.Provenance);
        ValidatePublicPath(directory); Directory.CreateDirectory(directory);
        var payload = JsonSerializer.SerializeToUtf8Bytes(new PublicRecord(entry.Key, (int)entry.Request.Deadline.TotalSeconds, clock.GetUtcNow(), entry.Provenance.AcquiredAtUtc, entry.Provenance.SyntheticIdentity!, entry.Candles.Select(c => new Row(c.OpenTimeUtc, c.Open, c.High, c.Low, c.Close, c.Volume)).ToArray()), StrictJson);
        if (payload.Length > MaximumEntryBytes) throw new IOException("Cache entry budget exceeded.");
        // Serialize all writers for this root across processes; hold the OS handle throughout replacement/cleanup.
        using var budgetDeadline = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken); budgetDeadline.CancelAfter(TimeSpan.FromSeconds(1));
        FileStream? gate = null;
        try
        {
            while (gate is null)
            {
                budgetDeadline.Token.ThrowIfCancellationRequested();
                try { gate = new FileStream(Path.Combine(directory, ".cache-write.lock"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None); }
                catch (IOException) { await Task.Delay(25, budgetDeadline.Token); }
            }
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested) { throw new IOException("Cache is busy."); }
        using (gate)
        {
            ValidatePublicPath(directory);
            var files = Directory.EnumerateFiles(directory).Select(p => new FileInfo(p)).ToArray();
            if (files.Any(f => f.LinkTarget is not null)) throw new IOException("Linked cache entries are forbidden.");
            // Temp writers use the same root gate, so leftovers belong to completed or abandoned writers.
            foreach (var file in files.Where(f => f.Extension == ".tmp")) file.Delete();
            foreach (var file in files.Where(f => f.Extension == ".json" && (clock.GetUtcNow().UtcDateTime - f.LastWriteTimeUtc >= TimeSpan.FromHours(24) || f.Length > MaximumEntryBytes))) file.Delete();
            var target = Path.Combine(directory, entry.Key.Fingerprint + ".json");
            var retained = Directory.EnumerateFiles(directory, "*.json").Select(p => new FileInfo(p)).Where(f => f.FullName != target).OrderByDescending(f => f.LastWriteTimeUtc).ToArray();
            foreach (var file in retained.Skip(retentionLimit - 1)) file.Delete();
            var total = Directory.EnumerateFiles(directory).Sum(p => new FileInfo(p).Length);
            if (total + payload.Length > MaximumDiskBytes) throw new IOException("Cache disk budget exceeded.");
            var temporary = Path.Combine(directory, "." + Guid.NewGuid().ToString("N") + ".tmp");
            try
            {
                await using (var stream = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.WriteThrough))
                { await stream.WriteAsync(payload, cancellationToken); await stream.FlushAsync(cancellationToken); stream.Flush(true); }
                cancellationToken.ThrowIfCancellationRequested(); File.Move(temporary, target, true);
                File.SetLastWriteTimeUtc(target, clock.GetUtcNow().UtcDateTime);
            }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }
    }
    private static void RejectDuplicates(JsonElement value)
    {
        if (value.ValueKind == JsonValueKind.Object)
        {
            var names = new HashSet<string>(StringComparer.Ordinal);
            foreach (var property in value.EnumerateObject()) { if (!names.Add(property.Name)) throw new JsonException("Duplicate property."); RejectDuplicates(property.Value); }
        }
        else if (value.ValueKind == JsonValueKind.Array) foreach (var item in value.EnumerateArray()) RejectDuplicates(item);
    }
    private static void RequireFields(JsonElement value, params string[] fields)
    {
        if (value.ValueKind != JsonValueKind.Object) throw new JsonException("Expected cache object.");
        var expected = new HashSet<string>(fields, StringComparer.Ordinal);
        foreach (var property in value.EnumerateObject()) if (!expected.Remove(property.Name)) throw new JsonException("Unknown cache property.");
        if (expected.Count != 0) throw new JsonException("Missing cache property.");
    }
    private sealed record PublicRecord(HistoricalMarketDataCacheKey Key, int DeadlineSeconds, DateTimeOffset CachedAtUtc, DateTimeOffset GeneratedAtUtc, SyntheticMarketDataIdentity Identity, IReadOnlyList<Row> Candles);
    private static Record FromEntry(HistoricalMarketDataCacheEntry entry) => new(entry.Key.Symbol, entry.Key.Interval, entry.Key.Range, entry.Key.ProviderIdentity, entry.Key.ContractVersion, entry.Request.Deadline, entry.CachedAtUtc, entry.Provenance.AcquiredAtUtc, entry.Provenance.LastValidatedAtUtc, entry.Provenance.IsStale, entry.Provenance.ContainsOnlyClosedCandles, entry.Candles.Select(static c => new Row(c.OpenTimeUtc, c.Open, c.High, c.Low, c.Close, c.Volume)).ToArray());
    private static HistoricalMarketDataCacheEntry ToEntry(Record r) { var key = new HistoricalMarketDataCacheKey(r.Symbol, r.Interval, r.Range, r.Provider, r.Version); var request = new HistoricalMarketDataRequest(r.Symbol, r.Interval, r.Range, r.Deadline); var provenance = new HistoricalMarketDataProvenance(r.Provider, r.Acquired, r.Validated, r.IsStale, r.Closed); return new HistoricalMarketDataCacheEntry(key, request, r.Candles.Select(c => new HistoricalCandle(r.Symbol, r.Interval, c.OpenTimeUtc, c.Open, c.High, c.Low, c.Close, c.Volume)), provenance, r.Cached); }
    private sealed record Record(CanonicalMarketSymbol Symbol, HistoricalCandleInterval Interval, HistoricalCandleRange Range, string Provider, string Version, TimeSpan Deadline, DateTimeOffset Cached, DateTimeOffset Acquired, DateTimeOffset Validated, bool IsStale, bool Closed, IReadOnlyList<Row> Candles);
    private sealed record Row(DateTimeOffset OpenTimeUtc, decimal Open, decimal High, decimal Low, decimal Close, decimal Volume);
}
