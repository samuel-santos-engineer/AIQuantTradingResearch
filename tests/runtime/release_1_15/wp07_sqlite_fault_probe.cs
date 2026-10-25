// Test-only driver compiled offline as the existing friend test assembly; never packaged in an image.
using System;
using System.IO;
using System.Linq;
using System.Threading;
using AIQuantTradingResearch.Application.MarketData;
using AIQuantTradingResearch.Infrastructure.Persistence.Sqlite;
using Microsoft.Data.Sqlite;

internal static class SqliteFaultProbe
{
    private static readonly DateTimeOffset Start = new(2026, 10, 1, 0, 0, 0, TimeSpan.Zero);
    private static readonly Guid Generation = Guid.Parse("23456789-1234-4234-8234-123456789abc");
    private static readonly Guid Epoch = Guid.Parse("34567890-1234-4234-8234-123456789abc");
    private static CompletedResearchCandle Candle(int minute) => new(ResearchInstrument.BtcUsd, Start.AddMinutes(minute), 100, 101, 99, 100, 1);
    public static int Main(string[] args)
    {
        try
        {
            var mode = args[0]; var directory = mode == "new-store" ? "/private/control/never-created" : "/private/data";
            if (mode == "tamper") { File.WriteAllText(Path.Combine(directory, "watermark.json"), "{}"); return 0; }
            if (mode == "expect-recovery")
            {
                try { using var unexpected = new SqliteResearchStateStore(directory, true); return 1; }
                catch (ResearchUnavailableException) { Console.WriteLine("RECOVERY_REJECTED"); return 0; }
            }
            var armed = false;
            Action<string>? fault = mode == "crash" ? point =>
            {
                if (!armed || point != args[1]) return;
                File.WriteAllText("/private/control/ready", point);
                Thread.Sleep(Timeout.Infinite); // Harness kills this exact container after observing the marker.
            } : null;
            using var store = new SqliteResearchStateStore(directory, true, fault, Generation);
            armed = true;
            if (mode == "seed")
            {
                store.ActivateTransform(new(Generation, Enumerable.Range(0, 32).Select(x => (byte)x).ToArray(), Epoch, 1), 0);
                store.Commit(new(ResearchInstrument.BtcUsd, Start.AddDays(2), TimeSpan.Zero), new([Candle(0)]), Generation, 0);
            }
            else if (mode == "hold")
            {
                File.WriteAllText("/private/control/ready", "hold"); Thread.Sleep(Timeout.Infinite);
            }
            else if (mode == "crash")
            {
                if (args[1].StartsWith("transform-", StringComparison.Ordinal))
                    store.ActivateTransform(new(Generation, Enumerable.Repeat((byte)42, 32).ToArray(), Guid.Parse("45678901-1234-4234-8234-123456789abc"), 2), 1);
                else store.Commit(new(ResearchInstrument.BtcUsd, Start.AddDays(2), TimeSpan.Zero), new([Candle(1)]), Generation, 1);
                return 1; // Requested fault point was not reached.
            }
            else if (mode == "recover")
            {
                var state = store.Read(ResearchInstrument.BtcUsd);
                if (state.Generation != Generation || state.Version != long.Parse(args[1], System.Globalization.CultureInfo.InvariantCulture) || state.Integrity != ResearchIntegrity.Protected) return 1;
                if (store.UseTransformSnapshot(ResearchInstrument.BtcUsd, (_, c) => c.TransformEpoch) != 1) return 1;
                using var connection = new SqliteConnection("Data Source="+Path.Combine(directory,"research.sqlite")+";Pooling=False"); connection.Open();
                using var command = connection.CreateCommand(); command.CommandText = "PRAGMA integrity_check";
                if ((string?)command.ExecuteScalar() != "ok") return 1;
                command.CommandText = "PRAGMA wal_checkpoint(TRUNCATE)";
                using var reader = command.ExecuteReader(); if (!reader.Read() || reader.GetInt64(0) != 0) return 1;
            }
            else if (mode != "seed") return 1;
            Console.WriteLine("LOCAL_STATE_PASS"); return 0;
        }
        catch { Console.Error.WriteLine("LOCAL_STATE_REJECTED"); return 1; }
    }
}
