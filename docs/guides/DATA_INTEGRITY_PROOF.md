# DATA INTEGRITY PROOF

> Step-by-step operational guide for proving that historical observations acquired from Twelve Data preserve their expected identity and values through normalization and durable SQLite persistence.

## Purpose

This guide demonstrates the next verification property of the AIQuantTradingResearch real-data vertical slice:

```text
Twelve Data
     ↓
Historical observations acquired
     ↓
Provider response normalized
     ↓
Application observation model
     ↓
SQLite persistence
     ↓
Persisted values independently inspected
     ↓
DATA INTEGRITY PROVEN
```

The objective is stronger than proving that acquisition succeeded or that rows exist in SQLite.

The objective is to answer:

> Did the platform persist the historical observations it actually acquired, without losing, duplicating, or unexpectedly changing the information required by the current model?

This guide complements:

```text
REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
        ↓
Can the platform acquire real observations?

DURABLE_PERSISTENCE_SQLITE.md
        ↓
Do accepted observations survive process termination?

IDEMPOTENCY_PROOF.md
        ↓
Can equivalent observations be replayed without duplicate history?

DATA_INTEGRITY_PROOF.md
        ↓
Does persisted history preserve the expected observation identity and values?
```

---

## 1. Target Outcome

The target proof is:

```text
Provider-backed acquisition
        ↓
N observations acquired
        ↓
N observations accepted by persistence
        ↓
N durable observations queried independently
        ↓
Expected target preserved
Expected timestamps preserved
Expected prices preserved
Expected observation count preserved
No duplicate logical identities
        ↓
DATA INTEGRITY: PASS
```

For a 100-observation demonstration:

```text
Acquired observations:             100
Persisted observations:            100
Distinct logical observations:     100
Duplicate logical observations:      0
Missing required values:             0
Unexpected targets:                  0
SQLite integrity check:             ok
```

The exact observation count may differ if the current Worker is configured for a smaller bounded request.

The proof remains the same.

---

## 2. What This Guide Proves

Completing this guide should provide evidence that:

- the expected target reaches persistence;
- the expected number of observations reaches durable storage;
- persisted historical identities are unique;
- required timestamps are present;
- required prices are present;
- timestamp representation can be inspected independently;
- price representation can be inspected independently;
- persisted observations remain queryable after the Worker exits;
- no unexpected target is introduced into the demonstration database;
- the SQLite database is internally readable and consistent.

This is an end-to-end integrity check across the currently implemented boundaries:

```text
External provider
      │
      ▼
Infrastructure acquisition adapter
      │
      ▼
Normalized observation
      │
      ▼
Application persistence boundary
      │
      ▼
Infrastructure persistence adapter
      │
      ▼
SQLite representation
```

---

## 3. What This Guide Does Not Claim

This guide does not claim cryptographic or forensic proof that every byte of the raw HTTP response is preserved.

The current architecture intentionally normalizes provider-specific data into platform observations.

Therefore the relevant integrity question is:

```text
Provider data required by the platform
        ↓
Normalized representation
        ↓
Persisted representation
        ↓
Semantically consistent historical observation
```

This guide also does not claim:

- provider correctness;
- exchange correctness;
- corporate-action correctness beyond the current provider request;
- full raw-response archival;
- provenance/version history not implemented by the current release;
- correction/revision workflows not implemented by the current release.

Those require separate capabilities and evidence.

---

## 4. Integrity Invariants

For this demonstration, use these invariants.

### Invariant 1 — Target

Every persisted observation belongs to the expected target.

For the current demonstration:

```text
target = AAPL
```

### Invariant 2 — Count

The number of persisted logical observations equals the expected acquisition count.

Example:

```text
acquired = 100
persisted = 100
```

### Invariant 3 — Logical identity uniqueness

Each:

```text
(target, instant_utc_ticks)
```

represents one logical historical observation.

Expected:

```text
duplicate logical history = 0
```

### Invariant 4 — Timestamp presence

Every persisted observation contains a valid persisted instant representation.

### Invariant 5 — Offset presence

The original offset representation required by the current persistence model is present.

