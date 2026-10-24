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
        return Hash(string.Join("\n", rows));
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
        if (last != revision || previous is null) throw new ResearchUnavailableException();
        // Missing witness after any committed mutation is uncertain, never silently initialized.
        if (!File.Exists(watermark)) { if (revision != 0) throw new ResearchUnavailableException(); }
        else
        {
            if (new FileInfo(watermark).Length > 1024) throw new ResearchUnavailableException();
            var witness = JsonSerializer.Deserialize<Witness>(File.ReadAllBytes(watermark)) ?? throw new ResearchUnavailableException();
            if (witness.Generation != generation || witness.Revision > revision || !commits.TryGetValue(witness.Revision, out var hash) || hash != witness.Hash)
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
