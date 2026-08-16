# Durable Persistence — SQLite

> Step-by-step operational guide for proving that AIQuantTradingResearch historical observations are durably persisted through the application persistence boundary into SQLite.

## Purpose

This guide demonstrates the second concrete operational outcome of the AIQuantTradingResearch real-data vertical slice:

```text
Observations acquired
        ↓
Application persistence use case
        ↓
SQLite persistence adapter
        ↓
historical_observations
        ↓
Durable observations verified
```

The focus of this guide is **durable persistence**.

Provider acquisition is covered separately in:

```text
docs/guides/real-provider-acquisition-twelve-data.md
```

That guide proves:

```text
Twelve Data
     ↓
Historical observations acquired
```

This guide starts from those acquired observations and proves:

```text
Historical observations
     ↓
SQLite durable persistence
```

The objective is not merely to confirm that the Worker completed successfully. The objective is to independently verify that accepted observations survive beyond the execution that acquired them.

---

## 1. Target Outcome

The target outcome is:

```text
Historical observations
        ↓
Persisted through the application boundary
        ↓
Stored in SQLite
        ↓
Worker process terminates
        ↓
Database reopened independently
        ↓
Observations still exist
```

A successful demonstration proves that historical observations are not held only in process memory.

They become durable platform data.

For the current real-provider vertical slice, the expected Worker output is equivalent to:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

A later equivalent execution can report:

```text
Persistence outcome: Idempotent
```

---

## 2. What This Guide Proves

Completing this guide should provide evidence for all of the following:

- the persistence boundary is reachable from the application flow;
- SQLite is configured as the concrete persistence technology;
- the database can be created and initialized;
- historical observations can be written successfully;
- persisted observations can be queried independently;
- persisted observations remain after the Worker process exits;
- equivalent repeated observations are handled idempotently;
- the logical historical identity is protected from incompatible replacement;
- persistence configuration remains external to source code.

This creates the operational proof:

```text
Application observation
        ↓
Persistence contract
        ↓
Infrastructure adapter
        ↓
SQLite
        ↓
Durable historical state
```

---

## 3. Prerequisites

Before starting, confirm:

- the AIQuantTradingResearch repository is available locally;
- the required .NET SDK is installed;
- the repository engineering baseline passes;
- a writable local directory is available for the SQLite database;
- the real-provider acquisition path can produce observations.

For the complete provider setup, follow:

```text
docs/guides/real-provider-acquisition-twelve-data.md
```

### Optional

Install the SQLite CLI if you want to inspect the database directly from the terminal:

```text
sqlite3
```

A SQLite database viewer can also be used.

---

## 4. Verify the Repository Baseline

From the repository root:

```powershell
git status
```

Then run:

```powershell
./eng/verify.ps1
```

Do not begin persistence troubleshooting until the repository baseline succeeds.

---

## 5. Understand the Persistence Boundary

The persistence demonstration should be understood as a boundary flow rather than as direct database access from the Worker.

Conceptually:

```text
Worker
   │
   ▼
Application execution
   │
   ▼
Historical observations
   │
   ▼
Persistence abstraction
   │
   ▼
SQLite infrastructure implementation
   │
   ▼
historical_observations
```

SQLite is an infrastructure detail.

The application should depend on the persistence contract rather than embedding SQLite-specific behavior into domain or application logic.

This separation matters because future persistence technologies should be replaceable without redesigning the core research model.

---

## 6. Create a Local Runtime Data Directory

From the repository root:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Resolve an absolute database path:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"
```

Display the path:

```powershell
$databasePath
```

Expected shape:

```text
C:\...\AIQuantTradingResearch\.local\data\market-data.db
```

The database is runtime data and should not be committed accidentally.

Check repository exclusions and always inspect:

```powershell
git status
```

before committing changes.

---

## 7. Configure the SQLite Database Path

The current persistence configuration requires:

```text
Persistence:DatabasePath
```

Using .NET environment-variable configuration, set:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Validate it:

```powershell
if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "Persistence__DatabasePath is not configured."
}