### Invariant 6 — Price presence

Every persisted observation contains a price representation.

### Invariant 7 — Durability

The same persisted values remain queryable after the Worker terminates and the database is reopened.

---

## 5. Prerequisites

Before starting:

- clone or update the AIQuantTradingResearch repository;
- install the .NET SDK required by `global.json`;
- confirm the repository verification gate passes;
- have a valid Twelve Data API key;
- have a writable local directory for SQLite;
- have the SQLite CLI or another SQLite inspection tool available. If you use `sqlite3`, install and verify it with the [SQLite CLI Installation](SQLITE_CLI_INSTALLATION.md) guide.

The preceding operational guides should already work:

```text
docs/guides/REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
docs/guides/DURABLE_PERSISTENCE_SQLITE.md
docs/guides/IDEMPOTENCY_PROOF.md
```

---

## 6. Verify the Repository Baseline

From the repository root:

```powershell
git status
```

Then:

```powershell
./eng/verify.ps1
```

Do not interpret an integrity demonstration until the normal engineering baseline is healthy.

---

## 7. Create a Dedicated Integrity-Proof Database

Use a separate disposable database so that unrelated historical observations do not affect the evidence.

Create the directory:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Resolve the path:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\data-integrity-proof.db"
```

Configure persistence:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Display the safe path:

```powershell
Write-Host "Database path: $env:Persistence__DatabasePath"
```

A dedicated database gives the proof a known initial state:

```text
Before demonstration
        ↓
0 historical observations
```

---

## 8. Configure Twelve Data

Set the API key only in the current terminal session:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Validate without printing the secret:

```powershell
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Twelve Data API key configured."
```

Never include the API key in screenshots or committed files.

---

## 9. Establish the Expected Dataset

Record the expected target and observation count before execution.

For example:

```powershell
$expectedTarget = "AAPL"
$expectedObservationCount = 100
```

If the current Worker still requests a different fixed count, use that real count instead.

For example:

```powershell
$expectedObservationCount = 3
```

Do not claim 100 observations were validated when the executable path produced only 3.

The evidence must reflect the current implementation.

---

## 10. Start From a Clean Database

Delete only the disposable integrity-proof database:

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

Do this only before the demonstration.

---

# Part I — Acquire and Persist

## 11. Run the Provider-Backed Worker

From the repository root:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

The flow should be:

```text
Twelve Data
     ↓
time-series response
     ↓
Provider normalization
     ↓
Historical observations
     ↓
Persistence use case
     ↓
SQLite
```

Record the Worker output.

For a 100-observation demonstration, the expected high-level result is equivalent to:

```text
Target: AAPL
Observation count: 100
Persistence outcome: NewlyAccepted
```

For the currently bounded smaller demonstration, use the actual count reported by the Worker.

---

## 12. Confirm the Worker Completed Successfully

The first application-level checkpoint is:

```text
Expected target = reported target
Expected count  = reported observation count
Persistence     = successful
```

Example:

```text
Expected target:       AAPL
Reported target:       AAPL

Expected observations: 100
Reported observations: 100

Persistence outcome:   NewlyAccepted
```

Application output is evidence, but not sufficient by itself.

The remaining steps inspect the durable state independently.

---

# Part II — Inspect Durable Representation

## 13. Confirm the Database Exists

After the Worker exits:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

Then:

```powershell
Get-Item $env:Persistence__DatabasePath
```

The Worker can now remain stopped for the rest of the core verification.

---

## 14. Open SQLite Independently

Run:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Configure readable output:

```sql
.headers on
.mode column
```

This establishes an independent verification process:

```text
Worker terminated
       ↓
SQLite opened separately
       ↓
Durable state inspected
```

---

## 15. Confirm the Expected Table

Run:

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

The current durable representation includes fields corresponding to:

```text
target
instant_utc_ticks
offset_minutes
price_text
```

---

# Part III — Count Integrity

## 16. Verify Total Persisted Count

Run:

```sql
SELECT COUNT(*) AS persisted_observations
FROM historical_observations;
```

For the 100-observation demonstration:

```text
persisted_observations
----------------------
100
```

The first count invariant is:

```text
acquired count = persisted count
```

Example:

```text
100 = 100
```

---

## 17. Verify Count by Target

Run:

```sql
SELECT
    target,
    COUNT(*) AS persisted_observations
