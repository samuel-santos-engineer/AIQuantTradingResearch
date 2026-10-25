using System.Globalization;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

/// <summary>Private, caller-driven WP03 store. No public endpoint, provider acquisition or calculated readiness.</summary>
public sealed class SqliteResearchStateStore : IResearchCommittedStateReader, IResearchStateWriter, IResearchTransformStateReader, IDisposable
{
    private readonly object sync = new();
    private readonly ResearchWriterOwnership ownership;
    private readonly string database;
    private readonly string watermark;
    private readonly Action<string>? fault;
    private readonly bool calculateResearch;
    private bool publication;
    private bool recovery;
    private bool disposed;
    private ResearchIntegrity protection;
    public SqliteResearchStateStore(string privateDirectory) : this(privateDirectory, false, null) { }
    public SqliteResearchStateStore(string privateDirectory, bool calculateResearch) : this(privateDirectory, calculateResearch, null) { }
    public SqliteResearchStateStore(string privateDirectory, bool calculateResearch, Guid initialGeneration) : this(privateDirectory, calculateResearch, null, initialGeneration) { }
    internal SqliteResearchStateStore(string privateDirectory, Action<string>? fault) : this(privateDirectory, false, fault) { }
    internal SqliteResearchStateStore(string privateDirectory, bool calculateResearch, Action<string>? fault, Guid? initialGeneration = null)
    {
        if (initialGeneration == Guid.Empty) throw new ArgumentException("Invalid research generation.");
        this.fault = fault; this.calculateResearch = calculateResearch;
        try { ownership = new ResearchWriterOwnership(privateDirectory); }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException) { throw new ResearchUnavailableException(); }
        database = Path.Combine(ownership.DirectoryPath, "research.sqlite");
        watermark = Path.Combine(ownership.DirectoryPath, "watermark.json");
        try
        {
            ownership.ProtectFiles();
            var exists = File.Exists(database);
            if (!exists && File.Exists(watermark)) throw new ResearchUnavailableException();
            using var connection = Open();
            using var transaction = connection.BeginTransaction();
            if (!exists) Initialize(connection, transaction, initialGeneration ?? Guid.NewGuid());
            ValidateStates(connection, transaction);
            var (generation, revision) = Metadata(connection, transaction);
            if (initialGeneration is not null && generation != initialGeneration) throw new ResearchUnavailableException();
            var witness = ResearchStateRecovery.Validate(connection, transaction, generation, revision, watermark);
            transaction.Commit();
            UpdateWitness(witness);
            ownership.ProtectFiles();
        }
        catch (Exception e) when (e is SqliteException or IOException or UnauthorizedAccessException or JsonException or ArgumentException or InvalidOperationException or KeyNotFoundException or FormatException)
        { ownership.Dispose(); throw new ResearchUnavailableException(); }
        catch { ownership.Dispose(); throw; }
    }
    private SqliteConnection Open()
    {
        ObjectDisposedException.ThrowIf(disposed, this);
        var connection = new SqliteConnection(new SqliteConnectionStringBuilder { DataSource = database, Pooling = false, DefaultTimeout = 1 }.ToString());
        try
        {
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "PRAGMA journal_mode=WAL";
            if (!string.Equals(cmd.ExecuteScalar()?.ToString(), "wal", StringComparison.OrdinalIgnoreCase)) throw new ResearchUnavailableException();
            cmd.CommandText = "PRAGMA synchronous=FULL; PRAGMA foreign_keys=ON; PRAGMA busy_timeout=1000;";cmd.ExecuteNonQuery();
            cmd.CommandText = "PRAGMA synchronous";if (Convert.ToInt64(cmd.ExecuteScalar(), CultureInfo.InvariantCulture) != 2) throw new ResearchUnavailableException();
            return connection;
        }
        catch { connection.Dispose(); throw; }
    }
    private static void Execute(SqliteConnection c, SqliteTransaction t, string sql, params (string, object?)[] parameters)
    {
        using var cmd = c.CreateCommand();cmd.Transaction = t;cmd.CommandText = sql;
        foreach (var (key, value) in parameters) cmd.Parameters.AddWithValue(key, value ?? DBNull.Value);
        cmd.ExecuteNonQuery();
    }
    private static (Guid Generation, long Revision) Metadata(SqliteConnection c, SqliteTransaction t)
    {
        using var cmd = c.CreateCommand();cmd.Transaction = t;cmd.CommandText = "SELECT generation,revision,schema_version FROM metadata WHERE id=1";
        using var r = cmd.ExecuteReader();
        if (!r.Read() || !Guid.TryParseExact(r.GetString(0), "D", out var g) || g == Guid.Empty || r.GetInt64(1) < 0 || r.GetInt32(2) != 1)
            throw new ResearchUnavailableException();
        return (g, r.GetInt64(1));
    }
    private static void Initialize(SqliteConnection c, SqliteTransaction t, Guid generation)
    {
        Execute(c, t, ResearchStateSchema.Sql);
        Execute(c, t, "INSERT INTO metadata VALUES(1,1,$g,0)", ("$g", generation.ToString("D")));
        foreach (var instrument in Enum.GetValues<ResearchInstrument>())
        {
            Execute(c, t, "INSERT INTO instruments VALUES($i,0,NULL,NULL,'[]')", ("$i", (int)instrument));
            var checkpoint = Checkpoint(0, "[]");
            Execute(c, t, "INSERT INTO checkpoints VALUES($i,0,$p,$h)", ("$i", (int)instrument), ("$p", checkpoint), ("$h", ResearchStateRecovery.Hash(checkpoint)));
        }
        var data = ResearchStateRecovery.DataHash(c, t);var hash = ResearchStateRecovery.Chain(generation, 0, "", data);
        Execute(c, t, "INSERT INTO commits VALUES(0,'',$d,$h)", ("$d", data), ("$h", hash));
    }
    private static string Checkpoint(long version, string payload) => JsonSerializer.Serialize(new { Version = version, StateHash = ResearchStateRecovery.Hash(payload), Algorithm = "wp03-raw-unavailable-v1" });
    private static void ValidateStates(SqliteConnection c, SqliteTransaction t)
    {
        var (generation, _) = Metadata(c, t);
        foreach (var i in Enum.GetValues<ResearchInstrument>()) _ = Load(c, t, generation, i, ResearchIntegrity.Protected);
        using var cmd = c.CreateCommand();cmd.Transaction = t;
        cmd.CommandText = "SELECT c.version,i.version,c.payload FROM checkpoints c JOIN instruments i USING(instrument)";
        using var reader = cmd.ExecuteReader();var count = 0;
        while (reader.Read())
        {
            count++;
            if (reader.GetInt64(0) < 0 || reader.GetInt64(0) > reader.GetInt64(1) || reader.GetInt64(1) - reader.GetInt64(0) > 4) throw new ResearchUnavailableException();
            using var checkpoint = JsonDocument.Parse(reader.GetString(2));
            var algorithm = checkpoint.RootElement.GetProperty("Algorithm").GetString();
            if (checkpoint.RootElement.GetProperty("Version").GetInt64() != reader.GetInt64(0) ||
                algorithm is not ("wp03-raw-unavailable-v1" or PrivateResearchCalculator.Algorithm) ||
                algorithm == PrivateResearchCalculator.Algorithm && reader.GetInt64(0) != reader.GetInt64(1)) throw new ResearchUnavailableException();
        }
        if (count != 2) throw new ResearchUnavailableException();
    }
    private static DateTimeOffset? Time(SqliteDataReader r, int index) => r.IsDBNull(index) ? null : DateTimeOffset.ParseExact(r.GetString(index), "O", CultureInfo.InvariantCulture);
    private static ResearchCommittedState Load(SqliteConnection c, SqliteTransaction t, Guid generation, ResearchInstrument instrument, ResearchIntegrity integrity)
    {
        using var cmd = c.CreateCommand();cmd.Transaction = t;cmd.CommandText = "SELECT version,last_commit,last_poll,payload FROM instruments WHERE instrument=$i";cmd.Parameters.AddWithValue("$i", (int)instrument);
        using var r = cmd.ExecuteReader();if (!r.Read()) throw new ResearchUnavailableException();
        var payload = r.GetString(3);if (payload.Length > 4_194_304) throw new ResearchUnavailableException();
        var rows = JsonSerializer.Deserialize<ResearchStoredCandle[]>(payload) ?? throw new ResearchUnavailableException();
        var version = r.GetInt64(0);var commit = Time(r, 1);var poll = Time(r, 2);
        if (rows.Length > 1000 || version < 0 || (rows.Length > 0 && (version == 0 || commit is null || poll is null))) throw new ResearchUnavailableException();
        ResearchStoredCandle? previous = null;
        foreach (var row in rows)
        {
            if (row is null || row.Candle is null || row.Candle.Instrument != instrument || row.Segment == Guid.Empty || row.SegmentIndex < 1 ||
                poll is null || row.Candle.OpenTimeUtc.AddMinutes(1) > poll ||
                (previous is not null && (row.Candle.OpenTimeUtc <= previous.Candle.OpenTimeUtc ||
                    (row.Segment == previous.Segment && (row.SegmentIndex != previous.SegmentIndex + 1 || row.Candle.OpenTimeUtc != previous.Candle.OpenTimeUtc.AddMinutes(1))) ||
                    (row.Segment != previous.Segment && row.SegmentIndex != 1)))) throw new ResearchUnavailableException();
            previous = row;
        }
        using var cpCommand = c.CreateCommand();cpCommand.Transaction = t;
        cpCommand.CommandText = "SELECT payload FROM checkpoints WHERE instrument=$i";cpCommand.Parameters.AddWithValue("$i", (int)instrument);
        var cpText = cpCommand.ExecuteScalar() as string ?? throw new ResearchUnavailableException();
        if (cpText.Length > 524288) throw new ResearchUnavailableException();
        using var cpDocument = JsonDocument.Parse(cpText);
        ResearchCalculationCheckpoint? checkpoint = null;
        if (cpDocument.RootElement.GetProperty("Algorithm").GetString() == PrivateResearchCalculator.Algorithm)
        {
            checkpoint = JsonSerializer.Deserialize<ResearchCalculationCheckpoint>(cpText) ?? throw new ResearchUnavailableException();
            if (rows.Length == 0) throw new ResearchUnavailableException();
            PrivateResearchCalculator.ValidateCheckpoint(checkpoint, generation, instrument, version, rows[0].Candle.OpenTimeUtc, rows[^1].Candle.OpenTimeUtc);
        }
        if (checkpoint is null && rows.Any(x => x.Calculation is not null)) throw new ResearchUnavailableException();
        return new ResearchCommittedState(generation, instrument, version, rows, commit, poll, integrity, checkpoint);
    }
    public ResearchCommittedState Read(ResearchInstrument instrument)
    {
        lock (sync)
        {
            if (recovery || !Enum.IsDefined(instrument)) throw new ResearchUnavailableException();
            try
            {
                using var c = Open();using var t = c.BeginTransaction();var (g, revision) = Metadata(c, t);
                _ = ResearchStateRecovery.Validate(c, t, g, revision, watermark);
                var state = Load(c, t, g, instrument, protection);t.Commit();return state;
            }
            catch (Exception e) when (e is SqliteException or JsonException or ArgumentException or InvalidOperationException or IOException or FormatException or ResearchUnavailableException)
            { recovery = true;throw new ResearchUnavailableException(); }
        }
    }
    public ResearchCommittedState Commit(ResearchPollRequest request, ResearchPollBatch batch, Guid expectedGeneration, long expectedVersion, CancellationToken cancellationToken = default)
    {
        try { return CommitCore(request, batch, expectedGeneration, expectedVersion, cancellationToken); }
        catch (Exception e) when (e is SqliteException or IOException or UnauthorizedAccessException or JsonException or FormatException)
        { throw new ResearchUnavailableException(); }
    }
    private ResearchCommittedState CommitCore(ResearchPollRequest request, ResearchPollBatch batch, Guid expectedGeneration, long expectedVersion, CancellationToken cancellationToken)
    {
        ResearchInputValidation.Validate(request, batch);cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            if (recovery || publication) throw new ResearchUnavailableException();
            using var c = Open();using var t = c.BeginTransaction();var (g, revision) = Metadata(c, t);
            ResearchStateRecovery.Witness prior;
            try { prior = ResearchStateRecovery.Validate(c, t, g, revision, watermark); }
            catch { recovery = true;throw new ResearchUnavailableException(); }
            var state = Load(c, t, g, request.Instrument, protection);
            if (state.Checkpoint is not null && !calculateResearch) throw new ResearchUnavailableException();
            if (g != expectedGeneration || state.Version != expectedVersion || request.ObservedAtUtc < state.LastSuccessfulProviderPollAt) throw new ResearchUnavailableException();
            if (state.Candles.Count > 0 && batch.Candles.Any(x => x.OpenTimeUtc < state.Candles[0].Candle.OpenTimeUtc)) throw new ResearchUnavailableException();
            var rows = state.Candles.ToDictionary(x => x.Candle.OpenTimeUtc);DateTimeOffset? reset = null;var changed = false;
            foreach (var candle in batch.Candles)
            {
                if (rows.TryGetValue(candle.OpenTimeUtc, out var old) && old.Candle == candle) continue;
                if (state.Candles.Count > 0 && candle.OpenTimeUtc <= state.Candles[^1].Candle.OpenTimeUtc)
                    reset = reset is null || candle.OpenTimeUtc < reset ? candle.OpenTimeUtc : reset;
                rows[candle.OpenTimeUtc] = new ResearchStoredCandle(candle, Guid.Empty, 0);changed = true;
            }
            var ordered = rows.Values.OrderBy(x => x.Candle.OpenTimeUtc);
            var retained = (calculateResearch ? ordered : ordered.TakeLast(1000)).ToArray();ResearchStoredCandle? previous = null;
            var resetSegment = Guid.Empty;
            for (var index = 0; index < retained.Length; index++)
            {
                var row = retained[index];var inReset = reset is not null && row.Candle.OpenTimeUtc >= reset;
                if (inReset && resetSegment == Guid.Empty) resetSegment = Guid.NewGuid();
                if (inReset || row.Segment == Guid.Empty)
                {
                    var continuing = previous is not null && row.Candle.OpenTimeUtc == previous.Candle.OpenTimeUtc.AddMinutes(1) &&
                        (!inReset || previous.Segment == resetSegment);
                    var segment = continuing ? previous!.Segment : inReset ? resetSegment : Guid.NewGuid();
                    if (!continuing && inReset) { segment = Guid.NewGuid();resetSegment = segment; }
                    row = new ResearchStoredCandle(row.Candle, segment, continuing ? checked(previous!.SegmentIndex + 1) : 1);
                    retained[index] = row;
                }
                previous = row;
            }
            var version = changed ? checked(state.Version + 1) : state.Version;
            ResearchCalculationCheckpoint? projectedCheckpoint = state.Checkpoint;
            if (calculateResearch && changed)
            {
                var candidate = new ResearchCommittedState(g, request.Instrument, version, retained, request.ObservedAtUtc, request.ObservedAtUtc, protection);
                var projection = PrivateResearchCalculator.Project(candidate, state.Checkpoint, cancellationToken);
                retained = projection.Candles.ToArray();projectedCheckpoint = projection.Checkpoint;
                fault?.Invoke("projection");
            }
            var payload = JsonSerializer.Serialize(retained);
            if (payload.Length > 4_194_304) throw new ResearchUnavailableException();
            Execute(c, t, "UPDATE instruments SET version=$v,last_commit=$c,last_poll=$p,payload=$d WHERE instrument=$i",
                ("$v", version), ("$c", changed ? request.ObservedAtUtc.ToString("O", CultureInfo.InvariantCulture) : state.LastCommitAt?.ToString("O", CultureInfo.InvariantCulture)),
                ("$p", request.ObservedAtUtc.ToString("O", CultureInfo.InvariantCulture)), ("$d", payload), ("$i", (int)request.Instrument));
            if (changed && (calculateResearch || version % 5 == 0))
            {
                fault?.Invoke("checkpoint");
                var cp = projectedCheckpoint is null ? Checkpoint(version, payload) : JsonSerializer.Serialize(projectedCheckpoint);
                if (cp.Length > 524288) throw new ResearchUnavailableException();
                Execute(c, t, "UPDATE checkpoints SET version=$v,payload=$p,checksum=$h WHERE instrument=$i", ("$v", version), ("$p", cp), ("$h", ResearchStateRecovery.Hash(cp)), ("$i", (int)request.Instrument));
            }
            cancellationToken.ThrowIfCancellationRequested();fault?.Invoke("before-commit");
            revision = checked(revision + 1);var data = ResearchStateRecovery.DataHash(c, t);var hash = ResearchStateRecovery.Chain(g, revision, prior.Hash, data);
            Execute(c, t, "UPDATE metadata SET revision=$r WHERE id=1", ("$r", revision));
            Execute(c, t, "INSERT INTO commits VALUES($r,$p,$d,$h)", ("$r", revision), ("$p", prior.Hash), ("$d", data), ("$h", hash));
            // Keep at most128 chained commits. Never prune an unresolved watermark dependency.
            Execute(c, t, "DELETE FROM commits WHERE revision < $floor", ("$floor", Math.Max(0, revision - 127)));
            if (File.Exists(watermark))
            {
                var witness = JsonSerializer.Deserialize<ResearchStateRecovery.Witness>(File.ReadAllBytes(watermark)) ?? throw new ResearchUnavailableException();
                if (witness.Revision < revision - 127) throw new ResearchUnavailableException();
            }
            t.Commit(); // From here cancellation/witness failure must not report a failed SQLite commit.
            UpdateWitness(new ResearchStateRecovery.Witness(g, revision, hash));
            try { ownership.ProtectFiles(); } catch { recovery = true;throw new ResearchUnavailableException(); }
            return new ResearchCommittedState(g, request.Instrument, version, retained,
                changed ? request.ObservedAtUtc : state.LastCommitAt, request.ObservedAtUtc, protection, projectedCheckpoint);
        }
    }
    /// <summary>Explicit private provisioning/rotation, using existing writer ownership and recovery chain.</summary>
    public void ActivateTransform(ResearchTransformConfiguration configuration, long expectedTransformEpoch,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        cancellationToken.ThrowIfCancellationRequested();
        lock (sync)
        {
            if (recovery || publication) throw new ResearchUnavailableException();
            var committed = false;
            try
            {
                using var c = Open(); using var t = c.BeginTransaction(); var (g, revision) = Metadata(c, t);
                var prior = ResearchStateRecovery.Validate(c, t, g, revision, watermark);
                var active = ResearchStateRecovery.ReadTransform(c, t);
                if (configuration.Generation != g || expectedTransformEpoch != (active?.Configuration.TransformEpoch ?? 0))
                    throw new ResearchUnavailableException();
                if (active is not null && configuration.TransformEpoch == active.Value.Configuration.TransformEpoch &&
                    configuration.KeyEpoch == active.Value.Configuration.KeyEpoch &&
                    System.Security.Cryptography.CryptographicOperations.FixedTimeEquals(configuration.CopyKey(), active.Value.Configuration.CopyKey()))
                    return; // Exact reinstall is a no-op, not a rotation.
                if (expectedTransformEpoch == long.MaxValue || configuration.TransformEpoch != expectedTransformEpoch + 1 ||
                    active is not null && configuration.KeyEpoch == active.Value.Configuration.KeyEpoch)
                    throw new ResearchUnavailableException();
                revision = checked(revision + 1);
                Execute(c, t, ResearchStateSchema.TransformSql);
                Execute(c, t, "INSERT INTO transform_configuration VALUES(1,$g,$k,$e,$p,$a,$r) ON CONFLICT(id) DO UPDATE SET generation=$g,key_base64=$k,key_epoch=$e,transform_epoch=$p,algorithm=$a,activation_revision=$r",
                    ("$g", g.ToString("D")), ("$k", Convert.ToBase64String(configuration.CopyKey())),
                    ("$e", configuration.KeyEpoch.ToString("D")), ("$p", configuration.TransformEpoch),
                    ("$a", ResearchTransformConfiguration.Algorithm), ("$r", revision));
                var data = ResearchStateRecovery.DataHash(c, t); var hash = ResearchStateRecovery.Chain(g, revision, prior.Hash, data);
                Execute(c, t, "UPDATE metadata SET revision=$r WHERE id=1", ("$r", revision));
                Execute(c, t, "INSERT INTO commits VALUES($r,$p,$d,$h)", ("$r", revision), ("$p", prior.Hash), ("$d", data), ("$h", hash));
                Execute(c, t, "DELETE FROM commits WHERE revision < $floor", ("$floor", Math.Max(0, revision - 127)));
                cancellationToken.ThrowIfCancellationRequested(); fault?.Invoke("transform-before-commit");
                t.Commit(); committed = true;
                fault?.Invoke("transform-after-commit");
                UpdateWitness(new ResearchStateRecovery.Witness(g, revision, hash));
                if (protection != ResearchIntegrity.Protected) throw new ResearchUnavailableException();
                ownership.ProtectFiles();
            }
            catch (Exception e) when (e is SqliteException or IOException or UnauthorizedAccessException or JsonException or FormatException or ArgumentException or InvalidOperationException or ResearchUnavailableException)
            {
                if (committed) recovery = true;
                throw new ResearchUnavailableException();
            }
            catch { if (committed) recovery = true; throw; }
        }
    }

    public TResult UseTransformSnapshot<TResult>(ResearchInstrument instrument,
        Func<ResearchCommittedState, ResearchTransformConfiguration, TResult> stageAndCommit)
    {
        ArgumentNullException.ThrowIfNull(stageAndCommit);
        lock (sync)
        {
            if (recovery || publication || !Enum.IsDefined(instrument)) throw new ResearchUnavailableException();
            ResearchCommittedState state; ResearchTransformConfiguration configuration;
            try
            {
                using var c = Open(); using var t = c.BeginTransaction(); var (g, revision) = Metadata(c, t);
                _ = ResearchStateRecovery.Validate(c, t, g, revision, watermark);
                var active = ResearchStateRecovery.ReadTransform(c, t) ?? throw new ResearchUnavailableException();
                configuration = active.Configuration;
                state = Load(c, t, g, instrument, protection); t.Commit();
            }
            catch (Exception e) when (e is SqliteException or IOException or UnauthorizedAccessException or JsonException or FormatException or ArgumentException or InvalidOperationException or ResearchUnavailableException)
            { throw new ResearchUnavailableException(); }
            publication = true;
            try { return stageAndCommit(state, configuration); }
            finally { publication = false; }
        }
    }

    private void UpdateWitness(ResearchStateRecovery.Witness witness)
    {
        try { ResearchStateRecovery.WriteWitness(watermark, witness, fault);protection = ResearchIntegrity.Protected; }
        catch (Exception e) when (e is IOException or UnauthorizedAccessException) { protection = ResearchIntegrity.Degraded; }
    }
    public void Dispose() { lock (sync) { if (disposed) return;if (publication) throw new ResearchUnavailableException();disposed = true;ownership.Dispose(); } }
}
