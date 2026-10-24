using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AIQuantTradingResearch.Application.MarketData;
using Microsoft.Data.Sqlite;

namespace AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;

internal static class ResearchStateRecovery
{
    internal sealed record Witness(Guid Generation, long Revision, string Hash);
    internal static string Hash(string value) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
    internal static string Chain(Guid generation, long revision, string previous, string data) =>
        Hash($"{generation:D}\n{revision.ToString(System.Globalization.CultureInfo.InvariantCulture)}\n{previous}\n{data}\n");
    internal static string DataHash(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var cmd = connection.CreateCommand(); cmd.Transaction = transaction;
        cmd.CommandText = "SELECT instrument,version,last_commit,last_poll,payload FROM instruments ORDER BY instrument";
        var rows = new List<string>();
        using (var reader = cmd.ExecuteReader()) while (reader.Read())
            rows.Add(JsonSerializer.Serialize(Enumerable.Range(0, 5).Select(i => reader.IsDBNull(i) ? null : Convert.ToString(reader.GetValue(i), System.Globalization.CultureInfo.InvariantCulture)).ToArray()));
        cmd.CommandText = "SELECT instrument,version,payload,checksum FROM checkpoints ORDER BY instrument";
        using (var reader = cmd.ExecuteReader()) while (reader.Read())
        {
            if (Hash(reader.GetString(2)) != reader.GetString(3)) throw new ResearchUnavailableException();
            rows.Add(JsonSerializer.Serialize(new[] { reader.GetInt64(0).ToString(System.Globalization.CultureInfo.InvariantCulture), reader.GetInt64(1).ToString(System.Globalization.CultureInfo.InvariantCulture), reader.GetString(2), reader.GetString(3) }));
        }
        var transform = ReadTransform(connection, transaction);
        if (transform is not null)
            rows.Add(JsonSerializer.Serialize(new[] { "D5", transform.Value.Configuration.Generation.ToString("D"),
                Convert.ToBase64String(transform.Value.Configuration.CopyKey()), transform.Value.Configuration.KeyEpoch.ToString("D"),
                transform.Value.Configuration.TransformEpoch.ToString(System.Globalization.CultureInfo.InvariantCulture),
                ResearchTransformConfiguration.Algorithm, transform.Value.Revision.ToString(System.Globalization.CultureInfo.InvariantCulture) }));
        return Hash(string.Join("\n", rows));
    }
    internal static (ResearchTransformConfiguration Configuration, long Revision)? ReadTransform(SqliteConnection connection, SqliteTransaction transaction)
    {
        using var cmd = connection.CreateCommand(); cmd.Transaction = transaction;
        cmd.CommandText = "SELECT count(*) FROM sqlite_master WHERE type='table' AND name='transform_configuration'";
        if (Convert.ToInt64(cmd.ExecuteScalar(), System.Globalization.CultureInfo.InvariantCulture) == 0) return null;
        cmd.CommandText = "SELECT id,generation,key_base64,key_epoch,transform_epoch,algorithm,activation_revision FROM transform_configuration";
        using var r = cmd.ExecuteReader();
        if (!r.Read()) return null;
        if (r.GetInt64(0) != 1 || !Guid.TryParseExact(r.GetString(1), "D", out var generation) || r.GetString(1) != generation.ToString("D") ||
            !Guid.TryParseExact(r.GetString(3), "D", out var epoch) || r.GetString(3) != epoch.ToString("D") ||
            r.GetString(2).Length != 44 || r.GetString(5) != ResearchTransformConfiguration.Algorithm || r.GetInt64(6) < 1)
            throw new ResearchUnavailableException();
        var key = Convert.FromBase64String(r.GetString(2));
        if (Convert.ToBase64String(key) != r.GetString(2)) throw new ResearchUnavailableException();
        var result = (new ResearchTransformConfiguration(generation, key, epoch, r.GetInt64(4)), r.GetInt64(6));
        if (r.Read()) throw new ResearchUnavailableException();
        return result;
    }
    internal static Witness Validate(SqliteConnection connection, SqliteTransaction transaction, Guid generation, long revision, string watermark)
    {
        using var cmd = connection.CreateCommand();cmd.Transaction = transaction;
        cmd.CommandText = "PRAGMA integrity_check";
        if ((string?)cmd.ExecuteScalar() != "ok") throw new ResearchUnavailableException();
        cmd.CommandText = "SELECT revision,previous_hash,data_hash,hash FROM commits ORDER BY revision";
        var commits = new Dictionary<long, string>(); string? previous = null; long last = -1;
        using (var reader = cmd.ExecuteReader()) while (reader.Read())
        {
            var index = reader.GetInt64(0);var before = reader.GetString(1);var data = reader.GetString(2);var hash = reader.GetString(3);
            if ((previous is not null && (before != previous || index != last + 1)) ||
                Chain(generation, index, before, data) != hash || (index == revision && data != DataHash(connection, transaction))) throw new ResearchUnavailableException();
            if (commits.Count >= 128) throw new ResearchUnavailableException();
            commits.Add(index, hash);previous = hash;last = index;
        }
        var transform = ReadTransform(connection, transaction);
        if (transform is not null && (transform.Value.Configuration.Generation != generation || transform.Value.Revision > revision))
            throw new ResearchUnavailableException();
        if (last != revision || previous is null) throw new ResearchUnavailableException();
        // Missing witness after any committed mutation is uncertain, never silently initialized.
        if (!File.Exists(watermark)) { if (revision != 0 || transform is not null) throw new ResearchUnavailableException(); }
        else
        {
            if (new FileInfo(watermark).Length > 1024) throw new ResearchUnavailableException();
            var witness = JsonSerializer.Deserialize<Witness>(File.ReadAllBytes(watermark)) ?? throw new ResearchUnavailableException();
            if (witness.Generation != generation || witness.Revision > revision ||
                transform is not null && witness.Revision < transform.Value.Revision || !commits.TryGetValue(witness.Revision, out var hash) || hash != witness.Hash)
                throw new ResearchUnavailableException();
        }
        return new Witness(generation, revision, previous);
    }
    internal static void WriteWitness(string path, Witness witness, Action<string>? fault)
    {
        var temporary = path + ".pending";
        try
        {
            fault?.Invoke("watermark");
            using (var file = new FileStream(temporary, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                if (!OperatingSystem.IsWindows()) File.SetUnixFileMode(temporary, UnixFileMode.UserRead | UnixFileMode.UserWrite);
                var bytes = JsonSerializer.SerializeToUtf8Bytes(witness);file.Write(bytes);file.Flush(true);
            }
            fault?.Invoke("watermark-replace");
            File.Move(temporary, path, true); // Same directory; no cross-file transaction/directory-fsync guarantee.
        }
        finally { if (File.Exists(temporary)) File.Delete(temporary); }
    }
}