FROM historical_observations
GROUP BY target
ORDER BY target;
```

Expected:

```text
target  persisted_observations
------  ----------------------
AAPL    100
```

or the actual configured count.

This proves that the expected records belong to the expected target.

---

## 18. Verify There Are No Unexpected Targets

For the AAPL demonstration:

```sql
SELECT COUNT(*) AS unexpected_targets
FROM historical_observations
WHERE target <> 'AAPL';
```

Expected:

```text
unexpected_targets
------------------
0
```

A dedicated clean database makes this assertion meaningful.

---

# Part IV — Identity Integrity

## 19. Verify Distinct Logical Observation Count

The current logical historical identity is represented by:

```text
target + instant_utc_ticks
```

Run:

```sql
SELECT COUNT(*) AS distinct_logical_observations
FROM (
    SELECT
        target,
        instant_utc_ticks
    FROM historical_observations
    GROUP BY target, instant_utc_ticks
);
```

Expected for the 100-observation demonstration:

```text
distinct_logical_observations
-----------------------------
100
```

---

## 20. Verify Zero Duplicate Logical History

Run:

```sql
SELECT COUNT(*) AS duplicate_history
FROM (
    SELECT
        target,
        instant_utc_ticks
    FROM historical_observations
    GROUP BY target, instant_utc_ticks
    HAVING COUNT(*) > 1
);
```

Expected:

```text
duplicate_history
-----------------
0
```

The identity invariant is therefore:

```text
persisted rows
      =
distinct logical observations

100 = 100
```

with:

```text
duplicates = 0
```

---

## 21. Inspect Any Duplicate Identities

The previous query should return zero.

If it does not, inspect the actual identities:

```sql
SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1
ORDER BY target, instant_utc_ticks;
```

Expected:

```text
0 rows
```

Any result requires investigation.

---

# Part V — Required-Value Integrity

## 22. Verify Target Values Are Present

Run:

```sql
SELECT COUNT(*) AS missing_target
FROM historical_observations
WHERE target IS NULL
   OR TRIM(target) = '';
```

Expected:

```text
missing_target
--------------
0
```

---

## 23. Verify Timestamp Values Are Present

Run:

```sql
SELECT COUNT(*) AS missing_instant
FROM historical_observations
WHERE instant_utc_ticks IS NULL;
```

Expected:

```text
missing_instant
---------------
0
```

If the schema enforces this structurally, the query provides additional operational evidence.

---

## 24. Verify Price Values Are Present

Run:

```sql
SELECT COUNT(*) AS missing_price
FROM historical_observations
WHERE price_text IS NULL
   OR TRIM(price_text) = '';
```

Expected:

```text
missing_price
-------------
0
```

---

## 25. Inspect Offset Representation

Run:

```sql
SELECT
    MIN(offset_minutes) AS minimum_offset_minutes,
    MAX(offset_minutes) AS maximum_offset_minutes
FROM historical_observations;
```

This is an inspection step rather than a universal assertion that the offset must have one specific value.

The expected value depends on the normalized provider timestamps and current platform behavior.

The important point is that the persisted representation can be independently inspected.

---

## 26. Combined Missing-Value Query

Run:

```sql
SELECT
    SUM(CASE
        WHEN target IS NULL OR TRIM(target) = ''
        THEN 1 ELSE 0 END) AS missing_target,

    SUM(CASE
        WHEN instant_utc_ticks IS NULL
        THEN 1 ELSE 0 END) AS missing_instant,

    SUM(CASE
        WHEN price_text IS NULL OR TRIM(price_text) = ''
        THEN 1 ELSE 0 END) AS missing_price
FROM historical_observations;
```

Expected:

```text
missing_target  missing_instant  missing_price
--------------  ---------------  -------------
0               0                0
```

This is useful screenshot evidence.

---

# Part VI — Value Inspection

## 27. Inspect Persisted Observations

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY instant_utc_ticks
LIMIT 10;
```