Write-Host "Database path: $env:Persistence__DatabasePath"
```

Persistence configuration should remain external to source code.

Do not hard-code a developer-specific absolute path into the repository.

---

## 8. Configure the Real Observation Source

The current Worker obtains observations through the Twelve Data provider integration.

Set the provider API key in the current PowerShell session:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Validate only that it exists:

```powershell
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Twelve Data API key configured."
```

Do not print the API key itself.

The provider is not the subject of this guide; it is simply the current source of real observations used to exercise the persistence path.

---

## 9. Start with a Clean Demonstration Database

For a deterministic first-persistence demonstration, remove the disposable local database if it already exists:

```powershell
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

Confirm:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
False
```

This establishes the initial state:

```text
No SQLite database
        ↓
No persisted observations
```

Use this reset procedure only for disposable local demonstration data.

Never use database deletion as a normal production-data lifecycle operation.

---

## 10. Execute the Worker

From the repository root:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

The current vertical slice should execute approximately this path:

```text
Worker
   ↓
Acquire historical observations
   ↓
Persistence use case
   ↓
SQLite adapter
   ↓
Schema initialization
   ↓
historical_observations
   ↓
Commit accepted observations
```

---

## 11. Confirm the First Persistence Outcome

A successful first run should report output equivalent to:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

The important line for this guide is:

```text
Persistence outcome: NewlyAccepted
```

This indicates that the observations were accepted as new historical state.

However, console output alone is not sufficient evidence of durability.

The next steps independently inspect the database.

---

## 12. Confirm the Database Exists

After the Worker exits:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

Inspect the file:

```powershell
Get-Item $env:Persistence__DatabasePath
```

This proves that a persistent database artifact exists outside the Worker process.

The state has moved from:

```text
Process memory
```

to:

```text
Filesystem-backed SQLite database
```

---

## 13. Open the Database Independently

If the SQLite CLI is installed:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

This is an important verification boundary.

At this point the Worker has already terminated.

The SQLite CLI is a separate process opening the durable artifact independently.

The verification path is therefore:

```text
Worker
   ↓
Persist
   ↓
Worker exits

Separate process
   ↓
Open SQLite database
   ↓
Read persisted state
```

---

## 14. Verify the Persistence Table

Inside SQLite:

```sql
.tables
```

Expected to include:

```text
historical_observations
```

Inspect its schema:

```sql
.schema historical_observations
```

This confirms that the expected durable persistence structure exists.

---

## 15. Query the Persisted Historical Observations

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY target, instant_utc_ticks;
```

For the current default Worker execution, the result should contain historical observations for:

```text
AAPL
```

The persisted representation preserves the important historical fields required by the current release, including:

- target;
- instant represented as UTC ticks;
- original offset in minutes;
- price represented as exact text.

---

## 16. Count the Persisted Observations

Run:

```sql
SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;
```

For the current Worker request, the expected result is equivalent to:

```text
AAPL|3
```

This independently verifies the acquisition-to-persistence result.

The proof is now:

```text
Worker reported 3 observations
        +
SQLite independently contains 3 AAPL observations
        =
Persistence verified
```

---

## 17. Verify Durability Across Process Lifetime

Exit SQLite:

```sql
.quit
```

At this point:

- the Worker is not running;
- the SQLite CLI is not running;
- the observations should still exist on disk.

Confirm the database file remains:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

Reopen it:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Query again:

```sql
SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;
```

The observations should still be present.

This is the key durability demonstration:

```text
Write observations
       ↓
Terminate process
       ↓
Reopen storage
       ↓
Read same observations
```

Durability is therefore demonstrated independently of the original application process.

---

## 18. Verify the Logical Historical Identity

The persistence model identifies a historical observation using the logical key:

```text
(target, instant_utc_ticks)
```

Conceptually:

```text
AAPL + observation instant
          ↓
Unique historical identity
```

This prevents the platform from treating the same target and historical instant as unrelated records.

Inspect the schema to confirm the database constraint representing this identity:

```sql
.schema historical_observations
```

This database-level enforcement complements application-level persistence semantics.

