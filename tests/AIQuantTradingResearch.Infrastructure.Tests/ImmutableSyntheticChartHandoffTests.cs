using System.Text.Json;
using AIQuantTradingResearch.Worker;
using Xunit;

namespace AIQuantTradingResearch.Infrastructure.Tests;

public sealed class ImmutableSyntheticChartHandoffTests
{
    [Fact]
    public async Task AllTwelveMembersAreDeterministicVisibleCSharpPayloadsWithContentBoundManifest()
    {
        var first = Path.Combine(Path.GetTempPath(), "aiq-handoff-test-" + Guid.NewGuid().ToString("N"));
        var second = first + "-repeat";
        try
        {
            var manifest = await ImmutableSyntheticChartHandoff.WriteAsync(first);
            Assert.Equal(manifest, await ImmutableSyntheticChartHandoff.WriteAsync(second));
            Assert.Equal(13, Directory.GetFiles(first).Length);
            using var doc = JsonDocument.Parse(manifest);
            Assert.Equal(ImmutableSyntheticChartHandoff.Schema, doc.RootElement.GetProperty("schema").GetString());
            Assert.Equal(114, doc.RootElement.GetProperty("seed").GetInt32());
            foreach (var member in doc.RootElement.GetProperty("files").EnumerateArray())
            {
                var name = member.GetProperty("name").GetString()!;
                var bytes = await File.ReadAllBytesAsync(Path.Combine(first, name));
                Assert.Equal(bytes, await File.ReadAllBytesAsync(Path.Combine(second, name)));
                Assert.Equal(bytes.Length, member.GetProperty("bytes").GetInt32());
                Assert.Equal(ImmutableSyntheticChartHandoff.Hash(bytes), member.GetProperty("sha256").GetString());
                Assert.Equal((byte)'\n', bytes[^1]); Assert.DoesNotContain((byte)'\r', bytes);
                using var data = JsonDocument.Parse(bytes);
                var p = data.RootElement;
                Assert.Equal(250, p.GetProperty("warmup").GetInt32());
                Assert.False(p.GetProperty("publicRuntimeVerified").GetBoolean());
                Assert.True(LocalSyntheticChart.TrySelection(p.GetProperty("interval").GetString()!, p.GetProperty("range").GetString()!, out var interval, out var range));
                // Independently re-execute the established qualified C# payload path, not Python arithmetic.
                var expected = await LocalSyntheticChart.CreatePayloadAsync(interval, range);
                var rows = p.GetProperty("candles"); Assert.Equal(expected.Candles.Count, rows.GetArrayLength());
                Assert.Equal(rows.GetArrayLength() + 250, p.GetProperty("calculationCount").GetInt32());
                for (var i = 0; i < rows.GetArrayLength(); i++)
                {
                    Assert.Equal(expected.Candles[i].TimeUtc, rows[i].GetProperty("timeUtc").GetString());
                    Assert.Equal(expected.Candles[i].Sma20, rows[i].GetProperty("sma20").GetString());
                    Assert.Equal(expected.Candles[i].Volume, rows[i].GetProperty("volume").GetString());
                }
            }
            await Assert.ThrowsAsync<IOException>(() => ImmutableSyntheticChartHandoff.WriteAsync(first));
        }
        finally { if (Directory.Exists(first)) Directory.Delete(first, true); if (Directory.Exists(second)) Directory.Delete(second, true); }
    }
}