Inspect:

- target;
- timestamp ordering;
- offset;
- price representation.

This is a human-readable sample of the normalized durable state.

---

## 28. Inspect the Earliest Observation

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY instant_utc_ticks ASC
LIMIT 1;
```

Record the result as:

```text
earliest persisted observation
```

---

## 29. Inspect the Latest Observation

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY instant_utc_ticks DESC
LIMIT 1;
```

Record the result as:

```text
latest persisted observation
```

Together, the earliest/latest queries help demonstrate the actual persisted historical range.

---

## 30. Verify Timestamp Ordering

Run:

```sql
SELECT
    COUNT(*) AS total,
    MIN(instant_utc_ticks) AS earliest_instant,
    MAX(instant_utc_ticks) AS latest_instant
FROM historical_observations;
```

For a non-empty historical dataset:

```text
earliest_instant < latest_instant
```

when more than one distinct observation is present.

This does not prove provider correctness, but it helps detect obviously malformed persisted history.

---

## 31. Verify Price Text Is Parseable as Numeric Data

SQLite can perform a useful operational sanity check:

```sql
SELECT COUNT(*) AS suspicious_price_values
FROM historical_observations
WHERE price_text IS NULL
   OR TRIM(price_text) = ''
   OR CAST(price_text AS REAL) <= 0;
```

For ordinary positive equity prices in this AAPL demonstration, expected:

```text
suspicious_price_values
-----------------------
0
```

This is a demonstration-specific sanity check, not a universal market-data rule for every future instrument or data type.

The platform's domain semantics remain authoritative.

---

# Part VII — Cross-Boundary Comparison

## 32. Compare Application Evidence With Storage Evidence

At this point, compare the Worker output with SQLite.

Example:

```text
WORKER

Target: AAPL
Observation count: 100
Persistence outcome: NewlyAccepted
```

Against:

```text
SQLITE

Target: AAPL
Persisted observations: 100
Distinct logical observations: 100
Duplicate history: 0
Missing target: 0
Missing instant: 0
Missing price: 0
```

The evidence should agree.

---

## 33. Stronger Provider-to-Storage Verification

If you capture a safe sample of the provider response during a controlled demonstration, select several observations such as:

```text
earliest
middle
latest
```

For each selected observation, compare the provider-derived normalized values with the persisted values.

Conceptually:

```text
Provider observation
target = AAPL
timestamp = T
price = P

        ↓ normalization

Platform observation
target = AAPL
instant = T'
price = P'

        ↓ persistence

SQLite
target = AAPL
instant_utc_ticks = T''
price_text = P''
```

The semantic values should represent the same platform observation after the intentional normalization and storage transformations.

Do not expect raw provider formatting to be identical to storage formatting.

For example:

```text
provider datetime string
```

may intentionally become:

```text
UTC ticks + offset minutes
```

Likewise, provider numeric formatting may intentionally become the platform's exact persisted price representation.

---

## 34. Avoid Comparing Raw Formatting as If It Were Semantics

This distinction is important.

Data integrity does not necessarily mean:

```text
raw provider JSON text
        ==
SQLite text
```

The platform performs normalization.

The correct comparison is:

```text
provider meaning
        ==
normalized platform meaning
        ==
persisted platform meaning
```

Examples of intentional representation changes can include:

- datetime string → typed instant;
- typed instant → UTC ticks plus offset;
- provider numeric representation → platform decimal representation;
- platform decimal → exact persistence text representation.

Integrity is preserved when the meaning required by the model is preserved.

---

# Part VIII — Durability Integrity

## 35. Close SQLite

Run:

```sql
.quit
```

At this point:

- the Worker has terminated;
- SQLite has terminated;
- the database remains on disk.

---

## 36. Reopen the Database

Run:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Then:

```sql
.headers on
.mode column
```

Run the count again:

```sql
SELECT COUNT(*) AS persisted_observations
FROM historical_observations;
```

Expected:

```text
100
```