---

## 19. Demonstrate Idempotent Persistence

Exit SQLite if necessary:

```sql
.quit
```

Without deleting the database, run the Worker again:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

When the provider returns observations equivalent to already accepted historical observations, the persistence result can be:

```text
Persistence outcome: Idempotent
```

`Idempotent` is a successful outcome.

It means the platform recognizes that the same historical fact has already been accepted and does not need to create another independent record.

---

## 20. Verify That Idempotency Did Not Create Duplicates

Reopen the database:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Run:

```sql
SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;
```

For an equivalent repeated three-observation acquisition, the database should not contain duplicate rows merely because the workflow was executed twice.

The desired behavior is:

```text
First equivalent write
       ↓
NewlyAccepted

Second equivalent write
       ↓
Idempotent

Database
       ↓
One durable representation per logical observation
```

This property is essential for historical acquisition workflows that may be retried.

---

## 21. Understand Conflict Protection

Idempotency applies when an incoming observation represents the same accepted historical fact.

A different value for an already accepted logical historical identity is not equivalent.

Conceptually:

```text
Existing:
AAPL + instant T + price X

Incoming:
AAPL + instant T + price Y

X != Y
```

This represents incompatible historical information.

The platform should not silently overwrite previously accepted historical state.

The persistence workflow distinguishes this situation as a conflict.

The intended behavior is:

```text
Same identity + same value
        ↓
Idempotent

Same identity + incompatible value
        ↓
Conflict
```

This protects historical research reproducibility.

---

## 22. Why Immutable Historical Data Matters

Quantitative research depends on reproducible input data.

If historical observations were silently modified, the same strategy or experiment could produce different results later without an explicit explanation.

The persistence behavior therefore supports this principle:

```text
Accepted historical fact
        ↓
Stable historical state
        ↓
Repeatable research inputs
        ↓
Reproducible outcomes
```

Future data-correction workflows may require explicit provenance, revisions, or versioning.

Those capabilities should be modeled deliberately rather than implemented as silent overwrites.

---

## 23. Verify Persistence Without Relying on Worker Output

A useful engineering demonstration separates application reporting from storage verification.

### Application evidence

```text
Persistence outcome: NewlyAccepted
```

### Independent storage evidence

```sql
SELECT target, COUNT(*)
FROM historical_observations
GROUP BY target;
```

Expected:

```text
AAPL|3
```

### Durability evidence

Close every process, reopen the database, and execute the query again.

The combination is stronger than any single signal.

---

## 24. Optional Database Integrity Check

Inside SQLite, run:

```sql
PRAGMA integrity_check;
```

Expected:

```text
ok
```

This is not a substitute for application correctness, but it provides additional evidence that the SQLite database structure is readable and internally consistent.

---

## 25. Optional Detailed Inspection

Inspect all stored values:

```sql
SELECT
    rowid,
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY target, instant_utc_ticks;
```

This is useful when investigating:

- ordering;
- duplicate concerns;
- timestamp representation;
- price representation;
- provider-to-storage normalization.

Avoid editing database rows manually during a normal validation run.

The goal is to observe platform behavior, not manufacture a successful state.

---

## 26. Common Failure — Missing Persistence Configuration

If:

```text
Persistence:DatabasePath
```

is missing, the Worker should fail rather than silently select an unexpected storage location.

Set:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Then run again.

External configuration makes the runtime storage location explicit and testable.

---

## 27. Common Failure — Database Directory Does Not Exist

Ensure the parent directory exists:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Then reconstruct the path:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"
$env:Persistence__DatabasePath = $databasePath
```

Retry the Worker.

---

## 28. Common Failure — Database Cannot Be Opened

Possible causes include:

- invalid path;
- missing parent directory;
- filesystem permissions;
- another process holding an incompatible lock;
- corrupted local database;
- unsupported local filesystem behavior.

Check:

```powershell
$env:Persistence__DatabasePath
```

Then:

```powershell
Test-Path (Split-Path $env:Persistence__DatabasePath -Parent)
```

For disposable demonstration data only, you can reset the database:

```powershell
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

