using System.Globalization;
using System.Text.Json;
using Microsoft.Data.Sqlite;

// Deliberately fake fixture host. Not a production research/recovery implementation.
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://127.0.0.1:52731");
builder.Logging.ClearProviders();
var app = builder.Build();
var store = new FixtureStore("/private");
store.Commit();
app.MapGet("/health", () => Results.Json(new { fixtureOnly = true, ready = true, version = store.Version }));
app.MapGet("/snapshot", (string? instrument) => instrument is "BTC/USD" or "ETH/USD"
    ? Results.Json(store.Snapshot(instrument)) : Results.BadRequest(new { error = "INVALID_INSTRUMENT" }));
using var timer = new Timer(_ => store.Commit(), null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
Console.WriteLine(JsonSerializer.Serialize(new { role = "research", fixtureOnly = true, startup = store.Recovery }));
await app.RunAsync();

sealed class FixtureStore
{
    private readonly object gate = new();
    private readonly string folder;
    private readonly string connection;
    private long version;
    public long Version => Interlocked.Read(ref version);
    public string Recovery { get; }
    public FixtureStore(string path)
    {
        folder = path;
        connection = "Data Source=" + Path.Combine(path, "fixture.sqlite");
        using var db = Open();
        using var schema = db.CreateCommand();
        schema.CommandText = "PRAGMA journal_mode=DELETE; CREATE TABLE IF NOT EXISTS observations(instrument TEXT, n INTEGER, raw_fixture REAL, PRIMARY KEY(instrument,n)); CREATE TABLE IF NOT EXISTS commits(version INTEGER);";
        schema.ExecuteNonQuery();
        using var integrity = db.CreateCommand(); integrity.CommandText = "PRAGMA integrity_check";
        if ((string?)integrity.ExecuteScalar() != "ok") throw new InvalidOperationException("fixture_integrity_failed");
        using var latest = db.CreateCommand(); latest.CommandText = "SELECT COALESCE(MAX(version),0) FROM commits";
        version = Convert.ToInt64(latest.ExecuteScalar(), CultureInfo.InvariantCulture);
        var witnessPath = Path.Combine(folder, "watermark");
        var witness = File.Exists(witnessPath) ? long.Parse(File.ReadAllText(witnessPath), CultureInfo.InvariantCulture) : 0;
        if (witness > version) throw new InvalidOperationException("fixture_watermark_ahead");
        Recovery = witness < version ? "fixture_db_ahead_integrity_only" : "fixture_consistent";
    }
    private SqliteConnection Open() { var db = new SqliteConnection(connection); db.Open(); return db; }
    public void Commit()
    {
        lock (gate)
        {
            var next = checked(version + 1);
            using var db = Open(); using var tx = db.BeginTransaction();
            foreach (var instrument in new[] { "BTC/USD", "ETH/USD" })
            {
                using var command = db.CreateCommand(); command.Transaction = tx;
                command.CommandText = "INSERT OR REPLACE INTO observations(instrument,n,raw_fixture) VALUES($s,$n,$p)";
                command.Parameters.AddWithValue("$s",instrument); var n=command.Parameters.Add("$n",SqliteType.Integer); var price=command.Parameters.Add("$p",SqliteType.Real);
                for(var i=0;i<1000;i++) { n.Value=i; price.Value=100+i%17; command.ExecuteNonQuery(); }
            }
            using var commit = db.CreateCommand(); commit.Transaction=tx;
            commit.CommandText="DELETE FROM commits; INSERT INTO commits(version) VALUES($v)"; commit.Parameters.AddWithValue("$v", next); commit.ExecuteNonQuery();
            tx.Commit();
            // In-memory publication strictly after database commit. This stub does NOT implement lineage proof.
            Interlocked.Exchange(ref version,next);
            Replace("watermark",next.ToString(CultureInfo.InvariantCulture));
            if(next%5==0) Replace("checkpoint",next.ToString(CultureInfo.InvariantCulture));
        }
    }
    private void Replace(string name,string value) { var path=Path.Combine(folder,name); File.WriteAllText(path+".tmp",value); File.Move(path+".tmp",path,true); }
    public object Snapshot(string instrument)
    {
        lock(gate)
        {
            var origin=new DateTimeOffset(2026,10,20,0,0,0,TimeSpan.Zero);
            // Fake transformed DTO only. No real market data or indicators; never an HMAC qualification vector.
            var rows=Enumerable.Range(0,1000).Select(i=>new { time=origin.AddMinutes(i).ToString("yyyy-MM-ddTHH:mm:ssZ",CultureInfo.InvariantCulture),open=105m,high=107.1m,low=102.9m,close=106.05m,volume=10m }).ToArray();
            return new { fixtureOnly=true,instrument,version=Version,classification="Unavailable",rows };
        }
    }
}