or the actual demonstration count.

---

## 37. Re-run the Core Integrity Query

Run:

```sql
SELECT
    (SELECT COUNT(*)
     FROM historical_observations) AS total_rows,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
     )) AS distinct_logical_observations,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
         HAVING COUNT(*) > 1
     )) AS duplicate_history,

    (SELECT COUNT(*)
     FROM historical_observations
     WHERE target IS NULL
        OR TRIM(target) = '') AS missing_target,

    (SELECT COUNT(*)
     FROM historical_observations
     WHERE instant_utc_ticks IS NULL) AS missing_instant,

    (SELECT COUNT(*)
     FROM historical_observations
     WHERE price_text IS NULL
        OR TRIM(price_text) = '') AS missing_price;
```

For the 100-observation proof:

```text
total_rows  distinct_logical_observations  duplicate_history  missing_target  missing_instant  missing_price
----------  -----------------------------  -----------------  --------------  ---------------  -------------
100         100                            0                  0               0                0
```

This is the primary database integrity evidence.

---

# Part IX — SQLite Integrity

## 38. Run SQLite Integrity Check

Run:

```sql
PRAGMA integrity_check;
```

Expected:

```text
ok
```

This verifies SQLite's internal structural consistency.

It does **not** prove business-semantic correctness by itself.

It complements the application-specific checks above.

---

## 39. Exit SQLite

Run:

```sql
.quit
```

---

# Part X — Optional Idempotent Recheck

## 40. Replay the Same Dataset

If the acquisition window is stable enough to return the same logical observations, run the Worker again against the same database:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Equivalent observations should be handled idempotently.

Then query the database again.

Expected:

```text
total rows unchanged
distinct logical observations unchanged
duplicate history = 0
```

For a detailed idempotency demonstration, use:

```text
docs/guides/IDEMPOTENCY_PROOF.md
```

---

# Part XI — Evidence Capture

## 41. Recommended Evidence 1 — Acquisition

Capture safe Worker output showing:

```text
Target: AAPL
Observation count: 100
Persistence outcome: NewlyAccepted
```

Use the actual count if the Worker is configured differently.

Do not expose the provider API key.

---

## 42. Recommended Evidence 2 — Core Integrity Query

Capture:

```text
total_rows  distinct_logical_observations  duplicate_history  missing_target  missing_instant  missing_price
----------  -----------------------------  -----------------  --------------  ---------------  -------------
100         100                            0                  0               0                0
```

This is the strongest compact evidence from the persistence side.

---

## 43. Recommended Evidence 3 — Persisted Sample

Capture a small sample:

```sql
SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY instant_utc_ticks
LIMIT 5;
```

This demonstrates that the database contains real historical values rather than only a count.

---

## 44. Recommended Evidence 4 — SQLite Integrity

Capture:

```text
PRAGMA integrity_check;
ok
```

This should be supporting evidence, not the primary integrity claim.

---

## 45. Portfolio-Quality Summary

A concise demonstration can be presented as:

```text
DATA INTEGRITY PROOF

Provider-backed acquisition
---------------------------
Target: AAPL
Observations acquired: 100

Persistence verification
------------------------
Persisted rows:                100
Distinct logical observations: 100
Duplicate history:               0
Missing targets:                 0
Missing timestamps:              0
Missing prices:                  0

Durability
----------
Database closed and reopened
Same 100 observations available

SQLite integrity
----------------
ok

RESULT: PASS
```

---

# Part XII — Troubleshooting

## 46. Persisted Count Does Not Match Acquired Count

If:

```text
acquired != persisted
```

do not immediately classify the result as corruption.

First determine whether:

- some observations were already present;
- persistence classified observations as idempotent;
- the database was not clean before the run;
- the acquisition result changed;
- persistence rejected a conflict;
- application validation failed.

For the clean first-run integrity demonstration, use a dedicated empty database.

---

## 47. Unexpected Target Appears

Run:

```sql
SELECT
    target,
    COUNT(*)
FROM historical_observations
GROUP BY target;
```

If the clean AAPL demonstration contains another target, investigate:

- whether the database was actually clean;
- whether the Worker requested multiple targets;
- whether another process used the same database;
- whether target mapping is incorrect.

Do not delete evidence before understanding the cause.

---

## 48. Duplicate History Is Greater Than Zero

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1;
```

Investigate:

- schema constraints;
- timestamp normalization;
- persistence identity rules;
- transaction behavior;
- migration/schema mismatch;
- manual database modifications.

A duplicate logical identity is not expected in the current persistence model.

---

## 49. Missing Price Values

Inspect:

```sql
SELECT *
FROM historical_observations
WHERE price_text IS NULL
   OR TRIM(price_text) = '';
```

If rows are returned, determine whether the problem originated in:

```text
provider response
        ↓
normalization
        ↓
application observation
        ↓
persistence mapping
```

Do not patch the SQLite database manually to make the proof pass.

---

## 50. Provider Values Differ From Persisted Formatting

Determine whether the difference is representational or semantic.

Example representational transformation:

```text
2026-08-14 00:00:00
        ↓
typed timestamp
        ↓
UTC ticks + offset
```

That can preserve integrity.

A semantic difference would be something like:

```text
provider normalized price = 220.10
persisted price           = 221.90
```

for the same logical observation without an intentional transformation.

That requires investigation.

---

## 51. SQLite Integrity Check Fails

If:

```sql
PRAGMA integrity_check;
```

does not return:

```text
ok
```

preserve the database for investigation.

Do not immediately delete it.

A structural database problem is different from a business-semantic data mismatch.

---

# Part XIII — Repeatable Execution

## 52. One-Pass Checklist

- [ ] Run `./eng/verify.ps1`.
- [ ] Create a dedicated integrity-proof database path.
- [ ] Configure `Persistence__DatabasePath`.
- [ ] Configure `TwelveData__ApiKey`.
- [ ] Record the expected target.
- [ ] Record the expected observation count.
- [ ] Delete the disposable proof database before execution.
- [ ] Run the Worker.
- [ ] Record the reported target.
- [ ] Record the reported observation count.
- [ ] Confirm persistence succeeds.
- [ ] Confirm the SQLite database exists.
- [ ] Open SQLite independently.
- [ ] Confirm `historical_observations` exists.
- [ ] Confirm persisted count matches the expected clean-run count.
- [ ] Confirm only the expected target exists.
- [ ] Confirm distinct logical count matches total count.
- [ ] Confirm duplicate history = 0.
- [ ] Confirm missing target = 0.
- [ ] Confirm missing instant = 0.
- [ ] Confirm missing price = 0.
- [ ] Inspect a sample of persisted observations.
- [ ] Inspect earliest and latest observations.
- [ ] Close SQLite.
- [ ] Reopen SQLite.
- [ ] Re-run the core integrity query.
- [ ] Run `PRAGMA integrity_check;`.
- [ ] Capture safe evidence if desired.
- [ ] Inspect `git status`.
- [ ] Run `./eng/verify.ps1` again.
- [ ] Clear secret environment variables.

---

## 53. Compact PowerShell Execution

Replace only the API-key placeholder and adjust the expected count to the current executable configuration.

```powershell
# Repository baseline
./eng/verify.ps1

# Expected demonstration
$expectedTarget = "AAPL"
$expectedObservationCount = 100

# Runtime storage
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
$databasePath = (Resolve-Path ".local\data").Path + "\data-integrity-proof.db"

# External configuration
$env:Persistence__DatabasePath = $databasePath
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"

if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "Persistence__DatabasePath is not configured."
}

if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Expected target: $expectedTarget"
Write-Host "Expected observations: $expectedObservationCount"
Write-Host "Database: $env:Persistence__DatabasePath"
Write-Host "Twelve Data API key configured."

# Clean proof state
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue

# Acquire + normalize + persist
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

# Confirm durable artifact
if (-not (Test-Path $env:Persistence__DatabasePath)) {
    throw "SQLite database was not created."
}

Get-Item $env:Persistence__DatabasePath

# Repository safety
git status

# Revalidate
./eng/verify.ps1