and rerun the Worker.

Do not delete valuable data as a troubleshooting shortcut.

---

## 29. Common Failure — No Rows Found

If the database exists but:

```sql
SELECT COUNT(*) FROM historical_observations;
```

returns zero, verify the complete upstream execution.

Persistence cannot store observations that were never successfully acquired and accepted by the application flow.

Check the Worker output for:

- provider failures;
- authentication failures;
- insufficient observations;
- application validation failures;
- persistence failures.

Use the acquisition guide when troubleshooting Twelve Data itself:

```text
docs/guides/real-provider-acquisition-twelve-data.md
```

---

## 30. Common Failure — Unexpected Duplicate Count

If the row count increases unexpectedly after an equivalent retry, inspect:

```sql
SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1;
```

The logical identity should prevent duplicate representations for the same:

```text
target + instant
```

If duplicates appear, treat that as a persistence correctness issue rather than normal behavior.

---

## 31. Capture Persistence Evidence

A strong persistence demonstration can contain two screenshots.

### Evidence 1 — Application result

Capture output equivalent to:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

### Evidence 2 — Independent database verification

Capture:

```sql
SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;
```

with output equivalent to:

```text
AAPL|3
```

Optionally capture a later execution showing:

```text
Persistence outcome: Idempotent
```

This provides a concise narrative:

```text
Observations produced
       ↓
Persistence accepted them
       ↓
Database independently verified them
       ↓
Retry did not duplicate them
```

---

## 32. Evidence Safety

Before committing screenshots or terminal transcripts:

- confirm the Twelve Data API key is not visible;
- remove unrelated environment-variable output;
- avoid exposing unnecessary local paths or usernames;
- do not commit the SQLite runtime database unless explicitly required by repository policy;
- inspect `git status`;
- inspect every evidence artifact before committing it.

Evidence should prove platform behavior without exposing secrets.

---

## 33. Suggested Evidence Location

If persistence evidence is committed to the repository, a possible structure is:

```text
assets/
└── evidence/
    └── durable-persistence/
        ├── sqlite-newly-accepted.png
        ├── sqlite-persisted-observations.png
        └── sqlite-idempotent-retry.png
```

Introduce evidence directories only when they add lasting project value.

---

## 34. Reset the Local Persistence Demonstration

For a clean local demonstration:

```powershell
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

Confirm:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
False
```

Run the Worker again:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

The persistence infrastructure should initialize the required database state again.

This demonstrates repeatable local bootstrap behavior.

---

## 35. Revalidate the Repository

After the persistence demonstration:

```powershell
git status
```

Confirm that the local database has not become an unintended repository change.

Then run:

```powershell
./eng/verify.ps1
```

The operational demonstration should leave the engineering baseline healthy.

---

## 36. Clear Runtime Configuration

When finished:

```powershell
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

This removes the values from the current PowerShell process.

It does not delete the database itself.

---

## 37. One-Pass Durable Persistence Checklist

- [ ] Start from a healthy repository state.
- [ ] Run `./eng/verify.ps1`.
- [ ] Create a writable local runtime-data directory.
- [ ] Configure `Persistence__DatabasePath`.
- [ ] Configure the Twelve Data API key for the current acquisition source.
- [ ] Remove any disposable previous demonstration database.
- [ ] Run the Worker.
- [ ] Confirm three observations were acquired.
- [ ] Confirm `Persistence outcome: NewlyAccepted`.
- [ ] Confirm the SQLite database exists.
- [ ] Open the database independently.
- [ ] Confirm `historical_observations` exists.
- [ ] Query the persisted observations.
- [ ] Confirm the expected AAPL observation count.
- [ ] Close and reopen SQLite.
- [ ] Confirm the observations still exist.
- [ ] Run the Worker again without deleting the database.
- [ ] Verify equivalent observations are handled idempotently.
- [ ] Confirm equivalent retries did not create duplicates.
- [ ] Run `PRAGMA integrity_check;` if desired.
- [ ] Confirm no secret or runtime database is accidentally staged.
- [ ] Run `./eng/verify.ps1` again.
- [ ] Clear secret environment variables.

---

## 38. Copy/Paste PowerShell Demonstration

Replace only the API-key placeholder.

```powershell
# Repository quality gate
./eng/verify.ps1