# Clear secrets/config from this shell
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

---

## 54. Compact SQLite Verification

Open:

```powershell
sqlite3 $databasePath
```

Run:

```sql
.headers on
.mode column

SELECT
    (SELECT COUNT(*)
     FROM historical_observations) AS total_rows,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
     )) AS distinct_logical_observations,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
         HAVING COUNT(*) > 1
     )) AS duplicate_history,

    (SELECT COUNT(*)
     FROM historical_observations
     WHERE target IS NULL
        OR TRIM(target) = '') AS missing_target,

    (SELECT COUNT(*)
     FROM historical_observations
     WHERE instant_utc_ticks IS NULL) AS missing_instant,

    (SELECT COUNT(*)
     FROM historical_observations
     WHERE price_text IS NULL
        OR TRIM(price_text) = '') AS missing_price;

SELECT
    target,
    COUNT(*) AS persisted_observations
FROM historical_observations
GROUP BY target
ORDER BY target;

SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY instant_utc_ticks
LIMIT 5;

PRAGMA integrity_check;

.quit
```

For a clean 100-observation AAPL run, the desired result is:

```text
total_rows                    = 100
distinct_logical_observations = 100
duplicate_history             = 0
missing_target                = 0
missing_instant               = 0
missing_price                 = 0

AAPL persisted observations   = 100

SQLite integrity              = ok
```

---

## 55. Definition of Done

The data-integrity proof is complete when:

```text
[PASS] Repository baseline succeeds
[PASS] Demonstration starts from a clean dedicated database
[PASS] Real provider-backed acquisition succeeds
[PASS] Worker reports the expected target
[PASS] Worker reports the expected observation count
[PASS] Persistence succeeds

[PASS] SQLite contains the expected observation count
[PASS] SQLite contains only the expected demonstration target
[PASS] Total rows equal distinct logical observations
[PASS] Duplicate logical history = 0
[PASS] Missing target values = 0
[PASS] Missing timestamp values = 0
[PASS] Missing price values = 0

[PASS] Persisted observations can be inspected independently
[PASS] Persisted state survives closing and reopening SQLite
[PASS] Core integrity query produces the same result after reopen
[PASS] SQLite integrity check = ok

[PASS] No credentials are committed
[PASS] Repository verification still succeeds
```

---

## 56. Final Proof

The completed evidence should tell this story:

```text
Twelve Data
     │
     ▼
Real historical observations
     │
     ▼
Provider normalization
     │
     ▼
Application observations
     │
     ▼
SQLite persistence
     │
     ├── expected count
     ├── expected target
     ├── unique logical identities
     ├── timestamps present
     ├── prices present
     └── no duplicate history
     │
     ▼
Database closed
     │
     ▼
Database reopened
     │
     ▼
Same verified historical state
     │
     ▼
DATA INTEGRITY PROVEN
```

---

## 57. Platform Verification Chain

With this guide, the operational proof chain becomes:

```text
1. REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
   │
   └── Can the platform acquire real market observations?
                    ↓

2. DURABLE_PERSISTENCE_SQLITE.md
   │
   └── Do accepted observations survive process termination?
                    ↓

3. IDEMPOTENCY_PROOF.md
   │
   └── Can equivalent observations be replayed safely?
                    ↓

4. DATA_INTEGRITY_PROOF.md
   │
   └── Does durable history preserve the expected observation
       identity and values?
```

Together:

```text
REAL DATA
    ↓
DURABLE DATA
    ↓
RETRY-SAFE DATA
    ↓
VERIFIED DATA
```

This provides a progressively stronger assessment of the currently executable AIQuantTradingResearch data platform.

---

## References

Related guides:

```text
docs/guides/REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
docs/guides/DURABLE_PERSISTENCE_SQLITE.md
docs/guides/IDEMPOTENCY_PROOF.md
```

AIQuantTradingResearch repository:

https://github.com/samuel-santos-engineer/AIQuantTradingResearch

Twelve Data documentation:

https://twelvedata.com/docs

SQLite documentation:

https://www.sqlite.org/docs.html