# Prepare disposable local persistence
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"

# Runtime configuration
$env:Persistence__DatabasePath = $databasePath
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"

# Validate configuration without printing the secret
if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "Persistence__DatabasePath is not configured."
}

if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Database path: $env:Persistence__DatabasePath"
Write-Host "Twelve Data API key configured."

# Clean first-run demonstration
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue

# Persist real historical observations
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

# Verify durable artifact
if (-not (Test-Path $env:Persistence__DatabasePath)) {
    throw "SQLite database was not created."
}

Get-Item $env:Persistence__DatabasePath

# Run again to exercise equivalent persistence behavior
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

# Repository safety and quality
git status
./eng/verify.ps1

# Clear runtime configuration
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

Database verification remains intentionally separate so that storage is inspected independently from the Worker process.

---

## 39. SQLite Verification Script

With `sqlite3` installed:

```powershell
sqlite3 $databasePath
```

Then:

```sql
.tables

.schema historical_observations

SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY target, instant_utc_ticks;

SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;

SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1;

PRAGMA integrity_check;

.quit
```

The expected high-level evidence is:

```text
historical_observations exists
AAPL observations exist
No duplicate logical observations exist
integrity_check = ok
```

---

## 40. Definition of Done

The durable-persistence demonstration is complete when:

```text
[PASS] Repository verification succeeds
[PASS] Persistence database path is supplied externally
[PASS] Worker obtains real historical observations
[PASS] Persistence reports NewlyAccepted for new observations
[PASS] SQLite database exists after Worker termination
[PASS] historical_observations exists
[PASS] Expected observations can be queried independently
[PASS] Observations remain after closing and reopening the database
[PASS] Equivalent repeated persistence is idempotent
[PASS] Equivalent retries do not create duplicate logical observations
[PASS] Database integrity can be verified
[PASS] No credentials are committed
[PASS] Runtime database is not accidentally committed
```

The final demonstrated outcome is:

```text
Historical observations
        ↓
Persistence application boundary
        ↓
SQLite infrastructure adapter
        ↓
historical_observations
        ↓
Worker terminates
        ↓
Database reopened
        ↓
Historical observations still available
        ↓
DURABLE PERSISTENCE PROVEN
```

---

## 41. Relationship to the Platform Roadmap

This guide demonstrates a foundational capability rather than the final data platform.

Durable historical storage enables later capabilities such as:

```text
Historical acquisition
        ↓
Durable persistence
        ↓
Data quality
        ↓
Research datasets
        ↓
Feature engineering
        ↓
Quantitative analytics
        ↓
AI/ML experiments
        ↓
Reproducible research outcomes
```

Without durable, reproducible inputs, later quantitative and AI results cannot be trusted.

SQLite is appropriate for the current bounded local platform stage because it allows the architecture and persistence semantics to be exercised without introducing unnecessary infrastructure.

Future storage technologies can evolve behind the persistence boundary as platform requirements grow.

---

## 42. Result

After completing this guide, the project can demonstrate two consecutive real capabilities.

### Guide 1 — Real provider acquisition

```text
Twelve Data
     ↓
Historical observations acquired
```

### Guide 2 — Durable persistence

```text
Historical observations
     ↓
SQLite
     ↓
Durable observations verified
```

Combined:

```text
Twelve Data
     ↓
Real historical observations
     ↓
Application boundaries
     ↓
SQLite
     ↓
Durable research data
```

This moves AIQuantTradingResearch beyond an architectural skeleton: the platform can acquire real external market data and preserve it as independently verifiable durable state.

---

## References

AIQuantTradingResearch repository:

https://github.com/samuel-santos-engineer/AIQuantTradingResearch

Related operational guide:

```text
docs/guides/real-provider-acquisition-twelve-data.md
```

SQLite documentation:

https://www.sqlite.org/docs.html
